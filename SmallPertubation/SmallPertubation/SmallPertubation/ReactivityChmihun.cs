using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallPertubation
{
    /// <summary>
    /// этот класс для рассчета реактивности по исходникам Чмыхуна от Бушерского АПИКА
    /// </summary>
    class ReactivityChmihun
    {
        double ro; //reactivity 

        public double Ro //возвращает реактивность, рассчитанную в Беттах
        {
            get { return ro; }
            set { ro = value; }
        }
        double tokOld;
        double timeOld;
        //var
        //   E,D,LT :array[1..6] of  single;
        //   S,DT:single;
        //   i:integer;
        double[] q_Ro;
        double[] a_Ro;
       // double[] LT;
        double[] FPrev;
        double[] FNext;
        double l_t;
        double DT;
        public ReactivityChmihun()
        {
            this.a_Ro = new double[6];
            this.q_Ro = new double[6];
        //    this.LT = new double[6];
            this.FPrev = new double[6];
            this.FNext = new double[6];
        }
        public void AddFirstData(double time, double tok)
        {
            this.tokOld = tok;
            this.timeOld = time;
            for (int i = 0; i < 6; i++)
            {
                FPrev[i] = tok;
            }
        }
        public void AddData(double time, double tok)
        {            
            double delta_t = time - timeOld;
            for (int i = 0; i < 6; i++)
            {
                          l_t=MyConst.Rect.l[i]*delta_t;
                          q_Ro[i]=1/Math.Exp(l_t);
                          a_Ro[i] = (1.0 - q_Ro[i]) / l_t;
            }
            DT = tok - tokOld;
            ro = 0;
            for (int i = 0; i < 6; i++)
            {
                FNext[i] = FPrev[i] * q_Ro[i] - DT * a_Ro[i] - tokOld * q_Ro[i] + tok;
                FPrev[i] = FNext[i];
                ro = ro + FNext[i] * MyConst.Rect.b[i];
            }
            if (tok!=0)
            {
              this.ro = 1 - ro / tok;  
            }            
            tokOld = tok;
            timeOld = time;
        }
        //public void AddData(double TimeNow, double J_Now)
        //{
        //    double dT, l_t;
        //    int length = 6;
        //    double[] q_Ro = new double[6];
        //    double[] a_Ro = new double[6];
        //    if (TimeNow > timeOld)
        //    {
        //        dT = TimeNow - timeOld;
        //    }
        //    else
        //    {
        //        this.error = true;
        //        dT = 0.1;
        //    }
        //    timeOld = TimeNow;
        //    for (int i = 0; i < length; i++)
        //    {
        //        l_t = MyConst.Rect.l[i] * dT;
        //        q_Ro[i] = Math.Exp(-l_t);
        //        a_Ro[i] = (1 - q_Ro[i]) / l_t;
        //    }
        //    Ro = 0;
        //    for (int i = 0; i < length; i++)
        //    {
        //        Dv[i] = Dv[i] * q_Ro[i] + J_Now * (1 - a_Ro[i]) + jOld * (a_Ro[i] - q_Ro[i]);
        //        Ro = Ro + Dv[i] * MyConst.Rect.b[i] / sai;
        //    }
        //    Ro = 1 - Ro / J_Now;
        //    jOld = J_Now;
        //}
        //procedure TForm1.RaschetReakt1;        
        //begin
        //           for i:=1 to 6 do
        //           begin
        //            LT[i]:=LMDA[i]*delta_t;
        //            E[i]:=1/exp(LT[i]);
        //            D[i]:=(1.0-E[i])/LT[i];
        //           end;
        ////*********************************************************************
        //        DT:=tok1-tok1do;//  ౨᡹殨塬ﺭﲲ衭ࡤᮭﬠ顣半        S:=0;
        //    for i:=1 to 6 do
        //    begin
        //     FNext1[i]:=FPrev1[i]*E[i] - DT*D[i] - tok1do*E[i] + tok1;
        //     FPrev1[i]:=FNext1[i];
        //     S:=S+FNext1[i]*BETA[i];
        //     end;
        //if tok1 <> 0 then r1:= (1.0 - S/tok1)  ;
        //tok1do:=tok1 ;
        //end;        
    }
}
