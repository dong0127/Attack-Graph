using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{
    class Element
    {
        string name;
        string from, to;
        bool visited;

        public string From { get => from; set => from = value; }
        public string To { get => to; set => to = value; }
        public string Name { get => name; set => name = value; }
        public bool Visited { get => visited; set => visited = value; }

        public Element(string name, string from, string to, string type)
        {
            this.Name = name;
            this.From = from;
            this.To = to;
            this.Visited = visited;
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
