using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace StoreManager.Api
{
    public class Computer
    {
        private List<Storage> Storages = new List<Storage>();
        public List<Storage> GetStorages()
        {
            return this.Storages;
        }


        public void PutOn(string Id, List<Storage> storageList)
        {
            for (int i = 0; i < storageList.Count; i++)
            {
                if (storageList[i].Id == Id)
                {

                    Storages.Add(storageList[i]);
                    storageList.Remove(storageList[i]);

                    break;
                }

                else if (storageList[i].Id != Id && i == storageList.Count() - 1)
                {
                    throw new Exception("There is no such store");
                }
            }
        }

        public void AllCapacity()
        {
            foreach (Storage element in Storages)
                Console.WriteLine($"the {element.StoreName} max capacity is : {element.MaxCapacity}");
        }
        public void AllFreeCapacity()
        {
            foreach (Storage element in Storages)
            {

                Console.WriteLine($"the {element.StoreName} free capacity is : {element.MaxCapacity} / {element.FreeCapacity}");
            }
        }
        public void AllReservedCapacity()
        {
            foreach (Storage element in Storages)
            {


                Console.WriteLine($"the {element.StoreName} reserved capacity is : {element.MaxCapacity} / {element.ReservedCapacity}");
            }
        }
        public void Archive(Storage storage1,Storage storage2)
        {
            Random rnd = new Random();

            string[] AFstring = new string[5];
            if (GetStorages().Count == 1)
            {
                throw new Exception("You need at least two mounted storage");
            }
            if (storage1.Equals(storage2))
            {
                throw new Exception("the two chose storage is the same");
            }
            else
            {
                foreach(File element in storage1.GetFileList())
                {
                    int result = 0;
                    foreach(File file in storage2.GetFileList())
                    {
                        if(element.Equals(file))
                        {
                            result++;
                        }

                    }
                    if (result > 0)
                        continue;
                    else
                    {
                        AFstring = element.ToString().Split(',');
                        storage2.AddFile(AFstring[0], Convert.ToInt32(AFstring[1]), Convert.ToBoolean(AFstring[2]), AFstring[3], Convert.ToBoolean(AFstring[4]));
                    }
                }
            }

           
            
        }
    }
}
