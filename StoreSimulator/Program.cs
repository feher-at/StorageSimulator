using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreManager.Api;



namespace StoreSimulator
{
    class Program
    {
        static ConsoleLogger cl = new ConsoleLogger();
        static void Main(string[] args)
        {
            try
            {

                StorageHandler sh = new StorageHandler(cl);
                sh.StorageMenu();
            }
            catch(Exception ex)
            {
                cl.Error(ex.Message);
            }

        }
    }
}
