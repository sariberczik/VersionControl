using _6.heti.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6.heti.Entities
{
    class CarFactory : IToyFactory
    {
        public Toy CreateNew()
        {
            return new Car(); // a fv maga annyi h letrehoz egy uj kocsit, ha meghívom a fv-t, return: mindig ujra csinal egyet
        }
    }
}
