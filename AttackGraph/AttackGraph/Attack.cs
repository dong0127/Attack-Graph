using System;

using System.Collections.Generic;

using System.Text;


//knowledges只存放属性节点
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

        //判断是否可以攻击//

        public bool Feasible(List<Element> knowledges)

        {

            foreach (Element start in knowledges)

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

        public List<Element> MakeOneMove(Element current, List<Element> knowledges)
        {
            //HashSet<string> countPre = new HashSet<string>();//利用hashset的性质来判断是否有足够的条件
            List<Element> results = new List<Element>();
            Element temp = null;
            string to="";
            string from = "";
            if (current.Type == "action")
            {
                foreach (Template tp in attacks)
                {
                    if (tp.Atom.Name == current.Name)
                    {
                        if (tp.Atom.From == "from")
                        {
                            from = current.From;
                        }
                        else if (tp.Atom.To == "from")
                        {
                            from = current.To;
                        }
                        if (tp.Atom.From == "to")
                        {
                            to = current.From;
                        }
                        else if (tp.Atom.To == "to")
                        {
                            to = current.To;
                        }
                        foreach (Element post in tp.Postconditions)
                        {
                            if (post.From == "from")
                            {
                                if (post.To == "from")
                                {
                                    temp = new Element(post.Name, from, from, "attribute");
                               
                                }
                                else if (post.To == "to")
                                {
                                    temp = new Element(post.Name, from, to, "attribute");
                                   
                                }
                            }
                                
                            else if (post.From == "to")
                            {
                                if (post.To == "from")
                                {
                                    temp = new Element(post.Name, to, from, "attribute");
                                }
                                else if (post.To == "to")
                                {
                                    temp = new Element(post.Name, to, to, "attribute");
                                }
                            }
                            if (temp.Name == "root" && temp.From != t)
                                continue;
                            else
                                results.Add(temp);
                                knowledges.Add(temp);

                        }
                    }
                }
            }
            else if (current.Type == "attribute")
            {
                foreach (Template tp in attacks)
                {
                    foreach (Element pre1 in tp.Preconditions)
                    {
                        //每个攻击模板的前置条件与current对比
                        if (current.Name == pre1.Name)
                        {
                            if (pre1.From == "from")
                            {
                                from = current.From;
                            }
                            else if (pre1.To == "from")
                            {
                                from = current.To;
                            }
                            if (pre1.From == "to")
                            {
                                to = current.From;
                            }
                            else if (pre1.To == "to")
                            {
                                to = current.To;
                            }
                            //只有一个前置条件
                            if (tp.Preconditions.Count == 1)
                            {
                                if (tp.Atom.From == "from")
                                {
                                    if (tp.Atom.To == "from")
                                    {
                                        temp = new Element(tp.Atom.Name, from, from, "action");
                                    }
                                    else if (tp.Atom.To == "to")
                                    {
                                        temp = new Element(tp.Atom.Name, from, to, "action");
                                    }
                                }

                                else if (tp.Atom.From == "to")
                                {
                                    if (tp.Atom.To == "from")
                                    {
                                        temp = new Element(tp.Atom.Name, to, from, "action");
                                    }
                                    else if (tp.Atom.To == "to")
                                    {
                                        temp = new Element(tp.Atom.Name, to, to, "action");
                                    }
                                }
                                if (temp.Name == "local-bof" && temp.From != t)
                                    continue;
                                else
                                    results.Add(temp);

                            }
                            //不只有一个前置条件的情况
                            else
                            {
                                foreach (Element knowledge in knowledges)
                                {
                                    foreach (Element pre2 in tp.Preconditions)
                                    {
                                        if (pre2.Name == knowledge.Name && pre2.Name != current.Name)
                                        {
                                            string from2 = "";
                                            string to2 = "";
                                            if (pre2.From == "from")
                                            {
                                                from2 = knowledge.From;
                                            }
                                            else if (pre2.To == "from")
                                            {
                                                from2 = knowledge.To;
                                            }
                                            if (pre2.From == "to")
                                            {
                                                to2 = knowledge.From;
                                            }
                                            else if (pre2.To == "to")
                                            {
                                                to2 = knowledge.To;
                                            }
                                            if (from == from2)
                                            {
                                                if (current.From == current.To)//pre1为权限条件
                                                {
                                                    temp = new Element(tp.Atom.Name, from, to2, "action");
                                                }
                                                else
                                                {
                                                    temp = new Element(tp.Atom.Name, from, to, "action");
                                                }
                                                results.Add(temp);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
            //    foreach (Element result in results)
            //    {
            //        Console.WriteLine(result);
            //    }
            //}
            return results;
        }

        public void DFS(List<Element> knowledges, Element destination)
        {
            //List<Element> trace = new List<Element>();//存放经过的点
            //Stack<int> depthStack = new Stack<int>();//当前搜索深度
            //List<int> depthList = new List<int>();//经过的深度
            Stack<Element> working = new Stack<Element>();

            HashSet<string> visited = new HashSet<string>();

            List<Element> results = new List<Element>();

            Element current = null;
            //int depth =0;
            //一开始是否满足权限
            //depthStack.Push(0);
            if (Feasible(knowledges) == true)
            {
                //把初始知识库全部压栈

                foreach (Element knowledge in knowledges)
                {

                    working.Push(knowledge);

                    visited.Add(knowledge.ToString());

                }

                while (working.Count != 0)
                {
                    current = working.Pop();
                    results = MakeOneMove(current, knowledges);
                    foreach (Element result in results)
                    {
                        if (!visited.Contains(result.ToString()))
                        {
                            visited.Add(result.ToString());
                            working.Push(result);
                        }
                    }
                }
                foreach (string vertex in visited)
                {
                    Console.WriteLine(vertex);
                }
                Console.WriteLine(visited.Count);

            }

            else
            {
                Console.WriteLine("不满足攻击条件");
            }

        }

    }

}