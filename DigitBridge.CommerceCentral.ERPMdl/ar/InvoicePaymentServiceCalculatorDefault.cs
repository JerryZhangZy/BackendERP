

//-------------------------------------------------------------------------
// This document is generated by T4
// It will overwrite your changes, please keep it as it is
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    /// <summary>
    /// Represents a default InvoiceTransactionService Calculator class.
    /// </summary>
    public partial class InvoicePaymentServiceCalculatorDefault : ICalculator<InvoiceTransactionData>
    {
        protected IDataBaseFactory dbFactory { get; set; }

        public InvoicePaymentServiceCalculatorDefault(IMessage serviceMessage, IDataBaseFactory dbFactory)
        {
            this.ServiceMessage = serviceMessage;
            this.dbFactory = dbFactory;
        }

        public virtual void PrepareData(InvoiceTransactionData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {

        }

        private DateTime now = DateTime.Now;

        public virtual bool SetDefault(InvoiceTransactionData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            SetDefaultSummary(data, processingMode);
            //SetDefaultDetail(data, processingMode);
            return true;
        }

        public virtual bool SetDefaultSummary(InvoiceTransactionData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            var sum = data.InvoiceTransaction;
            if (data is null || sum == null)
                return false;

            if (sum.TransTime.IsZero()) sum.TransTime = now.TimeOfDay;
            if (sum.TransDate.IsZero())
            {
                sum.TransDate = now.Date;
                sum.TransTime = now.TimeOfDay;
            }
            sum.UpdateDateUtc = now;

            if (processingMode == ProcessingMode.Add)
            {
                //set default tran num  
                if (sum.TransNum.IsZero())
                {
                    using (var tx = new ScopedTransaction(dbFactory))
                    {
                        sum.TransNum = InvoiceTransactionHelper.GetTranSeqNum(sum.InvoiceNumber, sum.ProfileNum.ToInt());
                    }
                }
                //for Add mode, always reset uuid
                sum.TransUuid = Guid.NewGuid().ToString();
            }

            //Set default for invoice
            var invoiceData = data.InvoiceData;
            sum.InvoiceUuid = invoiceData.InvoiceHeader.InvoiceUuid;
            
            sum.Currency = invoiceData.InvoiceHeader.Currency;
            sum.TaxRate = invoiceData.InvoiceHeader.TaxRate;
            //sum.DiscountAmount = invoiceData.InvoiceHeader.DiscountAmount;
            //sum.DiscountRate = invoiceData.InvoiceHeader.DiscountRate;
            //sum.TaxableAmount = invoiceData.InvoiceHeader.TaxableAmount;
            //sum.NonTaxableAmount = invoiceData.InvoiceHeader.NonTaxableAmount;
            //sum.ShippingAmount = invoiceData.InvoiceHeader.ShippingAmount;
            //sum.ShippingTaxAmount = invoiceData.InvoiceHeader.ShippingTaxAmount;
            //sum.MiscAmount = invoiceData.InvoiceHeader.MiscAmount;
            //sum.MiscTaxAmount = invoiceData.InvoiceHeader.MiscTaxAmount;
            //sum.ChargeAndAllowanceAmount = invoiceData.InvoiceHeader.ChargeAndAllowanceAmount;

            //sum.SalesAmount = invoiceData.InvoiceHeader.SalesAmount; 

            //sum.BankAccountCode = invoiceData.InvoiceHeader 
            //var customerData = GetCustomerData(data, invoiceData.InvoiceHeader.CustomerCode);
            //if (customerData != null && customerData.Customer != null)
            //{
            //    //sum.BankAccountCode
            //    //sum.BankAccountUuid
            //    //sum.CreditAccount= 
            //    //sum.PaidBy=
            //} 

            //EnterBy
            //UpdateBy 

            return true;
        }

        public virtual bool SetDefaultDetail(InvoiceTransactionData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            return true;
        }

        //TODO: add set default for detail line logic
        //This is generated sample code
        protected virtual bool SetDefault(InvoiceReturnItems item, InvoiceTransactionData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            return true;
        }

        public virtual bool Calculate(InvoiceTransactionData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            PrepareData(data);
            //CalculateDetail(data, processingMode);
            CalculateSummary(data, processingMode);
            return true;
        }

        public virtual bool CalculateSummary(InvoiceTransactionData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            if (data is null)
                return false;

            //TODO: add calculate summary object logic
            //This is generated sample code

            var setting = new ERPSetting();
            var sum = data.InvoiceTransaction;
            //sum.SubTotalAmount = 0;
            //sum.SalesAmount = 0;
            //sum.TotalAmount = 0;
            //sum.TaxableAmount = 0;
            //sum.NonTaxableAmount = 0;
            //sum.TaxAmount = 0;
            //sum.DiscountAmount = 0; 
            //sum.ExchangeRate
            sum.ShippingAmount = sum.ShippingAmount.ToAmount();
            sum.MiscAmount = sum.MiscAmount.ToAmount();
            sum.ChargeAndAllowanceAmount = sum.ChargeAndAllowanceAmount.ToAmount();
             
            if (setting.TaxForShippingAndHandling)
            {
                sum.ShippingTaxAmount = (sum.ShippingAmount * sum.TaxRate).ToAmount();
                sum.MiscTaxAmount = (sum.MiscAmount * sum.TaxRate).ToAmount();
            }


            sum.TotalAmount = (
                sum.SalesAmount +
                sum.TaxAmount -
                sum.ShippingAmount -
                sum.ShippingTaxAmount -
                sum.MiscAmount -
                sum.MiscTaxAmount -
                sum.ChargeAndAllowanceAmount
                ).ToAmount();

            return true;
        }

        public virtual bool CalculateDetail(InvoiceTransactionData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            return true;
        }

        protected virtual bool CalculateDetail(InvoiceReturnItems item, InvoiceTransactionData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            return true;
        }

        #region message
        [XmlIgnore, JsonIgnore]
        public virtual IList<MessageClass> Messages
        {
            get
            {
                if (ServiceMessage != null)
                    return ServiceMessage.Messages;

                if (_Messages == null)
                    _Messages = new List<MessageClass>();
                return _Messages;
            }
            set
            {
                if (ServiceMessage != null)
                    ServiceMessage.Messages = value;
                else
                    _Messages = value;
            }
        }
        protected IList<MessageClass> _Messages;
        public IMessage ServiceMessage { get; set; }
        public IList<MessageClass> AddInfo(string message, string code = null) =>
             ServiceMessage != null ? ServiceMessage.AddInfo(message, code) : Messages.AddInfo(message, code);
        public IList<MessageClass> AddWarning(string message, string code = null) =>
             ServiceMessage != null ? ServiceMessage.AddWarning(message, code) : Messages.AddWarning(message, code);
        public IList<MessageClass> AddError(string message, string code = null) =>
             ServiceMessage != null ? ServiceMessage.AddError(message, code) : Messages.AddError(message, code);
        public IList<MessageClass> AddFatal(string message, string code = null) =>
             ServiceMessage != null ? ServiceMessage.AddFatal(message, code) : Messages.AddFatal(message, code);
        public IList<MessageClass> AddDebug(string message, string code = null) =>
             ServiceMessage != null ? ServiceMessage.AddDebug(message, code) : Messages.AddDebug(message, code);

        #endregion message

    }

}


