using DigitBridge.CommerceCentral.ERPDb;
using Intuit.Ipp.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.QuickBooks.Integration
{
    public class InvoicePaymentMapper
    {
        private QboIntegrationSetting _setting { get; set; }
        public InvoicePaymentMapper(QboIntegrationSetting setting)
        {
            this._setting = setting;
            PrepareSetting();
        }
        protected void PrepareSetting()
        {
        }
        protected Line CreditToQboLine(InvoiceTransaction tran)
        {
            Line line = new Line();
            line.Amount = tran.TotalAmount;
            line.AmountSpecified = true;
            line.AnyIntuitObject = new SalesItemLineDetail()
            {
                ItemRef = new ReferenceType()
                {
                    Value = tran.CreditAccount.ToString()
                }
            };

            LinkedTxn linkedTxn = new LinkedTxn()
            {
                TxnId = tran.TransUuid, //Transaction Id of the related transaction.
                TxnType = PaymentMappingConsts.PaymentTypeCreditCardCredit,
                TxnLineId = tran.TransNum.ToString()
            };
            line.LinkedTxn = new LinkedTxn[] { linkedTxn };
            return line;
        }

        protected Line DebitToQboLine(InvoiceTransaction tran)
        {
            Line line = new Line();
            line.Amount = tran.TotalAmount;
            line.AmountSpecified = true;
            line.AnyIntuitObject = new SalesItemLineDetail()
            {
                ItemRef = new ReferenceType()
                {
                    Value = tran.DebitAccount.ToString()
                }
            };
            LinkedTxn linkedTxn = new LinkedTxn()
            {
                TxnId = tran.TransUuid, //Transaction Id of the related transaction.
                TxnType = PaymentMappingConsts.PaymentTypeExpense,
                TxnLineId = tran.TransNum.ToString()
            };
            line.LinkedTxn = new LinkedTxn[] { linkedTxn };
            return line;
        }

        public Payment ToPayment(InvoiceTransactionData tranData)
        {
            var payment = ToQboPayment(tranData);
            payment.Line = ToQboLines(tranData).ToArray();
            return payment;
        }

        protected List<Line> ToQboLines(InvoiceTransactionData tranData)
        {
            return new List<Line>()
            {
                CreditToQboLine(tranData.InvoiceTransaction),
                DebitToQboLine(tranData.InvoiceTransaction)
            };
        }

        protected Payment ToQboPayment(InvoiceTransactionData tranData)
        {
            var payment = new Payment();
            var tran = tranData.InvoiceTransaction;
            payment.TxnDate = tran.TransDate;
            payment.TxnDateSpecified = true;
            payment.TotalAmt = tran.TotalAmount;
            payment.TotalAmtSpecified = true;

            var invoiceInfo = tranData.InvoiceData.InvoiceHeaderInfo;
            var invoiceHeader = tranData.InvoiceData.InvoiceHeader;
            //AppendAddressToRefund(refund, invoiceInfo);
            AppendCustomFieldToPayment(payment, invoiceInfo);
            AppendCustomerToPayment(payment, invoiceHeader);

            return payment;
        }
        private void AppendCustomFieldToPayment(Payment payment, InvoiceHeaderInfo invoiceInfo)
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

            payment.CustomField = customFields.ToArray();
        }
        private void AppendCustomerToPayment(Payment payment, InvoiceHeader invoiceHeader)
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
            payment.CustomerRef = new ReferenceType() { Value = customerId };
        }
    }
}
