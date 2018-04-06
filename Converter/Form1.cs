﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;



//псевдонимы
using SD = System.Data;


namespace Converter
{
    public struct OneRec
    {
        public double dt;
        public double value;
    }

    public partial class Form1 : Form
    {
        private System.Collections.ArrayList customers = new System.Collections.ArrayList();
        private MyVirtualClass customerInEdit;
        private int rowInEdit = -1;
        private bool rowScopeCommit = true;

        private System.Collections.ArrayList customers1 = new System.Collections.ArrayList();
        private MyVirtualClass customerInEdit1;
        private int rowInEdit1 = -1;
        private bool rowScopeCommit1 = true;

        private System.Collections.ArrayList customers2 = new System.Collections.ArrayList();
        private MyVirtualClass customerInEdit2;
        private int rowInEdit2 = -1;
        private bool rowScopeCommit2 = true;

        private System.Collections.ArrayList customers3 = new System.Collections.ArrayList();
        private MyVirtualClass customerInEdit3;
        private int rowInEdit3 = -1;
        private bool rowScopeCommit3 = true;

        private System.Collections.ArrayList customers4 = new System.Collections.ArrayList();
        private MyVirtualClass customerInEdit4;
        private int rowInEdit4 = -1;
        private bool rowScopeCommit4 = true;

        private System.Collections.ArrayList customers5 = new System.Collections.ArrayList();
        private MyVirtualClass customerInEdit5;
        private int rowInEdit5 = -1;
        private bool rowScopeCommit5 = true;

        private System.Collections.ArrayList customers6 = new System.Collections.ArrayList();
        private MyVirtualClass customerInEdit6;
        private int rowInEdit6 = -1;
        private bool rowScopeCommit6 = true;

        public Form1()
        {
            InitializeComponent();

            chart1.Series["Series1"].Color = Color.Red;
            chart1.Series["Series2"].Color = Color.Blue;
            chart1.Series["Series3"].Color = Color.Lime;
            chart1.Series["Series4"].Color = Color.Aqua;

            chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.DarkGray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.DarkGray;

            chart1.ChartAreas[0].AxisX.MinorTickMark.Enabled = true;
            chart1.ChartAreas[0].AxisX.MinorTickMark.LineDashStyle = ChartDashStyle.Dash;
            chart1.ChartAreas[0].AxisX.MinorTickMark.LineColor = Color.DarkGray;

            chart1.ChartAreas[0].AxisY.MinorTickMark.Enabled = true;
            chart1.ChartAreas[0].AxisY.MinorTickMark.LineDashStyle = ChartDashStyle.Dash;
            chart1.ChartAreas[0].AxisY.MinorTickMark.LineColor = Color.DarkGray;

            chart1.ChartAreas[0].AxisY2.MinorTickMark.Enabled = true;
            chart1.ChartAreas[0].AxisY2.MinorTickMark.LineDashStyle = ChartDashStyle.Dash;
            chart1.ChartAreas[0].AxisY2.MinorTickMark.LineColor = Color.DarkGray;

            chart1.Legends["Legend1"].BorderColor = Color.Black;

        }

