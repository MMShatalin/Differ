using System;
using System.Collections.Generic;
using System.IO;

namespace Converter
{
    internal class Current
    {
        #region Свойства

        //6 групп запаздывающих нейтронов
        //два массива для упрощения вычислений реактивности в дальнейшем
        private double[] _one = new double[6];
        private double[] _two = new double[6];

        //для поиска реактивности по формуле обращенного уравнения кинетики
        //это массив первых 6-ти значений, они все равны первому вычисленному току
        //так как нет предыдущего времени, а есть только первое значение
        double[] psi0 = new double[6];
        //  double[] FNext = new double[6];
        public double Ro; ////возвращает реактивность, рассчитанную в Беттах
        private double _tok1Old = double.NaN;
        private double _tok2Old = double.NaN;

        public double Tok1New;
        public double Tok2New;
        List<double> MyReac = new List<double>();
        public double Reactivity1;
        public double Reactivity2;
        public double ReactivityAverage;

        public double TimeNow;
        public double TimeOld;

        #endregion

        //токи типа должны быть вычислены до этого в методе
        //в методе также должна быть переменная времени
        //в данном случае берем Sensors, а не живые данные
        StreamWriter Ttt = new StreamWriter("D:\\Test.txt");
        public void AddData(List<double> I, List<double> R, List<double> Time, List<double> MyListReactivity)
        {
            this._tok1Old = I[0];
            this.TimeOld = Time[0];

            Ttt.WriteLine("this._tok1Old =" + this._tok1Old);
            Ttt.WriteLine("this.TimeOld =" + this.TimeOld);
            for (int i = 0; i < 6; i++)
            {
                psi0[i] = this._tok1Old;
                Ttt.Write(psi0[i] + " ");
            }

            Ttt.WriteLine();
            Ttt.WriteLine("Лист расчетной реактивности до " + MyListReactivity.Count);
            MyListReactivity.Add(0);
            for (int k = 1; k < I.Count; k++)
            {
                double deltaT = Time[k] - TimeOld;
                //   var dt = R.MyListRecordsForOneKKS[k].Value - _tok1Old;
                Ro = 0;

                for (int i = 0; i < 6; i++)
                {
                    double constTRaspada = MyConst.LMetodiki[i] * deltaT;
                    _one[i] = Math.Exp(-constTRaspada);
                    _two[i] = (1 - _one[i]) / constTRaspada;
                    psi0[i] = psi0[i] * _one[i] - (I[k] - _tok1Old) * (_two[i]) - _tok1Old * _one[i] + I[k];
                    double yt = MyConst.AApik[i] * psi0[i];
                    Ro = Ro + yt;
                }
                Ro = 1 - Ro / I[k];
                MyListReactivity.Add(Ro);
                _tok1Old = I[k];
                TimeOld = Time[k];
            }
            Ttt.WriteLine("Лист расчетной реактивности после " + MyListReactivity.Count);
            Ttt.Close();
        }
    }



    class MyConst
    {
        #region Свойства

        //параметры запаздывающих нейтронов (постоянные распада - лямбда, взятые из методик физических испытаний)
        public static double[] LMetodiki = new double[6];
        //параметры запаздывающих нейтронов (относительные групповые доли - альфа)
        public static double[] AApik = new double[6];
        //коэффициент перевода из процентов в бетта эффективность
        public static double _beff;

        #endregion
    }

}