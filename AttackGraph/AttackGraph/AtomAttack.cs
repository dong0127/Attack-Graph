using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{
    /*
    struct attacks
    {
        public string name;
        public List<string> preconditions;
        public List<string> effects;
    }
    */
    class AtomAttack
    {
        Dictionary<string, List<string>> precondition = new Dictionary<string, List<string>>()
        {
            {"sshd-bof", new List<string>{ "user", "sshd" } },
            {"Ftp-rhosts", new List<string>{ "user", "ftp" } },
            {"rsh", new List<string>{ "user", "trust" } },
            {"local-bof", new List<string>{ "user" } }
        };
        
        Dictionary<string, List<string>> effect = new Dictionary<string, List<string>>()
        {
            {"sshd-bof", new List<string>{ "user" } },
            {"Ftp-rhosts", new List<string>{ "trust" } },
            {"rsh", new List<string>{ "user"} },
            {"local-bof", new List<string>{ "root" } }
        };
        /*
        public void GetAttack()
        {
            attacks attack0;
            attack0.name = "sshd-bof";
            attack0.preconditions =new List<string>() { "user", "sshd" };
            attack0.effects = new List<string>() { "user"};

            attacks attack1;
            attack1.name = "Ftp-rhosts";
            attack1.preconditions = new List<string>() { "user", "ftp" };
            attack1.effects = new List<string>() { "trust" };

            attacks attack2;
            attack2.name = "rsh";
            attack2.preconditions = new List<string>() { "user", "trust" };
            attack2.effects = new List<string>() { "user"};

            attacks attack3;
            attack2.name = "local-bof";
            attack3.preconditions = new List<string>() { "user" + "." + FinalTarget };
            attack3.effects = new List<string>() { "root" };
        }
        */
    }
}
