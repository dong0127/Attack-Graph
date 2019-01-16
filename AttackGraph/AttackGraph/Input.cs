using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{
    class Input
    {
        int start = 0;
        int end = 2;
      
        List<string> initial = new List<string>()
        {
           "user0",
           "ftp1",
           "ftp2",
           "sshd1"
        };

        public List<string> Initial { get => initial; set => initial = value; }
        public int Start { get => start; set => start = value; }
        public int End { get => end; set => end = value; }
    }
}
