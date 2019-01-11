using System;
using System.Collections.Generic;

namespace AttackGraph
{
    class Program
    {

        static void Main(string[] args)
        {
            //用户输入的初始条件
            Dictionary<string, int> pre = new Dictionary<string, int>();
            int[,] connect = new int[3, 3] { { 0, 1, 1 }, { 1, 0, 1 }, { 1, 1, 0 } };
            int source = 0;
            int finalTarget = 2;
            pre.Add("ftp", 1);
            pre.Add("sshd", 1);
            pre.Add("ftp", 2);
            pre.Add("user", source);
            Graph graph = new Graph();
            graph.Source = source;
            graph.Target = finalTarget;
            graph.Connection = connect;
            Console.WriteLine(connect[1,1]);
            Console.ReadKey();
        }
    }
}
