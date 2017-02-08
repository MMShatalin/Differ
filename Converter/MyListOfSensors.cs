using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Converter
{
    class MyListOfSensors : List<Sencors>
    {

        public Sencors getSensorByKKSName(string kks)
        {
            foreach (Sencors item in this)
            {
                if (item.KKS_Name == kks)
                {
                    return item;
                }
            }
            return null;
        }
        public Sencors getOneKKSByIndex(int index)
        {
            return this[index];
        }

        private int DetectType(string filename)
        {
            string[] temp = filename.Split('.');
            switch (temp[1])
            {
                case "txt":
                    return 1;
                    break;
                case "dat":
                    return 2;
                    break;
                case "wow":
                    return 3;
                case "ex":
                    return 4;
                default:
                    return -1;
                    break;
            }
            return -1;
        }

        public void LoadFromFile(string filename,MyListOfSensors y)
        {
            if (DetectType(filename) != -1)
            {
                switch (DetectType(filename))
                {
                    case 1:
                        this.LoadAPIK(filename,  y);
                        break;             
                   case 2:
                        this.LoadDATnvaes2(filename,  y);
                        break;
                    case 3:
                        this.LoadBusherFile(filename,  y);
                        break;
                    case 4:
                        this.LoadEx(filename, y);
                        break;

                    default:
                        break;                      
                }
            }
        }

        public void LoadEx(string filename, MyListOfSensors p)
        {
            string line = "";
            StreamReader MyFile = new StreamReader(filename, Encoding.GetEncoding("Windows-1251"));
            MyListOfSensors MyList = new MyListOfSensors();
            MyList.Clear();

            List<string> KKS = new List<string>();
            line = MyFile.ReadLine();
            KKS = line.Split('\t').ToList();

      
      
            int i2 = 0;
            foreach (string item in KKS)
            {
                i2++;
                if (i2 > 1 && item!="")
                {
                    Sencors myonekks = new Sencors();
                    myonekks.KKS_Name = item;
                    MyList.Add(myonekks);
                }
            }
    
            while ((line = MyFile.ReadLine())!=null)
            {
                KKS.Clear();
                KKS = line.Split('\t').ToList();
                for (int i = 1; i < MyList.Count+1; i++)
                {
                    Record myRec = new Record();
                 //   MessageBox.Show(KKS[0]);
                    myRec.ValueTimeForDAT = double.Parse(KKS[0].Replace(",","."));
                    myRec.DateTime = new DateTime(1970, 1, 1).AddSeconds(double.Parse(KKS[0].Replace(",",".").Trim()));
                    myRec.Value = double.Parse(KKS[i].Replace(",", "."));
                    MyList[i-1].MyListRecordsForOneKKS.Add(myRec);
                }
            }
            p.AddRange(MyList);
            MyFile.Close();  
        }

        public void LoadAPIK(string filename, MyListOfSensors p)
        {
            string line = "";
            StreamReader mysr = new StreamReader(filename, Encoding.GetEncoding("Windows-1251"));
            MyListOfSensors MyList = new MyListOfSensors();
            MyList.Clear();

            List<string> strarray = new List<string>();
            List<string> strarray1 = new List<string>();
            line = mysr.ReadLine();

            strarray1 = line.Split('\t').ToList();
            strarray1.RemoveAt(0);
            strarray.Add("Время реальное");
            strarray.Add("Время СКУД");
            strarray.AddRange(strarray1);
            strarray.RemoveAt(2);

            int i2 = 0;
            foreach (string item in strarray)
            {
                // i2++;
                if (i2 >= 0)
                {
                    Sencors myonekks = new Sencors();
                    myonekks.KKS_Name = item;
                    MyList.Add(myonekks);
                }
                i2++;

            }
            int N = strarray.Count() - 1;
            double[] mytempdouble = new double[strarray.Count];
            while (line != null)
            {
                line = mysr.ReadLine();
                if (line != null)
                {
                    mytempdouble = line.Replace('.', ',').Split('\t').Select(n => double.Parse(n)).ToArray();
                    //MessageBox.Show(mytempdouble[mytempdouble.Count()-1].ToString());

                    for (int i = 0; i < mytempdouble.Length; i++)
                    {
                        Record OneRec = new Record();
                      //  OneRec.ValueTimeForDAT = double.Parse(mytempdouble[0]);
                      //  MyListOfSensors.ConvertToUnixTimestamp();
                        OneRec.DateTime = DateTime.FromOADate(mytempdouble[0]);
                       // MyListOfSensors.ConvertToUnixTimestamp(OneRec.DateTime);
                        OneRec.ValueTimeForDAT = MyListOfSensors.ConvertToUnixTimestamp(OneRec.DateTime);
                     //   MessageBox.Show(mytempdouble[0].ToString() + " " + OneRec.DateTime.ToString());
                        OneRec.Value = mytempdouble[i];

                        MyList[MyList.Count - N + i - 1].MyListRecordsForOneKKS.Add(OneRec);
                    }
                }
            }
            p.AddRange(MyList);
            //Закрытие потока
            mysr.Close();
        }
        static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970,1,1);
            TimeSpan diff = date - origin;
            return diff.TotalSeconds;
        }
        private void LoadDATnvaes2(string filename,  MyListOfSensors p)
        {
            MyListOfSensors MyList = new MyListOfSensors();
            MyList.Clear();

            StreamReader MyFile = new StreamReader(filename, System.Text.Encoding.GetEncoding("Windows-1251"));
            StreamWriter MyRecord = new StreamWriter(@"D:\MyReactivity.txt");
                List<string> OriginalKKS = new List<string>();

                string line = MyFile.ReadLine();
              
                OriginalKKS = line.Split('\t').ToList();

                MessageBox.Show(OriginalKKS[0]);
  
                for (int i = 1; i < OriginalKKS.Count; i++)
                {
                    Sencors myonekks = new Sencors();
                    myonekks.KKS_Name = OriginalKKS[i];
                    myonekks.MyListRecordsForOneKKS = new List<Record>();
                    MyList.Add(myonekks);
                }
                MyList.RemoveAt(MyList.Count - 1);

                string[] massiv_znacheniy_postrochno = new string[OriginalKKS.Count + 1];
                while (line != null)
                {
                    line = MyFile.ReadLine();
                    if (line != null)
                    {
                        massiv_znacheniy_postrochno = line.Split('\t').ToArray();
                        for (int i = 1; i < massiv_znacheniy_postrochno.Length - 1; i++)
                        {
                            Record onerec = new Record();
                            onerec.Value = double.Parse(massiv_znacheniy_postrochno[i].Replace('.', ',').Trim());
                     //     MessageBox.Show(double.Parse(massiv_znacheniy_postrochno[0].Replace('.', ',').Trim()).ToString());
                            DateTime WindowsTime = new DateTime(1970, 1, 1).AddSeconds(double.Parse(massiv_znacheniy_postrochno[0].Replace('.', ',').Trim()));
                            onerec.ValueTimeForDAT = double.Parse(massiv_znacheniy_postrochno[0].Replace('.', ',').Trim());
                          //  MessageBox.Show(onerec.ValueTimeForDAT.ToString());
                            onerec.DateTime = WindowsTime;
                          //  MyRecord.WriteLine(onerec.DateTime.ToString() + " " + onerec.Value.ToString());
                           // MessageBox.Show(WindowsTime.ToOADate().ToString());
                            MyList[i - 1].MyListRecordsForOneKKS.Add(onerec);
                        }

                       
                    }// if
                } //whilr пока не конец ф
                p.AddRange(MyList);
                
                MyFile.Close();
             //   MyRecord.Close();
        
        }



        private void LoadBusherFile(string filename, MyListOfSensors p)
        {
            MyListOfSensors MyList = new MyListOfSensors();
            MyList.Clear();

            StreamReader MyFile = new StreamReader(filename, System.Text.Encoding.GetEncoding("Windows-1251"));

            List<string> OriginalKKS = new List<string>();

            string line = MyFile.ReadLine();

            OriginalKKS = line.Split('\t').ToList();

          //  MessageBox.Show(OriginalKKS[0]);

            for (int i = 1; i < 6; i++)
            {
                Sencors myonekks = new Sencors();
                myonekks.KKS_Name = OriginalKKS[i];
                myonekks.MyListRecordsForOneKKS = new List<Record>();
                MyList.Add(myonekks);
            }
            MyList.RemoveAt(MyList.Count - 1);

            string[] massiv_znacheniy_postrochno = new string[OriginalKKS.Count + 1];
            while (line != null)
            {
                line = MyFile.ReadLine();
                if (line != null)
                {
                    massiv_znacheniy_postrochno = line.Split('\t').ToArray();
                    for (int i = 1; i < 5; i++)
                    {
                        Record onerec = new Record();
                     //   MessageBox.Show(massiv_znacheniy_postrochno[i].Replace(',', '.').Trim());
                        onerec.Value = double.Parse(massiv_znacheniy_postrochno[i].Replace('.', ',').Trim());

                        DateTime WindowsTime = new DateTime(1970, 1, 1).AddSeconds(double.Parse(massiv_znacheniy_postrochno[0].Replace('.', ',').Trim()));
                        onerec.ValueTimeForDAT = double.Parse(massiv_znacheniy_postrochno[0].Replace('.', ',').Trim());
                        onerec.DateTime = WindowsTime;

                        MyList[i - 1].MyListRecordsForOneKKS.Add(onerec);
                    }
                }// if
            } //whilr пока не конец ф
            p.AddRange(MyList);

            MyFile.Close();
        }

      
    }
}   

