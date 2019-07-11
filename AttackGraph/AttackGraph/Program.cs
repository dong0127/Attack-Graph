using System;

using System.Collections.Generic;
using System.Data;

namespace AttackGraph
{
    class Program
    {
        static void Main(string[] args)
        {
            // input
            Dictionary<string, string> privilege = new Dictionary<string, string>
            {
                { "0", "user" }                
            };

            Dictionary<string, HashSet<string>> term = new Dictionary<string, HashSet<string>>
            {
                { "1", new HashSet<string>{ "ftp", "sshd" } },
                { "2", new HashSet<string>{ "ftp" } },
                { "3", new HashSet<string>{ "ftp", "sshd", "a3","a8","a6","a7","a5" } },
                { "4", new HashSet<string>{ "ftp", "x","y","z"} },
                { "5", new HashSet<string>{ "ftp", "sshd" } },
                { "6", new HashSet<string>{ "ftp" } },
                { "7", new HashSet<string>{ "ftp" } }
            };

            Dictionary<string, HashSet<string>> network = new Dictionary<string, HashSet<string>>
            {
                { "0", new HashSet<string>{ "1", "2" } },
                { "1", new HashSet<string>{ "0", "2","4" } },
                { "2", new HashSet<string>{ "0","1","3" } },
                { "3", new HashSet<string>{  "2","4", "6"} },
                { "4", new HashSet<string>{ "1", "3","5" } },
                { "5", new HashSet<string>{ "4", "6" } },
                { "6", new HashSet<string>{ "3", "5","7" } },
                { "7", new HashSet<string>{ "6" } }
            };
            List<Element> knowledges = new List<Element>           
            {

                new Element("user", "0", "0", "attribute"),

                new Element("ftp", "0", "2", "attribute"),

                new Element("ftp", "0", "1", "attribute"),

                new Element("ftp", "1", "2", "attribute"),

                new Element("sshd", "0", "1", "attribute"),

                new Element("sshd", "2", "1", "attribute"),

                //new Element("FATHER", "0", "1", "attribute")
            };

            //atomattack0
            List<Element> pre0 = new List<Element>
            {
                new Element("sshd", "from", "to", "attribute"),
                new Element("user", "from", "from", "attribute"),
                //new Element("network", "from", "to", "attribute")
            };
            List<Element> post0 = new List<Element>
            {

                new Element("user", "to", "to", "attribute")
            };

            Template atomAttack0 = new Template(new Element("sshd-bof", "from", "to", "action"), pre0, post0);



            //atomattack1

            List<Element> pre1 = new List<Element>
            {
                new Element("ftp", "from", "to", "attribute"),
                new Element("user", "from", "from", "attribute")
            };
            List<Element> post1 = new List<Element>
            {

                new Element("trust", "to", "from", "attribute")
            };

            Template atomAttack1 = new Template(new Element("Ftp-rhosts", "from", "to", "action"), pre1, post1);

            //atomattack2


            List<Element> pre2 = new List<Element>
            {
                new Element("trust", "to", "from", "attribute"),
                new Element("user", "from", "from", "attribute")
            };
            List<Element> post2 = new List<Element>
            {

                new Element("user", "to", "to", "attribute")
            };

            Template atomAttack2 = new Template(new Element("rsh", "from", "to", "action"), pre2, post2);

            //atomattack3


            List<Element> pre3 = new List<Element>
            {
                new Element("x", "to", "from", "attribute"),
                new Element("y", "to", "from", "attribute"),
                new Element("z", "to", "from", "attribute"),
                new Element("user", "from", "from", "attribute")
            };
            List<Element> post3 = new List<Element>
            {

                new Element("user", "to", "to", "attribute")
            };

            Template atomAttack3 = new Template(new Element("rsh", "from", "to", "action"), pre3, post3);
            //atomattack3

            //List<Element> pre3 = new List<Element>
            //{
            //    new Element("user", "from", "from", "attribute")
            //};
            //List<Element> post3 = new List<Element>
            //{
            //    new Element("root", "from", "from", "attribute")
            //};

            //Template atomAttack3 = new Template(new Element("local-bof", "from", "from", "action"), pre3, post3);

            //-----------------------
            List<Element> pre4 = new List<Element>
            {
                new Element("a1", "from", "to", "attribute"),
                new Element("a2", "from", "to", "attribute")
            };
            List<Element> post4 = new List<Element>
            {
                new Element("a0", "from", "to", "attribute")
            };

            Template atomAttack4 = new Template(new Element("A", "from", "to", "action"), pre4, post4);

            List<Element> pre5 = new List<Element>
            {
                new Element("a8", "from", "to", "attribute")
            };
            List<Element> post5 = new List<Element>
            {
                new Element("a3", "from", "to", "attribute"),
                new Element("a1", "from", "to", "attribute")
            };

            Template atomAttack5 = new Template(new Element("B", "from", "to", "action"), pre5, post5);

            List<Element> pre6 = new List<Element>
            {

                new Element("a6", "from", "to", "attribute")
            };
            List<Element> post6 = new List<Element>
            {
                new Element("a2", "from", "to", "attribute")
            };

            Template atomAttack6 = new Template(new Element("E", "from", "to", "action"), pre6, post6);
            List<Element> pre7 = new List<Element>
            {
                new Element("a3", "from", "to", "attribute"),
                new Element("a4", "from", "to", "attribute")
            };
            List<Element> post7 = new List<Element>
            {
                new Element("a2", "from", "to", "attribute")
            };

            Template atomAttack7 = new Template(new Element("C", "from", "to", "action"), pre7, post7);
            List<Element> pre8 = new List<Element>
            {

                new Element("a5", "from", "to", "attribute")
            };
            List<Element> post8 = new List<Element>
            {
                new Element("a2", "from", "to", "attribute")
            };

            Template atomAttack8 = new Template(new Element("D", "from", "to", "action"), pre8, post8);
            List<Element> pre9 = new List<Element>
            {

                new Element("a7", "from", "to", "attribute")
            };
            List<Element> post9 = new List<Element>
            {
                new Element("a4", "from", "to", "attribute")
            };

            Template atomAttack9 = new Template(new Element("F", "from", "to", "action"), pre9, post9);

            //put attacks in a list 

            List<Template> atomAttacks = new List<Template>
            {
                atomAttack0,
                atomAttack1,
                atomAttack2,
                //--------------------
                atomAttack4,
                atomAttack5,
                atomAttack6,
                atomAttack7,
                atomAttack8,
                atomAttack9,

            };

            Attack attacks = new Attack(atomAttacks, privilege, term, network, "0", "2");
            attacks.FindPath(new Element("a0","6","3","attribute"));

            Console.ReadKey();

        }

    }

}