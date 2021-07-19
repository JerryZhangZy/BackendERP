





              

              
    

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
    /// Represents a InvoiceReturnItems.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("InvoiceReturnItems")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("ReturnItemUuid")]
    [DtoName("InvoiceReturnItemsDto")]
    public partial class InvoiceReturnItems : TableRepository<InvoiceReturnItems, long>
    {

        public InvoiceReturnItems() : base() {}
        public InvoiceReturnItems(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("ReturnItemUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _returnItemUuid;

        [Column("TransUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _transUuid;

        [Column("Seq",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _seq;

        [Column("InvoiceUuid",SqlDbType.VarChar,NotNull=true)]
        private string _invoiceUuid;

        [Column("InvoiceItemsUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _invoiceItemsUuid;

        [Column("ReturnItemType",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _returnItemType;

        [Column("ReturnItemStatus",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _returnItemStatus;

        [Column("ReturnDate",SqlDbType.Date,NotNull=true)]
        private DateTime _returnDate;

        [Column("ReturnTime",SqlDbType.Time,NotNull=true)]
        private TimeSpan _returnTime;

        [Column("ReceiveDate",SqlDbType.Date)]
        private DateTime? _receiveDate;

        [Column("StockDate",SqlDbType.Date)]
        private DateTime? _stockDate;

        [Column("SKU",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _sKU;

        [Column("ProductUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _productUuid;

        [Column("InventoryUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _inventoryUuid;

        [Column("InvoiceWarehouseUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _invoiceWarehouseUuid;

        [Column("InvoiceWarehouseCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _invoiceWarehouseCode;

        [Column("WarehouseUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _warehouseUuid;

        [Column("WarehouseCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _warehouseCode;

        [Column("LotNum",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _lotNum;

        [Column("Reason",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _reason;

        [Column("Description",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _description;

        [Column("Notes",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _notes;

        [Column("Currency",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _currency;

        [Column("UOM",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _uOM;

        [Column("PackType",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _packType;

        [Column("PackQty",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _packQty;

        [Column("ReturnPack",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _returnPack;

        [Column("ReceivePack",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _receivePack;

        [Column("StockPack",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _stockPack;

        [Column("NonStockPack",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _nonStockPack;

        [Column("ReturnQty",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _returnQty;

        [Column("ReceiveQty",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _receiveQty;

        [Column("StockQty",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _stockQty;

        [Column("NonStockQty",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _nonStockQty;

        [Column("PutBackWarehouseUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _putBackWarehouseUuid;

        [Column("PutBackWarehouseCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _putBackWarehouseCode;

        [Column("DamageWarehouseUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _damageWarehouseUuid;

        [Column("DamageWarehouseCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _damageWarehouseCode;

        [Column("InvoiceDiscountPrice",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _invoiceDiscountPrice;

        [Column("Price",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _price;

        [Column("ExtAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _extAmount;

        [Column("TaxableAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _taxableAmount;

        [Column("NonTaxableAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _nonTaxableAmount;

        [Column("TaxRate",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _taxRate;

        [Column("TaxAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _taxAmount;

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

        [Column("Stockable",SqlDbType.TinyInt,NotNull=true,IsDefault=true)]
        private byte _stockable;

        [Column("IsAr",SqlDbType.TinyInt,NotNull=true,IsDefault=true)]
        private byte _isAr;

        [Column("Taxable",SqlDbType.TinyInt,NotNull=true,IsDefault=true)]
        private byte _taxable;

        [Column("UpdateDateUtc",SqlDbType.DateTime)]
        private DateTime? _updateDateUtc;

        [Column("EnterBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _enterBy;

        [Column("UpdateBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _updateBy;

        #endregion Fields - Generated 

        #region Properties - Generated 
		[IgnoreCompare] 
		public override string UniqueId => ReturnItemUuid; 
		public void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(ReturnItemUuid)) 
				ReturnItemUuid = Guid.NewGuid().ToString(); 
		}
		/// <summary>
		/// (Readonly) Invoice Return Item Line uuid. <br> Display: false, Editable: false
		/// </summary>
        public virtual string ReturnItemUuid
        {
            get
            {
				return _returnItemUuid?.TrimEnd(); 
            }
            set
            {
				_returnItemUuid = value.TruncateTo(50); 
				OnPropertyChanged("ReturnItemUuid", value);
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
		/// Item Line sequence number. <br> Title: Line#, Display: true, Editable: false
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
		/// (Readonly) Invoice Item Line uuid. <br> Display: false, Editable: false
		/// </summary>
        public virtual string InvoiceItemsUuid
        {
            get
            {
				return _invoiceItemsUuid?.TrimEnd(); 
            }
            set
            {
				_invoiceItemsUuid = value.TruncateTo(50); 
				OnPropertyChanged("InvoiceItemsUuid", value);
            }
        }

		/// <summary>
		/// Return item type. <br> Title: Type, Display: true, Editable: true
		/// </summary>
        public virtual int ReturnItemType
        {
            get
            {
				return _returnItemType; 
            }
            set
            {
				_returnItemType = value; 
				OnPropertyChanged("ReturnItemType", value);
            }
        }

		/// <summary>
		/// Return item status. <br> Title: Status, Display: true, Editable: true
		/// </summary>
        public virtual int ReturnItemStatus
        {
            get
            {
				return _returnItemStatus; 
            }
            set
            {
				_returnItemStatus = value; 
				OnPropertyChanged("ReturnItemStatus", value);
            }
        }

		/// <summary>
		/// Return date. <br> Title: Date, Display: true, Editable: true
		/// </summary>
        public virtual DateTime ReturnDate
        {
            get
            {
				return _returnDate; 
            }
            set
            {
				_returnDate = value.Date.ToSqlSafeValue(); 
				OnPropertyChanged("ReturnDate", value);
            }
        }

		/// <summary>
		/// Return time. <br> Title: Time, Display: true, Editable: true
		/// </summary>
        public virtual TimeSpan ReturnTime
        {
            get
            {
				return _returnTime; 
            }
            set
            {
				_returnTime = value.ToSqlSafeValue(); 
				OnPropertyChanged("ReturnTime", value);
            }
        }

		/// <summary>
		/// Return item received date. <br> Title: Receive Date, Display: true, Editable: true
		/// </summary>
        public virtual DateTime? ReceiveDate
        {
            get
            {
				if (!AllowNull && _receiveDate is null) 
					_receiveDate = new DateTime().MinValueSql(); 
				return _receiveDate; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_receiveDate = (value is null) ? (DateTime?) null : value?.Date.ToSqlSafeValue(); 
					OnPropertyChanged("ReceiveDate", value);
				}
            }
        }

		/// <summary>
		/// Stock Return Item Date. <br> Title: Processed Date, Display: true, Editable: true
		/// </summary>
        public virtual DateTime? StockDate
        {
            get
            {
				if (!AllowNull && _stockDate is null) 
					_stockDate = new DateTime().MinValueSql(); 
				return _stockDate; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_stockDate = (value is null) ? (DateTime?) null : value?.Date.ToSqlSafeValue(); 
					OnPropertyChanged("StockDate", value);
				}
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
		/// (Readonly) invoice Warehouse uuid, load from inventory data. <br> Display: false, Editable: false
		/// </summary>
        public virtual string InvoiceWarehouseUuid
        {
            get
            {
				return _invoiceWarehouseUuid?.TrimEnd(); 
            }
            set
            {
				_invoiceWarehouseUuid = value.TruncateTo(50); 
				OnPropertyChanged("InvoiceWarehouseUuid", value);
            }
        }

		/// <summary>
		/// Readable invoice warehouse code, load from inventory data. <br> Title: Invoice Warehouse Code, Display: true, Editable: true
		/// </summary>
        public virtual string InvoiceWarehouseCode
        {
            get
            {
				return _invoiceWarehouseCode?.TrimEnd(); 
            }
            set
            {
				_invoiceWarehouseCode = value.TruncateTo(50); 
				OnPropertyChanged("InvoiceWarehouseCode", value);
            }
        }

		/// <summary>
		/// (Readonly) return Warehouse uuid, load from inventory data. <br> Display: false, Editable: false
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
		/// Readable return warehouse code, load from inventory data. <br> Title: Return Warehouse Code, Display: true, Editable: true
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
		/// Item line description, default from ProductBasic data. <br> Title: Description, Display: true, Editable: true
		/// </summary>
        public virtual string Reason
        {
            get
            {
				return _reason?.TrimEnd(); 
            }
            set
            {
				_reason = value.TruncateTo(200); 
				OnPropertyChanged("Reason", value);
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
		/// item line notes. <br> Title: Notes, Display: true, Editable: true
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
		/// (Ignore) Product SKU Qty pack type, for example: Case, Box, Each
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
		/// (Ignore) Item Qty each per pack.
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
		/// (Ignore) Item Claim return number of pack.
		/// </summary>
        public virtual decimal ReturnPack
        {
            get
            {
				return _returnPack; 
            }
            set
            {
				_returnPack = value; 
				OnPropertyChanged("ReturnPack", value);
            }
        }

		/// <summary>
		/// (Ignore) Receive return number of pack.
		/// </summary>
        public virtual decimal ReceivePack
        {
            get
            {
				return _receivePack; 
            }
            set
            {
				_receivePack = value; 
				OnPropertyChanged("ReceivePack", value);
            }
        }

		/// <summary>
		/// (Ignore) Putback stock number of pack.
		/// </summary>
        public virtual decimal StockPack
        {
            get
            {
				return _stockPack; 
            }
            set
            {
				_stockPack = value; 
				OnPropertyChanged("StockPack", value);
            }
        }

		/// <summary>
		/// (Ignore) Damage or not putback stock number of pack.
		/// </summary>
        public virtual decimal NonStockPack
        {
            get
            {
				return _nonStockPack; 
            }
            set
            {
				_nonStockPack = value; 
				OnPropertyChanged("NonStockPack", value);
            }
        }

		/// <summary>
		/// Claim return Qty. <br> Title: Claim Qty, Display: true, Editable: true
		/// </summary>
        public virtual decimal ReturnQty
        {
            get
            {
				return _returnQty; 
            }
            set
            {
				_returnQty = value; 
				OnPropertyChanged("ReturnQty", value);
            }
        }

		/// <summary>
		/// Receive return qty. <br> Title: Receive Qty, Display: true, Editable: true
		/// </summary>
        public virtual decimal ReceiveQty
        {
            get
            {
				return _receiveQty; 
            }
            set
            {
				_receiveQty = value; 
				OnPropertyChanged("ReceiveQty", value);
            }
        }

		/// <summary>
		/// Putback stock qty. <br> Title: Putback Qty, Display: true, Editable: true
		/// </summary>
        public virtual decimal StockQty
        {
            get
            {
				return _stockQty; 
            }
            set
            {
				_stockQty = value; 
				OnPropertyChanged("StockQty", value);
            }
        }

		/// <summary>
		/// Damage or not putback stock qty. <br> Title: Damage Qty, Display: true, Editable: true
		/// </summary>
        public virtual decimal NonStockQty
        {
            get
            {
				return _nonStockQty; 
            }
            set
            {
				_nonStockQty = value; 
				OnPropertyChanged("NonStockQty", value);
            }
        }

		/// <summary>
		/// (Readonly) putback Warehouse uuid, load from inventory data. <br> Display: false, Editable: false
		/// </summary>
        public virtual string PutBackWarehouseUuid
        {
            get
            {
				return _putBackWarehouseUuid?.TrimEnd(); 
            }
            set
            {
				_putBackWarehouseUuid = value.TruncateTo(50); 
				OnPropertyChanged("PutBackWarehouseUuid", value);
            }
        }

		/// <summary>
		/// Readable putback warehouse code, load from inventory data. <br> Title: Putback Warehouse Code, Display: true, Editable: true
		/// </summary>
        public virtual string PutBackWarehouseCode
        {
            get
            {
				return _putBackWarehouseCode?.TrimEnd(); 
            }
            set
            {
				_putBackWarehouseCode = value.TruncateTo(50); 
				OnPropertyChanged("PutBackWarehouseCode", value);
            }
        }

		/// <summary>
		/// (Readonly) Damage Warehouse uuid, load from inventory data. <br> Display: false, Editable: false
		/// </summary>
        public virtual string DamageWarehouseUuid
        {
            get
            {
				return _damageWarehouseUuid?.TrimEnd(); 
            }
            set
            {
				_damageWarehouseUuid = value.TruncateTo(50); 
				OnPropertyChanged("DamageWarehouseUuid", value);
            }
        }

		/// <summary>
		/// Readable Damage warehouse code, load from inventory data. <br> Title: Damage Warehouse Code, Display: true, Editable: true
		/// </summary>
        public virtual string DamageWarehouseCode
        {
            get
            {
				return _damageWarehouseCode?.TrimEnd(); 
            }
            set
            {
				_damageWarehouseCode = value.TruncateTo(50); 
				OnPropertyChanged("DamageWarehouseCode", value);
            }
        }

		/// <summary>
		/// Item invoice after discount price. <br> Title: Unit Price, Title: Invoice Price, Display: true, Editable: false
		/// </summary>
        public virtual decimal InvoiceDiscountPrice
        {
            get
            {
				return _invoiceDiscountPrice; 
            }
            set
            {
				_invoiceDiscountPrice = value; 
				OnPropertyChanged("InvoiceDiscountPrice", value);
            }
        }

		/// <summary>
		/// Item return price. <br> Title: Return Price, Display: true, Editable: true
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
		/// Item total amount. <br> Title: Ext.Amount, Display: true, Editable: false
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
		/// Amount should apply tax. <br> Display: false, Editable: false
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
		/// Amount should not apply tax. <br> Display: false, Editable: false
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
		/// Default Tax rate for item. <br> Display: false, Editable: false
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
		/// Item level tax amount (include shipping tax and misc tax). <br> Display: false, Editable: false
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
		/// Item level Shipping fee for this item. <br> Display: false, Editable: false
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
		/// (Readonly) Item level tax amount for shipping fee. <br> Title: Shipping Tax, Display: false, Editable: false
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
		/// Item level handling charge. <br> Title: Handling, Display: false, Editable: true
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
		/// (Readonly) Item level tax amount for handling charge. <br> Title: Handling Tax, Display: false, Editable: false
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
		/// Item level other Charg and Allowance Amount. Positive is charge, Negative is Allowance. <br> Title: Charge&Allowance, Display: false, Editable: true
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
		/// item will update inventory instock qty. <br> Title: Stockable, Display: true, Editable: true
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
		/// item will add to Invoice total amount. <br> Title: A/R, Display: true, Editable: true
		/// </summary>
        public virtual bool IsAr
        {
            get
            {
				return (_isAr == 1); 
            }
            set
            {
				_isAr = value ? (byte)1 : (byte)0; 
				OnPropertyChanged("IsAr", value);
            }
        }

		/// <summary>
		/// item will apply tax. <br> Title: Taxable, Display: true, Editable: true
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
		private InvoiceTransactionData Parent { get; set; }
		public InvoiceTransactionData GetParent() => Parent;
		public InvoiceReturnItems SetParent(InvoiceTransactionData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			ReturnItemUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override InvoiceReturnItems Clear()
        {
            base.Clear();
			_returnItemUuid = String.Empty; 
			_transUuid = String.Empty; 
			_seq = default(int); 
			_invoiceUuid = String.Empty; 
			_invoiceItemsUuid = String.Empty; 
			_returnItemType = default(int); 
			_returnItemStatus = default(int); 
			_returnDate = new DateTime().MinValueSql(); 
			_returnTime = new TimeSpan().MinValueSql(); 
			_receiveDate = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_stockDate = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_sKU = String.Empty; 
			_productUuid = String.Empty; 
			_inventoryUuid = String.Empty; 
			_invoiceWarehouseUuid = String.Empty; 
			_invoiceWarehouseCode = String.Empty; 
			_warehouseUuid = String.Empty; 
			_warehouseCode = String.Empty; 
			_lotNum = String.Empty; 
			_reason = String.Empty; 
			_description = String.Empty; 
			_notes = String.Empty; 
			_currency = String.Empty; 
			_uOM = String.Empty; 
			_packType = String.Empty; 
			_packQty = default(decimal); 
			_returnPack = default(decimal); 
			_receivePack = default(decimal); 
			_stockPack = default(decimal); 
			_nonStockPack = default(decimal); 
			_returnQty = default(decimal); 
			_receiveQty = default(decimal); 
			_stockQty = default(decimal); 
			_nonStockQty = default(decimal); 
			_putBackWarehouseUuid = String.Empty; 
			_putBackWarehouseCode = String.Empty; 
			_damageWarehouseUuid = String.Empty; 
			_damageWarehouseCode = String.Empty; 
			_invoiceDiscountPrice = default(decimal); 
			_price = default(decimal); 
			_extAmount = default(decimal); 
			_taxableAmount = default(decimal); 
			_nonTaxableAmount = default(decimal); 
			_taxRate = default(decimal); 
			_taxAmount = default(decimal); 
			_shippingAmount = default(decimal); 
			_shippingTaxAmount = default(decimal); 
			_miscAmount = default(decimal); 
			_miscTaxAmount = default(decimal); 
			_chargeAndAllowanceAmount = default(decimal); 
			_stockable = default(byte); 
			_isAr = default(byte); 
			_taxable = default(byte); 
			_updateDateUtc = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_enterBy = String.Empty; 
			_updateBy = String.Empty; 
            ClearChildren();
            return this;
        }

        public virtual InvoiceReturnItems ClearChildren()
        {
            return this;
        }

        public virtual InvoiceReturnItems NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(InvoiceReturnItems data)
        {
            if (data is null) return;
            return;
        }

		public static IList<InvoiceReturnItems> FindByTransUuid(IDataBaseFactory dbFactory, string transUuid)
		{
			return dbFactory.Find<InvoiceReturnItems>("WHERE TransUuid = @0 ORDER BY Seq ", transUuid).ToList();
		}
		public static long CountByTransUuid(IDataBaseFactory dbFactory, string transUuid)
		{
			return dbFactory.Count<InvoiceReturnItems>("WHERE TransUuid = @0 ", transUuid);
		}
		public static async Task<IList<InvoiceReturnItems>> FindByAsyncTransUuid(IDataBaseFactory dbFactory, string transUuid)
		{
			return (await dbFactory.FindAsync<InvoiceReturnItems>("WHERE TransUuid = @0 ORDER BY Seq ", transUuid)).ToList();
		}
		public static async Task<long> CountByAsyncTransUuid(IDataBaseFactory dbFactory, string transUuid)
		{
			return await dbFactory.CountAsync<InvoiceReturnItems>("WHERE TransUuid = @0 ", transUuid);
		}
		public static IList<InvoiceReturnItems> FindByInvoiceUuid(IDataBaseFactory dbFactory, string invoiceUuid)
		{
			return dbFactory.Find<InvoiceReturnItems>("WHERE InvoiceUuid = @0 ORDER BY Seq ", invoiceUuid).ToList();
		}
		public static long CountByInvoiceUuid(IDataBaseFactory dbFactory, string invoiceUuid)
		{
			return dbFactory.Count<InvoiceReturnItems>("WHERE InvoiceUuid = @0 ", invoiceUuid);
		}
		public static async Task<IList<InvoiceReturnItems>> FindByAsyncInvoiceUuid(IDataBaseFactory dbFactory, string invoiceUuid)
		{
			return (await dbFactory.FindAsync<InvoiceReturnItems>("WHERE InvoiceUuid = @0 ORDER BY Seq ", invoiceUuid)).ToList();
		}
		public static async Task<long> CountByAsyncInvoiceUuid(IDataBaseFactory dbFactory, string invoiceUuid)
		{
			return await dbFactory.CountAsync<InvoiceReturnItems>("WHERE InvoiceUuid = @0 ", invoiceUuid);
		}
		public static IList<InvoiceReturnItems> FindByInvoiceItemsUuid(IDataBaseFactory dbFactory, string invoiceItemsUuid)
		{
			return dbFactory.Find<InvoiceReturnItems>("WHERE InvoiceItemsUuid = @0 ", invoiceItemsUuid).ToList();
		}
		public static long CountByInvoiceItemsUuid(IDataBaseFactory dbFactory, string invoiceItemsUuid)
		{
			return dbFactory.Count<InvoiceReturnItems>("WHERE InvoiceItemsUuid = @0 ", invoiceItemsUuid);
		}
		public static async Task<IList<InvoiceReturnItems>> FindByAsyncInvoiceItemsUuid(IDataBaseFactory dbFactory, string invoiceItemsUuid)
		{
			return (await dbFactory.FindAsync<InvoiceReturnItems>("WHERE InvoiceItemsUuid = @0 ", invoiceItemsUuid)).ToList();
		}
		public static async Task<long> CountByAsyncInvoiceItemsUuid(IDataBaseFactory dbFactory, string invoiceItemsUuid)
		{
			return await dbFactory.CountAsync<InvoiceReturnItems>("WHERE InvoiceItemsUuid = @0 ", invoiceItemsUuid);
		}


        #endregion Methods - Generated 
    }
}



