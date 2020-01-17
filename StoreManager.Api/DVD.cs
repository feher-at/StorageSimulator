using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManager.Api
{
    public class DVD : Storage
    {

        protected bool readOnly;
        protected bool ReadOnly
        {
            get { return readOnly; }
            set { this.readOnly = value; }
        }
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


        public DVD(string Id, string Name) : base(Id, Name)
        {

            this.Id = Id;
            this.MaxCapacity = 4.58984375;
            this.ReadOnly = false;
        }

        public void Block()
        {
            this.ReadOnly = true;
            this.freeCapacity = 0;
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
            double DvdFileSize = fileSize / 1048576;
            base.AddFile(fileName, DvdFileSize, onlyRead, system, hidden);

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
        public new void listFile()
        {
            foreach (File element in fileList)
                Console.WriteLine(element.ToString());
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Id = {Id} Max capacity = {MaxCapacity:F1}GB Name = {StoreName} Type = Dvd");
            return sb.ToString();
        }

    }
}
