using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _7.heti
{
    public partial class Form1 : Form
    {

        PortfolioEntities context = new PortfolioEntities();

        //List<Tick> ticks=new List<Tick>();
        List<Tick> Ticks;


        public Form1()
        {
            InitializeComponent();

            //dataGridView1.DataSource = context.Ticks.ToList();

            Ticks = context.Ticks.ToList();
            dataGridView1.DataSource = Ticks;
        }
    }
}
