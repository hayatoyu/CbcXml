using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;


namespace CbcXml
{
    [XmlRoot(Namespace = "urn:oecd:ties:cbc:v1")]
    [XmlType(Namespace = "urn:oecd:ties:cbc:v1")]
    public class MessageSpec
    {
        public string SendingEntityIN { get; set; }
        public string TransmittingCountry { get; set; }
        public string ReceivingCountry { get; set; }
        public string MessageType { get; set; }
        public string Language { get; set; }
        public string Warning { get; set; }     // Optional
        public string Contact { get; set; }     // Optional
        public string MessageRefId
        {
            get
            {
                return "GB" + ReportingPeriod.Substring(0, 4) + "GB" + SendingEntityIN + MessageTypeIndic + DateTime.Now.ToString("yyyyMMdd'T'hhmmss") + "001";
            }
            set
            {

            }
        }
        public string MessageTypeIndic { get; set; }
        public string ReportingPeriod { get; set; }
        public string Timestamp { get; set; }

        /*
         * MessageType 只能是 CBC
         * Language 英文是 EN，中文是 ZH
         * MessageTypeIndic 只有兩種
         *      CBC401 新資料
         *      CBC402 修正之前的資料
         * ReportingPeriod
         *      YYYY-MM-DD
         * Timestamp
         *      YYYY-MM-DD'T'HH:mm:ss(不確定有沒有帶毫秒和'Z')
         */
        
    }
}
