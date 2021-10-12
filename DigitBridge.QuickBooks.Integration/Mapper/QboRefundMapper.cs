using DigitBridge.CommerceCentral.ERPDb;
using Intuit.Ipp.Data;
using System;
using System.Collections.Generic;
using DigitBridge.Base.Utility;

namespace DigitBridge.QuickBooks.Integration
{
    public class QboRefundMapper
    {
        private QboIntegrationSetting _setting { get; set; }
        public QboRefundMapper(QboIntegrationSetting setting)
        {
            this._setting = setting;
            PrepareSetting();
        }
        protected void PrepareSetting()
        {
            if (this._setting == null)
            {
                _setting = new QboIntegrationSetting();
            }
            if (string.IsNullOrEmpty(_setting.QboDefaultItemId))
            {
                _setting.QboDefaultItemId = QboMappingConsts.QboDefaultItemId;
            }
            if (string.IsNullOrEmpty(_setting.QboShippingItemId))
            {
                _setting.QboShippingItemId = QboMappingConsts.SippingCostRefValue;
            }
            if (string.IsNullOrEmpty(_setting.QboMiscItemId))
            {
                _setting.QboMiscItemId = QboMappingConsts.QboMiscItemId;
            }
            if (string.IsNullOrEmpty(_setting.QboChargeAndAllowanceItemId))
            {
                _setting.QboChargeAndAllowanceItemId = QboMappingConsts.QboChargeAndAllowanceItemId;
            }
            if (string.IsNullOrEmpty(_setting.QboSalesTaxItemId))
            {
                _setting.QboSalesTaxItemId = QboMappingConsts.QboSalesTaxItemId;
            }
            if (string.IsNullOrEmpty(_setting.QboDiscountItemId))
            {
                _setting.QboDiscountItemId = QboMappingConsts.DiscountRefValue;
            }

            if (string.IsNullOrEmpty(_setting.QboInvoiceNumberFieldID))
            {
                _setting.QboInvoiceNumberFieldID = QboMappingConsts.QboInvoiceNumberFieldID;
            }
            if (string.IsNullOrEmpty(_setting.QboChnlOrderIdCustFieldId))
            {
                _setting.QboChnlOrderIdCustFieldId = QboMappingConsts.QboChnlOrderIdCustFieldId;
            }
            if (string.IsNullOrEmpty(_setting.Qbo2ndChnlOrderIdCustFieldId))
            {
                _setting.Qbo2ndChnlOrderIdCustFieldId = QboMappingConsts.Qbo2ndChnlOrderIdCustFieldId;
            }
        }

        protected Line ItemToQboLine(InvoiceReturnItems item)
        {
            Line line = new Line();

            line.Description = item.Description;
            line.Amount = item.IsAr ? item.ExtAmount : 0;//TODO check this one
            line.AmountSpecified = true;
            //line.LineNum = item.InvoiceItemsUuid;
            line.AnyIntuitObject = new SalesItemLineDetail()
            {
                ItemRef = new ReferenceType()
                {
                    Value = _setting.QboDefaultItemId,// All sku mapping to DefaultItemId in qbo. TODO mapping to qbo inventory sku. 
                },
                Qty = item.ReturnQty,
                QtySpecified = true,
                AnyIntuitObject = item.IsAr ? item.InvoiceDiscountPrice : 0,//TODO check this one
                ItemElementName = ItemChoiceType.UnitPrice,
                //DiscountAmt = item.DiscountRate.IsZero() ? item.DiscountAmount : 0,
            };
            line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            line.DetailTypeSpecified = true;
            return line;
        }

