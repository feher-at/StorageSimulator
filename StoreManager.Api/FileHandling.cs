using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;


namespace StoreManager.Api
{
    public class FileHandling
    {
       
        public static void SerializeProcess(List<Storage> storageList,string filepath)
        {

            using (Stream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write, FileShare.None))
            { 
                XmlSerializer serializer = new XmlSerializer(typeof(List<Storage>));
                serializer.Serialize(fs, storageList);
            }

        }
        public static void DeserializerProcess(List<Storage> storagelist, string filepath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Storage>));
            if (System.IO.File.Exists(filepath))
            {
                using (FileStream fs = System.IO.File.OpenRead(filepath))
                {
                    storagelist.AddRange((List<Storage>)serializer.Deserialize(fs));
                }
            }
            else
            {
                throw new FileNotFoundException("This file does not exist");
            }
        }
        
        
    }
}
