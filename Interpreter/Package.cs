using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public class Package
    {
        public List<Procedure> procedures = new List<Procedure>();
        
        public Package()
        {
        }

        private Dictionary<String, Procedure> allMethods;

        public void Prepare()
        {
            allMethods = new Dictionary<string, Procedure>();

            foreach (Procedure p in procedures)
            {
                p.Prepare();
                allMethods.Add(p.Name, p);
            }
        }

        public Procedure FindProcedure(String name)
        {
            return allMethods[name];
        }

        public void Run()
        {
            Procedure p = FindProcedure("System.Void local.Assembly.Main::EntryPoint()");

            IStack stack = new IStack();
            Frame frame = new Frame(this, stack);

            Interpreter i = new Interpreter();
            i.ExecuteProcedure(frame, p);
        }
    }
}