        protected Line ShippingCostToQboLine(InvoiceTransaction tran)
        {
            Line line = new Line();
            line.Amount = tran.ShippingAmount * -1;//TODO check this in qbo refund.
            line.AmountSpecified = true;
            line.AnyIntuitObject = new SalesItemLineDetail()
            {
                ItemRef = new ReferenceType()
                {
                    Value = _setting.QboShippingItemId.ToString(),
                }
            };
            line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            line.DetailTypeSpecified = true;
            line.Description = string.Format(ReturnMappingConsts.SummaryShippingLineDescription, tran.InvoiceNumber, tran.TransNum);
            return line;
        }
        protected Line MiscCostToQboLine(InvoiceTransaction tran)
        {
            Line line = new Line();
            line.Amount = tran.MiscAmount * -1;//TODO check this in qbo refund.
            line.AmountSpecified = true;
            line.AnyIntuitObject = new SalesItemLineDetail()
            {
                ItemRef = new ReferenceType()
                {
                    Value = _setting.QboMiscItemId.ToString(),
                }
            };
            line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            line.DetailTypeSpecified = true;
            line.Description = string.Format(ReturnMappingConsts.SummaryMiscLineDescription, tran.InvoiceNumber, tran.TransNum);
            return line;
        }
        protected Line ChargeAndAllowanceCostToQboLine(InvoiceTransaction tran)
        {
            Line line = new Line();
            line.Amount = tran.ChargeAndAllowanceAmount * -1;//TODO check this in qbo refund.
            line.AmountSpecified = true;
            line.AnyIntuitObject = new SalesItemLineDetail()
            {
                ItemRef = new ReferenceType()
                {
                    Value = _setting.QboChargeAndAllowanceItemId.ToString(),
                }
            };
            line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            line.DetailTypeSpecified = true;
            line.Description = string.Format(ReturnMappingConsts.SummaryChargeAndAllowanceLineDescription, tran.InvoiceNumber, tran.TransNum);
            return line;
        }
        protected Line TaxCostToQboLine(InvoiceTransaction tran)
        {
            Line line = new Line();
            line.Amount = tran.TaxAmount;
            line.AmountSpecified = true;
            line.AnyIntuitObject = new SalesItemLineDetail()
            {
                ItemRef = new ReferenceType()
                {
                    Value = _setting.QboSalesTaxItemId.ToString(),
                }
            };
            line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            line.DetailTypeSpecified = true;
            line.Description = string.Format(ReturnMappingConsts.SalesTaxItemDescription, tran.InvoiceNumber, tran.TransNum);
            return line;
        }

        protected IList<Line> ItemsToQboLine(IList<InvoiceReturnItems> items)
        {
            var lines = new List<Line>();
            foreach (var item in items)
            {
                lines.Add(ItemToQboLine(item));
            }
            return lines;
        }
        protected List<Line> ToQboLines(InvoiceTransactionData tranData)
        {
            var lines = new List<Line>();
            lines.AddRange(ItemsToQboLine(tranData.InvoiceReturnItems));
            lines.Add(ShippingCostToQboLine(tranData.InvoiceTransaction));
            lines.Add(MiscCostToQboLine(tranData.InvoiceTransaction));
            lines.Add(ChargeAndAllowanceCostToQboLine(tranData.InvoiceTransaction));
            //if (_setting.SalesTaxExportRule == (int)SalesTaxExportRule.ExportToDefaultSaleTaxItemAccount)
            lines.Add(TaxCostToQboLine(tranData.InvoiceTransaction));
            return lines;
        }


        #region Map Qbo RefundReceipt 

