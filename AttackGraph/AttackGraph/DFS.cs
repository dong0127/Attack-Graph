public void DFSVerification()
{
    List<String>HashTable Visited = new StringHashTable(Common.Classes.Ultility.Ultility.MC_INITIAL_SIZE);
    int Transitions;
    Stack<Element> working = new Stack<Element>(1024);
    Stack<int> depthStack = new Stack<int>(1024);

    List<Element> CounterExampleTrace = new 

    //extrapolation before add to TaskStack
    List<Element> initConfigModel = InitialStep as TAConfiguration;//函数初始状态求出来
    // as转型用的
    Visited.Add(InitialStep.GetID());

    working.Push(InitialStep);
  
    depthStack.Push(0);

    List<int> depthList = new List<int>(1024);
    
    do
    {
        ConfigurationBase current = working.Pop();
        int depth = depthStack.Pop();

        if (depth > 0)
        {
            while (depthList[depthList.Count - 1] >= depth)
            {
                int lastIndex = depthList.Count - 1;
                depthList.RemoveAt(lastIndex);
                CounterExampleTrace.RemoveAt(lastIndex);
            }
        }

        CounterExampleTrace.Add(current);
        IEnumerable<Element> list = current.MakeOneMove();
        Transitions += list.Count();

        if (current.IsDeadLock)
        {
                VerificationResult = VerificationResultType.INVALID;
                NoOfStates = Visited.Count;
                return;
        }

        depthList.Add(depth);

        foreach (Element step in list)
        {

            string stepID = step.GetID();
            if (!Visited.ContainsKey(stepID))
            {
                Visited.Add(stepID);
                working.Push(step);
                depthStack.Push(depth + 1);
            }
        }
    } while (working.Count > 0);

    CounterExampleTrace = null;

    NoOfStates = Visited.Count;
}

