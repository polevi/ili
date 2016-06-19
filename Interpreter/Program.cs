using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            String path = File.ReadAllText("assembly.txt");
            String fileName = Path.Combine(path.Trim(), @"Assembly\bin\Debug\assembly.dll.xml");

            Console.WriteLine(fileName);
            Console.ReadLine();
        }
    }
}
