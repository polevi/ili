using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public static class TValueHelper
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
            typeof(double),
            typeof(DateTime),
            typeof(TimeSpan),
            typeof(System.Decimal),
            typeof(System.Guid)
        };

        public delegate TValue B(TValue value);
        public delegate bool D(TValue value);
        public delegate int E(TValue value1, TValue value2, bool signed);

        static D[] checkZeroActions = new D[] {
            (TValue x) => x.As<int>() == 0,
            (TValue x) => x.As<int>() == 0,
            (TValue x) => x.As<int>() == 0,
            (TValue x) => x.As<int>() == 0,
            (TValue x) => x.As<int>() == 0,
            (TValue x) => x.As<int>() == 0,
            (TValue x) => x.As<int>() == 0,
            (TValue x) => x.As<Int64>() == 0,
            (TValue x) => x.As<UInt64>() == 0,
            (TValue x) => x.As<float>() == 0,
            (TValue x) => x.As<double>() == 0,
            (TValue x) => {throw new NotImplementedException();},
            (TValue x) => {throw new NotImplementedException();},
            (TValue x) => {throw new NotImplementedException();},
            (TValue x) => {throw new NotImplementedException();}
        };

        static E[,] compareActions = new E[,] {
            
            {//null
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();}
            },
            {//bool
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<Int64>()) : x.As<UInt32>().CompareTo(y.As<UInt64>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<Int64>()) : x.As<UInt32>().CompareTo(y.As<UInt64>()); },
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
            },
            {//sbyte
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<Int64>()) : x.As<UInt32>().CompareTo(y.As<UInt64>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<Int64>()) : x.As<UInt32>().CompareTo(y.As<UInt64>()); },
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
            },
            {//byte
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<Int64>()) : x.As<UInt32>().CompareTo(y.As<UInt64>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<Int64>()) : x.As<UInt32>().CompareTo(y.As<UInt64>()); },
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
            },
            {//int16
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<Int64>()) : x.As<UInt32>().CompareTo(y.As<UInt64>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<Int64>()) : x.As<UInt32>().CompareTo(y.As<UInt64>()); },
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},

            },
            {//uint16
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<Int64>()) : x.As<UInt32>().CompareTo(y.As<UInt64>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<Int64>()) : x.As<UInt32>().CompareTo(y.As<UInt64>()); },
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},

            },
            {//int
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<Int64>()) : x.As<UInt32>().CompareTo(y.As<UInt64>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<Int64>()) : x.As<UInt32>().CompareTo(y.As<UInt64>()); },
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},

            },
            {//uint32
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<int>()) : x.As<UInt32>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<Int64>()) : x.As<UInt32>().CompareTo(y.As<UInt64>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<int>().CompareTo(y.As<Int64>()) : x.As<UInt32>().CompareTo(y.As<UInt64>()); },
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},

            },
            {//int64
                (TValue x,TValue y,bool signed) => { return signed? x.As<Int64>().CompareTo(y.As<int>()) : x.As<UInt64>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<Int64>().CompareTo(y.As<int>()) : x.As<UInt64>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<Int64>().CompareTo(y.As<int>()) : x.As<UInt64>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<Int64>().CompareTo(y.As<int>()) : x.As<UInt64>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<Int64>().CompareTo(y.As<int>()) : x.As<UInt64>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<Int64>().CompareTo(y.As<int>()) : x.As<UInt64>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<Int64>().CompareTo(y.As<int>()) : x.As<UInt64>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<Int64>().CompareTo(y.As<Int64>()) : x.As<UInt64>().CompareTo(y.As<UInt64>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<Int64>().CompareTo(y.As<Int64>()) : x.As<UInt64>().CompareTo(y.As<UInt64>()); },
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},

            },
            {//uint64
                (TValue x,TValue y,bool signed) => { return signed? x.As<Int64>().CompareTo(y.As<int>()) : x.As<UInt64>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<Int64>().CompareTo(y.As<int>()) : x.As<UInt64>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<Int64>().CompareTo(y.As<int>()) : x.As<UInt64>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<Int64>().CompareTo(y.As<int>()) : x.As<UInt64>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<Int64>().CompareTo(y.As<int>()) : x.As<UInt64>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<Int64>().CompareTo(y.As<int>()) : x.As<UInt64>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<Int64>().CompareTo(y.As<int>()) : x.As<UInt64>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<Int64>().CompareTo(y.As<Int64>()) : x.As<UInt64>().CompareTo(y.As<UInt64>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<Int64>().CompareTo(y.As<Int64>()) : x.As<UInt64>().CompareTo(y.As<UInt64>()); },
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},

            },
            {//float
                (TValue x,TValue y,bool signed) => { return signed? x.As<double>().CompareTo(y.As<int>()) : x.As<double>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<double>().CompareTo(y.As<int>()) : x.As<double>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<double>().CompareTo(y.As<int>()) : x.As<double>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<double>().CompareTo(y.As<int>()) : x.As<double>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<double>().CompareTo(y.As<int>()) : x.As<double>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<double>().CompareTo(y.As<int>()) : x.As<double>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<double>().CompareTo(y.As<int>()) : x.As<double>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<double>().CompareTo(y.As<Int64>()) : x.As<double>().CompareTo(y.As<UInt64>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<double>().CompareTo(y.As<Int64>()) : x.As<double>().CompareTo(y.As<UInt64>()); },
                (TValue x,TValue y,bool signed) => { return x.As<double>().CompareTo(y.As<double>()); },
                (TValue x,TValue y,bool signed) => { return x.As<double>().CompareTo(y.As<double>()); },
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
            },
            {//double
                (TValue x,TValue y,bool signed) => { return signed? x.As<double>().CompareTo(y.As<int>()) : x.As<double>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<double>().CompareTo(y.As<int>()) : x.As<double>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<double>().CompareTo(y.As<int>()) : x.As<double>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<double>().CompareTo(y.As<int>()) : x.As<double>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<double>().CompareTo(y.As<int>()) : x.As<double>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<double>().CompareTo(y.As<int>()) : x.As<double>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<double>().CompareTo(y.As<int>()) : x.As<double>().CompareTo(y.As<UInt32>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<double>().CompareTo(y.As<Int64>()) : x.As<double>().CompareTo(y.As<UInt64>()); },
                (TValue x,TValue y,bool signed) => { return signed? x.As<double>().CompareTo(y.As<Int64>()) : x.As<double>().CompareTo(y.As<UInt64>()); },
                (TValue x,TValue y,bool signed) => { return x.As<double>().CompareTo(y.As<double>()); },
                (TValue x,TValue y,bool signed) => { return x.As<double>().CompareTo(y.As<double>()); },
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
            },
            {//DateTime
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();}
            },
            {//TimeSpan
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();}
            },
            {//Decimal
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();}
            },
            {//Guid
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();},
                (TValue x,TValue y,bool signed) => {throw new NotImplementedException();}
            }
        };

        public static bool CheckIfFalseNullZero(this TValue value)
        {
            if (value.IsRef)
                return false; // ref is always true

            if (value.Value == null) // null or empty
                return true;

            if (!value.Value.GetType().IsValueType)
                return false;
            else

            return checkZeroActions[Array.IndexOf<Type>(valueTypes, value.Value.GetType())](value);
        }

        public static int CompareTo(TValue value1, TValue value2, bool signed)
        {
            return compareActions[Array.IndexOf<Type>(valueTypes, value1.Value.GetType()) + 1, Array.IndexOf<Type>(valueTypes, value2.Value.GetType())](value1, value2, signed);
        }

        public static TValue ConvertTo(TValue value, Type t)
        {
            return new TValue(value.Value);
            //return convertActions[!value.Value.GetType().IsValueType ? 0 : Array.IndexOf<Type>(valueTypes, value.Value.GetType()) + 1, Array.IndexOf<Type>(valueTypes, t)](value);
        }
    }
}
