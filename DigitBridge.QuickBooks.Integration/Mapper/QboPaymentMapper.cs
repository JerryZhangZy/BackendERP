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

        private string invoiceTxnId { get; set; }
        public QboPaymentMapper(QboIntegrationSetting setting, string invoiceTxnId)
        {
            this.invoiceTxnId = invoiceTxnId;
            this.setting = setting;
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
                //case (int)PaidByAr.Cash: 
                //line = CashToQboLine(tran);
                //    break;
                case (int)PaidByAr.Check:
                    line = CheckToQboLine(tran);
                    break;
                case (int)PaidByAr.CreditCard:
                    line = CreditCardToQboLine(tran);
                    break;
                //case (int)PaidByAr.Expense:
                //    line = CashToQboLine(tran);
                //break;
                default:
                    line = CashToQboLine(tran);
                    break;
            }
            return line;
        }

        protected Payment ToQboPayment(InvoiceTransactionData transactionData, Payment payment)
        {
            var tran = transactionData.InvoiceTransaction;
            var invoiceData = transactionData.InvoiceData;

            payment.TxnDate = tran.TransDate;
            payment.TxnDateSpecified = true;
            payment.TotalAmt = tran.TotalAmount;
            payment.TotalAmtSpecified = true;
            payment.PaymentType = ConvertPaymentType(tran.PaidBy);
            payment.PrivateNote = tran.Notes;
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
                case (int)PaidByAr.Cash:
                    return PaymentTypeEnum.Cash;
                case (int)PaidByAr.Check:
                    return PaymentTypeEnum.Check;
                case (int)PaidByAr.CreditCard:
                    return PaymentTypeEnum.CreditCard;
                case (int)PaidByAr.ECheck:
                    return PaymentTypeEnum.Expense;
                default:
                    return PaymentTypeEnum.Other;
            }
        }
        private int ConvertPaymentMethod(int paidBy)
        {
            switch (paidBy)
            {
                case (int)PaidByAr.Cash:
                    return (int)PaymentMethodEnum.Cash;
                case (int)PaidByAr.Check:
                    return (int)PaymentMethodEnum.Check;
                case (int)PaidByAr.CreditCard:
                    return (int)PaymentMethodEnum.OtherCreditCard;
                default:
                    return (int)PaymentMethodEnum.Other;
            }
        }

        public Payment ToPayment(InvoiceTransactionData transactionData, Payment payment)
        {
            payment = ToQboPayment(transactionData, payment);
            payment.Line = new Line[] { ToQboLine(transactionData.InvoiceTransaction) };
            return payment;
        }
    }
}
