using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{

    class Template
    {
        string name;
        List<Element> preconditions = new List<Element>();
        Element postcondition = new Element();

        public Template(string name, List<Element> preconditions, Element postcondition)
        {
            this.name = name;
            this.preconditions = preconditions;
            this.postcondition = postcondition;
        }

        internal string Name { get => name; set => name = value; }
        internal List<Element> Preconditions { get => preconditions; set => preconditions= value; }
        internal Element Postcondition { get => postcondition; set => postcondition = value; }
    }
}
