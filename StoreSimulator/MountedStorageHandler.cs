using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManager.Api;

namespace StoreSimulator
{
    class MountedStorageHandler
    {
        private ILogger MsConsoleLogger;
        private Computer computer;


        public  MountedStorageHandler(ILogger logger,Computer computer)
        {
            MsConsoleLogger = logger;
            this.computer = computer;
        }
        
        private void MountedMenuList()
        {
            string[] MountMenu = new string[] { "ADDFILE : Add file to a storage",
                                                "REMOVE :  Remove a file from the storage",
                                                "ARCHIVE : Copy all the files from one storage to another storage",
                                                "FORMAT : Format the storage",
                                                "SETDEFENSE : Set the write defense on the storage(Floppy,Dvd,Dvd-rw)",
                                                "OPEN : Open the write defense on the given storage(Dvd-rw)",
                                                "EXIT : Go back to the Storage menu",};
            foreach(string menuPoint in MountMenu)
            {
                Console.WriteLine(menuPoint);
            }
        }

        private Storage SelectMounted()
        {
            MsConsoleLogger.UserInput("Please choose a mounted storage by ID");
            string SelectedMountID = Console.ReadLine();
            
            for (int i = 0; i < computer.GetStorages().Count; i++)
            {
                if (computer.GetStorages()[i].Id == SelectedMountID)
                {
                    MsConsoleLogger.ConsoleInfo($"You chose the {computer.GetStorages()[i].StoreName} storage");
                    return computer.GetStorages()[i];
                }
                else if (computer.GetStorages()[i].Id != SelectedMountID && i == computer.GetStorages().Count - 1)
                {
                    break;
                }
            }
            Console.WriteLine();
            throw new Exception("there is no such storage on the computer");
            
        }
        public void MountedMenu()
        {
            if(computer.GetStorages().Count == 0)
            { 
                MsConsoleLogger.Error("Mount a storage first");
            }
            else
            {
                Storage ChoosedStorage = SelectMounted();
                while(true)
                {

                    MountedMenuList();
                    MsConsoleLogger.UserInput("G");
                }
            }
        }
    }
}
