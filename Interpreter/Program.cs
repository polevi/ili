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
            String logName = Path.Combine(path.Trim(), @"Assembly\bin\Debug\assembly.dll.out");

            CheckResults(Run(fileName), logName);

            Console.ReadLine();
        }

        static Stream Run(String fileName)
        {
            XmlSerializer s = new XmlSerializer(typeof(Package));
            Package p;
            using (StreamReader sr = new StreamReader(fileName))
            {
                p = (Package)s.Deserialize(sr);
            }

            p.Prepare();

            TextWriter screenOutput = null;
            Stream output = OverrideOutput(ref screenOutput);
            try
            {
                p.Run();
            }
            finally
            {
                Console.SetOut(screenOutput);
            }

            return output;
        }

        static Stream OverrideOutput(ref TextWriter oldOutput)
        {
            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms);
            sw.AutoFlush = true;

            oldOutput = Console.Out;
            Console.SetOut(sw);

            return ms;
        }

        static void CheckResults(Stream output, String assemblyOutput)
        {
            Console.SetOut(Console.Out);

            String[] arr = File.ReadAllLines(assemblyOutput);

            output.Position = 0;
            using (var r = new System.IO.StreamReader(output))
            {
                int i = 0;
                while (!r.EndOfStream)
                {
                    String iLine = r.ReadLine();
                    String cLine = arr[i];

                    Console.WriteLine(iLine);

                    if (!iLine.Equals(cLine))
                        Console.WriteLine(String.Format("Error: {1}", cLine));
                    else
                        Console.WriteLine("ok");

                    i++;
                }
            }

        }
    }
}
