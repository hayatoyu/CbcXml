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
        public ReportingEntity reportingEntity { get; set; }
        public CbcReports cbcReports { get; set; }
        public AdditionalInfo addtionalInfo { get; set; }

        public class ReportingEntity
        {
            public Entity entity { get; set; }
            public string ReportingRole { get; set; }
            public DocSpec docSpec { get; set; }

            public class Entity
            {
                public string ResCountryCode { get; set; }
                
                public TIN tin { get; set; }

                public List<string> Name { get; set; }

                public Address address { get; set; }

                public class TIN
                {
                    [XmlAttribute]
                    public string issuedBy { get; set; }

                    [XmlText]
                    public string tin { get; set; }
                }
                
                public class Address
                {
                    [XmlAttribute]
                    public string legalAddressType { get; set; }

                    public string CountryCode { get; set; }

                    public AddressFix addressFix { get; set; }

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
            public ReportingEntity.DocSpec docSpec { get; set; }

            public string ResCountryCode { get; set; }

            public Summary summary { get; set; }

            public List<ConstEntities> constEntities {get;set;}

            public class Summary
            {
                public Revenues revenues { get; set; }                
                
                public ProfitOrLoss profitOrLoss { get; set; } 

                public TaxPaid taxPaid { get; set; }

                public TaxAccrued taxAccrued { get; set; }

                public Capital capital { get; set; }

                public Earnings earnings { get; set; }

                public NbEmployees nbEmployees { get; set; }
                
                public Assets assets { get; set; }

                public class CurrCode
                {
                    [XmlAttribute]
                    public string currCode { get; set; }
                }
                public class Revenues
                {
                    public Unrelated unrelated { get; set; }

                    public Related related { get; set; }

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
                public class NbEmployees : CurrCode
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

        }
    }
}
