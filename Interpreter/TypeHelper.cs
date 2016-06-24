using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    internal static class TypeHelper
    {
        static Dictionary<string, Type> _types;

        public static Type FindType(String s)
        {
            if (_types == null)
                _types = new Dictionary<string, Type>();

            Type result = null;
            if (!_types.TryGetValue(s, out result))
            {
                int p1 = s.IndexOf('[');
                if (p1 >= 0 && s.Contains('`')) //generic
                {
                    String q = s.Substring(p1 + 1, s.Length - p1 - 2);

                    Type[] args = GetArguments(q);

                    Type gt = FindType(s.Substring(0, p1));
                    result = gt.MakeGenericType(args);
                    _types.Add(s, result);
                }
                else
                {
                    result = Type.GetType(s);
                    if (result == null)
                    {
                        foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                        {
                            Type type = a.GetType(s);

                            if (type != null)
                            {
                                _types.Add(s, type);
                                result = type;
                                break;
                            }
                        }
                    }
                }
            }

            //            if (result == null)
            //                throw new Exception(String.Format("Type '{0}' is not found.", s));
            //            else
            return result;
        }

        public static String ExtractArguments(this String value)
        {
            int idx = value.IndexOf('(');
            return value.Substring(idx + 1, value.Length - idx - 2);
        }

        public static Type[] GetArguments(String q)
        {
            int idx = 0;

            List<Type> args = new List<Type>();
            do
            {
                String arg = FindNextArgument(q, idx);
                idx = idx + arg.Length + 1;
                args.Add(FindType(arg));
            }
            while (idx < q.Length);

            return args.ToArray();
        }

        public static Type[] GetGenericSubArguments(String s)
        {
            int p1 = s.IndexOf('[');
            String q = s.Substring(p1 + 1, s.Length - p1 - 2);
            return GetArguments(q);
        }

        private static String FindNextArgument(String s, int startPos)
        {
            int idx = s.Length;
            int cnt = 0;
            for (int i = startPos; i < s.Length; i++)
            {
                char c = s[i];
                if (c.Equals('['))
                    cnt++;
                if (c.Equals(']'))
                    cnt--;
                if (c.Equals(',') && cnt == 0)
                {
                    idx = i;
                    break;
                }
            }
            return s.Substring(startPos, idx - startPos);
        }

        public static Type[] ArgumentsFromString(String name)
        {
            int pos = name.IndexOf('(');
            String n = name.Substring(0, pos);
            String a = name.Substring(pos + 1, name.Length - pos - 2);

            Type[] args = null;
            if (!String.IsNullOrEmpty(a))
            {
                args = GetArguments(a);
            }
            return args != null ? args : new Type[] { };
        }

        public static bool IsNoArgFunc(this string value)
        {
            return value.IndexOf("()") >= 0;
        }

        public static bool IsConstructor(this string value)
        {
            return value.IndexOf(".ctor") >= 0;
        }

    }
}
