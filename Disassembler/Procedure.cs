using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disassembler
{
    public class Procedure
    {
        public String Name { get; set; }
        public List<ILInstr> instructions = new List<ILInstr>();

        public Procedure()
        {
        }

        public Procedure(String name)
        {
            this.Name = name;
        }

        public void AddInstruction(ILInstr instruction)
        {
            instructions.Add(instruction);
        }

        public ILInstr[] Instructions
        {
            get
            {
                return instructions.ToArray();
            }
        }

        public void Prepare()
        {
            Dictionary<int, int> addrs = new Dictionary<int, int>();

            for (int i = 0; i < instructions.Count; i++)
            {
                addrs.Add(instructions[i].GetOffset(), i);
            }

            foreach (ILInstr ili in instructions)
            {
                ili.Prepare(addrs);
            }
        }
    }
}
