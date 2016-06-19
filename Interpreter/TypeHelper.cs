using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    internal class TypeHelper
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

        static String FindNextArgument(String s, int startPos)
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

        /*
        public static Type FindTypeOld(String typeName)
        {
            if (_types == null)
                _types = new Dictionary<string, Type>();

            Type result;
            if (!_types.TryGetValue(typeName, out result))
            {
                String[] arr = typeName.Split(new char[] { '`' });
                bool isGeneric = false;
                if (arr.Length == 2)
                    isGeneric = (arr[1].Contains('['));

                if (!isGeneric)
                {
                    foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        Type type = a.GetType(typeName);

                        if (type != null)
                        {
                            _types.Add(typeName, type);
                            return type;
                        }
                    }
                }
                else
                {
                    int cnt = Int32.Parse(arr[1].Substring(0, 1));

                    int p1 = arr[1].IndexOf(']');
                    String s = arr[1].Substring(2, p1 - 2);


                    Type t = FindType(String.Format("{0}`{1}", arr[0], cnt.ToString()));
                    Type[] args = new Type[cnt];
                    String[] ts = arr[1].Substring(2, arr[1].Length - 3).Split(new char[] { ',' });
                    for (int i = 0; i < ts.Length; i++)
                    {
                        args[i] = FindType(ts[i]);
                    }
                    result = t.MakeGenericType(args);
                    _types.Add(typeName, result);
                    return result;
                }

                return TypeInfo.GetType(typeName);
            }
            else
            {
                if (result == null)
                    throw new Exception(String.Format("Type '{0}' is not found.", typeName));
                else
                    return result;
            }
        }
        */

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
    }
}
