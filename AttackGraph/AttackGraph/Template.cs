using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{

    class Template
    {
        string name;
        List<Element> preconditions = new List<Element>();
        List<Element> postconditions = new List<Element>();

        public Template(string name, List<Element> preconditions, List<Element> postconditions)
        {
            this.name = name;
            this.preconditions = preconditions;
            this.postconditions = postconditions;
        }

        internal string Name { get => name; set => name = value; }
        internal List<Element> Preconditions { get => preconditions; set => preconditions= value; }
        internal List<Element> Postconditions { get => postconditions; set => postconditions = value; }
    }
}
