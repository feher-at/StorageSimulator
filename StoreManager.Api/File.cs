using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace StoreManager.Api
{
    [Serializable]
    public class File:ISerializable
    {
        public string FileName { get; set; }
        public double FileSize { get; set; }
        public bool OnlyRead { get;  set; }
        public string System { get;  set; }
        public bool Hidden { get;  set; }


        public File()
        { }
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
            sb.Append($"{this.FileName};{this.FileSize};{this.OnlyRead};{this.System};{this.Hidden}");
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
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            
            info.AddValue("File Name", FileName);
            info.AddValue("File Size", FileSize);
            info.AddValue("Read Only", OnlyRead);
            info.AddValue("System", System);
            info.AddValue("Hidden", Hidden);
        }
        public File(SerializationInfo info, StreamingContext context)
        {
            FileName = (string)info.GetValue("File Name", typeof(string));
            FileSize = (double)info.GetValue("File Size", typeof(double));
            OnlyRead = (bool)info.GetValue("Read Only", typeof(bool));
            System = (string)info.GetValue("System", typeof(string));
            Hidden = (bool)info.GetValue("Hidden", typeof(bool));
        }
        

    }
}
