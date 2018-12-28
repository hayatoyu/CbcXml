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
    [XmlRoot(Namespace = "CBC")]
    [XmlType(Namespace ="CBC",TypeName ="CBC_OECD")]
    public class MessageSpec
    {
        public string SendingEntityIN { get; set; }
        public string TransmittingCountry { get; set; }
        public string ReceivingCountry { get; set; }
        public string MessageType { get; set; }
        public string Language { get; set; }
        public string Warning { get; set; }     // Optional
        public string Contact { get; set; }     // Optional
        public string MessageRefId { get; set; }
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

        public string Validation()
        {
            StringBuilder stbr = new StringBuilder();
            Commons commons = Commons.getInstance();
            DateTime date = new DateTime();
            

            // SendingEntityIN
            if (string.IsNullOrEmpty(SendingEntityIN))
                stbr.AppendLine("MessageSpecSendingEntityIN 為 空");

            // TransmittingCountry
            if (!commons.countries.Exists(x => x.Equals(TransmittingCountry)))
                stbr.AppendLine("TransmittingCountry 不在列表中");

            // ReceivingCountry
            if (!commons.countries.Exists(x => x.Equals(ReceivingCountry)))
                stbr.AppendLine("ReceivingCountry 不在列表中");

            // MessageType
            if (!MessageType.Equals("CBC"))
                stbr.AppendLine("MessageType 只能為 CBC");

            // Language
            if (!Language.Equals("EN") && !Language.Equals("ZH"))
                stbr.AppendLine("Language 不為 EN 或 ZH");

            // MessageTypeIndic
            if (!MessageTypeIndic.Equals("CBC401") && !MessageTypeIndic.Equals("CBC402"))
                stbr.AppendLine("MessageTypeIndic 只能為 CBC401 或 CBC402");

            // ReportingPeriod
            if (!DateTime.TryParse(ReportingPeriod, out date))
                stbr.AppendLine("ReportingPeriod 不是合法日期格式");

            // TimeStamp
            if (!DateTime.TryParse(Timestamp, out date))
                stbr.AppendLine("Timestamp 不是合法日期格式");

            return stbr.ToString();
        }
    }
}
