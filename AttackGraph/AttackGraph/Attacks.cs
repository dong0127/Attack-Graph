using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{
    class Attacks
    {
        List<AtomAttackTemplate> attack;
        string s, t;      //source host and target host

        public Attacks(List<AtomAttackTemplate> attack, string s, string t)
        {
            this.attack = attack;
            this.s = s;
            this.t = t;
        }

        Stack<AtomAttackTemplate> visited = new Stack<AtomAttackTemplate>();

        public List<AtomAttackTemplate> GetIntialStep(List<Node> pri, List<Node> ser)
        {
            foreach(Node p in pri)
            {
                if(p.Name!="none" && p.From==s)
              
            }
            return null;
        }

        public List<AtomAttackTemplate> Attack { get => attack; set => attack = value; }
        
    }
}
