              
    

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.Base.Common;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class CustomIOFormat
    {
        public CsvFormat GetFormatObject()
        {
            CsvFormat fmt = null;

            if (!int.TryParse(FormatType, out int formatType))
                formatType = -1;


            switch (formatType)
            {
                case (int)ActivityLogType.SalesOrder:
                    fmt = new SalesOrderIOFormat();
                    if (!string.IsNullOrEmpty(this.FormatObject))
                        fmt.LoadFormat(this.FormatObject.JsonToObject<SalesOrderIOFormat>());
                    return fmt;
                case (int)ActivityLogType.PurchaseOrder:
                    fmt = new PurchaseOrderIOFormat();
                    if (!string.IsNullOrEmpty(this.FormatObject))
                        fmt.LoadFormat(this.FormatObject.JsonToObject<PurchaseOrderIOFormat>());
                    return fmt;
                case (int)ActivityLogType.PoReceive:
                    fmt = new PoReceiveIOFormat();
                    if (!string.IsNullOrEmpty(this.FormatObject))
                        fmt.LoadFormat(this.FormatObject.JsonToObject<PoReceiveIOFormat>());
                    return fmt;
                case (int)ActivityLogType.Vendor:
                    fmt = new VendorIOFormat();
                    if (!string.IsNullOrEmpty(this.FormatObject))
                        fmt.LoadFormat(this.FormatObject.JsonToObject<VendorIOFormat>());
                    return fmt;
                case (int)ActivityLogType.ApInvoice:
                    fmt = new ApInvoiceIOFormat();
                    if (!string.IsNullOrEmpty(this.FormatObject))
                        fmt.LoadFormat(this.FormatObject.JsonToObject<ApInvoiceIOFormat>());
                    return fmt;
                case (int)ActivityLogType.Invoice:
                    fmt = new InvoiceIOFormat();
                    if (!string.IsNullOrEmpty(this.FormatObject))
                        fmt.LoadFormat(this.FormatObject.JsonToObject<InvoiceIOFormat>());
                    return fmt;
                case (int)ActivityLogType.InvoicePayment:
                    fmt = new InvoicePaymentIOFormat();
                    if (!string.IsNullOrEmpty(this.FormatObject))
                        fmt.LoadFormat(this.FormatObject.JsonToObject<InvoicePaymentIOFormat>());
                    return fmt;
                case (int)ActivityLogType.InvoiceReturn:
                    fmt = new InvoiceReturnIOFormat();
                    if (!string.IsNullOrEmpty(this.FormatObject))
                        fmt.LoadFormat(this.FormatObject.JsonToObject<InvoiceReturnIOFormat>());
                    return fmt;
                case (int)ActivityLogType.Customer:
                    fmt = new CustomerIOFormat();
                    if (!string.IsNullOrEmpty(this.FormatObject))
                        fmt.LoadFormat(this.FormatObject.JsonToObject<CustomerIOFormat>());
                    return fmt;
                case (int)ActivityLogType.Inventory:
                    fmt = new InventoryIOFormat();
                    if (!string.IsNullOrEmpty(this.FormatObject))
                        fmt.LoadFormat(this.FormatObject.JsonToObject<InventoryIOFormat>());
                    return fmt;
                case (int)ActivityLogType.InventoryUpdate:
                    fmt = new InventoryUpdateIOFormat();
                    if (!string.IsNullOrEmpty(this.FormatObject))
                        fmt.LoadFormat(this.FormatObject.JsonToObject<InventoryUpdateIOFormat>());
                    return fmt;
                case (int)ActivityLogType.WarehouseTransfer:
                    fmt = new WarehouseTransferIOFormat();
                    if (!string.IsNullOrEmpty(this.FormatObject))
                        fmt.LoadFormat(this.FormatObject.JsonToObject<WarehouseTransferIOFormat>());
                    return fmt;
            }

 

            return fmt;
        }

        public CsvFormat NewFormatObject(string passFormatType)
        {
            if (!int.TryParse(passFormatType, out int formatType))
                formatType = -1;

            switch (formatType)
            {
                case (int)ActivityLogType.SalesOrder:
                    return new SalesOrderIOFormat();
                case (int)ActivityLogType.PurchaseOrder:
                    return new PurchaseOrderIOFormat();
                case (int)ActivityLogType.PoReceive:
                    return new PoReceiveIOFormat();
                case (int)ActivityLogType.Vendor:
                    return new VendorIOFormat();
                case (int)ActivityLogType.ApInvoice:
                    return new ApInvoiceIOFormat();
                case (int)ActivityLogType.Invoice:
                    return new InvoiceIOFormat();
                case (int)ActivityLogType.InvoicePayment:
                    return new InvoicePaymentIOFormat();
                case (int)ActivityLogType.InvoiceReturn:
                    return new InvoiceReturnIOFormat();
                case (int)ActivityLogType.Customer:
                    return new CustomerIOFormat();
                case (int)ActivityLogType.Inventory:
                    return new InventoryIOFormat();
                case (int)ActivityLogType.InventoryUpdate:
                    return new InventoryUpdateIOFormat();
                case (int)ActivityLogType.WarehouseTransfer:
                    return new WarehouseTransferIOFormat();
            }
            return new CsvFormat();
        }

        public void SetFormatObject(CsvFormat csvFormat)
        {
            CsvFormat fmt = null;

            if (!int.TryParse(FormatType, out int formatType))
                formatType = -1;

            switch (formatType)
            {
                case (int)ActivityLogType.SalesOrder:
                    fmt = new SalesOrderIOFormat();
                    break;
                case (int)ActivityLogType.PurchaseOrder:
                    fmt = new PurchaseOrderIOFormat();
                    break;
                case (int)ActivityLogType.PoReceive:
                    fmt = new PoReceiveIOFormat();
                    break;
                case (int)ActivityLogType.Vendor:
                    fmt = new VendorIOFormat();
                    break;
                case (int)ActivityLogType.ApInvoice:
                    fmt = new ApInvoiceIOFormat();
                    break;
                case (int)ActivityLogType.Invoice:
                    fmt = new InvoiceIOFormat();
                    break;
                case (int)ActivityLogType.InvoicePayment:
                    fmt = new InvoicePaymentIOFormat();
                    break;
                case (int)ActivityLogType.InvoiceReturn:
                    fmt = new InvoiceReturnIOFormat();
                    break;
                case (int)ActivityLogType.Customer:
                    fmt = new CustomerIOFormat();
                    break;
                case (int)ActivityLogType.Inventory:
                    fmt = new InventoryIOFormat();
                    break;
                case (int)ActivityLogType.InventoryUpdate:
                    fmt = new InventoryUpdateIOFormat();
                    break;
                case (int)ActivityLogType.WarehouseTransfer:
                    fmt = new WarehouseTransferIOFormat();
                    break;
            }
 
            if (fmt != null)
            {
                fmt.LoadFormat(csvFormat);
                this.FormatObject = fmt.ObjectToString<CsvFormat>();
            }
        }
    }
}



