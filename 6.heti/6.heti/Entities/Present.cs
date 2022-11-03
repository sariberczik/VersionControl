using _6.heti.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6.heti.Entities
{
    class Present : Toy
    {
        protected override void DrawImage(Graphics graphics)
        {
            graphics.FillRectangle(boxcolor, 0, 0, Width, Height);
            graphics.FillRectangle(ribboncolor, Width / 2 - Width / 10, 0, Width/5, Height);
            graphics.FillRectangle(ribboncolor,0, Height / 2 - Height / 10,  Width , Height/5);
        }

        public Present(Color box, Color ribbon)
        {
            boxcolor = new SolidBrush(box);
            ribboncolor = new SolidBrush(ribbon);
        }

        public SolidBrush boxcolor { get; private set; }
        public SolidBrush ribboncolor { get; private set; }
    }
}
