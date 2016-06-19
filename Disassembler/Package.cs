using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disassembler
{
    public class Package
    {
        public List<Procedure> procedures = new List<Procedure>();

        public Package()
        {
        }

        public void AddProcedure(Procedure p)
        {
            procedures.Add(p);
        }

        public Procedure[] Procedures
        {
            get
            {
                return procedures.ToArray();
            }
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
