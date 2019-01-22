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
                new Element("user", "0", "0", "privilege"),                
                new Element("ftp", "0", "2", "other"),
                new Element("ftp", "1", "2", "other"),
                new Element("sshd", "0", "1", "other"),
                new Element("sshd", "2", "1", "other"),
            };
      
            //atomattack0
            List<Element> preconditions0 = new List<Element>();
            List<Element> effects0 = new List<Element>();
            preconditions0.Add(new Element("sshd", "x", "y", "attack"));
            preconditions0.Add(new Element("user", "x", "x", "privilege"));
            effects0.Add(new Element("user", "y", "y", "privilege"));
            AtomAttackTemplate atomAttack0 = new AtomAttackTemplate(preconditions0, effects0, "sshd-bof");

            //atomattack1
            List<Element> preconditions1 = new List<Element>();
            List<Element> effects1 = new List<Element>();
            preconditions1.Add(new Element("ftp", "x", "y", "attack"));
            preconditions1.Add(new Element("user", "x", "x", "privilege"));
            effects1.Add(new Element("user", "y", "y", "privilege"));
            AtomAttackTemplate atomAttack1 = new AtomAttackTemplate(preconditions1, effects1, "Union");

            //atomattack2
            List<Element> preconditions2 = new List<Element>();
            List<Element> effects2 = new List<Element>();
            preconditions2.Add(new Element("user", "x", "x", "privilege"));
            effects2.Add(new Element("root", "x", "x", "privilege"));
            AtomAttackTemplate atomAttack2 = new AtomAttackTemplate(preconditions2, effects2, "local-bof");

            //put attacks in a list 
            List<AtomAttackTemplate> atomAttacks = new List<AtomAttackTemplate>
            {
                atomAttack0,
                atomAttack1
                
            };
            Attacks attacks = new Attacks(atomAttacks, "0","2");
           

            Console.ReadKey();
        } 
    }
}
