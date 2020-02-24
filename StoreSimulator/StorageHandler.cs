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
        public string storesfilepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"AllStores.xml");
        public string mountedfilepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MountedStores.xml");
        public StorageHandler(ILogger consoleLogger)
        {
            ShConsole = consoleLogger;
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
                                                      "DELETESTORE: Delete an existing storage",
                                                      "REMOVEMOUNT: Remove a mount from the mounted storages and put back to unmounted storage list",
                                                      "MODIFYMOUNT: Modify the selected mounted storage's name",
                                                      "MODIFYSTORE: Modify the selected store name(if the store is empty)",
                                                      "SAVE       : Save all the storages and it's data",
                                                      "EXIT       : Exit of the program"};
            foreach (string menu in storageMenuList)
            {
                Console.WriteLine(menu);
            }
            Console.WriteLine();
        }
        private string IdGenerator()
        {
            Random random = new Random();
            string id = "";
            string letters = "abcdefghijklmnopqrstuvwxyz";
            string specialCharachters = "$ß#&";
            string numbers = "1234567890";
            for(int index = 0; index < 2; index++ )
            {
                int randomIndex = random.Next(specialCharachters.Count());
                id += specialCharachters[randomIndex];
                randomIndex = random.Next(letters.Count());
                id += char.ToUpper(letters[randomIndex]);
                randomIndex = random.Next(letters.Count());
                id += letters[randomIndex];
                randomIndex = random.Next(numbers.Count());
                id += numbers[randomIndex];
            }
            return id;
        }
        public void StorageMenu()
        {
            if(System.IO.File.Exists(storesfilepath) && new FileInfo(storesfilepath).Length > 0)
            {
                FileHandling.DeserializerProcess(storages, storesfilepath);
            }
            if(System.IO.File.Exists(mountedfilepath) && new FileInfo(mountedfilepath).Length > 0)
            {
                FileHandling.DeserializerProcess(computer.GetStorages(),mountedfilepath);
            }
            
            while (true)
            {
                try
                {
                    StorageMenuList();
                    ShConsole.UserInput("Choose a menu: ");
                    string answer = Console.ReadLine().ToLower();
                    if (answer == "listmounts" || answer == "list mounts")
                    {
                        if (computer.GetStorages().Count == 0)
                        {
                            ShConsole.Warning("Mount at least one storage first");
                        }
                        else
                        {
                            ListInfoAStorageList(computer.GetStorages());
                        }
                        Console.WriteLine();
                    }
                    else if (answer == "liststores" || answer == "list stores")
                    {
                        if (storages.Count == 0)
                        {
                            ShConsole.Warning("Make at least one storage first");
                        }
                        else
                        {
                            ListInfoAStorageList(storages);
                        }
                        Console.WriteLine();
                    }
                    else if (answer == "addmount" || answer == "add mount")
                    {
                        if (storages.Count == 0)
                        {
                            ShConsole.Warning("Make one storage at least!");
                        }
                        else
                        {
                            ListInfoAStorageList(storages);
                            ShConsole.UserInput("Give me the storage Id,which you want put on the computer: ");
                            string addAnswer = Console.ReadLine();
                            computer.Mount(addAnswer, storages);


                        }
                        Console.WriteLine();
                    }
                    else if (answer == "usemount" || answer == "use mount")
                    {
                        ListInfoAStorageList(computer.GetStorages());
                        MountedStorageHandler msh = new MountedStorageHandler(ShConsole, computer);
                        msh.MountedMenu();
                        Console.WriteLine();
                    }
                    else if (answer == "addstore" || answer == "add store")
                    {

                        Console.WriteLine("What storage you want to make(Hdd,Dvd,Dvd_rw,Floppy)");
                        string newstorage = Console.ReadLine().ToLower();

                        string id = IdGenerator();
                        if (newstorage == "hdd")
                        {
                            string[] storageParams = new string[] { "Max capacity", "Name" };
                            List<string> sp = new List<string>();
                            foreach (string param in storageParams)
                            {
                                ShConsole.UserInput($"Give me  {param}: ");
                                sp.Add(Console.ReadLine());
                            }
                            Storage storage = new Hdd(id, Convert.ToInt32(sp[0]), sp[1]);

                            storages.Add(storage);
                        }
                        else if (newstorage == "dvd" || newstorage == "floppy" || newstorage == "dvd_rw" || newstorage == "dvd-rw" || newstorage == "dvd rw")
                        {
                            ShConsole.UserInput($"Give me the storage Name : ");
                            string name = Console.ReadLine();
                            if (newstorage == "dvd")
                            {
                                Storage storage = new DVD(id, name);
                                storages.Add(storage);
                            }
                            else if (newstorage == "floppy")
                            {
                                Storage storage = new Floppy(id, name);
                                storages.Add(storage);
                            }
                            else if (newstorage == "dvd_rw" || newstorage == "dvd-rw" || newstorage == "dvd rw")
                            {
                                Storage storage = new DvD_RW(id, name);
                                storages.Add(storage);
                            }
                        }
                        else
                        {
                            throw new ArgumentException("Invalid argument");
                            
                        }
                        ShConsole.ConsoleInfo($"The {newstorage} storage has been created");
                        Console.WriteLine();

                    }
                    else if (answer == "deletestore" || answer == "delete store")
                    {
                        if (storages.Count == 0)
                        {
                            ShConsole.Warning("Create a storage first please");
                        }
                        else
                        {
                            ListInfoAStorageList(storages);
                            ShConsole.UserInput("Give me the storage Id you want to delete: ");
                            string deletingStoreId = Console.ReadLine();
                            for (int i = 0; i < storages.Count; i++)
                            {

                                if (storages[i].Id == deletingStoreId)
                                {
                                    ShConsole.UserInput($"Are you sure you want to delete this storage {storages[i].StoreName}?(yes/no): ");
                                    string deleteAnswer = Console.ReadLine().ToLower();
                                    if (deleteAnswer == "yes" || deleteAnswer == "y")
                                    {

                                        ShConsole.ConsoleInfo($"the {storages[i].StoreName} has been deleted");
                                        storages.Remove(storages[i]);
                                    }
                                    else if (deleteAnswer == "no" || deleteAnswer == "n")
                                    {
                                        ShConsole.ConsoleInfo("the deleting process has been declined");
                                    }
                                    else
                                    {
                                        throw new ArgumentException("invalid input");
                                    }
                                    break;
                                }
                                else if (storages[i].Id != deletingStoreId && i == storages.Count() - 1)
                                {
                                    throw new NullReferenceException("There is no such store");
                                }
                            }
                            Console.WriteLine();
                        }
                    }
                    else if (answer == "remove mount" || answer == "removemount")
                    {
                        if (computer.GetStorages().Count == 0)
                        {
                            throw new NullReferenceException("There is no mounted storage");
                        }
                        else
                        {
                            ListInfoAStorageList(computer.GetStorages());
                            ShConsole.UserInput("Give me the storage Id you want to remove: ");
                            string removingStoreId = Console.ReadLine();
                            for (int i = 0; i < computer.GetStorages().Count; i++)
                            {
                                if (computer.GetStorages()[i].Id == removingStoreId)
                                {
                                    ShConsole.UserInput($"Are you sure you want to remove this storage {computer.GetStorages()[i].StoreName}?(yes/no) : ");
                                    string removeAnswer = Console.ReadLine().ToLower();
                                    if (removeAnswer == "yes" || removeAnswer == "y")
                                    {
                                        ShConsole.ConsoleInfo($"the {computer.GetStorages()[i].StoreName} has been removed");
                                        storages.Add(computer.GetStorages()[i]);
                                        computer.GetStorages().Remove(computer.GetStorages()[i]);

                                    }
                                    else if (removeAnswer == "no" || removeAnswer == "n")
                                    {
                                        ShConsole.ConsoleInfo("the removing process has been declined");
                                    }
                                    else
                                    {
                                        throw new ArgumentException("invalid input");
                                    }
                                    break;
                                }
                                else if (computer.GetStorages()[i].Id != removingStoreId && i == computer.GetStorages().Count() - 1)
                                {
                                    throw new NullReferenceException("There is no such store");
                                    
                                }
                            }
                        }
                        Console.WriteLine();
                    }
                    else if (answer == "modify mount" || answer == "modifymount")
                    {
                        if (computer.GetStorages().Count == 0)
                        {
                            throw new NullReferenceException("There is no mounted storage");
                        }
                        else
                        {
                            ListInfoAStorageList(computer.GetStorages());
                            ShConsole.UserInput("Give me the mounted storage Id you want to modify");
                            string modifyingMountedStoreId = Console.ReadLine();
                            for (int i = 0; i < computer.GetStorages().Count; i++)
                            {
                                if (computer.GetStorages()[i].Id == modifyingMountedStoreId)
                                {
                                    ShConsole.UserInput($"Are you sure You want to modify the storage NAME?(yes/no): ");
                                    string removeAnswer = Console.ReadLine().ToLower();
                                    if (removeAnswer == "yes" || removeAnswer == "y")
                                    {
                                        ShConsole.UserInput("Give me the new store name: ");
                                        string newName = Console.ReadLine();
                                        computer.GetStorages()[i].StoreName = newName;

                                    }
                                    else if (removeAnswer == "no" || removeAnswer == "n")
                                    {
                                        ShConsole.ConsoleInfo("the modifying process has been declined");
                                    }
                                    else
                                    {
                                        throw new ArgumentException("invalid input");
                                    }
                                    break;
                                }
                                else if (computer.GetStorages()[i].Id != modifyingMountedStoreId && i == computer.GetStorages().Count() - 1)
                                {
                                   throw new Exception ("There is no such mounted store");
                                    
                                }
                            }
                        }
                        Console.WriteLine();
                    }
                    else if (answer == "modify store" || answer == "modifystore")
                    {
                        if (storages.Count == 0)
                        {
                            throw new NullReferenceException("There is no unmounted storage");
                        }
                        else
                        {
                            ListInfoAStorageList(storages);
                            ShConsole.UserInput("Give me the storage Id you want to modify: ");
                            string modifyingStoreId = Console.ReadLine();
                            for (int i = 0; i < storages.Count; i++)
                            {
                                if (storages[i].Id == modifyingStoreId)
                                {
                                    if (storages[i].FileList.Count == 0)
                                    {
                                        ShConsole.UserInput($"Are you sure You want to modify the storage NAME?(yes/no): ");
                                        string removeAnswer = Console.ReadLine().ToLower();
                                        if (removeAnswer == "yes" || removeAnswer == "y")
                                        {
                                            ShConsole.UserInput("Give me the new store name: ");
                                            string newName = Console.ReadLine();
                                            storages[i].StoreName = newName;

                                        }
                                        else if (removeAnswer == "no" || removeAnswer == "n")
                                        {
                                            ShConsole.ConsoleInfo("the modifying process has been declined");
                                        }
                                        else
                                        {
                                            throw new ArgumentException("invalid input");
                                        }
                                    }
                                    else if (storages[i].FileList.Count > 0)
                                    {
                                        throw new Exception ("The store is not empty,you can't modify the name");
                                    }
                                 
                                }
                                else if (storages[i].Id != modifyingStoreId && i == computer.GetStorages().Count() - 1)
                                {
                                    throw new NullReferenceException("There is no such store");
                                    
                                }
                            }
                            Console.WriteLine();
                        }
                    }
                    else if (answer == "save")
                    {

                        FileHandling.SerializeProcess(storages, storesfilepath);
                        ShConsole.ConsoleInfo("the storage data has been saved");
                        FileHandling.SerializeProcess(computer.GetStorages(), mountedfilepath);
                        ShConsole.ConsoleInfo("the mounted storage data has been saved");
                        Console.WriteLine();

                    }
                    else if (answer == "exit")
                    {
                        FileHandling.SerializeProcess(storages, storesfilepath);
                        ShConsole.ConsoleInfo("the storage data has been saved");
                        FileHandling.SerializeProcess(computer.GetStorages(), mountedfilepath);
                        ShConsole.ConsoleInfo("the mounted storage data has been saved");
                        Console.WriteLine();
                        ShConsole.ConsoleInfo("Good bye");
                        break;
                    }
                    else
                    {
                        throw new ArgumentException("invalid argument");
                    }
                }
                catch(Exception ex)
                {
                    ShConsole.Error(ex.Message);
                }
            }
        }
    }
}
