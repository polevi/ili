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

        public void Prepare()
        {
            foreach (Procedure p in procedures)
            {
                p.Prepare();
            }
        }
    }
}
