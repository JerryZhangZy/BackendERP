using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.QuickBooks.Integration.Model;
using Intuit.Ipp.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.QuickBooks.Integration
{
    public class InvoiceMapper
    {
        private QboIntegrationSetting _setting { get; set; }
        public InvoiceMapper(QboIntegrationSetting setting)
        {
            this._setting = setting;
        }

        protected Line ItemToQboLine(InvoiceItems item)
        {
            Line line = new Line();

            line.Description = item.Description;
            line.Amount = item.IsAr ? item.ExtAmount : 0;//TODO check this one
            line.AmountSpecified = true;
            line.LineNum = item.InvoiceItemsUuid;

            line.AnyIntuitObject = new SalesItemLineDetail()
            {
                ItemRef = new ReferenceType() { Value = item.InvoiceItemsUuid },
                Qty = item.ShipQty,
                QtySpecified = true,
                AnyIntuitObject = item.DiscountPrice,
                ItemElementName = ItemChoiceType.UnitPrice,
            };
            line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            line.DetailTypeSpecified = true;
            //TODO check in Qbo invoice page Amount=Qty*AnyIntuitObject
            return line;
        }
        protected Line DiscountToQboLine(InvoiceHeader invoiceHeader)
        {
            Line line = new Line();
            line.AnyIntuitObject = new SalesItemLineDetail()
            {
                ItemRef = new ReferenceType()
                {
                    Value = _setting.QboDiscountId.ToString(),
                    name = _setting.QboDiscountName,
                }
            };
            line.Amount = invoiceHeader.DiscountAmount;
            line.DetailType = LineDetailTypeEnum.DiscountLineDetail;//LineDetailTypeEnum.GroupLineDetail  //TODO Try this.
            line.AmountSpecified = true;
            line.DetailTypeSpecified = true;
            //line.Description
            return line;
        }
        protected Line ShippingCostToQboLine(InvoiceHeader invoiceHeader)
        {
            Line line = new Line();
            line.Amount = invoiceHeader.ShippingAmount;
            line.AmountSpecified = true;
            line.AnyIntuitObject = new SalesItemLineDetail()
            {
                ItemRef = new ReferenceType()
                {
                    Value = _setting.QboShippingId.ToString(),
                    name = _setting.QboShippingName,
                }
            };
            line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            line.DetailTypeSpecified = true;
            //line.Description

            return line;
        }
        protected Line MiscCostToQboLine(InvoiceHeader invoiceHeader)
        {
            Line line = new Line();
            line.Amount = invoiceHeader.MiscAmount;
            line.AmountSpecified = true;
            line.AnyIntuitObject = new SalesItemLineDetail()
            {
                ItemRef = new ReferenceType()
                {
                    Value = _setting.QboMiscId.ToString(),
                    name = _setting.QboMiscName,
                }
            };
            line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            line.DetailTypeSpecified = true;
            //line.Description
            return line;
        }
        protected Line ChargeAndAllowanceCostToQboLine(InvoiceHeader invoiceHeader)
        {
            Line line = new Line();
            line.Amount = invoiceHeader.ChargeAndAllowanceAmount;
            line.AmountSpecified = true;
            line.AnyIntuitObject = new SalesItemLineDetail()
            {
                ItemRef = new ReferenceType()
                {
                    Value = _setting.QboChargeAndAllowanceId.ToString(),
                    name = _setting.QboChargeAndAllowanceName,
                }
            };
            line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            line.DetailTypeSpecified = true;
            //line.Description
            return line;
        }
        protected Line TaxCostToQboLine(InvoiceHeader invoiceHeader)
        {
            Line line = new Line();
            line.Amount = invoiceHeader.TaxAmount;
            line.AmountSpecified = true;
            line.AnyIntuitObject = new SalesItemLineDetail()
            {
                ItemRef = new ReferenceType()
                {
                    Value = _setting.QboTaxId.ToString(),
                    name = _setting.QboTaxName,
                }
            };
            line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            line.DetailTypeSpecified = true;
            //line.Description
            return line;
        }
        protected Invoice ToQboInvoiceSummary(InvoiceData invoiceData, string customerId)
        {
            var invoice = new Invoice();
            invoice.CustomerRef = new ReferenceType() { Value = customerId };
            //invoice.TxnDate = qboSalesOrder.TxnDate;
            //invoice.TxnDateSpecified = true; 

            //TODO item has ShipDate but summary doesn't include ShipDate
            //if (!qboSalesOrder.ShipDate.Equals(DateTime.MinValue))
            //{
            //    invoice.ShipDate = qboSalesOrder.ShipDate;
            //    invoice.ShipDateSpecified = true;
            //    invoice.TrackingNum = qboSalesOrder.TrackingNum;
            //    invoice.ShipMethodRef = new ReferenceType() { Value = qboSalesOrder.ShipMethodRef };
            //}
            //invoice.PrivateNote =  
            //invoice.CustomerMemo = new MemoRef() { Value = qboSalesOrder.CustomerMemo };
            invoice.DocNumber = invoiceData.InvoiceHeader.InvoiceNumber;//TODO replace this.

            // Map Invoice customized fields
            List<CustomField> customFields = new List<CustomField>();

            CustomField endCustoerPoNumCustField = new CustomField();
            //endCustoerPoNumCustField.DefinitionId = _setting.QboEndCustomerPoNumCustFieldId.ToString();
            //endCustoerPoNumCustField.AnyIntuitObject = qboSalesOrder.EndCustomerPoNum;//TODO
            endCustoerPoNumCustField.Type = CustomFieldTypeEnum.StringType;
            customFields.Add(endCustoerPoNumCustField);

            CustomField chnlOrderIdCustField = new CustomField();
            //chnlOrderIdCustField.DefinitionId = _setting.QboChnlOrderIdCustFieldId.ToString();
            //chnlOrderIdCustField.AnyIntuitObject = qboSalesOrder.ChannelOrderId;//TODO
            chnlOrderIdCustField.Type = CustomFieldTypeEnum.StringType;
            customFields.Add(chnlOrderIdCustField);

            CustomField SecChnlOrderIdCustomField = new CustomField();
            //SecChnlOrderIdCustomField.DefinitionId = _setting.Qbo2ndChnlOrderIdCustFieldId.ToString();
            //SecChnlOrderIdCustomField.AnyIntuitObject = qboSalesOrder.SecondaryChannelOrderId;//TODO
            SecChnlOrderIdCustomField.Type = CustomFieldTypeEnum.StringType;
            customFields.Add(SecChnlOrderIdCustomField);

            invoice.CustomField = customFields.ToArray();

            //if (setting.QboCustomerCreateRule == (int)CustomerCreateRule.PerMarketPlace)
            //{
            //    PhysicalAddress shippingAddress = new PhysicalAddress();
            //    shippingAddress.Line1 = qboSalesOrder.ShipToName;
            //    shippingAddress.Line2 = qboSalesOrder.ShipToAddrLine1;
            //    shippingAddress.Line3 = qboSalesOrder.ShipToAddrLine2;
            //    shippingAddress.Line4 = qboSalesOrder.ShipToAddrLine3;
            //    shippingAddress.PostalCode = qboSalesOrder.ShipToPostCode;
            //    shippingAddress.City = qboSalesOrder.ShipToCity;
            //    shippingAddress.Country = qboSalesOrder.ShipToCountry;
            //    shippingAddress.CountrySubDivisionCode = qboSalesOrder.ShipToState;

            //    invoice.ShipAddr = shippingAddress;

            //    PhysicalAddress billingAddress = new PhysicalAddress();
            //    billingAddress.Line1 = qboSalesOrder.BillToName;
            //    billingAddress.Line2 = qboSalesOrder.BillToAddrLine1;
            //    billingAddress.Line3 = qboSalesOrder.BillToAddrLine2;
            //    billingAddress.Line4 = qboSalesOrder.BillToAddrLine3;
            //    billingAddress.PostalCode = qboSalesOrder.BillToPostCode;
            //    billingAddress.City = qboSalesOrder.BillToCity;
            //    billingAddress.Country = qboSalesOrder.BillToCountry;
            //    billingAddress.CountrySubDivisionCode = qboSalesOrder.BillToState;

            //    invoice.BillAddr = billingAddress;
            //}
            return invoice;
        }
        protected IList<Line> ItemsToQboLine(IList<InvoiceItems> items)
        {
            var lines = new List<Line>();
            foreach (var item in items)
            {
                lines.Add(ItemToQboLine(item));
            }
            return lines;
        }

        protected List<Line> ToQboLines(InvoiceData invoiceData)
        {
            var lines = new List<Line>();
            lines.AddRange(ItemsToQboLine(invoiceData.InvoiceItems));
            lines.Add(DiscountToQboLine(invoiceData.InvoiceHeader));
            lines.Add(ShippingCostToQboLine(invoiceData.InvoiceHeader));
            lines.Add(MiscCostToQboLine(invoiceData.InvoiceHeader));
            lines.Add(ChargeAndAllowanceCostToQboLine(invoiceData.InvoiceHeader));
            lines.Add(TaxCostToQboLine(invoiceData.InvoiceHeader));
            return lines;
        }
        public Invoice ToQboInvoice(InvoiceData invoiceData, string customerId)
        {
            var invoice = ToQboInvoiceSummary(invoiceData, customerId);
            invoice.Line = ToQboLines(invoiceData).ToArray();
            return invoice;
        }
    }
}
