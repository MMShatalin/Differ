using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistantPhysics
{

    public class OneSensor
    {
        /// <summary>
        /// индивидуальный идентификатор
        /// </summary>
        public string KksName;
        public string imya_parametra;


        public OneSensor(string sss)
        {

            OneRec OneRecord = new OneRec();
            OneRecord.dt = Convert.ToDateTime(sss.Split('\t')[0].Replace('.', '/').Replace(',', '.').Trim());//line.Split('\t')[0]//для вывода с милисекундами
            OneRecord.value = double.Parse(sss.Split('\t')[2].Replace('.', ',').Replace('-', '0').Trim());//line.Split('\t')[2]//и прочерками, но в стрингах

            this.values = new List<OneRec>();
            this.KksName = sss.Split('\t')[1];
            this.values.Add(OneRecord);
        }



        public OneSensor(string sss, double ttt)
        {

            OneRec OneRecord = new OneRec();
            OneRecord.dt = Convert.ToDateTime(sss.Split('\t')[0].Replace('.', '/').Replace(',', '.').Trim());//line.Split('\t')[0]//для вывода с милисекундами
            OneRecord.value = ttt;
            this.values = new List<OneRec>();
            this.KksName = sss.Split('\t')[1];
            this.values.Add(OneRecord);
        }

        public OneSensor()
        {


        }
        /// <summary>
        /// Его наборы значений
        /// </summary>
        public List<OneRec> values;
    }
}
