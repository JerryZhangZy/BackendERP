using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using Intuit.Ipp.Data;
using System;
using System.Collections.Generic;

namespace DigitBridge.QuickBooks.Integration
{
    public class InvoiceMapper
    {
        private QboIntegrationSetting _setting { get; set; }
        private QuickBooksExportLog _exportLog { get; set; }
        public InvoiceMapper(QboIntegrationSetting setting, QuickBooksExportLog exportLog)
        {
            this._setting = setting;
            this._exportLog = exportLog;
            if (this._exportLog == null)
            {
                _exportLog = new QuickBooksExportLog();
            }
            PrepareSetting();
        }

        protected void PrepareSetting()
        {
            _setting.QboInvoiceNumberFieldName = string.IsNullOrEmpty(_setting.QboInvoiceNumberFieldName) ? QboMappingConsts.QboInvoiceNumberFieldDefaultName : _setting.QboInvoiceNumberFieldName;
            _setting.QboShippingItemId = string.IsNullOrEmpty(_setting.QboShippingItemId) ? QboMappingConsts.SippingCostRefValue : _setting.QboShippingItemId;
        }

        #region Qbo lines 

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
            if (_setting.SalesTaxExportRule == (int)TaxExportRule.ItemLine)
                lines.Add(TaxCostToQboLine(invoiceData.InvoiceHeader));
            return lines;
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
                ItemRef = new ReferenceType()
                {
                    Value = _setting.QboDefaultItemId,// All sku mapping to DefaultItemId in qbo. TODO mapping to qbo inventory sku.
                    name = _setting.QboDefaultItemName,
                },
                Qty = item.ShipQty,
                QtySpecified = true,
                AnyIntuitObject = item.IsAr ? item.DiscountPrice : 0,//TODO check this one
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
            line.Amount = invoiceHeader.DiscountAmount;
            line.AmountSpecified = true;
            line.DetailType = LineDetailTypeEnum.DiscountLineDetail;
            line.DetailTypeSpecified = true;
            line.AnyIntuitObject = new DiscountLineDetail()
            {
                PercentBased = false,
                DiscountAccountRef = new ReferenceType()
                {
                    Value = QboMappingConsts.DiscountRefValue,
                    name = QboMappingConsts.DiscountRefName,
                }
            };
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
                    Value = _setting.QboShippingItemId,
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
                    Value = _setting.QboMiscItemId,
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
                    Value = _setting.QboChargeAndAllowanceItemId,
                    name = _setting.QboChargeAndAllowanceItemName,
                }
            };
            line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            line.DetailTypeSpecified = true;
            line.Description = QboMappingConsts.SummaryChargeAndAllowanceLineDescription + invoiceHeader.InvoiceNumber;
            return line;
        }
        /// <summary>
        ///  Mapping tax To Qbo line. 
        /// </summary>
        /// <param name="invoiceHeader"></param>
        /// <returns></returns>
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

        #endregion 

        #region Map Qbo invoice 

        protected Invoice ToQboInvoice(InvoiceData invoiceData)
        {
            var invoice = new Invoice();
            var invoiceHeader = invoiceData.InvoiceHeader;
            var invoiceInfo = invoiceData.InvoiceHeaderInfo;

            //invoice.Id = _exportLog.TxnId;
            invoice.DocNumber = _exportLog.DocNumber;
            invoice.Balance = invoiceHeader.Balance;
            //invoice.TotalAmt = invoiceHeader.TotalAmount;
            //invoice.TotalAmtSpecified = true;
            if (invoiceHeader.DueDate.HasValue)
                invoice.DueDate = invoiceHeader.DueDate.Value;
            //invoice.SalesTermRef // invoiceHeader.Terms
            invoice.TxnDate = invoiceHeader.InvoiceDate;
            invoice.TxnDateSpecified = true;

            if (!invoiceHeader.ShipDate.IsZero())
            {
                invoice.ShipDate = invoiceHeader.ShipDate.Value;
                invoice.ShipDateSpecified = true;
                invoice.TrackingNum = invoiceInfo.OrderShipmentNum.ToString();
                invoice.ShipMethodRef = new ReferenceType() { Value = invoiceInfo.ShippingCarrier + " " + invoiceInfo.ShippingClass };
            }

            invoice.ApplyTaxAfterDiscount = true;
            if (_setting.SalesTaxExportRule != (int)TaxExportRule.ItemLine)
            {
                invoice.TxnTaxDetail = TaxCostToQboTxnTaxDetail(invoiceHeader);
            }
            //TODO
            //invoice.PrivateNote = invoiceInfo.s//qboSalesOrder.PrivateNote = fulfilledOrder.OrderHeader.SellerPrivateNote;
            //invoice.CustomerMemo = new MemoRef() { Value = qboSalesOrder.CustomerMemo };//qboSalesOrder.CustomerMemo = fulfilledOrder.OrderHeader.SellerPublicNote;

            AppendAddressToInvoice(invoice, invoiceInfo);
            AppendCustomFieldToInvoice(invoice, invoiceInfo, invoiceHeader.InvoiceNumber);
            AppendCustomerToInvoice(invoice, invoiceHeader);
            return invoice;
        }
        /// <summary>
        /// Add CustomField to Qbo invoice. CustomField max num is 3.
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="invoiceInfo"></param>
        /// <param name="invoiceNumber"></param>
        private void AppendCustomFieldToInvoice(Invoice invoice, InvoiceHeaderInfo invoiceInfo, string invoiceNumber)
        {
            // Map Invoice customized fields
            List<CustomField> customFields = new List<CustomField>();

            CustomField invoiceNumberField = new CustomField();
            invoiceNumberField.DefinitionId = _setting.QboInvoiceNumberFieldID;
            //invoiceNumberField.Name = _setting.QboInvoiceNumberFieldName;
            invoiceNumberField.Type = CustomFieldTypeEnum.StringType;
            invoiceNumberField.AnyIntuitObject = invoiceNumber;// stirng value for this field.
            customFields.Add(invoiceNumberField);


            //CustomField endCustoerPoNumCustField = new CustomField();
            //endCustoerPoNumCustField.DefinitionId = _setting.QboEndCustomerPoNumCustFieldId.ToString();
            ////endCustoerPoNumCustField.Name = _setting.QboEndCustomerPoNumCustFieldName;
            //endCustoerPoNumCustField.AnyIntuitObject = invoiceInfo.CustomerPoNum;
            //endCustoerPoNumCustField.Type = CustomFieldTypeEnum.StringType;
            //customFields.Add(endCustoerPoNumCustField);

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
        }
        private void AppendCustomerToInvoice(Invoice invoice, InvoiceHeader invoiceHeader)
        {
            string customerId;
            if (_setting.QboCustomerCreateRule == (int)CustomerCreateRule.PerMarketPlace)
            {
                //get ChannelQboCustomerId
                customerId = CustomerMapper.GetMarketPlaceCustomer(_setting);
            }
            else //if (_setting.QboCustomerCreateRule == (int)CustomerCreateRule.PerOrder)
            {
                customerId = invoiceHeader.CustomerCode;//todo check this.
            }
            invoice.CustomerRef = new ReferenceType() { Value = customerId };
        }
        private void AppendAddressToInvoice(Invoice invoice, InvoiceHeaderInfo invoiceInfo)
        {
            if (_setting.QboCustomerCreateRule != (int)CustomerCreateRule.PerMarketPlace)
            {
                return;
            }
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
        /// <summary>
        ///  Mapping tax To Qbo invoice tax detail.
        /// </summary>
        /// <param name="invoiceHeader"></param>
        /// <returns></returns>
        protected TxnTaxDetail TaxCostToQboTxnTaxDetail(InvoiceHeader invoiceHeader)
        {
            var taxDetail = new TxnTaxDetail();
            taxDetail.TotalTax = invoiceHeader.TaxAmount;

            Line line = new Line()
            {
                DetailType = LineDetailTypeEnum.TaxLineDetail,
                Amount = invoiceHeader.TaxAmount,
                AnyIntuitObject = new TaxLineDetail()
                {
                    PercentBased = false,
                    NetAmountTaxable = invoiceHeader.TaxableAmount,
                    TaxPercent = invoiceHeader.TaxRate * 100
                }
            };
            taxDetail.TaxLine = new Line[] { line };
            return taxDetail;
        }
        #endregion


        public Invoice ToInvoice(InvoiceData invoiceData)
        {
            var invoice = ToQboInvoice(invoiceData);
            invoice.Line = ToQboLines(invoiceData).ToArray();
            return invoice;
        }
    }
}
