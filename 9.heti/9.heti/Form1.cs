﻿using _9.heti.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        public List<person> GetPopulation(string csvpath)
        {
            List<person> population = new List<person>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    person p = new person();
                    p.BirthYear = int.Parse(line[0]);
                    p.Gender = (Gender)int.Parse(line[1]);
                    p.NbrOfChildren = int.Parse(line[2]);
                    population.Add(p);
                }
            }

            return population;
        }

        public List<BirthProbability> GetBirthProbability(string csvpath)
        {
            List<BirthProbability> birthprobaility = new List<BirthProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    BirthProbability b = new BirthProbability();
                    b.Age = int.Parse(line[0]);
                    b.NbrOfChildren = int.Parse(line[1]);
                    b.P = double.Parse(line[2]);
                    birthprobaility.Add(b);
                }
            }

            return birthprobaility;
        }
        public List<DeathProbability> GetDeathProbability(string csvpath)
        {
            List<DeathProbability> deathprobability = new List<DeathProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    DeathProbability d = new DeathProbability();
                    d.Gender = (Gender)int.Parse(line[0]);
                    d.Age = int.Parse(line[1]);
                    d.P = double.Parse(line[2]);
                    deathprobability.Add(d);
                }
            }

            return deathprobability;
        }

        void SimStep(int year, person person)
        {
            if (!person.IsAlive) return;

            int age = year - person.BirthYear;

            double pD = (from x in DeathProbabilities
                         where x.Age == age && x.Gender == person.Gender
                         select x.P).FirstOrDefault();

            if (rng.NextDouble() <= pD) person.IsAlive = false;

            if (person.IsAlive == false || person.Gender == Gender.Male) return;

            double pB = (from x in BirthProbabilities
                         where x.Age == age && x.NbrOfChildren == person.NbrOfChildren
                         select x.P).FirstOrDefault();

            if (rng.NextDouble() <= pB)
            {
                person newBorn = new person();
                newBorn.Gender = (Gender)rng.Next(1, 3);
                newBorn.BirthYear = year;
                newBorn.NbrOfChildren = 0;
                Population.Add(newBorn);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Population = GetPopulation(textBox1.Text);
            BirthProbabilities = GetBirthProbability(@"C:\Windows\Temp\születés.csv");
            DeathProbabilities = GetDeathProbability(@"C:\Windows\Temp\halál.csv");

            Males.DefaultIfEmpty();
            Females.DefaultIfEmpty();
            richTextBox1.Text = "";

            Simulation();
        }







    }
}
