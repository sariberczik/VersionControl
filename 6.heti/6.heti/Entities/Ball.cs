using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _6.heti.Entities
{
    class Ball: Label
    {
        public Ball()
        {
            AutoSize = false;
            Width = 50;
            Height = 50;

            Paint += Ball_Paint; //painthez esemenykezelo: paint+=tabtab (throw...törlése)

        }

        private void Ball_Paint(object sender, PaintEventArgs e)
        {
            DrawImage(e.Graphics);

        }

        void DrawImage(Graphics graphics) //új függvényt DrawImage néven és Graphics típusú bemeneti paraméterrel, villanykorte
        {
            graphics.FillEllipse(new SolidBrush(Color.Blue), 0, 0, Width, Height); //kitöltött kék kör
        }
        
    }
}
