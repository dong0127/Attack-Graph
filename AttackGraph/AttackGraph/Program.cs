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

                //new Element("ftp", "0", "2", "attribute"),

                ///new Element("ftp", "0", "1", "attribute"),

                //new Element("ftp", "1", "2", "attribute"),

                //new Element("sshd", "0", "1", "attribute"),

                //new Element("sshd", "2", "1", "attribute"),

                new Element("FATHER", "0", "1", "attribute")
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

            //-----------------------
            List<Element> pre4 = new List<Element>
            {
                new Element("F", "from", "to", "attribute")
            };
            List<Element> post4 = new List<Element>
            {
                new Element("C", "from", "to", "attribute")
            };

            Template atomAttack4 = new Template(new Element("3", "from", "to", "action"), pre4, post4);
            List<Element> pre5 = new List<Element>
            {
                new Element("C", "from", "to", "attribute"),
                new Element("D", "from", "to", "attribute")
            };
            List<Element> post5 = new List<Element>
            {
                new Element("A", "from", "to", "attribute")
            };

            Template atomAttack5 = new Template(new Element("2", "from", "to", "action"), pre5, post5);
            List<Element> pre6 = new List<Element>
            {
                new Element("A", "from", "to", "attribute"),
                new Element("B", "from", "to", "attribute")
            };
            List<Element> post6 = new List<Element>
            {
                new Element("sshd", "from", "to", "attribute")
            };

            Template atomAttack6 = new Template(new Element("8", "from", "to", "action"), pre6, post6);
            List<Element> pre7 = new List<Element>
            {
                new Element("G", "from", "to", "attribute"),
                new Element("E", "from", "to", "attribute")
            };
            List<Element> post7 = new List<Element>
            {
                new Element("D", "from", "to", "attribute")
            };

            Template atomAttack7 = new Template(new Element("4", "from", "to", "action"), pre7, post7);
            List<Element> pre8 = new List<Element>
            {
                
                new Element("H", "from", "to", "attribute")
            };
            List<Element> post8 = new List<Element>
            {
                new Element("E", "from", "to", "attribute")
            };

            Template atomAttack8 = new Template(new Element("5", "from", "to", "action"), pre8, post8);
            List<Element> pre9 = new List<Element>
            {
                new Element("A", "from", "to", "attribute"),
                new Element("E", "from", "to", "attribute")
            };
            List<Element> post9 = new List<Element>
            {
                new Element("B", "from", "to", "attribute")
            };

            Template atomAttack9 = new Template(new Element("1", "from", "to", "action"), pre9, post9);
            //给初始条件加一个父节点
            List<Element> pre10 = new List<Element>
            {
                
                new Element("FATHER", "from", "to", "attribute")
            };
            List<Element> post10 = new List<Element>
            {
                new Element("H", "from", "to", "attribute"),
                new Element("F", "from", "to", "attribute"),
                new Element("G", "from", "to", "attribute")
                
            };

            Template atomAttack10 = new Template(new Element("0", "from", "to", "action"), pre10, post10);
            //put attacks in a list 

            List<Template> atomAttacks = new List<Template>
            {
                atomAttack0,

                atomAttack1,

                atomAttack2,

                atomAttack3,
                //--------------------
                atomAttack4,
                atomAttack5,
                atomAttack6,
                atomAttack7,
                atomAttack8,
                atomAttack9,
                atomAttack10

            };

            Attack attacks = new Attack(atomAttacks,"0", "2");

            attacks.DFS(knowledges, new Element("8", "0", "1", "action"));
            //attacks.MakeOneMove(new Element("2", "0", "1", "action"), knowledges);
            //attacks.MakeOneBack(new Element("trust", "1", "0", "attribute"), knowledges);
            Console.ReadKey();

        }

    }

}