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

namespace SmallPertubation
{
    public partial class AxisSettings : Form
    {

        private Chart form1Chart;

        private DataTable myDataTable;



        public AxisSettings(Chart mychart)
        {
            InitializeComponent();

            this.myDataTable = new DataTable();


            myDataTable.Columns.Add("Параметр");
            myDataTable.Columns.Add("Мин");
            myDataTable.Columns.Add("Макс");


            myDataTable.Rows.Add("Реактивность", MyConst.Rect.MinMax[(int)MyConst.Rect.AxisParam.react, 0], MyConst.Rect.MinMax[(int)MyConst.Rect.AxisParam.react, 1]);
            myDataTable.Rows.Add("ОР СУЗ", MyConst.Rect.MinMax[(int)MyConst.Rect.AxisParam.Hsuz, 0], MyConst.Rect.MinMax[(int)MyConst.Rect.AxisParam.Hsuz, 1]);


            dataGridView1.DataSource = myDataTable;

            
            this.form1Chart = mychart;

            //this.form1Chart.ChartAreas[0].AxisY.Minimum = -0.02;

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            double newVal = double.Parse(myDataTable.Rows[e.RowIndex][e.ColumnIndex].ToString());
            MyConst.Rect.MinMax[e.RowIndex, e.ColumnIndex - 1] = newVal;


            //MessageBox.Show(e.RowIndex.ToString());

            switch (e.RowIndex)
            {
                case 0:
                    if (e.ColumnIndex == 1)
                    {
                        this.form1Chart.ChartAreas[0].AxisY.Minimum = newVal;
                      //  MessageBox.Show("Минимальное реактивности" + newVal.ToString());
                    }
                    else {
                        this.form1Chart.ChartAreas[0].AxisY.Maximum = newVal;
                       // MessageBox.Show("Максимальное реактивноcnb" + newVal.ToString());
                    }
                    break;
                case 1:
                    if (e.ColumnIndex == 1)
                    {
                        this.form1Chart.ChartAreas[0].AxisY2.Minimum = newVal;
                        //  MessageBox.Show("Минимальное реактивности" + newVal.ToString());
                    }
                    else
                    {
                        this.form1Chart.ChartAreas[0].AxisY2.Maximum = newVal;
                        // MessageBox.Show("Максимальное реактивноcnb" + newVal.ToString());
                    }
                break;

                default:
                    break;
            }




        }
        

        



    }
}
