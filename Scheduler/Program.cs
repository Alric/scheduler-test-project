﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var arg in args)
            {
                try
                {
                    Console.WriteLine("The cost for " + arg + " is $" + Scheduler.ComputeCost(arg));
                }
                catch
                {
                    Console.WriteLine("Error when attempting to process file " + arg);
                }
            }
        }
    }
}
