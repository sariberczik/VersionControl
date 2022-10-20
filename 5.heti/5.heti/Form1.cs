using _5.heti.Entities;
using _5.heti.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace _5.heti
{
    public partial class Form1 : Form
    {
        BindingList<RateData> rateDatas = new BindingList<RateData>();
        


        public Form1()
        {
            InitializeComponent();

            harmadik();
            hatodik();
            Refresh();
        }

        private void RefreshData()
        {
            rateDatas.Clear();

            harmadik();
            hatodik();
        }

        private void harmadik()
        {

            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = comboBox1.SelectedItem.ToString(),
                startDate = dateTimePicker1.Value.ToString(),
                endDate = dateTimePicker2.Value.ToString()

            };

            var reponse = mnbService.GetExchangeRates(request);

            var result = reponse.GetExchangeRatesResult;



            var xml = new XmlDocument();
            xml.LoadXml(result);

            foreach (XmlElement element in xml.DocumentElement)
            {
                var rate = new RateData();
                rateDatas.Add(rate);

                rate.Date = DateTime.Parse(element.GetAttribute("date"));

                var childElement = (XmlElement)element.ChildNodes[0];
                rate.Currency = childElement.GetAttribute("curr");

                var unit =decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit!=0)
                {
                    rate.Value = value / unit;
                }
                

            }

        }

        private void hatodik()
        {
            chartRateData.DataSource = rateDatas;

            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2; //2 vastagsagu

            var legend=chartRateData.Legends[0];
            legend.Enabled = false;  //Ne látszódjon oldalt a címke (legend)

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false; //Ne látszódjanak a fő grid vonalak se az X, se az Y tengelyen
            chartArea.AxisY.MajorGrid.Enabled = false; 
            chartArea.AxisY.IsStartedFromZero = false; //0tol induljon

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
