using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManager.Api;

namespace StoreSimulator
{
    class StorageHandler
    {
        private ILogger ShConsole;
        Computer computer = new Computer();
        private List<Storage> storages = new List<Storage>();
        public StorageHandler(ILogger consoleLogger)
        {
            ShConsole = consoleLogger;
        }

        private void ReadfromFile(string fileName)
        {
            ReadStorages(FileHandling.ReadFromFile(fileName));
        }
        private void WriteToTheFile(List<Storage> storages,string filename)
        {
            FileHandling.WriteToStoreFile(storages, filename);
        }

        private void ReadStorages(string[] Storages)
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
                        storages.Add(TransitionStorage[0]);
                        TransitionStorage = new List<Storage>();
                        continue;
                    }
                    else if (Storages[i][0] == '[' && Storages[i + 1][0] == '[')
                    {
                        storages.Add(TransitionStorage[0]);
                        TransitionStorage = new List<Storage>();
                        continue;
                    }
                    else
                    {
                        string[] file = Storages[i].Split(',');
                        TransitionStorage[0].AddFile(file[0], Convert.ToInt32(file[1]), Convert.ToBoolean(file[2]), file[3], Convert.ToBoolean(file[4]));
                        storages.Add(TransitionStorage[0]);
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
        private void StorageMenu()
        {
            string[] storageMenu = new string[] { "LISTMOUNTS : List the mounted storages by id,name,and max capacity",
                                                  "LISTSTORES : List the unmounted storages by id name and max capacity",
                                                  "ADDMOUNT   : Mount a storage",
                                                  "USEMOUNT   : Use a selected mount(it bring in a sub menu)",
                                                  "ADDSTORE   : Make a new storage",
                                                  "DELETESTORE: Delete a existing storage",
                                                  "REMOVEMOUNT: Remove a mount from the mounted storages and put back to unmounted storage list",
                                                  "MODIFYMOUNT: Modify the selected mounted storage's id or name or both",
                                                  "MODIFYSTORE: Modify the selected store id or name or both(if the store is empty)"};
            foreach(string menu in storageMenu)
            {
                Console.WriteLine(menu);
            }
            Console.WriteLine();
        }
    }
}
