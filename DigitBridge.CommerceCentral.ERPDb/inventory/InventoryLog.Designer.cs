
              

              
    

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
    /// Represents a InventoryLog.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("InventoryLog")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("InventoryLogUuid")]
    [DtoName("InventoryLogDto")]
    public partial class InventoryLog : TableRepository<InventoryLog, long>
    {

        public InventoryLog() : base() {}
        public InventoryLog(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("DatabaseNum",SqlDbType.Int,NotNull=true)]
        private int _databaseNum;

        [Column("MasterAccountNum",SqlDbType.Int,NotNull=true)]
        private int _masterAccountNum;

        [Column("ProfileNum",SqlDbType.Int,NotNull=true)]
        private int _profileNum;

        [Column("InventoryLogUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _inventoryLogUuid;

        [Column("ProductUuid",SqlDbType.VarChar,NotNull=true)]
        private string _productUuid;

        [Column("InventoryUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _inventoryUuid;

        [Column("BatchNum",SqlDbType.BigInt,IsDefault=true)]
        private long? _batchNum;

        [Column("LogType",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _logType;

        [Column("LogUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _logUuid;

        [Column("LogItemUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _logItemUuid;

        [Column("LogStatus",SqlDbType.Int,IsDefault=true)]
        private int? _logStatus;

        [Column("LogDate",SqlDbType.Date,NotNull=true)]
        private DateTime _logDate;

        [Column("LogTime",SqlDbType.Time,NotNull=true)]
        private TimeSpan _logTime;

        [Column("LogBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _logBy;

        [Column("SKU",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _sKU;

        [Column("Description",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _description;

        [Column("WarehouseUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _warehouseUuid;

        [Column("WhsDescription",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _whsDescription;

        [Column("LotNum",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _lotNum;

        [Column("LotInDate",SqlDbType.Date)]
        private DateTime? _lotInDate;

        [Column("LotExpDate",SqlDbType.Date)]
        private DateTime? _lotExpDate;

        [Column("LotDescription",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _lotDescription;

        [Column("LpnNum",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _lpnNum;

        [Column("LpnDescription",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _lpnDescription;

        [Column("Notes",SqlDbType.NVarChar,NotNull=true)]
        private string _notes;

        [Column("UOM",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _uOM;

        [Column("LogQty",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _logQty;

        [Column("BeforeInstock",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _beforeInstock;

        [Column("BeforeBaseCost",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _beforeBaseCost;

        [Column("BeforeUnitCost",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _beforeUnitCost;

        [Column("BeforeAvgCost",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _beforeAvgCost;

        [Column("UpdateDateUtc",SqlDbType.DateTime)]
        private DateTime? _updateDateUtc;

        [Column("EnterBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _enterBy;

        [Column("UpdateBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _updateBy;

        #endregion Fields - Generated 

        #region Properties - Generated 
		public override string UniqueId => InventoryLogUuid; 
		public void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(InventoryLogUuid)) 
				InventoryLogUuid = Guid.NewGuid().ToString(); 
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
				OnPropertyChanged("DatabaseNum", value);
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
				OnPropertyChanged("MasterAccountNum", value);
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
				OnPropertyChanged("ProfileNum", value);
            }
        }

        public virtual string InventoryLogUuid
        {
            get
            {
				return _inventoryLogUuid?.TrimEnd(); 
            }
            set
            {
				_inventoryLogUuid = value.TruncateTo(50); 
				OnPropertyChanged("InventoryLogUuid", value);
            }
        }

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

        public virtual long? BatchNum
        {
            get
            {
				if (!AllowNull && _batchNum is null) 
					_batchNum = default(long); 
				return _batchNum; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_batchNum = value; 
					OnPropertyChanged("BatchNum", value);
				}
            }
        }

        public virtual string LogType
        {
            get
            {
				return _logType?.TrimEnd(); 
            }
            set
            {
				_logType = value.TruncateTo(50); 
				OnPropertyChanged("LogType", value);
            }
        }

        public virtual string LogUuid
        {
            get
            {
				return _logUuid?.TrimEnd(); 
            }
            set
            {
				_logUuid = value.TruncateTo(50); 
				OnPropertyChanged("LogUuid", value);
            }
        }

        public virtual string LogItemUuid
        {
            get
            {
				return _logItemUuid?.TrimEnd(); 
            }
            set
            {
				_logItemUuid = value.TruncateTo(50); 
				OnPropertyChanged("LogItemUuid", value);
            }
        }

        public virtual int? LogStatus
        {
            get
            {
				if (!AllowNull && _logStatus is null) 
					_logStatus = default(int); 
				return _logStatus; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_logStatus = value; 
					OnPropertyChanged("LogStatus", value);
				}
            }
        }

        public virtual DateTime LogDate
        {
            get
            {
				return _logDate; 
            }
            set
            {
				_logDate = value.Date.ToSqlSafeValue(); 
				OnPropertyChanged("LogDate", value);
            }
        }

        public virtual TimeSpan LogTime
        {
            get
            {
				return _logTime; 
            }
            set
            {
				_logTime = value.ToSqlSafeValue(); 
				OnPropertyChanged("LogTime", value);
            }
        }

        public virtual string LogBy
        {
            get
            {
				return _logBy?.TrimEnd(); 
            }
            set
            {
				_logBy = value.TruncateTo(100); 
				OnPropertyChanged("LogBy", value);
            }
        }

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

        public virtual string WhsDescription
        {
            get
            {
				return _whsDescription?.TrimEnd(); 
            }
            set
            {
				_whsDescription = value.TruncateTo(200); 
				OnPropertyChanged("WhsDescription", value);
            }
        }

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

        public virtual decimal LogQty
        {
            get
            {
				return _logQty; 
            }
            set
            {
				_logQty = value; 
				OnPropertyChanged("LogQty", value);
            }
        }

        public virtual decimal BeforeInstock
        {
            get
            {
				return _beforeInstock; 
            }
            set
            {
				_beforeInstock = value; 
				OnPropertyChanged("BeforeInstock", value);
            }
        }

        public virtual decimal BeforeBaseCost
        {
            get
            {
				return _beforeBaseCost; 
            }
            set
            {
				_beforeBaseCost = value; 
				OnPropertyChanged("BeforeBaseCost", value);
            }
        }

        public virtual decimal BeforeUnitCost
        {
            get
            {
				return _beforeUnitCost; 
            }
            set
            {
				_beforeUnitCost = value; 
				OnPropertyChanged("BeforeUnitCost", value);
            }
        }

        public virtual decimal BeforeAvgCost
        {
            get
            {
				return _beforeAvgCost; 
            }
            set
            {
				_beforeAvgCost = value; 
				OnPropertyChanged("BeforeAvgCost", value);
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
		private InventoryData Parent { get; set; }
		public InventoryData GetParent() => Parent;
		public InventoryLog SetParent(InventoryData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			InventoryLogUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override InventoryLog Clear()
        {
            base.Clear();
			_databaseNum = default(int); 
			_masterAccountNum = default(int); 
			_profileNum = default(int); 
			_inventoryLogUuid = String.Empty; 
			_productUuid = String.Empty; 
			_inventoryUuid = String.Empty; 
			_batchNum = AllowNull ? (long?)null : default(long); 
			_logType = String.Empty; 
			_logUuid = String.Empty; 
			_logItemUuid = String.Empty; 
			_logStatus = AllowNull ? (int?)null : default(int); 
			_logDate = new DateTime().MinValueSql(); 
			_logTime = new TimeSpan().MinValueSql(); 
			_logBy = String.Empty; 
			_sKU = String.Empty; 
			_description = String.Empty; 
			_warehouseUuid = String.Empty; 
			_whsDescription = String.Empty; 
			_lotNum = String.Empty; 
			_lotInDate = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_lotExpDate = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_lotDescription = String.Empty; 
			_lpnNum = String.Empty; 
			_lpnDescription = String.Empty; 
			_notes = String.Empty; 
			_uOM = String.Empty; 
			_logQty = default(decimal); 
			_beforeInstock = default(decimal); 
			_beforeBaseCost = default(decimal); 
			_beforeUnitCost = default(decimal); 
			_beforeAvgCost = default(decimal); 
			_updateDateUtc = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_enterBy = String.Empty; 
			_updateBy = String.Empty; 
            ClearChildren();
            return this;
        }

        public virtual InventoryLog ClearChildren()
        {
            return this;
        }

        public virtual InventoryLog NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(InventoryLog data)
        {
            if (data is null) return;
            return;
        }

		public static IList<InventoryLog> FindByInventoryUuid(IDataBaseFactory dbFactory, string inventoryUuid)
		{
			return dbFactory.Find<InventoryLog>("WHERE InventoryUuid = @0 ", inventoryUuid).ToList();
		}
		public static long CountByInventoryUuid(IDataBaseFactory dbFactory, string inventoryUuid)
		{
			return dbFactory.Count<InventoryLog>("WHERE InventoryUuid = @0 ", inventoryUuid);
		}
		public static async Task<IList<InventoryLog>> FindByAsyncInventoryUuid(IDataBaseFactory dbFactory, string inventoryUuid)
		{
			return (await dbFactory.FindAsync<InventoryLog>("WHERE InventoryUuid = @0 ", inventoryUuid)).ToList();
		}
		public static async Task<long> CountByAsyncInventoryUuid(IDataBaseFactory dbFactory, string inventoryUuid)
		{
			return await dbFactory.CountAsync<InventoryLog>("WHERE InventoryUuid = @0 ", inventoryUuid);
		}
		public static IList<InventoryLog> FindByWarehouseUuid(IDataBaseFactory dbFactory, string warehouseUuid)
		{
			return dbFactory.Find<InventoryLog>("WHERE WarehouseUuid = @0 ", warehouseUuid).ToList();
		}
		public static long CountByWarehouseUuid(IDataBaseFactory dbFactory, string warehouseUuid)
		{
			return dbFactory.Count<InventoryLog>("WHERE WarehouseUuid = @0 ", warehouseUuid);
		}
		public static async Task<IList<InventoryLog>> FindByAsyncWarehouseUuid(IDataBaseFactory dbFactory, string warehouseUuid)
		{
			return (await dbFactory.FindAsync<InventoryLog>("WHERE WarehouseUuid = @0 ", warehouseUuid)).ToList();
		}
		public static async Task<long> CountByAsyncWarehouseUuid(IDataBaseFactory dbFactory, string warehouseUuid)
		{
			return await dbFactory.CountAsync<InventoryLog>("WHERE WarehouseUuid = @0 ", warehouseUuid);
		}


        #endregion Methods - Generated 
    }
}



