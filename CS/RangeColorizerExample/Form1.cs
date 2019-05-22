using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DevExpress.XtraCharts;

namespace RangeColorizerExample {
    public partial class Form1 : Form {
        const string filepath = @"..//..//Data//HPI.xml";

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            #region #BarSeries
            // Create and customize a bar series.
            Series barSeries = new Series() {
                DataSource = LoadData(filepath),               
                ArgumentDataMember = "Country",
                ColorDataMember = "Hpi",
                View = new SideBySideBarSeriesView()                
            };
            barSeries.View.Colorizer = CreateColorizer();
            barSeries.ValueDataMembers.AddRange(new string[] { "Product" });
            #endregion #BarSeries

            // Add the series to the ChartControl's Series collection.
            chartControl1.Series.Add(barSeries);

            // Show a title for the values axis.
            ((XYDiagram)chartControl1.Diagram).AxisY.Title.Text = "GDP per capita, $";
            ((XYDiagram)chartControl1.Diagram).AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
        }

        #region #RangeColorizer
        // Creates a range colorizer.
        ChartColorizerBase CreateColorizer() {
            Palette palette = new Palette("Custom");
            palette.Add(Color.FromArgb(255, 255, 90, 25), Color.FromArgb(255, 255, 90, 25));
            palette.Add(Color.FromArgb(255, 229, 227, 53), Color.FromArgb(255, 229, 227, 53));
            palette.Add(Color.FromArgb(255, 110, 201, 92), Color.FromArgb(255, 110, 201, 92));

            RangeColorizer colorizer = new RangeColorizer() {
                LegendItemPattern = "{V1} - {V2} HPI",
                Palette = palette
            };
            colorizer.RangeStops.AddRange(new double[] { 22, 30, 38, 46, 54, 64 });
            return colorizer;
        }
        #endregion #RangeColorizer

        #region #DataLoad
        class HpiPoint {
            public string Country { get; set; }
            public double Product { get; set; }
            public double Hpi { get; set; }
        }

        // Loads data from an XML data source.
        static List<HpiPoint> LoadData(string filepath) {
            XDocument doc = XDocument.Load(filepath);
            List<HpiPoint> points = new List<HpiPoint>();
            foreach (XElement element in doc.Element("G20HPIs").Elements("CountryStatistics")) {
                points.Add(new HpiPoint() {
                    Country = element.Element("Country").Value,
                    Product = Convert.ToDouble(element.Element("Product").Value),
                    Hpi = Convert.ToDouble(element.Element("HPI").Value),
                });
            }
            return points;
        }
        #endregion #DataLoad
    }
}
