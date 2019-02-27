﻿using System;
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
                new Element("ftp", "0", "2", "attribute"),
                new Element("ftp", "0", "1", "attribute"),
                new Element("ftp", "1", "2", "attribute"),
                new Element("sshd", "0", "1", "attribute"),
                new Element("sshd", "2", "1", "attribute"),
                new Element("user", "0", "attribute")
            };
           
      
            //atomattack0
            List<Element> pre0 = new List<Element>
            {
                new Element("sshd", "from", "to", "attribute"),
                new Element("user", "from", "attribute"),
            };
           
            Element post0 = new Element("user", "to", "attribute");
            Template atomAttack0 = new Template("sshd-bof", pre0, post0);

            //atomattack1
            List<Element> pre1 = new List<Element>
            {
                new Element("ftp", "from", "to"),
                new Element("user", "to", "attribute")
            };
      
            Element post1 = new Element("trust", "from", "to", "attribute");
            Template atomAttack1 = new Template("Ftp-rhosts", pre1, post1);

            //atomattack2
            List<Element> pre2 = new List<Element>
            {
                new Element("trust", "from", "to", "attribute"),
                new Element("user", "from", "attribute"),
            };
    
            Element post2 = new Element("user", "to", "attribute");
            Template atomAttack2 = new Template("rsh", pre2, post2);

            //atomattack3
            List<Element> pre3 = new List<Element> 
            {
                new Element("user", "from", "attribute"),
            };
            Element post3 =   new Element("root", "to", "attribute");
          
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
            
            attacks.DFS(new Element("Ftp-rhosts", "0", "1", "attribute"), knowledges);
            Console.ReadKey();
        } 
    }
}
