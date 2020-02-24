using System;
using System.Collections.Generic;
using StoreManager.Api;

namespace StoreSimulator
{
    enum Fileenum
    {
        Filename,
        FileSize,
        Onlyread,
        System,
        Hidden
    }
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
                foreach (File element in storage.FileList)
                {
                    MsConsoleLogger.ConsoleInfo($"name = {element.FileName} filesize = {element.FileSize}Kb " +
                                                $" OnlyRead = {element.OnlyRead} system = {element.System} " +
                                                $" Hidden = {element.Hidden}");
                }
            }
            else
            {
                foreach (File element in storage.FileList)
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
                    throw new NullReferenceException("there is no such storage on the computer");
                    
                }
            }
            Console.WriteLine();
            throw new Exception("Something is wrong");
            
            
        }
        public void MountedMenu()
        {
            if(computer.GetStorages().Count == 0)
            {
                MsConsoleLogger.Warning("Mount a storage first");
                return;
                
            }
            else
            {
                Storage ChoosedStorage = SelectMounted();
                while (true)
                {
                    try
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

                                if (param == "file size")
                                {
                                    while (true)
                                    {
                                        MsConsoleLogger.UserInput($"Give me  {param}: ");
                                        int n;
                                        bool isNumeric = int.TryParse(Console.ReadLine(), out n);
                                        if (isNumeric && n > 0)
                                        {
                                            fp.Add(Convert.ToString(n));
                                            break;
                                        }
                                        else
                                            MsConsoleLogger.Warning("Give me a proper file size");
                                    }
                                }
                                else if (param == "only read(true/false)")
                                {

                                    while (true)
                                    {
                                        MsConsoleLogger.UserInput($"Give me  {param}: ");
                                        string paramanswer = Console.ReadLine().ToLower();
                                        if (paramanswer == "true" || paramanswer == "false")
                                        {
                                            fp.Add(paramanswer);
                                            break;
                                        }
                                        else
                                            MsConsoleLogger.Warning("Give me a proper argument(false/true)");
                                    }
                                }
                                else if (param == "hidden(true/false)")
                                {
                                    while (true)
                                    {
                                        MsConsoleLogger.UserInput($"Give me  {param}: ");
                                        string paramanswer = Console.ReadLine().ToLower();
                                        if (paramanswer == "true" || paramanswer == "false")
                                        {
                                            fp.Add(paramanswer);
                                            break;
                                        }
                                        else
                                            MsConsoleLogger.Warning("Give me a proper argument(false/true)");
                                    }
                                }
                                else
                                {
                                    MsConsoleLogger.UserInput($"Give me  {param}: ");
                                    fp.Add(Console.ReadLine());
                                }
                            }
                            ChoosedStorage.AddFile(fp[(int)Fileenum.Filename],
                                                   Convert.ToInt32(fp[(int)Fileenum.FileSize]),
                                                   Convert.ToBoolean(fp[(int)Fileenum.Onlyread]),
                                                   fp[(int)Fileenum.System],
                                                   Convert.ToBoolean(fp[(int)Fileenum.Hidden]));
                            MsConsoleLogger.ConsoleInfo("The file has been added to the storage");
                            Console.WriteLine();
                        }
                        else if (answer == "check cap" || answer == "checkcap")
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
                            if (ChoosedStorage.FileList.Count == 0)
                            { throw new NullReferenceException("The storage is empty"); }
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
                            foreach (File file in mutualFiles)
                            {
                                MsConsoleLogger.ConsoleInfo($"{file.ToString()} is already on the {secondStorage.StoreName} storage ");
                            }
                            MsConsoleLogger.ConsoleInfo($"The unmutual files has been archive to the {secondStorage} storage");
                            Console.WriteLine();
                        }
                        else if (answer == "format")
                        {
                            ChoosedStorage.Format();
                            MsConsoleLogger.ConsoleInfo($"The {ChoosedStorage.StoreName} has been formatted");
                            Console.WriteLine();
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
                                throw new InvalidCastException("To this storage has no write defense ");
                            }
                            Console.WriteLine();
                        }
                        else if (answer == "open")
                        {
                            if (ChoosedStorage is DvD_RW)
                            {
                                DvD_RW dvd_rw = (DvD_RW)ChoosedStorage;
                                dvd_rw.Open();
                                MsConsoleLogger.ConsoleInfo("You can write again on this storage,but all saved file what was on this storage has been removed");
                            }
                            else if (!(ChoosedStorage is DvD_RW))
                            {
                                throw new InvalidCastException("This is not a dvd_rw storage please select another mounted storage,wich type is dvd_rw");
                            }
                        }
                        else if (answer == "exit")
                        {
                            MsConsoleLogger.ConsoleInfo("Go back to the storage menu");
                            Console.WriteLine();
                            break;
                        }
                        else
                        {
                            throw new ArgumentException("Invalid argument");
                        }
                    }
                    catch(Exception ex)
                    {
                        MsConsoleLogger.Error(ex.Message);
                    }
                }
            }
        }
    }
}
