

              
              
    

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
    /// Represents a InvoiceHeader.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("InvoiceHeader")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("InvoiceUuid")]
    [DtoName("InvoiceHeaderDto")]
    public partial class InvoiceHeader : TableRepository<InvoiceHeader, long>
    {

        public InvoiceHeader() : base() {}
        public InvoiceHeader(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("DatabaseNum",SqlDbType.Int,NotNull=true)]
        private int _databaseNum;

        [Column("MasterAccountNum",SqlDbType.Int,NotNull=true)]
        private int _masterAccountNum;

        [Column("ProfileNum",SqlDbType.Int,NotNull=true)]
        private int _profileNum;

        [Column("InvoiceUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _invoiceUuid;

        [Column("InvoiceNumber",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _invoiceNumber;

        [Column("QboDocNumber",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _qboDocNumber;

        [Column("SalesOrderUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _salesOrderUuid;

        [Column("OrderNumber",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _orderNumber;

        [Column("InvoiceType",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _invoiceType;

        [Column("InvoiceStatus",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _invoiceStatus;

        [Column("InvoiceDate",SqlDbType.Date,NotNull=true)]
        private DateTime _invoiceDate;

        [Column("InvoiceTime",SqlDbType.Time,NotNull=true)]
        private TimeSpan _invoiceTime;

        [Column("DueDate",SqlDbType.Date)]
        private DateTime? _dueDate;

        [Column("BillDate",SqlDbType.Date)]
        private DateTime? _billDate;

        [Column("ShipDate",SqlDbType.Date)]
        private DateTime? _shipDate;

        [Column("CustomerUuid",SqlDbType.VarChar,NotNull=true)]
        private string _customerUuid;

        [Column("CustomerCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _customerCode;

        [Column("CustomerName",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _customerName;

        [Column("Terms",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _terms;

        [Column("TermsDays",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _termsDays;

        [Column("Currency",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _currency;

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

        [Column("PaidAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _paidAmount;

        [Column("CreditAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _creditAmount;

        [Column("Balance",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _balance;

        [Column("UnitCost",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _unitCost;

        [Column("AvgCost",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _avgCost;

        [Column("LotCost",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _lotCost;

        [Column("InvoiceSourceCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _invoiceSourceCode;

        [Column("UpdateDateUtc",SqlDbType.DateTime)]
        private DateTime? _updateDateUtc;

        [Column("EnterBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _enterBy;

        [Column("UpdateBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _updateBy;

        #endregion Fields - Generated 

        #region Properties - Generated 
		[IgnoreCompare] 
		public override string UniqueId => InvoiceUuid; 
		public override void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(InvoiceUuid)) 
				InvoiceUuid = Guid.NewGuid().ToString(); 
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
		/// Readable QboDocNumber, when push record to quickbook update number. <br> when push record to quickbook update number.
		/// </summary>
        public virtual string QboDocNumber
        {
            get
            {
				return _qboDocNumber?.TrimEnd(); 
            }
            set
            {
				_qboDocNumber = value.TruncateTo(50); 
				OnPropertyChanged("QboDocNumber", value);
            }
        }

		/// <summary>
		/// Link to Order uuid. <br> Display: false, Editable: false.
		/// </summary>
        public virtual string SalesOrderUuid
        {
            get
            {
				return _salesOrderUuid?.TrimEnd(); 
            }
            set
            {
				_salesOrderUuid = value.TruncateTo(50); 
				OnPropertyChanged("SalesOrderUuid", value);
            }
        }

		/// <summary>
		/// Link to order number, unique in same database and profile. <br> Parameter should pass ProfileNum-OrderNumber. <br> Title: Order Number, Display: true, Editable: false
		/// </summary>
        public virtual string OrderNumber
        {
            get
            {
				return _orderNumber?.TrimEnd(); 
            }
            set
            {
				_orderNumber = value.TruncateTo(50); 
				OnPropertyChanged("OrderNumber", value);
            }
        }

		/// <summary>
		/// Invoice type. <br> Title: Type, Display: true, Editable: true
		/// </summary>
        public virtual int InvoiceType
        {
            get
            {
				return _invoiceType; 
            }
            set
            {
				_invoiceType = value; 
				OnPropertyChanged("InvoiceType", value);
            }
        }

		/// <summary>
		/// Invoice status. <br> Title: Status, Display: true, Editable: true
		/// </summary>
        public virtual int InvoiceStatus
        {
            get
            {
				return _invoiceStatus; 
            }
            set
            {
				_invoiceStatus = value; 
				OnPropertyChanged("InvoiceStatus", value);
            }
        }

		/// <summary>
		/// Invoice date. <br> Title: Date, Display: true, Editable: true
		/// </summary>
        public virtual DateTime InvoiceDate
        {
            get
            {
				return _invoiceDate; 
            }
            set
            {
				_invoiceDate = value.Date.ToSqlSafeValue(); 
				OnPropertyChanged("InvoiceDate", value);
            }
        }

		/// <summary>
		/// Invoice time. <br> Title: Time, Display: true, Editable: true
		/// </summary>
        public virtual TimeSpan InvoiceTime
        {
            get
            {
				return _invoiceTime; 
            }
            set
            {
				_invoiceTime = value.ToSqlSafeValue(); 
				OnPropertyChanged("InvoiceTime", value);
            }
        }

		/// <summary>
		/// Balance Due date. <br> Title: Date, Display: true, Editable: false
		/// </summary>
        public virtual DateTime? DueDate
        {
            get
            {
				if (!AllowNull && _dueDate is null) 
					_dueDate = new DateTime().MinValueSql(); 
				return _dueDate; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_dueDate = (value is null) ? (DateTime?) null : value?.Date.ToSqlSafeValue(); 
					OnPropertyChanged("DueDate", value);
				}
            }
        }

		/// <summary>
		/// (Ignore) Next Billing date.
		/// </summary>
        public virtual DateTime? BillDate
        {
            get
            {
				if (!AllowNull && _billDate is null) 
					_billDate = new DateTime().MinValueSql(); 
				return _billDate; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_billDate = (value is null) ? (DateTime?) null : value?.Date.ToSqlSafeValue(); 
					OnPropertyChanged("BillDate", value);
				}
            }
        }

		/// <summary>
		/// Estimated vendor ship date. <br> Title: Ship Date, Display: true, Editable: true
		/// </summary>
        public virtual DateTime? ShipDate
        {
            get
            {
				if (!AllowNull && _shipDate is null) 
					_shipDate = new DateTime().MinValueSql(); 
				return _shipDate; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_shipDate = (value is null) ? (DateTime?) null : value?.Date.ToSqlSafeValue(); 
					OnPropertyChanged("ShipDate", value);
				}
            }
        }

		/// <summary>
		/// Customer uuid, load from customer data. <br> Display: false, Editable: false
		/// </summary>
        public virtual string CustomerUuid
        {
            get
            {
				return _customerUuid?.TrimEnd(); 
            }
            set
            {
				_customerUuid = value.TruncateTo(50); 
				OnPropertyChanged("CustomerUuid", value);
            }
        }

		/// <summary>
		/// Customer number. use DatabaseNum-CustomerCode too load customer data. <br> Title: Customer Number, Display: true, Editable: true
		/// </summary>
        public virtual string CustomerCode
        {
            get
            {
				return _customerCode?.TrimEnd(); 
            }
            set
            {
				_customerCode = value.TruncateTo(50); 
				OnPropertyChanged("CustomerCode", value);
            }
        }

		/// <summary>
		/// (Readonly) Customer name, load from customer data. <br> Title: Customer Name, Display: true, Editable: false
		/// </summary>
        public virtual string CustomerName
        {
            get
            {
				return _customerName?.TrimEnd(); 
            }
            set
            {
				_customerName = value.TruncateTo(200); 
				OnPropertyChanged("CustomerName", value);
            }
        }

		/// <summary>
		/// Payment terms, default from customer data. <br> Title: Terms, Display: true, Editable: true
		/// </summary>
        public virtual string Terms
        {
            get
            {
				return _terms?.TrimEnd(); 
            }
            set
            {
				_terms = value.TruncateTo(50); 
				OnPropertyChanged("Terms", value);
            }
        }

		/// <summary>
		/// Payment terms days, default from customer data. <br> Title: Days, Display: true, Editable: true
		/// </summary>
        public virtual int TermsDays
        {
            get
            {
				return _termsDays; 
            }
            set
            {
				_termsDays = value; 
				OnPropertyChanged("TermsDays", value);
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
		/// Total Paid amount. <br> Display: true, Editable: false
		/// </summary>
        public virtual decimal PaidAmount
        {
            get
            {
				return _paidAmount; 
            }
            set
            {
				_paidAmount = value; 
				OnPropertyChanged("PaidAmount", value);
            }
        }

		/// <summary>
		/// Total Credit amount. <br> Display: true, Editable: false
		/// </summary>
        public virtual decimal CreditAmount
        {
            get
            {
				return _creditAmount; 
            }
            set
            {
				_creditAmount = value; 
				OnPropertyChanged("CreditAmount", value);
            }
        }

		/// <summary>
		/// Current balance of Invoice. <br> Display: true, Editable: false
		/// </summary>
        public virtual decimal Balance
        {
            get
            {
				return _balance; 
            }
            set
            {
				_balance = value; 
				OnPropertyChanged("Balance", value);
            }
        }

		/// <summary>
		/// (Ignore) Total Unit Cost. <br> Display: false, Editable: false
		/// </summary>
        public virtual decimal UnitCost
        {
            get
            {
				return _unitCost; 
            }
            set
            {
				_unitCost = value; 
				OnPropertyChanged("UnitCost", value);
            }
        }

		/// <summary>
		/// (Ignore) Total Avg.Cost. <br> Display: false, Editable: false
		/// </summary>
        public virtual decimal AvgCost
        {
            get
            {
				return _avgCost; 
            }
            set
            {
				_avgCost = value; 
				OnPropertyChanged("AvgCost", value);
            }
        }

		/// <summary>
		/// (Ignore) Total Lot Cost. <br> Display: false, Editable: false
		/// </summary>
        public virtual decimal LotCost
        {
            get
            {
				return _lotCost; 
            }
            set
            {
				_lotCost = value; 
				OnPropertyChanged("LotCost", value);
            }
        }

		/// <summary>
		/// (Readonly) Invoice created from other entity number, use to prevent import duplicate invoice. <br> Title: Source Number, Display: false, Editable: false
		/// </summary>
        public virtual string InvoiceSourceCode
        {
            get
            {
				return _invoiceSourceCode?.TrimEnd(); 
            }
            set
            {
				_invoiceSourceCode = value.TruncateTo(100); 
				OnPropertyChanged("InvoiceSourceCode", value);
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
		/// (Readonly) User who created this order. <br> Title: Created By, Display: true, Editable: false
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
		private InvoiceData Parent { get; set; }
		public InvoiceData GetParent() => Parent;
		public InvoiceHeader SetParent(InvoiceData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			InvoiceUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override InvoiceHeader Clear()
        {
            base.Clear();
			_databaseNum = default(int); 
			_masterAccountNum = default(int); 
			_profileNum = default(int); 
			_invoiceUuid = String.Empty; 
			_invoiceNumber = String.Empty; 
			_qboDocNumber = String.Empty; 
			_salesOrderUuid = String.Empty; 
			_orderNumber = String.Empty; 
			_invoiceType = default(int); 
			_invoiceStatus = default(int); 
			_invoiceDate = new DateTime().MinValueSql(); 
			_invoiceTime = new TimeSpan().MinValueSql(); 
			_dueDate = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_billDate = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_shipDate = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_customerUuid = String.Empty; 
			_customerCode = String.Empty; 
			_customerName = String.Empty; 
			_terms = String.Empty; 
			_termsDays = default(int); 
			_currency = String.Empty; 
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
			_paidAmount = default(decimal); 
			_creditAmount = default(decimal); 
			_balance = default(decimal); 
			_unitCost = default(decimal); 
			_avgCost = default(decimal); 
			_lotCost = default(decimal); 
			_invoiceSourceCode = String.Empty; 
			_updateDateUtc = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_enterBy = String.Empty; 
			_updateBy = String.Empty; 
            ClearChildren();
            return this;
        }

        public override InvoiceHeader CheckIntegrity()
        {
            CheckUniqueId();
            CheckIntegrityOthers();
            return this;
        }

        public virtual InvoiceHeader ClearChildren()
        {
            return this;
        }

        public virtual InvoiceHeader NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(InvoiceHeader data)
        {
            if (data is null) return;
            return;
        }


		public override InvoiceHeader ConvertDbFieldsToData()
		{
			base.ConvertDbFieldsToData();
			return this;
		}
		public override InvoiceHeader ConvertDataFieldsToDb()
		{
			base.ConvertDataFieldsToDb();
			UpdateDateUtc =DateTime.UtcNow;
			return this;
		}

        #endregion Methods - Generated 
    }
}



