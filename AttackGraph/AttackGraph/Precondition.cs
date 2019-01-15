using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{
    class Precondition: Model
    {
         List<string> preconditions = new List<string>()
        {
            "user" + "." +0,
            "ftp" + "." +1,
            "ftp" + "." +2,
            "sshd" + "." +1
        };

        public List<string> Preconditions { get => preconditions; set => preconditions = value; }
    }
}
