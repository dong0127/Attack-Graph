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

        public List<AtomAttackTemplate> GetIntialStep(List<Element> initialKnowlege)
        {

            return null;
        }
     
        public List<AtomAttackTemplate> Attack { get => attack; set => attack = value; }
        
    }
}
