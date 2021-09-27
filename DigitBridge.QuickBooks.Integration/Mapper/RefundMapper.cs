using DigitBridge.CommerceCentral.ERPDb;
using Intuit.Ipp.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.QuickBooks.Integration
{
    public class RefundMapper
    {
        private QboIntegrationSetting _setting { get; set; }
        public RefundMapper(QboIntegrationSetting setting)
        {
            this._setting = setting;
            PrepareSetting();
        }
        protected void PrepareSetting()
        {
            _setting.QboShippingItemId = string.IsNullOrEmpty(_setting.QboShippingItemId) ? QboMappingConsts.SippingCostRefValue : _setting.QboShippingItemId;
        }
        protected Line ItemToQboLine(InvoiceReturnItems item)
        {
            Line line = new Line();

            line.Description = item.Description;
            line.Amount = item.IsAr ? item.ExtAmount : 0;//TODO check this one
            line.AmountSpecified = true;
            line.LineNum = item.InvoiceItemsUuid;

            line.AnyIntuitObject = new SalesItemLineDetail()
            {
                ItemRef = new ReferenceType() { Value = item.InvoiceItemsUuid },
                Qty = item.ReceiveQty,
                QtySpecified = true,
                AnyIntuitObject = item.InvoiceDiscountPrice,
                ItemElementName = ItemChoiceType.UnitPrice,
            };
            line.DetailType = LineDetailTypeEnum.SalesItemLineDetail;
            line.DetailTypeSpecified = true;
            //TODO check in Qbo invoice page Amount=Qty*AnyIntuitObject
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
                    name = _setting.QboShippingItemName,
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
                    name = _setting.QboMiscItemName,
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
                    name = _setting.QboChargeAndAllowanceItemName,
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
                    name = _setting.QboSalesTaxItemName,
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
            if (_setting.SalesTaxExportRule == (int)SalesTaxExportRule.ExportToDefaultSaleTaxItemAccount)
                lines.Add(TaxCostToQboLine(tranData.InvoiceTransaction));
            return lines;
        }


        #region Map Qbo RefundReceipt 

        protected RefundReceipt ToQboRefundReceipt(InvoiceTransactionData tranData)
        {
            var refund = new RefundReceipt();
            var tran = tranData.InvoiceTransaction;
            refund.DocNumber = tran.InvoiceNumber + "_" + tran.TransNum;//tran.TransUuid//TODO check use which one. 
            refund.TotalAmt = tran.TotalAmount;
            refund.TxnDate = tran.TransDate;
            refund.TxnDateSpecified = true;
            //refund.PaymentMethodRef = new ReferenceType() 
            //{ };
            refund.DepositToAccountRef = new ReferenceType() //required.
            {
                Value = tran.BankAccountUuid,//TODO check which account.(BankAccountCode,CreditAccount,DebitAccount,BankAccountUuid) 
                type = ""//Bank?Credit?Debit or All? this is an option.
            };

            //TODO
            //invoice.PrivateNote = invoiceInfo.s//qboSalesOrder.PrivateNote = fulfilledOrder.OrderHeader.SellerPrivateNote;
            //invoice.CustomerMemo = new MemoRef() { Value = qboSalesOrder.CustomerMemo };//qboSalesOrder.CustomerMemo = fulfilledOrder.OrderHeader.SellerPublicNote;

            var invoiceInfo = tranData.InvoiceData.InvoiceHeaderInfo;
            var invoiceHeader = tranData.InvoiceData.InvoiceHeader;
            AppendAddressToInvoice(refund, invoiceInfo);
            AppendCustomFieldToInvoice(refund, invoiceInfo);
            AppendCustomerToInvoice(refund, invoiceHeader);
            return refund;
        }
        private void AppendCustomFieldToInvoice(RefundReceipt refund, InvoiceHeaderInfo invoiceInfo)
        {
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

            refund.CustomField = customFields.ToArray();
        }
        private void AppendCustomerToInvoice(RefundReceipt refund, InvoiceHeader invoiceHeader)
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
        private void AppendAddressToInvoice(RefundReceipt refund, InvoiceHeaderInfo invoiceInfo)
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
        public RefundReceipt ToRefundReceipt(InvoiceTransactionData tranData)
        {
            var refund = ToQboRefundReceipt(tranData);
            refund.Line = ToQboLines(tranData).ToArray();
            return refund;
        }
    }
}
