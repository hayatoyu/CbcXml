using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CbcXml
{
    [XmlRoot(Namespace = "urn:oecd:ties:cbc:v1")]
    public class CBC_OECD
    {
        [XmlAttribute(AttributeName = "schemaLocation",Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string schemaLocation = "urn:oecd:ties:cbc:v1 CbcXML_v1.0.xsd";

        [XmlAttribute(AttributeName = "version")]
        public string version = "String";

        public MessageSpec MessageSpec { get; set; }

        public CbcBody CbcBody { get; set; }
    }
}
