using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{
    class Attacks
    {
        List<AtomAttackTemplate> attack;
        string s, t;      //source host and target host
        
        List<Element> updatePrivilege = new List<Element>();
        List<Element> updateOther = new List<Element>();
        Element oneStepAttack, oneAttackEffect;
        
        List<Element> attacks = new List<Element>();

        public Attacks(List<AtomAttackTemplate> attack, string s, string t)
        {
            this.attack = attack;
            this.s = s;
            this.t = t;
        }
        public Element MakeOneMove(Element vertex, List<Element> input)
        {
            List<Element> privilege = new List<Element>();
            List<Element> service = new List<Element>();

            foreach (Element element in input)
            {
                if (element.Type == "privilege")
                    privilege.Add(element);
                else if (element.Type == "other")
                    service.Add(element);
            }
        
            foreach (AtomAttackTemplate atom in attack)
            {
                if (vertex.Type == "attack")
                {
                    foreach (Element effect in atom.Effects)
                    {
                        oneAttackEffect = new Element(effect.Name, vertex.To, vertex.To, "privilege");
                        return oneAttackEffect;
                    }
                }
                else if (vertex.Type == "privilege")
                {
                    oneStepAttack = new Element(atom.AttackName, vertex.From, "", "attack");
                    foreach (Element pre in atom.Preconditions)
                    {
                        foreach (Element ser in service)
                        {
                            if (pre.Name == ser.Name)
                                oneStepAttack = new Element(atom.AttackName, vertex.From, ser.To, "attack");
                            Console.WriteLine(oneAttackEffect.Name);
                            return oneStepAttack;
                        }
                    }
                }
                else if (vertex.Type == "other")
                {
                    foreach (Element pri in privilege)
                    {
                        if(pri.Name == vertex.Name && pri.From == vertex.From)
                            oneStepAttack = new Element(atom.AttackName, vertex.From, vertex.To, "attack");
                        Console.WriteLine(oneAttackEffect.Name);
                        return oneStepAttack;
                    } 
                }
            }
            return null;
        }
        /*
        public List<Element> GetElements(List<Element> privileges, List<Element> others)
        {
            foreach (Element other in others)
            {
                //Traversal attacks list
                foreach (AtomAttackTemplate atom in Attack)
                {
                    //Traversal precontions list
                    foreach (Element elements in atom.Preconditions)
                    {
                        if (other.Name== elements.Name)
                        {
                            foreach (Element privilege in privileges)
                            {
                                if (privilege.Name == "user" && privilege.From == other.From)
                                {
                                    //get one step attack
                                    oneStepAttack = new Element(atom.AttackName, other.From, other.To, "attack");
                                    attacks.Add(oneStepAttack);
                                    //get the effects
                                    foreach (Element ele in atom.Effects)
                                    {
                                        if (ele.Type == "privilege")
                                        {
                                            oneAttackEffect = new Element(ele.Name, oneStepAttack.To, oneStepAttack.To, ele.Type);
                                            updatePrivilege.RemoveAt(0);
                                            updatePrivilege.Add(oneAttackEffect);
                                            
                                        }
                                        else if (ele.Type == "other")
                                        {
                                            oneAttackEffect = new Element(ele.Name, oneStepAttack.From, oneStepAttack.To, ele.Type);
                                            updateOther.Add(oneAttackEffect);
                                        }
                                      
                                    }

                                    Console.WriteLine(oneStepAttack.ToString());
                                    Console.WriteLine(oneAttackEffect.ToString());
                                }
                            }
                        }
                    }
                }
            }
            privileges = updatePrivilege;
            others=updateOther;
            foreach (Element e in privileges)
            {
                if (e.Name == "root" && e.From == t && e.To == t)
                {
                    return attacks;
                }
            }
           
            return GetElements(privileges, others);
        }
       */
        public List<AtomAttackTemplate> Attack { get => attack; set => attack = value; }
    }
}
