using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.Office.Interop.Excel;
using Chart = System.Windows.Forms.DataVisualization.Charting.Chart;

namespace Converter
{
    internal class Graph
    {
        public static int _numberseries = 0;
        public static List<double> MinmaxListPrimary  = new List<double>();
        public static List<double> MinmaxListSecondary = new List<double>();
        private static int _countTime;
      //  public static double[] indexTimelist;
        public static void CreateLine(List<Sensors> allSensors, string nameChecked, Chart chart, bool flagAxis)
        {
            for (int j = 0; j < allSensors.Count; j++)
            {
                if (nameChecked.Split('\t')[0] == allSensors[j].KKS_Name)
                {
                    for (int i = 0; i < allSensors[j].MyListRecordsForOneKKS.Count; i++)
                    {
                        chart.Series[_numberseries].Points.AddXY(
                            allSensors[j].MyListRecordsForOneKKS[i].ValueTimeForDAT,
                            allSensors[j].MyListRecordsForOneKKS[i].Value);
                        
                       
                        if (flagAxis == true)
                        {
                            MinmaxListPrimary.Add(allSensors[j].MyListRecordsForOneKKS[i].Value);
                        }
                        if (flagAxis == false)
                        {
                            MinmaxListSecondary.Add(allSensors[j].MyListRecordsForOneKKS[i].Value);
                        }
                       
                    }
              //     MessageBox.Show(minmaxList.Count.ToString());
                    chart.Series[_numberseries].IsVisibleInLegend = true;
                    chart.Series[_numberseries].LegendText = allSensors[j].KKS_Name;
             
                }
            }


            chart.Series[_numberseries].BorderWidth = 3;
            if (flagAxis == true)
            {
                chart.Series[_numberseries].YAxisType = AxisType.Primary;
                if (MinmaxListPrimary.Max() > 5)
                {
                    chart.ChartAreas[0].AxisY.Maximum = Math.Round((MinmaxListPrimary.Max() + 0.25), 2);
                }
                if (MinmaxListPrimary.Max() < -5)
                {
                    chart.ChartAreas[0].AxisY.Minimum = Math.Round((MinmaxListPrimary.Min() - 0.25), 2);
                }
            }
            if (flagAxis == false)
            {
                chart.Series[_numberseries].YAxisType = AxisType.Secondary;
                if (MinmaxListSecondary.Max() > 5 || MinmaxListSecondary.Min() < -5)
                {
                    chart.ChartAreas[0].AxisY2.Maximum = Math.Round((MinmaxListSecondary.Max() + 0.25), 2);
                    chart.ChartAreas[0].AxisY2.Minimum = Math.Round((MinmaxListSecondary.Min() - 0.25), 2);
                }
            }

            chart.ChartAreas[0].AxisY2.MinorTickMark.Enabled = true;
            chart.ChartAreas[0].AxisY2.MinorTickMark.LineDashStyle = ChartDashStyle.Dash;
            chart.ChartAreas[0].AxisY2.MinorGrid.Enabled = true;
            chart.ChartAreas[0].AxisY2.MinorGrid.LineDashStyle = ChartDashStyle.Dash;
            chart.ChartAreas[0].AxisY2.MinorGrid.LineColor = Color.LightGray;
            chart.ChartAreas[0].AxisY2.MinorTickMark.LineColor = Color.LightGray;

            chart.ChartAreas[0].AxisY2.MajorTickMark.Enabled = true;
            chart.ChartAreas[0].AxisY2.MajorTickMark.LineDashStyle = ChartDashStyle.Dash;
            chart.ChartAreas[0].AxisY2.MajorGrid.Enabled = true;
            chart.ChartAreas[0].AxisY2.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            chart.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Gray;
            chart.ChartAreas[0].AxisY2.MajorTickMark.LineColor = Color.Gray;

            _numberseries++;
             chart.ChartAreas[0].Position.Auto = true;        
        }

      //  public static double 

    }
}
