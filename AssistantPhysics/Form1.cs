using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace AssistantPhysics
{
    public struct OneRec
    {
        public DateTime dt;
        public double value;
    }
/// <summary>
/// Это АдЫн ККС
/// </summary>

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MyListOfSensors allSensors = new MyListOfSensors();

        int indexSVBU;
        private void radioButton1_MouseHover(object sender, EventArgs e)
        {
            
        }
        private void radioButton2_MouseHover(object sender, EventArgs e)
        {
           
        }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "txt файлы (*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader streamReader;
                streamReader = new System.IO.StreamReader(openFileDialog1.FileName, System.Text.Encoding.GetEncoding("windows-1252"));
                textBox1.Text = Convert.ToString(openFileDialog1.FileName);

                if (indexSVBU == 0)
                {
                    for (int i = 0; i < 4; i++)
                        streamReader.ReadLine();

                    string line;;

                    while ((line = streamReader.ReadLine()) != null)
                    {
                        if (allSensors.getSensorByKKSName(line.Split('\t')[1])!=null)
                        {                          
                                OneRec OneRecord = new OneRec();
                                OneRecord.dt = Convert.ToDateTime(line.Split('\t')[0].Replace('.', '/').Replace(',', '.').Trim());
                                OneRecord.value = double.Parse(line.Split('\t')[2].Replace('.', ',').Replace('-', '0').Trim());
                                allSensors.getSensorByKKSName(line.Split('\t')[1]).values.Add(OneRecord);
                        } 
                        else
                        {                        
                            OneSensor myoneKKS = new OneSensor(line);
                            allSensors.Add(myoneKKS);
                        } //else
                        } //while
                    } // indexSVBU==0

                    //   checkedListBox1.Items.Add("START");
                    //ДОБАВИМ В КОЛЛЕКЦИЮ ЧЕКБОКСОВ 
                if (indexSVBU == 1)
                {
                    string line;

                    for (int i = 0; i < 4; i++)
                    {
                        streamReader.ReadLine();
                    }

                    line = streamReader.ReadLine();

                    List<string> origkks = new List<string>();
                    origkks = line.Split('|').ToList();

                    int p = 0;
                    foreach (string item in origkks)
                    {
                        p++;
                        if (p > 1)
                        {
                            OneSensor myonekks = new OneSensor();
                            myonekks.values = new List<OneRec>();
                            myonekks.KksName = item;
                            allSensors.Add(myonekks);
                        }
                    }

                    string[] massiv_znacheniy_postrochno = new string[origkks.Count + 1];
                    while (line != null)
                    {
                        line = streamReader.ReadLine();
                        if (line != null)
                        {
                            massiv_znacheniy_postrochno = line.Split('|').ToArray();
                            for (int i = 1; i < massiv_znacheniy_postrochno.Length - 1; i++)
                            {
                                OneRec onerec = new OneRec();
                                onerec.dt = Convert.ToDateTime(massiv_znacheniy_postrochno[0].Replace(".000", "").Trim());
                                onerec.value = double.Parse(massiv_znacheniy_postrochno[i].Replace('.', ',').Trim());

                                allSensors[i - 1].values.Add(onerec);
                            }
                        }// if
                    } //whilr пока не конец файла
                }
                streamReader.Close();
            } // if openFIle Dialog

            foreach (OneSensor item in allSensors)
            {
                checkedListBox1.Items.Add(item.KksName);
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++) //По всем отвеченным
            {
                //   MessageBox.Show((string)checkedListBox1.CheckedItems[i]);
                for (int ii = 0; ii < allSensors.Count; ii++)
                {
                    if ((string)checkedListBox1.CheckedItems[i] == allSensors[ii].KksName)
                    {

                        StreamWriter streamwriter = new StreamWriter("D:\\" + allSensors[ii].KksName + ".txt", false, System.Text.Encoding.GetEncoding("utf-8"));
                        streamwriter.WriteLine(allSensors[ii].KksName);

                        for (int j = 0; j < allSensors[ii].values.Count; j++)
                        {
                            streamwriter.WriteLine("{0:dd.MM.yy H:mm:ss.fff} {1}", allSensors[ii].values[j].dt, allSensors[ii].values[j].value);
                        }
                        streamwriter.Close();
                    }
                }
            }

        }

        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // checkedListBox1.Items.Clear();
            //textBox1.Text = "";
            //allSensors.Clear();
            chart1.Series[0].Points.Clear();
            chart1.Series["Series1"].Points.Clear();
            masY.Clear();
            masX.Clear();
           
      
            
        }

        private void checkedListBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void данныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Время");
            dt.Columns.Add("Значение");
            // Создаём столбцы//???????????????????

            //   MessageBox.Show((string)checkedListBox1.CheckedItems[i]);
            for (int ii = 0; ii < allSensors.Count; ii++)
            {
                if ((string)checkedListBox1.Text == allSensors[ii].KksName)
                {
                    foreach (OneRec item in allSensors[ii].values)
                    {
                        dt.Rows.Add(item.dt, item.value);
                    }
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void выбратьПараметрПоОсиХToolStripMenuItem_Click(object sender, EventArgs e)
        {

            for (int j = 0; j < allSensors.Count; j++)
            {
                if ((string)checkedListBox1.Text == allSensors[j].KksName)
                {
                    for (int i = 0; i < allSensors[j].values.Count; i++)
                    {
                        chart1.Series["Series1"].XValueType = ChartValueType.DateTime;
                        System.DateTime x = new System.DateTime();
                        x = allSensors[j].values[i].dt;

                        chart1.Series["Series1"].Points.AddXY(x.ToOADate(), allSensors[j].values[i].value);
                    }
                    chart1.Series[0].LegendText = allSensors[j].KksName;
                }
                
            }
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "H:mm:ss"; 
        }
        List<double> masX = new List<double>();
        List<double> masY = new List<double>();
        private void выбратьПараметрПоОсиYToolStripMenuItem_Click(object sender, EventArgs e)
        {
       

            for (int j = 0; j < allSensors.Count; j++)
            {
                if (allSensors[j].KksName == (string)checkedListBox1.CheckedItems[0])
                {
                    for (int i = 0; i < allSensors[j].values.Count; i++)
                    {
                        masY.Add(allSensors[j].values[i].value);
                    }
                    chart1.Series[0].LegendText = allSensors[j].KksName;
                }
            }
            for (int j = 0; j < allSensors.Count; j++)
            {
                if (allSensors[j].KksName == (string)checkedListBox1.CheckedItems[1])
                {
                    for (int i = 0; i < allSensors[j].values.Count; i++)
                    {
                        masX.Add(allSensors[j].values[i].value);
                    }
                    chart1.Titles.Add(allSensors[j].KksName);
                }
            }
            for (int i = 0; i< masY.Count;i++ )
                chart1.Series["Series1"].Points.AddXY(masX[i], masY[i]);
            
        }
        private void radioButton2_Click(object sender, EventArgs e)
        {
            indexSVBU = 1;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            indexSVBU = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            chart1.Series[0].LegendText = checkedListBox1.Text;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoom(-350, 350);
            chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;
        }

        private void chart1_MouseClick(object sender, MouseEventArgs e)
        {
        }
        private void очистиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
            textBox1.Text = "";
            allSensors.Clear();
            chart1.Series[0].Points.Clear();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}




     

    



