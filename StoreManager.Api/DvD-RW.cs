using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManager.Api
{
    public class DvD_RW : DVD
    {
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
                this.fileList = new List<File>();
            }
        }


    }
