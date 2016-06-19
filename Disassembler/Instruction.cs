using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection.Emit;

namespace Disassembler
{
    public class Instruction
    {
        public object Operand { get; set; }
        public OpCode Code { get; set; }
        public int Offset { get; set; }
    }
}
