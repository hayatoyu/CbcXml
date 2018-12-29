using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.IO;

namespace CbcXml
{
    class XmlValidator
    {
        private bool isSuccess = true;
        StringBuilder stbr = new StringBuilder();
        
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
            else
            {
                // 將錯誤訊息寫入txt檔案
                string path = System.Environment.CurrentDirectory + @"\" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
                using (StreamWriter outputFile = new StreamWriter(path, false, Encoding.UTF8))
                {
                    outputFile.WriteLine(stbr.ToString());
                }
            }
        }

        public void xmlSettingsValidationEventHandler(object sender,ValidationEventArgs e)
        {
            if(e.Severity == XmlSeverityType.Warning)
            {
                stbr.AppendLine("WARNING: ").AppendLine(e.Message);                
                isSuccess = false;
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                stbr.AppendLine("ERROR: ").AppendLine(e.Message);                
                isSuccess = false;
            }
            
        }
    }
}
