using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManager.Api
{
    public class File
    {
        public string FileName { get; set; }
        public double FileSize { get; }
        public bool OnlyRead { get; private set; }
        public string System { get; private set; }
        public bool Hidden { get; private set; }



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

        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }
            if (!(obj is File))
            { return false; }
            return this.FileName == ((File)obj).FileName &&
                    this.FileSize == ((File)obj).FileSize;
        }
        public override int GetHashCode()
        {
            return this.FileName.GetHashCode() ^ this.FileSize.GetHashCode();
        }
    }
}
