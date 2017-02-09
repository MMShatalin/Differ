using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmallPertubation
{
    /// <summary>
    /// Эта класс в котором храняться все данные загруженные из файла
    /// </summary>
   public class InputData
    {
        /// <summary>
        /// тут будут лежать данные загруженные пользователем из текстового файла
        /// </summary>
        /// массив листов
        private List<double>[] data;     
        public List<double>[] Data
        {
            get { return data; }   
        }

        public Exception ReadDataFromProkerFile(string filename)
        {
            StreamReader sr = new StreamReader(filename, Encoding.GetEncoding("Windows-1251"));

         
        
                string line = sr.ReadLine();
                int cnt = line.Split('\t').Length;

                List<string> f = line.Split('\t').ToList();
       
                this.data = new List<double>[cnt];
                for (int i = 0; i < cnt; i++)
                {
                    this.data[i] = new List<double>();
                }

            //    MessageBox.Show(f.Count +  " " + data.Count().ToString());
                List<double> temp = new List<double>();
                List<string> valueLineList = new List<string>();

                while (line != null)
                {
                    line = sr.ReadLine();

                   // MessageBox.Show(line);
                    if (line != null)
                    {
                        valueLineList = line.Split('\t').ToList();
                      

                  
                  //      MessageBox.Show();

                        //   MessageBox.Show(data.Count().ToString() + valueLineList.Count);
                        //    temp = valueLineList.Select(n => double.Parse(n.Replace('.', ','))).ToList();
                        for (int i = 0; i < data.Length; i++)
                        {
                             data[i].Add(double.Parse((valueLineList[i]).Replace('.', ',')));
                        }


                    }
                }
                sr.Close();
                return null;

  
        }
    }
}
