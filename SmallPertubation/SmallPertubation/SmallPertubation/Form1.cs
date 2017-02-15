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
    public partial class Form1 : Form
    {
        /// <summary>
        /// А в этом классе храняться входные данные введенные пользователем
        /// Это тупо двумерный массив данных для хранения 
        /// </summary>
        InputData mydata;// = new InputData();

        /// <summary>
        /// Эта штука для того, чтобы сохранять туда результаты измерений
        /// </summary>
        DataTable dataResultTable;

        public Form1()
        {
            InitializeComponent();
            mydata = new InputData();
            dataResultTable = new DataTable();            
            dataResultTable.Columns.Add("H1");
            dataResultTable.Columns.Add("H2");
            dataResultTable.Columns.Add("dr/dh");
            dataResultTable.Columns.Add("dr/dN");
            dataResultTable.Columns.Add("tau");
            ///привяжем объект DataTable к dataGridView1. Далее DataTable будем передавать окну обработчика возмущений, чтобы туда записывать результаты
            this.dataGridView1.DataSource = dataResultTable; // чтобы отображалась сразу
        }



        /// <summary>
        /// Это обработчик события "ФАЙЛ - Открыть от прокера"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void открытьОтПрокераToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Exception ex;
                if ((ex = mydata.ReadDataFromProkerFile(openFileDialog1.FileName)) != null)
                {
                    MessageBox.Show("Возникола следующая ошибка при чтении файла: " + ex.GetType().Name);
                }
                chart1.Series[0].XValueType = ChartValueType.Double;                
                for (int i = 0; i < mydata.Data[0].Count; i++)
                {
                    //нанесем на график реактивности
                    if (mydata.Data[MyConst.r1][i]<1)
                    {
                        chart1.Series[0].Points.AddXY(mydata.Data[0][i], mydata.Data[MyConst.r1][i]);    
                    }
                    
                    
                }


                /////сформируем комбобокс для выбора интервала времени нижней оси
                comboBox1.Items.Clear();
                comboBox1.Items.Add("ALL");
                foreach (string item in MyConst.XintervalNames)
                {
                    comboBox1.Items.Add(item);
                }
                comboBox1.SelectedIndex = 0; ///по умолчанию пускай будет выбран первый элемент
            } // if openfileDialog
        }
        /// <summary>
        /// Кнопка > сдвинуть отображение на один отчет сигнала вправа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisX.Minimum = chart1.ChartAreas[0].AxisX.Minimum + 1;
            chart1.ChartAreas[0].AxisX.Maximum = chart1.ChartAreas[0].AxisX.Maximum + 1;
        }
        /// <summary>
        /// Кнопка < сдвинуть отображение на один отчет сигнала влево
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisX.Minimum = chart1.ChartAreas[0].AxisX.Minimum - 1;
            chart1.ChartAreas[0].AxisX.Maximum = chart1.ChartAreas[0].AxisX.Maximum - 1;
        }
        /// <summary>
        /// Кнопка начала возмущений, запускается форма, где пользователь указывается начало и конец каждого шага
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {          
           ///и тут пускай будет жить наш объект 
           ///посвященный обработке возмущения
           ///
           ///да пускай он будет рождаться именно ТУТ
           ///
           // OneVozmuchenieData myOneVozmuch = new OneVozmuchenieData();  
            ///сформируем форму и запустим ее
            //DataCompose   DataComposeForm = new DataCompose(this.chart1, this.mydata, this.dataResultTable, myOneVozmuch);

            DataCompose DataComposeForm = new DataCompose(this.chart1, this.mydata, this.dataResultTable);
            DataComposeForm.Show();     
        }
        /// <summary>
        /// если пользователь выбрал определенный интервал 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                chart1.ChartAreas[0].AxisX.Minimum = chart1.Series[0].Points[0].XValue;
                chart1.ChartAreas[0].AxisX.Maximum = chart1.Series[0].Points[chart1.Series[0].Points.Count - 1].XValue;
            }
            else
            {
                chart1.ChartAreas[0].AxisX.Maximum = chart1.ChartAreas[0].AxisX.Minimum + MyConst.XintervalVal[comboBox1.SelectedIndex];
            }
        }
        /// <summary>
        /// кнопка запускает форму установки минимума и максимума осей графика
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void осьГрафикаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ///форма запускается
            AxisSettings myAS = new AxisSettings(this.chart1);
            myAS.Show();
        }
        /// <summary>
        /// КНОПКА >> сдвигает на ширину окна вправо
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            double wid = chart1.ChartAreas[0].AxisX.Maximum - chart1.ChartAreas[0].AxisX.Minimum;
            chart1.ChartAreas[0].AxisX.Maximum = chart1.ChartAreas[0].AxisX.Maximum + wid;
            chart1.ChartAreas[0].AxisX.Minimum = chart1.ChartAreas[0].AxisX.Minimum + wid;
        }
        /// <summary>
        /// КНОПКА << сдвигает на окно влево
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            double wid = chart1.ChartAreas[0].AxisX.Maximum - chart1.ChartAreas[0].AxisX.Minimum;
            chart1.ChartAreas[0].AxisX.Minimum = chart1.ChartAreas[0].AxisX.Minimum - wid;
            chart1.ChartAreas[0].AxisX.Maximum = chart1.ChartAreas[0].AxisX.Maximum - wid;
        }

        private void пересчитатьРеактивностиToolStripMenuItem_Click(object sender, EventArgs e)
        {          
            Reactivity mynewR = new Reactivity();
            mynewR.AddFirstData(mydata.Data[0][0], mydata.Data[MyConst.tok1][0]);
            for (int i = 1; i < mydata.Data[MyConst.tok1].Count; i++)
            {
                mynewR.AddData(mydata.Data[0][i], mydata.Data[MyConst.tok1][i]);
                chart1.Series[1].Points.AddXY(mydata.Data[0][i], mynewR.Ro);
            }
        }

        private void реактивностьПоЧмыхунуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReactivityChmihun mynewRC = new ReactivityChmihun();
            mynewRC.AddFirstData(mydata.Data[0][0], mydata.Data[MyConst.tok1][0]);
            for (int i = 1; i < mydata.Data[MyConst.tok1].Count; i++)
            {
                mynewRC.AddData(mydata.Data[0][i], mydata.Data[MyConst.tok1][i]);
                chart1.Series[2].Points.AddXY(mydata.Data[0][i], mynewRC.Ro);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}