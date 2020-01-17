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
        public Hdd(string Id, int MaxCapacity, string StoreName) : base(Id, MaxCapacity, StoreName)
        {
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Id = {Id} Max capacity = {MaxCapacity}GB Name = {StoreName} Type = Hdd");
            return sb.ToString();
        }
    }
}
