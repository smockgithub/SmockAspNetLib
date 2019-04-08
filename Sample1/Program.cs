using System;
using System.Collections.Generic;

namespace Samples.Sample1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            List<int> list = new List<int>();

            int i = 10000;
            int j = 2;
            while (i>0)
            {
                Random rd = new Random();
                var result = rd.Next(1, j+1);
                Console.Write(result + ",");
                list.Add(result);
                i--;
            }

            Console.WriteLine("-------------");

            for(int a = 0; a <= j; a++)
            {
                Console.WriteLine(a+"：" + list.FindAll(e => e == a).Count);
            }

            Console.ReadKey();
        }
    }
}
