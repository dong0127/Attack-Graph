using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{
    class Attack
    {
        List<Template> attacks;
        string s, t;      //source host and target host

        public Attack(List<Template> attacks, string s, string t)
        {
            this.attacks = attacks;
            this.s = s;
            this.t = t;
        }
        //得到初始点
        public List<Element> GetInitialVertex(List<Element> input)
        {
            Element first = null;
            List<Element> initialVertex = new List<Element>();
            foreach (Element p in input)
            {
                if (p.Name == "user" || p.Name == "root")
                {
                    if (p.From == s)
                    {
                        foreach (Template t in attacks)
                        {
                            foreach (Element pre in t.Preconditions)
                            {
                                foreach (Element ser in input)
                                {
                                    if (pre.Name != p.Name && pre.Name == ser.Name)
                                    {
                                        if (ser.From == s)
                                        {
                                            first = new Element(t.Name, p.From, ser.To, "action");
                                            initialVertex.Add(first);
                                            Console.WriteLine(first);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }    
            }
            return initialVertex;
        }
        //搜索
        public void DFS(Element vertex, List<Element> input)
        {
            Element v = null;
            Stack<Element> stack = new Stack<Element>();
            HashSet<string> visited = new HashSet<string>();
            List<string> path = new List<string>();
            stack.Push(vertex);
            visited.Add(vertex.ToString());
            path.Add(vertex.ToString());
            while (stack.Count != 0)
            {
                foreach (Template parent in attacks)
                {
                    //找到父节点对应的攻击模板
                    if (stack.Peek().Name == parent.Name)
                    {
                        foreach (Template child in attacks)
                        {
                            //根据父节点的后果在模板库中找到对应的子节点
                            foreach (Element pre in child.Preconditions)
                            {
                                if (parent.Postcondition.Name == pre.Name)
                                {
                                    //判断输入知识库是否有满足一步攻击的条件
                                    foreach (Element k in input)
                                    {
                                        foreach (Element pre2 in child.Preconditions)
                                        {
                                            //判断有没有子节点
                                            if (k.Name == pre2.Name && k.Name != pre.Name)
                                            {
                                                v = new Element(child.Name, pre2.From, pre.To, "action");
                                                //判断子节点是否被访问过
                                                foreach (string vis in visited)
                                                {
                                                    if (vis != v.ToString())
                                                    {
                                                        stack.Push(v);
                                                        visited.Add(v.ToString());
                                                        path.Add(v.ToString());
                                                    }
                                                    else
                                                    {
                                                        stack.Pop();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        
            Console.WriteLine(visited.Count);
            Console.WriteLine(path);
            foreach (string s in path)
            {
                Console.WriteLine(s);
            }

        }
    }
}
