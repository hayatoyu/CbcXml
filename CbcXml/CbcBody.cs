using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CbcXml
{
    public class CbcBody
    {
        [XmlElement(ElementName = "ReportingEntity")]
        public ReportingEntity reportingEntity { get; set; }

        [XmlElement(ElementName = "CbcReports")]
        public List<CbcReports> cbcReports { get; set; }

        [XmlElement(ElementName = "AdditionalInfo")]
        public List<AdditionalInfo> addtionalInfo { get; set; }

        public class ReportingEntity
        {
            [XmlElement(ElementName = "Entity")]
            public Entity entity { get; set; }

            public string ReportingRole { get; set; }

            [XmlElement(ElementName = "DocSpec")]
            public DocSpec docSpec { get; set; }

            public class Entity
            {
                public string ResCountryCode { get; set; }

                [XmlElement(ElementName = "TIN")]
                public TIN tin { get; set; }

                public IN _in {get;set;}
                
                [XmlElement(ElementName = "Name")]
                public List<string> Name { get; set; }

                [XmlElement(ElementName = "Address")]
                public Address address { get; set; }

                public class TIN
                {
                    [XmlAttribute]
                    public string issuedBy { get; set; }

                    [XmlText]
                    public string tin { get; set; }
                }

                public class IN
                {
                    [XmlAttribute]
                    public string issuedBy { get; set; }

                    [XmlAttribute]
                    public string INType { get; set; }

                    [XmlText]
                    public string value { get; set; }
                }
                
                public class Address
                {
                    [XmlAttribute]
                    public string legalAddressType { get; set; }

                    public string CountryCode { get; set; }

                    [XmlElement(ElementName = "AddressFix")]
                    public AddressFix addressFix { get; set; }

                    public string AddressFree { get; set; }

                    public class AddressFix
                    {
                        public string Street { get; set; }
                        public string BuildingIdentifier { get; set; }
                        public string SuiteIdentifier { get; set; }
                        public string FloorIdentifier { get; set; }
                        public string DistrictName { get; set; }
                        public string POB { get; set; }
                        public string PostCode { get; set; }
                        public string City { get; set; }
                        public string CountrySubentity { get; set; }
                    }
                }
            }

            [XmlType(Namespace = "urn:oecd:ties:stf:v4")]
            public class DocSpec
            {
                public string DocTypeIndic { get; set; }
                public string DocRefId { get; set; }
            }
        }

        public class CbcReports
        {
            [XmlElement(ElementName = "DocSpec")]
            public ReportingEntity.DocSpec docSpec { get; set; }

            public string ResCountryCode { get; set; }

            [XmlElement(ElementName = "Summary")]
            public Summary summary { get; set; }

            [XmlElement(ElementName = "ConstEntities")]
            public List<ConstEntities> constEntities {get;set;}

            public class Summary
            {
                [XmlElement(ElementName = "Revenues")]
                public Revenues revenues { get; set; }                
                
                [XmlElement(ElementName = "ProfitOrLoss")]
                public ProfitOrLoss profitOrLoss { get; set; } 

                [XmlElement(ElementName = "TaxPaid")]
                public TaxPaid taxPaid { get; set; }

                [XmlElement(ElementName = "TaxAccrued")]
                public TaxAccrued taxAccrued { get; set; }

                [XmlElement(ElementName = "Capital")]
                public Capital capital { get; set; }

                [XmlElement(ElementName = "Earnings")]
                public Earnings earnings { get; set; }
                
                public string NbEmployees { get; set; }
                
                [XmlElement(ElementName = "Assets")]
                public Assets assets { get; set; }

                public class CurrCode
                {
                    [XmlAttribute]
                    public string currCode { get; set; }
                }
                public class Revenues
                {
                    [XmlElement(ElementName = "Unrelated")]
                    public Unrelated unrelated { get; set; }

                    [XmlElement(ElementName = "Related")]
                    public Related related { get; set; }

                    [XmlElement(ElementName = "Total")]
                    public Total total { get; set; }

                    public class Unrelated : CurrCode
                    {                        
                        [XmlText]
                        public string value { get; set; }
                    }

                    public class Related : CurrCode
                    {                        
                        [XmlText]
                        public string value { get; set; }
                    }

                    public class Total : CurrCode
                    {                        
                        [XmlText]
                        public string value { get; set; }
                    }
                }
                public class ProfitOrLoss : CurrCode
                {
                    [XmlText]
                    public string value { get; set; }
                }
                public class TaxPaid : CurrCode
                {
                    [XmlText]
                    public string value { get; set; }
                }
                public class TaxAccrued : CurrCode
                {
                    [XmlText]
                    public string value;
                }
                public class Capital : CurrCode
                {
                    [XmlText]
                    public string value { get; set; }
                }
                public class Earnings : CurrCode
                {
                    [XmlText]
                    public string value { get; set; }
                }
                
                public class Assets : CurrCode
                {
                    [XmlText]
                    public string value { get; set; }
                }
            }

            public class ConstEntities
            {
                [XmlElement(ElementName = "ConstEntity")]
                public ReportingEntity.Entity constEntity { get; set; }

                public string IncorpCountryCode { get; set; }

                [XmlElement(ElementName = "BizActivities")]
                public List<string> BizActivities { get; set; }

                public string OtherEntityInfo { get; set; }


            }
        }

        public class AdditionalInfo
        {
            public ReportingEntity.DocSpec DocSpec { get; set; }
            public string OtherInfo { get; set; }
            public string ResCountryCode { get; set; }
            public string SummaryRef { get; set; }
        }
    }
}
