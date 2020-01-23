using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace StoreManager.Api
{
    [Serializable()]
    public class Floppy : Storage,ISerializable
    {

       
        public bool WriteDefense { get; set; }
        public override double FreeCapacity
        {
            get
            {
                double freecap = this.MaxCapacity;
                foreach (File element in this.FileList)
                {
                    freecap -= element.FileSize;
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
                    reservedCap += element.FileSize;

                }

                return reservedCap;
            }
            set { }
        }
        public Floppy()
        { }
        public Floppy(string Id, string name) : base(Id, name)
        {
            this.Id = Id;
            this.StoreName = name;
            this.MaxCapacity = 1700;
            this.WriteDefense = false;
        }

        public void TurnOnWriteDefense()
        {
            this.WriteDefense = true;
        }
        public override void Format()
        {
            if (WriteDefense == true)
            {
                throw new Exception("The floppy has write defense");
            }
            else
                base.Format();
        }

        public override void AddFile(string fileName, double fileSize, bool onlyRead, string system, bool hidden)
        {
            if (WriteDefense == true)
            {
                throw new Exception("The floppy has write defense");
            }
            if (FileList.Count > 0)
            {
                foreach (File element in FileList)
                {
                    if (element.FileName == fileName)
                        throw new Exception("This file is already in the file list");
                }
            }
            if (fileSize > this.FreeCapacity)
                throw new Exception("There is not enough free capacity");
            else if (fileSize <= this.FreeCapacity)
            {
                File addfile = new File(fileName, fileSize, onlyRead, system, hidden);
                FileList.Add(addfile);
                return;
            }
        }

        public override void Remove(string fileName)
        {
            if (this.WriteDefense == true)
            {
                throw new Exception("The floppy has write defense");
            }

            else
            {
                base.Remove(fileName);
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Id = {Id} Max capacity = {MaxCapacity}Kb Name = {StoreName} Type = Floppy");
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
            info.AddValue("Write Defense", WriteDefense);

        }
        public Floppy(SerializationInfo info, StreamingContext context)
        {
            Id = (string)info.GetValue("Storage ID", typeof(string));
            StoreName = (string)info.GetValue("Storage Name", typeof(string));
            FileList = (List<File>)info.GetValue("Files", typeof(List<File>));
            MaxCapacity = (double)info.GetValue("Max capacity", typeof(double));
            FreeCapacity = (double)info.GetValue("Free capacity", typeof(double));
            ReservedCapacity = (double)info.GetValue("Reserved capacity", typeof(double));
            WriteDefense = (bool)info.GetValue("Write Defense", typeof(bool));

        }
    }
}
