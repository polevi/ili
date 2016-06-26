using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using System.IO;

namespace Disassembler
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                Assembly assembly = Assembly.LoadFrom(args[0]);

                using (System.IO.FileStream fs = new System.IO.FileStream(assembly.Location + ".xml", FileMode.Create))
                    Disassembler.Serialize(assembly, fs);

                CreateOutput(assembly);
            }
        }

        static void CreateOutput(Assembly assembly)
        {
            using (System.IO.FileStream fs = new System.IO.FileStream(assembly.Location + ".out", FileMode.Create))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.AutoFlush = true;
                    Console.SetOut(sw);

                    Type t = assembly.GetType("Assembly.Main");
                    MethodInfo mi = t.GetMethod("EntryPoint", BindingFlags.Static | BindingFlags.Public);
                    mi.Invoke(null, new object[] { });
                }
            }
        }
    }
}
