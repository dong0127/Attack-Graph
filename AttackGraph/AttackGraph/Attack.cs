using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{
    class Attack
    {
        private int target, source;
        private Input input;
        private Effect eff;
        private AtomAttack atomAttack;
        
        public int Target { get => target; set => target = value; }
        public int Source { get => source; set => source = value; }
        internal Input Pre { get => input; set => input = value; }
        internal Effect Eff { get => eff; set => eff = value; }
        internal AtomAttack AtomAttack { get => atomAttack; set => atomAttack = value; }

        public string IsAttack()
        {
            input = new Input();
            string result = null;
            foreach (string key in input.Initial)
            {
                if (key.Contains("input.Start"))
                {
                    if (key.Contains("user") || key.Contains("root"))
                    {
                        foreach (List<string> atom in atomAttack.Precondition)
                        {
                            
                        }
                    }
                }
             
            }
            return result;
        }
    }
}
