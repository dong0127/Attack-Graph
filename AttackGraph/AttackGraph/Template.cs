using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{

    class Template
    {
        string name;
        HashSet<string> preconditions = new HashSet<string>();
        HashSet<string> postconditions = new HashSet<string>();

        public Template(string name, HashSet<string> preconditions, HashSet<string> postconditions)
        {
            this.name = name;
            this.preconditions = preconditions;
            this.postconditions = postconditions;
        }

        internal string Name { get => name; set => name = value; }
        internal HashSet<string> Preconditions { get => preconditions; set => preconditions= value; }
        internal HashSet<string> Postconditions { get => postconditions; set => postconditions = value; }
    }
}
