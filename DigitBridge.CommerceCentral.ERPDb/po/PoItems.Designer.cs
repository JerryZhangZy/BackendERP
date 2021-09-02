              
              
    

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
    /// Represents a PoItems.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("PoItems")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("PoItemUuid")]
    [DtoName("PoItemsDto")]
    public partial class PoItems : TableRepository<PoItems, long>
    {

        public PoItems() : base() {}
        public PoItems(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("PoItemUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _poItemUuid;

        [Column("PoUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _poUuid;

        [Column("Seq",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _seq;

        [Column("PoItemType",SqlDbType.Int,IsDefault=true)]
        private int? _poItemType;

        [Column("PoItemStatus",SqlDbType.Int,IsDefault=true)]
        private int? _poItemStatus;

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

        [Column("ProductUuid",SqlDbType.VarChar,NotNull=true)]
        private string _productUuid;

        [Column("InventoryUuid",SqlDbType.VarChar,NotNull=true)]
        private string _inventoryUuid;

        [Column("SKU",SqlDbType.VarChar,NotNull=true)]
        private string _sKU;

        [Column("Description",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _description;

        [Column("Notes",SqlDbType.NVarChar,NotNull=true)]
        private string _notes;

        [Column("Currency",SqlDbType.VarChar)]
        private string _currency;

        [Column("PoQty",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _poQty;

        [Column("ReceivedQty",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _receivedQty;

        [Column("CancelledQty",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _cancelledQty;

        [Column("PriceRule",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _priceRule;

        [Column("Price",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _price;

        [Column("ExtAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _extAmount;

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

        [Column("Stockable",SqlDbType.TinyInt,NotNull=true,IsDefault=true)]
        private byte _stockable;

        [Column("Costable",SqlDbType.TinyInt,NotNull=true,IsDefault=true)]
        private byte _costable;

        [Column("Taxable",SqlDbType.TinyInt,NotNull=true,IsDefault=true)]
        private byte _taxable;

        [Column("IsAp",SqlDbType.TinyInt,NotNull=true,IsDefault=true)]
        private byte _isAp;

        [Column("UpdateDateUtc",SqlDbType.DateTime)]
        private DateTime? _updateDateUtc;

        [Column("EnterBy",SqlDbType.VarChar,NotNull=true)]
        private string _enterBy;

        [Column("UpdateBy",SqlDbType.VarChar,NotNull=true)]
        private string _updateBy;

        #endregion Fields - Generated 

        #region Properties - Generated 
		[IgnoreCompare] 
		public override string UniqueId => PoItemUuid; 
		public override void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(PoItemUuid)) 
				PoItemUuid = Guid.NewGuid().ToString(); 
		}
		/// <summary>
		/// Global Unique Guid for P/O Item Line. <br> Display: false, Editable: false
		/// </summary>
        public virtual string PoItemUuid
        {
            get
            {
				return _poItemUuid?.TrimEnd(); 
            }
            set
            {
				_poItemUuid = value.TruncateTo(50); 
				OnPropertyChanged("PoItemUuid", value);
            }
        }

		/// <summary>
		/// Global Unique Guid for P/O. <br> Display: false, Editable: false
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
		/// P/O Item Line sort sequence. <br> Title: Line#, Display: true, Editable: false
		/// </summary>
        public virtual int Seq
        {
            get
            {
				return _seq; 
            }
            set
            {
				_seq = value; 
				OnPropertyChanged("Seq", value);
            }
        }

		/// <summary>
		/// P/O item type.<br> Title: Type, Display: true, Editable: true
		/// </summary>
        public virtual int? PoItemType
        {
            get
            {
				if (!AllowNull && _poItemType is null) 
					_poItemType = default(int); 
				return _poItemType; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_poItemType = value; 
					OnPropertyChanged("PoItemType", value);
				}
            }
        }

		/// <summary>
		/// P/O item status. <br> Title: Status, Display: true, Editable: true
		/// </summary>
        public virtual int? PoItemStatus
        {
            get
            {
				if (!AllowNull && _poItemStatus is null) 
					_poItemStatus = default(int); 
				return _poItemStatus; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_poItemStatus = value; 
					OnPropertyChanged("PoItemStatus", value);
				}
            }
        }

		/// <summary>
		/// (Ignore) P/O date
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
		/// (Ignore) P/O time
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
		/// Estimated vendor ship date . <br> Title: Ship Date, Display: true, Editable: true
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
		/// Estimated date when item arrival to buyer. <br> Title: Arrival Date, Display: true, Editable: true
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
		/// Usually it is related to shipping instruction. <br> Title: Cancel Date, Display: true, Editable: true
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
		/// (Readonly) Product uuid. load from ProductBasic data. <br> Display: false, Editable: false
		/// </summary>
        public virtual string ProductUuid
        {
            get
            {
				return _productUuid?.TrimEnd(); 
            }
            set
            {
				_productUuid = value.TruncateTo(50); 
				OnPropertyChanged("ProductUuid", value);
            }
        }

		/// <summary>
		/// (Readonly) Inventory Item Line uuid, load from inventory data. <br> Display: false, Editable: false
		/// </summary>
        public virtual string InventoryUuid
        {
            get
            {
				return _inventoryUuid?.TrimEnd(); 
            }
            set
            {
				_inventoryUuid = value.TruncateTo(50); 
				OnPropertyChanged("InventoryUuid", value);
            }
        }

		/// <summary>
		/// Product SKU. <br> Title: SKU, Display: true, Editable: true
		/// </summary>
        public virtual string SKU
        {
            get
            {
				return _sKU?.TrimEnd(); 
            }
            set
            {
				_sKU = value.TruncateTo(100); 
				OnPropertyChanged("SKU", value);
            }
        }

		/// <summary>
		/// Item line description, default from ProductBasic data. <br> Title: Description, Display: true, Editable: true
		/// </summary>
        public virtual string Description
        {
            get
            {
				return _description?.TrimEnd(); 
            }
            set
            {
				_description = value.TruncateTo(200); 
				OnPropertyChanged("Description", value);
            }
        }

		/// <summary>
		/// P/O item notes . <br> Title: Notes, Display: true, Editable: true
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
		/// (Ignore)
		/// </summary>
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
				{
					_currency = value.TruncateTo(10); 
					OnPropertyChanged("Currency", value);
				}
            }
        }

		/// <summary>
		/// (Ignore) Item P/O Qty.
		/// </summary>
        public virtual decimal PoQty
        {
            get
            {
				return _poQty; 
            }
            set
            {
				_poQty = value; 
				OnPropertyChanged("PoQty", value);
            }
        }

		/// <summary>
		/// (Ignore) Item Received Qty.
		/// </summary>
        public virtual decimal ReceivedQty
        {
            get
            {
				return _receivedQty; 
            }
            set
            {
				_receivedQty = value; 
				OnPropertyChanged("ReceivedQty", value);
            }
        }

		/// <summary>
		/// (Ignore) Item Cancelled Qty.
		/// </summary>
        public virtual decimal CancelledQty
        {
            get
            {
				return _cancelledQty; 
            }
            set
            {
				_cancelledQty = value; 
				OnPropertyChanged("CancelledQty", value);
            }
        }

		/// <summary>
		/// Item P/O price rule. <br> Title: Price Type, Display: true, Editable: true
		/// </summary>
        public virtual string PriceRule
        {
            get
            {
				return _priceRule?.TrimEnd(); 
            }
            set
            {
				_priceRule = value.TruncateTo(50); 
				OnPropertyChanged("PriceRule", value);
            }
        }

		/// <summary>
		/// Item P/O price.  <br> Title: Unit Price, Display: true, Editable: true
		/// </summary>
        public virtual decimal Price
        {
            get
            {
				return _price; 
            }
            set
            {
				_price = value; 
				OnPropertyChanged("Price", value);
            }
        }

		/// <summary>
		/// Item total amount.  <br> Title: Ext.Amount, Display: true, Editable: false
		/// </summary>
        public virtual decimal ExtAmount
        {
            get
            {
				return _extAmount; 
            }
            set
            {
				_extAmount = value; 
				OnPropertyChanged("ExtAmount", value);
            }
        }

		/// <summary>
		/// Default Tax rate for P/O items.  <br> Display: false, Editable: false
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
		/// Total P/O tax amount (include shipping tax and misc tax) . <br> Display: false, Editable: false
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
		/// P/O level discount rate. <br> Title: Discount Rate, Display: true, Editable: true
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
		/// P/O level discount amount, base on SubTotalAmount.<br> Title: Discount Amount, Display: true, Editable: true
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
		/// Total shipping fee for all items. <br> Display: false, Editable: false
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
		/// tax amount of shipping fee. <br> Display: false, Editable: false
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
		/// P/O handling charge . <br> Display: false, Editable: false
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
		/// tax amount of handling charge. <br> Display: false, Editable: false
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
		/// P/O total Charg Allowance Amount. <br> Display: false, Editable: false
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
		/// P/O item will update inventory instock qty . <br> Title: Stockable, Display: true, Editable: true
		/// </summary>
        public virtual bool Stockable
        {
            get
            {
				return (_stockable == 1); 
            }
            set
            {
				_stockable = value ? (byte)1 : (byte)0; 
				OnPropertyChanged("Stockable", value);
            }
        }

		/// <summary>
		/// P/O item will update inventory cost. <br> Title: Apply Cost, Display: true, Editable: true
		/// </summary>
        public virtual bool Costable
        {
            get
            {
				return (_costable == 1); 
            }
            set
            {
				_costable = value ? (byte)1 : (byte)0; 
				OnPropertyChanged("Costable", value);
            }
        }

		/// <summary>
		/// P/O item will apply tax. <br> Title: Taxable, Display: true, Editable: true
		/// </summary>
        public virtual bool Taxable
        {
            get
            {
				return (_taxable == 1); 
            }
            set
            {
				_taxable = value ? (byte)1 : (byte)0; 
				OnPropertyChanged("Taxable", value);
            }
        }

		/// <summary>
		/// P/O item will apply to total amount . <br> Title: A/P, Display: true, Editable: true
		/// </summary>
        public virtual bool IsAp
        {
            get
            {
				return (_isAp == 1); 
            }
            set
            {
				_isAp = value ? (byte)1 : (byte)0; 
				OnPropertyChanged("IsAp", value);
            }
        }

		/// <summary>
		/// (Ignore)
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
					_updateDateUtc = (value is null) ? (DateTime?) null : value?.Date.ToSqlSafeValue(); 
					OnPropertyChanged("UpdateDateUtc", value);
				}
            }
        }

		/// <summary>
		/// (Ignore)
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
		/// (Ignore)
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
		public PoItems SetParent(PurchaseOrderData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent

		#region Methods - Children PoItemsAttributes
		protected PoItemsAttributes _PoItemsAttributes;
		[IgnoreCompare]
		public PoItemsAttributes PoItemsAttributes
		{
			get
			{
				return _PoItemsAttributes;
			}
			set
			{
				_PoItemsAttributes = value;
				CheckIntegrityPoItemsAttributes();
			}
		}
		public PoItemsAttributes SetChildrenPoItemsAttributes(IList<PoItemsAttributes> children)
		{
			var childrenList = children.ToList();
			PoItemsAttributes = childrenList.FirstOrDefault(x => !string.IsNullOrEmpty(PoItemUuid) && x.PoItemUuid == PoItemUuid);
			return PoItemsAttributes;
		}
		public IList<PoItemsAttributes> GetChildrenPoItemsAttributes()
		{
			return new List<PoItemsAttributes>() { PoItemsAttributes };
		}
		public IList<PoItemsAttributes> GetChildrenDeletedPoItemsAttributes()
		{
			return null;
		}
		public PoItemsAttributes CheckIntegrityPoItemsAttributes()
		{
			if (PoItemsAttributes == null)
				return PoItemsAttributes;
			CheckUniqueId();
			PoItemsAttributes.SetParent(Parent);
			if (PoItemsAttributes.PoItemUuid != PoItemUuid) PoItemsAttributes.PoItemUuid = PoItemUuid;
			PoItemsAttributes.CheckIntegrity();
			return PoItemsAttributes;
		}
		public PoItemsAttributes LoadPoItemsAttributes()
		{
			PoItemsAttributes = dbFactory.GetById<PoItemsAttributes>(PoItemUuid);
			return PoItemsAttributes;
		}
		public async Task<PoItemsAttributes> LoadPoItemsAttributesAsync()
		{
			PoItemsAttributes = await dbFactory.GetByIdAsync<PoItemsAttributes>(PoItemUuid);
			return PoItemsAttributes;
		}
		public PoItemsAttributes NewPoItemsAttributes()
		{
			CheckUniqueId();
			var child = new PoItemsAttributes(dbFactory);
			child.SetParent(Parent);
			child.PoItemUuid = PoItemUuid;
			child.CheckIntegrity();
			return child;
		}
		public PoItemsAttributes AddPoItemsAttributes(PoItemsAttributes child)
		{
			if (child == null)
				child = NewPoItemsAttributes();
			PoItemsAttributes = child;
			return PoItemsAttributes;
			child.CheckIntegrity();
		}
		#endregion Methods - Children PoItemsAttributes
		#region Methods - Children PoItemsRef
		protected PoItemsRef _PoItemsRef;
		[IgnoreCompare]
		public PoItemsRef PoItemsRef
		{
			get
			{
				return _PoItemsRef;
			}
			set
			{
				_PoItemsRef = value;
				CheckIntegrityPoItemsRef();
			}
		}
		public PoItemsRef SetChildrenPoItemsRef(IList<PoItemsRef> children)
		{
			var childrenList = children.ToList();
			PoItemsRef = childrenList.FirstOrDefault(x => !string.IsNullOrEmpty(PoItemUuid) && x.PoItemUuid == PoItemUuid);
			return PoItemsRef;
		}
		public IList<PoItemsRef> GetChildrenPoItemsRef()
		{
			return new List<PoItemsRef>() { PoItemsRef };
		}
		public IList<PoItemsRef> GetChildrenDeletedPoItemsRef()
		{
			return null;
		}
		public PoItemsRef CheckIntegrityPoItemsRef()
		{
			if (PoItemsRef == null)
				return PoItemsRef;
			CheckUniqueId();
			PoItemsRef.SetParent(Parent);
			if (PoItemsRef.PoItemUuid != PoItemUuid) PoItemsRef.PoItemUuid = PoItemUuid;
			PoItemsRef.CheckIntegrity();
			return PoItemsRef;
		}
		public PoItemsRef LoadPoItemsRef()
		{
			PoItemsRef = dbFactory.GetById<PoItemsRef>(PoItemUuid);
			return PoItemsRef;
		}
		public async Task<PoItemsRef> LoadPoItemsRefAsync()
		{
			PoItemsRef = await dbFactory.GetByIdAsync<PoItemsRef>(PoItemUuid);
			return PoItemsRef;
		}
		public PoItemsRef NewPoItemsRef()
		{
			CheckUniqueId();
			var child = new PoItemsRef(dbFactory);
			child.SetParent(Parent);
			child.PoItemUuid = PoItemUuid;
			child.CheckIntegrity();
			return child;
		}
		public PoItemsRef AddPoItemsRef(PoItemsRef child)
		{
			if (child == null)
				child = NewPoItemsRef();
			PoItemsRef = child;
			return PoItemsRef;
			child.CheckIntegrity();
		}
		#endregion Methods - Children PoItemsRef

        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			PoItemUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override PoItems Clear()
        {
            base.Clear();
			_poItemUuid = String.Empty; 
			_poUuid = String.Empty; 
			_seq = default(int); 
			_poItemType = AllowNull ? (int?)null : default(int); 
			_poItemStatus = AllowNull ? (int?)null : default(int); 
			_poDate = new DateTime().MinValueSql(); 
			_poTime = new TimeSpan().MinValueSql(); 
			_etaShipDate = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_etaArrivalDate = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_cancelDate = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_productUuid = String.Empty; 
			_inventoryUuid = String.Empty; 
			_sKU = String.Empty; 
			_description = String.Empty; 
			_notes = String.Empty; 
			_currency = AllowNull ? (string)null : String.Empty; 
			_poQty = default(decimal); 
			_receivedQty = default(decimal); 
			_cancelledQty = default(decimal); 
			_priceRule = String.Empty; 
			_price = default(decimal); 
			_extAmount = default(decimal); 
			_taxRate = AllowNull ? (decimal?)null : default(decimal); 
			_taxAmount = AllowNull ? (decimal?)null : default(decimal); 
			_discountRate = AllowNull ? (decimal?)null : default(decimal); 
			_discountAmount = AllowNull ? (decimal?)null : default(decimal); 
			_shippingAmount = AllowNull ? (decimal?)null : default(decimal); 
			_shippingTaxAmount = AllowNull ? (decimal?)null : default(decimal); 
			_miscAmount = AllowNull ? (decimal?)null : default(decimal); 
			_miscTaxAmount = AllowNull ? (decimal?)null : default(decimal); 
			_chargeAndAllowanceAmount = AllowNull ? (decimal?)null : default(decimal); 
			_stockable = default(byte); 
			_costable = default(byte); 
			_taxable = default(byte); 
			_isAp = default(byte); 
			_updateDateUtc = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_enterBy = String.Empty; 
			_updateBy = String.Empty; 
            ClearChildren();
            return this;
        }

        public override PoItems CheckIntegrity()
        {
            CheckUniqueId();
			CheckIntegrityPoItemsAttributes();
			CheckIntegrityPoItemsRef();
            CheckIntegrityOthers();
            return this;
        }

        public virtual PoItems ClearChildren()
        {
			PoItemsAttributes?.Clear();
			PoItemsRef?.Clear();
            return this;
        }

        public virtual PoItems NewChildren()
        {
			AddPoItemsAttributes(NewPoItemsAttributes());
			AddPoItemsRef(NewPoItemsRef());
            return this;
        }

        public virtual void CopyChildrenFrom(PoItems data)
        {
            if (data is null) return;
			PoItemsAttributes?.CopyFrom(data.PoItemsAttributes);
			CheckIntegrityPoItemsAttributes(); 
			PoItemsRef?.CopyFrom(data.PoItemsRef);
			CheckIntegrityPoItemsRef(); 
            return;
        }



        #endregion Methods - Generated 
    }
}



