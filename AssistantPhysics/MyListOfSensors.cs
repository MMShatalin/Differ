using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistantPhysics
{
    class MyListOfSensors : List<OneSensor>
    {

        public OneSensor getSensorByKKSName(string kks)
        {

            foreach (OneSensor item in this)
            {
                if (item.KksName==kks)
                {
                    return item;
                }
            }

            return null;       
        }
    }
}
