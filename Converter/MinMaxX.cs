using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Converter
{
    public partial class MinMaxX : Form
    {
        public MinMaxX()
        {
            InitializeComponent();


        }

        private void MinMaxX_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Form1 main = new Form1();



                    main.chart1.ChartAreas[0].AxisY.Maximum = 4;

                 //   MinMaxX ChangeParametrImage = new MinMaxX(); ;
                  //  ChangeParametrImage.Show();
                  //  ChangeParametrImage.Location = e.Location;

                   // ChangeParametrImage.textBox1.Text = chart1.ChartAreas[0].AxisY.Maximum.ToString();
                  //  ChangeParametrImage.textBox2.Text = chart1.ChartAreas[0].AxisY.Minimum.ToString();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Form1 main = this.Owner as Form1;

               // if (double.Parse(textBox2.Text) < main.chart1.ChartAreas[0].AxisY.Maximum)
               // {
              //      main.chart1.ChartAreas[0].AxisY.Minimum = double.Parse(textBox2.Text);
              //  }

        }
    }
}
