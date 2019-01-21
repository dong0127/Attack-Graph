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

        public string From { get => from; set => from = value; }
        public string To { get => to; set => to = value; }
        public string Name { get => name; set => name = value; }
        public string Type { get => type; set => type = value; }

        public Element(string name, string from, string to, string type)
        {
            this.Name = name;
            this.From = from;
            this.To = to;
            this.Type = type;
        }

        public Element()
        {
        }

        public override string ToString()
        {
            return Name+"("+From+","+To+")";
        }
    }
}
