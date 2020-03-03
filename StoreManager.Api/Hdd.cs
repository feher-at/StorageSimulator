using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace StoreManager.Api
{
    [Serializable()]
    public class Hdd : Storage,ISerializable
    {
        public override double FreeCapacity
        {
            get
            {
                double freecap = this.MaxCapacity;
                foreach (File element in this.FileList)
                {
                    freecap -= element.FileSize/ 1048576;
                }

                return freecap;
            }
            set
            {
            }
        }
        public override double ReservedCapacity
        {
            get
            {
                double reservedCap = 0;
                foreach (File element in this.FileList)
                {
                    reservedCap += element.FileSize/ 1048576;

                }
                
                return reservedCap;
            }
            set { }

        }
        public Hdd()
        { }
        
        public Hdd(string Id, int MaxCapacity, string StoreName) : base(Id, MaxCapacity, StoreName)
        {
        }

       
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Id = {Id} Max capacity = {MaxCapacity}GB Name = {StoreName} Type = Hdd");
            return sb.ToString();
        }
        public override void AddFile(string fileName, double fileSize, bool onlyRead, string system, bool hidden)
        {

            if (FileList.Count > 0)
            {
                foreach (File element in FileList)
                {
                    if (element.FileName.ToLower() == fileName.ToLower())
                        throw new Exception("This file is already in the file list");
                }
            }
            if (fileSize/ 1048576 > this.FreeCapacity)
                throw new Exception("There is not enough free capacity");
            else if (fileSize/ 1048576 <= this.FreeCapacity)
            {
                File addfile = new File(fileName, fileSize, onlyRead, system, hidden);
                FileList.Add(addfile);
                return;
            }

        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Storage ID", Id);
            info.AddValue("Storage Name", StoreName);
            info.AddValue("Files", FileList);
            info.AddValue("Max capacity", MaxCapacity);
            info.AddValue("Free capacity", FreeCapacity);
            info.AddValue("Reserved capacity", ReservedCapacity);
            

        }
        public Hdd(SerializationInfo info, StreamingContext context)
        {
            Id = (string)info.GetValue("Storage ID", typeof(string));
            StoreName = (string)info.GetValue("Storage Name", typeof(string));
            FileList = (List<File>)info.GetValue("Files", typeof(List<File>));
            MaxCapacity = (double)info.GetValue("Max capacity", typeof(double));
            FreeCapacity = (double)info.GetValue("Free capacity", typeof(double));
            ReservedCapacity = (double)info.GetValue("Reserved capacity", typeof(double));

        }

    }
}
