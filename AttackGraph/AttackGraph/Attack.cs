using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{
    class Attack: Graph
    {
        private string name;

        public string Name { get => name; set => name = value; }
    }
}
