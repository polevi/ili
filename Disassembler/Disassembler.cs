using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Disassembler
{
    public class Disassembler
    {
        public static void Serialize(Assembly assembly, System.IO.Stream stream)
        {
            CultureInfo prevCulture = SwapCulture();

            try
            {
                OpCodeList.LoadOpCodes();

                Package package = new Package();

                foreach (MethodBase mb in FindMethods(assembly))
                {
                    MethodBody body = new MethodBody(mb);

                    String procName = Helper.GetFullMethodName(mb, assembly);

                    Procedure p = new Procedure(procName);
                    foreach (Instruction ili in body.GetInstructions())
                    {
                        ILInstr i = new ILInstr(ili, assembly);
                        p.AddInstruction(i);
                    }

                    package.AddProcedure(p);
                }

                package.Prepare();

                XmlSerializer s = new XmlSerializer(typeof(Package));
                System.IO.MemoryStream ms = new System.IO.MemoryStream();

                using (System.IO.StreamWriter wr = new System.IO.StreamWriter(stream, Encoding.UTF8, 8192, true))
                {
                    s.Serialize(wr, package);
                }
            }
            finally
            {
                SwapCulture(prevCulture);
            }
        }

        static IEnumerable<MethodBase> FindMethods(Assembly a)
        {
            Module[] modules = a.GetModules();
            for (int i = 0; i < modules.Length; i++)
            {
                Type[] types = modules[i].GetTypes();
                for (int k = 0; k < types.Length; k++)
                {
                    MethodBase[] mis = types[k].GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
                    for (int j = 0; j < mis.Length; j++)
                    {
                        yield return mis[j];
                    }
                    MethodBase[] cis = types[k].GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
                    for (int j = 0; j < cis.Length; j++)
                    {
                        yield return cis[j];
                    }
                }
            }
        }

        public static CultureInfo SwapCulture(CultureInfo culture = null)
        {
            if (culture == null)
            {
                CultureInfo userCulture = Thread.CurrentThread.CurrentCulture;
                CultureInfo newCulture = new System.Globalization.CultureInfo("ru-RU");
                newCulture.NumberFormat.NumberDecimalSeparator = ".";

                Thread.CurrentThread.CurrentCulture = newCulture;

                return newCulture;
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = culture;
                return null;
            }
        }

    }
}
