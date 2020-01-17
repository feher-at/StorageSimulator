using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManager.Api;
using System.IO;

namespace StoreSimulator
{
    class StorageHandler
    {
        private ILogger ShConsole;
        private Computer computer = new Computer();
        private List<Storage> storages = new List<Storage>();
        public StorageHandler(ILogger consoleLogger)
        {
            ShConsole = consoleLogger;
        }

        private void ReadfromFile(string fileName,List<Storage> givenStorageList)
        {
            ReadStorages(FileHandling.ReadFromFile(fileName),givenStorageList);
        }
        private void WriteToTheFile(List<Storage> storages,string filename)
        {
            FileHandling.WriteToStoreFile(storages, filename);
        }

        private void ReadStorages(string[] Storages,List<Storage> givenStorageList)
        {
            List<Storage> TransitionStorage = new List<Storage>();
            for (int i = 0; i < Storages.Count(); i++)
            {
                if (Storages[i][0] == '[')
                {
                    string[] StoragesRow = Storages[i].Split('=');
                    if (StoragesRow[0] == "[Hdd] ")
                    {
                        string[] StoreInfo = StoragesRow[1].Split(',');
                        Storage Store = new Hdd(StoreInfo[0].TrimStart(), Convert.ToInt32(StoreInfo[1]), StoreInfo[2]);
                        TransitionStorage.Add(Store);
                    }
                    else if (StoragesRow[0] == "[Dvd] ")
                    {
                        string[] StoreInfo = StoragesRow[1].Split(',');
                        Storage Store = new DVD(StoreInfo[0].TrimStart(), StoreInfo[1].TrimStart());
                        TransitionStorage.Add(Store);
                    }
                    else if (StoragesRow[0] == "[Dvd_Rw] ")
                    {
                        string[] StoreInfo = StoragesRow[1].Split(',');
                        Storage Store = new DvD_RW(StoreInfo[0].TrimStart(), StoreInfo[1].TrimStart());
                        TransitionStorage.Add(Store);
                    }
                    else if (StoragesRow[0] == "[Floppy] ")
                    {
                        string[] StoreInfo = StoragesRow[1].Split(',');
                        Storage Store = new Floppy(StoreInfo[0].TrimStart(), StoreInfo[1].TrimStart());
                        TransitionStorage.Add(Store);
                    }
                }
                if (i <= Storages.Count() - 2 && Storages[i + 1][0] == '[' || i == Storages.Count() - 1)
                {
                    if (Storages[i][0] == '[' && i == Storages.Count() - 1)
                    {
                        givenStorageList.Add(TransitionStorage[0]);
                        TransitionStorage = new List<Storage>();
                        continue;
                    }
                    else if (Storages[i][0] == '[' && Storages[i + 1][0] == '[')
                    {
                        givenStorageList.Add(TransitionStorage[0]);
                        TransitionStorage = new List<Storage>();
                        continue;
                    }
                    else
                    {
                        string[] file = Storages[i].Split(',');
                        TransitionStorage[0].AddFile(file[0], Convert.ToInt32(file[1]), Convert.ToBoolean(file[2]), file[3], Convert.ToBoolean(file[4]));
                        givenStorageList.Add(TransitionStorage[0]);
                        TransitionStorage = new List<Storage>();
                    }
                }
                else if (Storages[i][0] != '[')
                {
                    string[] file = Storages[i].Split(',');

                    TransitionStorage[0].AddFile(file[0], Convert.ToInt32(file[1]), Convert.ToBoolean(file[2]), file[3], Convert.ToBoolean(file[4]));
                }
            }
        }

        private void ListInfoAStorageList(List<Storage> storegeList)
        {
            foreach (Storage store in storegeList)
            {
                if (store is Hdd)
                {
                    Hdd hdd = (Hdd)store;
                    ShConsole.ConsoleInfo(hdd.ToString());
                }
                else if (store is DvD_RW)
                {
                    DvD_RW dvd_rw = (DvD_RW)store;
                    ShConsole.ConsoleInfo(dvd_rw.ToString());
                }
                else if (store is DVD)
                {
                    DVD dvd = (DVD)store;
                    ShConsole.ConsoleInfo(dvd.ToString());
                }
                else if (store is Floppy)
                {
                    Floppy floppy = (Floppy)store;
                    ShConsole.ConsoleInfo(floppy.ToString());
                }
            }
        }
        private void StorageMenuList()
        {
            string[] storageMenuList = new string[] { "LISTMOUNTS : List the mounted storages by id,name,and max capacity",
                                                      "LISTSTORES : List the unmounted storages by id name and max capacity",
                                                      "ADDMOUNT   : Mount a storage",
                                                      "USEMOUNT   : Use a selected mount(it bring in a sub menu)",
                                                      "ADDSTORE   : Make a new storage",
                                                      "DELETESTORE: Delete a existing storage",
                                                      "REMOVEMOUNT: Remove a mount from the mounted storages and put back to unmounted storage list",
                                                      "MODIFYMOUNT: Modify the selected mounted storage's id or name or both",
                                                      "MODIFYSTORE: Modify the selected store id or name or both(if the store is empty)",
                                                      "SAVE       : Save all the storages and it's data",
                                                      "EXIT       : Exit of the program",};
            foreach(string menu in storageMenuList)
            {
                Console.WriteLine(menu);
            }
            Console.WriteLine();
        }
        public void StorageMenu()
        {
            if(System.IO.File.Exists("../../AllStores.txt") && new FileInfo("../../AllStores.txt").Length > 0)
            {
                ReadfromFile("../../AllStores", storages);
            }
            if(System.IO.File.Exists("../../MountedStores.txt") && new FileInfo("../../MountedStores.txt").Length > 0)
            {
                ReadfromFile("../../MountedStores.txt", computer.GetStorages());
            }
            while(true)
            {
                StorageMenuList();
                ShConsole.UserInput("Choose a menu: ");
                string answer = Console.ReadLine().ToLower();
                if(answer == "listmounts" || answer == "list mounts")
                {
                    if (computer.GetStorages().Count == 0)
                    {
                        ShConsole.Error("Mount at least one storage first");
                    }
                    else
                    {
                        ListInfoAStorageList(computer.GetStorages());
                    }
                }
                else if(answer == "liststores" || answer == "list stores")
                {
                    if (storages.Count == 0)
                    {
                        ShConsole.Error("Make at least one storage first");
                    }
                    else
                    {
                        ListInfoAStorageList(storages);
                    }
                }
                else if(answer == "addmount" ||answer == "add mount")
                {
                    if (storages.Count == 0)
                    {
                        ShConsole.Error("Make one storage at least!");
                    }
                    else
                    {
                        ShConsole.UserInput("Give me the storage Id,which you want put on the computer: ");
                        string addAnswer = Console.ReadLine();
                        computer.PutOn(addAnswer, storages);
                        
                    }
                    Console.WriteLine();
                }
                else if(answer == "usemount" || answer == "use mount")
                {
                    MountedStorageHandler msh = new MountedStorageHandler(ShConsole, computer);
                    msh.MountedMenu();
                }
            }
        }
    }
}
