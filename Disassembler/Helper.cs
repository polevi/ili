using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Disassembler
{
    public class Helper
    {
        public static string ProcessSpecialTypes(Type type, Assembly localAssembly)
        {
            return type.Assembly.Equals(localAssembly) ? String.Format("local.{0}", type.ToString()) : type.ToString();
        }

        public static string ParametersAsString(System.Reflection.ParameterInfo[] parameters, bool isLocal = false)
        {
            StringBuilder sb = new StringBuilder();
            if (isLocal)
                sb.Append("System.Object");
            foreach (System.Reflection.ParameterInfo pi in parameters)
            {
                if (sb.Length != 0)
                    sb.Append(",");
                sb.Append(pi.ParameterType.ToString());
            }
            sb.Insert(0, "(");
            sb.Append(")");
            return sb.ToString();
        }

        public static String GetFullMethodName(MethodBase mb, Assembly assembly)
        {
            if (mb is MethodInfo)
                return String.Format("{0} {1}::{2}{3}", ((MethodInfo)mb).ReturnType.ToString(), Helper.ProcessSpecialTypes(mb.ReflectedType, assembly), mb.Name, Helper.ParametersAsString(mb.GetParameters(), !mb.IsStatic));
            else
                return String.Format("System.Void {0}::{1}{2}", Helper.ProcessSpecialTypes(mb.ReflectedType, assembly), mb.Name, Helper.ParametersAsString(mb.GetParameters(), !mb.IsStatic));
        }
    }
}
