using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{
    class Attack
    {
        private Precondition pre;
        private Effect eff;

        internal Precondition Pre { get => pre; set => pre = value; }
        internal Effect Eff { get => eff; set => eff = value; }

        public Effect isAttack()
        {
            
            return Eff;
        }
    }
}
