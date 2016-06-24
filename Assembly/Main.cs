using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly
{
    public class Main
    {
        public static void EntryPoint()
        {
            TestMath<int>(3.14, 9.8);
            TestFor();
        }

        public static void TestFor()
        {
            var arr = new String[] { "one", "two", "three" };

            foreach (var v in arr)
            {
                Console.WriteLine(v);
            }

            /*
            for (int i = 0; i < 10; i++)
            {
                PrintTime();
                HelloWorld();
            }
            */
        }

        public static void TestMath<T>(double a, double b)
        {
            Console.WriteLine(String.Format("{0}", (a - b).ToString()));
        }

        public static void HelloWorld()
        {
            Console.WriteLine(GetHello());
        }

        public static String GetHello()
        {
            return "Hello" + " " + "world!";
        }

        public static void PrintTime()
        {
            Console.WriteLine(DateTime.Now.ToLongTimeString());
        }

    }
}
