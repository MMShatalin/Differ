using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallPertubation
{

    /// <summary>
    /// этот класс для аггрегации данных за одно малое возмещение с методам их обработки направленными для получения аН, b, Tt
    /// 
    /// </summary>
    public class OneVozmuchenieData
    {
        /// <summary>
        /// индекс начала текущего возмущения
        /// </summary>
        int ir; //тут индекс начала возмущений

        public int Ir
        {
            get { return ir; }
            set { ir = value; }
        }

        /// <summary>
        /// индекс окончания текущего возмущения
        /// </summary>
        int iend; //тут индекс конца возмущений

        public int Iend
        {
            get { return iend; }
            set { iend = value; }
        }


       /// <summary>
       /// ну я так думаю
       /// </summary>
       /// 
        //List<double> ddh; ///тут будет лежать изменения высоты ОР СУЗ, мужду следующими друг за друга отчетами

        //public List<double> Ddh
        //{
        //    get { return ddh; }
        //    set { ddh = value; }
        //}


        //double aH; //(искомая величина) дифф. эффективность

        //public double AH
        //{
        //    get { return aH; }
        //    set { aH = value; }
        //}
        //double b; //(искомая величина) aN*N

        //public double B
        //{
        //    get { return b; }
        //    set { b = value; }
        //}
        //double _tau; // (искомая величина) постоянная времени разогрева топлива 

        //public double tau
        //{
        //    get { return _tau; }
        //    set { _tau = value; }
        //}

        //double S; //S^2 - невязка

        //public double S1
        //{
        //    get { return S; }
        //    set { S = value; }
        //}

        /// <summary>
        /// Колличество перемещений
        /// </summary>
        int nPer;// колличество перемещений
        public int NPer
        {
            get { return nPer; }
            set { nPer = value; }
        }




        double[,] per; //массив времени начала и конца каждого шага
        /// <summary>
        /// Тут временя начала и конца каждого шага
        /// </summary>
        public double[,] Per
        {
            get { return per; }
            set { per = value; }
        }

        int[,] perIndex; //такой же массив но с индексами
        /// <summary>
        /// Тут индексы начала и конца каждого шага
        /// </summary>
        public int[,] PerIndex
        {
            get { return perIndex; }
            set { perIndex = value; }
        }

        double nt; //тепловая мощность реактора, очевидно что перед возмущением

        public double Nt
        {
            get { return nt; }
            set { nt = value; }
        }
        double t1k;//темпратура 1 контура

        public double T1k
        {
            get { return t1k; }
            set { t1k = value; }
        }

        //string nsuz; //№суз

        //public string Nsuz
        //{
        //    get { return nsuz; }
        //    set { nsuz = value; }
        //}
        //double h; //высота ОР СУЗ в процентах

        //public double H
        //{
        //    get { return h; }
        //    set { h = value; }
        //}
        //int nJ; //номер канала расчета
        //public int NJ
        //{
        //    get { return nJ; }
        //    set { nJ = value; }
        //}
        //double j1; //ток1
        //public double J1
        //{
        //    get { return j1; }
        //    set { j1 = value; }
        //}
        //double j2; //ток2
        //public double J2
        //{
        //    get { return j2; }
        //    set { j2 = value; }
        //}
        //double dTm; //время окончания раасчета
        //public double DTm
        //{
        //    get { return dTm; }
        //    set { dTm = value; }
        //}
        //double dH1; //перемещение текущей группа на СКОЛЬКО ШАГОВ
        //public double DH1
        //{
        //    get { return dH1; }
        //    set { dH1 = value; }
        //}
        //double dH2; //перемещение следующей группы на СКОЛЬКО ШАГОВ
        //public double DH2
        //{
        //    get { return dH2; }
        //    set { dH2 = value; }
        //}
        //double ro; //рассчетная реактивность
        //public double Ro
        //{
        //    get { return ro; }
        //    set { ro = value; }
        //}

        public OneVozmuchenieData()
        {         
        this.per = new double[50,2]; //тут лежать времена начала и конца каждого шага отмеченного пользователем
        this.perIndex = new int[50, 2]; //тут лежать индекса реактивности в которых начало и конец каждого шага возмущения, отмеченного пользователем
        }
    }
}
