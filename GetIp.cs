using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ver2._1
{
    class GetIp
    {
        public class root
        {
            public String status { get; set; }
            public String country { get; set; }
            public string countryCode { get; set; }
            public String city { get; set; }
            public double lat { get; set; }
            public double lon { get; set; }
            public string org { get; set; } //供應商
            public String query { get; set; }
        }
    }
}
