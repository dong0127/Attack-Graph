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

        //判断是否可以攻击
        public bool Feasible(List<Element> input)
        {
            foreach (Element start in input)
            {
                if (start.Name == "user" || start.Name == "root")
                {
                    Console.WriteLine("扫描初始知识库，可以攻击");
                    return true;
                }
            }
            Console.WriteLine("扫描初始知识库，无法攻击");
            return false;
        }

        //搜索
        public void DFS(List<Element> input)
        {
            Stack<Element> stack = new Stack<Element>();
            HashSet<string> visited = new HashSet<string>();
            //List<Element> knowledges = new List<Element>(input);
            Element oneStep ,temp = null;
            //一开始是否满足权限
            if (Feasible(input) == true)
            {
                //把初始知识库全部压栈
                foreach (Element knowledge in input)
                {
                    stack.Push(knowledge);
                }
                //visited.Add("开始攻击");
                //开始搜索
                while (stack.Count != 0)
                {
                    oneStep = stack.Pop();
                    //不同类型的点采用不同的策略——————————————————————————————————
                    if (oneStep.Type == "attribute")
                    {
                        foreach (Template tp in attacks)
                        {
                            foreach (Element pre1 in tp.Preconditions)
                            {
                                if (oneStep.Name == pre1.Name)
                                {
                                    if (tp.Name == "local-bof" && oneStep.From == t)
                                    {
                                        temp = new Element(tp.Name, oneStep.From, "action");
                                        //没有被访问过的话，就压栈
                                        if (!visited.Contains(temp.ToString()))
                                        {
                                            stack.Push(temp);
                                            visited.Add(temp.ToString());
                                        }
                                    }
                                    else
                                    {
                                        foreach (Element i in input)
                                        {
                                            foreach (Element pre2 in tp.Preconditions)
                                            {
                                                if (pre2.Name == i.Name && pre2.Name != pre1.Name)
                                                {
                                                    if (oneStep.Name == "user")
                                                    {
                                                        temp = new Element(tp.Name, oneStep.From, i.To, "action");
                                                    }
                                                    else
                                                    {
                                                        temp = new Element(tp.Name, oneStep.From, oneStep.To, "action");
                                                    }
                                                    //没有被访问过的话，就压栈
                                                    if (!visited.Contains(temp.ToString()))
                                                    {
                                                        stack.Push(temp);
                                                        visited.Add(temp.ToString());
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (oneStep.Type == "action")
                    {
                        foreach (Template tt in attacks)
                        {
                            if (tt.Name == oneStep.Name)
                            {
                                foreach (Element one in tt.Postconditions)
                                {
                                    if (one.Name == "user" || one.Name == "root")
                                    {
                                        temp = new Element(one.Name, oneStep.To, "attribute");
                                    }
                                    else
                                    {
                                        temp = new Element(one.Name, oneStep.From, oneStep.To, "attribute");
                                    }
                                    //没有被访问过的话，就压栈
                                    if (!visited.Contains(temp.ToString()))
                                    {
                                        stack.Push(temp);
                                        visited.Add(temp.ToString());
                                    }
                                }
                            }
                        }
                    }
                    //—————————————————————————————————————————————
                }
                Console.WriteLine(visited.Count);
                foreach (string s in visited) { Console.WriteLine(s); }
                
            }
            else
            {
                Console.WriteLine("不满足攻击条件");
            }
        }
    }
}
