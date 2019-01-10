using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{
    class Precondition: Graph
    {
       
        private string priviledge, service;
        
        public string Priviledge { get => priviledge; set => priviledge = value; }
        public string Service { get => service; set => service = value; }
        
    }
}
