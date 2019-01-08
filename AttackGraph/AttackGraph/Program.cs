using System;

namespace AttackGraph
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] connect = new int[3, 3] { { 0, 1, 1 }, { 1, 0, 1 }, { 1, 1, 0 } };
            Console.WriteLine(connect[1,1]);
            Console.ReadKey();
        }
    }
}
