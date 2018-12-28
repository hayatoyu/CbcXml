using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CbcXml
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlValidator validator = new XmlValidator();

            validator.Validate(@"C:\Users\51942\Downloads\CbC schema v1.0.1\CbcXML_v1.0.1.xsd"
                , @"C:\Users\51942\Documents\會計處報稅\cbc-report_new.xml"
                , "urn:oecd:ties:cbc:v1");
        }
    }
}
