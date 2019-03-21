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
                else if (start.Name == "root" && start.To == t)
                {
                    Console.WriteLine("已经达到目的了");
                    return false;
                }
                
            }
            Console.WriteLine("扫描初始知识库，无法攻击");
            return false;
        }

        public void DFS(List<Element> input)
        {
            Stack<Element> stack = new Stack<Element>();
            HashSet<string> visited = new HashSet<string>();
            HashSet<string> countPre = new HashSet<string>();//记录满足条件的
            Element oneStep ,temp = null;
            //一开始是否满足权限
            if (Feasible(input) == true)
            {
                //把初始知识库全部压栈
                foreach (Element knowledge in input)
                {
                    stack.Push(knowledge);
                    visited.Add(knowledge.ToString());
                }

                while (stack.Count != 0)
                {
                    oneStep = stack.Pop();
                    
                    //不同类型的点采用不同的策略
                    if (oneStep.Type == "attribute")
                    {  
                        foreach (Template tp in attacks)
                        {
                            if (tp.Preconditions.Contains(oneStep.Name))
                            {
                                foreach (string pre in tp.Preconditions)
                                {
                                    foreach (Element vis in input)
                                    {
                                        countPre.Add(oneStep.Name);
                                        if (vis.Name == pre && pre != oneStep.Name)
                                        {
                                            countPre.Add(vis.Name);
                                        }
                                        //如果满足所有前置条件,再检查主机号
                                        if (countPre.Count== tp.Preconditions.Count)
                                        {
                                            string to = "";
                                            string from = oneStep.From;
                                            if (oneStep.Name == "user") { to = from; }
                                            else { to = oneStep.To; }
                                            temp = new Element(tp.Name, from, to, "action");
                                            countPre.Clear();
                                            if (!visited.Contains(temp.ToString()))
                                            {
                                                if (temp.Name == "local-bof" && temp.From != t)
                                                    continue;
                                                else if (temp.Name != "local-bof" && temp.From == temp.To)
                                                    continue;
                                                else
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
                    else if (oneStep.Type == "action")
                    {
                        foreach (Template tp in attacks)
                        {
                            if (tp.Name == oneStep.Name)
                            {
                                foreach (string one in tp.Postconditions)
                                {
                                    temp = new Element(one, oneStep.From, oneStep.To, "attribute");

                                    //没有被访问过的话，就压栈
                                    if (!visited.Contains(temp.ToString()))
                                    {
                                        stack.Push(temp);
                                        visited.Add(temp.ToString());
                                        input.Add(temp);
                                    }
                                }
                            }
                        }
                    }
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
