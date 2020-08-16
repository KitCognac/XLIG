using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLib
{
    public class Region
    {
        public Region(string regionName)
        {
            this.RegionName = regionName;
        }

        public string RegionName { get; private set; }

        readonly List<State> _states = new List<State>();
        public List<State> States
        {
            get { return _states; }
        }
    }
}
