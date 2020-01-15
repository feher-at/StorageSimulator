using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace StoreManager.Api
{
    class Computer
    {
        private List<Storage> Storages = new List<Storage>();
        public List<Storage> GetStorages()
        {
            return this.Storages;
        }


        public void PutOn(string storeName, List<Storage> storageList)
        {
            for (int i = 0; i < storageList.Count; i++)
            {
                if (storageList[i].StoreName == storeName)
                {

                    Storages.Add(storageList[i]);

                    break;
                }

                else if (storageList[i].StoreName != storeName && i == storageList.Count() - 1)
                {
                    throw new Exception("There is no such store");
                }
            }
        }

        public void AllCapacity()
        {
            foreach (Storage element in Storages)
                Console.WriteLine($"the {element.StoreName} max capacity is : {element.maxCapacity}");
        }
        public void AllFreeCapacity()
        {
            foreach (Storage element in Storages)
            {

                Console.WriteLine($"the {element.StoreName} free capacity is : {element.maxCapacity} / {element.FreeCapacity}");
            }
        }
        public void AllReservedCapacity()
        {
            foreach (Storage element in Storages)
            {


                Console.WriteLine($"the {element.StoreName} reserved capacity is : {element.maxCapacity} / {element.ReservedCapacity}");
            }
        }
        public void Archive(string fileName, string storingPlaceName = "")
        {
            Random rnd = new Random();

            string[] AFstring = new string[5];
            int ignoreThisHdd = 0;

            for (int index = 0; index < Storages.Count; index++)
            {
                int result = 0;
                ignoreThisHdd++;
                int fileCount = Storages[index].FileListToComputer().Count();
                for (int i = 0; i < fileCount; i++)

                {
                    if (Storages[index].FileListToComputer()[i].FileName == fileName)
                    {
                        AFstring = Storages[index].FileListToComputer()[i].ToString().Split(',');
                        Storages[index].Remove(Storages[index].FileListToComputer()[i].FileName);
                        result++;
                        break;
                    }

                }
                if (result == 1)
                    break;
                else if (index == Storages.Count - 1)
                {
                    throw new Exception($"there is no such file on this computer");
                }
            }

            if (storingPlaceName != "")
            {
                for (int i = 0; i < Storages.Count; i++)
                {
                    if (Storages[i].StoreName == storingPlaceName)
                    {
                        Storages[i].AddFile(AFstring[0], Convert.ToInt32(AFstring[1]), Convert.ToBoolean(AFstring[2]), AFstring[3], Convert.ToBoolean(AFstring[4]));
                    }
                    else if (Storages[i].FileListToComputer()[i].FileName != fileName && i == Storages.Count - 1)
                        throw new Exception("There is no such storage on the computer");
                }

            }
            else
            {

                while (true)
                {
                    int hddCount = rnd.Next(0, Storages.Count());
                    if (hddCount != (ignoreThisHdd - 1) && Storages[hddCount].FreeCapacity > Convert.ToInt32(AFstring[1]))
                    {
                        Storages[hddCount].AddFile(AFstring[0], Convert.ToInt32(AFstring[1]), Convert.ToBoolean(AFstring[2]), AFstring[3], Convert.ToBoolean(AFstring[4]));
                        break;
                    }
                }
            }
        }
    }
}
