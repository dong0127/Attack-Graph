using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{
    class Node
    {
        string name, from, to;

        public string From { get => from; set => from = value; }
        public string To { get => to; set => to = value; }
        public string Name { get => name; set => name = value; }
        

        public Node(string name, string from, string to)
        {
            this.Name = name;
            this.From = from;
            this.To = to; 
        }
        //privilege
        public Node(string name, string from)
        {
            this.Name = name;
            this.From = from;
        }

        public Node()
        {
        }

        public override string ToString()
        {
            return Name+"("+From+","+To+")";
        }
    }
}
