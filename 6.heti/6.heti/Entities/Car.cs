using _6.heti.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6.heti.Entities
{
    class Car : Toy //Implementáld az absztrakt osztály által kikényszerített elemeket: villanykorte a car-ra
    {
        protected override void DrawImage(Graphics graphics)
        {
            Image imageFile = Image.FromFile("Images/car.png");
            g.DrawImage(imageFile, new Rectangle(0, 0, Width, Height));
        }
    }
}
