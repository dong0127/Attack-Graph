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
            List<string> precondition = new List<string>();
            List<string> effect = new List<string>();
            switch (attackName)
            {
                case "sshd-bof":
                    precondition.Add("user" + "," + source);
                    precondition.Add("sshd" + "," + target);
                    effect.Add("user" + "," + target);
                    this.Preconditions = precondition;
                    this.Effects = effects;
                    break;
                case "Ftp-rhosts":
                    precondition.Add("user" + "," + source);
                    precondition.Add("ftp" + "," + target);
                    effect.Add("trust" + "," + source + "," + target);
                    this.Preconditions = precondition;
                    this.Effects = effects;
                    break;
                case "rsh":
                    precondition.Add("user" + "," + source);
                    precondition.Add("trust" + "," + source + ","+ target);
                    effect.Add("user" + "," + target);
                    this.Preconditions = precondition;
                    this.Effects = effects;
                    break;
                case "local-bof":
                    precondition.Add("user" + "," + target);
                    effect.Add("root" + "," + target);
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