        protected RefundReceipt ToQboRefundReceipt(InvoiceTransactionData tranData, RefundReceipt refund)
        {
            var tran = tranData.InvoiceTransaction;
            //refund.DocNumber = tran.InvoiceNumber + "_" + tran.TransNum;//tran.TransUuid//TODO check use which one. 
            refund.TotalAmt = tran.TotalAmount;
            refund.TxnDate = tran.TransDate;
            refund.TxnDateSpecified = true;
            //refund.PaymentMethodRef = new ReferenceType() 
            //{ };
            refund.DepositToAccountRef = new ReferenceType() //required.
            {
                Value = ReturnMappingConsts.DefaultDepositToAccountRef//"35" // 
                //type = ""//Bank?Credit?Debit or All? this is an option.
            };
            refund.PrivateNote = tran.Notes;
            //TODO
            //refund.CustomerMemo = new MemoRef() { Value = qboSalesOrder.CustomerMemo };//qboSalesOrder.CustomerMemo = fulfilledOrder.OrderHeader.SellerPublicNote;

            var invoiceInfo = tranData.InvoiceData.InvoiceHeaderInfo;
            var invoiceHeader = tranData.InvoiceData.InvoiceHeader;
            //AppendAddressToRefund(refund, invoiceInfo);
            AppendCustomFieldToRefund(refund, invoiceInfo, tran.InvoiceNumber);
            AppendCustomerToRefund(refund, invoiceHeader);
            return refund;
        }
        private void AppendCustomFieldToRefund(RefundReceipt refund, InvoiceHeaderInfo invoiceInfo, string invoiceNumber)
        {
            // Map Invoice customized fields
            List<CustomField> customFields = new List<CustomField>();

            CustomField invoiceNumberField = new CustomField();
            invoiceNumberField.DefinitionId = _setting.QboInvoiceNumberFieldID;
            //invoiceNumberField.Name = _setting.QboInvoiceNumberFieldName;
            invoiceNumberField.Type = CustomFieldTypeEnum.StringType;
            invoiceNumberField.AnyIntuitObject = invoiceNumber.ToCustomField();// stirng value for this field.// max length is 31.
            customFields.Add(invoiceNumberField);

            CustomField chnlOrderIdCustField = new CustomField();
            chnlOrderIdCustField.DefinitionId = _setting.QboChnlOrderIdCustFieldId.ToString();
            chnlOrderIdCustField.AnyIntuitObject = invoiceInfo.ChannelOrderID.ToCustomField();
            chnlOrderIdCustField.Type = CustomFieldTypeEnum.StringType;
            customFields.Add(chnlOrderIdCustField);

            CustomField secChnlOrderIdCustomField = new CustomField();
            secChnlOrderIdCustomField.DefinitionId = _setting.Qbo2ndChnlOrderIdCustFieldId.ToString();
            secChnlOrderIdCustomField.AnyIntuitObject = invoiceInfo.SecondaryChannelOrderID.ToCustomField();
            secChnlOrderIdCustomField.Type = CustomFieldTypeEnum.StringType;
            customFields.Add(secChnlOrderIdCustomField);

            refund.CustomField = customFields.ToArray();
        }
        private void AppendCustomerToRefund(RefundReceipt refund, InvoiceHeader invoiceHeader)
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
            refund.CustomerRef = new ReferenceType() { Value = customerId };
        }
        private void AppendAddressToRefund(RefundReceipt refund, InvoiceHeaderInfo invoiceInfo)
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

            refund.ShipAddr = shippingAddress;

            PhysicalAddress billingAddress = new PhysicalAddress();
            billingAddress.Line1 = invoiceInfo.BillToName;
            billingAddress.Line2 = invoiceInfo.BillToAddressLine1;
            billingAddress.Line3 = invoiceInfo.BillToAddressLine2;
            billingAddress.Line4 = invoiceInfo.BillToAddressLine3;
            billingAddress.PostalCode = invoiceInfo.BillToPostalCode;
            billingAddress.City = invoiceInfo.BillToCity;
            billingAddress.Country = invoiceInfo.BillToCountry;
            billingAddress.CountrySubDivisionCode = invoiceInfo.BillToState;

            refund.BillAddr = billingAddress;
        }

        #endregion
        public RefundReceipt ToRefund(InvoiceTransactionData tranData, RefundReceipt refund)
        {
            refund = ToQboRefundReceipt(tranData, refund);
            refund.Line = ToQboLines(tranData).ToArray();
            return refund;
        }
    }
}
