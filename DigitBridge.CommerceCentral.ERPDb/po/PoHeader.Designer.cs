

              
              
    

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
    /// Represents a PoHeader.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("PoHeader")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("PoUuid")]
    [DtoName("PoHeaderDto")]
    public partial class PoHeader : TableRepository<PoHeader, long>
    {

        public PoHeader() : base() {}
        public PoHeader(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("DatabaseNum",SqlDbType.Int,NotNull=true)]
        private int _databaseNum;

        [Column("MasterAccountNum",SqlDbType.Int,NotNull=true)]
        private int _masterAccountNum;

        [Column("ProfileNum",SqlDbType.Int,NotNull=true)]
        private int _profileNum;

        [Column("PoUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _poUuid;

        [Column("PoNum",SqlDbType.VarChar,NotNull=true)]
        private string _poNum;

        [Column("PoType",SqlDbType.Int,IsDefault=true)]
        private int? _poType;

        [Column("PoStatus",SqlDbType.Int,IsDefault=true)]
        private int? _poStatus;

        [Column("PoDate",SqlDbType.Date,NotNull=true)]
        private DateTime _poDate;

        [Column("PoTime",SqlDbType.Time,NotNull=true)]
        private TimeSpan _poTime;

        [Column("EtaShipDate",SqlDbType.Date)]
        private DateTime? _etaShipDate;

        [Column("EtaArrivalDate",SqlDbType.Date)]
        private DateTime? _etaArrivalDate;

        [Column("CancelDate",SqlDbType.Date)]
        private DateTime? _cancelDate;

        [Column("VendorUuid",SqlDbType.VarChar,IsDefault=true)]
        private string _vendorUuid;

        [Column("VendorCode",SqlDbType.VarChar)]
        private string _VendorCode;

        [Column("VendorName",SqlDbType.NVarChar)]
        private string _vendorName;

        [Column("Currency",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _currency;

        [Column("SubTotalAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _subTotalAmount;

        [Column("TotalAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _totalAmount;

        [Column("TaxRate",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _taxRate;

        [Column("TaxAmount",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _taxAmount;

        [Column("TaxableAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _taxableAmount;

        [Column("NonTaxableAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _nonTaxableAmount;

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

        [Column("PoSourceCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _poSourceCode;

        [Column("UpdateDateUtc",SqlDbType.DateTime)]
        private DateTime? _updateDateUtc;

        [Column("EnterBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _enterBy;

        [Column("UpdateBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _updateBy;

        #endregion Fields - Generated 

        #region Properties - Generated 
		[IgnoreCompare] 
		public override string UniqueId => PoUuid; 
		public override void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(PoUuid)) 
				PoUuid = Guid.NewGuid().ToString(); 
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
		/// Global Unique Guid for P/O. <br> Display: false, Editable: false.
		/// </summary>
        public virtual string PoUuid
        {
            get
            {
				return _poUuid?.TrimEnd(); 
            }
            set
            {
				_poUuid = value.TruncateTo(50); 
				OnPropertyChanged("PoUuid", value);
            }
        }

		/// <summary>
		/// Unique in this database. <br> ProfileNum + PoNum is DigitBridgePoNum, which is global unique. <br> Title: PoNum, Display: true, Editable: true
		/// </summary>
        public virtual string PoNum
        {
            get
            {
				return _poNum?.TrimEnd(); 
            }
            set
            {
				_poNum = value.TruncateTo(50); 
				OnPropertyChanged("PoNum", value);
            }
        }

		/// <summary>
		/// P/O type. <br> Title: Type, Display: true, Editable: true
		/// </summary>
        public virtual int? PoType
        {
            get
            {
				if (!AllowNull && _poType is null) 
					_poType = default(int); 
				return _poType; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_poType = value; 
					OnPropertyChanged("PoType", value);
				}
            }
        }

		/// <summary>
		/// P/O status. <br> Title: Status, Display: true, Editable: true
		/// </summary>
        public virtual int? PoStatus
        {
            get
            {
				if (!AllowNull && _poStatus is null) 
					_poStatus = default(int); 
				return _poStatus; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_poStatus = value; 
					OnPropertyChanged("PoStatus", value);
				}
            }
        }

		/// <summary>
		/// P/O date. <br> Title: Date, Display: true, Editable: true
		/// </summary>
        public virtual DateTime PoDate
        {
            get
            {
				return _poDate; 
            }
            set
            {
				_poDate = value.Date.ToSqlSafeValue(); 
				OnPropertyChanged("PoDate", value);
            }
        }

		/// <summary>
		/// P/O time. <br> Title: Time, Display: true, Editable: true
		/// </summary>
        public virtual TimeSpan PoTime
        {
            get
            {
				return _poTime; 
            }
            set
            {
				_poTime = value.ToSqlSafeValue(); 
				OnPropertyChanged("PoTime", value);
            }
        }

		/// <summary>
		/// Estimated vendor ship date. <br> Title: Ship Date, Display: true, Editable: true
		/// </summary>
        public virtual DateTime? EtaShipDate
        {
            get
            {
				if (!AllowNull && _etaShipDate is null) 
					_etaShipDate = new DateTime().MinValueSql(); 
				return _etaShipDate; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_etaShipDate = (value is null) ? (DateTime?) null : value?.Date.ToSqlSafeValue(); 
					OnPropertyChanged("EtaShipDate", value);
				}
            }
        }

		/// <summary>
		/// Estimated date when item arrival to buyer . <br> Title: Arrival Date, Display: true, Editable: true
		/// </summary>
        public virtual DateTime? EtaArrivalDate
        {
            get
            {
				if (!AllowNull && _etaArrivalDate is null) 
					_etaArrivalDate = new DateTime().MinValueSql(); 
				return _etaArrivalDate; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_etaArrivalDate = (value is null) ? (DateTime?) null : value?.Date.ToSqlSafeValue(); 
					OnPropertyChanged("EtaArrivalDate", value);
				}
            }
        }

		/// <summary>
		/// Usually it is related to shipping instruction. <br> Title: Cancel Date, Display: false, Editable: false
		/// </summary>
        public virtual DateTime? CancelDate
        {
            get
            {
				if (!AllowNull && _cancelDate is null) 
					_cancelDate = new DateTime().MinValueSql(); 
				return _cancelDate; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_cancelDate = (value is null) ? (DateTime?) null : value?.Date.ToSqlSafeValue(); 
					OnPropertyChanged("CancelDate", value);
				}
            }
        }

		/// <summary>
		/// reference Vendor Unique Guid. <br> Display: false, Editable: false
		/// </summary>
        public virtual string VendorUuid
        {
            get
            {
				if (!AllowNull && _vendorUuid is null) 
					_vendorUuid = String.Empty; 
				return _vendorUuid?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_vendorUuid = value.TruncateTo(50); 
					OnPropertyChanged("VendorUuid", value);
				}
            }
        }

		/// <summary>
		/// Vendor readable number.<br> DatabaseNum + VendorCode is DigitBridgeVendorCode, which is global unique. <br> Display: false, Editable: false
		/// </summary>
        public virtual string VendorCode
        {
            get
            {
				if (!AllowNull && _VendorCode is null) 
					_VendorCode = String.Empty; 
				return _VendorCode?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_VendorCode = value.TruncateTo(50); 
					OnPropertyChanged("VendorCode", value);
				}
            }
        }

		/// <summary>
		/// Vendor name. <br> Display: false, Editable: false
		/// </summary>
        public virtual string VendorName
        {
            get
            {
				if (!AllowNull && _vendorName is null) 
					_vendorName = String.Empty; 
				return _vendorName?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_vendorName = value.TruncateTo(100); 
					OnPropertyChanged("VendorName", value);
				}
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
		/// Sub total amount is sumary items amount. . <br> Title: Subtotal, Display: true, Editable: false
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
		/// Total order amount. Include every charge. Related to VAT. For US orders, tax should not be included. Refer to tax info to find more detail. Reference calculation <br>(Sum of all items OrderItems
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
		/// Default Tax rate for P/O items. . <br> Title: Tax, Display: true, Editable: true
		/// </summary>
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
				{
					_taxRate = value; 
					OnPropertyChanged("TaxRate", value);
				}
            }
        }

		/// <summary>
		/// Total P/O tax amount (include shipping tax and misc tax) . <br> Title: Tax Amount, Display: true, Editable: false
		/// </summary>
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
				{
					_taxAmount = value; 
					OnPropertyChanged("TaxAmount", value);
				}
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
		/// P/O level discount rate. <br> Title: Discount, Display: true, Editable: true
		/// </summary>
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
				{
					_discountRate = value; 
					OnPropertyChanged("DiscountRate", value);
				}
            }
        }

		/// <summary>
		/// P/O level discount amount, base on SubTotalAmount. <br> Title: Discount Amount, Display: true, Editable: true
		/// </summary>
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
				{
					_discountAmount = value; 
					OnPropertyChanged("DiscountAmount", value);
				}
            }
        }

		/// <summary>
		/// Total shipping fee for all items. <br> Title: Shipping, Display: true, Editable: true
		/// </summary>
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
				{
					_shippingAmount = value; 
					OnPropertyChanged("ShippingAmount", value);
				}
            }
        }

		/// <summary>
		/// tax amount of shipping fee. <br> Title: Shipping Tax, Display: true, Editable: false
		/// </summary>
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
				{
					_shippingTaxAmount = value; 
					OnPropertyChanged("ShippingTaxAmount", value);
				}
            }
        }

		/// <summary>
		/// P/O handling charge . <br> Title: Handling, Display: true, Editable: true
		/// </summary>
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
				{
					_miscAmount = value; 
					OnPropertyChanged("MiscAmount", value);
				}
            }
        }

		/// <summary>
		/// tax amount of handling charge. <br> Title: Handling Tax, Display: true, Editable: false
		/// </summary>
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
				{
					_miscTaxAmount = value; 
					OnPropertyChanged("MiscTaxAmount", value);
				}
            }
        }

		/// <summary>
		/// P/O total Charg Allowance Amount. <br> Title: Charge&Allowance, Display: true, Editable: true
		/// </summary>
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
				{
					_chargeAndAllowanceAmount = value; 
					OnPropertyChanged("ChargeAndAllowanceAmount", value);
				}
            }
        }

		/// <summary>
		/// P/O import or create from other entity number, use to prevent import duplicate P/O. <br> Title: Source Code, Display: false, Editable: false
		/// </summary>
        public virtual string PoSourceCode
        {
            get
            {
				return _poSourceCode?.TrimEnd(); 
            }
            set
            {
				_poSourceCode = value.TruncateTo(100); 
				OnPropertyChanged("PoSourceCode", value);
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
					_updateDateUtc = (value is null) ? (DateTime?) null : value?.ToSqlSafeValue(); 
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
		private PurchaseOrderData Parent { get; set; }
		public PurchaseOrderData GetParent() => Parent;
		public PoHeader SetParent(PurchaseOrderData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			PoUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override PoHeader Clear()
        {
            base.Clear();
			_databaseNum = default(int); 
			_masterAccountNum = default(int); 
			_profileNum = default(int); 
			_poUuid = String.Empty; 
			_poNum = String.Empty; 
			_poType = AllowNull ? (int?)null : default(int); 
			_poStatus = AllowNull ? (int?)null : default(int); 
			_poDate = new DateTime().MinValueSql(); 
			_poTime = new TimeSpan().MinValueSql(); 
			_etaShipDate = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_etaArrivalDate = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_cancelDate = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_vendorUuid = AllowNull ? (string)null : String.Empty; 
			_VendorCode = AllowNull ? (string)null : String.Empty; 
			_vendorName = AllowNull ? (string)null : String.Empty; 
			_currency = String.Empty; 
			_subTotalAmount = default(decimal); 
			_totalAmount = default(decimal); 
			_taxRate = AllowNull ? (decimal?)null : default(decimal); 
			_taxAmount = AllowNull ? (decimal?)null : default(decimal); 
			_taxableAmount = default(decimal); 
			_nonTaxableAmount = default(decimal); 
			_discountRate = AllowNull ? (decimal?)null : default(decimal); 
			_discountAmount = AllowNull ? (decimal?)null : default(decimal); 
			_shippingAmount = AllowNull ? (decimal?)null : default(decimal); 
			_shippingTaxAmount = AllowNull ? (decimal?)null : default(decimal); 
			_miscAmount = AllowNull ? (decimal?)null : default(decimal); 
			_miscTaxAmount = AllowNull ? (decimal?)null : default(decimal); 
			_chargeAndAllowanceAmount = AllowNull ? (decimal?)null : default(decimal); 
			_poSourceCode = String.Empty; 
			_updateDateUtc = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_enterBy = String.Empty; 
			_updateBy = String.Empty; 
            ClearChildren();
            return this;
        }

        public override PoHeader CheckIntegrity()
        {
            CheckUniqueId();
            CheckIntegrityOthers();
            return this;
        }

        public virtual PoHeader ClearChildren()
        {
            return this;
        }

        public virtual PoHeader NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(PoHeader data)
        {
            if (data is null) return;
            return;
        }



        #endregion Methods - Generated 
    }
}



