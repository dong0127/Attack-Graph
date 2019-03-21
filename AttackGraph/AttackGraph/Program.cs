using System;
using System.Collections.Generic;

namespace AttackGraph
{
    class Program
    {
        static void Main(string[] args)
        {
            ////serivce of net
            //list<string> serivce1 = new list<string>
            //{
            //    "ftp",
            //    "sshd"
            //};
            //network host1 = new network("1", serivce1);
            //list<string> serivce2 = new list<string>
            //{
            //    "ftp",
            //};
            //network host2 = new network("2", serivce2);

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
            HashSet<string> pre0 = new HashSet<string>{ "sshd","user" };
            HashSet<string> post0 = new HashSet<string> { "user"};
            
            Template atomAttack0 = new Template("sshd-bof", pre0, post0);

            //atomattack1
            HashSet<string> pre1 = new HashSet<string>{"ftp","user"};
            HashSet<string> post1 = new HashSet<string> { "trust"};
            
            Template atomAttack1 = new Template("Ftp-rhosts", pre1, post1);

            //atomattack2
            HashSet<string> pre2 = new HashSet<string>{"trust", "user" };
            HashSet<string> post2 = new HashSet<string> {"user" };
            
            Template atomAttack2 = new Template("rsh", pre2, post2);

            //atomattack3
            HashSet<string> pre3 = new HashSet<string>{"user"};
            HashSet<string> post3 = new HashSet<string> { "root"};
          
            Template atomAttack3 = new Template("local-bof", pre3, post3);
            //put attacks in a list 
            List<Template> atomAttacks = new List<Template>
            {
                atomAttack0,
                atomAttack1,
                atomAttack2,
                atomAttack3,
            };
            Attack attacks = new Attack(atomAttacks, "0","2");
            //attacks.GetInitialVertex(knowledges);
            attacks.DFS(knowledges);
            Console.ReadKey();
        } 
    }
}
