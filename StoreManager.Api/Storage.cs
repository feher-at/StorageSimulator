using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManager.Api
{
    public abstract class Storage
    {
        public string Id { get; protected set; }
        public string StoreName { get; protected set; }
        protected List<File> fileList = new List<File>();
        public double MaxCapacity { get; protected set; }
        protected double freeCapacity;
        protected double reservedCapacity;
        public abstract double FreeCapacity { get; }
        public abstract double ReservedCapacity { get; }


        public List<File> GetFileList()
        {
            return this.fileList;
        }
        public Storage(string Id, string StoreName)
        {
            this.StoreName = StoreName;
        }

        public Storage(string Id, int MaxCapacity, string StoreName)

        {
            this.Id = Id;
            this.MaxCapacity = MaxCapacity;
            this.StoreName = StoreName;
        }

        public virtual void Format()
        {
            this.fileList = new List<File>();

        }

        public virtual void AddFile(string fileName, double fileSize, bool onlyRead, string system, bool hidden)
        {

            if (fileList.Count > 0)
            {
                foreach (File element in fileList)
                {
                    if (element.FileName == fileName)
                        throw new Exception("This file is already in the file list");
                }
            }



            if (fileSize > this.FreeCapacity)
                throw new Exception("There is not enough free capacity");
            else if (fileSize <= this.freeCapacity)
            {
                File addfile = new File(fileName, fileSize, onlyRead, system, hidden);
                fileList.Add(addfile);
                return;
            }

        }

        public string Search(string fileName)
        {
            foreach (File element in fileList)
            {
                if (element.FileName == fileName)
                {
                    return element.ToString();
                }
            }
            throw new Exception($"There is no such filename: {fileName}");
        }
        public virtual void Remove(string fileName)
        {

            for (int i = 0; i < this.fileList.Count(); i++)
            {

                if (fileList[i].FileName == fileName)
                {
                    fileList.Remove(fileList[i]);
                    return;

                }

                else if (fileList[i].FileName != fileName && i == fileList.Count() - 1)
                {
                    throw new Exception("There is no such file");
                }


            }
        }
        public void listFile()
        {
            foreach (File element in fileList)
                Console.WriteLine(element.ToString());
        }

        public List<File> FileListToComputer()
        {
            return this.fileList;
        }


    }
}
