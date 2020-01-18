﻿using System;
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
            StorageHandler sh = new StorageHandler(cl);
            sh.StorageMenu();

        }
    }
}
