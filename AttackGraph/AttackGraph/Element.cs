using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{
    class Element
    {
        string name, from, to, attribute;

        public string From { get => from; set => from = value; }
        public string To { get => to; set => to = value; }
        public string Name { get => name; set => name = value; }
        public string Attribute { get => attribute; set => attribute = value; }

        public Element(string name, string from, string to, string attribute)
        {
            this.Name = name;
            this.From = from;
            this.To = to;
            this.Attribute = attribute;
        }
        //privilege
        public Element(string name, string from, string attribute)
        {
            this.Name = name;
            this.From = from;
            this.Attribute = attribute;
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
