using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{
    class Network
    {
        string id;
        List<string> serivce;

        public Network(string id, List<string> serivce)
        {
            this.id = id;
            this.serivce = serivce;
        }

        public List<string> Serivce { get => serivce; set => serivce = value; }
        public string Id { get => id; set => id = value; }
    }
}
