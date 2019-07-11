using System;

using System.Collections.Generic;

using System.Text;
using System.Linq;

//knowledges只存放属性节点
namespace AttackGraph
{
    class Attack
    {      
        List<Template> attacks;
        Dictionary<string, string> privilege;
        Dictionary<string, HashSet<string>> term;
        Dictionary<string, HashSet<string>> network;
        string s, t;      //source host and target host

        public Attack(List<Template> attacks, Dictionary<string, string> privilege, Dictionary<string, HashSet<string>> term, Dictionary<string, HashSet<string>> network, string s, string t)
        {
            this.attacks = attacks;
            this.privilege = privilege;
            this.term = term;
            this.network = network;
            this.s = s;
            this.t = t;
        }

        public List<Element> FindPath(Element target)
        {
            HashSet<string> path = new HashSet<string>();//路径
            List<Element> trace = new List<Element>();//路径

            Stack<int> depthStack = new Stack<int>();//当前搜索深度
            List<int> depthList = new List<int>();//经过的深度
            Stack<Element> working = new Stack<Element>();//工作栈
            HashSet<string> visited = new HashSet<string>();
            Dictionary<string,int> visited_depth = new Dictionary<string,int>();//key是每一个点，value是对应的深度

            List<Element> results = new List<Element>();
            Element temp = null; 
            Element current = null;

            //term和network生成初始条件
            Console.WriteLine("网络的初始条件");
            //为了满足回溯的过程，给trace一个唯一的起始点。
            trace.Add(new Element("start", "", "", ""));
            depthList.Add(0);
            depthStack.Push(0);
            //List<Element> temp_value = new List<Element>();
            foreach (KeyValuePair<string, string> pri in privilege)
            {
                temp = new Element(pri.Value, pri.Key, pri.Key, "attribute");
                visited.Add(temp.ToString());
                working.Push(temp);
                visited_depth.Add(temp.ToString(), 1);
                depthStack.Push(1);
                Console.WriteLine(temp.ToString());
            }
            foreach (KeyValuePair<string, HashSet<string>> host in network)
            {
                foreach(string adjacent in host.Value)
                {
                    foreach (KeyValuePair<string, HashSet<string>> service in term)
                    {
                        foreach (string service_name in service.Value)
                        {
                            if (adjacent == service.Key)
                            {
                                temp = new Element(service_name, host.Key, adjacent, "attribute");
                                visited.Add(temp.ToString());
                                working.Push(temp);
                                visited_depth.Add(temp.ToString(), 1);
                                depthStack.Push(1);
                                Console.WriteLine(temp.ToString());
                            }                              
                        }
                    }
                }
            }
            Console.WriteLine("栈里的数量："+working.Count);
            Console.WriteLine("visited的数量：" + visited.Count);

            //DFS
            while (working.Count != 0)
            {
                current = working.Pop();
                int depth = depthStack.Pop();

                if (current.ToString() == target.ToString())
                {
                    foreach(Element ttt in trace)
                    {
                        path.Add(ttt.ToString());
                    }

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

                    //补全路径
                    List<Element> re_trace = new List<Element>();
                    List<Element> re_results = new List<Element>();
                    List<Element> finalResults = new List<Element>();//最终结果
                    Stack<Element> re_working = new Stack<Element>();//补全路径用
                    List<Element> temp_trace = new List<Element>(trace);
                    Element curr = null;

                    foreach (Element tr in trace)
                    {
                        if (tr.Type == "action")
                        {
                            re_working.Push(tr);
                            int k = trace.IndexOf(tr);
                            while (re_working.Count != 0)
                            {
                                curr = re_working.Pop();
                                re_results = Backward(curr, visited);

                                if (re_results.Count != 0)
                                {
                                    if(re_results[0].Type == "action")
                                    {
                                        List<Element> temp_re_results = new List<Element>();
                                        //剔除不符合条件的结果
                                        foreach (Element re in re_results)
                                        {
                                            if (visited.Contains(re.ToString()) && !path.Contains(re.ToString()))
                                            {
                                                temp_re_results.Add(re);                                 
                                            }
                                        }
                                        //找到最小深度值
                                        int min = 0;
                                        visited_depth.TryGetValue(temp_re_results[0].ToString(), out min);
                                        foreach (Element rrr in temp_re_results)
                                        {
                                            int tem = 0;
                                            visited_depth.TryGetValue(rrr.ToString(), out tem);
                                            if (tem < min)
                                            {
                                                min = tem;                                                
                                            }
                                        }
                                        
                                        //这个最小深度可能有多个攻击，选一个就break
                                        List<string> keyList = (from q in visited_depth where q.Value == min select q.Key).ToList<string>();
                                        foreach (Element rrr in temp_re_results)
                                        {
                                            bool found = false;
                                            foreach (string sss in keyList)
                                            {
                                                if (rrr.ToString() == sss)
                                                {
                                                    found = true;
                                                    re_working.Push(rrr);
                                                    re_trace.Add(rrr);
                                                    break;
                                                }
                                            }
                                            if (found)
                                                break;
                                        }
                                        temp_re_results.Clear();
                                    }
                                    else
                                    {
                                        foreach (Element re in re_results)
                                        {
                                            if (visited.Contains(re.ToString()) && !path.Contains(re.ToString()))
                                            {
                                                re_working.Push(re);
                                                re_trace.Add(re);
                                            }
                                        }
                                    }
                                   
                                }
                            }
                            if (re_trace.Count != 0)
                            {
                                foreach (Element re in re_trace)
                                {

                                    temp_trace.Insert(k, re);
                                    Console.WriteLine(re.ToString());
                                }
                                Console.WriteLine("由" + tr.ToString() + "反补");
                                re_trace.Clear();
                                re_results.Clear();
                                Console.WriteLine("-------");
                            }
                        }
                    }
                    foreach(Element fi in temp_trace)
                    {
                        if (fi.Type == "action")
                        {
                            finalResults.Add(fi);
                            Console.WriteLine(fi.ToString());
                        }
                    }
                    return finalResults;
                }
                //回溯的过程
                if (depth > 0)
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
                 
                results = Forward(current, visited);

                //入栈
                foreach (Element result in results)
                {
                    if (!visited.Contains(result.ToString()))
                    {                       
                        visited.Add(result.ToString());
                        visited_depth.Add(result.ToString(), depth + 1);
                        working.Push(result);
                        depthStack.Push(depth + 1);
                    }
                }
            }
            Console.WriteLine("未找到目标");
            foreach (string ss in path)
            {
                Console.WriteLine(ss);
            }
            return null;
        }
        private List<Element> Forward(Element current, HashSet<string> visited)
        {  
            List<Element> results = new List<Element>();
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
                            if (tp.Atom.To == "to")
                            {
                                to = current.To;
                            }
                            else
                            {
                                to = current.From;
                            }
                        }
                        else
                        {
                            from = current.To;
                            if (tp.Atom.To == "to")
                            {
                                to = current.To;
                            }
                            else
                            {
                                to = current.From;
                            }
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
                            results.Add(temp);
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
                            //使from，to与模板对应
                            if (pre1.From == "from")
                            {
                                from = current.From;
                                if (pre1.To == "to")
                                {
                                    to = current.To;
                                }
                                else
                                {
                                    to = current.From;
                                }
                            }
                            else
                            {
                                from = current.To;
                                if (pre1.To == "to")
                                {
                                    to = current.To;
                                }
                                else
                                {
                                    to = current.From;
                                }
                            }

                            Element temp_re = null;

                            //current是权限的
                            if (current.Name == "user")
                            {
                                //是否满足全部前置，用temp_count计数
                                //找到from的邻接主机 
                                HashSet<string> val_adj = null;
                                network.TryGetValue(from, out val_adj);
                                foreach(string hostID in val_adj)
                                {
                                    int temp_count = 0;
                                    foreach (Element precondition in tp.Preconditions)
                                    {
                                        if (precondition.From == "from")
                                        {
                                            if (precondition.To == "to")
                                            {
                                                temp_re = new Element(precondition.Name, from, hostID, "attribute");
                                            }
                                            else if (precondition.To == "from")
                                            {
                                                temp_re = new Element(precondition.Name, from, from, "attribute");
                                            }
                                        }
                                        else
                                        {
                                            if (precondition.To == "from")
                                            {
                                                temp_re = new Element(precondition.Name, hostID, from, "attribute");
                                            }
                                            else if (precondition.To == "to")
                                            {
                                                temp_re = new Element(precondition.Name, hostID, hostID, "attribute");
                                            }
                                        }
                                        if (visited.Contains(temp_re.ToString()))
                                        {
                                            temp_count++;
                                        }
                                    }
                                    if (temp_count == tp.Preconditions.Count)
                                    {
                                        if (tp.Atom.From == "from")
                                        {
                                            if (tp.Atom.To == "from")
                                            {
                                                temp = new Element(tp.Atom.Name, from, from, "action");
                                            }
                                            else if (tp.Atom.To == "to")
                                            {
                                                temp = new Element(tp.Atom.Name, from, hostID, "action");
                                            }
                                        }
                                        else if (tp.Atom.From == "to")
                                        {
                                            if (tp.Atom.To == "from")
                                            {
                                                temp = new Element(tp.Atom.Name, hostID, from, "action");
                                            }
                                            else if (tp.Atom.To == "to")
                                            {
                                                temp = new Element(tp.Atom.Name, hostID, hostID, "action");
                                            }
                                        }
                                        results.Add(temp);
                                    }
                                }
                              
                            }
                            //current是服务的
                            else
                            {
                                int temp_count = 0;
                                foreach (Element precondition in tp.Preconditions)
                                {
                                    if (precondition.From == "from")
                                    {
                                        if (precondition.To == "to")
                                        {
                                            temp_re = new Element(precondition.Name, from, to, "attribute");
                                        }
                                        else if (precondition.To == "from")
                                        {
                                            temp_re = new Element(precondition.Name, from, from, "attribute");
                                        }
                                    }
                                    else
                                    {
                                        if (precondition.To == "from")
                                        {
                                            temp_re = new Element(precondition.Name, to, from, "attribute");
                                        }
                                        else if (precondition.To == "to")
                                        {
                                            temp_re = new Element(precondition.Name, to, to, "attribute");
                                        }
                                    }
                                    if (visited.Contains(temp_re.ToString()))
                                    {
                                        temp_count++;
                                    }
                                }
                                if (temp_count == tp.Preconditions.Count)
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
                                    results.Add(temp);
                                }
                            }         
                        }
                    }
                }
            }
            //foreach (Element rrr in results)
            //{
            //    Console.WriteLine(rrr.ToString());
            //}

            return results;
        }
        private List<Element> Backward(Element current, HashSet<string>visited)
        {
            List<Element> results = new List<Element>();
            Element temp = null;

            string from = "";
            string to = "";
            //Console.WriteLine(current.ToString());
            if (current.Type == "action")
            {
                foreach (Template t in attacks)
                {
                    //再攻击模板中找到对应的攻击
                    if (current.Name == t.Atom.Name)
                    {

                        if (t.Atom.From == "from")
                        {
                            from = current.From;
                            if (t.Atom.To == "to")
                            {
                                to = current.To;
                            }
                            else
                            {
                                to = current.From;
                            }
                        }
                        else
                        {
                            from = current.To;
                            if (t.Atom.To == "to")
                            {
                                to = current.To;
                            }
                            else
                            {
                                to = current.From;
                            }
                        }

                        foreach (Element pre in t.Preconditions)
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
                            results.Add(temp);
                        }
                    }
                }
            }
            else if (current.Type == "attribute")
            {
                //生成的攻击，如果是想要结果，那它必然在visited中
                //在模板的后置中找满足的攻击
                foreach (Template tp in attacks)
                {
                    foreach (Element post in tp.Postconditions)
                    {
                        if (post.Name == current.Name)
                        {
                            Element temp_re = null;

                            //使from，to与模板对应
                            if (post.From == "from")
                            {
                                from = current.From;
                                if (post.To == "to")
                                {
                                    to = current.To;
                                }
                                else
                                {
                                    to = current.From;
                                }
                            }
                            else
                            {
                                from = current.To;
                                if (post.To == "to")
                                {
                                    to = current.To;
                                }
                                else
                                {
                                    to = current.From;
                                }
                            }
                            if (current.Name == "user")
                            {
                                HashSet<string> val_adj = null;
                                network.TryGetValue(from, out val_adj);
                                //to是确定的
                                foreach (string hostID in val_adj)
                                {
                                    int temp_count = 0;
                                    foreach (Element precondition in tp.Preconditions)
                                    {
                                        if (precondition.From == "from")
                                        {
                                            if (precondition.To == "to")
                                            {
                                                temp_re = new Element(precondition.Name, hostID, to, "attribute");
                                            }
                                            else if (precondition.To == "from")
                                            {
                                                temp_re = new Element(precondition.Name, hostID, hostID, "attribute");
                                            }
                                        }
                                        else
                                        {
                                            if (precondition.To == "from")
                                            {
                                                temp_re = new Element(precondition.Name, to, hostID, "attribute");
                                            }
                                            else if (precondition.To == "to")
                                            {
                                                temp_re = new Element(precondition.Name, to, to, "attribute");
                                            }
                                        }
                                        if (visited.Contains(temp_re.ToString()))
                                        {
                                            temp_count++;
                                        }
                                    }
                                    if (temp_count == tp.Preconditions.Count)
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
                                        results.Add(temp);
                                    }
                                }
                            }
                            else
                            {
                                if (tp.Atom.From == "from")
                                {
                                    if (tp.Atom.To == "to")
                                        //(name, from ,to, "")
                                        temp = new Element(tp.Atom.Name, from, to, "action");
                                    else
                                        //(name, from, from ,"")
                                        temp = new Element(tp.Atom.Name, from, from, "action");
                                }
                                else
                                {
                                    //(name ,to, from, "")
                                    if (tp.Atom.To == "from")
                                        temp = new Element(tp.Atom.Name, to, from, "action");
                                    else
                                        //(name, to ,to, "")
                                        temp = new Element(tp.Atom.Name, to, to, "action");
                                }
                                results.Add(temp);
                            }
                        }
                    }
                }
            }

            return results;
        }

    }
}