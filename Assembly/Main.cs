﻿using System;
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
            Console.WriteLine("==Loops==");

            Loops.TestForeach();
            Loops.TestFor();
            Loops.TestWhile();
            Loops.TestDoWhile();
            Loops.TestGoto();
        }
    }
}
