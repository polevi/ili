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
        private TStack stack;
        private TValue[] locals;
        private TValue[] arguments;

        public Frame(Package package, TStack stack)
        {
            this.package = package;
            this.stack = stack;
            this.locals = new TValue[256];
        }

        public void LoadArguments(Type[] args)
        {
            if (args.Length == 0 || stack.Count == 0)
                return;
            arguments = new TValue[args.Length];
            for (int i = args.Length - 1; i >= 0; i--)
                arguments[i] = stack.Pop();
        }

        public void Push(TValue value)
        {
            stack.Push(value);
        }

        public TValue Pop()
        {
            return stack.Pop();
        }

        public TValue Peek()
        {
            return stack.Peek();
        }

        public TValue[] Locals
        {
            get
            {
                return locals;
            }
        }

        public TValue[] Arguments
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

        public TValue ResolveRef(TValue obj)
        {
            if (obj.IsLocalRef)
                obj = this.Locals[obj.Index];
            if (obj.IsArgRef)
                obj = this.Arguments[obj.Index];
            if (obj.IsArrayRef)
                obj = new TValue(((Array)obj.Value).GetValue(obj.Index));
            return obj;
        }

        public void AssignRef(TValue obj, TValue value)
        {
            if (obj.IsLocalRef)
                this.Locals[obj.Index] = value;
            if (obj.IsArgRef)
            {
                bool isRefArgument = this.Arguments[obj.Index].IsValueRef;
                this.Arguments[obj.Index] = value;
                if (isRefArgument)
                    this.Arguments[obj.Index].MakeValueRef();
                else
                    this.Arguments[obj.Index].MakeSimple();
            }
            if (obj.IsArrayRef)
                ((Array)obj.Value).SetValue(value.Value, obj.Index);
        }

        public void LdArg(byte n)
        {
            if (this.Arguments[n].IsValueRef)
                this.Push(TValue.CreateArgRef(n));
            else
                this.Push(this.Arguments[n]);
        }
    }
}
