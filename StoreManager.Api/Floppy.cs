using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManager.Api
{
    public class Floppy : Storage
    {

        private bool writeDefense;
        public bool WriteDefense { get { return writeDefense; } private set { writeDefense = value; } }
        public override double FreeCapacity
        {
            get
            {
                double freecap = this.MaxCapacity;
                foreach (File element in this.fileList)
                {
                    freecap -= element.FileSize;
                }
                this.freeCapacity = freecap;
                return this.freeCapacity;
            }
        }
        public override double ReservedCapacity
        {
            get
            {
                double reservedCap = 0;
                foreach (File element in this.fileList)
                {
                    reservedCap += element.FileSize;

                }
                this.reservedCapacity = reservedCap;
                return this.reservedCapacity;
            }
        }
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
            else
            {
                base.AddFile(fileName, fileSize, onlyRead, system, hidden);
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
    }
}
