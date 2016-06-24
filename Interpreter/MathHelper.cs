using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public class MathHelper
    {
        static Type[] valueTypes = new Type[] {
            typeof(System.Boolean),
            typeof(sbyte),
            typeof(byte),
            typeof(System.Int16), 
            typeof(System.UInt16),
            typeof(System.Int32),
            typeof(System.UInt32),
            typeof(System.Int64),
            typeof(System.UInt64),
            typeof(float),
            typeof(double)
        };

        public delegate TValue E(TValue value1, TValue value2);

        static E[,] actions = new E[,] {
            
                {//add
                    (TValue x,TValue y) => {throw new NotImplementedException();},
                    (TValue x,TValue y) => {return new TValue(x.As<int>() + y.As<int>());},
                    (TValue x,TValue y) => {return new TValue(x.As<int>() + y.As<int>());},
                    (TValue x,TValue y) => {return new TValue(x.As<int>() + y.As<int>());},
                    (TValue x,TValue y) => {return new TValue(x.As<int>() + y.As<int>());},
                    (TValue x,TValue y) => {return new TValue(x.As<Int32>() + y.As<Int32>());},
                    (TValue x,TValue y) => {return new TValue(x.As<UInt32>() + y.As<UInt32>());},
                    (TValue x,TValue y) => {return new TValue(x.As<Int64>() + y.As<Int64>());},
                    (TValue x,TValue y) => {return new TValue(x.As<UInt64>() + y.As<UInt64>());},
                    (TValue x,TValue y) => {return new TValue(x.As<float>() + y.As<float>());},
                    (TValue x,TValue y) => {return new TValue(x.As<double>() + y.As<double>());},
                },
                {//sub
                    (TValue x,TValue y) => {throw new NotImplementedException();},
                    (TValue x,TValue y) => {return new TValue(x.As<int>() - y.As<int>());},
                    (TValue x,TValue y) => {return new TValue(x.As<int>() - y.As<int>());},
                    (TValue x,TValue y) => {return new TValue(x.As<int>() - y.As<int>());},
                    (TValue x,TValue y) => {return new TValue(x.As<int>() - y.As<int>());},
                    (TValue x,TValue y) => {return new TValue(x.As<Int32>() - y.As<Int32>());},
                    (TValue x,TValue y) => {return new TValue(x.As<UInt32>() - y.As<UInt32>());},
                    (TValue x,TValue y) => {return new TValue(x.As<Int64>() - y.As<Int64>());},
                    (TValue x,TValue y) => {return new TValue(x.As<UInt64>() - y.As<UInt64>());},
                    (TValue x,TValue y) => {return new TValue(x.As<float>() - y.As<float>());},
                    (TValue x,TValue y) => {return new TValue(x.As<double>() - y.As<double>());},
                },
                {//mul
                    (TValue x,TValue y) => {throw new NotImplementedException();},
                    (TValue x,TValue y) => {return new TValue(x.As<int>() * y.As<int>());},
                    (TValue x,TValue y) => {return new TValue(x.As<int>() * y.As<int>());},
                    (TValue x,TValue y) => {return new TValue(x.As<int>() * y.As<int>());},
                    (TValue x,TValue y) => {return new TValue(x.As<int>() * y.As<int>());},
                    (TValue x,TValue y) => {return new TValue(x.As<Int32>() * y.As<Int32>());},
                    (TValue x,TValue y) => {return new TValue(x.As<UInt32>() * y.As<UInt32>());},
                    (TValue x,TValue y) => {return new TValue(x.As<Int64>() * y.As<Int64>());},
                    (TValue x,TValue y) => {return new TValue(x.As<UInt64>() * y.As<UInt64>());},
                    (TValue x,TValue y) => {return new TValue(x.As<float>() * y.As<float>());},
                    (TValue x,TValue y) => {return new TValue(x.As<double>() * y.As<double>());},
                },
                {//div
                    (TValue x,TValue y) => {throw new NotImplementedException();},
                    (TValue x,TValue y) => {return new TValue(x.As<int>() / y.As<int>());},
                    (TValue x,TValue y) => {return new TValue(x.As<int>() / y.As<int>());},
                    (TValue x,TValue y) => {return new TValue(x.As<int>() / y.As<int>());},
                    (TValue x,TValue y) => {return new TValue(x.As<int>() / y.As<int>());},
                    (TValue x,TValue y) => {return new TValue(x.As<Int32>() / y.As<Int32>());},
                    (TValue x,TValue y) => {return new TValue(x.As<UInt32>() / y.As<UInt32>());},
                    (TValue x,TValue y) => {return new TValue(x.As<Int64>() / y.As<Int64>());},
                    (TValue x,TValue y) => {return new TValue(x.As<UInt64>() / y.As<UInt64>());},
                    (TValue x,TValue y) => {return new TValue(x.As<float>() / y.As<float>());},
                    (TValue x,TValue y) => {return new TValue(x.As<double>() / y.As<double>());},
                }
           };

        public static TValue Add(TValue a, TValue b)
        {
            return actions[0, Array.IndexOf<Type>(valueTypes, a.Value.GetType())](a, b);
        }

        public static TValue Sub(TValue a, TValue b)
        {
            return actions[1, Array.IndexOf<Type>(valueTypes, a.Value.GetType())](a, b);
        }

        public static TValue Mul(TValue a, TValue b)
        {
            return actions[2, Array.IndexOf<Type>(valueTypes, a.Value.GetType())](a, b);
        }

        public static TValue Div(TValue a, TValue b)
        {
            return actions[3, Array.IndexOf<Type>(valueTypes, a.Value.GetType())](a, b);
        }
    }
}
