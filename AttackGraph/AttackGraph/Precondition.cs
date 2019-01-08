using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{
    class Precondition: Graph
    {
        private string priviledge, service;
        private int connection;

        public string Priviledge { get => priviledge; set => priviledge = value; }
        public string Service { get => service; set => service = value; }
        public int Connection { get => connection; set => connection = value; }
    }
}
