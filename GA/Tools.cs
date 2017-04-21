using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA
{
    public class Tools
    {
        public static void WriteLineColor(string s, ConsoleColor fg = ConsoleColor.White)
        {
            var x = Console.ForegroundColor;
            Console.ForegroundColor = fg;
            Console.WriteLine(s);
            Console.ForegroundColor = x;
        }

        public static void WriteColor(string s, ConsoleColor fg= ConsoleColor.White)
        {
            var x = Console.ForegroundColor;
            Console.ForegroundColor = fg;
            Console.Write(s);
            Console.ForegroundColor = x;
        }

        public static string ReadColor(string s, ConsoleColor fg = ConsoleColor.White)
        {
            var x = Console.ForegroundColor;
            Console.ForegroundColor = fg;
            Console.Write(s);
            var f = Console.ReadLine();
            Console.ForegroundColor = x;
            return f;
        }
    }
}
