using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using Intuit.Ipp.Data;
using System.Collections.Generic;

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
                    Value = _setting.QboDiscountItemId.ToString(),
                    name = _setting.QboDiscountItemName,
                }
            };
            line.Amount = invoiceHeader.DiscountAmount;
            line.DetailType = LineDetailTypeEnum.DiscountLineDetail;//LineDetailTypeEnum.SalesItemLineDetail  //TODO Try this.
            line.AmountSpecified = true;
            line.DetailTypeSpecified = true;
            line.Description = QboMappingConsts.SummaryDiscountLineDescription + invoiceHeader.InvoiceNumber;
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
                    Value = _setting.QboShippingItemId.ToString(),
                    name = _setting.QboShippingItemName,
                }
            };
            line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            line.DetailTypeSpecified = true;
            line.Description = QboMappingConsts.SummaryShippingLineDescription + invoiceHeader.InvoiceNumber;
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
                    Value = _setting.QboMiscItemId.ToString(),
                    name = _setting.QboMiscItemName,
                }
            };
            line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            line.DetailTypeSpecified = true;
            line.Description = QboMappingConsts.SummaryMiscLineDescription + invoiceHeader.InvoiceNumber;
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
                    Value = _setting.QboChargeAndAllowanceItemId.ToString(),
                    name = _setting.QboChargeAndAllowanceItemName,
                }
            };
            line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            line.DetailTypeSpecified = true;
            line.Description = QboMappingConsts.SummaryChargeAndAllowanceLineDescription + invoiceHeader.InvoiceNumber;
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
                    Value = _setting.QboSalesTaxItemId.ToString(),
                    name = _setting.QboSalesTaxItemName,
                }
            };
            line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            line.DetailTypeSpecified = true;
            line.Description = QboMappingConsts.SalesTaxItemDescription + invoiceHeader.InvoiceNumber;
            return line;
        }
        protected Invoice ToQboInvoiceSummary(InvoiceData invoiceData, string customerId)
        {
            var invoice = new Invoice();
            var invoiceHeader = invoiceData.InvoiceHeader;
            var invoiceInfo = invoiceData.InvoiceHeaderInfo;
            invoice.CustomerRef = new ReferenceType() { Value = customerId };
            invoice.TotalAmt = invoiceHeader.TotalAmount;
            invoice.TxnDate = invoiceHeader.InvoiceDate;
            invoice.TxnDateSpecified = true;

            if (!invoiceHeader.ShipDate.IsZero())
            {
                invoice.ShipDate = invoiceHeader.ShipDate.Value;
                invoice.ShipDateSpecified = true;
                invoice.TrackingNum = invoiceInfo.OrderShipmentNum.ToString();
                invoice.ShipMethodRef = new ReferenceType() { Value = invoiceInfo.ShippingCarrier + " " + invoiceInfo.ShippingClass };
            }
            //TODO
            //invoice.PrivateNote = invoiceInfo.s//qboSalesOrder.PrivateNote = fulfilledOrder.OrderHeader.SellerPrivateNote;
            //invoice.CustomerMemo = new MemoRef() { Value = qboSalesOrder.CustomerMemo };//qboSalesOrder.CustomerMemo = fulfilledOrder.OrderHeader.SellerPublicNote;
            invoice.DocNumber = invoiceHeader.InvoiceNumber;//qboSalesOrder.DocNumber = fulfilledOrder.OrderHeader.DigitbridgeOrderId;

            // Map Invoice customized fields
            List<CustomField> customFields = new List<CustomField>();

            CustomField endCustoerPoNumCustField = new CustomField();
            endCustoerPoNumCustField.AnyIntuitObject = _setting.QboEndCustomerPoNumCustFieldId.ToString(); //endCustoerPoNumCustField.DefinitionId = _setting.QboEndCustomerPoNumCustFieldId.ToString();
            endCustoerPoNumCustField.AnyIntuitObject = invoiceInfo.CustomerPoNum; //endCustoerPoNumCustField.AnyIntuitObject = qboSalesOrder.EndCustomerPoNum; 
            endCustoerPoNumCustField.Type = CustomFieldTypeEnum.StringType;
            customFields.Add(endCustoerPoNumCustField);

            CustomField chnlOrderIdCustField = new CustomField();
            chnlOrderIdCustField.DefinitionId = _setting.QboChnlOrderIdCustFieldId.ToString();
            chnlOrderIdCustField.AnyIntuitObject = invoiceInfo.ChannelOrderID;
            chnlOrderIdCustField.Type = CustomFieldTypeEnum.StringType;
            customFields.Add(chnlOrderIdCustField);

            CustomField secChnlOrderIdCustomField = new CustomField();
            secChnlOrderIdCustomField.DefinitionId = _setting.Qbo2ndChnlOrderIdCustFieldId.ToString();//SecChnlOrderIdCustomField.DefinitionId = _setting.Qbo2ndChnlOrderIdCustFieldId.ToString();
            secChnlOrderIdCustomField.AnyIntuitObject = invoiceInfo.SecondaryChannelOrderID; //SecChnlOrderIdCustomField.AnyIntuitObject = qboSalesOrder.SecondaryChannelOrderId;//TODO
            secChnlOrderIdCustomField.Type = CustomFieldTypeEnum.StringType;
            customFields.Add(secChnlOrderIdCustomField);

            invoice.CustomField = customFields.ToArray();

            if (_setting.QboCustomerCreateRule == (int)CustomerCreateRule.PerMarketPlace)
            {
                PhysicalAddress shippingAddress = new PhysicalAddress();
                shippingAddress.Line1 = invoiceInfo.ShipToName;
                shippingAddress.Line2 = invoiceInfo.ShipToAddressLine1;
                shippingAddress.Line3 = invoiceInfo.ShipToAddressLine2;
                shippingAddress.Line4 = invoiceInfo.ShipToAddressLine3;
                shippingAddress.PostalCode = invoiceInfo.ShipToPostalCode;
                shippingAddress.City = invoiceInfo.ShipToCity;
                shippingAddress.Country = invoiceInfo.ShipToCountry;
                shippingAddress.CountrySubDivisionCode = invoiceInfo.ShipToState;

                invoice.ShipAddr = shippingAddress;

                PhysicalAddress billingAddress = new PhysicalAddress();
                billingAddress.Line1 = invoiceInfo.BillToName;
                billingAddress.Line2 = invoiceInfo.BillToAddressLine1;
                billingAddress.Line3 = invoiceInfo.BillToAddressLine2;
                billingAddress.Line4 = invoiceInfo.BillToAddressLine3;
                billingAddress.PostalCode = invoiceInfo.BillToPostalCode;
                billingAddress.City = invoiceInfo.BillToCity;
                billingAddress.Country = invoiceInfo.BillToCountry;
                billingAddress.CountrySubDivisionCode = invoiceInfo.BillToState;

                invoice.BillAddr = billingAddress;
            }
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
            if (_setting.SalesTaxExportRule == (int)SalesTaxExportRule.ExportToDefaultSaleTaxItemAccount)
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
