using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public class TValue
    {
        private object value;
        private byte addrFlags; //1 - local var, 2 - arguments, 3 - array
        private System.Int32 addrIndex;

        public TValue()
        {
        }

        public TValue(object v)
        {
            value = v;
        }

        public object Value
        {
            get
            {
                return value;
            }
        }

        public bool IsLocalRef
        {
            get
            {
                return addrFlags == 1;
            }
        }

        public bool IsArgRef
        {
            get
            {
                return addrFlags == 2;
            }
        }

        public bool IsArrayRef
        {
            get
            {
                return addrFlags == 3;
            }
        }

        public bool IsFieldRef
        {
            get
            {
                return addrFlags == 4;
            }
        }

        public bool IsValueRef
        {
            get
            {
                return addrFlags == 5;
            }
        }

        public int Index
        {
            get
            {
                return addrIndex;
            }
        }

        public void MakeValueRef()
        {
            addrFlags = 5;
        }

        public void MakeSimple()
        {
            addrFlags = 0;
        }
    }
}
