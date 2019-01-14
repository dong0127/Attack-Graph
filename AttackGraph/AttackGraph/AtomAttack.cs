using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{
    class AtomAttack
    {
        private string attackName;
        private int source, target;
        private List<string> preconditions = new List<string>();
        private List<string> effects = new List<string>();
        
        public AtomAttack(string attackName, int source, int target)
        {
            this.AttackName = attackName;
            this.Source = source;
            this.Target = target;
            switch (attackName)
            {
                case "sshd-bof":
                    List<string> precondition = new List<string>();
                    precondition.Add("user"+ source);
                    precondition.Add("sshd");
                    this.Preconditions = precondition;
                    this.Effects = effects;
                    break;
            }
            
        }

        public string AttackName { get => attackName; set => attackName = value; }
        public int Source { get => source; set => source = value; }
        public int Target { get => target; set => target = value; }
        public List<string> Effects { get => effects; set => effects = value; }
        public List<string> Preconditions { get => preconditions; set => preconditions = value; }
        
        
    }
}
