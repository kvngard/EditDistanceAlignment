using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticsLab
{
    class node
    {
        public double value { get; set; }
        public node prev { get; set; }
        public string type { get; set; }

        public node(double value, node prev, string type)
        {
            this.value = value;
            this.prev = prev;
            this.type = type;
        }
    }
}
