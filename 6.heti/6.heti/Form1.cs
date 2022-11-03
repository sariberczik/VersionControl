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
        List<Ball> _balls = new List<Ball>();


        private BallFactory _factory;

        public BallFactory Factory
        {
            get { return _factory; }
            set { BallFactory _factory = value; }
        }


        public Form1()
        {
            InitializeComponent();

            Factory = new BallFactory(); //A konstruktorban töltsd fel a Factory változót egy BallFactory példánnyal.

        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var ball = Factory.CreateNew(); //Factory CreateNew metódusát felhasználva hozz létre egy Ball példányt

            _balls.Add(ball); //listazhoz hozzaadas

            ball.Left = -ball.Width; //A Left tulajdonságát pedig állítsd a szélessége negatív értékére.

            mainPanel.Controls.Add(ball); //panelhez adás (panelnél kell a controls, listanal nem)

        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {

        }
    }
}
