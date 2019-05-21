using System;
using System.Collections.Generic;
using System.Text;
//knowledges只存放属性节点

namespace AttackGraph
{
    class Attack
    {
        List<Template> attacks;
        List<Element> knowledges;
        string s, t;      //source host and target host

        public Attack(List<Template> attacks, List<Element> knowledges, string s, string t)
        {

            this.attacks = attacks;
            this.knowledges = knowledges;
            this.s = s;

            this.t = t;

        }

        //判断是否可以攻击
        public bool Feasible()
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



        public List<Element> MakeOneMove(Element current)
        {

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

            //foreach (Element result in results)

            //{

            //    Console.WriteLine(result);

            //}

            return results;

        }



        public List<Element> MakeOneBack(Element current)
        {

            List<Element> re_results = new List<Element>();

            Element temp = null;

            string to = "";

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

                        foreach (Element pre in tp.Preconditions)

                        {

                            if (pre.From == "from")

                            {

                                if (pre.To == "from")

                                {

                                    temp = new Element(pre.Name, from, from, "attribute");
                                }

                                else if (pre.To == "to")
                                {

                                    temp = new Element(pre.Name, from, to, "attribute");
                                }

                            }



                            else if (pre.From == "to")

                            {

                                if (pre.To == "from")

                                {

                                    temp = new Element(pre.Name, to, from, "attribute");

                                }

                                else if (pre.To == "to")

                                {
                                    temp = new Element(pre.Name, to, to, "attribute");

                                }

                            }

                            foreach (Element one in knowledges)

                            {

                                if (one.ToString() == temp.ToString())

                                {

                                    re_results.Add(temp);

                                }

                            }

                        }

                    }

                }

            }

            else if (current.Type == "attribute")

            {

                

                foreach (Template tp in attacks)

                {

                    foreach (Element post in tp.Postconditions)

                    {

                        //每个攻击模板的前置条件与current对比

                        if (current.Name == post.Name)

                        {

                            if (post.From == "from")

                            {

                                from = current.From;

                            }

                            else if (post.To == "from")

                            {

                                from = current.To;

                            }

                            if (post.From == "to")

                            {

                                to = current.From;

                            }

                            else if (post.To == "to")

                            {

                                to = current.To;

                            }

                            if (post.From == post.To)//只能确定攻击的to

                            {

                                foreach (Element knowledge in knowledges)

                                {

                                    foreach (Element pre1 in tp.Preconditions)

                                    {

                                        if (pre1.Name == knowledge.Name && pre1.Name != current.Name)

                                        {

                                            string from2 = "";

                                            string to2 = "";

                                            if (pre1.From == "from")

                                            {

                                                from2 = knowledge.From;

                                            }

                                            else if (pre1.To == "from")

                                            {

                                                from2 = knowledge.To;

                                            }

                                            if (pre1.From == "to")

                                            {

                                                to2 = knowledge.From;

                                            }

                                            else if (pre1.To == "to")

                                            {

                                                to2 = knowledge.To;

                                            }

                                            if (to == to2)

                                            {

                                                temp = new Element(tp.Atom.Name, from2, to, "action");

                                            }

                                        }

                                        foreach (Element one in knowledges)

                                        {

                                            if (one.ToString() == temp.ToString())

                                            {

                                                re_results.Add(temp);

                                            }

                                        }

                                    }

                                }

                            }

                            else//根据后果就可以确定攻击的from和to

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

                                foreach (Element one in knowledges)

                                {

                                    if (one.ToString() == temp.ToString())

                                    {

                                        re_results.Add(temp);

                                    }

                                }  

                            }

                        }

                    }

                }

            }



            //foreach (Element result in re_results)

            //{

            //    Console.WriteLine(result);

            //}

            return re_results;

        }

        public void DFS(Element destination)

        {
            List<Element> trace = new List<Element>();//存放经过的点

            Stack<int> depthStack = new Stack<int>();//当前搜索深度

            List<int> depthList = new List<int>();//经过的深度

            Stack<Element> working = new Stack<Element>();

            HashSet<string> visited = new HashSet<string>();

            List<Element> results = new List<Element>();

            Element current = null;

            //一开始是否满足权限

           if (Feasible() == true)
            {

                //把初始知识库全部压栈

                foreach (Element knowledge in knowledges)
                {

                    working.Push(knowledge);

                    visited.Add(knowledge.ToString());

                    depthStack.Push(0);

                }

                while (working.Count != 0)
                {

                    current = working.Pop();

                    int depth = depthStack.Pop();

                    //回溯的过程

                    if (depth>0)
                    {

                        while (depthList[depthList.Count - 1] >= depth)
                        {

                            int lastIndex = depthList.Count - 1;

                            depthList.RemoveAt(lastIndex);

                            trace.RemoveAt(lastIndex);

                        }

                    }
                    //一个深度有一个点，trace和depthList长度应该一样

                    trace.Add(current);

                    depthList.Add(depth);

                    results = MakeOneMove(current);

                    if (current.ToString() == destination.ToString())
                    {

                        Console.WriteLine("路径");

                        foreach (Element step in trace)
                        {

                            Console.WriteLine(step.ToString());

                        }

                        Console.WriteLine("深度");

                        foreach (int no in depthList)
                        {

                            Console.WriteLine(no);

                        }

                        Console.WriteLine("访问过的点");

                        foreach (string vertex in visited)
                        {

                            Console.WriteLine(vertex);

                        }

                        Console.WriteLine("数量");

                        Console.WriteLine(visited.Count);

                        //补全路径

                        List<Element> re_trace = new List<Element>();

                        List<Element> re_results = new List<Element>();

                        List<Element> finalResults = new List<Element>(trace);//最终结果

                        Stack<Element> re_working = new Stack<Element>();//补全路径用

                        HashSet<string> path = new HashSet<string>();

                        foreach (Element e in trace)
                        {

                            path.Add(e.ToString());

                        }

                      

                        foreach (Element tr in trace)
                        {

                            if (tr.Type == "action")
                            {

                                re_working.Push(tr);
                                int k = trace.IndexOf(tr);
                                while (re_working.Count != 0)
                                {

                                    current = re_working.Pop();

                                    re_results = MakeOneBack(current);

                                    foreach (Element re in re_results)
                                    {

                                        if (visited.Contains(re.ToString()) && !path.Contains(re.ToString()))
                                        {

                                            re_working.Push(re);

                                            path.Add(re.ToString());

                                            re_trace.Add(re);

                                        }

                                    }

                                }

                                if (re_trace.Count != 0)
                                {
                                    foreach (Element re in re_trace)
                                    {
                                        finalResults.Insert(k, re);
                                        Console.WriteLine(re.ToString());

                                    }

                                    Console.WriteLine("由" + current.ToString() + "反补");

                                    re_trace.Clear();

                                    re_results.Clear();

                                    Console.WriteLine("-------");

                                }

                            }

                        }
                        foreach (Element element in finalResults)
                        {
                            if (element.Type == "action")
                            {
                                Console.WriteLine(element.ToString());
                            }

                        }
                        return;

                    }
                    //入栈

                    foreach (Element result in results)
                    {

                        if (!visited.Contains(result.ToString()))
                        {

                            knowledges.Add(result);

                            visited.Add(result.ToString());

                            working.Push(result);

                            depthStack.Push(depth + 1);

                        }

                    }

                }

                Console.WriteLine("访问过的点");

                foreach (string vertex in visited)

                {

                    Console.WriteLine(vertex);

                }

                Console.WriteLine("点的数量");

                Console.WriteLine(visited.Count);  

            }

            else

            {

                Console.WriteLine("不满足攻击条件");

            }

        }

    }

}