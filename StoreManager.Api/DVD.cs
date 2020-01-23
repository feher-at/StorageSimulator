using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace StoreManager.Api
{
    [Serializable()]
    public class DVD : Storage,ISerializable
    {

        
        public bool ReadOnly { get; set; }
        
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
            set { }
        }
        public override double ReservedCapacity
        {
            get
            {
                double reservedCap = 0;
                foreach (File element in this.FileList)
                {
                    reservedCap += element.FileSize / 1048576;

                }
                
                return reservedCap;
            }
            set { }
        }
        public DVD()
        { }

        public DVD(string Id, string Name) : base(Id, Name)
        {

            this.Id = Id;
            this.MaxCapacity = 4.58984375;
            this.ReadOnly = false;
        }

        public void Block()
        {
            this.ReadOnly = true;
            this.FreeCapacity = 0;
        }

        public override void Format()
        {
            if (this.ReadOnly == true)
                throw new Exception("The DvD has been blocked sorry");
            else
            {
                base.Format();
            }
        }

        public override void AddFile(string fileName, double fileSize, bool onlyRead, string system, bool hidden)
        {
            if (ReadOnly == true)
            {
                throw new Exception("The DvD has been blocked sorry");
            }

            if (FileList.Count > 0)
            {
                foreach (File element in FileList)
                {
                    if (element.FileName == fileName)
                        throw new Exception("This file is already in the file list");
                }
            }
            if (fileSize / 1048576 > this.FreeCapacity)
                throw new Exception("There is not enough free capacity");
            else if (fileSize / 1048576 <= this.FreeCapacity)
            {
                File addfile = new File(fileName, fileSize, onlyRead, system, hidden);
                FileList.Add(addfile);
                return;
            }

        }

        public override void Remove(string fileName)
        {
            if (this.ReadOnly == true)
            {
                throw new Exception("There is not enough free capacity");
            }
            else
            {
                base.Remove(fileName);
            }




        }
        

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Id = {Id} Max capacity = {MaxCapacity:F1}GB Name = {StoreName} Type = Dvd");
            return sb.ToString();
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Storage ID", Id);
            info.AddValue("Storage Name", StoreName);
            info.AddValue("Files", FileList);
            info.AddValue("Max capacity", MaxCapacity);
            info.AddValue("Free capacity", FreeCapacity);
            info.AddValue("Reserved capacity", ReservedCapacity);
            info.AddValue("Read Only", ReadOnly);

        }
        public DVD(SerializationInfo info, StreamingContext context)
        {
            Id = (string)info.GetValue("Storage ID", typeof(string));
            StoreName = (string)info.GetValue("Storage Name", typeof(string));
            FileList = (List<File>)info.GetValue("Files", typeof(List<File>));
            MaxCapacity = (double)info.GetValue("Max capacity", typeof(double));
            FreeCapacity = (double)info.GetValue("Free capacity", typeof(double));
            ReservedCapacity = (double)info.GetValue("Reserved capacity", typeof(double));
            ReadOnly = (bool)info.GetValue("Read Only", typeof(bool));

        }

    }
}
