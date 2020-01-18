using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSimulator
{
    class ConsoleLogger:ILogger
    {
        public void Error(string error)
        {
            string err = "ERROR";

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(err);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(" " + error);

        }
        
        public void UserInput(string userinput)
        {
            string input = "INPUT";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(input);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(" " + userinput);
        }
        public void ConsoleInfo(string Info)
        {
            string info = "INFO";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(info);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(" " + Info);

        }

        public void Warning(string warninginfo)
        {
            string warning = "WARNING";
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(warning);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(" " + warninginfo);
        }
    }
}
