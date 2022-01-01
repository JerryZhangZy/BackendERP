using System;
using System.Collections.Generic;
using System.Text;

namespace Digitbridge.QuickbooksOnline.CentralToDbMdl
{
    public class QboOrderImportErrorMsgs
    {
        public static readonly String OrderImportErrorPrefix = "Order Import Error on DigitBridge Order Id: ";
        public static readonly String OrderUpdateErrorPrefix = "Order Update Error on DigitBridge Order Id: ";
        public static readonly String OrderImportExceptionPrefix = "Order Import Exception on DigitBridge Order Id: ";
        public static readonly String OrderUpdateExceptionPrefix = "Order Update Error on DigitBridge Order Id: ";
        public static readonly String OrderImportDateRangeError = "Get Order Import Range Error for Central Order Import.";
        public static readonly String OrderImportModifiedError = 
            "SalesOrder from Central Order API has been " +
            "modified after such order header created in the Database.";
        public static readonly String OrderImportLineExistError = 
            "Tried to create Item Line under Sales Order but There is Item Line " +
            "already exist in Database when it should be empty before initial transfer.";
        public static readonly String OrderUpdateNotExistInDbError = "Trying to update SalesOrder but it has not been created.";
        public static readonly String OrderUpdateTargetStatusError = "Trying to update SalesOrder but the status is not SyncedSuccess.";
    }
    public class GetOrdersApiParamNames
    {
        public static readonly String Code = "code";
        public static readonly String MasterAccountNum = "MasterAccountNum";
        public static readonly String ProfileNum = "ProfileNum";
        public static readonly String OrderDateFrom = "orderDateFrom";
        public static readonly String OrderDateTo = "orderDateTo";
        public static readonly String LastUpdateDateFrom = "lastUpdateDateFrom";
        public static readonly String LastUpdateDateTo = "lastUpdateDateTo";
        public static readonly String ChannelOrderID = "channelOrderID";
        public static readonly String OrderStatus = "orderStatus";
        public static readonly String OrderExtensionFlags = "OrderExtensionFlags";
        public static readonly String OrderExtensionFlags_not = "OrderExtensionFlags_not";
        public static readonly String Top = "$top";
        public static readonly String Skip = "$skip";
        public static readonly String Count = "$count";
    }

}
