using _6.heti.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6.heti.Entities
{
    class PresentFactory : IToyFactory
    {
        public Color ribboncolor { get; set; } //villanykorte

        public Color boxcolor { get; set; }

        public Toy CreateNew()
        {
            return new Present(boxcolor, ribboncolor); // a fv maga annyi h letrehoz egy uj labdat, ha meghívom a fv-t, return: mindig ujra csinal egyet
        }
    }
}
