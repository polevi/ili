using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly
{
    public static class Loops
    {
        public static void TestForeach()
        {
            var arr = new String[] { "one", "two", "three", "four", "five" };

            foreach (var s in arr)
            {
                Console.Write(s);
            }

            Console.WriteLine();
        }

        public static void TestFor()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.Write(i.ToString());
            }

            Console.WriteLine();
        }

        public static void TestWhile()
        {
            int i = 0;
            while(i < 5)
            {
                Console.Write(i.ToString());
                i++;
            }

            Console.WriteLine();
        }

        public static void TestDoWhile()
        {
            int i = 0;
            do
            {
                Console.Write(i.ToString());
                i++;
            }
            while(i<5);

            Console.WriteLine();
        }

        public static void TestGoto()
        {
            int i = 0;

            label:
                Console.Write(i.ToString());
                i++;

                if (i < 5)
                    goto label;

            Console.WriteLine();
        }
    }
}
