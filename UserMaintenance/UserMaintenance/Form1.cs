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
using UserMaintenance.Entities;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {

        BindingList<User> users = new BindingList<User>();



        public Form1()
        {
            InitializeComponent();

            label1.Text = Resource1.FullName;
            //label2.Text = Resource1.FirstName;

            button1.Text = Resource1.Add;

            listBox1.DataSource = users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "FullName";

            button2.Text = Resource1.Writing_to_a_file;

            button3.Text = Resource1.Delete;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var u = new User()
            {
                //LastName = textBox1.Text,
                //FirstName=textBox2.Text

                FullName=textBox2.Text
            };
            users.Add(u);


        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Application.StartupPath;
            sfd.Filter="Vesszővel tagolt szöveg(*.csv)|*.csv";
            sfd.DefaultExt = "csv";
            sfd.AddExtension = true;

            if (sfd.ShowDialog()==DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8);
                foreach (var u in users)
                {
                    sw.Write(u.ID);
                    sw.Write(";");
                    sw.Write(u.FullName);
                    sw.WriteLine();
                }
                sw.Close();
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            Guid delete = ((User)listBox1.SelectedItem).ID;
            var d = (from u in users
                     where u.ID == delete
                     select u).FirstOrDefault();
            users.Remove(d);
        }
    }
}
