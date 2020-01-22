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
            string[] MountMenu = new string[] { "ADDFILE    : Add file to a storage",
                                                "CHECKCAP   : List the storage max capacity free capacity and the reserved capacity",
                                                "LISTFILE   : List the storage's file(s)",
                                                "REMOVE     : Remove a file from the storage",
                                                "ARCHIVE    : Copy all the files from one storage to another storage",
                                                "FORMAT     : Format the storage",
                                                "SETDEFENSE : Set the write defense on the storage(Floppy,Dvd,Dvd-rw)",
                                                "OPEN       : Open the write defense on the given storage(Dvd-rw)",
                                                "EXIT       : Go back to the Storage menu"};
            foreach(string menuPoint in MountMenu)
            {
                Console.WriteLine(menuPoint);
            }
            Console.WriteLine();
        }
        private void ListFile(Storage storage)
        {
            if (storage is Floppy)
            {
                foreach (File element in storage.GetFileList())
                {
                    MsConsoleLogger.ConsoleInfo($"name = {element.FileName} filesize = {element.FileSize}Kb " +
                                                $" OnlyRead = {element.OnlyRead} system = {element.System} " +
                                                $" Hidden = {element.Hidden}");
                }
            }
            else
            {
                foreach (File element in storage.GetFileList())
                {
                    MsConsoleLogger.ConsoleInfo($"name = {element.FileName} filesize = {element.FileSize/ 1048576 :F1}GB " +
                                                $" OnlyRead = {element.OnlyRead} system = {element.System} " +
                                                $" Hidden = {element.Hidden}");
                }
            }
        }

        private Storage SelectMounted()
        {
            MsConsoleLogger.UserInput("Please choose a mounted storage by ID: ");
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
                    else if (answer == "check cap" ||answer == "checkcap")
                    {
                        if (ChoosedStorage is DVD || ChoosedStorage is DvD_RW || ChoosedStorage is Hdd)
                        {
                            MsConsoleLogger.ConsoleInfo($"max capacity = {ChoosedStorage.MaxCapacity:F1}GB" +
                                                        $" free capacity = {ChoosedStorage.FreeCapacity:F1}GB" +
                                                        $" reserved capacity = {ChoosedStorage.ReservedCapacity:F1}GB");
                        }
                        else if (ChoosedStorage is Floppy)
                        {
                            MsConsoleLogger.ConsoleInfo($"max capacity = {ChoosedStorage.MaxCapacity:F1}Kb" +
                                                       $" free capacity = {ChoosedStorage.FreeCapacity:F1}Kb" +
                                                       $" reserved capacity = {ChoosedStorage.ReservedCapacity:F1}Kb");
                        }
                    
                    }
                    else if (answer == "list file" || answer == "listfile")
                    {
                        ListFile(ChoosedStorage);
                    }
                    else if (answer == "remove")
                    {
                        if (ChoosedStorage.GetFileList().Count == 0)
                        { MsConsoleLogger.Error("The storage is empty"); }
                        else
                        {
                            ListFile(ChoosedStorage);
                            Console.WriteLine();
                            MsConsoleLogger.UserInput("Give me the file which you want to delete: ");
                            string fileYouWantToRemove = Console.ReadLine();
                            ChoosedStorage.Remove(fileYouWantToRemove);
                        }
                    }
                    else if (answer == "archive")
                    {
                        Storage secondStorage = SelectMounted();
                        List<File> mutualFiles = computer.Archive(ChoosedStorage, secondStorage);
                        foreach(File file in mutualFiles)
                        {
                            MsConsoleLogger.ConsoleInfo($"{file.ToString()} is already on the {secondStorage.StoreName} storage ");
                        }
                    }
                    else if (answer == "format")
                    {
                        ChoosedStorage.Format();
                        MsConsoleLogger.ConsoleInfo($"The {ChoosedStorage.StoreName} has been formatted");

                    }
                    else if (answer == "setdefense" || answer == "set defense")
                    {
                        if (ChoosedStorage is DvD_RW || ChoosedStorage is DVD)
                        {
                            DVD dvd = (DVD)ChoosedStorage;
                            dvd.Block();
                            MsConsoleLogger.ConsoleInfo("The dvd's write defense has been set");
                        }
                        else if (ChoosedStorage is Floppy)
                        {
                            Floppy floppy = (Floppy)ChoosedStorage;
                            floppy.TurnOnWriteDefense();
                            MsConsoleLogger.ConsoleInfo("The floppy's write defense has been set");
                        }
                        else if (!(ChoosedStorage is Floppy) && !(ChoosedStorage is DVD) && !(ChoosedStorage is DvD_RW))
                        {
                            throw new Exception("To this storage has no write defense ");
                        }
                    }
                    else if (answer == "open")
                    {
                        if (ChoosedStorage is DvD_RW)
                        {
                            DvD_RW dvd_rw = (DvD_RW)ChoosedStorage;
                            dvd_rw.Open();
                            MsConsoleLogger.ConsoleInfo("You can write again on this storage,all saved file what was on this storage has been removed");
                        }
                        else if(!(ChoosedStorage is DvD_RW))
                        {
                            throw new Exception("This is not a dvd_rw storage please select another mounted storage,wich type is dvd_rw");
                        }
                    }
                    else if(answer == "exit")
                    {
                        MsConsoleLogger.ConsoleInfo("Go back to the storage menu");
                        Console.WriteLine();
                        break;
                    }
                    else
                    {
                        throw new Exception("Invalid argument");
                    }
                }
            }
        }
    }
}
