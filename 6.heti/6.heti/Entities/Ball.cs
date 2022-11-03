using _6.heti.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _6.heti.Entities
{
    public class Ball: Toy //toyra atirtuk a labelt, be kellett ujra hivatkozni
    {
        

        protected override void DrawImage(Graphics graphics) //új függvényt DrawImage néven és Graphics típusú bemeneti paraméterrel, villanykorte
        //protected: ha nem a fv-re van szükségem, csak arr h hasznalhassam

        //override: h implementáljam az absztraktot, labelt atirtam toyra
        {
            graphics.FillEllipse(new SolidBrush(Color.Blue), 0, 0, Width, Height); //kitöltött kék kör
        }

        


    }
}
