using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallPertubation
{
    static class MyConst
    {

        public static class Rect
        {

            /// <summary>
            /// возьмеме Бушерский вариант, но нужно перебить
            /// </summary>
            public static double Beff = 0.74;






            //BETA : array[1..6] of single = (0.033,0.219,0.196,0.395,0.115,0.042);
            //LMDA : array[1..6] of single = (0.0124,0.0305,0.111,0.301,1.14,3.01);



            /// <summary>
            /// Значения долей запаздывающих нейтронов в каждой группе
            /// </summary>
            public static double[] b = { 0.033, 0.219, 0.196, 0.395, 0.115, 0.042 };

            /// <summary>
            /// лямбды для каждой группы 
            /// </summary>
            public static double[] l = { 0.0124, 0.0305, 0.111, 0.301, 1.14, 3.01 };



            ///у чмыхуна
            ///0.033	0.219	0.196	0.395	0.115	0.042
            ///0.0124	0.0305	0.111	0.301	1.14	3.01



            /// <summary>
            /// тут храниться синимум и максимум шкалы реактивности
            /// </summary>
            public static double[,] MinMax = new double[2, 2] { { -0.02, 0.02 }, { 50, 100 } };

            /// <summary>
            /// тут порядковый номер параметра в массиве 
            /// </summary>
            public enum AxisParam { react, Hsuz };


            //public static PointF MinMax = new PointF() {X = (float)-0.02, Y= (float)0.02};

        }



        public static int delayTime = 200;



        public static string[] XintervalNames = { "5 сек", "10 сек", "30 сек", "1 мин", "5 мин", "30 мин" };
        public static int[] XintervalVal = { 5, 10, 30, 60, 300, 1800 };

        /// <summary>
        /// Индексы в сигналов в наших массивах
        /// ЭТО МОЖНО БУДЕТ ПОТОМ ПЕРЕПИСАТЬ КАК НИБУДЬ ПО ИЗЯЩНЕЕ
        /// </summary>
        public static int tok1 = 1;
        public static int tok2 = 2;

        public static int r1 = 3;
        public static int r2 = 4;
        public static int RC = 5;

        public static int H10 = 10;
        public static int H9 = 11;
        public static int H8 = 12;


        //тепловая мощность лежит в 18 индексеа
        public static int N1k = 18;





    }
}
