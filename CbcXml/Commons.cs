using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CbcXml
{
    class Commons
    {
        private static Commons commons = new Commons();

        public List<string> countries { get; set; }

        private Commons()
        {
            string[] countrylist = new string[] {"TW","CN","LR","US","GB","PA","KY","SG","VN","HK"};
            countries.AddRange(countrylist);
        }

        public static Commons getInstance()
        {
            return commons;
        }
    }
}
