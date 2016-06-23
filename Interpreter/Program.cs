using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Interpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            String path = File.ReadAllText("assembly.txt");
            String fileName = Path.Combine(path.Trim(), @"Assembly\bin\Debug\assembly.dll.xml");

            XmlSerializer s = new XmlSerializer(typeof(Package));
            Package p;
            using (StreamReader sr = new StreamReader(fileName))
            {
                p = (Package)s.Deserialize(sr);
                p.Prepare();
            }

            p.Run();

            Console.ReadLine();
        }
    }
}
