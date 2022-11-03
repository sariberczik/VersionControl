﻿using _6.heti.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6.heti.Entities
{
    public class BallFactory : IToyFactory
    {
        public Toy CreateNew()
        {
            return new Ball(); // a fv maga annyi h letrehoz egy uj labdat, ha meghívom a fv-t, return: mindig ujra csinal egyet
        }
    }
}
