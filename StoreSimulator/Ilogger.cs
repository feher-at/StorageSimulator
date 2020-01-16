using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSimulator
{
    interface ILogger
    {
        void Error(string message);

        void UserInput(string message);

        void ConsoleInfo(string message);


    }
}
