using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _6.heti.Abstractions
{
    class Toy: Label
    {

        public Toy()
        {
            AutoSize = false;
            Width = 50;
            Height = 50;

            Paint += Toy_Paint; //painthez esemenykezelo: paint+=tabtab (throw...törlése)

        }

        private void Toy_Paint(object sender, PaintEventArgs e)
        {
            DrawImage(e.Graphics);

        }

        protected void DrawImage(Graphics graphics) //új függvényt DrawImage néven és Graphics típusú bemeneti paraméterrel, villanykorte
        //protected: ha nem a fv-re van szükségem, csak arr h hasznalhassam
        {
            graphics.FillEllipse(new SolidBrush(Color.Blue), 0, 0, Width, Height); //kitöltött kék kör
        }

        public void MoveBall() //publikus fv
        {
            Left++; //1gyel növelem
        }

    }
}
