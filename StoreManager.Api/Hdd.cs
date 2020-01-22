using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManager.Api
{
    public class Hdd : Storage
    {
        public override double FreeCapacity
        {
            get
            {
                double freecap = this.MaxCapacity;
                foreach (File element in this.fileList)
                {
                    freecap -= element.FileSize/ 1048576;
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
                    reservedCap += element.FileSize/ 1048576;

                }
                this.reservedCapacity = reservedCap;
                return this.reservedCapacity;
            }

        }
        
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

            if (fileList.Count > 0)
            {
                foreach (File element in fileList)
                {
                    if (element.FileName == fileName)
                        throw new Exception("This file is already in the file list");
                }
            }
            if (fileSize/ 1048576 > this.FreeCapacity)
                throw new Exception("There is not enough free capacity");
            else if (fileSize/ 1048576 <= this.freeCapacity)
            {
                File addfile = new File(fileName, fileSize, onlyRead, system, hidden);
                fileList.Add(addfile);
                return;
            }

        }

    }
}
