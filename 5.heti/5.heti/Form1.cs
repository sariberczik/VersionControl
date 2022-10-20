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

        }

        private void harmadik()
        {

            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"

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

                rate.Currency=
            }

        }

        
    }
}
