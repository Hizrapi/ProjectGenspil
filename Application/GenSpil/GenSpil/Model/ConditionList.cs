using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenSpil.Model
{

    /// <summary>
    /// Singleton class for handling a list of Conditions.
    /// </summary>


    public class ConditionList
    {
        private static ConditionList? _instance;
        private static readonly object padlock = new object();

        public static ConditionList Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new ConditionList();
                    }
                    return _instance;
                }

            }

        } ///> Singleton instance of the Conditionlist
        public List<Condition> _boardGames = new List<Condition>(); // List of Conditions 
    }

}
