using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GA
{
    class Program
    {
        static void Main(string[] args)
        {

            var DATA_DIR = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            while (true)
            {
                Tools.WriteLineColor("Welcome to GA program", ConsoleColor.Yellow);
                Tools.WriteLineColor("=====================", ConsoleColor.Yellow);

                Functions.GenerateNames(DATA_DIR);

                Console.WriteLine("Press any key to continue !");
                Console.ReadLine();
                Console.Clear();
            }
        }


    }
}
