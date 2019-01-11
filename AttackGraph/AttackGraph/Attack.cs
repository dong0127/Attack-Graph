using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{
    class Attack: Graph
    {
        private string attackName;
        //还要有变量前因和后果
        List<string> nodeAttack = new List<string>();
        
        Stack<string> eff = new Stack<string>();

        public string AttackName { get => attackName; set => attackName = value; }

        public List<string> Node(Precondition preconditions)
        {
            return null;
        }
    }
}
