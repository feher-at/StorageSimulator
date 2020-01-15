﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManager.Api
{
    public class File
    {
        public string FileName { get; set; }
        public double FileSize { get; }
        private bool OnlyRead { get; set; }
        private string System { get; set; }
        private bool Hidden { get; set; }



        public File(string fileName, double fileSize, bool onlyRead, string system, bool hidden)
        {
            this.FileName = fileName;
            this.FileSize = fileSize;
            this.OnlyRead = onlyRead;
            this.System = system;
            this.Hidden = hidden;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{this.FileName},{this.FileSize},{this.OnlyRead},{this.System},{this.Hidden}");
            return sb.ToString();
        }

    }
}