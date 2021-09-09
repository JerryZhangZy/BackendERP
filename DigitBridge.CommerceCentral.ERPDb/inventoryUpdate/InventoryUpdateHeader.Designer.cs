

              
              
    

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
    /// Represents a InventoryUpdateHeader.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("InventoryUpdateHeader")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("InventoryUpdateUuid")]
    [DtoName("InventoryUpdateHeaderDto")]
    public partial class InventoryUpdateHeader : TableRepository<InventoryUpdateHeader, long>
    {

        public InventoryUpdateHeader() : base() {}
        public InventoryUpdateHeader(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("DatabaseNum",SqlDbType.Int,NotNull=true)]
        private int _databaseNum;

        [Column("MasterAccountNum",SqlDbType.Int,NotNull=true)]
        private int _masterAccountNum;

        [Column("ProfileNum",SqlDbType.Int,NotNull=true)]
        private int _profileNum;

        [Column("InventoryUpdateUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _inventoryUpdateUuid;

        [Column("BatchNumber",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _batchNumber;

        [Column("InventoryUpdateType",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _inventoryUpdateType;

        [Column("InventoryUpdateStatus",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _inventoryUpdateStatus;

        [Column("UpdateDate",SqlDbType.Date,NotNull=true)]
        private DateTime _updateDate;

        [Column("UpdateTime",SqlDbType.Time,NotNull=true)]
        private TimeSpan _updateTime;

        [Column("Processor",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _processor;

        [Column("WarehouseUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _warehouseUuid;

        [Column("WarehouseCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _warehouseCode;

        [Column("CustomerUuid",SqlDbType.VarChar,NotNull=true)]
        private string _customerUuid;

        [Column("CustomerCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _customerCode;

        [Column("CustomerName",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _customerName;

        [Column("VendorUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _vendorUuid;

        [Column("VendorCode",SqlDbType.VarChar)]
        private string _vendorCode;

        [Column("VendorName",SqlDbType.NVarChar)]
        private string _vendorName;

        [Column("ReferenceType",SqlDbType.Int,NotNull=true)]
        private int _referenceType;

        [Column("ReferenceUuid",SqlDbType.VarChar,NotNull=true)]
        private string _referenceUuid;

        [Column("ReferenceNum",SqlDbType.VarChar,NotNull=true)]
        private string _referenceNum;

        [Column("InventoryUpdateSourceCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _inventoryUpdateSourceCode;

        [Column("UpdateDateUtc",SqlDbType.DateTime)]
        private DateTime? _updateDateUtc;

        [Column("EnterBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _enterBy;

        [Column("UpdateBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _updateBy;

        #endregion Fields - Generated 

        #region Properties - Generated 
		[IgnoreCompare] 
		public override string UniqueId => InventoryUpdateUuid; 
		public override void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(InventoryUpdateUuid)) 
				InventoryUpdateUuid = Guid.NewGuid().ToString(); 
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
		/// InventoryUpdate uuid. <br> Display: false, Editable: false.
		/// </summary>
        public virtual string InventoryUpdateUuid
        {
            get
            {
				return _inventoryUpdateUuid?.TrimEnd(); 
            }
            set
            {
				_inventoryUpdateUuid = value.TruncateTo(50); 
				OnPropertyChanged("InventoryUpdateUuid", value);
            }
        }

		/// <summary>
		/// Readable InventoryUpdate number, unique in same database and profile. <br> Parameter should pass ProfileNum-BatchNumber. <br> Title: InventoryUpdate Number, Display: true, Editable: true
		/// </summary>
        public virtual string BatchNumber
        {
            get
            {
				return _batchNumber?.TrimEnd(); 
            }
            set
            {
				_batchNumber = value.TruncateTo(50); 
				OnPropertyChanged("BatchNumber", value);
            }
        }

		/// <summary>
		/// InventoryUpdate type (Adjust/Damage/Cycle Count/Physical Count). <br> Title: Type, Display: true, Editable: true
		/// </summary>
        public virtual int InventoryUpdateType
        {
            get
            {
				return _inventoryUpdateType; 
            }
            set
            {
				_inventoryUpdateType = value; 
				OnPropertyChanged("InventoryUpdateType", value);
            }
        }

		/// <summary>
		/// InventoryUpdate status. <br> Title: Status, Display: true, Editable: true
		/// </summary>
        public virtual int InventoryUpdateStatus
        {
            get
            {
				return _inventoryUpdateStatus; 
            }
            set
            {
				_inventoryUpdateStatus = value; 
				OnPropertyChanged("InventoryUpdateStatus", value);
            }
        }

		/// <summary>
		/// InventoryUpdate date. <br> Title: Date, Display: true, Editable: true
		/// </summary>
        public virtual DateTime UpdateDate
        {
            get
            {
				return _updateDate; 
            }
            set
            {
				_updateDate = value.Date.ToSqlSafeValue(); 
				OnPropertyChanged("UpdateDate", value);
            }
        }

		/// <summary>
		/// InventoryUpdate time. <br> Title: Time, Display: true, Editable: true
		/// </summary>
        public virtual TimeSpan UpdateTime
        {
            get
            {
				return _updateTime; 
            }
            set
            {
				_updateTime = value.ToSqlSafeValue(); 
				OnPropertyChanged("UpdateTime", value);
            }
        }

		/// <summary>
		/// InventoryUpdate processor account. <br> Title: Processor, Display: true, Editable: true
		/// </summary>
        public virtual string Processor
        {
            get
            {
				return _processor?.TrimEnd(); 
            }
            set
            {
				_processor = value.TruncateTo(50); 
				OnPropertyChanged("Processor", value);
            }
        }

		/// <summary>
		/// (Readonly) Warehouse uuid, warehouse. <br> Display: false, Editable: false
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
		/// Readable warehouse code, warehouse to update inventory. <br> Title: Warehouse Code, Display: true, Editable: true
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
		/// Vendor uuid, load from Vendor data. <br> Display: false, Editable: false
		/// </summary>
        public virtual string VendorUuid
        {
            get
            {
				return _vendorUuid?.TrimEnd(); 
            }
            set
            {
				_vendorUuid = value.TruncateTo(50); 
				OnPropertyChanged("VendorUuid", value);
            }
        }

		/// <summary>
		/// Vendor number. use DatabaseNum-VendorCode too load Vendor data. <br> Title: Vendor code, Display: true, Editable: true
		/// </summary>
        public virtual string VendorCode
        {
            get
            {
				if (!AllowNull && _vendorCode is null) 
					_vendorCode = String.Empty; 
				return _vendorCode?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_vendorCode = value.TruncateTo(50); 
					OnPropertyChanged("VendorCode", value);
				}
            }
        }

		/// <summary>
		/// (Readonly) Vendor name, load from Vendor data. <br> Title: Vendor Name, Display: true, Editable: false
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
					_vendorName = value.TruncateTo(200); 
					OnPropertyChanged("VendorName", value);
				}
            }
        }

		/// <summary>
		/// Reference Transaction Type, reference to invoice, P/O. <br> Display: true, Editable: true
		/// </summary>
        public virtual int ReferenceType
        {
            get
            {
				return _referenceType; 
            }
            set
            {
				_referenceType = value; 
				OnPropertyChanged("ReferenceType", value);
            }
        }

		/// <summary>
		/// Reference Transaction uuid, reference to uuid of invoice, P/O#. <br> Display: false, Editable: false
		/// </summary>
        public virtual string ReferenceUuid
        {
            get
            {
				return _referenceUuid?.TrimEnd(); 
            }
            set
            {
				_referenceUuid = value.TruncateTo(50); 
				OnPropertyChanged("ReferenceUuid", value);
            }
        }

		/// <summary>
		/// Reference Transaction number, reference to invoice#, P/O#. <br> Display: true, Editable: true
		/// </summary>
        public virtual string ReferenceNum
        {
            get
            {
				return _referenceNum?.TrimEnd(); 
            }
            set
            {
				_referenceNum = value.TruncateTo(50); 
				OnPropertyChanged("ReferenceNum", value);
            }
        }

		/// <summary>
		/// (Readonly) InventoryUpdate created from other entity number, use to prevent import duplicate InventoryUpdate. <br> Title: Source Number, Display: false, Editable: false
		/// </summary>
        public virtual string InventoryUpdateSourceCode
        {
            get
            {
				return _inventoryUpdateSourceCode?.TrimEnd(); 
            }
            set
            {
				_inventoryUpdateSourceCode = value.TruncateTo(100); 
				OnPropertyChanged("InventoryUpdateSourceCode", value);
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
		/// (Readonly) User who created this InventoryUpdate. <br> Title: Created By, Display: true, Editable: false
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
		private InventoryUpdateData Parent { get; set; }
		public InventoryUpdateData GetParent() => Parent;
		public InventoryUpdateHeader SetParent(InventoryUpdateData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			InventoryUpdateUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override InventoryUpdateHeader Clear()
        {
            base.Clear();
			_databaseNum = default(int); 
			_masterAccountNum = default(int); 
			_profileNum = default(int); 
			_inventoryUpdateUuid = String.Empty; 
			_batchNumber = String.Empty; 
			_inventoryUpdateType = default(int); 
			_inventoryUpdateStatus = default(int); 
			_updateDate = new DateTime().MinValueSql(); 
			_updateTime = new TimeSpan().MinValueSql(); 
			_processor = String.Empty; 
			_warehouseUuid = String.Empty; 
			_warehouseCode = String.Empty; 
			_customerUuid = String.Empty; 
			_customerCode = String.Empty; 
			_customerName = String.Empty; 
			_vendorUuid = String.Empty; 
			_vendorCode = AllowNull ? (string)null : String.Empty; 
			_vendorName = AllowNull ? (string)null : String.Empty; 
			_referenceType = default(int); 
			_referenceUuid = String.Empty; 
			_referenceNum = String.Empty; 
			_inventoryUpdateSourceCode = String.Empty; 
			_updateDateUtc = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_enterBy = String.Empty; 
			_updateBy = String.Empty; 
            ClearChildren();
            return this;
        }

        public override InventoryUpdateHeader CheckIntegrity()
        {
            CheckUniqueId();
            CheckIntegrityOthers();
            return this;
        }

        public virtual InventoryUpdateHeader ClearChildren()
        {
            return this;
        }

        public virtual InventoryUpdateHeader NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(InventoryUpdateHeader data)
        {
            if (data is null) return;
            return;
        }



        #endregion Methods - Generated 
    }
}



