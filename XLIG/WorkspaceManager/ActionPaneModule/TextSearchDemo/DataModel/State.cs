using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLib
{
    public class State
    {
        public State(string stateName)
        {
            this.StateName = stateName;
        }

        readonly List<City> _cities = new List<City>();
        public List<City> Cities
        {
            get { return _cities; }
        }

        public string StateName { get; private set; }
    }
}
