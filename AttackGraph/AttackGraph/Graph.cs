using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{
    class Graph
    {
        private int source, target;
        //private int[] connect = new int[2];

        public int Source { get => source; set => source = value; }
        public int Target { get => target; set => target = value; }
        //public int[] Connect { get => connect; set => connect = value; }
        public int[] connect()
        {
            int[] edge = new int[2] { source, target };
            return edge;
        }
    }
}
