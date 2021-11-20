              
              
    

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
        [Column("DatabaseNum",SqlDbType.Int,NotNull=true)]
        private int _databaseNum;

        [Column("MasterAccountNum",SqlDbType.Int,NotNull=true)]
        private int _masterAccountNum;

        [Column("ProfileNum",SqlDbType.Int,NotNull=true)]
        private int _profileNum;

        [Column("TransUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _transUuid;

        [Column("TransNum",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _transNum;

        [Column("InvoiceUuid",SqlDbType.VarChar,NotNull=true)]
        private string _invoiceUuid;

        [Column("InvoiceNumber",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _invoiceNumber;

        [Column("PaymentUuid",SqlDbType.VarChar,NotNull=true)]
        private string _paymentUuid;

        [Column("PaymentNumber",SqlDbType.VarChar,NotNull=true)]
        private string _paymentNumber;

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

        [Column("BankAccountCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _bankAccountCode;

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

        [Column("TransSourceCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _transSourceCode;

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
		public override void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(TransUuid)) 
				TransUuid = Guid.NewGuid().ToString(); 
		}
		/// <summary>
		/// (Readonly) Database Number. <br> Display: false, Editable: false.
		/// </summary>
        public virtual int DatabaseNum
        {
            get
            {
				return _databaseNum; 
            }
            set
            {
				_databaseNum = value; 
				OnPropertyChanged("DatabaseNum", value);
            }
        }

		/// <summary>
		/// (Readonly) Login user account. <br> Display: false, Editable: false.
		/// </summary>
        public virtual int MasterAccountNum
        {
            get
            {
				return _masterAccountNum; 
            }
            set
            {
				_masterAccountNum = value; 
				OnPropertyChanged("MasterAccountNum", value);
            }
        }

		/// <summary>
		/// (Readonly) Login user profile. <br> Display: false, Editable: false.
		/// </summary>
        public virtual int ProfileNum
        {
            get
            {
				return _profileNum; 
            }
            set
            {
				_profileNum = value; 
				OnPropertyChanged("ProfileNum", value);
            }
        }

		/// <summary>
		/// Invoice Transaction uuid. <br> Display: false, Editable: false.
		/// </summary>
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

		/// <summary>
		/// Readable invoice transaction number, unique in same database and profile. <br> Parameter should pass ProfileNum-InvoiceNumber-TransNum. <br> Title: Order Number, Display: true, Editable: true
		/// </summary>
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

		/// <summary>
		/// Invoice uuid. <br> Display: false, Editable: false.
		/// </summary>
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

		/// <summary>
		/// Readable invoice number, unique in same database and profile. <br> Parameter should pass ProfileNum-OrderNumber. <br> Title: Order Number, Display: true, Editable: true
		/// </summary>
        public virtual string InvoiceNumber
        {
            get
            {
				return _invoiceNumber?.TrimEnd(); 
            }
            set
            {
				_invoiceNumber = value.TruncateTo(50); 
				OnPropertyChanged("InvoiceNumber", value);
            }
        }

		/// <summary>
		/// Group Payment uuid. <br> Display: false, Editable: false.
		/// </summary>
        public virtual string PaymentUuid
        {
            get
            {
				return _paymentUuid?.TrimEnd(); 
            }
            set
            {
				_paymentUuid = value.TruncateTo(50); 
				OnPropertyChanged("PaymentUuid", value);
            }
        }

		/// <summary>
		/// Group Payment readable Number. <br> Display: false, Editable: false.
		/// </summary>
        public virtual string PaymentNumber
        {
            get
            {
				return _paymentNumber?.TrimEnd(); 
            }
            set
            {
				_paymentNumber = value.TruncateTo(50); 
				OnPropertyChanged("PaymentNumber", value);
            }
        }

		/// <summary>
		/// Transaction type, payment, return. <br> Title: Type, Display: true, Editable: true
		/// </summary>
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

		/// <summary>
		/// Transaction status. <br> Title: Status, Display: true, Editable: true
		/// </summary>
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

		/// <summary>
		/// Invoice date. <br> Title: Date, Display: true, Editable: true
		/// </summary>
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

		/// <summary>
		/// Invoice time. <br> Title: Time, Display: true, Editable: true
		/// </summary>
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

		/// <summary>
		/// Description of Invoice Transaction. <br> Title: Description, Display: true, Editable: true
		/// </summary>
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

		/// <summary>
		/// Notes of Invoice Transaction. <br> Title: Notes, Display: true, Editable: true
		/// </summary>
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

		/// <summary>
		/// Payment method number. <br> Title: Paid By, Display: true, Editable: true
		/// </summary>
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

		/// <summary>
		/// Payment bank account uuid.
		/// </summary>
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

		/// <summary>
		/// Readable payment Bank account code. <br> Title: Bank, Display: true, Editable: true
		/// </summary>
        public virtual string BankAccountCode
        {
            get
            {
				return _bankAccountCode?.TrimEnd(); 
            }
            set
            {
				_bankAccountCode = value.TruncateTo(50); 
				OnPropertyChanged("BankAccountCode", value);
            }
        }

		/// <summary>
		/// Check number. <br> Title: Check No., Display: true, Editable: true
		/// </summary>
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

		/// <summary>
		/// Auth code from merchant bank. <br> Title: Auth. No., Display: true, Editable: true
		/// </summary>
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

		/// <summary>
		/// Currency code. <br> Title: Currency, Display: true, Editable: true
		/// </summary>
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

		/// <summary>
		/// Realtime Exchange Rate when process transaction. <br> Title: Exchange Rate, Display: true, Editable: true
		/// </summary>
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

		/// <summary>
		/// (Readonly) Sub total amount of items. Sales amount without discount, tax and other charge. <br> Title: Subtotal, Display: true, Editable: false
		/// </summary>
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

		/// <summary>
		/// (Readonly) Sub Total amount deduct discount, but not include tax and other charge. <br> Title: Sales Amount, Display: true, Editable: false
		/// </summary>
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

		/// <summary>
		/// (Readonly) Total amount. Include every charge (tax, shipping, misc...). <br> Title: Total, Display: true, Editable: false
		/// </summary>
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

		/// <summary>
		/// (Readonly) Amount should apply tax. <br> Title: Taxable Amount, Display: true, Editable: false
		/// </summary>
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

		/// <summary>
		/// (Readonly) Amount should not apply tax. <br> Title: NonTaxable, Display: true, Editable: false
		/// </summary>
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

		/// <summary>
		/// Invoice Tax rate. <br> Title: Tax, Display: true, Editable: true
		/// </summary>
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

		/// <summary>
		/// Invoice tax amount (include shipping tax and misc tax). <br> Title: Tax Amount, Display: true, Editable: false
		/// </summary>
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

		/// <summary>
		/// Invoice discount rate base on SubTotalAmount. If user enter discount rate, should recalculate discount amount. <br> Title: Discount, Display: true, Editable: true
		/// </summary>
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

		/// <summary>
		/// Invoice discount amount base on SubTotalAmount. If user enter discount amount, should set discount rate to zero. <br> Title: Discount Amount, Display: true, Editable: true
		/// </summary>
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

		/// <summary>
		/// Invoice shipping fee. <br> Title: Shipping, Display: true, Editable: true
		/// </summary>
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

		/// <summary>
		/// (Readonly) tax amount for shipping fee. <br> Title: Shipping Tax, Display: true, Editable: false
		/// </summary>
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

		/// <summary>
		/// Invoice handling charge. <br> Title: Handling, Display: true, Editable: true
		/// </summary>
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

		/// <summary>
		/// (Readonly) tax amount for handling charge. <br> Title: Handling Tax, Display: true, Editable: false
		/// </summary>
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

		/// <summary>
		/// Invoice other Charg and Allowance Amount. Positive is charge, Negative is Allowance. <br> Title: Charge&Allowance, Display: true, Editable: true
		/// </summary>
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

		/// <summary>
		/// G/L Credit account. <br> Display: true, Editable: true
		/// </summary>
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

		/// <summary>
		/// G/L Debit account. <br> Display: true, Editable: true
		/// </summary>
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

		/// <summary>
		/// (Readonly) Invoice transaction created from other entity number, use to prevent import duplicate invoice transaction. <br> Title: Source Number, Display: false, Editable: false
		/// </summary>
        public virtual string TransSourceCode
        {
            get
            {
				return _transSourceCode?.TrimEnd(); 
            }
            set
            {
				_transSourceCode = value.TruncateTo(100); 
				OnPropertyChanged("TransSourceCode", value);
            }
        }

		/// <summary>
		/// (Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
		/// </summary>
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
					_updateDateUtc = (value is null) ? (DateTime?) null : value.ToSqlSafeValue(); 
					OnPropertyChanged("UpdateDateUtc", value);
				}
            }
        }

		/// <summary>
		/// (Readonly) User who created this transaction. <br> Title: Created By, Display: true, Editable: false
		/// </summary>
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

		/// <summary>
		/// (Readonly) Last updated user. <br> Title: Update By, Display: true, Editable: false
		/// </summary>
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

		[JsonIgnore, XmlIgnore, IgnoreCompare]
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
			_databaseNum = default(int); 
			_masterAccountNum = default(int); 
			_profileNum = default(int); 
			_transUuid = String.Empty; 
			_transNum = default(int); 
			_invoiceUuid = String.Empty; 
			_invoiceNumber = String.Empty; 
			_paymentUuid = String.Empty; 
			_paymentNumber = String.Empty; 
			_transType = default(int); 
			_transStatus = default(int); 
			_transDate = new DateTime().MinValueSql(); 
			_transTime = new TimeSpan().MinValueSql(); 
			_description = String.Empty; 
			_notes = String.Empty; 
			_paidBy = default(int); 
			_bankAccountUuid = String.Empty; 
			_bankAccountCode = String.Empty; 
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
			_transSourceCode = String.Empty; 
			_updateDateUtc = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_enterBy = String.Empty; 
			_updateBy = String.Empty; 
            ClearChildren();
            return this;
        }

        public override InvoiceTransaction CheckIntegrity()
        {
            CheckUniqueId();
            CheckIntegrityOthers();
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
			return dbFactory.Find<InvoiceTransaction>("WHERE InvoiceUuid = @0 ORDER BY TransNum ", invoiceUuid).ToList();
		}
		public static long CountByInvoiceUuid(IDataBaseFactory dbFactory, string invoiceUuid)
		{
			return dbFactory.Count<InvoiceTransaction>("WHERE InvoiceUuid = @0 ", invoiceUuid);
		}
		public static async Task<IList<InvoiceTransaction>> FindByAsyncInvoiceUuid(IDataBaseFactory dbFactory, string invoiceUuid)
		{
			return (await dbFactory.FindAsync<InvoiceTransaction>("WHERE InvoiceUuid = @0 ORDER BY TransNum ", invoiceUuid)).ToList();
		}
		public static async Task<long> CountByAsyncInvoiceUuid(IDataBaseFactory dbFactory, string invoiceUuid)
		{
			return await dbFactory.CountAsync<InvoiceTransaction>("WHERE InvoiceUuid = @0 ", invoiceUuid);
		}

		public override InvoiceTransaction ConvertDbFieldsToData()
		{
			base.ConvertDbFieldsToData();
			return this;
		}
		public override InvoiceTransaction ConvertDataFieldsToDb()
		{
			base.ConvertDataFieldsToDb();
			UpdateDateUtc =DateTime.UtcNow;
			return this;
		}

        #endregion Methods - Generated 
    }
}



