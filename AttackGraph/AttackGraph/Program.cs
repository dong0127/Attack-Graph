using System;
using System.Collections.Generic;

namespace AttackGraph
{
    class Program
    {
       
        
        static void Main(string[] args)
        {

            List<Element> initial = new List<Element>();
            List<Element> pre = new List<Element>();
            List<Element> eff = new List<Element>();
            pre.Add(new Element("ftp", "x", "y"));
            pre.Add(new Element("user", "x", "x"));
            eff.Add(new Element("trust", "y", "x"));
            AtomAttack atomAttack = new AtomAttack(pre,eff,"Ftp-rhosts");
            Attack attack = new Attack();
            attack.IsAttack();
            Console.WriteLine(attack.Source);
            
            Console.ReadKey();
        }
    }
}
