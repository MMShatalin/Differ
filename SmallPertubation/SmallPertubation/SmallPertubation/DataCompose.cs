using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SmallPertubation
{
    public partial class DataCompose : Form
    {
              
        private Chart form1Chart1;     //ссылка на чарт, чтобы в онлайне обновлять ее;
    
        private DataTable mydataTableResult; //таблица для вывода результатов;


        private InputData myInputData; // тут лежат все входные данные

        private Series VozmSeries;     //ряд для временного маркера для юзера

        private Series FixVozmSeries;  // Ряд для фиксированных маркеров уже установленных 

        private int index;              //индекс текущей точки, которую выбирает пользователь

        private OneVozmuchenieData myOneVozmuchenie;


        /// <summary>
        /// Чтобы пользователю выбирать и компановать данные необходимы ссылки на следующие объекты
        /// </summary>
        /// <param name="mychart"> на основной ЧАРТ</param>
        /// <param name="mydata"> на массив входных данных</param>
        /// <param name="mydatatable"> ссылка на DataTable c результатыми</param>
        /// <param name="myOneVozmuch"> и ссылка на объект в котором агрегированы все данные и методы по работе с одним возмущением</param>
        //public DataCompose(Chart mychart, InputData mydata, DataTable mydatatable,OneVozmuchenieData myOneVozmuch)
         public DataCompose(Chart mychart, InputData mydata, DataTable mydatatable)
        {
            InitializeComponent();

            this.form1Chart1 = mychart; //ссылка на чарт где отображается реактивность
            this.myInputData = mydata; // ссылка на массив где исходные данные
            this.mydataTableResult = mydatatable; //ссылка на таблицу, которая выводит результаты в датагридвью
            //this.myOneVozmuchenie = myOneVozmuch; ///пока это проста ссылка на пустой объект, который потихонбку надо заполнять
            this.myOneVozmuchenie = new OneVozmuchenieData();


            ///временный ряд для движущихся точек МАРКЕРА
            VozmSeries = new Series();
            VozmSeries.ChartType = SeriesChartType.Point;
            VozmSeries.MarkerStyle = MarkerStyle.Cross;
            VozmSeries.MarkerSize = 12;
            VozmSeries.Color = Color.DarkGreen;
            this.form1Chart1.Series.Add(VozmSeries);

            ///этот ряд УЖЕ для фиксированнных точек МАРКЕРА
            FixVozmSeries = new Series();
            FixVozmSeries.ChartType = SeriesChartType.Point;
            FixVozmSeries.MarkerStyle = MarkerStyle.Cross;
            FixVozmSeries.MarkerSize = 14;
            FixVozmSeries.Color = Color.DarkBlue;
            this.form1Chart1.Series.Add(FixVozmSeries);
        }

        /// <summary>
        /// CДВИНУТЬ ВРЕМЕННОЙ МАРКЕР НА ОДИН ОТЧЕТ ВПРАВО
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (VozmSeries.Points.Count == 0)
            {
                ///найдем индекс
                ///
                this.index = 0;
                do
                {
                    index++;
                } while (this.form1Chart1.ChartAreas[0].CursorX.Position > this.myInputData.Data[0][index]);
                this.VozmSeries.Points.AddXY(this.myInputData.Data[0][index], this.myInputData.Data[MyConst.r1][index]);
            }
            else
            {
                index++;
                //удалим последнюю точку
                this.VozmSeries.Points.RemoveAt(this.VozmSeries.Points.Count - 1);
                this.VozmSeries.Points.AddXY(this.myInputData.Data[0][index], this.myInputData.Data[MyConst.r1][index]);
                //this.FixVozmSeries.Points.AddXY(this.myData.Data[0][i], this.myData.Data[MyConst.r1][i]);
            }
        }

        /// <summary>
        /// КНОПКА НАЧАЛО
        /// ЗАФИКСИРОВАЬТ НАЧАЛО ШАГА ПЕРЕМЕЩЕНИЯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            /*ПРЕДПОЛОЖИТЕЛЬНО ЗАПОМИНАТЬ НУЖНО ТОЛЬКО ДВЕ ТОЧКИ УЧАСТКА ОБРАБОТКИ С ВЫВОДОМ ИХ ЗНАЧЕНИЯ НА КОНТРОЛ
             * НЕ НУЖНЫ КНОПКИ НАЧАЛО И КОНЕЦ (ВСЕГО ДВЕ ТОЧКИ ДЛЯ УЧАСТКА) В СЛУЧАЕ ЕСЛИ ЕСТЬ ПОЛОЖЕНИЕ ГРУПП ДЛЯ ПОИСКА ТОЧЕК АВТОМАТОМ
             * В СЛУЧАЕ ЕСЛИ НЕТ ТАКОЙ ВОЗМОЖНОСТИ ТО ДОЛЖЕН БЫТЬ ВАРИАНТ В РУЧНУЮ ПО ТОЧКАМ           
             */


           this.FixVozmSeries.Points.AddXY(this.myInputData.Data[0][index], this.myInputData.Data[MyConst.r1][index]);
        //   MessageBox.Show(this.FixVozmSeries.Points.Count.ToString());
            //перенесем для удобства пользователя  точку на предположительное начало/конец шага
           this.VozmSeries.Points.RemoveAt(this.VozmSeries.Points.Count - 1);    
            ///с визуалкой покончили, начнем компановать данные для обработки в наш объект
            ///            
           ////итак если это первая точка серии шагов
           if (this.FixVozmSeries.Points.Count == 1)
            {
                myOneVozmuchenie.Ir = index;
                ///прибавим единичку к колличеству перемещений
                myOneVozmuchenie.NPer = 1;
                ///я думаю тут тепловая мощность, поэтому запишим начальную тепловую мощность
                myOneVozmuchenie.Nt = myInputData.Data[MyConst.N1k][index];
                ///запишем время начала текущего перемещения
                myOneVozmuchenie.Per[0, 0] = myInputData.Data[0][index];
                myOneVozmuchenie.PerIndex[0, 0] = index; //запишем индекс в массиве данных начальной точки возмущения
            }
            else
            {
                ///увеличим счетчик числа шагов, а так же запишем индекс и время начала текущего шага
                myOneVozmuchenie.NPer++;
                myOneVozmuchenie.Per[myOneVozmuchenie.NPer-1, 0] = myInputData.Data[0][index];
                myOneVozmuchenie.PerIndex[myOneVozmuchenie.NPer - 1, 0] = index; 
            }            
            index += 6; 
            this.VozmSeries.Points.AddXY(this.myInputData.Data[0][index], this.myInputData.Data[MyConst.r1][index]);
        }


        private struct pertubResult
        {

           public double aH;
           public double b;
           public double Ro;
           public double tau;

           public List<double> FF;  //чисто для вывода 

            //величина невязки
           public double SS;
                      
        }




        //List<double> time = new List<double>();
        //List<double> I = new List<double>();
        //List<double> P = new List<double>();
        //List<double> pdH = new List<double>();

        /// <summary>
        /// основная функция для расчета
        /// </summary>
        /// <param name="Tau">постоянная времени разогрева</param>
        /// <param name="time">массив времени</param>
        /// <param name="I">массив токов</param>
        /// <param name="P">массив реактивностей</param>
        /// <param name="dH">массив перемещений группы</param>
        /// <returns></returns>
        /// 

        private pertubResult Calc(double Tt, List<double> time, List<double> I, List<double> R, List<double> dH)
        {
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
                if (dT==0)
                {
                    myresult.FF.Add(0);
                    continue;
                }
                g = Math.Exp(-dT/Tt);
                c = (1 - g) * Tt / dT;

                F = F*g + (I[i]-1)*(1-c) + (I[i-1]-1)*(c-g);
                myresult.FF.Add(F);
                sH += dH[i];
                sHH += dH[i] * dH[i];
                sF += F;
                sFF += F * F;
                sR += R[i];
                sFH += F * dH[i];
                sRH += R[i] * dH[i];
                sRF += R[i] * F;
                cnt++;            
            }


            //считаем суммы

            SA = sH * sH / cnt / cnt - sHH / cnt;
            SB = sF * sH / cnt / cnt - sFH / cnt;
            SC = sR * sH / cnt / cnt - sRH / cnt;
            SD = sF * sF / cnt / cnt - sFF / cnt;
            SE = sR * sF / cnt / cnt - sRF / cnt;          


            myresult.aH = (SC*SD-SB*SE)/(SA*SD-SB*SB);
            myresult.b = (SA*SE-SB*SC)/(SA*SD-SB*SB);

            myresult.Ro = sR / cnt - myresult.aH * sH / cnt - myresult.b * sF / cnt;
            

            // невязки


            F = 0;
            cnt = 0;
            sS = 0;

          for (int i = 1; i < time.Count; i++)
            {
                dT = time[i] - time[i - 1];
                if (dT==0)
                {
                    continue;
                }

                g = Math.Exp(-dT/Tt);
                c = (1 - g) * Tt / dT;
                F = F * g + (I[i] - 1) * (1 - c) + (I[i - 1] - 1) * (c - g);

                Rm = myresult.Ro + myresult.aH * dH[i] + myresult.b * F;
                sS += (R[i] - Rm) * (R[i] - Rm);

                cnt++;
            } //i
            myresult.SS = sS;



            return myresult;
        }



        /// <summary>
        /// КНОПКА РАСЧЕТА ТЕКУЩЕГО ВОЗМУЩЕНИЯ - КНОПКА "МНК"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            //ЭТА КНОПКА НАЧАЛА РАСЧЕТА
            //ИТАК НУЖНО ДЛЯ НАЧАЛА СЧЕТА ПОДГОТОВИТЬ ВСЕ ДАННЫЕ
            ///НА ПОНАДОБЯТЬСЯ:
            ///

            //Пускай это будет у нас для удобства в отдельных массивах

            List<double> time = new List<double>();
            List<double> I = new List<double>();
            List<double> P = new List<double>();
            List<double> pdH = new List<double>();

            /*Итак у нас есть:
             1) исходный двумерный массив входных данных MyInputData             
             2) индекс когда начались все перемещения  myOneVozmuchenie.Ir или что тоже самое myOneVozmuchenie.PerIndex[0, 0];
             3) кол-во перемещений myOneVozmuchenie.NPer;
             4) тепловая мощность в момент начала перемещений myOneVozmuchenie.Nt
             5) индекс окончания последнего перемещения  myOneVozmuchenie.PerIndex[myOneVozmuchenie.NPer - 1, 1];
              */


            //обозначили границы нашего перемещения
            myOneVozmuchenie.Ir = myOneVozmuchenie.PerIndex[0, 0];

            myOneVozmuchenie.Iend = myOneVozmuchenie.PerIndex[myOneVozmuchenie.NPer - 1, 1]+MyConst.delayTime; //при условии что частота 40 Гц добавим 5 секунд




            //Reactivity mynewR = new Reactivity();
            //mynewR.AddFirstData(mydata.Data[0][0], mydata.Data[MyConst.tok1][0]);
            //for (int i = 1; i < mydata.Data[MyConst.tok1].Count; i++)
            //{
            //    mynewR.AddData(mydata.Data[0][i], mydata.Data[MyConst.tok1][i]);
            //    chart1.Series[1].Points.AddXY(mydata.Data[0][i], mynewR.Ro);
            //}

            Reactivity mynewR = new Reactivity();       
            
            for (int i = myOneVozmuchenie.Ir; i < myOneVozmuchenie.Iend; i++)
            {
                time.Add(myInputData.Data[0][i]);
                //ну так у Боева в методе GetJ
                I.Add(myInputData.Data[MyConst.tok1][i] / myInputData.Data[MyConst.tok1][myOneVozmuchenie.Ir]);
                  
                
                if (i == myOneVozmuchenie.Ir)
                   {
                      mynewR.AddFirstData(myInputData.Data[0][i], I[0]);
                   }
                   else
                   { 
                   mynewR.AddData(myInputData.Data[0][i],I[I.Count-1]);               
                   }
               P.Add(mynewR.Ro);

                  this.form1Chart1.Series[3].Points.AddXY(myInputData.Data[0][i], mynewR.Ro);
                  //this.form1Chart1.Series[4].Points.AddXY(myInputData.Data[0][i], myInputData.Data[MyConst.H10][i]);


                //myOneVozmuchenie.PerIndex[myOneVozmuchenie.NPer - 1, 1]

            }



            ///нужно раздробить перемещения
            double ddH = 0;
            for (int i = 0; i < myOneVozmuchenie.NPer; i++)
            {
                //ведь каждый шаг должен в сумме давать 2
                for (int j = myOneVozmuchenie.PerIndex[i, 0]+1; j <= myOneVozmuchenie.PerIndex[i, 1]; j++)
                {
                    ddH -= (2 / (myOneVozmuchenie.Per[i, 1] - myOneVozmuchenie.Per[i, 0])) * (myInputData.Data[0][j] - myInputData.Data[0][j-1]);
                    pdH.Add(ddH);                   
                }

                //ДОБАВИТЬ КУСОК КОГДА НЕ ПЕРЕМЕЩАЛОСЬ

                if (i!=myOneVozmuchenie.NPer-1)
                {
                    for (int jj = myOneVozmuchenie.PerIndex[i, 1] + 1; jj <= myOneVozmuchenie.PerIndex[i + 1, 0]; jj++)
                    {
                        pdH.Add(ddH);
                    }   
                }
            } // for i по всем перемещениям



            for (int i = 0; i < MyConst.delayTime; i++)
            {
                pdH.Add(ddH);
            }

          // MessageBox.Show(pdH.Count.ToString());



            this.form1Chart1.Series[4].Points.Clear();




  


            //Ну все якобы ВСЕ ГОТОВО К СЧЕТУ
            
            
            //List<double> time = new List<double>();
            //List<double> I = new List<double>();
            //List<double> P = new List<double>();
            //List<double> pdH = new List<double>();
                    //Сверка всех длин массива прошла успешно
             // MessageBox.Show(time.Count + "  " + I.Count + " " + P.Count + " " + pdH.Count);








              double Ss = 1000;
            pertubResult tempR = new pertubResult();

              for (int i = 0; i < 200; i++)
              {
                  tempR = Calc(3 + i / 200, time, I, P, pdH); 
                  if (tempR.SS>Ss)
                  {
                   tempR =  Calc(3 + (i-1) / 200, time, I, P, pdH);
                      break;  
                  }
                  Ss = tempR.SS;                  
              }

              tempR.Ro = tempR.Ro * MyConst.Rect.Beff;
              tempR.aH = tempR.aH * MyConst.Rect.Beff;
              tempR.b = tempR.b * MyConst.Rect.Beff;
              tempR.SS = tempR.SS * MyConst.Rect.Beff;


             // this.mydataTableResult.Rows.Add(80, "DR/DH", "DR/DN", "tau");


            //  MessageBox.Show(tempR.FF.Count.ToString());


              this.mydataTableResult.Rows.Add(myInputData.Data[MyConst.H10][myOneVozmuchenie.Ir], tempR.aH, tempR.b / myOneVozmuchenie.Nt, tempR.tau);


              StreamWriter sr = new StreamWriter("Возмущение " + myInputData.Data[MyConst.H10][myOneVozmuchenie.Ir] + "_"+myInputData.Data[MyConst.H9][myOneVozmuchenie.Ir] + "_"+myInputData.Data[MyConst.H8][myOneVozmuchenie.Ir] + " .csv");

              sr.WriteLine("h10 s; h10 e; h10 mid; h9 s; h9 e; h9 mid; h8 s; h8 e; h8 mid; N; b; aH; aN; tau ; Ro;  'PO PICHKAM'; 'Rm = myresult.Ro + myresult.aH * dH[i] + myresult.b * F;'");

              sr.Write(myInputData.Data[MyConst.H10][myOneVozmuchenie.Ir] + ";" + myInputData.Data[MyConst.H10][myOneVozmuchenie.Iend] + ";" + (myInputData.Data[MyConst.H10][myOneVozmuchenie.Ir] + myInputData.Data[MyConst.H10][myOneVozmuchenie.Iend]) / 2 + ";");
              sr.Write(myInputData.Data[MyConst.H9][myOneVozmuchenie.Ir] + ";" + myInputData.Data[MyConst.H9][myOneVozmuchenie.Iend] + ";" + (myInputData.Data[MyConst.H9][myOneVozmuchenie.Ir] + myInputData.Data[MyConst.H9][myOneVozmuchenie.Iend]) / 2 + ";");
              sr.Write(myInputData.Data[MyConst.H8][myOneVozmuchenie.Ir] + ";" + myInputData.Data[MyConst.H8][myOneVozmuchenie.Iend] + ";" + (myInputData.Data[MyConst.H8][myOneVozmuchenie.Ir] + myInputData.Data[MyConst.H8][myOneVozmuchenie.Iend]) / 2 + ";");

              sr.Write(myOneVozmuchenie.Nt);

              sr.Write(";" + tempR.b);

              sr.Write(";" + tempR.aH);
              sr.Write(";" + tempR.b / myOneVozmuchenie.Nt);
              sr.Write(";" + tempR.tau);
              sr.Write(";" + tempR.Ro);




            //посчитаем aH по пичкам

              double DroAver =0;

              for (int i = 0; i < myOneVozmuchenie.NPer; i++)
              {
                  MessageBox.Show(myInputData.Data[MyConst.r1][myOneVozmuchenie.PerIndex[i, 1]].ToString() + " " + myInputData.Data[MyConst.r1][myOneVozmuchenie.PerIndex[i, 0]]);
                  DroAver = DroAver + (myInputData.Data[MyConst.r1][myOneVozmuchenie.PerIndex[i, 1]] - myInputData.Data[MyConst.r1][myOneVozmuchenie.PerIndex[i, 0]]);

                  
                  //ведь каждый шаг должен в сумме давать 2
                  //for (int j = myOneVozmuchenie.PerIndex[i, 0] + 1; j <= myOneVozmuchenie.PerIndex[i, 1]; j++)
                  //{
                  //    ddH -= (2 / (myOneVozmuchenie.Per[i, 1] - myOneVozmuchenie.Per[i, 0])) * (myInputData.Data[0][j] - myInputData.Data[0][j - 1]);
                  //    pdH.Add(ddH);
                  //}


              }
              MessageBox.Show(myOneVozmuchenie.NPer.ToString());
              MessageBox.Show(DroAver.ToString());
              DroAver = DroAver / myOneVozmuchenie.NPer;
              MessageBox.Show(DroAver.ToString());
              DroAver = DroAver / -2;
              MessageBox.Show(DroAver.ToString());
              DroAver = DroAver * MyConst.Rect.Beff;
              MessageBox.Show(DroAver.ToString());
              sr.Write(";" + DroAver);

              sr.WriteLine();

              // sr.WriteLine("начало -" + myOneVozmuchenie.Ir + "; конец - " + myOneVozmuchenie.Iend);

              sr.WriteLine("Time; Current; React; pdH; F");

              for (int i = 0; i < pdH.Count; i++)
              {
                  this.form1Chart1.Series[4].Points.AddXY(myInputData.Data[0][myOneVozmuchenie.Ir + i], pdH[i]);
                  sr.WriteLine(time[i] + ";" + I[i] + ";" + P[i] + ";" + pdH[i]  + " ; "+ tempR.FF[i]);
              }




              sr.Close();





            //for (int i = myOneVozmuchenie.Ir; i < myOneVozmuchenie.Iend; i++)
            //{
            //    if ((i > myOneVozmuchenie.PerIndex[currPerem, 0]) && (i < myOneVozmuchenie.PerIndex[currPerem, 1]))
            //    {
                
            //        ddH+=(2/(myOneVozmuchenie.Per[currPerem,1]-myOneVozmuchenie.Per[currPerem,0]))*(myInputData.Data[0][i])

            //    }
            //    else
            //    {



            //        if (true)
            //        {
                        
            //        }
                
                
            //    }



            //}







            //Занесем пока все что есть, а все что просто. А дальше будем думать






            //СКОРЕЕ ВСЕГО ЭТО ВСЕ ЧТО НАМ ПОНАДОБИТЬСЯ:
            //double[] I -  МАССИВ ТОКОВ С ВЫСОКОЙ ЧАСТОТОЙ
            ///double[] pm - МАССИВ РЕАКТИВНОСТЕЙ РАСЧИТАННЫХ ПО ТОКАМ с такой де высокой частосто
            ///double[] H - МАССИВ ПОЛОЖЕНИЯ ГРУПП ДЛЯ КАЖДОГО ОТЧЕТА (перессчитанная также с высокой частостой для большой частоты регистрации)
             
            
            //this.mydataTableResult.Rows.Add(80, 1E-3, 1E-4);
            //this.mydataTableResult.Rows.Add(80, 1E-3, 1E-4);
            //MessageBox.Show(this.form1Chart1.ChartAreas[0].CursorX.Position.ToString());



        }

        /// <summary>
        /// СДВИНУТЬ ВРЕМЕННЫЙ МАРКЕР НА ОДИН ОТЧЕТ ВЛЕВО
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (index > 0)
            {
                index--;
                //удалим последнюю точку
                this.VozmSeries.Points.RemoveAt(this.VozmSeries.Points.Count - 1);
                this.VozmSeries.Points.AddXY(this.myInputData.Data[0][index], this.myInputData.Data[MyConst.r1][index]);
                //this.FixVozmSeries.Points.AddXY(this.myData.Data[0][i], this.myData.Data[MyConst.r1][i]);
            }
        }

        /// <summary>
        /// ОЧИСТИИТЬ ОБЛСТЬ ОТ ВРЕМЕННЫХ МАРКЕРОВ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataCompose_FormClosing(object sender, FormClosingEventArgs e)
        {
            VozmSeries.Points.Clear();
            FixVozmSeries.Points.Clear();
        }

        /// <summary>
        /// КНОПКА КОНЕЦ
        /// ЗАФИКСИРОВАТЬ КОНЕЦ ШАГА ПЕРЕМЕЩЕНИЯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.FixVozmSeries.Points.AddXY(this.myInputData.Data[0][index], this.myInputData.Data[MyConst.r1][index]);
            //перенесем для удобства пользователя  точку на предположительное начало/конец шага
            this.VozmSeries.Points.RemoveAt(this.VozmSeries.Points.Count - 1);

           // myOneVozmuchenie.NPer++; //так счетчик уже щелкнул при начале каждого перемещения

            myOneVozmuchenie.Per[myOneVozmuchenie.NPer - 1, 1] = myInputData.Data[0][index];
            myOneVozmuchenie.PerIndex[myOneVozmuchenie.NPer - 1, 1] = index; 
            
            index += 34;
            this.VozmSeries.Points.AddXY(this.myInputData.Data[0][index], this.myInputData.Data[MyConst.r1][index]);
        }
    }
}

