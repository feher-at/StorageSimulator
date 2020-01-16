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
            Console.WriteLine();
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
                return;
                
            }
            else
            {
                Storage ChoosedStorage = SelectMounted();
                while (true)
                {

                    MountedMenuList();
                    MsConsoleLogger.UserInput("Choose a menu: ");
                    string answer = Console.ReadLine().ToLower();

                    if (answer == "addfile" || answer == "add file")
                    {

                        string[] fileParams = new string[] {  "file name",
                                                                "file size",
                                                                "only read(true/false)",
                                                                "system",
                                                                "hidden(true/false)"};
                        List<string> fp = new List<string>();
                        foreach (string param in fileParams)
                        {
                            MsConsoleLogger.UserInput($"Give me  {param}: ");
                            fp.Add(Console.ReadLine());
                        }
                        ChoosedStorage.AddFile(fp[0], Convert.ToInt32(fp[1]), Convert.ToBoolean(fp[2]), fp[3], Convert.ToBoolean(fp[4]));

                    }
                    else if (answer == "remove")
                    {
                        if (ChoosedStorage.GetFileList().Count == 0)
                        { MsConsoleLogger.Error("The storage is empty"); }
                        else
                        {
                            ChoosedStorage.listFile();
                            Console.WriteLine();
                            MsConsoleLogger.UserInput("Give me the file which you want to delete: ");
                            string fileYouWantToRemove = Console.ReadLine();
                            ChoosedStorage.Remove(fileYouWantToRemove);
                        }
                    }
                    else if (answer == "archive")
                    {
                        Storage secondStorage = SelectMounted();
                        computer.Archive(ChoosedStorage, secondStorage);
                    }
                    else if (answer == "format") 
                    {
                        ChoosedStorage.Format();
                        MsConsoleLogger.ConsoleInfo($"The {ChoosedStorage.StoreName} has been formatted");

                    }
                }
            }
        }
    }
}
