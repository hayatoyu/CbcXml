﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace CbcXml
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1. 驗證\n2. 產生xml");
            string input;
            while(true)
            {
                input = Console.ReadLine();
                if(input.Equals("1"))
                {
                    XmlValidator validator = new XmlValidator();

                    validator.Validate(@"C:\Users\51942\Documents\會計處報稅\CbcXML_v1.0.1.xsd"
                        , @"C:\Users\51942\Source\Repos\CbcXml\CbcXml\bin\Debug\cbc-report_20181229T18_21_07.xml"
                        , "urn:oecd:ties:cbc:v1");
                }
                else if (input.Equals("2"))
                {
                    string excelPath = @"C:\Users\51942\Documents\會計處報稅\106年度國別報告書.xls";

                    HSSFWorkbook wb = null;
                    ISheet sheet = null;
                    IRow row = null;
                    ICell cell = null;
                    int rowIndex = 1;
                    int numofSheet = 0;

                    XmlSerializer serializer;
                    XmlSerializerNamespaces myNamespace;
                    string xmlPath = System.Environment.CurrentDirectory + @"\cbc-report_" + DateTime.Now.ToString("yyyyMMdd'T'HH_mm_ss") + ".xml";

                    CBC_OECD cBC_OECD = new CBC_OECD();

                    using (FileStream fs = new FileStream(excelPath, FileMode.Open, FileAccess.Read))
                    {
                        wb = new HSSFWorkbook(fs);
                        numofSheet = wb.NumberOfSheets;
                        // 設定 Message Spec
                        #region 設定 MessageSpec
                        cBC_OECD.MessageSpec = new MessageSpec();
                        cBC_OECD.MessageSpec.SendingEntityIN = "03036306";
                        cBC_OECD.MessageSpec.TransmittingCountry = "TW";
                        cBC_OECD.MessageSpec.ReceivingCountry = "TW";
                        cBC_OECD.MessageSpec.MessageType = "CBC";
                        cBC_OECD.MessageSpec.Language = "EN";
                        cBC_OECD.MessageSpec.MessageRefId = "TW-03036306-" + DateTime.Today.ToString("yyyy") + "CBC" + "0001";
                        cBC_OECD.MessageSpec.MessageTypeIndic = "CBC401";
                        cBC_OECD.MessageSpec.ReportingPeriod = "2017-12-31";
                        cBC_OECD.MessageSpec.Timestamp = DateTime.Now.ToString("yyyyMMdd'T'HH:mm:ss.ssssss'Z'");
                        #endregion

                        // 設定 CbcBody/ReportingEntity
                        #region 設定 CbcBody/ReportingEntity
                        cBC_OECD.CbcBody = new CbcBody();
                        cBC_OECD.CbcBody.reportingEntity = new CbcBody.ReportingEntity();
                        cBC_OECD.CbcBody.reportingEntity.entity = new CbcBody.ReportingEntity.Entity();
                        cBC_OECD.CbcBody.reportingEntity.entity.tin = new CbcBody.ReportingEntity.Entity.TIN();
                        cBC_OECD.CbcBody.reportingEntity.entity.Name = new List<string>();
                        cBC_OECD.CbcBody.reportingEntity.entity.address = new CbcBody.ReportingEntity.Entity.Address();
                        cBC_OECD.CbcBody.reportingEntity.entity.address.addressFix = new CbcBody.ReportingEntity.Entity.Address.AddressFix();
                        cBC_OECD.CbcBody.reportingEntity.docSpec = new CbcBody.ReportingEntity.DocSpec();
                        cBC_OECD.CbcBody.reportingEntity.entity.ResCountryCode = "TW";
                        cBC_OECD.CbcBody.reportingEntity.entity.tin.issuedBy = "TW";
                        cBC_OECD.CbcBody.reportingEntity.entity.tin.tin = "03036306";
                        cBC_OECD.CbcBody.reportingEntity.entity.Name.Add("The Shanghai Commercial and Savings Bank,Ltd.");
                        cBC_OECD.CbcBody.reportingEntity.entity.address.legalAddressType = "OECD303";
                        cBC_OECD.CbcBody.reportingEntity.entity.address.CountryCode = "TW";
                        cBC_OECD.CbcBody.reportingEntity.entity.address.addressFix.Street = "2,Min Chuan E Rd.,Sec.1";
                        cBC_OECD.CbcBody.reportingEntity.entity.address.addressFix.City = "Taipei";
                        cBC_OECD.CbcBody.reportingEntity.ReportingRole = "CBC701";
                        cBC_OECD.CbcBody.reportingEntity.docSpec.DocTypeIndic = "OECD1";
                        cBC_OECD.CbcBody.reportingEntity.docSpec.DocRefId = "03036306-" + DateTime.Now.ToString("yyyyMMdd") + "-200";
                        #endregion

                        // 先處理 Summary
                        #region Summary
                        sheet = wb.GetSheetAt(0);
                        row = sheet.GetRow(rowIndex);
                        cBC_OECD.CbcBody.cbcReports = new List<CbcBody.CbcReports>();
                        while(row != null)
                        {
                            // CountryCode
                            cell = row.GetCell(0);
                            string countryCode = cell.StringCellValue;
                            var temp = new CbcBody.CbcReports();
                            temp.docSpec = new CbcBody.ReportingEntity.DocSpec();
                            temp.docSpec.DocTypeIndic = "OECD1";
                            temp.docSpec.DocRefId = "03036306-" + DateTime.Now.ToString("yyyyMMdd") + "-" + countryCode;
                            temp.ResCountryCode = countryCode;
                            temp.summary = new CbcBody.CbcReports.Summary();
                            temp.summary.revenues = new CbcBody.CbcReports.Summary.Revenues();
                            temp.summary.revenues.unrelated = new CbcBody.CbcReports.Summary.Revenues.Unrelated();
                            temp.summary.revenues.related = new CbcBody.CbcReports.Summary.Revenues.Related();
                            temp.summary.revenues.total = new CbcBody.CbcReports.Summary.Revenues.Total();
                            temp.summary.profitOrLoss = new CbcBody.CbcReports.Summary.ProfitOrLoss();
                            temp.summary.taxPaid = new CbcBody.CbcReports.Summary.TaxPaid();
                            temp.summary.taxAccrued = new CbcBody.CbcReports.Summary.TaxAccrued();
                            temp.summary.capital = new CbcBody.CbcReports.Summary.Capital();
                            temp.summary.earnings = new CbcBody.CbcReports.Summary.Earnings();
                            temp.summary.assets = new CbcBody.CbcReports.Summary.Assets();

                            // Unrelated
                            cell = row.GetCell(1);
                            if(cell != null)
                            {
                                cell.SetCellType(CellType.String);                                
                                temp.summary.revenues.unrelated.value = cell.StringCellValue;
                            }
                            else
                            {
                                temp.summary.revenues.unrelated.value = "0";
                            }
                            temp.summary.revenues.unrelated.currCode = Currency(countryCode);

                            // Related
                            cell = row.GetCell(2);
                            if(cell != null)
                            {
                                cell.SetCellType(CellType.String);
                                temp.summary.revenues.related.value = cell.StringCellValue;
                            }
                            else
                            {
                                temp.summary.revenues.related.value = "0";
                            }
                            temp.summary.revenues.related.currCode = Currency(countryCode);

                            // Total
                            cell = row.GetCell(3);
                            if (cell != null)
                            {
                                cell.SetCellType(CellType.String);
                                temp.summary.revenues.total.value = cell.StringCellValue;
                            }
                            else
                            {
                                temp.summary.revenues.total.value = "0";
                            }
                            temp.summary.revenues.total.currCode = Currency(countryCode);

                            // ProfitOrLoss
                            cell = row.GetCell(4);
                            if (cell != null)
                            {
                                cell.SetCellType(CellType.String);
                                temp.summary.profitOrLoss.value = cell.StringCellValue;
                            }
                            else
                            {
                                temp.summary.profitOrLoss.value = "0";
                            }
                            temp.summary.profitOrLoss.currCode = Currency(countryCode);

                            // TaxPaid
                            cell = row.GetCell(5);
                            if (cell != null)
                            {
                                cell.SetCellType(CellType.String);
                                temp.summary.taxPaid.value = cell.StringCellValue;
                            }
                            else
                            {
                                temp.summary.taxPaid.value = "0";
                            }
                            temp.summary.taxPaid.currCode = Currency(countryCode);

                            // TaxAccrued
                            cell = row.GetCell(6);
                            if (cell != null)
                            {
                                cell.SetCellType(CellType.String);
                                temp.summary.taxAccrued.value = cell.StringCellValue;
                            }
                            else
                            {
                                temp.summary.taxAccrued.value = "0";
                            }
                            temp.summary.taxAccrued.currCode = Currency(countryCode);

                            // Capital
                            cell = row.GetCell(7);
                            if (cell != null)
                            {
                                cell.SetCellType(CellType.String);
                                temp.summary.capital.value = cell.StringCellValue;
                            }
                            else
                            {
                                temp.summary.capital.value = "0";
                            }
                            temp.summary.capital.currCode = Currency(countryCode);

                            // Earnings
                            cell = row.GetCell(8);
                            if (cell != null)
                            {
                                cell.SetCellType(CellType.String);
                                temp.summary.earnings.value = cell.StringCellValue;
                            }
                            else
                            {
                                temp.summary.earnings.value = "0";
                            }
                            temp.summary.earnings.currCode = Currency(countryCode);

                            // NbEmployee
                            cell = row.GetCell(9);
                            if (cell != null)
                            {
                                cell.SetCellType(CellType.String);
                                temp.summary.NbEmployees = cell.StringCellValue;
                            }
                            else
                            {
                                temp.summary.NbEmployees = "0";
                            }

                            // Assets
                            cell = row.GetCell(10);
                            if (cell != null)
                            {
                                cell.SetCellType(CellType.String);
                                temp.summary.assets.value = cell.StringCellValue;
                            }
                            else
                            {
                                temp.summary.assets.value = "0";
                            }
                            temp.summary.assets.currCode = Currency(countryCode);

                            cBC_OECD.CbcBody.cbcReports.Add(temp);
                            rowIndex++;
                            row = sheet.GetRow(rowIndex);
                            
                        }
                        #endregion

                        /*
                         * Summary處理完應該每個國家會有一個CbcReports
                         * 接著對每個 CbcReports 加入 ConstEntities
                         */
                        #region 對每個CbcReports加入ConstEntities
                        for (int i = 1;i < numofSheet;i++)
                        {
                            string countryCode;
                            rowIndex = 1;
                            sheet = wb.GetSheetAt(i);
                            countryCode = sheet.SheetName;

                            var cbcReports = cBC_OECD.CbcBody.cbcReports.Where(c => c.ResCountryCode.Equals(countryCode)).FirstOrDefault();
                            cbcReports.constEntities = new List<CbcBody.CbcReports.ConstEntities>();
                            if(cbcReports != null)
                            {                                
                                row = sheet.GetRow(rowIndex);
                                while(row != null)
                                {
                                    var tempEntity = new CbcBody.ReportingEntity.Entity();
                                    var tempEntities = new CbcBody.CbcReports.ConstEntities();

                                    tempEntities.BizActivities = new List<string>();
                                    tempEntity.address = new CbcBody.ReportingEntity.Entity.Address();
                                    tempEntity.address.addressFix = new CbcBody.ReportingEntity.Entity.Address.AddressFix();
                                    tempEntity.Name = new List<string>();
                                    tempEntity.tin = new CbcBody.ReportingEntity.Entity.TIN();
                                    tempEntity._in = new CbcBody.ReportingEntity.Entity.IN();

                                    // TIN
                                    cell = row.GetCell(0);
                                    if(cell != null)
                                    {
                                        cell.SetCellType(CellType.String);                                        
                                        tempEntity.tin.tin = cell.StringCellValue;
                                    }
                                    else
                                    {
                                        tempEntity.tin.tin = "NOTIN";
                                    }
                                    tempEntity.tin.issuedBy = countryCode;

                                    // IN
                                    cell = row.GetCell(1);
                                    if(cell != null)
                                    {
                                        cell.SetCellType(CellType.String);
                                        tempEntity._in.issuedBy = countryCode;
                                        tempEntity._in.value = cell.StringCellValue;                                        
                                    }

                                    // Name
                                    cell = row.GetCell(2);
                                    cell.SetCellType(CellType.String);
                                    tempEntity.Name.Add(cell.StringCellValue);

                                    // Street
                                    cell = row.GetCell(3);                                    
                                    tempEntity.address.legalAddressType = "OECD303";
                                    tempEntity.address.CountryCode = countryCode;
                                    if(cell != null)
                                    {
                                        cell.SetCellType(CellType.String);
                                        tempEntity.address.addressFix.Street = cell.StringCellValue;
                                    }

                                    // Building
                                    cell = row.GetCell(4);
                                    if(cell != null)
                                    {
                                        cell.SetCellType(CellType.String);
                                        tempEntity.address.addressFix.BuildingIdentifier = cell.StringCellValue;
                                    }

                                    // Suite
                                    cell = row.GetCell(5);
                                    if(cell != null)
                                    {
                                        cell.SetCellType(CellType.String);
                                        tempEntity.address.addressFix.SuiteIdentifier = cell.StringCellValue;
                                    }

                                    // Floor
                                    cell = row.GetCell(6);
                                    if(cell != null)
                                    {
                                        cell.SetCellType(CellType.String);
                                        tempEntity.address.addressFix.FloorIdentifier = cell.StringCellValue;
                                    }

                                    // DistrictName
                                    cell = row.GetCell(7);
                                    if(cell != null)
                                    {
                                        cell.SetCellType(CellType.String);
                                        tempEntity.address.addressFix.DistrictName = cell.StringCellValue;
                                    }

                                    // POB
                                    cell = row.GetCell(8);
                                    if(cell != null)
                                    {
                                        cell.SetCellType(CellType.String);
                                        tempEntity.address.addressFix.POB = cell.StringCellValue;
                                    }

                                    // PostCode
                                    cell = row.GetCell(9);
                                    if(cell != null)
                                    {
                                        cell.SetCellType(CellType.String);
                                        tempEntity.address.addressFix.PostCode = cell.StringCellValue;
                                    }

                                    // City
                                    cell = row.GetCell(10);
                                    if(cell != null)
                                    {
                                        cell.SetCellType(CellType.String);
                                        tempEntity.address.addressFix.City = cell.StringCellValue;
                                    }

                                    // CountrySubEntity
                                    cell = row.GetCell(11);
                                    if(cell != null)
                                    {
                                        cell.SetCellType(CellType.String);
                                        tempEntity.address.addressFix.CountrySubentity = cell.StringCellValue;
                                    }

                                    // BizActivities
                                    cell = row.GetCell(12);
                                    if(cell != null)
                                    {
                                        cell.SetCellType(CellType.String);
                                        tempEntities.BizActivities.Add(cell.StringCellValue);                                        
                                    }

                                    // OtherEntityInfo
                                    cell = row.GetCell(13);
                                    if(cell != null)
                                    {
                                        cell.SetCellType(CellType.String);
                                        tempEntities.OtherEntityInfo = cell.StringCellValue;
                                    }

                                    // IncorpCountryCode
                                    tempEntities.IncorpCountryCode = countryCode;
                                    tempEntities.constEntity = tempEntity;

                                    cbcReports.constEntities.Add(tempEntities);

                                    rowIndex++;
                                    row = sheet.GetRow(rowIndex);
                                   
                                }
                                
                                
                                
                                
                            }
                        }
                        #endregion

                        wb.Close();
                        fs.Close();
                    }

                    serializer = new XmlSerializer(typeof(CBC_OECD));
                    myNamespace = new XmlSerializerNamespaces();
                    myNamespace.Add("cbc", "urn:oecd:ties:cbc:v1");
                    myNamespace.Add("stf", "urn:oecd:ties:stf:v4");
                    myNamespace.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");

                    try
                    {
                        using (StreamWriter sw = new StreamWriter(xmlPath, false, Encoding.UTF8))
                        {
                            serializer.Serialize(sw, cBC_OECD, myNamespace);
                        }
                        Console.WriteLine("Xml 序列化完成");
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    

                }
                else if (input.Equals("q"))
                {
                    break;
                }
            }
            Console.ReadLine();
        }

        private static string Currency(string countryCode)
        {
            string currency = string.Empty;
            switch(countryCode)
            {
                case "TW":
                    currency = "TWD";
                    break;
                case "CN":
                    currency = "CNY";
                    break;
                case "LR":
                    currency = "LRD";
                    break;
                case "US":
                    currency = "USD";
                    break;
                case "GB":
                    currency = "GBP";
                    break;
                case "PA":
                    currency = "PAB";
                    break;
                case "KY":
                    currency = "KYD";
                    break;
                case "SG":
                    currency = "SGD";
                    break;
                case "VN":
                    currency = "VND";
                    break;
                case "HK":
                    currency = "HKD";
                    break;
            }
            return currency;
        }
    }
}
