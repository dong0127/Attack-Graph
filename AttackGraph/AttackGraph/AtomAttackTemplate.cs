using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{

    class AtomAttackTemplate
    {

        List<Node> adjNodes = new List<Node>();
        Node atom;

        public AtomAttackTemplate(List<Node> adjNodes, Node atom)
        {
            this.AdjNodes = adjNodes;
            this.atom = atom;
        }

        internal List<Node> AdjNodes { get => adjNodes; set => adjNodes = value; }
        internal Node Atom { get => atom; set => atom = value; }
    }
}
