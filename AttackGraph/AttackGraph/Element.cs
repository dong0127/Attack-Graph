using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{
    class ELement
    {
        string name, from, to;

        public string From { get => from; set => from = value; }
        public string To { get => to; set => to = value; }
        public string Name { get => name; set => name = value; }
        

        public ELement(string name, string from, string to)
        {
            this.Name = name;
            this.From = from;
            this.To = to; 
        }
        //privilege
        public ELement(string name, string from)
        {
            this.Name = name;
            this.From = from;
        }

        public ELement()
        {
        }

        public override string ToString()
        {
            return Name+"("+From+","+To+")";
        }
    }
}
