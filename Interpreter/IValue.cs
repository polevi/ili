using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public class IValue
    {
        private object value;

        public IValue(object v)
        {
            value = v;
        }
    }
}
