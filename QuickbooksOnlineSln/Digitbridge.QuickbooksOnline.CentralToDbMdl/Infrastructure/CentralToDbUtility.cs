using Digitbridge.QuickbooksOnline.CentralToDbMdl.Model;
using Digitbridge.QuickbooksOnline.Db.Infrastructure;
using Digitbridge.QuickbooksOnline.Db.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UneedgoHelper.DotNet.Common;
using UneedgoHelper.DotNet.Data.MsSql;

namespace Digitbridge.QuickbooksOnline.CentralToDbMdl
{
    public static class QuickBooksDbUtility
    {
        public static List<QboSalesOrderWrapper> GetMappedQboSalesOrdersFromCentral(CentralOrderResult centralOrderResult)
        {
            List<QboSalesOrderWrapper> qboSalesOrderWrappers = new List<QboSalesOrderWrapper>();
            try
            {
                if (centralOrderResult != null || centralOrderResult.Count > 0)
                {
                    foreach (Order fulfilledOrder in centralOrderResult.Orders)
                    {
                        QboSalesOrderWrapper qboSalesOrderWrapper = new QboSalesOrderWrapper();
                        QboSalesOrder qboSalesOrder = MapQboSalseOrderFromCentral(fulfilledOrder);
                        qboSalesOrderWrapper.qboSalesOrder = qboSalesOrder;

                        foreach (OrderItem fulfilledOrderItem in fulfilledOrder.OrderItems)
                        {
                            QboOrderItemLine qboItemLine = MapQboItemLineFromCentral(fulfilledOrder, fulfilledOrderItem);
                            qboSalesOrderWrapper.qboItemLines.Add(qboItemLine);
                        }

                        qboSalesOrderWrappers.Add(qboSalesOrderWrapper);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
            
            return qboSalesOrderWrappers;
        }

        public static QboOrderItemLine MapQboItemLineFromCentral(Order fulfilledOrder, OrderItem fulfilledOrderItem)
        {
            QboOrderItemLine qboItemLine = new QboOrderItemLine();

            try
            {
                qboItemLine.DatabaseNum = fulfilledOrder.OrderHeader.DatabaseNum;
                qboItemLine.MasterAccountNum = fulfilledOrder.OrderHeader.MasterAccountNum;
                qboItemLine.ProfileNum = fulfilledOrder.OrderHeader.ProfileNum;
                qboItemLine.ChannelNum = fulfilledOrder.OrderHeader.ChannelNum;
                qboItemLine.ChannelName = fulfilledOrder.OrderHeader.ChannelName;
                qboItemLine.ChannelAccountNum = fulfilledOrder.OrderHeader.ChannelAccountNum;
                qboItemLine.ChannelAccountName = fulfilledOrder.OrderHeader.ChannelAccountName;
                qboItemLine.ChannelOrderId = fulfilledOrder.OrderHeader.ChannelOrderID;
                qboItemLine.CentralOrderLineNum = fulfilledOrderItem.CentralOrderLineNum;
                qboItemLine.ChannelItemId = fulfilledOrderItem.ChannelItemID;
                qboItemLine.SecondaryChannelOrderId = fulfilledOrder.OrderHeader.SecondaryChannelOrderID;
                qboItemLine.DigitbridgeOrderId = fulfilledOrder.OrderHeader.DigitbridgeOrderId;
                qboItemLine.DetailType = Convert.ToByte(
                    fulfilledOrderItem.BundleStatus == 1 ? SalesOrderDetailType.GroupLineDetail
                    : SalesOrderDetailType.SalesItemLineDetail
                    );
                qboItemLine.Description = fulfilledOrderItem.ItemTitle;
                qboItemLine.QboSyncStatus = (int)QboSyncStatus.UnSynced;
                qboItemLine.Sku = fulfilledOrderItem.Sku;
                qboItemLine.CentralProductNum = fulfilledOrderItem.CentralProductNum;
                qboItemLine.Amount = fulfilledOrderItem.ItemTotalAmount.ForceToDecimal();
                qboItemLine.Quantity = fulfilledOrderItem.OrderQty.ForceToDecimal();
                qboItemLine.UnitPrice = fulfilledOrderItem.UnitPrice.ForceToDecimal();
                qboItemLine.CentralCreateTime = fulfilledOrder.OrderHeader.EnterDateUtc;
                qboItemLine.CentralUpdatedTime = fulfilledOrder.OrderHeader.OrderStatusUpdateDateUtc;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }

            return qboItemLine;
        }

        public static QboSalesOrder MapQboSalseOrderFromCentral(Order fulfilledOrder )
        {
            QboSalesOrder qboSalesOrder = new QboSalesOrder();

            try
            {
                qboSalesOrder.MasterAccountNum = fulfilledOrder.OrderHeader.MasterAccountNum;
                qboSalesOrder.ProfileNum = fulfilledOrder.OrderHeader.ProfileNum;
                qboSalesOrder.DatabaseNum = fulfilledOrder.OrderHeader.DatabaseNum;
                qboSalesOrder.ChannelNum = fulfilledOrder.OrderHeader.ChannelNum;
                qboSalesOrder.ChannelName = fulfilledOrder.OrderHeader.ChannelName;
                qboSalesOrder.ChannelAccountNum = fulfilledOrder.OrderHeader.ChannelAccountNum;
                qboSalesOrder.ChannelAccountName = fulfilledOrder.OrderHeader.ChannelAccountName;
                qboSalesOrder.ChannelOrderId = fulfilledOrder.OrderHeader.ChannelOrderID;
                qboSalesOrder.SecondaryChannelOrderId = fulfilledOrder.OrderHeader.SecondaryChannelOrderID;
                qboSalesOrder.CentralOrderNum = fulfilledOrder.OrderHeader.CentralOrderNum;
                qboSalesOrder.DigitbridgeOrderId = fulfilledOrder.OrderHeader.DigitbridgeOrderId;
                qboSalesOrder.EndCustomerPoNum = fulfilledOrder.OrderHeader.EndCustomerPoNum;
                qboSalesOrder.DocNumber = fulfilledOrder.OrderHeader.DigitbridgeOrderId;
                qboSalesOrder.TxnDate = fulfilledOrder.OrderHeader.OriginalOrderDateUtc;
                qboSalesOrder.PaymentStatus = fulfilledOrder.OrderHeader.PaymentStatus.ForceToInt();
                qboSalesOrder.QboSyncStatus = (int)QboSyncStatus.UnSynced;
                qboSalesOrder.TotalAmt = fulfilledOrder.OrderHeader.TotalOrderAmount.ForceToDecimal();
                qboSalesOrder.TotalShippingAmount = fulfilledOrder.OrderHeader.TotalShippingAmount.ForceToDecimal();
                qboSalesOrder.TotalShippingDiscount = fulfilledOrder.OrderHeader.TotalShippingDiscount.ForceToDecimal();
                qboSalesOrder.PromotionAmount = fulfilledOrder.OrderHeader.PromotionAmount.ForceToDecimal();
                qboSalesOrder.TotalTaxAmount = fulfilledOrder.OrderHeader.TotalTaxAmount.ForceToDecimal();
                qboSalesOrder.ShipToAddrLine1 = fulfilledOrder.OrderHeader.ShipToAddressLine1;
                qboSalesOrder.ShipToAddrLine2 = fulfilledOrder.OrderHeader.ShipToAddressLine2;
                qboSalesOrder.ShipToAddrLine3 = fulfilledOrder.OrderHeader.ShipToAddressLine3;
                qboSalesOrder.ShipToCity = fulfilledOrder.OrderHeader.ShipToCity;
                qboSalesOrder.ShipToCountry = fulfilledOrder.OrderHeader.ShipToCountry;
                qboSalesOrder.ShipToState = fulfilledOrder.OrderHeader.ShipToState;
                qboSalesOrder.ShipToPostCode = fulfilledOrder.OrderHeader.ShipToPostalCode;
                qboSalesOrder.ShipToCompanyName = fulfilledOrder.OrderHeader.ShipToCompany;
                qboSalesOrder.ShipToName = fulfilledOrder.OrderHeader.ShipToName;
                qboSalesOrder.BillToAddrLine1 = fulfilledOrder.OrderHeader.BillToAddressLine1;
                qboSalesOrder.BillToAddrLine2 = fulfilledOrder.OrderHeader.BillToAddressLine2;
                qboSalesOrder.BillToAddrLine3 = fulfilledOrder.OrderHeader.BillToAddressLine3;
                qboSalesOrder.BillToCity = fulfilledOrder.OrderHeader.BillToCity;
                qboSalesOrder.BillToCountry = fulfilledOrder.OrderHeader.BillToCountry;
                qboSalesOrder.BillToState = fulfilledOrder.OrderHeader.BillToState;
                qboSalesOrder.BillToPostCode = fulfilledOrder.OrderHeader.BillToPostalCode;
                qboSalesOrder.BillToCompanyName = fulfilledOrder.OrderHeader.BillToCompany;
                qboSalesOrder.BillToName = fulfilledOrder.OrderHeader.BillToName;
                // To be confirm
                qboSalesOrder.ShipDate = fulfilledOrder.OrderHeader.ShippedDateUtc;
                // To be confirm
                qboSalesOrder.TrackingNum = fulfilledOrder.OrderHeader.TrackingNumber;
                qboSalesOrder.CentralCreateTime = fulfilledOrder.OrderHeader.EnterDateUtc;
                qboSalesOrder.CentralUpdatedTime = fulfilledOrder.OrderHeader.OrderStatusUpdateDateUtc;
                qboSalesOrder.PrivateNote = fulfilledOrder.OrderHeader.SellerPrivateNote;
                qboSalesOrder.CustomerMemo = fulfilledOrder.OrderHeader.SellerPublicNote;
                qboSalesOrder.ShipMethodRef = fulfilledOrder.OrderHeader.RequestedShippingCarrier +
                    " " + fulfilledOrder.OrderHeader.RequestedShippingClass;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }

            return qboSalesOrder;
        }
        /// <summary>
        /// Get Order Import From Date and Order Import To Date for rolling order import from Central 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="qboOrderDb"></param>
        /// <param name="qboIntegrationSettingDb"></param>
        /// <returns></returns>
        public static async Task<DateTime> GetImportOrderFromDate(Command command, 
            QboOrderDb qboOrderDb, QboIntegrationSettingDb qboIntegrationSettingDb)
        {
            DateTime exportFromDate = new DateTime(); 
            try
            {
                exportFromDate = await qboOrderDb.GetLatestCentralUpdatedTime(command);
                // Handle Senerio that there is no order exported yet
                if (exportFromDate == default(DateTime))
                {
                    // Get ExportedFromDate from User's Integration Setting
                    exportFromDate = await qboIntegrationSettingDb.GetExportOrderFromDate(command);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
            return exportFromDate;
        }

        public static async Task<string> PostExtensionFlagsAsync(string endPoint, 
            string code, OrderExtensionFlagsPostType flagsPostType)
        {
            string jsonString = "";
            try
            {
                string json = JsonConvert.SerializeObject(flagsPostType);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                UriBuilder builder = new UriBuilder(endPoint);
                var query = HttpUtility.ParseQueryString(string.Empty);
                query["code"] = code;

                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("EnterBy", "QuickBooksOnlineIntegration");

                builder.Query = query.ToString();
                var response = await client.PostAsync(builder.Uri, content);

                jsonString = await response.Content.ReadAsStringAsync();

                return jsonString;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public static async Task<string> GetSalesOrdersFromCentral(
            GetOrderDateFilterType dateType, string endPoint, string code, int masterAccNum, 
            int profileNum, int top, int skip, 
            CentralOrderStatus[] centralOrderStatuses,
            OrderExtensionFlag[] inExtensionFlags,
            OrderExtensionFlag[] notInExtensionFlags,
            DateTime fromDate,
            DateTime toDate,
            Boolean isRequireCount = true)
        {
            string jsonString = "";

            try
            {
                UriBuilder builder = new UriBuilder(endPoint);

                var query = HttpUtility.ParseQueryString(string.Empty);
                query[GetOrdersApiParamNames.Code] = code;
                query[GetOrdersApiParamNames.MasterAccountNum] = masterAccNum.ForceToTrimString();
                query[GetOrdersApiParamNames.ProfileNum] = profileNum.ForceToTrimString();
                query[GetOrdersApiParamNames.Top] = top.ForceToTrimString();
                if(centralOrderStatuses.Count() > 0)
                {
                    query[GetOrdersApiParamNames.OrderStatus] = string.Join(",", centralOrderStatuses.Select(n => n.ForceToInt().ToString()));
                }
                query[GetOrdersApiParamNames.OrderExtensionFlags] = string.Join(",", inExtensionFlags.Select(n => n.ForceToInt().ToString()));
                query[GetOrdersApiParamNames.OrderExtensionFlags_not] = string.Join(",", notInExtensionFlags.Select(n => n.ForceToInt().ToString()));
                query[GetOrdersApiParamNames.Skip] = skip.ForceToTrimString();
                query[GetOrdersApiParamNames.Count] = isRequireCount.ForceToTrimString();

                switch (dateType)
                {
                    case GetOrderDateFilterType.LastUpdateDate:
                        query[GetOrdersApiParamNames.LastUpdateDateFrom] = fromDate.ToString();
                        query[GetOrdersApiParamNames.LastUpdateDateTo] = toDate.ToString();
                        break;
                    case GetOrderDateFilterType.OrderDate:
                        query[GetOrdersApiParamNames.OrderDateFrom] = fromDate.ToString();
                        query[GetOrdersApiParamNames.OrderDateTo] = fromDate.ToString();
                        break;
                }
                
                builder.Query = query.ToString();

                HttpClient client = new HttpClient();
                // Add an Accept header for JSON format.  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(builder.Uri).Result;

                jsonString = await response.Content.ReadAsStringAsync();
               
                return jsonString;
            }
            catch (Exception ex)
            {
                throw ExceptionUtility.WrapException(MethodBase.GetCurrentMethod(), ex);
            }
        }

    }
}
