using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{
    class Graph
    {
        private int source, target;
        private int[,] connection;

        public int Source { get => source; set => source = value; }
        public int Target { get => target; set => target = value; }
        public int[,] Connection { get => connection; set => connection = value; }

    }
}
