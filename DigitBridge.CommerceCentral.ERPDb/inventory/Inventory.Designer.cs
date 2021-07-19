





              

              
    

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
    /// Represents a Inventory.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("Inventory")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("InventoryUuid")]
    [DtoName("InventoryDto")]
    public partial class Inventory : TableRepository<Inventory, long>
    {

        public Inventory() : base() {}
        public Inventory(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("DatabaseNum",SqlDbType.Int,NotNull=true)]
        private int _databaseNum;

        [Column("MasterAccountNum",SqlDbType.Int,NotNull=true)]
        private int _masterAccountNum;

        [Column("ProfileNum",SqlDbType.Int,NotNull=true)]
        private int _profileNum;

        [Column("ProductUuid",SqlDbType.VarChar,NotNull=true)]
        private string _productUuid;

        [Column("InventoryUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _inventoryUuid;

        [Column("StyleCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _styleCode;

        [Column("ColorPatternCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _colorPatternCode;

        [Column("SizeType",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _sizeType;

        [Column("SizeCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _sizeCode;

        [Column("WidthCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _widthCode;

        [Column("LengthCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _lengthCode;

        [Column("PriceRule",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _priceRule;

        [Column("LeadTimeDay",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _leadTimeDay;

        [Column("PoSize",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _poSize;

        [Column("MinStock",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _minStock;

        [Column("SKU",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _sKU;

        [Column("WarehouseUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _warehouseUuid;

        [Column("WarehouseCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _warehouseCode;

        [Column("WarehouseName",SqlDbType.NVarChar,NotNull=true)]
        private string _warehouseName;

        [Column("LotNum",SqlDbType.VarChar,NotNull=true)]
        private string _lotNum;

        [Column("LotInDate",SqlDbType.Date)]
        private DateTime? _lotInDate;

        [Column("LotExpDate",SqlDbType.Date)]
        private DateTime? _lotExpDate;

        [Column("LotDescription",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _lotDescription;

        [Column("LpnNum",SqlDbType.VarChar,NotNull=true)]
        private string _lpnNum;

        [Column("LpnDescription",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _lpnDescription;

        [Column("Notes",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _notes;

        [Column("Currency",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _currency;

        [Column("UOM",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _uOM;

        [Column("QtyPerPallot",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _qtyPerPallot;

        [Column("QtyPerCase",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _qtyPerCase;

        [Column("QtyPerBox",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _qtyPerBox;

        [Column("PackType",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _packType;

        [Column("PackQty",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _packQty;

        [Column("DefaultPackType",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _defaultPackType;

        [Column("Instock",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _instock;

        [Column("OnHand",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _onHand;

        [Column("OpenSoQty",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _openSoQty;

        [Column("OpenFulfillmentQty",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _openFulfillmentQty;

        [Column("AvaQty",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _avaQty;

        [Column("OpenPoQty",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _openPoQty;

        [Column("OpenInTransitQty",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _openInTransitQty;

        [Column("OpenWipQty",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _openWipQty;

        [Column("ProjectedQty",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _projectedQty;

        [Column("BaseCost",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _baseCost;

        [Column("TaxRate",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _taxRate;

        [Column("TaxAmount",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _taxAmount;

        [Column("ShippingAmount",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _shippingAmount;

        [Column("MiscAmount",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _miscAmount;

        [Column("ChargeAndAllowanceAmount",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _chargeAndAllowanceAmount;

        [Column("UnitCost",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _unitCost;

        [Column("AvgCost",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _avgCost;

        [Column("SalesCost",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _salesCost;

        [Column("UpdateDateUtc",SqlDbType.DateTime)]
        private DateTime? _updateDateUtc;

        [Column("EnterBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _enterBy;

        [Column("UpdateBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _updateBy;

        #endregion Fields - Generated 

        #region Properties - Generated 
		[IgnoreCompare] 
		public override string UniqueId => InventoryUuid; 
		public void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(InventoryUuid)) 
				InventoryUuid = Guid.NewGuid().ToString(); 
		}
		[IgnoreCompare] 
		public override bool IsEmpty => ( string.IsNullOrWhiteSpace(SKU) );
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
		/// (Readonly) Inventory uuid. <br> Display: false, Editable: false
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
		/// Product style code use to group multiple SKU. load from ProductExt data. <br> Title: Style Code, Display: true, Editable: true
		/// </summary>
        public virtual string StyleCode
        {
            get
            {
				return _styleCode?.TrimEnd(); 
            }
            set
            {
				_styleCode = value.TruncateTo(100); 
				OnPropertyChanged("StyleCode", value);
            }
        }

		/// <summary>
		/// Product color and pattern code. <br> Title: Color, Display: true, Editable: true
		/// </summary>
        public virtual string ColorPatternCode
        {
            get
            {
				return _colorPatternCode?.TrimEnd(); 
            }
            set
            {
				_colorPatternCode = value.TruncateTo(50); 
				OnPropertyChanged("ColorPatternCode", value);
            }
        }

		/// <summary>
		/// Product size type. <br> Title: Size Type, Display: true, Editable: true
		/// </summary>
        public virtual string SizeType
        {
            get
            {
				return _sizeType?.TrimEnd(); 
            }
            set
            {
				_sizeType = value.TruncateTo(50); 
				OnPropertyChanged("SizeType", value);
            }
        }

		/// <summary>
		/// Product size code. <br> Title: Size, Display: true, Editable: true
		/// </summary>
        public virtual string SizeCode
        {
            get
            {
				return _sizeCode?.TrimEnd(); 
            }
            set
            {
				_sizeCode = value.TruncateTo(50); 
				OnPropertyChanged("SizeCode", value);
            }
        }

		/// <summary>
		/// Product width code. <br> Title: Width, Display: true, Editable: true
		/// </summary>
        public virtual string WidthCode
        {
            get
            {
				return _widthCode?.TrimEnd(); 
            }
            set
            {
				_widthCode = value.TruncateTo(30); 
				OnPropertyChanged("WidthCode", value);
            }
        }

		/// <summary>
		/// Product length code. <br> Title: Length, Display: true, Editable: true
		/// </summary>
        public virtual string LengthCode
        {
            get
            {
				return _lengthCode?.TrimEnd(); 
            }
            set
            {
				_lengthCode = value.TruncateTo(30); 
				OnPropertyChanged("LengthCode", value);
            }
        }

		/// <summary>
		/// Product Default Price Rule. <br> Title: Price Rule, Display: true, Editable: true
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
		/// Processing days before shipping. <br> Title: Leading Days, Display: true, Editable: true
		/// </summary>
        public virtual int LeadTimeDay
        {
            get
            {
				return _leadTimeDay; 
            }
            set
            {
				_leadTimeDay = value; 
				OnPropertyChanged("LeadTimeDay", value);
            }
        }

		/// <summary>
		/// Default P/O qty. <br> Title: Deafult P/O Qty, Display: true, Editable: true
		/// </summary>
        public virtual decimal PoSize
        {
            get
            {
				return _poSize; 
            }
            set
            {
				_poSize = value; 
				OnPropertyChanged("PoSize", value);
            }
        }

		/// <summary>
		/// Garantee minimal Instock in anytime. <br> Title: Min.Stock, Display: true, Editable: true
		/// </summary>
        public virtual decimal MinStock
        {
            get
            {
				return _minStock; 
            }
            set
            {
				_minStock = value; 
				OnPropertyChanged("MinStock", value);
            }
        }

		/// <summary>
		/// Product SKU. load from ProductBasic data. <br> Display: false, Editable: false
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
		/// (Readonly) Warehouse uuid, load from warehouse data. <br> Display: false, Editable: false
		/// </summary>
        public virtual string WarehouseUuid
        {
            get
            {
				return _warehouseUuid?.TrimEnd(); 
            }
            set
            {
				_warehouseUuid = value.TruncateTo(50); 
				OnPropertyChanged("WarehouseUuid", value);
            }
        }

		/// <summary>
		/// Readable warehouse code, load from warehouse data. <br> Title: Warehouse Code, Display: true, Editable: true
		/// </summary>
        public virtual string WarehouseCode
        {
            get
            {
				return _warehouseCode?.TrimEnd(); 
            }
            set
            {
				_warehouseCode = value.TruncateTo(50); 
				OnPropertyChanged("WarehouseCode", value);
            }
        }

		/// <summary>
		/// (Readonly) Warehouse name, load from warehouse data. <br> Title: Warehouse Name, Display: true, Editable: false
		/// </summary>
        public virtual string WarehouseName
        {
            get
            {
				return _warehouseName?.TrimEnd(); 
            }
            set
            {
				_warehouseName = value.TruncateTo(200); 
				OnPropertyChanged("WarehouseName", value);
            }
        }

		/// <summary>
		/// Lot Number. <br> Title: Lot Number, Display: true, Editable: true
		/// </summary>
        public virtual string LotNum
        {
            get
            {
				return _lotNum?.TrimEnd(); 
            }
            set
            {
				_lotNum = value.TruncateTo(100); 
				OnPropertyChanged("LotNum", value);
            }
        }

		/// <summary>
		/// Lot receive Date. <br> Title: Receive Date, Display: true, Editable: true
		/// </summary>
        public virtual DateTime? LotInDate
        {
            get
            {
				if (!AllowNull && _lotInDate is null) 
					_lotInDate = new DateTime().MinValueSql(); 
				return _lotInDate; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_lotInDate = (value is null) ? (DateTime?) null : value?.Date.ToSqlSafeValue(); 
					OnPropertyChanged("LotInDate", value);
				}
            }
        }

		/// <summary>
		/// Lot Expiration date. <br> Title: Expiration Date, Display: true, Editable: true
		/// </summary>
        public virtual DateTime? LotExpDate
        {
            get
            {
				if (!AllowNull && _lotExpDate is null) 
					_lotExpDate = new DateTime().MinValueSql(); 
				return _lotExpDate; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_lotExpDate = (value is null) ? (DateTime?) null : value?.Date.ToSqlSafeValue(); 
					OnPropertyChanged("LotExpDate", value);
				}
            }
        }

		/// <summary>
		/// Lot description. <br> Title: Lot Description, Display: true, Editable: true
		/// </summary>
        public virtual string LotDescription
        {
            get
            {
				return _lotDescription?.TrimEnd(); 
            }
            set
            {
				_lotDescription = value.TruncateTo(200); 
				OnPropertyChanged("LotDescription", value);
            }
        }

		/// <summary>
		/// LPN Number. <br> Title: LPN, Display: true, Editable: true
		/// </summary>
        public virtual string LpnNum
        {
            get
            {
				return _lpnNum?.TrimEnd(); 
            }
            set
            {
				_lpnNum = value.TruncateTo(100); 
				OnPropertyChanged("LpnNum", value);
            }
        }

		/// <summary>
		/// LPN description. <br> Title: LPN Description, Display: true, Editable: true
		/// </summary>
        public virtual string LpnDescription
        {
            get
            {
				return _lpnDescription?.TrimEnd(); 
            }
            set
            {
				_lpnDescription = value.TruncateTo(200); 
				OnPropertyChanged("LpnDescription", value);
            }
        }

		/// <summary>
		/// Inventory notes. <br> Title: Notes, Display: true, Editable: true
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
		/// (Ignore) Inventory price in currency. <br> Title: Currency, Display: false, Editable: false
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
		/// (Readonly) Product unit of measure, load from ProductBasic data. <br> Title: UOM, Display: true, Editable: false
		/// </summary>
        public virtual string UOM
        {
            get
            {
				return _uOM?.TrimEnd(); 
            }
            set
            {
				_uOM = value.TruncateTo(50); 
				OnPropertyChanged("UOM", value);
            }
        }

		/// <summary>
		/// Item Qty per Pallot. <br> Title: Qty/Pallot, Display: true, Editable: true
		/// </summary>
        public virtual decimal QtyPerPallot
        {
            get
            {
				return _qtyPerPallot; 
            }
            set
            {
				_qtyPerPallot = value; 
				OnPropertyChanged("QtyPerPallot", value);
            }
        }

		/// <summary>
		/// Item Qty per case. <br> Title: Qty/Case, Display: true, Editable: true
		/// </summary>
        public virtual decimal QtyPerCase
        {
            get
            {
				return _qtyPerCase; 
            }
            set
            {
				_qtyPerCase = value; 
				OnPropertyChanged("QtyPerCase", value);
            }
        }

		/// <summary>
		/// Item Qty per box. <br> Title: Qty/Box, Display: true, Editable: true
		/// </summary>
        public virtual decimal QtyPerBox
        {
            get
            {
				return _qtyPerBox; 
            }
            set
            {
				_qtyPerBox = value; 
				OnPropertyChanged("QtyPerBox", value);
            }
        }

		/// <summary>
		/// Product specified pack type name. <br> Title: Pack Type, Display: true, Editable: true
		/// </summary>
        public virtual string PackType
        {
            get
            {
				return _packType?.TrimEnd(); 
            }
            set
            {
				_packType = value.TruncateTo(50); 
				OnPropertyChanged("PackType", value);
            }
        }

		/// <summary>
		/// Qty per each pack. <br> Title: Qty/Pack, Display: true, Editable: true
		/// </summary>
        public virtual decimal PackQty
        {
            get
            {
				return _packQty; 
            }
            set
            {
				_packQty = value; 
				OnPropertyChanged("PackQty", value);
            }
        }

		/// <summary>
		/// Default pack type in S/O or invoice. <br> Title: Default Pack, Display: true, Editable: true
		/// </summary>
        public virtual string DefaultPackType
        {
            get
            {
				return _defaultPackType?.TrimEnd(); 
            }
            set
            {
				_defaultPackType = value.TruncateTo(50); 
				OnPropertyChanged("DefaultPackType", value);
            }
        }

		/// <summary>
		/// Item in stock Qty. <br> Title: Instock, Display: true, Editable: false
		/// </summary>
        public virtual decimal Instock
        {
            get
            {
				return _instock; 
            }
            set
            {
				_instock = value; 
				OnPropertyChanged("Instock", value);
            }
        }

		/// <summary>
		/// (Ignore) Item On hand. <br> Title: Onhand, Display: false, Editable: false
		/// </summary>
        public virtual decimal OnHand
        {
            get
            {
				return _onHand; 
            }
            set
            {
				_onHand = value; 
				OnPropertyChanged("OnHand", value);
            }
        }

		/// <summary>
		/// Open S/O qty. <br> Title: Open S/O, Display: true, Editable: false
		/// </summary>
        public virtual decimal OpenSoQty
        {
            get
            {
				return _openSoQty; 
            }
            set
            {
				_openSoQty = value; 
				OnPropertyChanged("OpenSoQty", value);
            }
        }

		/// <summary>
		/// Open Fulfillment qty. <br> Title: Open Fulfillment, Display: true, Editable: false
		/// </summary>
        public virtual decimal OpenFulfillmentQty
        {
            get
            {
				return _openFulfillmentQty; 
            }
            set
            {
				_openFulfillmentQty = value; 
				OnPropertyChanged("OpenFulfillmentQty", value);
            }
        }

		/// <summary>
		/// Availiable sales qty. <br> Title: Available, Display: true, Editable: false
		/// </summary>
        public virtual decimal AvaQty
        {
            get
            {
				return _avaQty; 
            }
            set
            {
				_avaQty = value; 
				OnPropertyChanged("AvaQty", value);
            }
        }

		/// <summary>
		/// Open P/O qty. <br> Title: Open P/O, Display: true, Editable: false
		/// </summary>
        public virtual decimal OpenPoQty
        {
            get
            {
				return _openPoQty; 
            }
            set
            {
				_openPoQty = value; 
				OnPropertyChanged("OpenPoQty", value);
            }
        }

		/// <summary>
		/// Open InTransit qty. <br> Title: In transit, Display: true, Editable: false
		/// </summary>
        public virtual decimal OpenInTransitQty
        {
            get
            {
				return _openInTransitQty; 
            }
            set
            {
				_openInTransitQty = value; 
				OnPropertyChanged("OpenInTransitQty", value);
            }
        }

		/// <summary>
		/// Open Work in process qty. <br> Title: WIP, Display: true, Editable: false
		/// </summary>
        public virtual decimal OpenWipQty
        {
            get
            {
				return _openWipQty; 
            }
            set
            {
				_openWipQty = value; 
				OnPropertyChanged("OpenWipQty", value);
            }
        }

		/// <summary>
		/// Forcasting projected qty. <br> Title: Projected, Display: true, Editable: false
		/// </summary>
        public virtual decimal ProjectedQty
        {
            get
            {
				return _projectedQty; 
            }
            set
            {
				_projectedQty = value; 
				OnPropertyChanged("ProjectedQty", value);
            }
        }

		/// <summary>
		/// P/O receive price. <br> Title: Base Cost, Display: true, Editable: true
		/// </summary>
        public virtual decimal BaseCost
        {
            get
            {
				return _baseCost; 
            }
            set
            {
				_baseCost = value; 
				OnPropertyChanged("BaseCost", value);
            }
        }

		/// <summary>
		/// Duty Tax rate. <br> Title: Duty Rate, Display: true, Editable: true
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
		/// Duty tax amount. <br> Title: Duty Amount, Display: true, Editable: true
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
		/// Shipping fee. <br> Title: Shipping Fee, Display: true, Editable: true
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
		/// Handling charge. <br> Title: Handling Fee, Display: true, Editable: true
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
		/// Other Charg or Allowance Amount. <br> Title: Charg&Allowance, Display: true, Editable: true
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
		/// Unit cost include duty,and charge. <br> Title: Unit Cost, Display: true, Editable: false
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
		/// Moving average cost. <br> Title: Avg.Cost, Display: true, Editable: false
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
		/// Item cost display for sales. <br> Title: Sales Cost, Display: true, Editable: false
		/// </summary>
        public virtual decimal SalesCost
        {
            get
            {
				return _salesCost; 
            }
            set
            {
				_salesCost = value; 
				OnPropertyChanged("SalesCost", value);
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
					_updateDateUtc = (value is null) ? (DateTime?) null : value?.Date.ToSqlSafeValue(); 
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
		private InventoryData Parent { get; set; }
		public InventoryData GetParent() => Parent;
		public Inventory SetParent(InventoryData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent

		#region Methods - Children InventoryAttributes
		protected InventoryAttributes _InventoryAttributes;
		[IgnoreCompare]
		public InventoryAttributes InventoryAttributes
		{
			get
			{
				return _InventoryAttributes;
			}
			set
			{
				_InventoryAttributes = value;
				CheckIntegrityInventoryAttributes();
			}
		}
		public InventoryAttributes SetChildrenInventoryAttributes(IList<InventoryAttributes> children)
		{
			var childrenList = children.ToList();
			InventoryAttributes = childrenList.FirstOrDefault(x => !string.IsNullOrEmpty(InventoryUuid) && x.InventoryUuid == InventoryUuid);
			return InventoryAttributes;
		}
		public IList<InventoryAttributes> GetChildrenInventoryAttributes()
		{
			return new List<InventoryAttributes>() { InventoryAttributes };
		}
		public IList<InventoryAttributes> GetChildrenDeletedInventoryAttributes()
		{
			return null;
		}
		public InventoryAttributes CheckIntegrityInventoryAttributes()
		{
			if (InventoryAttributes == null)
				return InventoryAttributes;
			CheckUniqueId();
			InventoryAttributes.SetParent(Parent);
			if (InventoryAttributes.InventoryUuid != InventoryUuid) InventoryAttributes.InventoryUuid = InventoryUuid;
			return InventoryAttributes;
		}
		public InventoryAttributes LoadInventoryAttributes()
		{
			InventoryAttributes = dbFactory.GetById<InventoryAttributes>(InventoryUuid);
			return InventoryAttributes;
		}
		public async Task<InventoryAttributes> LoadInventoryAttributesAsync()
		{
			InventoryAttributes = await dbFactory.GetByIdAsync<InventoryAttributes>(InventoryUuid);
			return InventoryAttributes;
		}
		public InventoryAttributes NewInventoryAttributes()
		{
			CheckUniqueId();
			var child = new InventoryAttributes(dbFactory);
			child.SetParent(Parent);
			child.InventoryUuid = InventoryUuid;
			return child;
		}
		public InventoryAttributes AddInventoryAttributes(InventoryAttributes child)
		{
			if (child == null)
				child = NewInventoryAttributes();
			InventoryAttributes = child;
			return InventoryAttributes;
		}
		#endregion Methods - Children InventoryAttributes

        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			InventoryUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override Inventory Clear()
        {
            base.Clear();
			_databaseNum = default(int); 
			_masterAccountNum = default(int); 
			_profileNum = default(int); 
			_productUuid = String.Empty; 
			_inventoryUuid = String.Empty; 
			_styleCode = String.Empty; 
			_colorPatternCode = String.Empty; 
			_sizeType = String.Empty; 
			_sizeCode = String.Empty; 
			_widthCode = String.Empty; 
			_lengthCode = String.Empty; 
			_priceRule = String.Empty; 
			_leadTimeDay = default(int); 
			_poSize = default(decimal); 
			_minStock = default(decimal); 
			_sKU = String.Empty; 
			_warehouseUuid = String.Empty; 
			_warehouseCode = String.Empty; 
			_warehouseName = String.Empty; 
			_lotNum = String.Empty; 
			_lotInDate = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_lotExpDate = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_lotDescription = String.Empty; 
			_lpnNum = String.Empty; 
			_lpnDescription = String.Empty; 
			_notes = String.Empty; 
			_currency = String.Empty; 
			_uOM = String.Empty; 
			_qtyPerPallot = default(decimal); 
			_qtyPerCase = default(decimal); 
			_qtyPerBox = default(decimal); 
			_packType = String.Empty; 
			_packQty = default(decimal); 
			_defaultPackType = String.Empty; 
			_instock = default(decimal); 
			_onHand = default(decimal); 
			_openSoQty = default(decimal); 
			_openFulfillmentQty = default(decimal); 
			_avaQty = default(decimal); 
			_openPoQty = default(decimal); 
			_openInTransitQty = default(decimal); 
			_openWipQty = default(decimal); 
			_projectedQty = default(decimal); 
			_baseCost = default(decimal); 
			_taxRate = AllowNull ? (decimal?)null : default(decimal); 
			_taxAmount = AllowNull ? (decimal?)null : default(decimal); 
			_shippingAmount = AllowNull ? (decimal?)null : default(decimal); 
			_miscAmount = AllowNull ? (decimal?)null : default(decimal); 
			_chargeAndAllowanceAmount = AllowNull ? (decimal?)null : default(decimal); 
			_unitCost = default(decimal); 
			_avgCost = default(decimal); 
			_salesCost = default(decimal); 
			_updateDateUtc = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_enterBy = String.Empty; 
			_updateBy = String.Empty; 
            ClearChildren();
            return this;
        }

        public virtual Inventory ClearChildren()
        {
			InventoryAttributes.Clear();
            return this;
        }

        public virtual Inventory NewChildren()
        {
			AddInventoryAttributes(NewInventoryAttributes());
            return this;
        }

        public virtual void CopyChildrenFrom(Inventory data)
        {
            if (data is null) return;
			InventoryAttributes?.CopyFrom(data.InventoryAttributes);
			CheckIntegrityInventoryAttributes(); 
            return;
        }

		public static IList<Inventory> FindByProductUuid(IDataBaseFactory dbFactory, string productUuid)
		{
			return dbFactory.Find<Inventory>("WHERE ProductUuid = @0 ORDER BY WarehouseUuid ", productUuid).ToList();
		}
		public static long CountByProductUuid(IDataBaseFactory dbFactory, string productUuid)
		{
			return dbFactory.Count<Inventory>("WHERE ProductUuid = @0 ", productUuid);
		}
		public static async Task<IList<Inventory>> FindByAsyncProductUuid(IDataBaseFactory dbFactory, string productUuid)
		{
			return (await dbFactory.FindAsync<Inventory>("WHERE ProductUuid = @0 ORDER BY WarehouseUuid ", productUuid)).ToList();
		}
		public static async Task<long> CountByAsyncProductUuid(IDataBaseFactory dbFactory, string productUuid)
		{
			return await dbFactory.CountAsync<Inventory>("WHERE ProductUuid = @0 ", productUuid);
		}
		public static IList<Inventory> FindByWarehouseUuid(IDataBaseFactory dbFactory, string warehouseUuid)
		{
			return dbFactory.Find<Inventory>("WHERE WarehouseUuid = @0 ", warehouseUuid).ToList();
		}
		public static long CountByWarehouseUuid(IDataBaseFactory dbFactory, string warehouseUuid)
		{
			return dbFactory.Count<Inventory>("WHERE WarehouseUuid = @0 ", warehouseUuid);
		}
		public static async Task<IList<Inventory>> FindByAsyncWarehouseUuid(IDataBaseFactory dbFactory, string warehouseUuid)
		{
			return (await dbFactory.FindAsync<Inventory>("WHERE WarehouseUuid = @0 ", warehouseUuid)).ToList();
		}
		public static async Task<long> CountByAsyncWarehouseUuid(IDataBaseFactory dbFactory, string warehouseUuid)
		{
			return await dbFactory.CountAsync<Inventory>("WHERE WarehouseUuid = @0 ", warehouseUuid);
		}


        #endregion Methods - Generated 
    }
}



