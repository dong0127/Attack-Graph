using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{

    class AtomAttackTemplate
    {
        
        List<Element> preconditions = new List<Element>();
        List<Element> effects = new List<Element>();
        string attackName;
        
        public AtomAttackTemplate(List<Element> preconditions, List<Element> effects, string attackName)
        {
            this.Preconditions = preconditions;
            this.Effects = effects;
            this.AttackName = attackName;
        }

        public string AttackName { get => attackName; set => attackName = value; }
        internal List<Element> Preconditions { get => preconditions; set => preconditions = value; }
        internal List<Element> Effects { get => effects; set => effects = value; }

    }
}
