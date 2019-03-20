using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{
    class Element
    {
        string name, from, to, type;

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
        //privilege
        public Element(string name, string from, string type)
        {
            this.Name = name;
            this.From = from;
            this.Type = type;
        }

        public Element()
        {
        }

        public override string ToString()
        {
            if (Name == "user" || Name == "root")
            {
                return Name + "(" + From + ")";
            }
            else if (Name == "trust")
            {
                return Name + "(" + To + "," + From + ")";
            }
            else if (Name == "local-bof")
            {
                return Name + "(" + From + ")";
            }
            return Name+"("+From+","+To+")";
        }
    }
}
