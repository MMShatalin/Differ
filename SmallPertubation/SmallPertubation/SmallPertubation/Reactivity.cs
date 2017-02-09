using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallPertubation
{
    /// <summary>
    /// класс для расчета реактивности
    /// </summary>
    class Reactivity
    {
        private double[] Dv;
        private double sai;
        private double timeOld;
        private double jOld;
        private Boolean error;
        public double Ro; ////возвращает реактивность, рассчитанную в Беттах
        public double[] BI;    
        public void ClearRo()
        {
            this.error = false;
            for (int i = 0; i < 6; i++)
            {
                this.Dv[i] = this.jOld;
            }        
        }
        public Reactivity()
        {
            this.Dv = new double[6];
            this.BI = new double[6];
        }
        public void AddFirstData(double TimeNow, double J_Now)
        {     
            this.timeOld = TimeNow;
            this.jOld = J_Now;
            this.ClearRo();
            this.sai = 0;
            for (int i = 0; i < 6; i++)
            {
                this.sai = this.sai + MyConst.Rect.b[i];
            }              
        }
        public void AddData(double TimeNow, double J_Now)
        {           
            double dT, l_t;
            int length = 6;
            double[] q_Ro = new double[6];
            double[] a_Ro = new double[6];
            if (TimeNow > timeOld)
            {
                dT = TimeNow - timeOld;
            }
            else
            {
                this.error = true;
                dT = 0.1;            
            }
            timeOld = TimeNow;            
            for (int i = 0; i < length; i++)
            {
                l_t = MyConst.Rect.l[i] * dT;
                q_Ro[i] = Math.Exp(-l_t);
                a_Ro[i] = (1 - q_Ro[i]) / l_t;
            }
            Ro = 0;
            for (int i = 0; i < length; i++)
            {
                Dv[i] = Dv[i] * q_Ro[i] + J_Now * (1 - a_Ro[i]) + jOld * (a_Ro[i] - q_Ro[i]);
                Ro = Ro + Dv[i] * MyConst.Rect.b[i] / sai;
            }
            Ro = 1 - Ro / J_Now;
            jOld = J_Now;      
        }
    }
}
