using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace CbcXml
{
    class XmlValidator
    {
        private bool isSuccess = true;
        
        public void Validate(string xsdPath,string xmlPath,string nameSpace)
        {
            XmlReaderSettings settings = new XmlReaderSettings();            
            settings.Schemas.Add(nameSpace, XmlReader.Create(xsdPath));
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationEventHandler += new ValidationEventHandler(xmlSettingsValidationEventHandler);

            XmlReader reader = XmlReader.Create(xmlPath, settings);

            while(reader.Read()) { }

            if (isSuccess)
                Console.WriteLine("驗證成功");
        }

        public void xmlSettingsValidationEventHandler(object sender,ValidationEventArgs e)
        {
            if(e.Severity == XmlSeverityType.Warning)
            {
                Console.WriteLine("WARNING: ");
                Console.WriteLine(e.Message);
                isSuccess = false;
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                Console.WriteLine("ERROR: ");
                Console.WriteLine(e.Message);
                isSuccess = false;
            }
            
        }
    }
}
