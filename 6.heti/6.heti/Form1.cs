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


        private Toy _nextToy; //osztályszintű Toy típusú változót _nextToy néven

        private IToyFactory _factory;

        public IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value;
                DisplayNext();//Így a Factory megváltozása esetén mindig megváltozik majd a megjelenített kép is.
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            Factory = new CarFactory();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Factory = new BallFactory();

            Factory = new BallFactory
            {
                BallColor = button2.BackColor
            };

        }

        private void DisplayNext()
        {
            if (_nextToy != null)
                Controls.Remove(_nextToy); //ha nem ures, eltavolitas

            _nextToy = Factory.CreateNew();

            _nextToy.Top = label1.Top + label1.Height + 20;
            _nextToy.Left = label1.Left;

            Controls.Add(_nextToy);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var button = (Button)sender; //változót button néven, amiben a sender-t Button-á cast-olva tárolod

            var colorPicker = new ColorDialog(); //példányosíts egy ColorDialog-ot

            colorPicker.Color = button3.BackColor;

            if (colorPicker.ShowDialog() != DialogResult.OK)
                return; //megnyitas, ha nem okkal zarul, kilépés
            button.BackColor = colorPicker.Color;

        }



    }
}
