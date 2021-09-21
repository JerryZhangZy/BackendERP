using Digitbridge.QuickbooksOnline.Db;
using Digitbridge.QuickbooksOnline.Db.Infrastructure;
using Digitbridge.QuickbooksOnline.Db.Model;
using Digitbridge.QuickbooksOnline.QuickBooksConnection;
using Digitbridge.QuickbooksOnline.QuickBooksConnection.Model;
using Intuit.Ipp.Data;
using Microsoft.Azure.Documents.SystemFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UneedgoHelper.DotNet.Common;
using UneedgoHelper.DotNet.Data.MsSql;

namespace Digitbridge.QuickbooksOnline.DbToQuickbooksMdl.Infrastructure
{
    public class DbToQuickbooksUtility
    {
        public static async Task<string> GetQboCustomerId(QboIntegrationSetting setting, QboSalesOrder orderHeader, MsSqlUniversal msSql, QboDbConfig dbConfig, QboUniversal qboUniversal)
        {
            string customerId = "";
            try
            {
                Command command = new Command(setting.MasterAccountNum, setting.ProfileNum);

                switch (setting.QboCustomerCreateRule)
                {
                    case (int)CustomerCreateRule.PerMarketPlace:
                        QboChnlAccSettingDb qboChnlAccSettingDb = new QboChnlAccSettingDb(dbConfig.QuickBooksChannelAccSettingTableName, msSql);
                        string settingCustomerId = await qboChnlAccSettingDb.GetChnlCustomerIdAsync(command, orderHeader.ChannelAccountNum);
                        if(settingCustomerId != "")
                        {
                            Customer checkCustomer = await qboUniversal.GetCustomerById(settingCustomerId);
                            customerId = checkCustomer == null ? customerId : settingCustomerId;
                        }
                        break;
                    case (int)CustomerCreateRule.PerOrder:
                        // TBD: Function approval pending.
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
            return customerId;
        }
        public static async Task<string> GetQboItemId(QboIntegrationSetting setting, QboOrderItemLine line, QboUniversal qboUniversal)
        {
            string itemId = "";
            try
            {
                switch (setting.QboItemCreateRule)
                {
                    case (int)QboItemCreateRule.DefaultItem:
                        itemId = setting.QboDefaultItemId.ForceToTrimString();
                        break;
                    case (int)QboItemCreateRule.SkipOrderWithoutMatching:
                        Item queriedItem = await qboUniversal.GetItemByName(line.Sku);
                        itemId = queriedItem != null ? queriedItem.Id : QboMappingConsts.NoMatchingItemReturnString;
                        break;
                    case (int)QboItemCreateRule.CreateNewItem:
                        // TBD: Function approval pending. Currently Items will not be synced two ways.
                        break;
                }
                return itemId;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }
        public static Line MapOrderDiscountToQboLine(QboSalesOrder qboSalesOrder, QboIntegrationSetting setting)
        {
            Line line = new Line();
            try
            {                
                if(setting.ExportOrderAs == (int)SaleOrderQboType.DailySummarySalesReceipt ||
                    setting.ExportOrderAs == (int)SaleOrderQboType.DailySummaryInvoice)
                {
                    // For SalesItemLineDetail, amoung must multiply by -1 to make it negative.
                    line.Amount = Math.Abs(qboSalesOrder.PromotionAmount) * -1;
                    line.Description = QboMappingConsts.SummaryDiscountLineDescription + qboSalesOrder.DigitbridgeOrderId;
                    SalesItemLineDetail discountSummaryLineDetail = new SalesItemLineDetail();
                    discountSummaryLineDetail.ItemRef = new ReferenceType()
                    {
                        Value = setting.QboDiscountItemId.ForceToTrimString(),
                        name = setting.QboDiscountItemName,
                    };
                    line.AnyIntuitObject = discountSummaryLineDetail;
                    line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
                }
                else
                {
                    // The amoung is negative bu default in DiscountLineDetail type.
                    line.Amount = Math.Abs(qboSalesOrder.PromotionAmount);
                    DiscountLineDetail discountLineDetail = new DiscountLineDetail();
                    discountLineDetail.DiscountRef = new ReferenceType
                    {
                        Value = QboMappingConsts.DiscountRefValue,
                        name = QboMappingConsts.DiscountRefName,
                    };
                    line.AnyIntuitObject = discountLineDetail;
                    line.DetailType = LineDetailTypeEnum.DiscountLineDetail;
                }
                line.AmountSpecified = true;
                line.DetailTypeSpecified = true;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
            return line;
        }
        public static Line MapOrderShippingCostToQboLine(QboSalesOrder qboSalesOrder, QboIntegrationSetting setting)
        {
            Line line = new Line();
            try 
            {
                string smryShippingItemId= "";

                line.Amount = qboSalesOrder.TotalShippingAmount + qboSalesOrder.TotalShippingDiscount;
                line.AmountSpecified = true;
                if (setting.ExportOrderAs == (int)SaleOrderQboType.DailySummarySalesReceipt ||
                    setting.ExportOrderAs == (int)SaleOrderQboType.DailySummaryInvoice)
                {
                    line.Description = QboMappingConsts.SummaryShippingLineDescription + qboSalesOrder.DigitbridgeOrderId;
                    smryShippingItemId = setting.QboShippingItemId.ForceToTrimString();
                }
                SalesItemLineDetail salesItemLineShippingDetail = new SalesItemLineDetail();
                salesItemLineShippingDetail.ItemRef = new ReferenceType()
                {
                    Value = smryShippingItemId != "" ? smryShippingItemId : QboMappingConsts.SippingCostRefValue
                };
                line.AnyIntuitObject = salesItemLineShippingDetail;

                line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
                line.DetailTypeSpecified = true;

            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
            return line;
        }
        public static Line MapOrderTaxToQboLine(QboSalesOrder qboSalesOrder, QboIntegrationSetting setting)
        {
            Line line = new Line();
            try
            {
                line.Amount = qboSalesOrder.TotalTaxAmount;
                line.AmountSpecified = true;
                line.Description = QboMappingConsts.SalesTaxItemDescription + qboSalesOrder.DigitbridgeOrderId;
                SalesItemLineDetail salesItemLineTaxDetail = new SalesItemLineDetail();
                salesItemLineTaxDetail.ItemRef = new ReferenceType()
                {
                    Value = setting.QboSalesTaxItemId.ForceToTrimString()
                };
                line.AnyIntuitObject = salesItemLineTaxDetail;
                line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
                line.DetailTypeSpecified = true;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
            return line;
        }
        public static Line MapOrderItemLinesToQboLine(QboOrderItemLine qboOrderItemLine, string itemId = "")
        {
            Line line = new Line();
            try
            {
                line.Description = qboOrderItemLine.Description;
                line.Amount = qboOrderItemLine.UnitPrice * qboOrderItemLine.Quantity;
                line.AmountSpecified = true;
                // Assign line number if specified, default as -1, line number info TBC from Central
                if (qboOrderItemLine.LineNum != 0)
                {
                    line.LineNum = qboOrderItemLine.LineNum.ToString();
                }
                SalesItemLineDetail salesItemLineDetail = new SalesItemLineDetail();

                itemId = !String.IsNullOrEmpty(itemId) ? itemId : qboOrderItemLine.ItemRef.ForceToTrimString();
                salesItemLineDetail.ItemRef = new ReferenceType() { Value = itemId };
                salesItemLineDetail.Qty = qboOrderItemLine.Quantity;
                salesItemLineDetail.QtySpecified = true;
                salesItemLineDetail.AnyIntuitObject = qboOrderItemLine.UnitPrice;
                salesItemLineDetail.ItemElementName = ItemChoiceType.UnitPrice;

                line.AnyIntuitObject = salesItemLineDetail;
                line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
                line.DetailTypeSpecified = true;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
            return line;
        }
        public static Invoice MapSalesOrderToQboInvoice(QboSalesOrder qboSalesOrder, String customerId, QboIntegrationSetting setting)
        {
            Invoice invoice = new Invoice();
            try
            {
                invoice.CustomerRef = new ReferenceType() { Value = customerId };
                invoice.TxnDate = qboSalesOrder.TxnDate;
                invoice.TxnDateSpecified = true;
                if (!qboSalesOrder.ShipDate.Equals(DateTime.MinValue))
                {
                    invoice.ShipDate = qboSalesOrder.ShipDate;
                    invoice.ShipDateSpecified = true;
                    invoice.TrackingNum = qboSalesOrder.TrackingNum;
                    invoice.ShipMethodRef = new ReferenceType() { Value = qboSalesOrder.ShipMethodRef };
                }
                invoice.PrivateNote = qboSalesOrder.PrivateNote;
                invoice.CustomerMemo = new MemoRef() { Value = qboSalesOrder.CustomerMemo };
                invoice.DocNumber = qboSalesOrder.DocNumber;

                // Map Invoice customized fields
                List<CustomField> customFields = new List<CustomField>();

                CustomField endCustoerPoNumCustField = new CustomField();
                endCustoerPoNumCustField.DefinitionId = setting.QboEndCustomerPoNumCustFieldId.ForceToTrimString();
                endCustoerPoNumCustField.AnyIntuitObject = qboSalesOrder.EndCustomerPoNum;
                endCustoerPoNumCustField.Type = CustomFieldTypeEnum.StringType;
                customFields.Add(endCustoerPoNumCustField);

                CustomField chnlOrderIdCustField = new CustomField();
                chnlOrderIdCustField.DefinitionId = setting.QboChnlOrderIdCustFieldId.ForceToTrimString();
                chnlOrderIdCustField.AnyIntuitObject = qboSalesOrder.ChannelOrderId;
                chnlOrderIdCustField.Type = CustomFieldTypeEnum.StringType;
                customFields.Add(chnlOrderIdCustField);

                CustomField SecChnlOrderIdCustomField = new CustomField();
                SecChnlOrderIdCustomField.DefinitionId = setting.Qbo2ndChnlOrderIdCustFieldId.ForceToTrimString();
                SecChnlOrderIdCustomField.AnyIntuitObject = qboSalesOrder.SecondaryChannelOrderId;
                SecChnlOrderIdCustomField.Type = CustomFieldTypeEnum.StringType;
                customFields.Add(SecChnlOrderIdCustomField);

                invoice.CustomField = customFields.ToArray();

                if (setting.QboCustomerCreateRule == (int)CustomerCreateRule.PerMarketPlace)
                {
                    PhysicalAddress shippingAddress = new PhysicalAddress();
                    shippingAddress.Line1 = qboSalesOrder.ShipToName;
                    shippingAddress.Line2 = qboSalesOrder.ShipToAddrLine1;
                    shippingAddress.Line3 = qboSalesOrder.ShipToAddrLine2;
                    shippingAddress.Line4 = qboSalesOrder.ShipToAddrLine3;
                    shippingAddress.PostalCode = qboSalesOrder.ShipToPostCode;
                    shippingAddress.City = qboSalesOrder.ShipToCity;
                    shippingAddress.Country = qboSalesOrder.ShipToCountry;
                    shippingAddress.CountrySubDivisionCode = qboSalesOrder.ShipToState;

                    invoice.ShipAddr = shippingAddress;

                    PhysicalAddress billingAddress = new PhysicalAddress();
                    billingAddress.Line1 = qboSalesOrder.BillToName;
                    billingAddress.Line2 = qboSalesOrder.BillToAddrLine1;
                    billingAddress.Line3 = qboSalesOrder.BillToAddrLine2;
                    billingAddress.Line4 = qboSalesOrder.BillToAddrLine3;
                    billingAddress.PostalCode = qboSalesOrder.BillToPostCode;
                    billingAddress.City = qboSalesOrder.BillToCity;
                    billingAddress.Country = qboSalesOrder.BillToCountry;
                    billingAddress.CountrySubDivisionCode = qboSalesOrder.BillToState;

                    invoice.BillAddr = billingAddress;
                }

            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
            return invoice;
        }
        public static SalesReceipt MapSalesOrderToQboSalesReceipt(QboSalesOrder qboSalesOrder, String customerId, QboIntegrationSetting setting)
        {
            SalesReceipt salesReceipt = new SalesReceipt();
            try
            {
                salesReceipt.CustomerRef = new ReferenceType() { Value = customerId };
                salesReceipt.TxnDate = qboSalesOrder.TxnDate;
                salesReceipt.TxnDateSpecified = true;
                if (!qboSalesOrder.ShipDate.Equals(DateTime.MinValue))
                {
                    salesReceipt.ShipDate = qboSalesOrder.ShipDate;
                    salesReceipt.ShipDateSpecified = true;
                    salesReceipt.TrackingNum = qboSalesOrder.TrackingNum;
                }
                salesReceipt.ShipMethodRef = new ReferenceType() { Value = qboSalesOrder.ShipMethodRef };
                salesReceipt.PrivateNote = qboSalesOrder.PrivateNote;
                salesReceipt.CustomerMemo = new MemoRef() { Value = qboSalesOrder.CustomerMemo };
                salesReceipt.DocNumber = qboSalesOrder.DocNumber;

                // Map Invoice customized fields
                List<CustomField> customFields = new List<CustomField>();

                CustomField endCustoerPoNumCustField = new CustomField();
                endCustoerPoNumCustField.DefinitionId = setting.QboEndCustomerPoNumCustFieldId.ForceToTrimString();
                endCustoerPoNumCustField.AnyIntuitObject = qboSalesOrder.EndCustomerPoNum;
                endCustoerPoNumCustField.Type = CustomFieldTypeEnum.StringType;
                customFields.Add(endCustoerPoNumCustField);
                
                CustomField chnlOrderIdCustField = new CustomField();
                chnlOrderIdCustField.DefinitionId = setting.QboChnlOrderIdCustFieldId.ForceToTrimString();
                chnlOrderIdCustField.AnyIntuitObject = qboSalesOrder.ChannelOrderId;
                chnlOrderIdCustField.Type = CustomFieldTypeEnum.StringType;
                customFields.Add(chnlOrderIdCustField);

                CustomField SecChnlOrderIdCustomField = new CustomField();
                SecChnlOrderIdCustomField.DefinitionId = setting.Qbo2ndChnlOrderIdCustFieldId.ForceToTrimString();
                SecChnlOrderIdCustomField.AnyIntuitObject = qboSalesOrder.SecondaryChannelOrderId;
                SecChnlOrderIdCustomField.Type = CustomFieldTypeEnum.StringType;
                customFields.Add(SecChnlOrderIdCustomField);

                salesReceipt.CustomField = customFields.ToArray();

                if (setting.QboCustomerCreateRule == (int)CustomerCreateRule.PerMarketPlace)
                {
                    PhysicalAddress shippingAddress = new PhysicalAddress();
                    shippingAddress.Line1 = qboSalesOrder.ShipToName;
                    shippingAddress.Line2 = qboSalesOrder.ShipToAddrLine1;
                    shippingAddress.Line3 = qboSalesOrder.ShipToAddrLine2;
                    shippingAddress.Line4 = qboSalesOrder.ShipToAddrLine3;
                    shippingAddress.PostalCode = qboSalesOrder.ShipToPostCode;
                    shippingAddress.City = qboSalesOrder.ShipToCity;
                    shippingAddress.Country = qboSalesOrder.ShipToCountry;
                    shippingAddress.CountrySubDivisionCode = qboSalesOrder.ShipToState;

                    salesReceipt.ShipAddr = shippingAddress;

                    PhysicalAddress billingAddress = new PhysicalAddress();
                    billingAddress.Line1 = qboSalesOrder.BillToName;
                    billingAddress.Line2 = qboSalesOrder.BillToAddrLine1;
                    billingAddress.Line3 = qboSalesOrder.BillToAddrLine2;
                    billingAddress.Line4 = qboSalesOrder.BillToAddrLine3;
                    billingAddress.PostalCode = qboSalesOrder.BillToPostCode;
                    billingAddress.City = qboSalesOrder.BillToCity;
                    billingAddress.Country = qboSalesOrder.BillToCountry;
                    billingAddress.CountrySubDivisionCode = qboSalesOrder.BillToState;

                    salesReceipt.BillAddr = billingAddress;
                }

            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
            return salesReceipt;
        }
        public static async Task<QboConnectionInfo> GetQboConnectionInfoAndDecrypt(QboConnectionInfoDb qboConnectionInfoDb, String cryptKey, Command command)
        {
            QboConnectionInfo qboConnectionInfo = new QboConnectionInfo();
            try
            {
                DataTable conInfoTb = await qboConnectionInfoDb.GetConnectionInfoByCommandAsync(command);
                List<QboConnectionInfo> qboConnectionInfos = 
                    ComlexTypeConvertExtension.DatatableToList<QboConnectionInfo>(conInfoTb);
                if (qboConnectionInfos.Count == 1)
                {
                    qboConnectionInfo = qboConnectionInfos.FirstOrDefault();
                    // decrypt sensitive credentials
                    qboConnectionInfo.ClientId = CryptoUtility.DecrypTextTripleDES(qboConnectionInfo.ClientId, cryptKey);
                    qboConnectionInfo.ClientSecret = CryptoUtility.DecrypTextTripleDES(qboConnectionInfo.ClientSecret, cryptKey);
                    qboConnectionInfo.AuthCode = CryptoUtility.DecrypTextTripleDES(qboConnectionInfo.AuthCode, cryptKey);
                    qboConnectionInfo.RealmId = CryptoUtility.DecrypTextTripleDES(qboConnectionInfo.RealmId, cryptKey);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
            return qboConnectionInfo;
        }
        public static DateTime GetEndOfDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
        }
        public static DateTime GetStartOfDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }
    }
}
