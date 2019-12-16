using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CbcXml
{
    class NameSpaces
    {
        public CbcNameSpace[] NameSpaceList;

        public class CbcNameSpace
        {
            public string CountryCode { get; set; }
            public string Namespace { get; set; }
        }
    }
}
