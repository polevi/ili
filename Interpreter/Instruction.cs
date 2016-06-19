using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection.Emit;
using System.Reflection;

namespace Interpreter
{
    public class Instruction
    {
        public String Name { get; set; }
        public ushort Code { get; set; }
        public byte OpType { get; set; }
        public String ArgType { get; set; }
        public String ArgValue { get; set; }

        private IValue operand;
        private Type operandType;

        public void Prepare()
        {
            if (ArgType != null)
            {
                operandType = TypeHelper.FindType(ArgType);
                OperandType ot = (OperandType)Convert.ToInt16(OpType);
                String[] r;
                String[] m;
                switch (ot)
                {
                    case OperandType.InlineField:
                        ArgValue = new System.Text.RegularExpressions.Regex(@"\[\[(.+?)\]\]").Replace(ArgValue, "");
                        r = ArgValue.Split(new String[] { " " }, StringSplitOptions.None);
                        m = r[1].Split(new String[] { "::" }, StringSplitOptions.None);
                        if (m[0].StartsWith("local."))
                            operand = new IValue(m[1]);
                        else
                            operand = new IValue(TypeHelper.FindType(m[0]).GetField(m[1]));
                        break;
                    case OperandType.InlineMethod:
                        ArgValue = new System.Text.RegularExpressions.Regex(@"\[\[(.+?)\]\]").Replace(ArgValue, "");
                        r = ArgValue.Split(new String[] { " " }, StringSplitOptions.None);
                        m = r[1].Split(new String[] { "::" }, StringSplitOptions.None);
                        operand = new IValue(MethodFromString(r[0], m[0], m[1]));
                        break;
                    case OperandType.ShortInlineBrTarget:
                    case OperandType.InlineBrTarget:
                        operand = new IValue(Convert.ToInt32(ArgValue));
                        break;
                    case OperandType.InlineType:
                        if (ArgValue.StartsWith("local."))
                        {
                            operand = new IValue(ArgValue);
                            operandType = typeof(IValue);
                        }
                        else
                        {
                            operand = new IValue(TypeHelper.FindType(ArgValue));
                            operandType = TypeHelper.FindType(ArgValue);
                        }
                        break;
                    case OperandType.InlineString:
                        operand = new IValue(ArgValue);
                        break;
                    case OperandType.ShortInlineVar:
                        operand = new IValue(Convert.ToInt32(ArgValue));
                        break;
                    case OperandType.InlineI:
                    case OperandType.ShortInlineI:
                        operand = new IValue(Convert.ToInt32(ArgValue));
                        break;
                    case OperandType.InlineR:
                    case OperandType.ShortInlineR:
                        System.Globalization.CultureInfo userCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
                        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
                        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
                        try
                        {
                            operand = new IValue(Convert.ToDouble(ArgValue));
                        }
                        finally
                        {
                            System.Threading.Thread.CurrentThread.CurrentCulture = userCulture;

                        }
                        break;
                    case OperandType.InlineTok:
                        if (ArgValue.StartsWith("local."))
                            operand = new IValue(ArgValue);
                        else
                            operand = new IValue(TypeHelper.FindType(ArgValue));
                        break;
                    case OperandType.InlineSwitch:
                        operand = new IValue(Array.ConvertAll<String, int>(ArgValue.Split(new String[] { "," }, StringSplitOptions.None), item => System.Int32.Parse(item)));
                        break;
                    case OperandType.InlineI8:
                        operand = new IValue(Convert.ToUInt64(ArgValue));
                        break;
                }
            }
        }

        public static object MethodFromString(String returnType, String type, String name)
        {
            int pos = name.IndexOf('(');
            int posSpace = name.IndexOf(' ');
            String n = name.Substring(0, pos);
            String a = name.Substring(pos + 1, name.Length - pos - 2);

            Type[] args = null;
            if (!String.IsNullOrEmpty(a))
                args = TypeHelper.GetArguments(a);

            Type t;
            t = TypeHelper.FindType(type);

            if (t == null)
                throw new Exception("Type " + type + " is not found");

            if (n.Equals(".ctor"))
                return t.GetConstructor(args != null ? args : new Type[] { });
            else
            {
                object result = null;
                try
                {
                    result = t.GetMethod(n, args != null ? args : new Type[] { });
                }
                catch (AmbiguousMatchException) // several methods
                {
                    foreach (MethodInfo mi in t.GetMethods())
                    {
                        if (mi.ReturnType != TypeHelper.FindType(returnType))
                            continue;
                        if (!mi.Name.Equals(n))
                            continue;
                        ParameterInfo[] pis = mi.GetParameters();
                        bool found = true;
                        for (int paramIdx = 0; paramIdx < pis.Length; paramIdx++)
                        {
                            if (args[paramIdx] != pis[paramIdx].ParameterType)
                            {
                                found = false;
                                break;
                            }
                        }
                        if (found)
                        {
                            result = mi;
                            break;
                        }
                    }
                }

                if (result == null)
                    throw new Exception(String.Format("Unable to find method: {0} {1} {2}", returnType, type, name));
                return result;
            }
        }

    }
}
