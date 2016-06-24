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

        public TValue(Type t, int n) //array
            : this()
        {
            value = Array.CreateInstance(t, n);
        }

        public object Value
        {
            get
            {
                return value;
            }
        }

        public T As<T>()
        {
            return (T)value;
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

        public bool IsRef
        {
            get
            {
                return addrFlags >= 1 && addrFlags <= 5;
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

        public static TValue CreateLocalRef(System.Int32 n)
        {
            TValue result = new TValue();
            result.addrFlags = 1;
            result.addrIndex = n;
            return result;
        }

        public static TValue CreateArgRef(System.Int32 n)
        {
            TValue result = new TValue();
            result.addrFlags = 2;
            result.addrIndex = n;
            return result;
        }

        public bool CheckIfFalseNullZero()
        {
            if (IsRef)
                return false; // ref is always true
            if (value == null) // null or empty
                return true;

            return value.GetType().IsValueType ? TValueHelper.CheckIfFalseNullZero(this) : false;
        }

        public int CompareTo(TValue other, bool signed = true)
        {
            if (value == null && other.Value == null)
                return 0;

            if (value != null && other.Value != null)
            {
                if (value.GetType().IsValueType && other.Value.GetType().IsValueType)
                    return TValueHelper.CompareTo(this, other, signed);

                if (!value.GetType().IsValueType && !other.Value.GetType().IsValueType)
                    return value.Equals(other.Value) ? 0 : 1;

                return 1;
            }

            return 1;
        }

    }
}
