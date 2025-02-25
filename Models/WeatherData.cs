using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    class WeatherData
    {
        public float Temp { get; set; }
        public Weather[] Weathers { get; set; }
    }
}
