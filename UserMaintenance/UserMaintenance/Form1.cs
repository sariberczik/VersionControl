using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMaintenance.Entities;

namespace UserMaintenance
{
    using Excel = Microsoft.Office.Interop.Excel;
    using System.Reflection;

    public partial class Form1 : Form
    {
        

        BindingList<User> users = new BindingList<User>();

        List<Flat> Flats; //lista
        RealEstateEntities context = new RealEstateEntities(); //példányosítás

        Excel.Application xlApp; // A Microsoft Excel alkalmazás
        Excel.Workbook xlWB; // A létrehozott munkafüzet
        Excel.Worksheet xlSheet; // Munkalap a munkafüzeten belül


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

            LoadData();

            CreateExcel();

            

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

        private void LoadData() 
        {
            context.Flats.ToList();
                        
        }

        private void CreateExcel()
        {
            try
            {
                // Excel elindítása és az applikáció objektum betöltése
                xlApp = new Excel.Application();

                // Új munkafüzet
                xlWB = xlApp.Workbooks.Add(Missing.Value);

                // Új munkalap
                xlSheet = xlWB.ActiveSheet;

                // Tábla létrehozása
                CreateTable(); // Ennek megírása a következő feladatrészben következik

                // Control átadása a felhasználónak
                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex) // Hibakezelés a beépített hibaüzenettel
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

                // Hiba esetén az Excel applikáció bezárása automatikusan
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }

        }

        private void CreateTable()
        {
            string[] headers = new string[] {
             "Kód",
             "Eladó",
             "Oldal",
             "Kerület",
             "Lift",
             "Szobák száma",
             "Alapterület (m2)",
             "Ár (mFt)",
             "Négyzetméter ár (Ft/m2)"};

            for (int i = 0; i < headers.Count(); i++)
            {
                xlSheet.Cells[1, 1] = headers[0];
            }

            object[,] values = new object[Flats.Count, headers.Length];

            int counter = 0;
            foreach (Flat f in Flats)
            {
                values[counter, 0] = f.Code;
                values[counter, 1] = f.Vendor;
                values[counter, 2] = f.Side;
                values[counter, 3] = f.District;

                if (f.Elevator)
                {
                    values[counter, 4] = "Van";
                }
                else
                {
                    values[counter, 4] = "Nincs";
                }

                values[counter, 5] = f.NumberOfRooms;
                values[counter, 6] = f.FloorArea;
                values[counter, 7] = f.Price;
                values[counter, 8] = string.Format("={0}/{1}*1000000", GetCell(counter+2,7),GetCell(counter+2,8));
                counter++;
                

            }

            xlSheet.get_Range(GetCell(2, 1), GetCell(1 + values.GetLength(0), values.GetLength(1))).Value2 = values;

            FormatContent();
            FormatHeader();

        }

        private string GetCell(int x, int y)
        {
            string ExcelCoordinate = "";
            int dividend = y;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                dividend = (int)((dividend - modulo) / 26);
            }
            ExcelCoordinate += x.ToString();

            return ExcelCoordinate;

            
        }

        private void FormatHeader()
        {

            Excel.Range headerRange = xlSheet.get_Range(GetCell(1, 1), GetCell(1, 9));
            headerRange.Font.Bold = true;
            headerRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            headerRange.EntireColumn.AutoFit();
            headerRange.RowHeight = 40;
            headerRange.Interior.Color = Color.LightBlue;
            headerRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);
        }

        
        void FormatContent()
        {
            int lastRowID = xlSheet.UsedRange.Rows.Count;
            Excel.Range contentRange = xlSheet.get_Range(GetCell(2, 1), GetCell(lastRowID, 9));
            contentRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

            Excel.Range lastcolumnRange = xlSheet.get_Range(GetCell(2, 9), GetCell(lastRowID, 9));
            lastcolumnRange.Interior.Color = Color.LightGreen;
            //lastcolumnRange.

            Excel.Range firstcolumnRange = xlSheet.get_Range(GetCell(2, 1), GetCell(lastRowID, 1));
            firstcolumnRange.Interior.Color = Color.LightYellow;
            firstcolumnRange.Font.Bold = true;
        }

    }
}
