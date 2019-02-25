using System;
using System.Collections.Generic;

namespace AttackGraph
{
    class Program
    {
        static void Main(string[] args)
        {
            // input
            List<Node> knowledgesOfSer = new List<Node>
            {         
                new Node("ftp", "0", "2"),
                new Node("ftp", "1", "2"),
                new Node("sshd", "0", "1"),
                new Node("sshd", "2", "1"),
            };
            List<Node> knowledgesOfPri = new List<Node>
            {
                new Node("user", "0", "0"),
            };
            //atomattack0
            List<Node> nodes0 = new List<Node>
            {
                new Node("sshd", "from", "to"),
                new Node("user", "from"),
                new Node("user", "to")
            };
            AtomAttackTemplate atomAttack0 = new AtomAttackTemplate(nodes0, new Node("sshd-bof", "from", "to"));

            //atomattack1
            List<Node> nodes1 = new List<Node>
            {
                new Node("ftp", "from", "to"),
                new Node("trust", "from", "to"),
                new Node("user", "to")
            };
            AtomAttackTemplate atomAttack1 = new AtomAttackTemplate(nodes1, new Node("Ftp-rhosts", "from", "to"));

            //atomattack2
            List<Node> nodes2 = new List<Node>
            {
                new Node("trust", "from", "to"),
                new Node("user", "from"),
                new Node("user", "to")
            };
           
            AtomAttackTemplate atomAttack2 = new AtomAttackTemplate(nodes2, new Node("rsh", "from", "to"));

            //atomattack3
            List<Node> nodes3 = new List<Node>
            {
                new Node("user", "from"),
                new Node("root", "to")
            };
            AtomAttackTemplate atomAttack3 = new AtomAttackTemplate(nodes2, new Node("local-bof", "from", "to"));
            //put attacks in a list 
            List<AtomAttackTemplate> atomAttacks = new List<AtomAttackTemplate>
            {
                atomAttack0,
                atomAttack1,
                atomAttack2,
                atomAttack3,
            };
            Attacks attacks = new Attacks(atomAttacks, "0","2");
           

            Console.ReadKey();
        } 
    }
}
