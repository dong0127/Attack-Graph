using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{
    class Attacks
    {
        List<AtomAttackTemplate> attack;

        public Attacks(List<AtomAttackTemplate> attacks)
        {
            this.Attack = attack;
        }

        public List<AtomAttackTemplate> Attack { get => attack; set => attack = value; }
    }
}
