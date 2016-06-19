using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public class Frame
    {
        private Package package;
        private IStack stack;
        private IValue[] locals;
        private IValue[] arguments;

        public Frame(Package package, IStack stack)
        {
            this.package = package;
            this.stack = stack;
            this.locals = new IValue[256];
        }

        public void LoadArguments(Type[] args)
        {
            if (args.Length == 0 || stack.Count == 0)
                return;
            arguments = new IValue[args.Length];
            for (int i = args.Length - 1; i >= 0; i--)
                arguments[i] = stack.Pop();
        }

        public void Push(IValue value)
        {
            stack.Push(value);
        }

        public IValue Pop()
        {
            return stack.Pop();
        }

        public IValue[] Locals
        {
            get
            {
                return locals;
            }
        }

        public IValue[] Arguments
        {
            get
            {
                return arguments;
            }
        }

        public Package Package
        {
            get
            {
                return this.package;
            }
        }

    }
}
