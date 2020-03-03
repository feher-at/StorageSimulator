using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace StoreManager.Api
{
    [XmlInclude(typeof(Hdd))]
    [XmlInclude(typeof(DVD))]
    [XmlInclude(typeof(Floppy))]
    [XmlInclude(typeof(DvD_RW))]
    public abstract class Storage
    {
        public string Id { get; set; }
        public string StoreName { get; set; }
        public List<File> FileList = new List<File>();
        private double maxCapacity;
        public double MaxCapacity { get { return maxCapacity; } 
            set 
            { 
                if(value < 0)
                {
                    throw new Exception("This is an invalid capacity number");
                }
                else
                {
                    maxCapacity = value;
                }
            } }
        
        public abstract double FreeCapacity { get; set; }
        public abstract double ReservedCapacity { get; set; }
        public Storage()
        {
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
            this.FileList = new List<File>();

        }

        public abstract void AddFile(string fileName, double fileSize, bool onlyRead, string system, bool hidden);
        

        //public string Search(string fileName)
        //{
        //    foreach (File element in FileList)
        //    {
        //        if (element.FileName.ToLower() == fileName.ToLower())
        //        {
        //            return element.ToString();
        //        }
        //    }
        //    throw new Exception($"There is no such filename: {fileName}");
        //}
        public virtual void Remove(string fileName)
        {

            for (int i = 0; i < this.FileList.Count(); i++)
            {

                if (FileList[i].FileName == fileName)
                {
                    FileList.Remove(FileList[i]);
                    return;

                }

                else if (FileList[i].FileName != fileName && i == FileList.Count() - 1)
                {
                    throw new Exception("There is no such file");
                }


            }
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is Storage))
            { 
                return false;
            }

            return this.Id == ((Storage)obj).Id && this.StoreName == ((Storage)obj).StoreName;
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode() ^ this.StoreName.GetHashCode();
        }


    }
}
