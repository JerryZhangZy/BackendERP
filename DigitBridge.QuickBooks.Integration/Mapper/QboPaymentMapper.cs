using DigitBridge.Base.Common;
using DigitBridge.CommerceCentral.ERPDb;
using Intuit.Ipp.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.QuickBooks.Integration
{
    public class QboPaymentMapper
    {
        private QboIntegrationSetting setting { get; set; }
        private QuickBooksExportLog exportLog { get; set; }
        private string invoiceTxnId { get; set; }
        public QboPaymentMapper(QboIntegrationSetting setting, QuickBooksExportLog exportLog, string invoiceTxnId)
        {
            this.invoiceTxnId = invoiceTxnId;
            this.setting = setting;
            this.exportLog = exportLog;
            if (this.exportLog == null)
            {
                this.exportLog = new QuickBooksExportLog();
            }
            PrepareSetting();
        }
        protected void PrepareSetting()
        {
            if (this.setting == null)
            {
                setting = new QboIntegrationSetting();
            }
            if (string.IsNullOrEmpty(setting.QboInvoiceNumberFieldID))
            {
                setting.QboInvoiceNumberFieldID = QboMappingConsts.QboInvoiceNumberFieldID;
            }
        }
        protected Line CreditCardToQboLine(InvoiceTransaction tran)
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
                TxnType = PaymentTxtType.PaymentTypeCreditCardCredit,
                //TxnLineId = tran.TransNum.ToString()
            };
            line.LinkedTxn = new LinkedTxn[] { linkedTxn };
            return line;
        }
        protected Line CheckToQboLine(InvoiceTransaction tran)
        {
            Line line = new Line();
            line.Amount = tran.TotalAmount;
            line.AmountSpecified = true;
            line.AnyIntuitObject = new SalesItemLineDetail()
            {
                ItemRef = new ReferenceType()
                {
                    Value = tran.CheckNum
                }
            };

            LinkedTxn linkedTxn = new LinkedTxn()
            {
                TxnId = invoiceTxnId,
                TxnType = PaymentTxtType.PaymentTypeCheck
            };
            line.LinkedTxn = new LinkedTxn[] { linkedTxn };
            return line;
        }
        protected Line CashToQboLine(InvoiceTransaction tran)
        {
            Line line = new Line();
            line.Amount = tran.TotalAmount;
            line.AmountSpecified = true;
            //line.AnyIntuitObject = new SalesItemLineDetail()
            //{
            //    ItemRef = new ReferenceType()
            //    {
            //        Value = tran.c
            //    }
            //};

            LinkedTxn linkedTxn = new LinkedTxn()
            {
                TxnId = invoiceTxnId,
                TxnType = PaymentTxtType.PaymentTypeInvoice
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
                TxnId = invoiceTxnId,
                TxnType = PaymentTxtType.PaymentTypeInvoice//TODO mapper Debit cart to right txttype.
            };
            line.LinkedTxn = new LinkedTxn[] { linkedTxn };
            return line;
        }
        protected Line ToQboLine(InvoiceTransaction tran)
        {
            Line line;
            switch (tran.PaidBy)
            {
                //case (int)PaidByEnum.Cash: 
                //line = CashToQboLine(tran);
                //    break;
                case (int)PaidByEnum.Check:
                    line = CheckToQboLine(tran);
                    break;
                case (int)PaidByEnum.CreditCard:
                    line = CreditCardToQboLine(tran);
                    break;
                //case (int)PaidByEnum.Expense:
                //    line = CashToQboLine(tran);
                //break;
                default:
                    line = CashToQboLine(tran);
                    break;
            }
            return line;
        }

        protected Payment ToQboPayment(InvoiceTransaction tran, InvoiceData invoiceData)
        {
            var payment = new Payment();
            payment.TxnDate = tran.TransDate;
            payment.TxnDateSpecified = true;
            payment.TotalAmt = tran.TotalAmount;
            payment.TotalAmtSpecified = true;
            payment.Id = exportLog.TxnId;
            payment.PaymentType = ConvertPaymentType(tran.PaidBy);
            //payment.PaymentMethodRef = new ReferenceType()
            //{
            //    Value = ConvertPaymentMethod(tran.PaidBy).ToString()
            //};
            LinkedTxn linkedTxn = new LinkedTxn()
            {
                TxnId = invoiceTxnId,
                TxnType = PaymentTxtType.PaymentTypeInvoice
            };
            payment.LinkedTxn = new LinkedTxn[] { linkedTxn };
            AppendCustomerToInvoice(payment, invoiceData.InvoiceHeader);
            AppendCustomFieldToPayment(payment, invoiceData.InvoiceHeader.InvoiceNumber);
            return payment;
        }
        private void AppendCustomerToInvoice(Payment payment, InvoiceHeader invoiceHeader)
        {
            string customerId;
            if (setting.QboCustomerCreateRule == (int)CustomerCreateRule.PerMarketPlace)
            {
                //get ChannelQboCustomerId
                customerId = CustomerMapper.GetMarketPlaceCustomer(setting);
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
            invoiceNumberField.DefinitionId = setting.QboInvoiceNumberFieldID;
            invoiceNumberField.Type = CustomFieldTypeEnum.StringType;
            invoiceNumberField.AnyIntuitObject = invoiceNumber.ToCustomField();// stirng value for this field.// max length is 31.
            customFields.Add(invoiceNumberField);
            payment.CustomField = customFields.ToArray();
        }

        private PaymentTypeEnum ConvertPaymentType(int paidBy)
        {
            switch (paidBy)
            {
                case (int)PaidByEnum.Cash:
                    return PaymentTypeEnum.Cash;
                case (int)PaidByEnum.Check:
                    return PaymentTypeEnum.Check;
                case (int)PaidByEnum.CreditCard:
                    return PaymentTypeEnum.CreditCard;
                case (int)PaidByEnum.Expense:
                    return PaymentTypeEnum.Expense;
                default:
                    return PaymentTypeEnum.Other;
            }
        }
        private int ConvertPaymentMethod(int paidBy)
        {
            switch (paidBy)
            {
                case (int)PaidByEnum.Cash:
                    return (int)PaymentMethodEnum.Cash;
                case (int)PaidByEnum.Check:
                    return (int)PaymentMethodEnum.Check;
                case (int)PaidByEnum.CreditCard:
                    return (int)PaymentMethodEnum.OtherCreditCard;
                default:
                    return (int)PaymentMethodEnum.Other;
            }
        }

        public Payment ToPayment(InvoiceTransaction tran, InvoiceData invoiceData)
        {
            var payment = ToQboPayment(tran, invoiceData);
            payment.Line = new Line[] { ToQboLine(tran) };
            return payment;
        }
    }
}
