using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{
    class Effect: Graph
    {
        private string intruderEffect, networkEffect;

        public string IntruderEffect { get => intruderEffect; set => intruderEffect = value; }
        public string NetworkEffect { get => networkEffect; set => networkEffect = value; }
    }
}
