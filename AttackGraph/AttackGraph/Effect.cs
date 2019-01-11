using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{
    class Effect: Graph
    {
        private string intruderEffect, networkEffect;
        List<string> edgeEffect = new List<string>();
        public string IntruderEffect { get => intruderEffect; set => intruderEffect = value; }
        public string NetworkEffect { get => networkEffect; set => networkEffect = value; }

        public List<string> Edge(Attack attack)
        {
            return null;
        }
    }
}
