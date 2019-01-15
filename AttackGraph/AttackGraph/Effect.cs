using System;
using System.Collections.Generic;
using System.Text;

namespace AttackGraph
{
    class Effect: Model
    {
        private List<string> effects;

        public List<string> Effects { get => effects; set => effects = value; }
    }
}
