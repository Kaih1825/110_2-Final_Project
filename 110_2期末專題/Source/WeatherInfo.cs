using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ver2._1
{
    class WeatherInfo
    {
        public class weather
        {
            public String description { get; set; }
            public String icon { get; set; }
        }

        public class main
        {
            public Double temp { get; set; }
            public Double feels_like { get; set; }
            public Double temp_min { get; set; }
            public Double temp_max { get; set; }
            public Double humidity { get; set; }
        }


        public class root
        {
            public List<weather> weather { get; set; }
            public main main { get; set; }
            public string name { get; set; }
        }
    }
}
