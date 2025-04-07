using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenSpil.Model
{
    public class ConditionHolder
    {
        // property med get og set
       public ICollection<Condition> ConditionList {  get; private set; }

        // Constructor der sætter værdierne
        public ConditionHolder() 
        {
            ConditionList = new List<Condition>((Condition[])Enum.GetValues(typeof(Condition)));

        }

    }
}

