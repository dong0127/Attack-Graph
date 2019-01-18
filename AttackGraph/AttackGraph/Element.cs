using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{
    class Element
    {
        string name;
        string from, to;
        string type;

        public Element(string name, string from, string to, string type) : this(name, from, to)
        {
            this.type = type;
        }

        public Element(string name, string from, string to)
        {
            this.name = name;
            this.from = from;
            this.to = to;
            
        }

        public string getId()
        {
            return null;
        }
    }
}
