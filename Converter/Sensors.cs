using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    public struct Record
    {
        public DateTime DateTime;
        public double Value;
        public double ValueTimeForDAT;
    }

    public class Sencors: IComparable
    {
        int L;
        public string KKS_Name { get; set; }
        public List<Record> MyListRecordsForOneKKS;
        public Sencors()
        {
            this.MyListRecordsForOneKKS = new List<Record>();
            L = this.MyListRecordsForOneKKS.Count;

        }

        public Sencors(string sss)
        {
            Record OneRecord = new Record();
            OneRecord.DateTime = Convert.ToDateTime(sss.Split('\t')[0].Replace('.', '/').Replace(',', '.').Trim());//line.Split('\t')[0]//для вывода с милисекундами
            OneRecord.Value = double.Parse(sss.Split('\t')[2].Replace('.', ',').Replace('-', '0').Trim());//line.Split('\t')[2]//и прочерками, но в стрингах
            
            this.MyListRecordsForOneKKS = new List<Record>();
            this.KKS_Name = sss.Split('\t')[1];
            this.MyListRecordsForOneKKS.Add(OneRecord);
        }
        public int CompareTo(object other)
        {
            var oth = other as Sencors;
            return this.KKS_Name.CompareTo(oth.KKS_Name);
        }

        public static int getOneKKSByIndex(string checkName, List<Sencors> MyList)
        {
            int index = 0;
            for (int i = 0; i < MyList.Count; i++)
            {
                if (checkName == MyList[i].KKS_Name )
                {
                    index = i;
                }
            }

            return index;
        }
    }
}
