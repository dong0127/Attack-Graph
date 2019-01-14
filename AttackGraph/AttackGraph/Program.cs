using System;
using System.Collections.Generic;

namespace AttackGraph
{
    class Program
    {
        List<string> s = new List<string>() {"fasf", "fsf"};
        static void Main(string[] args)
        {
            Program p = new Program();

            Console.WriteLine(p.s[1]);
            //用户输入的初始条件
            Console.ReadKey();
        }
    }
}
