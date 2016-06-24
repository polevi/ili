using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public class Procedure
    {
        public String Name { get; set; }
        public List<Instruction> instructions = new List<Instruction>();

        private Type[] arguments;

        public Instruction[] GetInstructions()
        {
            return instructions.ToArray();
        }

        public Type[] GetArguments()
        {
            return arguments;
        }

        public void Prepare()
        {
            arguments = TypeHelper.ArgumentsFromString(Name.Split(new String[] { "::" }, StringSplitOptions.RemoveEmptyEntries)[1]);

            foreach (Instruction ili in instructions)
            {
                ili.Prepare();
            }
        }
    }
}
