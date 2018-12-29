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
            Console.WriteLine("1. 驗證\n2. 產生xml");
            string input = Console.ReadLine();
            while(true)
            {
                if(input.Equals("1"))
                {
                    XmlValidator validator = new XmlValidator();

                    validator.Validate(@"C:\Users\51942\Documents\會計處報稅\CbcXML_v1.0.1.xsd"
                        , @"C:\Users\51942\Documents\會計處報稅\cbc-report_new.xml"
                        , "urn:oecd:ties:cbc:v1");
                }
                else if (input.Equals("2"))
                {

                }
                else if (input.Equals("q"))
                {
                    break;
                }
            }
            Console.ReadLine();
        }
    }
}
