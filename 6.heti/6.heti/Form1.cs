using _6.heti.Abstractions;
using _6.heti.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _6.heti
{
    public partial class Form1 : Form
    {
        List<Toy> _toys = new List<Toy>();


        private IToyFactory _factory;

        public IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }


        public Form1()
        {
            InitializeComponent();

            Factory = new BallFactory(); //A konstruktorban töltsd fel a Factory változót egy BallFactory példánnyal.
            //ezt at lehet irni CarFactory-re es akk kocsik jelennek meg
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var ball = Factory.CreateNew(); //Factory CreateNew metódusát felhasználva hozz létre egy Ball példányt

            _toys.Add(ball); //listazhoz hozzaadas

            ball.Left = -ball.Width; //A Left tulajdonságát pedig állítsd a szélessége negatív értékére.

            mainPanel.Controls.Add(ball); //panelhez adás (panelnél kell a controls, listanal nem)

        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPosition = 0;

            foreach (var ball in _toys)
            {
                ball.MoveToy(); //mindegyik elem moveball metodusa

                if (ball.Left > maxPosition)
                    maxPosition = ball.Left;
            }

            if (maxPosition > 1000)
            {
                var oldestBall = _toys[0];
                mainPanel.Controls.Remove(oldestBall);
                _toys.Remove(oldestBall);
            }
        }
    }
}
