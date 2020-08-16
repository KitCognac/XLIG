using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLib
{
    public class City
    {
        public City(string cityName)
        {
            this.CityName = cityName;
        }

        public string CityName { get; private set; }
    }
}
