              
              
    

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
    /// Represents a ProductBasic.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("ProductBasic")]
    [PrimaryKey("CentralProductNum", AutoIncrement = true)]
    [UniqueId("ProductUuid")]
    [DtoName("ProductBasicDto")]
    public partial class ProductBasic : TableRepository<ProductBasic, long>
    {

        public ProductBasic() : base() {}
        public ProductBasic(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
		[ResultColumn(Name = "CentralProductNum", IncludeInAutoSelect = IncludeInAutoSelect.Yes)] 
		protected long _centralProductNum; 
		[XmlIgnore, IgnoreCompare] 
		public virtual long CentralProductNum
		{
			get => _centralProductNum;
			set => _centralProductNum = value;
		}
		[XmlIgnore, IgnoreCompare] 
		public override long RowNum
		{
			get => CentralProductNum.ToLong();
			set => CentralProductNum = value.ToLong();
		}
		[JsonIgnore, XmlIgnore, IgnoreCompare] 
		public override bool IsNew => CentralProductNum <= 0; 
        [Column("DatabaseNum",SqlDbType.Int,NotNull=true)]
        private int _databaseNum;

        [Column("MasterAccountNum",SqlDbType.Int,NotNull=true)]
        private int _masterAccountNum;

        [Column("ProfileNum",SqlDbType.Int,NotNull=true)]
        private int _profileNum;

        [Column("SKU",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _sku;

        [Column("FNSku",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _fNSku;

        [Column("Condition",SqlDbType.TinyInt,NotNull=true,IsDefault=true)]
        private byte _condition;

        [Column("Brand",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _brand;

        [Column("Manufacturer",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _manufacturer;

        [Column("ProductTitle",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _productTitle;

        [Column("LongDescription",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _longDescription;

        [Column("ShortDescription",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _shortDescription;

        [Column("Subtitle",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _subtitle;

        [Column("ASIN",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _aSIN;

        [Column("UPC",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _uPC;

        [Column("EAN",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _eAN;

        [Column("ISBN",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _iSBN;

        [Column("MPN",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _mPN;

        [Column("Price",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _price;

        [Column("Cost",SqlDbType.Money,NotNull=true,IsDefault=true)]
        private decimal _cost;

        [Column("AvgCost",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _avgCost;

        [Column("MAPPrice",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _mAPPrice;

        [Column("MSRP",SqlDbType.Money,NotNull=true,IsDefault=true)]
        private decimal _mSRP;

        [Column("BundleType",SqlDbType.TinyInt,NotNull=true,IsDefault=true)]
        private byte _bundleType;

        [Column("ProductType",SqlDbType.TinyInt,NotNull=true,IsDefault=true)]
        private byte _productType;

        [Column("VariationVaryBy",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _variationVaryBy;

        [Column("CopyToChildren",SqlDbType.TinyInt,NotNull=true,IsDefault=true)]
        private byte _copyToChildren;

        [Column("MultipackQuantity",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _multipackQuantity;

        [Column("VariationParentSKU",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _variationParentSKU;

        [Column("IsInRelationship",SqlDbType.TinyInt,NotNull=true,IsDefault=true)]
        private byte _isInRelationship;

        [Column("NetWeight",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _netWeight;

        [Column("GrossWeight",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _grossWeight;

        [Column("WeightUnit",SqlDbType.TinyInt,NotNull=true,IsDefault=true)]
        private byte _weightUnit;

        [Column("ProductHeight",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _productHeight;

        [Column("ProductLength",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _productLength;

        [Column("ProductWidth",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _productWidth;

        [Column("BoxHeight",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _boxHeight;

        [Column("BoxLength",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _boxLength;

        [Column("BoxWidth",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _boxWidth;

        [Column("DimensionUnit",SqlDbType.TinyInt,NotNull=true,IsDefault=true)]
        private byte _dimensionUnit;

        [Column("HarmonizedCode",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _harmonizedCode;

        [Column("TaxProductCode",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _taxProductCode;

        [Column("IsBlocked",SqlDbType.TinyInt,NotNull=true,IsDefault=true)]
        private byte _isBlocked;

        [Column("Warranty",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _warranty;

        [Column("CreateBy",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _createBy;

        [Column("UpdateBy",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _updateBy;

        [Column("CreateDate",SqlDbType.DateTime,NotNull=true,IsDefault=true)]
        private DateTime _createDate;

        [Column("UpdateDate",SqlDbType.DateTime,NotNull=true,IsDefault=true)]
        private DateTime _updateDate;

        [Column("ClassificationNum",SqlDbType.BigInt,NotNull=true,IsDefault=true)]
        private long _classificationNum;

        [Column("OriginalUPC",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _originalUPC;

        [Column("ProductUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _productUuid;

        #endregion Fields - Generated 

        #region Properties - Generated 
		[IgnoreCompare] 
		public override string UniqueId => ProductUuid; 
		public override void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(ProductUuid)) 
				ProductUuid = Guid.NewGuid().ToString(); 
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
		/// Product SKU. Required. <br> Title: Sku, Display: true, Editable: true
		/// </summary>
        public virtual string SKU
        {
            get
            {
				return _sku?.TrimEnd(); 
            }
            set
            {
				_sku = value.TruncateTo(100); 
				OnPropertyChanged("SKU", value);
            }
        }

		/// <summary>
		/// Product FN SKU. <br> Title: FNSku, Display: true, Editable: true
		/// </summary>
        public virtual string FNSku
        {
            get
            {
				return _fNSku?.TrimEnd(); 
            }
            set
            {
				_fNSku = value.TruncateTo(10); 
				OnPropertyChanged("FNSku", value);
            }
        }

		/// <summary>
		/// Product FN SKU. <br> Title: Condition, Display: true, Editable: true
		/// </summary>
        public virtual byte Condition
        {
            get
            {
				return _condition; 
            }
            set
            {
				_condition = value; 
				OnPropertyChanged("Condition", value);
            }
        }

		/// <summary>
		/// Product Brand. <br> Title: Brand, Display: true, Editable: true
		/// </summary>
        public virtual string Brand
        {
            get
            {
				return _brand?.TrimEnd(); 
            }
            set
            {
				_brand = value.TruncateTo(150); 
				OnPropertyChanged("Brand", value);
            }
        }

		/// <summary>
		/// Product Manufacturer. <br> Title: Manufacturer, Display: true, Editable: true
		/// </summary>
        public virtual string Manufacturer
        {
            get
            {
				return _manufacturer?.TrimEnd(); 
            }
            set
            {
				_manufacturer = value.TruncateTo(255); 
				OnPropertyChanged("Manufacturer", value);
            }
        }

		/// <summary>
		/// Product Title. <br> Title: Title, Display: true, Editable: true
		/// </summary>
        public virtual string ProductTitle
        {
            get
            {
				return _productTitle?.TrimEnd(); 
            }
            set
            {
				_productTitle = value.TruncateTo(500); 
				OnPropertyChanged("ProductTitle", value);
            }
        }

		/// <summary>
		/// Product Long Description. <br> Title: Long Description, Display: true, Editable: true
		/// </summary>
        public virtual string LongDescription
        {
            get
            {
				return _longDescription?.TrimEnd(); 
            }
            set
            {
				_longDescription = value.TruncateTo(2000); 
				OnPropertyChanged("LongDescription", value);
            }
        }

		/// <summary>
		/// Product Short Description. <br> Title: Short Description, Display: true, Editable: true
		/// </summary>
        public virtual string ShortDescription
        {
            get
            {
				return _shortDescription?.TrimEnd(); 
            }
            set
            {
				_shortDescription = value.TruncateTo(100); 
				OnPropertyChanged("ShortDescription", value);
            }
        }

		/// <summary>
		/// Product Subtitle. <br> Title: Subtitle, Display: true, Editable: true
		/// </summary>
        public virtual string Subtitle
        {
            get
            {
				return _subtitle?.TrimEnd(); 
            }
            set
            {
				_subtitle = value.TruncateTo(50); 
				OnPropertyChanged("Subtitle", value);
            }
        }

		/// <summary>
		/// Product ASIN. <br> Title: ASIN, Display: true, Editable: true
		/// </summary>
        public virtual string ASIN
        {
            get
            {
				return _aSIN?.TrimEnd(); 
            }
            set
            {
				_aSIN = value.TruncateTo(10); 
				OnPropertyChanged("ASIN", value);
            }
        }

		/// <summary>
		/// Product UPC. <br> Title: UPC, Display: true, Editable: true
		/// </summary>
        public virtual string UPC
        {
            get
            {
				return _uPC?.TrimEnd(); 
            }
            set
            {
				_uPC = value.TruncateTo(20); 
				OnPropertyChanged("UPC", value);
            }
        }

		/// <summary>
		/// Product EAN. <br> Title: EAN, Display: true, Editable: true
		/// </summary>
        public virtual string EAN
        {
            get
            {
				return _eAN?.TrimEnd(); 
            }
            set
            {
				_eAN = value.TruncateTo(20); 
				OnPropertyChanged("EAN", value);
            }
        }

		/// <summary>
		/// Product UPC. <br> Title: ISBN, Display: true, Editable: true
		/// </summary>
        public virtual string ISBN
        {
            get
            {
				return _iSBN?.TrimEnd(); 
            }
            set
            {
				_iSBN = value.TruncateTo(20); 
				OnPropertyChanged("ISBN", value);
            }
        }

		/// <summary>
		/// Product UPC. <br> Title: MPN, Display: true, Editable: true
		/// </summary>
        public virtual string MPN
        {
            get
            {
				return _mPN?.TrimEnd(); 
            }
            set
            {
				_mPN = value.TruncateTo(50); 
				OnPropertyChanged("MPN", value);
            }
        }

		/// <summary>
		/// Product retail price. <br> Title: Default Price, Display: true, Editable: true
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
		/// Product display sales cost. <br> Title: Sales Cost, Display: true, Editable: true
		/// </summary>
        public virtual decimal Cost
        {
            get
            {
				return _cost; 
            }
            set
            {
				_cost = value; 
				OnPropertyChanged("Cost", value);
            }
        }

		/// <summary>
		/// (Ignore) Product display avg. cost. <br> Title: Sales Cost, Display: true, Editable: true
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
		/// Product MAP Price. <br> Title: MAP Price, Display: true, Editable: true
		/// </summary>
        public virtual decimal MAPPrice
        {
            get
            {
				return _mAPPrice; 
            }
            set
            {
				_mAPPrice = value; 
				OnPropertyChanged("MAPPrice", value);
            }
        }

		/// <summary>
		/// Product MSRP Price. <br> Title: MSRP, Display: true, Editable: true
		/// </summary>
        public virtual decimal MSRP
        {
            get
            {
				return _mSRP; 
            }
            set
            {
				_mSRP = value; 
				OnPropertyChanged("MSRP", value);
            }
        }

		/// <summary>
		/// Product is Bundle. None=0 ; BundleItem =1. <br> Title: Bundle, Display: true, Editable: true
		/// </summary>
        public virtual byte BundleType
        {
            get
            {
				return _bundleType; 
            }
            set
            {
				_bundleType = value; 
				OnPropertyChanged("BundleType", value);
            }
        }

		/// <summary>
		/// Product Type. <br> Title: Type, Display: true, Editable: true
		/// </summary>
        public virtual byte ProductType
        {
            get
            {
				return _productType; 
            }
            set
            {
				_productType = value; 
				OnPropertyChanged("ProductType", value);
            }
        }

		/// <summary>
		/// Product Variation By Item=0 ; Child =1; Parent =2. <br> Title: VariationBy, Display: true, Editable: true
		/// </summary>
        public virtual string VariationVaryBy
        {
            get
            {
				return _variationVaryBy?.TrimEnd(); 
            }
            set
            {
				_variationVaryBy = value.TruncateTo(80); 
				OnPropertyChanged("VariationVaryBy", value);
            }
        }

		/// <summary>
		/// Product info need CopyToChildren. <br> Title: CopyToChildren, Display: true, Editable: true
		/// </summary>
        public virtual byte CopyToChildren
        {
            get
            {
				return _copyToChildren; 
            }
            set
            {
				_copyToChildren = value; 
				OnPropertyChanged("CopyToChildren", value);
            }
        }

		/// <summary>
		/// Product include Multiple Quantity. <br> Title: MultipackQuantity, Display: true, Editable: true
		/// </summary>
        public virtual int MultipackQuantity
        {
            get
            {
				return _multipackQuantity; 
            }
            set
            {
				_multipackQuantity = value; 
				OnPropertyChanged("MultipackQuantity", value);
            }
        }

		/// <summary>
		/// Variation Parent SKU. <br> Title: Parent SKU, Display: true, Editable: true
		/// </summary>
        public virtual string VariationParentSKU
        {
            get
            {
				return _variationParentSKU?.TrimEnd(); 
            }
            set
            {
				_variationParentSKU = value.TruncateTo(50); 
				OnPropertyChanged("VariationParentSKU", value);
            }
        }

		/// <summary>
		/// IsInRelationship. <br> Title: In Relationship, Display: true, Editable: true
		/// </summary>
        public virtual byte IsInRelationship
        {
            get
            {
				return _isInRelationship; 
            }
            set
            {
				_isInRelationship = value; 
				OnPropertyChanged("IsInRelationship", value);
            }
        }

		/// <summary>
		/// Net Weight. <br> Title: Net Weight, Display: true, Editable: true
		/// </summary>
        public virtual decimal NetWeight
        {
            get
            {
				return _netWeight; 
            }
            set
            {
				_netWeight = value; 
				OnPropertyChanged("NetWeight", value);
            }
        }

		/// <summary>
		/// Gross Weight. <br> Title: Gross Weight, Display: true, Editable: true
		/// </summary>
        public virtual decimal GrossWeight
        {
            get
            {
				return _grossWeight; 
            }
            set
            {
				_grossWeight = value; 
				OnPropertyChanged("GrossWeight", value);
            }
        }

		/// <summary>
		/// Unit measure of Weight. <br> Title: Weight Unit, Display: true, Editable: true
		/// </summary>
        public virtual byte WeightUnit
        {
            get
            {
				return _weightUnit; 
            }
            set
            {
				_weightUnit = value; 
				OnPropertyChanged("WeightUnit", value);
            }
        }

		/// <summary>
		/// Height. <br> Title: Height, Display: true, Editable: true
		/// </summary>
        public virtual decimal ProductHeight
        {
            get
            {
				return _productHeight; 
            }
            set
            {
				_productHeight = value; 
				OnPropertyChanged("ProductHeight", value);
            }
        }

		/// <summary>
		/// Length. <br> Title: Length, Display: true, Editable: true
		/// </summary>
        public virtual decimal ProductLength
        {
            get
            {
				return _productLength; 
            }
            set
            {
				_productLength = value; 
				OnPropertyChanged("ProductLength", value);
            }
        }

		/// <summary>
		/// Width. <br> Title: Width, Display: true, Editable: true
		/// </summary>
        public virtual decimal ProductWidth
        {
            get
            {
				return _productWidth; 
            }
            set
            {
				_productWidth = value; 
				OnPropertyChanged("ProductWidth", value);
            }
        }

		/// <summary>
		/// Box Height. <br> Title: Box Height, Display: true, Editable: true
		/// </summary>
        public virtual decimal BoxHeight
        {
            get
            {
				return _boxHeight; 
            }
            set
            {
				_boxHeight = value; 
				OnPropertyChanged("BoxHeight", value);
            }
        }

		/// <summary>
		/// Box Length. <br> Title: Box Length, Display: true, Editable: true
		/// </summary>
        public virtual decimal BoxLength
        {
            get
            {
				return _boxLength; 
            }
            set
            {
				_boxLength = value; 
				OnPropertyChanged("BoxLength", value);
            }
        }

		/// <summary>
		/// Box Width. <br> Title: Box Width, Display: true, Editable: true
		/// </summary>
        public virtual decimal BoxWidth
        {
            get
            {
				return _boxWidth; 
            }
            set
            {
				_boxWidth = value; 
				OnPropertyChanged("BoxWidth", value);
            }
        }

		/// <summary>
		/// Dimension measure unit. <br> Title: Dimension Unit, Display: true, Editable: true
		/// </summary>
        public virtual byte DimensionUnit
        {
            get
            {
				return _dimensionUnit; 
            }
            set
            {
				_dimensionUnit = value; 
				OnPropertyChanged("DimensionUnit", value);
            }
        }

		/// <summary>
		/// HarmonizedCode. <br> Title: Harmonized, Display: true, Editable: true
		/// </summary>
        public virtual string HarmonizedCode
        {
            get
            {
				return _harmonizedCode?.TrimEnd(); 
            }
            set
            {
				_harmonizedCode = value.TruncateTo(20); 
				OnPropertyChanged("HarmonizedCode", value);
            }
        }

		/// <summary>
		/// TaxProductCode. <br> Title: Tax Code, Display: true, Editable: true
		/// </summary>
        public virtual string TaxProductCode
        {
            get
            {
				return _taxProductCode?.TrimEnd(); 
            }
            set
            {
				_taxProductCode = value.TruncateTo(25); 
				OnPropertyChanged("TaxProductCode", value);
            }
        }

		/// <summary>
		/// Product Is Blocked. <br> Title: Blocked, Display: true, Editable: true
		/// </summary>
        public virtual byte IsBlocked
        {
            get
            {
				return _isBlocked; 
            }
            set
            {
				_isBlocked = value; 
				OnPropertyChanged("IsBlocked", value);
            }
        }

		/// <summary>
		/// Product Warranty. <br> Title: Warranty, Display: true, Editable: true
		/// </summary>
        public virtual string Warranty
        {
            get
            {
				return _warranty?.TrimEnd(); 
            }
            set
            {
				_warranty = value.TruncateTo(255); 
				OnPropertyChanged("Warranty", value);
            }
        }

		/// <summary>
		/// (Readonly) User who created this product. <br> Title: Created By, Display: true, Editable: false
		/// </summary>
        public virtual string CreateBy
        {
            get
            {
				return _createBy?.TrimEnd(); 
            }
            set
            {
				_createBy = value.TruncateTo(100); 
				OnPropertyChanged("CreateBy", value);
            }
        }

		/// <summary>
		/// (Readonly) User who updated this product. <br> Title: Updated By, Display: true, Editable: false
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

		/// <summary>
		/// (Radonly) Created Date time. <br> Title: Created At, Display: true, Editable: false
		/// </summary>
        public virtual DateTime CreateDate
        {
            get
            {
				return _createDate; 
            }
            set
            {
				_createDate = value.ToSqlSafeValue(); 
				OnPropertyChanged("CreateDate", value);
            }
        }

		/// <summary>
		/// (Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
		/// </summary>
        public virtual DateTime UpdateDate
        {
            get
            {
				return _updateDate; 
            }
            set
            {
				_updateDate = value.ToSqlSafeValue(); 
				OnPropertyChanged("UpdateDate", value);
            }
        }

		/// <summary>
		/// ClassificationNum. <br> Title: Classification, Display: true, Editable: true
		/// </summary>
        public virtual long ClassificationNum
        {
            get
            {
				return _classificationNum; 
            }
            set
            {
				_classificationNum = value; 
				OnPropertyChanged("ClassificationNum", value);
            }
        }

		/// <summary>
		/// Product Original UPC. <br> Title: Original UPC, Display: true, Editable: true
		/// </summary>
        public virtual string OriginalUPC
        {
            get
            {
				return _originalUPC?.TrimEnd(); 
            }
            set
            {
				_originalUPC = value.TruncateTo(20); 
				OnPropertyChanged("OriginalUPC", value);
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



        #endregion Properties - Generated 

        #region Methods - Parent

		[JsonIgnore, XmlIgnore, IgnoreCompare]
		private InventoryData Parent { get; set; }
		public InventoryData GetParent() => Parent;
		public ProductBasic SetParent(InventoryData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			ProductUuid = Guid.NewGuid().ToString(); 
			_centralProductNum = 0; 
            return;
        }

        public override ProductBasic Clear()
        {
            base.Clear();
			_centralProductNum = default(long); 
			_databaseNum = default(int); 
			_masterAccountNum = default(int); 
			_profileNum = default(int); 
			_sku = String.Empty; 
			_fNSku = String.Empty; 
			_condition = default(byte); 
			_brand = String.Empty; 
			_manufacturer = String.Empty; 
			_productTitle = String.Empty; 
			_longDescription = String.Empty; 
			_shortDescription = String.Empty; 
			_subtitle = String.Empty; 
			_aSIN = String.Empty; 
			_uPC = String.Empty; 
			_eAN = String.Empty; 
			_iSBN = String.Empty; 
			_mPN = String.Empty; 
			_price = default(decimal); 
			_cost = default(decimal); 
			_avgCost = default(decimal); 
			_mAPPrice = default(decimal); 
			_mSRP = default(decimal); 
			_bundleType = default(byte); 
			_productType = default(byte); 
			_variationVaryBy = String.Empty; 
			_copyToChildren = default(byte); 
			_multipackQuantity = default(int); 
			_variationParentSKU = String.Empty; 
			_isInRelationship = default(byte); 
			_netWeight = default(decimal); 
			_grossWeight = default(decimal); 
			_weightUnit = default(byte); 
			_productHeight = default(decimal); 
			_productLength = default(decimal); 
			_productWidth = default(decimal); 
			_boxHeight = default(decimal); 
			_boxLength = default(decimal); 
			_boxWidth = default(decimal); 
			_dimensionUnit = default(byte); 
			_harmonizedCode = String.Empty; 
			_taxProductCode = String.Empty; 
			_isBlocked = default(byte); 
			_warranty = String.Empty; 
			_createBy = String.Empty; 
			_updateBy = String.Empty; 
			_createDate = new DateTime().MinValueSql(); 
			_updateDate = new DateTime().MinValueSql(); 
			_classificationNum = default(long); 
			_originalUPC = String.Empty; 
			_productUuid = String.Empty; 
            ClearChildren();
            return this;
        }

        public override ProductBasic CheckIntegrity()
        {
            CheckUniqueId();
            CheckIntegrityOthers();
            return this;
        }

        public virtual ProductBasic ClearChildren()
        {
            return this;
        }

        public virtual ProductBasic NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(ProductBasic data)
        {
            if (data is null) return;
            return;
        }


		public override ProductBasic ConvertDbFieldsToData()
		{
			base.ConvertDbFieldsToData();
			return this;
		}
		public override ProductBasic ConvertDataFieldsToDb()
		{
			base.ConvertDataFieldsToDb();
			return this;
		}

        #endregion Methods - Generated 
    }
}



