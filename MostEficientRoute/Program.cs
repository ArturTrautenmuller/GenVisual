using System;
using System.Collections.Generic;
using System.Linq;

namespace MostEficientRoute
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            
            Core core = new Core(10,100,0.2);

            core.Execute(100);
           

            Console.ReadKey();
        }
    }
}
