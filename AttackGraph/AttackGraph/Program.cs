using System;

using System.Collections.Generic;



namespace AttackGraph

{

    class Program

    {

        static void Main(string[] args)

        {
            // input

            List<Element> knowledges = new List<Element>

            {

                new Element("user", "0", "0", "attribute"),

                new Element("ftp", "0", "2", "attribute"),

                new Element("ftp", "0", "1", "attribute"),

                new Element("ftp", "1", "2", "attribute"),

                new Element("sshd", "0", "1", "attribute"),

                new Element("sshd", "2", "1", "attribute")
            };



            //atomattack0
            List<Element> pre0 = new List<Element>
            {
                new Element("sshd", "from", "to", "attribute"),
                new Element("user", "from", "from", "attribute")
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
                new Element("user", "from", "from", "attribute")
            };
            List<Element> post3 = new List<Element>
            {
                new Element("root", "from", "from", "attribute")
            };

            Template atomAttack3 = new Template(new Element("local-bof", "from", "from", "action"), pre3, post3);
            //put attacks in a list 

            List<Template> atomAttacks = new List<Template>

            {

                atomAttack0,

                atomAttack1,

                atomAttack2,

                atomAttack3,

            };

            Attack attacks = new Attack(atomAttacks, "0", "2");

            attacks.DFS(knowledges, new Element("root", "2", "2", "attribute"));
            //attacks.MakeOneMove(new Element("user", "2", "2", "attribute"), knowledges);

            Console.ReadKey();

        }

    }

}