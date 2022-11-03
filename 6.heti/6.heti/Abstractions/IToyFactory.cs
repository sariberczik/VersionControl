using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6.heti.Abstractions
{
    public interface IToyFactory //interfacenek mindig publicnak kell lennie
    {
        Toy CreateNew(); //nem kell kapcsos zarojel, ahogy az absztaktnal se kellett

    }
}
