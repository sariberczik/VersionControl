using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _9.heti
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            List<person> Population = new List<person>();
            List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
            List<DeathProbability> DeathProbabilities = new List<DeathProbability>();

            List<int> Males = new List<int>();
            List<int> Females = new List<int>();
        }
    }
}
