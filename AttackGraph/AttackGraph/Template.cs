
using System;

using System.Collections.Generic;

using System.Text;



namespace AttackGraph

{

    class Template

    {

        Element atom;
        List<Element> preconditions, postconditions;

        public Template(Element atom, List<Element> preconditions, List<Element> postconditions)
        {
            this.Atom = atom;
            this.Preconditions = preconditions;
            this.Postconditions = postconditions;
        }

        internal Element Atom { get => atom; set => atom = value; }
        internal List<Element> Preconditions { get => preconditions; set => preconditions = value; }
        internal List<Element> Postconditions { get => postconditions; set => postconditions = value; }
    }
}