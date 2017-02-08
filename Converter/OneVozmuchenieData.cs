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
        int I_N; //тут индекс начала возмущений

        public int Index_N
        {
            get { return I_N; }
            set { I_N = value; }
        }

        /// <summary>
        /// индекс окончания текущего возмущения
        /// </summary>
        int I_K; //тут индекс конца возмущений

        public int Index_K
        {
            get { return I_K; }
            set { I_K = value; }
        }

        /// <summary>
        /// Колличество перемещений
        /// </summary>
        int n_Per;// колличество перемещений
        public int N_Per
        {
            get { return n_Per; }
            set { n_Per = value; }
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

        public OneVozmuchenieData()
        {
            this.per = new double[50,2]; //тут лежать времена начала и конца каждого шага отмеченного пользователем
            this.perIndex = new int[50, 2]; //тут лежать индекса реактивности в которых начало и конец каждого шага возмущения, отмеченного пользователем
        }
    }
}