        private MyListOfSensors MyAllSensors = new MyListOfSensors();

      
        private void checkedListBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                listBox1.ContextMenuStrip = contextMenuStrip1;
                listBox1.SelectedIndex = listBox1.IndexFromPoint(e.X, e.Y);
            }
        }

        private bool flagAxis = true;
        private List<double> _timeList = new List<double>();
        private List<int> _indexList = new List<int>();

        public double Min;
        public double Max;

        private void добавитьНаОсьXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flagAxis = true;
            Graph.CreateLine(MyAllSensors, listBox1.Text, chart1, flagAxis);
            Min = chart1.ChartAreas[0].AxisY.Minimum;
            Max = chart1.ChartAreas[0].AxisY.Maximum;

            добавитьНаДополнительнуюОсьYОтВремениToolStripMenuItem.Enabled = true;
            _timeList.Clear();
            _indexList.Clear();
            for (int i = 0; i < MyAllSensors[0].MyListRecordsForOneKKS.Count; i++)
            {
                _timeList.Add(MyAllSensors[0].MyListRecordsForOneKKS[i].ValueTimeForDAT);
                _indexList.Add(i);
            }
            button1.Enabled = true;
            button5.Enabled = true;
        }

        private double prosent;

        private void Form1_Load(object sender, EventArgs e)
        {
            numericUpDown7.Value = (decimal)0.0127;
            numericUpDown8.Value = (decimal)0.0317;
            numericUpDown9.Value = (decimal)0.118;
            numericUpDown10.Value = (decimal)0.317;
            numericUpDown11.Value = (decimal)1.4;
            numericUpDown12.Value = (decimal)3.92;

            numericUpDown13.Value = (decimal)0.034;
            numericUpDown14.Value = (decimal)0.201;
            numericUpDown15.Value = (decimal)0.184;
            numericUpDown16.Value = (decimal)0.4040;
            numericUpDown17.Value = (decimal)0.143;
            numericUpDown18.Value = (decimal)0.034;

            numericUpDown19.Value = (decimal)0.72;



            tabPage6.Text = "Константы";
            // tabPage8.vis
            checkBox3.Checked = true;
            button1.Enabled = false;
            button5.Enabled = false;
         //   button10.Enabled = false;
            comboBox2.Text = "Выберите параметр тока, А";
            comboBox1.Text = "Выберите параметр реактивности, bэфф";
            comboBox3.Text = "Выберите параметр мощности, %";
            comboBox5.Text = "Выберите параметр положения группы, см";
            comboBox4.Text = "Выберите параметр температуры, \u2103";

            button2.BackColor = Color.White;
            button4.BackColor = Color.White;
            button9.BackColor = Color.White;
            button19.BackColor = Color.White;


            добавитьНаОсьXToolStripMenuItem.Enabled = false;
            добавитьНаДополнительнуюОсьYОтВремениToolStripMenuItem.Enabled = false;
            button6.Enabled = false;
            comboBox6.Text = 0.ToString();



            dataGridView5.Columns.Add("Время", "Время");
            dataGridView5.Columns.Add("Jотн", "Jотн");
            dataGridView5.Columns.Add("Jкор", "Jкор");
            dataGridView5.Columns.Add("Rэкс", "Rэкс");
            dataGridView5.Columns.Add("Rрасч", "Rрасч");
            dataGridView5.Columns.Add("dH12", "dH12");
            dataGridView5.Columns.Add("F", "F");

            tabPage5.Text = "Все данные";
           // tabPage6.Text = "Данные для поиска ПЭ и J(отн)";
            tabPage7.Text = "Расчет";
      //      tabPage8.Text = "ПЭ";
            tabPage9.Text = "Данные из расчета dp/dH";

            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.Size = 20;
            chart1.ChartAreas[0].AxisY.ScrollBar.Size = 20;
            chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorY.LineColor = Color.Blue;
            chart1.ChartAreas[0].CursorX.LineColor = Color.Blue;
            chart1.ChartAreas[0].CursorY.LineWidth = 2;
            chart1.ChartAreas[0].CursorX.LineWidth = 2;
            chart1.ChartAreas[0].CursorY.LineDashStyle = ChartDashStyle.Dash;
            chart1.ChartAreas[0].CursorX.LineDashStyle = ChartDashStyle.Dash;
            chart1.ChartAreas[0].CursorY.Interval = 0.0000001;
            chart1.ChartAreas[0].CursorX.Interval = 0.0000001;
            chart1.ChartAreas[0].CursorY.SelectionColor = Color.Blue;


            button8.Enabled = false;

            textBox3.BackColor = Color.Red;
            textBox4.BackColor = Color.Red;
            tabPage1.Text = "Зона обработки";
            tabPage2.Text = "Результаты";
            button6.Visible = true;
            button6.BackColor = Color.Yellow;
            button7.Visible = false;
            tabPage3.Text = "Параметры";
            tabPage4.Text = "Данные";
            // button6.Enabled = false;
            chart1.ChartAreas[0].BackColor = Color.Gainsboro;

            for (int i = 0; i < chart1.Series.Count; i++)
                chart1.Series[i].IsVisibleInLegend = false;

            chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Times New Roman", 14, FontStyle.Regular);
            chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Times New Roman", 14, FontStyle.Regular);

            dataGridView2.Columns.Add("a(H), %/см", "a(H), %/см");
            //       dataGridView2.Columns.Add("По пичкам \u03B2/см", "По пичкам \u03B2/см");
            dataGridView2.Columns.Add("TAU", "TAU");
            dataGridView2.Columns.Add("b", "b");
            dataGridView2.Columns.Add("R0, %", "R0, %");
            dataGridView2.Columns.Add("N, МВт", "N, МВт");
            dataGridView2.Columns.Add("a(N) %/МВт", "a(N) %/МВт");
            dataGridView2.Columns.Add("H12, %", "Н12, %");
            dataGridView2.Columns.Add("ПЭ", "ПЭ");

            this.dataGridView1.VirtualMode = true;
            this.dataGridView4.VirtualMode = true;

            DataGridViewTextBoxColumn companyNameColumn = new DataGridViewTextBoxColumn();
            companyNameColumn.HeaderText = "Время";
            companyNameColumn.Name = "Время";
            this.dataGridView1.Columns.Add(companyNameColumn);

            DataGridViewTextBoxColumn companyNameColumn1 = new DataGridViewTextBoxColumn();
            companyNameColumn1.HeaderText = "Значение";
            companyNameColumn1.Name = "Значение";
            this.dataGridView1.Columns.Add(companyNameColumn1);


            tableLayoutPanel10.ColumnStyles[3].Width = 0;
            tableLayoutPanel10.ColumnStyles[2].Width = 2F;

            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[1].Width = 200;

            // label24.Text = "T, \u2103";
            // label1.Text = "\u03C1, %";
        }

        private void dataGridView1_CellValueNeeded(object sender,
            System.Windows.Forms.DataGridViewCellValueEventArgs e)
        {
            //this.dataGridView1.RowCount = 1;
            // If this is the row for new records, no values are needed.
            if (e.RowIndex == this.dataGridView1.RowCount - 1) return;
            //   if (e.RowIndex == this.dataGridView3.RowCount - 1) return;

            MyVirtualClass customerTmp = null;

            // Store a reference to the Customer object for the row being painted.
            if (e.RowIndex == rowInEdit)
            {
                customerTmp = this.customerInEdit;
            }
            else
            {
                customerTmp = (MyVirtualClass) this.customers[e.RowIndex];
            }

            // Set the cell value to paint using the Customer object retrieved.
            switch (this.dataGridView1.Columns[e.ColumnIndex].Name)
            {
                case "Время":
                    e.Value = customerTmp.time;
                    break;

                case "Значение":
                    e.Value = customerTmp.value;
                    break;
            }
        }

        private void dataGridView4_CellValueNeeded(object sender,
            System.Windows.Forms.DataGridViewCellValueEventArgs e)
        {
            //this.dataGridView1.RowCount = 1;
            // If this is the row for new records, no values are needed.
            if (e.RowIndex == this.dataGridView4.RowCount - 1) return;
            //   if (e.RowIndex == this.dataGridView3.RowCount - 1) return;

            MyVirtualClass customerTmp = null;

            // Store a reference to the Customer object for the row being painted.
            if (e.RowIndex == rowInEdit1)
            {
                customerTmp = this.customerInEdit1;
            }
            else
            {
                customerTmp = (MyVirtualClass) this.customers1[e.RowIndex];
            }

            // Set the cell value to paint using the Customer object retrieved.
            switch (this.dataGridView4.Columns[e.ColumnIndex].Name)
            {
                case "Время":
                    e.Value = customerTmp.value;
                    break;
            }
        }



        private void dataGridView41_CellValueNeeded(object sender,
            System.Windows.Forms.DataGridViewCellValueEventArgs e)
        {
            //this.dataGridView1.RowCount = 1;
            // If this is the row for new records, no values are needed.
            if (e.RowIndex == this.dataGridView4.RowCount - 1) return;
            //   if (e.RowIndex == this.dataGridView3.RowCount - 1) return;

            MyVirtualClass customerTmp = null;

            // Store a reference to the Customer object for the row being painted.
            if (e.RowIndex == rowInEdit2)
            {
                customerTmp = this.customerInEdit2;
            }
            else
            {
                customerTmp = (MyVirtualClass) this.customers2[e.RowIndex];
            }

            // Set the cell value to paint using the Customer object retrieved.
            switch (this.dataGridView4.Columns[e.ColumnIndex].Name)
            {
                case "Ток(эксперимент), А":
                    e.Value = customerTmp.value;
                    break;
            }
        }

        private void dataGridView42_CellValueNeeded(object sender,
            System.Windows.Forms.DataGridViewCellValueEventArgs e)
        {
            //this.dataGridView1.RowCount = 1;
            // If this is the row for new records, no values are needed.
            if (e.RowIndex == this.dataGridView4.RowCount - 1) return;
            //   if (e.RowIndex == this.dataGridView3.RowCount - 1) return;

            MyVirtualClass customerTmp = null;

            // Store a reference to the Customer object for the row being painted.
            if (e.RowIndex == rowInEdit3)
            {
                customerTmp = this.customerInEdit3;
            }
            else
            {
                customerTmp = (MyVirtualClass) this.customers3[e.RowIndex];
            }

            // Set the cell value to paint using the Customer object retrieved.
            switch (this.dataGridView4.Columns[e.ColumnIndex].Name)
            {
                case "\u03C1, %":
                    e.Value = customerTmp.value;
                    break;
            }
        }

        private void dataGridView43_CellValueNeeded(object sender,
            System.Windows.Forms.DataGridViewCellValueEventArgs e)
        {
            //this.dataGridView1.RowCount = 1;
            // If this is the row for new records, no values are needed.
            if (e.RowIndex == this.dataGridView4.RowCount - 1) return;
            //   if (e.RowIndex == this.dataGridView3.RowCount - 1) return;

            MyVirtualClass customerTmp = null;

            // Store a reference to the Customer object for the row being painted.
            if (e.RowIndex == rowInEdit4)
            {
                customerTmp = this.customerInEdit4;
            }
            else
            {
                customerTmp = (MyVirtualClass) this.customers4[e.RowIndex];
            }

            // Set the cell value to paint using the Customer object retrieved.
            switch (this.dataGridView4.Columns[e.ColumnIndex].Name)
            {
                case "N, МВт":
                    e.Value = customerTmp.value;
                    break;
            }
        }

        private void dataGridView44_CellValueNeeded(object sender,
            System.Windows.Forms.DataGridViewCellValueEventArgs e)
        {
            //this.dataGridView1.RowCount = 1;
            // If this is the row for new records, no values are needed.
            if (e.RowIndex == this.dataGridView4.RowCount - 1) return;
            //   if (e.RowIndex == this.dataGridView3.RowCount - 1) return;

            MyVirtualClass customerTmp = null;

            // Store a reference to the Customer object for the row being painted.
            if (e.RowIndex == rowInEdit5)
            {
                customerTmp = this.customerInEdit5;
            }
            else
            {
                customerTmp = (MyVirtualClass) this.customers5[e.RowIndex];
            }

            // Set the cell value to paint using the Customer object retrieved.
            switch (this.dataGridView4.Columns[e.ColumnIndex].Name)
            {
                case "H12, см":
                    e.Value = customerTmp.value;
                    break;
            }
        }

        private void dataGridView45_CellValueNeeded(object sender,
            System.Windows.Forms.DataGridViewCellValueEventArgs e)
        {
            //this.dataGridView1.RowCount = 1;
            // If this is the row for new records, no values are needed.
            if (e.RowIndex == this.dataGridView4.RowCount - 1) return;
            //   if (e.RowIndex == this.dataGridView3.RowCount - 1) return;

            MyVirtualClass customerTmp = null;

            // Store a reference to the Customer object for the row being painted.
            if (e.RowIndex == rowInEdit6)
            {
                customerTmp = this.customerInEdit6;
            }
            else
            {
                customerTmp = (MyVirtualClass) this.customers6[e.RowIndex];
            }

            // Set the cell value to paint using the Customer object retrieved.
            switch (this.dataGridView4.Columns[e.ColumnIndex].Name)
            {
                case "T, \u2103":
                    e.Value = customerTmp.value;
                    break;
            }
        }



        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                chart1.SaveImage(saveFileDialog1.FileName + ".TIFF", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

      

        public int NumberPoints;
       private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                button7.Enabled = true;
                button6.Enabled = true;
            //    button10.Enabled = true;
                indexPositionCursor--;
                chart1.ChartAreas[0].CursorX.Position =
                    MyAllSensors[0].MyListRecordsForOneKKS[indexPositionCursor].ValueTimeForDAT;
                textBox3.Text = MyAllSensors[0].MyListRecordsForOneKKS[indexPositionCursor].DateTime.ToString();
                textBox4.Text = chart1.ChartAreas[0].CursorX.Position.ToString();
                for (int j = 0; j < MyAllSensors.Count; j++)
                {
                    if (comboBox1.Text == MyAllSensors[j].KKS_Name)
                    {
                        chart1.Series[19].ChartType = SeriesChartType.Point;
                        chart1.Series[19].Color = Color.Black;
                        chart1.Series[19].Points.Clear();
                        DataPoint dp = new DataPoint(chart1.ChartAreas[0].CursorX.Position,
                            MyAllSensors[j].MyListRecordsForOneKKS[indexPositionCursor].Value);
                        dp.MarkerStyle = MarkerStyle.Cross;
                        dp.MarkerSize = 10;
                        dp.IsValueShownAsLabel = true;
                        chart1.Series[19].Points.Add(dp);
                    }
                }
            }
            catch (Exception ex)
            {
                
            }

        }
        private double position;

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {


              //  button10.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
                indexPositionCursor++;
                chart1.ChartAreas[0].CursorX.Position =
                    MyAllSensors[0].MyListRecordsForOneKKS[indexPositionCursor].ValueTimeForDAT;
                textBox3.Text = MyAllSensors[0].MyListRecordsForOneKKS[indexPositionCursor].DateTime.ToString();
                textBox4.Text = chart1.ChartAreas[0].CursorX.Position.ToString();
                for (int j = 0; j < MyAllSensors.Count; j++)
                {
                    if (comboBox1.Text == MyAllSensors[j].KKS_Name)
                    {
                        chart1.Series[19].ChartType = SeriesChartType.Point;
                        chart1.Series[19].Color = Color.Black;
                        chart1.Series[19].Points.Clear();
                        DataPoint dp = new DataPoint(chart1.ChartAreas[0].CursorX.Position,
                            MyAllSensors[j].MyListRecordsForOneKKS[indexPositionCursor].Value);
                        dp.MarkerStyle = MarkerStyle.Cross;
                        dp.MarkerSize = 10;
                        dp.IsValueShownAsLabel = true;
                        chart1.Series[19].Points.Add(dp);
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

      

        private double indexPoint;
        private int index;

      

        public Chart MaxAxisY()
        {
            return chart1;
        }

        private int indexPositionCursor;

        private void chart1_MouseDown_1(object sender, MouseEventArgs e)
        {
            try
            {

                button6.Enabled = false;
                button7.Enabled = false;
                if (e.Button == MouseButtons.Right)
                {
                    button6.Enabled = true;
                    chart1.ContextMenuStrip = contextMenuStrip2;
                }
                if (e.Button == MouseButtons.Left)
                {
                    if (e.Y < 450 && e.Y > 0 && e.X > 0 && e.X < 100)
                    {
                        if (Graph._numberseries > 0)
                        {
                            MinMaxX ChangeParametrImage = new MinMaxX();
                            ChangeParametrImage.Owner = this;
                            ChangeParametrImage.Show();
                            ChangeParametrImage.Location = e.Location;

                            ChangeParametrImage.textBox1.Text = chart1.ChartAreas[0].AxisY.Maximum.ToString();
                            ChangeParametrImage.textBox2.Text = chart1.ChartAreas[0].AxisY.Minimum.ToString();
                        }
                    }
                  
                    try
                    {
                        position = chart1.ChartAreas[0].CursorX.Position;

                        List<double> dTDoubles = new List<double>();
                        dTDoubles.Clear();
                        for (int i = 0; i < _timeList.Count; i++)
                        {
                            dTDoubles.Add(Math.Abs(_timeList[i] - position));
                        }
                        indexPositionCursor = dTDoubles.IndexOf(dTDoubles.Min());
                    }
                    catch
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                
            }

        }

        private bool ter = false;
        private void button3_Click_2(object sender, EventArgs e)
        {
            if (ter)
            {
                tableLayoutPanel10.ColumnStyles[3].Width = 0;
                tableLayoutPanel10.ColumnStyles[2].Width = 2F;
                ter = false;
            }

            else
            {
                tableLayoutPanel10.ColumnStyles[3].Width = 86F;
                tableLayoutPanel10.ColumnStyles[2].Width = 3F;
                ter = true;
                double timestamp1 = chart1.ChartAreas[0].AxisX.Minimum;
                DateTime dateBeg = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(timestamp1);
                double timestamp2 = chart1.ChartAreas[0].AxisX.Maximum;
                DateTime dateEnd = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(timestamp2);
                dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = "dd.MM.yy HH:mm:ss";
                dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                dateTimePicker2.CustomFormat = "dd.MM.yy HH:mm:ss";
                dateTimePicker1.Value = DateTime.FromOADate(dateBeg.ToOADate());
                dateTimePicker2.Value = DateTime.FromOADate(dateEnd.ToOADate());
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(textBox1.Text) < chart1.ChartAreas[0].AxisY.Maximum)
                {
                    chart1.ChartAreas[0].AxisY.Minimum = double.Parse(textBox1.Text);
                }

            }
            catch
            {

            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(textBox5.Text) >= chart1.ChartAreas[0].AxisY.Minimum)
                {
                    chart1.ChartAreas[0].AxisY.Maximum = double.Parse(textBox5.Text);
                }
            }
            catch
            {

            }
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(textBox12.Text) < chart1.ChartAreas[0].AxisY2.Maximum)
                {
                    chart1.ChartAreas[0].AxisY2.Minimum = double.Parse(textBox12.Text);
                }

            }
            catch
            {

            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(textBox11.Text) >= chart1.ChartAreas[0].AxisY2.Minimum)
                {
                    chart1.ChartAreas[0].AxisY2.Maximum = double.Parse(textBox11.Text);
                }
            }
            catch
            {

            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                chart1.ChartAreas[0].AxisY.Interval = double.Parse(textBox6.Text);
            }
            catch
            {

            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            try
            {
                chart1.ChartAreas[0].AxisY2.Interval = double.Parse(textBox10.Text);
            }
            catch
            {

            }
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                chart1.ChartAreas[0].AxisY.LabelAutoFitMinFontSize = (int) numericUpDown4.Value;
            }
            catch
            {

            }
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                chart1.ChartAreas[0].AxisY2.LabelAutoFitMinFontSize = (int) numericUpDown6.Value;
            }
            catch
            {

            }
        }

        private List<int> indexPositionCursorList = new List<int>();

        private void button6_Click(object sender, EventArgs e)
        {
            indexPositionCursorList.Add(indexPositionCursor);
            for (int i = 0; i < MyAllSensors.Count; i++)
            {
                if (comboBox3.Text == MyAllSensors[i].KKS_Name)
                {
                    N = MyAllSensors[i].MyListRecordsForOneKKS[indexPositionCursorList[0]].Value;
                }

                if (comboBox5.Text == MyAllSensors[i].KKS_Name)
                {
                    H12 = MyAllSensors[i].MyListRecordsForOneKKS[indexPositionCursorList[0]].Value;
                }

            }
                button6.Visible = false;
                button7.Visible = true;
                button7.Enabled = false;
                button7.BackColor = Color.Yellow;

                for (int j = 0; j < MyAllSensors.Count; j++)
                {
                    if (comboBox1.Text == MyAllSensors[j].KKS_Name)
                    {
                        chart1.Series[18].ChartType = SeriesChartType.Point;
                        chart1.Series[18].Color = Color.Blue;
                        DataPoint dp = new DataPoint(chart1.ChartAreas[0].CursorX.Position,
                            MyAllSensors[j].MyListRecordsForOneKKS[indexPositionCursor].Value);
                        dp.MarkerStyle = MarkerStyle.Cross;
                        dp.MarkerSize = 11;
                        dp.IsValueShownAsLabel = true;
                        chart1.Series[18].Points.Add(dp);
                    }
                }
         //   indexPositionCursor = indexPositionCursor + 15;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            indexPositionCursorList.Add(indexPositionCursor);
   //      indexPositionCursor =
            button6.Visible = true;
            button7.Visible = false;
            button6.Enabled = false;
            button6.BackColor = Color.Yellow;
            button8.Enabled = true;

             for (int j = 0; j < MyAllSensors.Count; j++)
                {
                    if (comboBox1.Text == MyAllSensors[j].KKS_Name)
                    {
                        chart1.Series[18].ChartType = SeriesChartType.Point;
                        chart1.Series[18].Color = Color.Blue;
                        DataPoint dp = new DataPoint(chart1.ChartAreas[0].CursorX.Position,
                            MyAllSensors[j].MyListRecordsForOneKKS[indexPositionCursor].Value);
                        dp.MarkerStyle = MarkerStyle.Cross;
                        dp.MarkerSize = 11;
                        dp.IsValueShownAsLabel = true;
                        chart1.Series[18].Points.Add(dp);
                    }
                }
             indexPositionCursor = indexPositionCursor + 15;
        }
   
        double DroAver = 0;

        private double PoPichkam()
        {
            for (int i = 0; i < MyAllSensors.Count; i++)
            {
                if (comboBox1.Text == MyAllSensors[i].KKS_Name)
                {
                    for (int j = 1;
                        j < indexPositionCursorList.Count;
                        j++)
                    {
                        if (j%2 != 0)
                        {
                            DroAver = DroAver +
                                      (MyAllSensors[i].MyListRecordsForOneKKS[indexPositionCursorList[j]].Value -
                                       MyAllSensors[i].MyListRecordsForOneKKS[indexPositionCursorList[j - 1]].Value);
                        }
                    }

                    DroAver = DroAver/(indexPositionCursorList.Count/2);


                    DroAver = DroAver/-2;

                    //TODO: ПЕРЕВОД В ПРОЦЕНТЫ ЗАЧЕМ?
                    DroAver = DroAver*MyConst._beff;
                  }
            }
            return DroAver;
        }

        static pertubResult tempR;

        private void SearchDiffEffect()
        {
            tempR = new pertubResult();
           
                double Ss = 1000;

                for (int i = 0; i < 400; i++)
                {
                    tempR = Calc(3 + i/200.0, _tList, _jKorList, _rCalcList, _dHList);
                    if (tempR.SS > Ss)
                    {
                        tempR = Calc(3 + (i - 1)/200.0, _tList, _jKorList, _rCalcList, _dHList);
                        break;
                    }
                    Ss = tempR.SS;
                }
                //TODO: 400 РАЗ ОБРАБАТЫВАЕТСЯ ОДНО И ТОЖЕ ВОЗМУЩЕНИЕ С РАЗНЫМ ПОДБОРОМ ТАУ. В МЕТОДЕ CALC ВСЕГДА НАБИРАЕТСЯ СУММА КВАДРАТОВ РАЗНОСТЕЙ РЕАКТВИНОСТЕЙ НАЗВАЕМОЙ НЕВЯЗКОЙ. 
                //TODO: TEMPR.SS В НЕМ ЖЕ ПРИРАВНИВАЕТСЯ ЭТОЙ СУММЕ. В ИТОГЕ ПО УСЛОВИЮ И ПО ЛОГИКЕ НЕВЯЗКА ИЛИ ЖЕ СВОЙСТВО TEMPR.SS СРАВНИВАЕТСЯ C ЕГО ЖЕ ПРЕДЫДУЩЕМ ЗНАЧЕНИЕМ
                //TODO: И КАК ТОЛЬКО ЭТО СВОЙСТВО СТАНОВИТСЯ БОЛЬШЕ ПРЕДЫДУЩЕГО ТО ПРОИСХОДИТ ОТКАТ К ПРЕДЫДУЩЕМУ ЗНАЧЕНИЮ ТАУ И АЛГОРИТМ ПРЕРЫВАЕТСЯ
                //TODO: ВОПРОС: МОЖЕТ ЛИ БЫТЬ ПОСЛЕ СРАБАТЫВАНИЯ ВЕЛИЧИНА НЕВЯЗКИ МЕНЬШЕ ТОГО ЧТО БЫЛО ЕСЛИ АЛГОРИТМ НЕ ПРЕРВЕТСЯ ВЕДЬ ИЗ ФАЙЛА ЗАПИСИ ВИДНО ЧТО НЕТ

            tempR.Ro = tempR.Ro*MyConst._beff;
            tempR.aH = tempR.aH*MyConst._beff;
            tempR.b = tempR.b*MyConst._beff;
            tempR.SS = tempR.SS*MyConst._beff;
        }


        private void button2_Click_2(object sender, EventArgs e)
        {
            MyListOfSensors N = new MyListOfSensors();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (var item in openFileDialog1.FileNames)
                {
                    N.LoadFromFile(item, MyAllSensors);
                }
            }
            if (MyAllSensors.Count > 0)
            {
                добавитьНаОсьXToolStripMenuItem.Enabled = true;
         
               
            }
            dataGridView4.Columns.Clear();
            dataGridView4.Columns.Add("Время", "Время");
            dataGridView4.Columns.Add("Ток(эксперимент), А", "Ток(эксперимент), А");
            dataGridView4.Columns.Add("\u03C1, %", "\u03C1, %");
            dataGridView4.Columns.Add("N, МВт", "N, МВт");
            dataGridView4.Columns.Add("H12, см", "H12, см");
            dataGridView4.Columns.Add("T, \u2103", "T, \u2103");

            //dataGridView3.Columns.Clear();
            //dataGridView3.Columns.Add("Время", "Время");
            //dataGridView3.Columns.Add("Ток(эксперимент), А", "Ток(эксперимент), А");
            //dataGridView3.Columns.Add("\u03C1, %", "\u03C1, %");
            //dataGridView3.Columns.Add("N, МВт", "N, МВт");
            //dataGridView3.Columns.Add("H12, см", "H12, см");
            //dataGridView3.Columns.Add("T, \u2103", "T, \u2103");
            //dataGridView3.Columns.Add("Ток(относительный)", "Ток(относительный)");
            //dataGridView3.Columns.Add("ПЭ", "ПЭ");


            try
            {
                dataGridView4.Visible = false;

                this.dataGridView4.CellValueNeeded += new
                    DataGridViewCellValueEventHandler(dataGridView4_CellValueNeeded);

                dataGridView4.Rows.Clear();
                customers1.Clear();

                for (int i = 0; i < MyAllSensors[0].MyListRecordsForOneKKS.Count; i++)
                {
                    this.customers1.Add(
                        new MyVirtualClass(MyAllSensors[0].MyListRecordsForOneKKS[i].DateTime.ToString("HH:mm:ss.fff")));
                }

                if (this.dataGridView4.RowCount == 0)
                {
                    this.dataGridView4.RowCount = 1;
                }

                this.dataGridView4.RowCount = MyAllSensors[0].MyListRecordsForOneKKS.Count;
                dataGridView4.Visible = true;
            }
            catch
            {
                //  MessageBox.Show(ex0.Message);
            }

            dataGridView4.Columns[indexData4].Width = 100;

            listBox1.Items.Clear();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            foreach (Sensors item in MyAllSensors)
            {
                listBox1.Items.Add(item.KKS_Name);
                comboBox1.Items.Add(item.KKS_Name);
                comboBox2.Items.Add(item.KKS_Name);
                comboBox3.Items.Add(item.KKS_Name);
                comboBox4.Items.Add(item.KKS_Name);
                comboBox5.Items.Add(item.KKS_Name);
            }
        }

        private bool flag = true;

        private void button9_Click_1(object sender, EventArgs e)
        {
            if (flag)
            {
                button9.BackColor = Color.Black;
                chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
                chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
                chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
                chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
                chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
                chart1.ChartAreas[0].CursorY.LineColor = Color.Blue;
                chart1.ChartAreas[0].CursorX.LineColor = Color.Blue;
                chart1.ChartAreas[0].CursorY.LineWidth = 2;
                chart1.ChartAreas[0].CursorX.LineWidth = 2;
                chart1.ChartAreas[0].CursorY.LineDashStyle = ChartDashStyle.Dash;
                chart1.ChartAreas[0].CursorX.LineDashStyle = ChartDashStyle.Dash;

                chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
                chart1.ChartAreas[0].CursorY.Interval = 0.0000001;
                chart1.ChartAreas[0].CursorX.Interval = 0.0000001;
                chart1.ChartAreas[0].CursorY.AutoScroll = true;

                chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;

                chart1.ChartAreas[0].CursorY.SelectionColor = Color.Blue;
                flag = false;
            }
            else
            {
                button9.BackColor = Color.White;
                chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;
                chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = false;
                chart1.ChartAreas[0].CursorY.AutoScroll = false;
                chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = false;
                flag = true;
            }
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            добавитьНаОсьXToolStripMenuItem.Enabled = false;
            добавитьНаДополнительнуюОсьYОтВремениToolStripMenuItem.Enabled = false;
            button1.Enabled = false;
            button5.Enabled = false;
         //   button10.Enabled = false;
            Graph.MinmaxListPrimary.Clear();
            Graph.MinmaxListSecondary.Clear();

            Graph._numberseries = 0;
            chart1.Titles.Clear();
            listBox1.Items.Clear();

            MyAllSensors.Clear();

            chart1.ChartAreas[0].Position.Auto = true;
            dataGridView4.Rows.Clear();
        //    dataGridView3.Rows.Clear();

            for (int i = 0; i < chart1.Series.Count; i++)
            {
                chart1.Series[i].IsVisibleInLegend = false;
                chart1.Series[i].Points.Clear();
            }
        }

        private void добавитьНаДополнительнуюОсьYОтВремениToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flagAxis = false;
            Graph.CreateLine(MyAllSensors, listBox1.Text, chart1, flagAxis);
            button6.Enabled = true;
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (Graph._numberseries > 0)
                {
                    textBox2.Text = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.X).ToString();
                    if (checkBox1.Checked == false)
                    {
                        textBox14.Text = chart1.ChartAreas[0].AxisY.PixelPositionToValue(e.Y).ToString();
                    }
                    if (checkBox1.Checked == true)
                    {
                        textBox14.Text = chart1.ChartAreas[0].AxisY2.PixelPositionToValue(e.Y).ToString();
                    }

                  }
            }
            catch (Exception)
            {

            }
        }

        private int indexData4 = 0;

        private string comboBoxText = null;

        public static List<double> _PEList = new List<double>();
        public static List<double> _jExpList = new List<double>();
        public static List<double> _jKorList = new List<double>();
        public static List<double> _jRealList = new List<double>();
        public static List<double> _rExpList = new List<double>();
        public static List<double> _rCalcList = new List<double>();
        public static List<double> _dHList = new List<double>();
        public static List<double> _tList = new List<double>();

        double PE;

        private double N;
        private double H12;

        private void button8_Click_2(object sender, EventArgs e)
        {
            MyConst.LMetodiki[0] = (double)numericUpDown7.Value;
            MyConst.LMetodiki[1] = (double)numericUpDown8.Value;
            MyConst.LMetodiki[2] = (double)numericUpDown9.Value;
            MyConst.LMetodiki[3] = (double)numericUpDown10.Value;
            MyConst.LMetodiki[4] = (double)numericUpDown11.Value;
            MyConst.LMetodiki[5] = (double)numericUpDown12.Value;

            MyConst.AApik[0] = (double)numericUpDown13.Value;
            MyConst.AApik[1] = (double)numericUpDown14.Value;
            MyConst.AApik[2] = (double)numericUpDown15.Value;
            MyConst.AApik[3] = (double)numericUpDown16.Value;
            MyConst.AApik[4] = (double)numericUpDown17.Value;
            MyConst.AApik[5] = (double)numericUpDown18.Value;

            MyConst._beff = (double)numericUpDown19.Value;

            bool HFlag = false;
            for (int i = 0; i < MyAllSensors.Count; i++)
            {
                if (comboBox1.Text == MyAllSensors[i].KKS_Name)
                {
                    if (MyAllSensors[i].MyListRecordsForOneKKS[indexPositionCursorList[1]].Value < MyAllSensors[i].MyListRecordsForOneKKS[indexPositionCursorList[0]].Value)
                    {
                        HFlag = false;
                    }
                    if (MyAllSensors[i].MyListRecordsForOneKKS[indexPositionCursorList[1]].Value > MyAllSensors[i].MyListRecordsForOneKKS[indexPositionCursorList[0]].Value)
                    {
                        HFlag = true;
                    }
                }
            }

            PE = double.Parse(comboBox6.Text);
            //TODO: РАСЧЕТ DDH
            double ddH = 0;
            _dHList.Add(ddH);
            int step = 0;
            for (int i = 0; i < 4000; i++)
            {
                _dHList.Add(ddH);
            }

            if (HFlag==false)
            {

                for (int j = 1; j < indexPositionCursorList.Count; j++)
                {
                    if (j%2 != 0)
                    {
                        for (int i = indexPositionCursorList[j - 1];
                            i < indexPositionCursorList[j];
                            i++)
                        {
                            ddH -= (2/
                                    (MyAllSensors[0].MyListRecordsForOneKKS[indexPositionCursorList[j]].ValueTimeForDAT -
                                     MyAllSensors[0].MyListRecordsForOneKKS[indexPositionCursorList[j - 1]]
                                         .ValueTimeForDAT))*
                                   (MyAllSensors[0].MyListRecordsForOneKKS[i + 1].ValueTimeForDAT -
                                    MyAllSensors[0].MyListRecordsForOneKKS[i].ValueTimeForDAT);
                            _dHList.Add(ddH);
                        }
                    }
                    if (j%2 == 0)
                    {
                        for (int i = indexPositionCursorList[j - 1];
                            i < indexPositionCursorList[j];
                            i++)
                        {
                            _dHList.Add(ddH);
                        }
                    }
                }

                for (int i = 0; i < 200; i++)
                {
                    _dHList.Add(ddH);
                }
            }
            if (HFlag)
            {

                for (int j = 1; j < indexPositionCursorList.Count; j++)
                {
                    if (j % 2 != 0)
                    {
                        for (int i = indexPositionCursorList[j - 1];
                            i < indexPositionCursorList[j];
                            i++)
                        {
                            ddH += (2 /
                                    (MyAllSensors[0].MyListRecordsForOneKKS[indexPositionCursorList[j]].ValueTimeForDAT -
                                     MyAllSensors[0].MyListRecordsForOneKKS[indexPositionCursorList[j - 1]]
                                         .ValueTimeForDAT)) *
                                   (MyAllSensors[0].MyListRecordsForOneKKS[i + 1].ValueTimeForDAT -
                                    MyAllSensors[0].MyListRecordsForOneKKS[i].ValueTimeForDAT);
                            _dHList.Add(ddH);
                        }
                    }
                    if (j % 2 == 0)
                    {
                        for (int i = indexPositionCursorList[j - 1];
                            i < indexPositionCursorList[j];
                            i++)
                        {
                            _dHList.Add(ddH);
                        }
                    }
                }

                for (int i = 0; i < 200; i++)
                {
                    _dHList.Add(ddH);
                }
            }
            int indexDDH = 0;

            //TODO: ДОБАВЛЕНИЕ ЗНАЧЕНИЙ ТОКА И ВРЕМЕНИ ПО ОТМЕЧЕННЫМ ТОЧКАМ
            for (int i = 0; i < MyAllSensors.Count; i++)
            {
                if (comboBox2.Text == MyAllSensors[i].KKS_Name)
                {
                    for (int j = indexPositionCursorList[0] - 4000;
                        j < indexPositionCursorList[indexPositionCursorList.Count - 1] + 201;
                        j++)
                    {
                        double Inext = MyAllSensors[i].MyListRecordsForOneKKS[j].Value/
                                       MyAllSensors[i].MyListRecordsForOneKKS[indexPositionCursorList[0]].Value;
                        if (_jExpList.Count > 0 && indexDDH > 0)
                        {
                            _jKorList.Add(Inext - (PE*(_dHList[indexDDH])));
                        }
                        if (_jExpList.Count == 0)
                        {
                            _jKorList.Add(Inext);
                        }
                        _jExpList.Add(Inext);
                        _jRealList.Add(MyAllSensors[i].MyListRecordsForOneKKS[j].Value);
                        _tList.Add(MyAllSensors[i].MyListRecordsForOneKKS[j].ValueTimeForDAT);
                        indexDDH++;
                    }
                }

                if (comboBox1.Text == MyAllSensors[i].KKS_Name)
                {
                    for (int j = indexPositionCursorList[0] - 4000;
                        j < indexPositionCursorList[indexPositionCursorList.Count - 1] + 201;
                        j++)
                    {
                        _rExpList.Add(MyAllSensors[i].MyListRecordsForOneKKS[j].Value);
                    }
                }
            }

            Current Y = new Current();
            Y.AddData(_jKorList, _rExpList, _tList, _rCalcList);

            int y = 0;
            for (int i = 0; i < 4000; i++)
            {
                _dHList.RemoveAt(y);
                _jKorList.RemoveAt(y);
                _rCalcList.RemoveAt(y);
                _tList.RemoveAt(y);
                _rExpList.RemoveAt(y);
                _jExpList.RemoveAt(y);

            }

   
            SearchDiffEffect();
  
            Series RCalc = new Series();
            RCalc.Name = "Rcorr" + Graph._numberseries;
            RCalc.ChartType = SeriesChartType.FastLine;
            RCalc.Points.Clear();
            for (int i = 0; i < _tList.Count; i++)
            {
                RCalc.Points.AddXY(
                    _tList[i],
                    _rCalcList[i]);
            }

            RCalc.BorderWidth = 3;
            chart1.Series.Add(RCalc);
            Graph._numberseries++;


            Series dH = new Series();
            dH.Name = "dH" + Graph._numberseries;
            dH.ChartType = SeriesChartType.Line;
            dH.Points.Clear();
            for (int i = 0; i < _tList.Count; i++)
            {
                dH.Points.AddXY(
                    _tList[i],
                    _dHList[i]);
            }

            dH.YAxisType = AxisType.Secondary;
            dH.BorderWidth = 3;
            chart1.Series.Add(dH);
            Graph._numberseries++;

            MessageBox.Show(_rmodelList.Count.ToString());
            Series Rmod = new Series();
            Rmod.Name = "Rmodel" + Graph._numberseries;
            Rmod.ChartType = SeriesChartType.Line;
            Rmod.Points.Clear();
            for (int i = 1; i < _tList.Count; i++)
            {
                Rmod.Points.AddXY(
                    _tList[i],
                    _rmodelList[i - 1]);
            }

            Rmod.BorderWidth = 3;

            chart1.Series.Add(Rmod);
            Graph._numberseries++;


            for (int i = 0; i < _jExpList.Count; i++)
            {
                dataGridView5.Rows.Add(_tList[i], _jExpList[i], _jKorList[i], _rExpList[i], _rCalcList[i], _dHList[i],
                    tempR.FF[i]);
                dataGridView5.Rows[i].DefaultCellStyle.BackColor = Color.NavajoWhite;
            }

            dataGridView2.Rows.Add(tempR.aH, tempR.tau, tempR.b, tempR.Ro, N, tempR.b/N, H12, PE);

           

            indexPositionCursorList.Clear();
            _jKorList.Clear();
            _tList.Clear();
            tempR.FF.Clear();
            _rExpList.Clear();
            _jExpList.Clear();
            _rCalcList.Clear();
            _dHList.Clear();
            button8.Enabled = false;
        }

        private int _indexRow = 0;

        private void button10_Click(object sender, EventArgs e)
        {
            //List<double> myValue = new List<double>();
            //myValue.Clear();
            //myValue.Add(MyAllSensors[0].MyListRecordsForOneKKS[indexPositionCursor].ValueTimeForDAT);
            //for (int i = 0; i < MyAllSensors.Count; i++)
            //{
            //    if (comboBox2.Text == MyAllSensors[i].KKS_Name)
            //    {
            //        myValue.Add(MyAllSensors[i].MyListRecordsForOneKKS[indexPositionCursor].Value);
            //        break;
            //    }
            //}
            //for (int i = 0; i < MyAllSensors.Count; i++)
            //{
            //    if (comboBox1.Text == MyAllSensors[i].KKS_Name)
            //    {
            //        myValue.Add(MyAllSensors[i].MyListRecordsForOneKKS[indexPositionCursor].Value);
            //        break;
            //    }
            //}
            //for (int i = 0; i < MyAllSensors.Count; i++)
            //{
            //    if (comboBox3.Text == MyAllSensors[i].KKS_Name)
            //    {
            //        myValue.Add(MyAllSensors[i].MyListRecordsForOneKKS[indexPositionCursor].Value);
            //        break;
            //    }
            //}
            //for (int i = 0; i < MyAllSensors.Count; i++)
            //{
            //    if (comboBox5.Text == MyAllSensors[i].KKS_Name)
            //    {
            //        myValue.Add(MyAllSensors[i].MyListRecordsForOneKKS[indexPositionCursor].Value);
            //        break;
            //    }
            //}
            //for (int i = 0; i < MyAllSensors.Count; i++)
            //{
            //    if (comboBox4.Text == MyAllSensors[i].KKS_Name)
            //    {
            //        myValue.Add(MyAllSensors[i].MyListRecordsForOneKKS[indexPositionCursor].Value);
            //        break;
            //    }
            //}
            //dataGridView3.Rows.Add();
            //for (int i = 0; i < myValue.Count; i++)
            //{
            //    dataGridView3.Rows[_indexRow].Cells[i].Value = myValue[i];

            //}
            //_indexRow++;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //List<double> MyIlist = new List<double>();
            //MyIlist.Clear();
            //for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
            //{
            //    if (checkBox6.Checked == false)
            //    {
            //        MyIlist.Add((double.Parse(dataGridView3.Rows[i].Cells[1].Value.ToString().Trim()) /
            //                     double.Parse(dataGridView3.Rows[0].Cells[1].Value.ToString().Trim())) *
            //                    (double.Parse(dataGridView3.Rows[0].Cells[3].Value.ToString().Trim()) /
            //                     double.Parse(dataGridView3.Rows[i].Cells[3].Value.ToString().Trim())) *
            //                    (1 +
            //                     0.01 *
            //                     (double.Parse(dataGridView3.Rows[0].Cells[5].Value.ToString().Trim()) -
            //                      double.Parse(dataGridView3.Rows[i].Cells[5].Value.ToString().Trim()))));
            //    }
            //    if (checkBox6.Checked == true)
            //    {
            //        MyIlist.Add((double.Parse(dataGridView3.Rows[i].Cells[1].Value.ToString().Trim()) /
            //                     double.Parse(dataGridView3.Rows[0].Cells[1].Value.ToString().Trim())) *
            //                    ((double.Parse(dataGridView3.Rows[0].Cells[3].Value.ToString().Trim()) * 32) /
            //                     (double.Parse(dataGridView3.Rows[i].Cells[3].Value.ToString().Trim()) * 32)) *
            //                    (1 +
            //                     0.01 *
            //                     (double.Parse(dataGridView3.Rows[0].Cells[5].Value.ToString().Trim()) -
            //                      double.Parse(dataGridView3.Rows[i].Cells[5].Value.ToString().Trim()))));
            //    }
            //}
            //for (int i = 0; i < MyIlist.Count; i++)
            //{
            //    dataGridView3.Rows[i].Cells[6].Value = MyIlist[i];
            //}
 
            //_PEList.Clear();
            //for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
            //{
            //    if (checkBox5.Checked == false)
            //    {
            //        if (i > 0)
            //        {
            //            _PEList.Add((double.Parse(dataGridView3.Rows[i].Cells[6].Value.ToString().Trim()) -
            //                         double.Parse(dataGridView3.Rows[i - 1].Cells[6].Value.ToString().Trim()))/
            //                        ((double.Parse(dataGridView3.Rows[i].Cells[4].Value.ToString().Trim())) -
            //                         (double.Parse(dataGridView3.Rows[i - 1].Cells[4].Value.ToString().Trim()))));
            //        }
            //    }
            //    if (checkBox5.Checked == true)
            //    {
            //        if (i > 0)
            //        {
            //            _PEList.Add((double.Parse(dataGridView3.Rows[i].Cells[6].Value.ToString().Trim()) -
            //                         double.Parse(dataGridView3.Rows[i - 1].Cells[6].Value.ToString().Trim()))/
            //                        ((double.Parse(dataGridView3.Rows[i].Cells[4].Value.ToString().Trim())*3.75) -
            //                         (double.Parse(dataGridView3.Rows[i - 1].Cells[4].Value.ToString().Trim()))*3.75));
            //        }
            //    }
            //}

            //for (int i = 0; i < _PEList.Count; i++)
            //{
            //    dataGridView3.Rows[i + 1].Cells[7].Value = _PEList[i];
            //}

            //comboBox6.Items.Clear();
            //for (int i = 0; i < _PEList.Count; i++)
            //{
            //    comboBox6.Items.Add(_PEList[i]);
            //}
        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            dataGridView4.Visible = false;

            this.dataGridView4.CellValueNeeded += new
                DataGridViewCellValueEventHandler(dataGridView41_CellValueNeeded);

            customers2.Clear();

            for (int k = 0; k < MyAllSensors.Count; k++)
            {
                if (comboBox2.Text == MyAllSensors[k].KKS_Name)
                {
                    for (int i = 0; i < MyAllSensors[k].MyListRecordsForOneKKS.Count; i++)
                    {
                        this.customers2.Add(
                            new MyVirtualClass(MyAllSensors[k].MyListRecordsForOneKKS[i].Value.ToString()));
                    }
                }
            }
            if (this.dataGridView4.RowCount == 0)
            {
                this.dataGridView4.RowCount = 1;
            }

            this.dataGridView4.RowCount = MyAllSensors[0].MyListRecordsForOneKKS.Count;
            dataGridView4.Visible = true;

            comboBox2.BackColor = Color.LightBlue;
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            dataGridView4.Visible = false;

            this.dataGridView4.CellValueNeeded += new
                DataGridViewCellValueEventHandler(dataGridView42_CellValueNeeded);

            customers3.Clear();


            for (int k = 0; k < MyAllSensors.Count; k++)
            {
                if (comboBox1.Text == MyAllSensors[k].KKS_Name)
                {
                    for (int i = 0; i < MyAllSensors[k].MyListRecordsForOneKKS.Count; i++)
                    {
                        this.customers3.Add(
                            new MyVirtualClass(MyAllSensors[k].MyListRecordsForOneKKS[i].Value.ToString()));
                    }
                }
            }
            if (this.dataGridView4.RowCount == 0)
            {
                this.dataGridView4.RowCount = 1;
            }

            this.dataGridView4.RowCount = MyAllSensors[0].MyListRecordsForOneKKS.Count;
            dataGridView4.Visible = true;

            comboBox1.BackColor = Color.LightBlue;
        }

        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            dataGridView4.Visible = false;

            this.dataGridView4.CellValueNeeded += new
                DataGridViewCellValueEventHandler(dataGridView43_CellValueNeeded);

            customers4.Clear();


            for (int k = 0; k < MyAllSensors.Count; k++)
            {
                if (comboBox3.Text == MyAllSensors[k].KKS_Name)
                {
                    for (int i = 0; i < MyAllSensors[k].MyListRecordsForOneKKS.Count; i++)
                    {
                        this.customers4.Add(
                            new MyVirtualClass(MyAllSensors[k].MyListRecordsForOneKKS[i].Value.ToString()));
                    }
                }
            }
            if (this.dataGridView4.RowCount == 0)
            {
                this.dataGridView4.RowCount = 1;
            }

            this.dataGridView4.RowCount = MyAllSensors[0].MyListRecordsForOneKKS.Count;
            dataGridView4.Visible = true;

            comboBox3.BackColor = Color.LightBlue;
        }

        private void comboBox5_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            dataGridView4.Visible = false;

            this.dataGridView4.CellValueNeeded += new
                DataGridViewCellValueEventHandler(dataGridView44_CellValueNeeded);

            customers5.Clear();


            for (int k = 0; k < MyAllSensors.Count; k++)
            {
                if (comboBox5.Text == MyAllSensors[k].KKS_Name)
                {
                    for (int i = 0; i < MyAllSensors[k].MyListRecordsForOneKKS.Count; i++)
                    {
                        this.customers5.Add(
                            new MyVirtualClass(MyAllSensors[k].MyListRecordsForOneKKS[i].Value.ToString()));
                    }
                }
            }
            if (this.dataGridView4.RowCount == 0)
            {
                this.dataGridView4.RowCount = 1;
            }

            this.dataGridView4.RowCount = MyAllSensors[0].MyListRecordsForOneKKS.Count;
            dataGridView4.Visible = true;

            comboBox5.BackColor = Color.LightBlue;
        }

        private void comboBox4_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            dataGridView4.Visible = false;

            this.dataGridView4.CellValueNeeded += new
                DataGridViewCellValueEventHandler(dataGridView45_CellValueNeeded);

            customers6.Clear();


            for (int k = 0; k < MyAllSensors.Count; k++)
            {
                if (comboBox4.Text == MyAllSensors[k].KKS_Name)
                {
                    for (int i = 0; i < MyAllSensors[k].MyListRecordsForOneKKS.Count; i++)
                    {
                        this.customers6.Add(
                            new MyVirtualClass(MyAllSensors[k].MyListRecordsForOneKKS[i].Value.ToString()));
                    }
                }
            }
            if (this.dataGridView4.RowCount == 0)
            {
                this.dataGridView4.RowCount = 1;
            }

            this.dataGridView4.RowCount = MyAllSensors[0].MyListRecordsForOneKKS.Count;
            dataGridView4.Visible = true;

            comboBox4.BackColor = Color.LightBlue;
        }


        public struct pertubResult
        {

            public double aH;
            public double b;
            public double Ro;
            public double tau;

            public List<double> FF; //чисто для вывода 

            //величина невязки
            public double SS;
        }

        static List<double> _rmodelList = new List<double>();
        //  private static double Rm;
        public static pertubResult Calc(double Tt, List<double> time, List<double> I, List<double> R, List<double> dH)
        {


            //       RmodelList.Add();
            pertubResult myresult = new pertubResult();
            myresult.FF = new List<double>();
            //тут основной код для вычислений

            myresult.tau = Tt;

            int cnt = 0;

            double F, g, c, dT, sH, sHH, sF, sFF, sR, sFH, sRH, sRF;


            double SA, SB, SC, SD, SE;

            double Rm;
            double sS;


            F = 0;
            sH = 0;
            sHH = 0;
            sF = 0;
            sFF = 0;
            sFH = 0;
            sR = 0;
            sRH = 0;
            sRF = 0;

            myresult.FF.Add(0);

            for (int i = 1; i < time.Count; i++)
            {
                dT = time[i] - time[i - 1];
                if (dT == 0)
                {
                    myresult.FF.Add(0);
                    continue;
                }
                g = Math.Exp(-dT/Tt);
                c = (1 - g)*Tt/dT;
                F = F*g + (I[i] - 1)*(1 - c) + (I[i - 1] - 1)*(c - g);
                //TODO: ЧТО ТАКОЕ FF??????????
                myresult.FF.Add(F);



                sH += dH[i];
                sHH += dH[i]*dH[i];
                sF += F;
                sFF += F*F;
                sR += R[i];
                sFH += F*dH[i];
                sRH += R[i]*dH[i];
                sRF += R[i]*F;
                cnt++;
            }


            //считаем суммы

            SA = sH*sH/cnt/cnt - sHH/cnt;
            SB = sF*sH/cnt/cnt - sFH/cnt;
            SC = sR*sH/cnt/cnt - sRH/cnt;
            SD = sF*sF/cnt/cnt - sFF/cnt;
            SE = sR*sF/cnt/cnt - sRF/cnt;


            myresult.aH = (SC*SD - SB*SE)/(SA*SD - SB*SB);
            myresult.b = (SA*SE - SB*SC)/(SA*SD - SB*SB);
            myresult.Ro = sR/cnt - myresult.aH*sH/cnt - myresult.b*sF/cnt;


            // невязки


            F = 0;
            cnt = 0;
            sS = 0;

            _rmodelList.Clear();
            for (int i = 1; i < time.Count; i++)
            {
                dT = time[i] - time[i - 1];
                if (dT == 0)
                {
                    continue;
                }

                g = Math.Exp(-dT/Tt);
                c = (1 - g)*Tt/dT;
                F = F*g + (I[i] - 1)*(1 - c) + (I[i - 1] - 1)*(c - g);

                Rm = myresult.Ro + myresult.aH*dH[i] + myresult.b*F;

                _rmodelList.Add(Rm);

                sS += (R[i] - Rm)*(R[i] - Rm);

                cnt++;
            } //i
            myresult.SS = sS;


            return myresult;
        }

        private void button14_Click(object sender, EventArgs e)
        {

            if (saveFileDialog4.ShowDialog() == DialogResult.OK)
            {
                StreamWriter MyRecord = new StreamWriter(saveFileDialog4.FileName + ".csv", false,
                    Encoding.Default);
                saveFileDialog4.Filter = "out data (*.csv)|*.csv|All files (*.*)|*.*";
                saveFileDialog4.DefaultExt = "csv";

                foreach (DataGridViewColumn column in dataGridView2.Columns)
                {
                    MyRecord.Write(column.HeaderText + ";");
                }

                MyRecord.WriteLine();

                try
                {
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            //Add the Data rows.
                            MyRecord.Write(cell.Value + ";");
                        }
                        MyRecord.WriteLine();
                        //Add new line.
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //Exporting to CSV.

                MyRecord.Close();
            }
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
     //       StreamReader r = new StreamReader("D:\\GitHub\\Differ\\Файл для заполнения таблицы для расчета ПЭ.txt",  Encoding.Default);
     //       string line = r.ReadLine();
     //int _indexRow1 = 0;
     //     while ((line = r.ReadLine()) != null)
     //      {
     //          dataGridView3.Rows.Add();
     //          for (int i = 0; i < 6; i++)
     //          {
     //              dataGridView3.Rows[_indexRow].Cells[i].Value = line.Split('\t')[i].Trim();

     //         }
     //          _indexRow++;
     //     }

     //       r.Close();  
        }

        private void button16_Click_1(object sender, EventArgs e)
        {
            comboBox2.Text = "J1";
            comboBox1.Text = "R1";
            comboBox3.Text = "N1";
            comboBox5.Text = "H10";
            comboBox4.Text = "Tcold";
        }

  
        private void button19_Click(object sender, EventArgs e)
        {
            //progressBar1.Value = 
          //  progressBar1.Maximum = chart1.Series.Count;
          //  progressBar1.Step = 1;
           // progressBar1.Minimum = 0;
            Graph.MinmaxListPrimary.Clear();
            Graph.MinmaxListSecondary.Clear();
            Graph._numberseries = 0;
            chart1.ChartAreas[0].Position.Auto = true;

            for (int i = 0; i < chart1.Series.Count; i++)
            {
              //  progressBar1.PerformStep();
                chart1.Series[i].IsVisibleInLegend = false;
                chart1.Series[i].Points.Clear();
            }
            button1.Enabled = false;
            button5.Enabled = false;
        }

        private void comboBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ',')
                if (comboBox6.Text.Contains(',') ||
                    comboBox6.Text == String.Empty)
                {
                    e.Handled = true;
                    return;
                }
                else
                {
                    e.Handled = false;
                    return;
                }

            if (e.KeyChar == '-')
                if (comboBox6.Text.Contains('-') ||
                    comboBox6.Text == String.Empty)
                {
                    e.Handled = true;
                    return;
                }
                else
                {
                    e.Handled = false;
                    return;
                }

            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                listBox1.ContextMenuStrip = contextMenuStrip1;
                listBox1.SelectedIndex = listBox1.IndexFromPoint(e.X, e.Y);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Visible = false;
                Sensors myOneKKS = new Sensors();
                MyListOfSensors N = new MyListOfSensors();
                myOneKKS = N.getOneKKSByIndex(listBox1.SelectedIndex, MyAllSensors);

                this.dataGridView1.CellValueNeeded += new
                    DataGridViewCellValueEventHandler(dataGridView1_CellValueNeeded);

                dataGridView1.Rows.Clear();
                customers.Clear();

                for (int i = 0; i < myOneKKS.MyListRecordsForOneKKS.Count; i++)
                {
                    this.customers.Add(
                        new MyVirtualClass(myOneKKS.MyListRecordsForOneKKS[i].DateTime.ToString("HH:mm:ss.fff"),
                            myOneKKS.MyListRecordsForOneKKS[i].Value.ToString()));

                }
                if (this.dataGridView1.RowCount == 0)
                {
                    this.dataGridView1.RowCount = 1;
                }

                this.dataGridView1.RowCount = myOneKKS.MyListRecordsForOneKKS.Count;
                dataGridView1.Visible = true;
            }
            catch
            {
            }
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
          //  dataGridView3.Rows.Clear();
            _indexRow = 0;
            
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == false)
            {
                chart1.Legends[0].Enabled = false; 
            }
            if (checkBox3.Checked == true)
            {
                chart1.Legends[0].Enabled = true;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                chart1.ChartAreas[0].AxisX.MinorTickMark.Enabled = true;
                chart1.ChartAreas[0].AxisX.MinorTickMark.LineDashStyle = ChartDashStyle.Dash;

                chart1.ChartAreas[0].AxisX.MinorGrid.Enabled = true;
                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                chart1.ChartAreas[0].AxisX.MinorGrid.LineDashStyle = ChartDashStyle.Dash;
                chart1.ChartAreas[0].AxisX.MinorGrid.LineColor = Color.Gray;
                chart1.ChartAreas[0].AxisX.MinorTickMark.LineColor = Color.Gray;
            }
            if (checkBox4.Checked == false)
            {
                chart1.ChartAreas[0].AxisX.MinorTickMark.Enabled = false;
               chart1.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                chart1.ChartAreas[0].AxisY.MinorTickMark.Enabled = true;
                chart1.ChartAreas[0].AxisY.MinorTickMark.LineDashStyle = ChartDashStyle.Dash;

                chart1.ChartAreas[0].AxisY.MinorGrid.Enabled = true;
                chart1.ChartAreas[0].AxisY.MinorGrid.LineDashStyle = ChartDashStyle.Dash;
                chart1.ChartAreas[0].AxisY.MinorGrid.LineColor = Color.Gray;
                chart1.ChartAreas[0].AxisY.MinorTickMark.LineColor = Color.Gray;
            }
            if (checkBox2.Checked == false)
            {
                chart1.ChartAreas[0].AxisY.MinorTickMark.Enabled = false;
                chart1.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisX.MinorTickMark.LineDashStyle = ChartDashStyle.Dash;
            chart1.ChartAreas[0].AxisX.MinorGrid.LineDashStyle = ChartDashStyle.Dash;
            chart1.ChartAreas[0].AxisX.MinorGrid.LineColor = Color.Gray;
            chart1.ChartAreas[0].AxisX.MinorTickMark.LineColor = Color.Gray;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
             chart1.ChartAreas[0].AxisX.MinorTickMark.LineDashStyle = ChartDashStyle.Dot;
             chart1.ChartAreas[0].AxisX.MinorGrid.LineDashStyle = ChartDashStyle.Dot;
             chart1.ChartAreas[0].AxisX.MinorGrid.LineColor = Color.Gray;
             chart1.ChartAreas[0].AxisX.MinorTickMark.LineColor = Color.Gray;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
             chart1.ChartAreas[0].AxisY.MinorTickMark.LineDashStyle = ChartDashStyle.Dash;
             chart1.ChartAreas[0].AxisY.MinorGrid.LineDashStyle = ChartDashStyle.Dash;
             chart1.ChartAreas[0].AxisY.MinorGrid.LineColor = Color.Gray;
             chart1.ChartAreas[0].AxisY.MinorTickMark.LineColor = Color.Gray;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
             chart1.ChartAreas[0].AxisY.MinorTickMark.LineDashStyle = ChartDashStyle.Dot;
             chart1.ChartAreas[0].AxisY.MinorGrid.LineDashStyle = ChartDashStyle.Dot;
             chart1.ChartAreas[0].AxisY.MinorGrid.LineColor = Color.Gray;
             chart1.ChartAreas[0].AxisY.MinorTickMark.LineColor = Color.Gray;

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            try
            {
                chart1.Titles.Clear();
                chart1.Titles.Add(textBox13.Text);
            }
            catch
            {

            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {

                 chart1.Titles[0].Font = new Font("Times New Roman", (int)numericUpDown1.Value, FontStyle.Regular);
            }
            catch
            {

            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            try
            {
                 chart1.ChartAreas[0].AxisX.Interval = double.Parse(textBox7.Text);
         
            }
            catch
            {

            }
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            try
            {
               chart1.ChartAreas[0].AxisX.LabelAutoFitMinFontSize = (int)numericUpDown5.Value;
            }
            catch
            {

            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //   main.chart1.ChartAreas[0].AxisY.Title = textBox8.Text;
                chart1.ChartAreas[0].AxisX.Title = textBox9.Text;
                //       main.chart1.ChartAreas[0].AxisX.TitleFont = new Font("Times New Roman", (int)numericUpDown2.Value, FontStyle.Regular);

            }
            catch
            {
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                chart1.ChartAreas[0].AxisX.TitleFont = new Font("Times New Roman", (int)numericUpDown2.Value, FontStyle.Regular);
  }
            catch
            {

            }    
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            try
            {

            chart1.ChartAreas[0].AxisY.TitleFont = new Font("Times New Roman", (int)numericUpDown3.Value, FontStyle.Regular);
            }
            catch
            {

            }    
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            try
            {

                chart1.ChartAreas[0].AxisY.Title = textBox8.Text;


            }
            catch
            {

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel27_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
    

