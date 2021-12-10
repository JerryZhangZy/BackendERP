              
    

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

            if (FormatType.EqualsIgnoreSpace(ActivityLogType.SalesOrder.ToString()))
            {
                fmt = new SalesOrderIOFormat();
                if (!string.IsNullOrEmpty(this.FormatObject))
                    fmt.LoadFormat(this.FormatObject.JsonToObject<SalesOrderIOFormat>());
                return fmt;
            }
            else if (FormatType.EqualsIgnoreSpace(ActivityLogType.PurchaseOrder.ToString()))
            {
                fmt = new PurchaseOrderIOFormat();
                if (!string.IsNullOrEmpty(this.FormatObject))
                    fmt.LoadFormat(this.FormatObject.JsonToObject<PurchaseOrderIOFormat>());
                return fmt;
            }
            else if (FormatType.EqualsIgnoreSpace(ActivityLogType.PoReceive.ToString()))
            {
                fmt = new PoReceiveIOFormat();
                if (!string.IsNullOrEmpty(this.FormatObject))
                    fmt.LoadFormat(this.FormatObject.JsonToObject<PoReceiveIOFormat>());
                return fmt;
            }
            else if (FormatType.EqualsIgnoreSpace(ActivityLogType.Vendor.ToString()))
            {
                fmt = new VendorIOFormat();
                if (!string.IsNullOrEmpty(this.FormatObject))
                    fmt.LoadFormat(this.FormatObject.JsonToObject<VendorIOFormat>());
                return fmt;
            }
            else if (FormatType.EqualsIgnoreSpace(ActivityLogType.ApInvoice.ToString()))
            {
                fmt = new ApInvoiceIOFormat();
                if (!string.IsNullOrEmpty(this.FormatObject))
                    fmt.LoadFormat(this.FormatObject.JsonToObject<ApInvoiceIOFormat>());
                return fmt;
            }
            else if (FormatType.EqualsIgnoreSpace(ActivityLogType.Invoice.ToString()))
            {
                fmt = new InvoiceIOFormat();
                if (!string.IsNullOrEmpty(this.FormatObject))
                    fmt.LoadFormat(this.FormatObject.JsonToObject<InvoiceIOFormat>());
                return fmt;

            }
            else if (FormatType.EqualsIgnoreSpace(ActivityLogType.InvoicePayment.ToString()))
            {
                fmt = new InvoicePaymentIOFormat();
                if (!string.IsNullOrEmpty(this.FormatObject))
                    fmt.LoadFormat(this.FormatObject.JsonToObject<InvoicePaymentIOFormat>());
                return fmt;

            }
            else if (FormatType.EqualsIgnoreSpace(ActivityLogType.InvoiceReturn.ToString()))
            {
                fmt = new InvoiceReturnIOFormat();
                if (!string.IsNullOrEmpty(this.FormatObject))
                    fmt.LoadFormat(this.FormatObject.JsonToObject<InvoiceReturnIOFormat>());
                return fmt;

            }
            else if (FormatType.EqualsIgnoreSpace(ActivityLogType.Customer.ToString()))
            {
                fmt = new CustomerIOFormat();
                if (!string.IsNullOrEmpty(this.FormatObject))
                    fmt.LoadFormat(this.FormatObject.JsonToObject<CustomerIOFormat>());
                return fmt;

            }
            else if (FormatType.EqualsIgnoreSpace(ActivityLogType.Inventory.ToString()))
            {
                fmt = new InventoryIOFormat();
                if (!string.IsNullOrEmpty(this.FormatObject))
                    fmt.LoadFormat(this.FormatObject.JsonToObject<InventoryIOFormat>());
                return fmt;

            }
            else if (FormatType.EqualsIgnoreSpace(ActivityLogType.InventoryUpdate.ToString()))
            {
                fmt = new InventoryUpdateIOFormat();
                if (!string.IsNullOrEmpty(this.FormatObject))
                    fmt.LoadFormat(this.FormatObject.JsonToObject<InventoryUpdateIOFormat>());
                return fmt;

            }


            return fmt;
        }

        public void SetFormatObject(CsvFormat csvFormat)
        {
            CsvFormat fmt = null;
            if (FormatType.EqualsIgnoreSpace(ActivityLogType.SalesOrder.ToString()))
            {
                fmt = new SalesOrderIOFormat();
                fmt.LoadFormat(csvFormat);
                this.FormatObject = fmt.ObjectToString<CsvFormat>();
            }
            else if (FormatType.EqualsIgnoreSpace(ActivityLogType.PurchaseOrder.ToString()))
            {
                fmt = new PurchaseOrderIOFormat();
                fmt.LoadFormat(csvFormat);
                this.FormatObject = fmt.ObjectToString<CsvFormat>();
            }
            else if (FormatType.EqualsIgnoreSpace(ActivityLogType.PoReceive.ToString()))
            {
                fmt = new PoReceiveIOFormat();
                fmt.LoadFormat(csvFormat);
                this.FormatObject = fmt.ObjectToString<CsvFormat>();
            }
            else if (FormatType.EqualsIgnoreSpace(ActivityLogType.Vendor.ToString()))
            {
                fmt = new VendorIOFormat();
                fmt.LoadFormat(csvFormat);
                this.FormatObject = fmt.ObjectToString<CsvFormat>();
            }
            else if (FormatType.EqualsIgnoreSpace(ActivityLogType.ApInvoice.ToString()))
            {
                fmt = new ApInvoiceIOFormat();
                fmt.LoadFormat(csvFormat);
                this.FormatObject = fmt.ObjectToString<CsvFormat>();
            }
        }
        }
    }



