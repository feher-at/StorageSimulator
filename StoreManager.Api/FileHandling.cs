using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;


namespace StoreManager.Api
{
    public class FileHandling
    {
        public static string[] ReadFromFile(string fileName)
        {
            string[] file = System.IO.File.ReadAllLines(fileName);
            return file;
        }
        public static void WriteToStoreFile(List<Storage> list, string FileName)
        {
            using (TextWriter outputFile = new StreamWriter(FileName))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] is Floppy)
                    {
                        if (i == list.Count() - 1 && list[i].GetFileList().Count == 0)
                        {
                            outputFile.Write($"[Floppy] = {list[i].Id},{list[i].StoreName}");
                        }
                        else if (i == list.Count() - 1 && list[i].GetFileList().Count > 0)
                        {
                            outputFile.WriteLine($"[Floppy] = {list[i].Id},{list[i].StoreName}");

                            for (int index = 0; i < list[i].GetFileList().Count; i++)
                            {
                                if (index == list[i].GetFileList().Count - 1)
                                    outputFile.Write(list[i].GetFileList()[index].ToString());
                                else
                                    outputFile.WriteLine(list[i].GetFileList()[index].ToString());

                            }
                        }

                        else
                        {
                            outputFile.WriteLine($"[Floppy] = {list[i].Id},{list[i].StoreName}");
                            if (list[i].GetFileList().Count > 0)
                            {
                                foreach (File element in list[i].GetFileList())
                                {
                                    outputFile.WriteLine(element.ToString());
                                }
                            }
                        }
                    }
                    else if (list[i] is DvD_RW)
                    {
                        if (i == list.Count() - 1 && list[i].GetFileList().Count == 0)
                        {
                            outputFile.Write($"[Dvd_Rw] = {list[i].Id},{list[i].StoreName}");
                        }
                        else if (i == list.Count() - 1 && list[i].GetFileList().Count > 0)
                        {
                            outputFile.WriteLine($"[Dvd_Rw] = {list[i].Id},{list[i].StoreName}");

                            for (int index = 0; i < list[i].GetFileList().Count; i++)
                            {
                                if (index == list[i].GetFileList().Count - 1)
                                    outputFile.Write(list[i].GetFileList()[index].ToString());
                                else
                                    outputFile.WriteLine(list[i].GetFileList()[index].ToString());

                            }
                        }

                        else
                        {
                            outputFile.WriteLine($"[Dvd_Rw] = {list[i].Id},{list[i].StoreName}");
                            if (list[i].GetFileList().Count > 0)
                            {
                                foreach (File element in list[i].GetFileList())
                                {
                                    outputFile.WriteLine(element.ToString());
                                }
                            }
                        }
                    }
                    else if (list[i] is DVD)
                    {
                        if (i == list.Count() - 1 && list[i].GetFileList().Count == 0)
                        {
                            outputFile.Write($"[Dvd] = {list[i].Id},{list[i].StoreName}");
                        }
                        else if (i == list.Count() - 1 && list[i].GetFileList().Count > 0)
                        {
                            outputFile.WriteLine($"[Dvd] = {list[i].Id},{list[i].StoreName}");

                            for (int index = 0; i < list[i].GetFileList().Count; i++)
                            {
                                if (index == list[i].GetFileList().Count - 1)
                                    outputFile.Write(list[i].GetFileList()[index].ToString());
                                else
                                    outputFile.WriteLine(list[i].GetFileList()[index].ToString());

                            }
                        }

                        else
                        {
                            outputFile.WriteLine($"[Dvd] = {list[i].Id},{list[i].StoreName}");
                            if (list[i].GetFileList().Count > 0)
                            {
                                foreach (File element in list[i].GetFileList())
                                {
                                    outputFile.WriteLine(element.ToString());
                                }
                            }
                        }
                    }

                    else if (list[i] is Hdd)
                    {
                        if (i == list.Count() - 1 && list[i].GetFileList().Count == 0)
                        {
                            outputFile.Write($"[storage] = {list[i].Id},{list[i].maxCapacity},{list[i].StoreName}");
                        }
                        else if (i == list.Count() - 1 && list[i].GetFileList().Count > 0)
                        {
                            outputFile.WriteLine($"[storage] = {list[i].Id},{list[i].maxCapacity},{list[i].StoreName}");

                            for (int index = 0; i < list[i].GetFileList().Count; i++)
                            {
                                if (index == list[i].GetFileList().Count - 1)
                                    outputFile.Write(list[i].GetFileList()[index].ToString());
                                else
                                    outputFile.WriteLine(list[i].GetFileList()[index].ToString());

                            }
                        }

                        else
                        {
                            outputFile.WriteLine($"[storage] = {list[i].Id},{list[i].maxCapacity},{list[i].StoreName}");
                            if (list[i].GetFileList().Count > 0)
                            {
                                foreach (File element in list[i].GetFileList())
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
