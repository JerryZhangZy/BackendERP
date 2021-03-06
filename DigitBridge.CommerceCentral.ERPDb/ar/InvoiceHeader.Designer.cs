














    

              

              
    

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

        [Column("InvoiceNumber",SqlDbType.VarChar,NotNull=true)]
        private string _invoiceNumber;

        [Column("InvoiceType",SqlDbType.Int,IsDefault=true)]
        private int? _invoiceType;

        [Column("InvoiceStatus",SqlDbType.Int,IsDefault=true)]
        private int? _invoiceStatus;

        [Column("InvoiceDate",SqlDbType.Date,NotNull=true)]
        private DateTime _invoiceDate;

        [Column("InvoiceTime",SqlDbType.Time,NotNull=true)]
        private TimeSpan _invoiceTime;

        [Column("DueDate",SqlDbType.Date)]
        private DateTime? _dueDate;

        [Column("BillDate",SqlDbType.Date)]
        private DateTime? _billDate;

        [Column("CustomerUuid",SqlDbType.VarChar)]
        private string _customerUuid;

        [Column("CustomerNum",SqlDbType.VarChar)]
        private string _customerNum;

        [Column("CustomerName",SqlDbType.NVarChar)]
        private string _customerName;

        [Column("Currency",SqlDbType.VarChar)]
        private string _currency;

        [Column("SubTotalAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _subTotalAmount;

        [Column("TotalAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _totalAmount;

        [Column("TaxRate",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _taxRate;

        [Column("TaxAmount",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _taxAmount;

        [Column("DiscountRate",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _discountRate;

        [Column("DiscountAmount",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _discountAmount;

        [Column("ShippingAmount",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _shippingAmount;

        [Column("ShippingTaxAmount",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _shippingTaxAmount;

        [Column("MiscAmount",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _miscAmount;

        [Column("MiscTaxAmount",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _miscTaxAmount;

        [Column("ChargeAndAllowanceAmount",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _chargeAndAllowanceAmount;

        [Column("PaidAmount",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _paidAmount;

        [Column("CreditAmount",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _creditAmount;

        [Column("Balance",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _balance;

        [Column("UnitCost",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _unitCost;

        [Column("AvgCost",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _avgCost;

        [Column("LotCost",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _lotCost;

        [Column("UpdateDateUtc",SqlDbType.DateTime)]
        private DateTime? _updateDateUtc;

        [Column("EnterBy",SqlDbType.VarChar,NotNull=true)]
        private string _enterBy;

        [Column("UpdateBy",SqlDbType.VarChar,NotNull=true)]
        private string _updateBy;

        #endregion Fields - Generated 

        #region Properties - Generated 
		public override string UniqueId => InvoiceUuid; 
		public void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(InvoiceUuid)) 
				InvoiceUuid = Guid.NewGuid().ToString(); 
		}
        public virtual int DatabaseNum
        {
            get
            {
				return _databaseNum; 
            }
            set
            {
				_databaseNum = value; 
            }
        }

        public virtual int MasterAccountNum
        {
            get
            {
				return _masterAccountNum; 
            }
            set
            {
				_masterAccountNum = value; 
            }
        }

        public virtual int ProfileNum
        {
            get
            {
				return _profileNum; 
            }
            set
            {
				_profileNum = value; 
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
            }
        }

        public virtual string InvoiceNumber
        {
            get
            {
				return _invoiceNumber?.TrimEnd(); 
            }
            set
            {
				_invoiceNumber = value.TruncateTo(50); 
            }
        }

        public virtual int? InvoiceType
        {
            get
            {
				if (!AllowNull && _invoiceType is null) 
					_invoiceType = default(int); 
				return _invoiceType; 
            }
            set
            {
				if (value != null || AllowNull) 
					_invoiceType = value; 
            }
        }

        public virtual int? InvoiceStatus
        {
            get
            {
				if (!AllowNull && _invoiceStatus is null) 
					_invoiceStatus = default(int); 
				return _invoiceStatus; 
            }
            set
            {
				if (value != null || AllowNull) 
					_invoiceStatus = value; 
            }
        }

        public virtual DateTime InvoiceDate
        {
            get
            {
				return _invoiceDate; 
            }
            set
            {
				_invoiceDate = value.Date.ToSqlSafeValue(); 
            }
        }

        public virtual TimeSpan InvoiceTime
        {
            get
            {
				return _invoiceTime; 
            }
            set
            {
				_invoiceTime = value.ToSqlSafeValue(); 
            }
        }

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
					_dueDate = (value is null) ? (DateTime?) null : value?.Date.ToSqlSafeValue(); 
            }
        }

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
					_billDate = (value is null) ? (DateTime?) null : value?.Date.ToSqlSafeValue(); 
            }
        }

        public virtual string CustomerUuid
        {
            get
            {
				if (!AllowNull && _customerUuid is null) 
					_customerUuid = String.Empty; 
				return _customerUuid?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_customerUuid = value.TruncateTo(50); 
            }
        }

        public virtual string CustomerNum
        {
            get
            {
				if (!AllowNull && _customerNum is null) 
					_customerNum = String.Empty; 
				return _customerNum?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_customerNum = value.TruncateTo(50); 
            }
        }

        public virtual string CustomerName
        {
            get
            {
				if (!AllowNull && _customerName is null) 
					_customerName = String.Empty; 
				return _customerName?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_customerName = value.TruncateTo(200); 
            }
        }

        public virtual string Currency
        {
            get
            {
				if (!AllowNull && _currency is null) 
					_currency = String.Empty; 
				return _currency?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_currency = value.TruncateTo(10); 
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
            }
        }

        public virtual decimal? TaxRate
        {
            get
            {
				if (!AllowNull && _taxRate is null) 
					_taxRate = default(decimal); 
				return _taxRate; 
            }
            set
            {
				if (value != null || AllowNull) 
					_taxRate = value; 
            }
        }

        public virtual decimal? TaxAmount
        {
            get
            {
				if (!AllowNull && _taxAmount is null) 
					_taxAmount = default(decimal); 
				return _taxAmount; 
            }
            set
            {
				if (value != null || AllowNull) 
					_taxAmount = value; 
            }
        }

        public virtual decimal? DiscountRate
        {
            get
            {
				if (!AllowNull && _discountRate is null) 
					_discountRate = default(decimal); 
				return _discountRate; 
            }
            set
            {
				if (value != null || AllowNull) 
					_discountRate = value; 
            }
        }

        public virtual decimal? DiscountAmount
        {
            get
            {
				if (!AllowNull && _discountAmount is null) 
					_discountAmount = default(decimal); 
				return _discountAmount; 
            }
            set
            {
				if (value != null || AllowNull) 
					_discountAmount = value; 
            }
        }

        public virtual decimal? ShippingAmount
        {
            get
            {
				if (!AllowNull && _shippingAmount is null) 
					_shippingAmount = default(decimal); 
				return _shippingAmount; 
            }
            set
            {
				if (value != null || AllowNull) 
					_shippingAmount = value; 
            }
        }

        public virtual decimal? ShippingTaxAmount
        {
            get
            {
				if (!AllowNull && _shippingTaxAmount is null) 
					_shippingTaxAmount = default(decimal); 
				return _shippingTaxAmount; 
            }
            set
            {
				if (value != null || AllowNull) 
					_shippingTaxAmount = value; 
            }
        }

        public virtual decimal? MiscAmount
        {
            get
            {
				if (!AllowNull && _miscAmount is null) 
					_miscAmount = default(decimal); 
				return _miscAmount; 
            }
            set
            {
				if (value != null || AllowNull) 
					_miscAmount = value; 
            }
        }

        public virtual decimal? MiscTaxAmount
        {
            get
            {
				if (!AllowNull && _miscTaxAmount is null) 
					_miscTaxAmount = default(decimal); 
				return _miscTaxAmount; 
            }
            set
            {
				if (value != null || AllowNull) 
					_miscTaxAmount = value; 
            }
        }

        public virtual decimal? ChargeAndAllowanceAmount
        {
            get
            {
				if (!AllowNull && _chargeAndAllowanceAmount is null) 
					_chargeAndAllowanceAmount = default(decimal); 
				return _chargeAndAllowanceAmount; 
            }
            set
            {
				if (value != null || AllowNull) 
					_chargeAndAllowanceAmount = value; 
            }
        }

        public virtual decimal? PaidAmount
        {
            get
            {
				if (!AllowNull && _paidAmount is null) 
					_paidAmount = default(decimal); 
				return _paidAmount; 
            }
            set
            {
				if (value != null || AllowNull) 
					_paidAmount = value; 
            }
        }

        public virtual decimal? CreditAmount
        {
            get
            {
				if (!AllowNull && _creditAmount is null) 
					_creditAmount = default(decimal); 
				return _creditAmount; 
            }
            set
            {
				if (value != null || AllowNull) 
					_creditAmount = value; 
            }
        }

        public virtual decimal? Balance
        {
            get
            {
				if (!AllowNull && _balance is null) 
					_balance = default(decimal); 
				return _balance; 
            }
            set
            {
				if (value != null || AllowNull) 
					_balance = value; 
            }
        }

        public virtual decimal UnitCost
        {
            get
            {
				return _unitCost; 
            }
            set
            {
				_unitCost = value; 
            }
        }

        public virtual decimal AvgCost
        {
            get
            {
				return _avgCost; 
            }
            set
            {
				_avgCost = value; 
            }
        }

        public virtual decimal LotCost
        {
            get
            {
				return _lotCost; 
            }
            set
            {
				_lotCost = value; 
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
					_updateDateUtc = (value is null) ? (DateTime?) null : value?.Date.ToSqlSafeValue(); 
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
            }
        }

        #endregion Properties - Generated 

        #region Methods - Parent

		[XmlIgnore, JsonIgnore, IgnoreCompare]
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
			_databaseNum = default(int); 
			_masterAccountNum = default(int); 
			_profileNum = default(int); 
			_invoiceUuid = String.Empty; 
			_invoiceNumber = String.Empty; 
			_invoiceType = AllowNull ? (int?)null : default(int); 
			_invoiceStatus = AllowNull ? (int?)null : default(int); 
			_invoiceDate = new DateTime().MinValueSql(); 
			_invoiceTime = new TimeSpan().MinValueSql(); 
			_dueDate = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_billDate = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_customerUuid = AllowNull ? (string)null : String.Empty; 
			_customerNum = AllowNull ? (string)null : String.Empty; 
			_customerName = AllowNull ? (string)null : String.Empty; 
			_currency = AllowNull ? (string)null : String.Empty; 
			_subTotalAmount = default(decimal); 
			_totalAmount = default(decimal); 
			_taxRate = AllowNull ? (decimal?)null : default(decimal); 
			_taxAmount = AllowNull ? (decimal?)null : default(decimal); 
			_discountRate = AllowNull ? (decimal?)null : default(decimal); 
			_discountAmount = AllowNull ? (decimal?)null : default(decimal); 
			_shippingAmount = AllowNull ? (decimal?)null : default(decimal); 
			_shippingTaxAmount = AllowNull ? (decimal?)null : default(decimal); 
			_miscAmount = AllowNull ? (decimal?)null : default(decimal); 
			_miscTaxAmount = AllowNull ? (decimal?)null : default(decimal); 
			_chargeAndAllowanceAmount = AllowNull ? (decimal?)null : default(decimal); 
			_paidAmount = AllowNull ? (decimal?)null : default(decimal); 
			_creditAmount = AllowNull ? (decimal?)null : default(decimal); 
			_balance = AllowNull ? (decimal?)null : default(decimal); 
			_unitCost = default(decimal); 
			_avgCost = default(decimal); 
			_lotCost = default(decimal); 
			_updateDateUtc = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_enterBy = String.Empty; 
			_updateBy = String.Empty; 
            ClearChildren();
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
            return;
        }

        #endregion Methods - Generated 
    }
}



