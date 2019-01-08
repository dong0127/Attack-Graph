using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{
    class Graph
    {
        private int source, target;
     
        public int Source { get => source; set => source = value; }
        public int Target { get => target; set => target = value; }
       
        //得到主机的数量
        public int GetHostNumber(int[,] connect)
        {
            return connect.Length;
        }

    }
}
