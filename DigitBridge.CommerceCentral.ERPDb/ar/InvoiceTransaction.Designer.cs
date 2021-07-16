
              

              
    

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
using System.Text;
using Newtonsoft.Json;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a InvoiceTransaction.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("InvoiceTransaction")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("TransUuid")]
    [DtoName("InvoiceTransactionDto")]
    public partial class InvoiceTransaction : TableRepository<InvoiceTransaction, long>
    {

        public InvoiceTransaction() : base() {}
        public InvoiceTransaction(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("TransUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _transUuid;

        [Column("TransNum",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _transNum;

        [Column("InvoiceUuid",SqlDbType.VarChar,NotNull=true)]
        private string _invoiceUuid;

        [Column("TransType",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _transType;

        [Column("TransStatus",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _transStatus;

        [Column("TransDate",SqlDbType.Date,NotNull=true)]
        private DateTime _transDate;

        [Column("TransTime",SqlDbType.Time,NotNull=true)]
        private TimeSpan _transTime;

        [Column("Description",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _description;

        [Column("Notes",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _notes;

        [Column("PaidBy",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _paidBy;

        [Column("BankAccountUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _bankAccountUuid;

        [Column("CheckNum",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _checkNum;

        [Column("AuthCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _authCode;

        [Column("Currency",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _currency;

        [Column("ExchangeRate",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _exchangeRate;

        [Column("SubTotalAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _subTotalAmount;

        [Column("SalesAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _salesAmount;

        [Column("TotalAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _totalAmount;

        [Column("TaxableAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _taxableAmount;

        [Column("NonTaxableAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _nonTaxableAmount;

        [Column("TaxRate",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _taxRate;

        [Column("TaxAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _taxAmount;

        [Column("DiscountRate",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _discountRate;

        [Column("DiscountAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _discountAmount;

        [Column("ShippingAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _shippingAmount;

        [Column("ShippingTaxAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _shippingTaxAmount;

        [Column("MiscAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _miscAmount;

        [Column("MiscTaxAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _miscTaxAmount;

        [Column("ChargeAndAllowanceAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _chargeAndAllowanceAmount;

        [Column("CreditAccount",SqlDbType.BigInt,NotNull=true,IsDefault=true)]
        private long _creditAccount;

        [Column("DebitAccount",SqlDbType.BigInt,NotNull=true,IsDefault=true)]
        private long _debitAccount;

        [Column("UpdateDateUtc",SqlDbType.DateTime)]
        private DateTime? _updateDateUtc;

        [Column("EnterBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _enterBy;

        [Column("UpdateBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _updateBy;

        #endregion Fields - Generated 

        #region Properties - Generated 
		[IgnoreCompare] 
		public override string UniqueId => TransUuid; 
		public void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(TransUuid)) 
				TransUuid = Guid.NewGuid().ToString(); 
		}
        public virtual string TransUuid
        {
            get
            {
				return _transUuid?.TrimEnd(); 
            }
            set
            {
				_transUuid = value.TruncateTo(50); 
				OnPropertyChanged("TransUuid", value);
            }
        }

        public virtual int TransNum
        {
            get
            {
				return _transNum; 
            }
            set
            {
				_transNum = value; 
				OnPropertyChanged("TransNum", value);
            }
        }

        public virtual string InvoiceUuid
        {
            get
            {
				return _invoiceUuid?.TrimEnd(); 
            }
            set
            {
				_invoiceUuid = value.TruncateTo(50); 
				OnPropertyChanged("InvoiceUuid", value);
            }
        }

        public virtual int TransType
        {
            get
            {
				return _transType; 
            }
            set
            {
				_transType = value; 
				OnPropertyChanged("TransType", value);
            }
        }

        public virtual int TransStatus
        {
            get
            {
				return _transStatus; 
            }
            set
            {
				_transStatus = value; 
				OnPropertyChanged("TransStatus", value);
            }
        }

        public virtual DateTime TransDate
        {
            get
            {
				return _transDate; 
            }
            set
            {
				_transDate = value.Date.ToSqlSafeValue(); 
				OnPropertyChanged("TransDate", value);
            }
        }

        public virtual TimeSpan TransTime
        {
            get
            {
				return _transTime; 
            }
            set
            {
				_transTime = value.ToSqlSafeValue(); 
				OnPropertyChanged("TransTime", value);
            }
        }

        public virtual string Description
        {
            get
            {
				return _description?.TrimEnd(); 
            }
            set
            {
				_description = value.TruncateTo(100); 
				OnPropertyChanged("Description", value);
            }
        }

        public virtual string Notes
        {
            get
            {
				return _notes?.TrimEnd(); 
            }
            set
            {
				_notes = value.TruncateTo(500); 
				OnPropertyChanged("Notes", value);
            }
        }

        public virtual int PaidBy
        {
            get
            {
				return _paidBy; 
            }
            set
            {
				_paidBy = value; 
				OnPropertyChanged("PaidBy", value);
            }
        }

        public virtual string BankAccountUuid
        {
            get
            {
				return _bankAccountUuid?.TrimEnd(); 
            }
            set
            {
				_bankAccountUuid = value.TruncateTo(50); 
				OnPropertyChanged("BankAccountUuid", value);
            }
        }

        public virtual string CheckNum
        {
            get
            {
				return _checkNum?.TrimEnd(); 
            }
            set
            {
				_checkNum = value.TruncateTo(100); 
				OnPropertyChanged("CheckNum", value);
            }
        }

        public virtual string AuthCode
        {
            get
            {
				return _authCode?.TrimEnd(); 
            }
            set
            {
				_authCode = value.TruncateTo(100); 
				OnPropertyChanged("AuthCode", value);
            }
        }

        public virtual string Currency
        {
            get
            {
				return _currency?.TrimEnd(); 
            }
            set
            {
				_currency = value.TruncateTo(10); 
				OnPropertyChanged("Currency", value);
            }
        }

        public virtual decimal ExchangeRate
        {
            get
            {
				return _exchangeRate; 
            }
            set
            {
				_exchangeRate = value; 
				OnPropertyChanged("ExchangeRate", value);
            }
        }

        public virtual decimal SubTotalAmount
        {
            get
            {
				return _subTotalAmount; 
            }
            set
            {
				_subTotalAmount = value; 
				OnPropertyChanged("SubTotalAmount", value);
            }
        }

        public virtual decimal SalesAmount
        {
            get
            {
				return _salesAmount; 
            }
            set
            {
				_salesAmount = value; 
				OnPropertyChanged("SalesAmount", value);
            }
        }

        public virtual decimal TotalAmount
        {
            get
            {
				return _totalAmount; 
            }
            set
            {
				_totalAmount = value; 
				OnPropertyChanged("TotalAmount", value);
            }
        }

        public virtual decimal TaxableAmount
        {
            get
            {
				return _taxableAmount; 
            }
            set
            {
				_taxableAmount = value; 
				OnPropertyChanged("TaxableAmount", value);
            }
        }

        public virtual decimal NonTaxableAmount
        {
            get
            {
				return _nonTaxableAmount; 
            }
            set
            {
				_nonTaxableAmount = value; 
				OnPropertyChanged("NonTaxableAmount", value);
            }
        }

        public virtual decimal TaxRate
        {
            get
            {
				return _taxRate; 
            }
            set
            {
				_taxRate = value; 
				OnPropertyChanged("TaxRate", value);
            }
        }

        public virtual decimal TaxAmount
        {
            get
            {
				return _taxAmount; 
            }
            set
            {
				_taxAmount = value; 
				OnPropertyChanged("TaxAmount", value);
            }
        }

        public virtual decimal DiscountRate
        {
            get
            {
				return _discountRate; 
            }
            set
            {
				_discountRate = value; 
				OnPropertyChanged("DiscountRate", value);
            }
        }

        public virtual decimal DiscountAmount
        {
            get
            {
				return _discountAmount; 
            }
            set
            {
				_discountAmount = value; 
				OnPropertyChanged("DiscountAmount", value);
            }
        }

        public virtual decimal ShippingAmount
        {
            get
            {
				return _shippingAmount; 
            }
            set
            {
				_shippingAmount = value; 
				OnPropertyChanged("ShippingAmount", value);
            }
        }

        public virtual decimal ShippingTaxAmount
        {
            get
            {
				return _shippingTaxAmount; 
            }
            set
            {
				_shippingTaxAmount = value; 
				OnPropertyChanged("ShippingTaxAmount", value);
            }
        }

        public virtual decimal MiscAmount
        {
            get
            {
				return _miscAmount; 
            }
            set
            {
				_miscAmount = value; 
				OnPropertyChanged("MiscAmount", value);
            }
        }

        public virtual decimal MiscTaxAmount
        {
            get
            {
				return _miscTaxAmount; 
            }
            set
            {
				_miscTaxAmount = value; 
				OnPropertyChanged("MiscTaxAmount", value);
            }
        }

        public virtual decimal ChargeAndAllowanceAmount
        {
            get
            {
				return _chargeAndAllowanceAmount; 
            }
            set
            {
				_chargeAndAllowanceAmount = value; 
				OnPropertyChanged("ChargeAndAllowanceAmount", value);
            }
        }

        public virtual long CreditAccount
        {
            get
            {
				return _creditAccount; 
            }
            set
            {
				_creditAccount = value; 
				OnPropertyChanged("CreditAccount", value);
            }
        }

        public virtual long DebitAccount
        {
            get
            {
				return _debitAccount; 
            }
            set
            {
				_debitAccount = value; 
				OnPropertyChanged("DebitAccount", value);
            }
        }

        public virtual DateTime? UpdateDateUtc
        {
            get
            {
				if (!AllowNull && _updateDateUtc is null) 
					_updateDateUtc = new DateTime().MinValueSql(); 
				return _updateDateUtc; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_updateDateUtc = (value is null) ? (DateTime?) null : value?.Date.ToSqlSafeValue(); 
					OnPropertyChanged("UpdateDateUtc", value);
				}
            }
        }

        public virtual string EnterBy
        {
            get
            {
				return _enterBy?.TrimEnd(); 
            }
            set
            {
				_enterBy = value.TruncateTo(100); 
				OnPropertyChanged("EnterBy", value);
            }
        }

        public virtual string UpdateBy
        {
            get
            {
				return _updateBy?.TrimEnd(); 
            }
            set
            {
				_updateBy = value.TruncateTo(100); 
				OnPropertyChanged("UpdateBy", value);
            }
        }



        #endregion Properties - Generated 

        #region Methods - Parent

		[XmlIgnore, JsonIgnore, IgnoreCompare]
		private InvoiceTransactionData Parent { get; set; }
		public InvoiceTransactionData GetParent() => Parent;
		public InvoiceTransaction SetParent(InvoiceTransactionData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			TransUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override InvoiceTransaction Clear()
        {
            base.Clear();
			_transUuid = String.Empty; 
			_transNum = default(int); 
			_invoiceUuid = String.Empty; 
			_transType = default(int); 
			_transStatus = default(int); 
			_transDate = new DateTime().MinValueSql(); 
			_transTime = new TimeSpan().MinValueSql(); 
			_description = String.Empty; 
			_notes = String.Empty; 
			_paidBy = default(int); 
			_bankAccountUuid = String.Empty; 
			_checkNum = String.Empty; 
			_authCode = String.Empty; 
			_currency = String.Empty; 
			_exchangeRate = default(decimal); 
			_subTotalAmount = default(decimal); 
			_salesAmount = default(decimal); 
			_totalAmount = default(decimal); 
			_taxableAmount = default(decimal); 
			_nonTaxableAmount = default(decimal); 
			_taxRate = default(decimal); 
			_taxAmount = default(decimal); 
			_discountRate = default(decimal); 
			_discountAmount = default(decimal); 
			_shippingAmount = default(decimal); 
			_shippingTaxAmount = default(decimal); 
			_miscAmount = default(decimal); 
			_miscTaxAmount = default(decimal); 
			_chargeAndAllowanceAmount = default(decimal); 
			_creditAccount = default(long); 
			_debitAccount = default(long); 
			_updateDateUtc = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_enterBy = String.Empty; 
			_updateBy = String.Empty; 
            ClearChildren();
            return this;
        }

        public virtual InvoiceTransaction ClearChildren()
        {
            return this;
        }

        public virtual InvoiceTransaction NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(InvoiceTransaction data)
        {
            if (data is null) return;
            return;
        }

		public static IList<InvoiceTransaction> FindByInvoiceUuid(IDataBaseFactory dbFactory, string invoiceUuid)
		{
			return dbFactory.Find<InvoiceTransaction>("WHERE InvoiceUuid = @0 ", invoiceUuid).ToList();
		}
		public static long CountByInvoiceUuid(IDataBaseFactory dbFactory, string invoiceUuid)
		{
			return dbFactory.Count<InvoiceTransaction>("WHERE InvoiceUuid = @0 ", invoiceUuid);
		}
		public static async Task<IList<InvoiceTransaction>> FindByAsyncInvoiceUuid(IDataBaseFactory dbFactory, string invoiceUuid)
		{
			return (await dbFactory.FindAsync<InvoiceTransaction>("WHERE InvoiceUuid = @0 ", invoiceUuid)).ToList();
		}
		public static async Task<long> CountByAsyncInvoiceUuid(IDataBaseFactory dbFactory, string invoiceUuid)
		{
			return await dbFactory.CountAsync<InvoiceTransaction>("WHERE InvoiceUuid = @0 ", invoiceUuid);
		}


        #endregion Methods - Generated 
    }
}


