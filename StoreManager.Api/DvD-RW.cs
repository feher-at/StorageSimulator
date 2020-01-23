using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;

namespace StoreManager.Api
{
    [Serializable()]
    public class DvD_RW : DVD, ISerializable
    {
        public DvD_RW()
        { }
        public DvD_RW(string Id, string Name) : base(Id, Name)
        {
            this.StoreName = Name;
        }

        public void Open()
        {
            if (ReadOnly == false)
                throw new Exception("The DvD is already Open");
            else
            {
                this.ReadOnly = false;
                this.FileList = new List<File>();
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Id = {Id} Max capacity = {MaxCapacity:F1}GB Name = {StoreName} Type = Dvd-rw");
            return sb.ToString();
        }
       
    }
}
