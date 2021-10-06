using DigitBridge.CommerceCentral.ERPDb;
using Intuit.Ipp.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.QuickBooks.Integration
{
    public class QboPaymentMapper
    {
        private QboIntegrationSetting _setting { get; set; }
        private QuickBooksExportLog _exportLog { get; set; }
        public QboPaymentMapper(QboIntegrationSetting setting, QuickBooksExportLog exportLog)
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
            if (this._setting == null)
            {
                _setting = new QboIntegrationSetting();
            }
        }
        protected Line CreditToQboLine(InvoiceTransaction tran, string invoiceTxnId)
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
                TxnId = invoiceTxnId,
                //TxnType = PaymentMappingConsts.PaymentTypeCreditCardCredit,
                TxnLineId = tran.TransNum.ToString()
            };
            line.LinkedTxn = new LinkedTxn[] { linkedTxn };
            return line;
        }

        protected Line DebitToQboLine(InvoiceTransaction tran, string invoiceTxnId)
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
                TxnId = invoiceTxnId,
                //TxnType = PaymentMappingConsts.PaymentTypeExpense,
                TxnLineId = tran.TransNum.ToString()
            };
            line.LinkedTxn = new LinkedTxn[] { linkedTxn };
            return line;
        }

        public Payment ToPayment(InvoiceTransaction tranData, string invoiceTxnId, InvoiceData invoiceData)
        {
            var payment = ToQboPayment(tranData, invoiceData);
            payment.Line = ToQboLines(tranData, invoiceTxnId).ToArray();
            return payment;
        }

        protected List<Line> ToQboLines(InvoiceTransaction tran, string invoiceTxnId)
        {
            return new List<Line>()
            {
                CreditToQboLine(tran,invoiceTxnId),
                DebitToQboLine(tran,  invoiceTxnId)
            };
        }

        protected Payment ToQboPayment(InvoiceTransaction tran, InvoiceData invoiceData)
        {
            var payment = new Payment();
            payment.TxnDate = tran.TransDate;
            payment.TxnDateSpecified = true;
            payment.TotalAmt = tran.TotalAmount;
            payment.TotalAmtSpecified = true;

            AppendCustomerToInvoice(payment, invoiceData.InvoiceHeader);
            AppendCustomFieldToPayment(payment, invoiceData.InvoiceHeader.InvoiceNumber);


            return payment;
        }
        private void AppendCustomerToInvoice(Payment payment, InvoiceHeader invoiceHeader)
        {
            string customerId;
            if (_setting.QboCustomerCreateRule == (int)CustomerCreateRule.PerMarketPlace)
            {
                //get ChannelQboCustomerId
                customerId = CustomerMapper.GetMarketPlaceCustomer(_setting);
            }
            else //if (_setting.QboCustomerCreateRule == (int)CustomerCreateRule.PerOrder)
            {
                //customerId = invoiceHeader.CustomerCode;//todo check this.
                customerId = "1";//TODO replace this 
            }
            payment.CustomerRef = new ReferenceType() { Value = customerId };
        }
        private void AppendCustomFieldToPayment(Payment payment, string invoiceNumber)
        {
            // Map Invoice customized fields
            List<CustomField> customFields = new List<CustomField>();

            CustomField invoiceNumberField = new CustomField();
            invoiceNumberField.DefinitionId = _setting.QboInvoiceNumberFieldID;
            invoiceNumberField.Type = CustomFieldTypeEnum.StringType;
            invoiceNumberField.AnyIntuitObject = invoiceNumber.ToCustomField();// stirng value for this field.// max length is 31.
            customFields.Add(invoiceNumberField);

            payment.CustomField = customFields.ToArray();
        }
    }
}
