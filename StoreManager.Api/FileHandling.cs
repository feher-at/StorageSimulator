using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Xml.Serialization;


namespace StoreManager.Api
{
    public class FileHandling
    {
        public static string[] ReadFromFile(string fileName)
        {
            string[] file = System.IO.File.ReadAllLines(fileName);
            return file;
        }
        public static void SerializeProcess(List<Storage> storageList,string filepath)
        {

            using (Stream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write, FileShare.None))
            { 
                XmlSerializer serializer = new XmlSerializer(typeof(List<Storage>));
                serializer.Serialize(fs, storageList);
            }

        }
        public static void DeserializerProcess(List<Storage> storagelist, string filepath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Storage>));
            if (System.IO.File.Exists(filepath))
            {
                using (FileStream fs = System.IO.File.OpenRead(filepath))
                {
                    storagelist.AddRange((List<Storage>)serializer.Deserialize(fs));
                }
            }
            else
            {
                throw new FileNotFoundException("This file does not exist");
            }
        }
        public static void WriteToStoreFile(List<Storage> list, string FileName)
        {
            using (TextWriter outputFile = new StreamWriter(FileName))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] is Floppy)
                    {
                        if (i == list.Count() - 1 && list[i].FileList.Count == 0)
                        {
                            outputFile.Write($"[Floppy] = {list[i].Id};{list[i].StoreName}");
                        }
                        else if (i == list.Count() - 1 && list[i].FileList.Count > 0)
                        {
                            outputFile.WriteLine($"[Floppy] = {list[i].Id};{list[i].StoreName}");

                            for (int index = 0; i < list[i].FileList.Count; i++)
                            {
                                if (index == list[i].FileList.Count - 1)
                                {
                                    outputFile.Write(list[i].FileList[index].ToString());
                                    break;
                                }
                                else
                                    outputFile.WriteLine(list[i].FileList[index].ToString());

                            }
                        }

                        else
                        {
                            outputFile.WriteLine($"[Floppy] = {list[i].Id};{list[i].StoreName}");
                            if (list[i].FileList.Count > 0)
                            {
                                foreach (File element in list[i].FileList)
                                {
                                    outputFile.WriteLine(element.ToString());
                                }
                            }
                        }
                    }
                    else if (list[i] is DvD_RW)
                    {
                        if (i == list.Count() - 1 && list[i].FileList.Count == 0)
                        {
                            outputFile.Write($"[Dvd_Rw] = {list[i].Id};{list[i].StoreName}");
                        }
                        else if (i == list.Count() - 1 && list[i].FileList.Count > 0)
                        {
                            outputFile.WriteLine($"[Dvd_Rw] = {list[i].Id};{list[i].StoreName}");

                            for (int index = 0; i < list[i].FileList.Count; i++)
                            {
                                if (index == list[i].FileList.Count - 1)
                                {
                                    outputFile.Write(list[i].FileList[index].ToString());
                                    break;
                                }
                                else
                                    outputFile.WriteLine(list[i].FileList[index].ToString());

                            }
                        }

                        else
                        {
                            outputFile.WriteLine($"[Dvd_Rw] = {list[i].Id};{list[i].StoreName}");
                            if (list[i].FileList.Count > 0)
                            {
                                foreach (File element in list[i].FileList)
                                {
                                    outputFile.WriteLine(element.ToString());
                                }
                            }
                        }
                    }
                    else if (list[i] is DVD)
                    {
                        if (i == list.Count() - 1 && list[i].FileList.Count == 0)
                        {
                            outputFile.Write($"[Dvd] = {list[i].Id};{list[i].StoreName}");
                        }
                        else if (i == list.Count() - 1 && list[i].FileList.Count > 0)
                        {
                            outputFile.WriteLine($"[Dvd] = {list[i].Id};{list[i].StoreName}");

                            for (int index = 0; i < list[i].FileList.Count; i++)
                            {
                                if (index == list[i].FileList.Count - 1)
                                {
                                    outputFile.Write(list[i].FileList[index].ToString());
                                    break;
                                }
                                else
                                    outputFile.WriteLine(list[i].FileList[index].ToString());

                            }
                        }

                        else
                        {
                            outputFile.WriteLine($"[Dvd] = {list[i].Id};{list[i].StoreName}");
                            if (list[i].FileList.Count > 0)
                            {
                                foreach (File element in list[i].FileList)
                                {
                                    outputFile.WriteLine(element.ToString());
                                }
                            }
                        }
                    }

                    else if (list[i] is Hdd)
                    {
                        if (i == list.Count() - 1 && list[i].FileList.Count == 0)
                        {
                            outputFile.Write($"[Hdd] = {list[i].Id};{list[i].MaxCapacity};{list[i].StoreName}");
                        }
                        else if (i == list.Count() - 1 && list[i].FileList.Count > 0)
                        {
                            outputFile.WriteLine($"[Hdd] = {list[i].Id};{list[i].MaxCapacity};{list[i].StoreName}");

                            for (int index = 0; index < list[i].FileList.Count; index++)
                            {
                                if (index == list[i].FileList.Count - 1)
                                {
                                    outputFile.Write(list[i].FileList[index].ToString());
                                    break;
                                }
                                else
                                    outputFile.WriteLine(list[i].FileList[index].ToString());

                            }
                        }

                        else
                        {
                            outputFile.WriteLine($"[Hdd] = {list[i].Id};{list[i].MaxCapacity};{list[i].StoreName}");
                            if (list[i].FileList.Count > 0)
                            {
                                foreach (File element in list[i].FileList)
                                {
                                    outputFile.WriteLine(element.ToString());
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
