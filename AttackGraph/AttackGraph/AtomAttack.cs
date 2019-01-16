using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{

    class AtomAttack
    {
        
        List<List<string>> precondition =new  List<List<string>>()
        {
           new List<string> {"sshd-bof", "user", "sshd" },
           new List<string> {"Ftp-rhosts", "user", "ftp" },
           new List<string> {"rsh","user", "trust" },
           new List<string> {"local-bof", "user" }
        };

        List<List<string>> effect = new List<List<string>>()
        {
           new List<string>{"sshd-bof", "user" },
           new List<string> {"Ftp-rhosts", "trust" },
           new List<string> {"rsh", "user" },
           new List<string>{"local-bof", "root" }
        };

        public List<List<string>> Precondition { get => precondition; set => precondition = value; }
        public List<List<string>> Effect { get => effect; set => effect = value; }
    }
}
