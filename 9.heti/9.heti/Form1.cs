using _9.heti.Entities;
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
        List<person> Population = new List<person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();

        List<int> Males = new List<int>();
        List<int> Females = new List<int>();

        Random rng = new Random(1234);

        public Form1()
        {
            InitializeComponent();

        }

        void Simulation()
        {
            for (int i = 2005; i <= numericUpDown1.Value; i++)
            {
                for (int j = 0; j < Population.Count; j++)
                {
                    SimStep(i, Population[j]);

                    if (Population[j].Gender == Gender.Male)
                    {
                        Males.Add(i);
                    }
                    else
                    {
                        Females.Add(i);
                    }

                }

                int NbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive == true
                                  select x).Count();

                int NbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive == true
                                    select x).Count();
            }

            DisplayResult(Males, Females);
        }
    }
}
