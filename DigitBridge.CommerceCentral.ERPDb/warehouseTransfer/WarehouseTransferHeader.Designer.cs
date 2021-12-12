

              
              
    

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
    /// Represents a WarehouseTransferHeader.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("WarehouseTransferHeader")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("WarehouseTransferUuid")]
    [DtoName("WarehouseTransferHeaderDto")]
    public partial class WarehouseTransferHeader : TableRepository<WarehouseTransferHeader, long>
    {

        public WarehouseTransferHeader() : base() {}
        public WarehouseTransferHeader(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("DatabaseNum",SqlDbType.Int,NotNull=true)]
        private int _databaseNum;

        [Column("MasterAccountNum",SqlDbType.Int,NotNull=true)]
        private int _masterAccountNum;

        [Column("ProfileNum",SqlDbType.Int,NotNull=true)]
        private int _profileNum;

        [Column("WarehouseTransferUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _warehouseTransferUuid;

        [Column("BatchNumber",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _batchNumber;

        [Column("TransferStatus",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _transferStatus;

        [Column("WarehouseTransferType",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _warehouseTransferType;

        [Column("WarehouseTransferStatus",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _warehouseTransferStatus;

        [Column("TransferDate",SqlDbType.Date,NotNull=true)]
        private DateTime _transferDate;

        [Column("TransferTime",SqlDbType.Time,NotNull=true)]
        private TimeSpan _transferTime;

        [Column("Processor",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _processor;

        [Column("ReceiveDate",SqlDbType.Date,NotNull=true)]
        private DateTime _receiveDate;

        [Column("ReceiveTime",SqlDbType.Time,NotNull=true)]
        private TimeSpan _receiveTime;

        [Column("ReceiveProcessor",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _receiveProcessor;

        [Column("FromWarehouseUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _fromWarehouseUuid;

        [Column("FromWarehouseCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _fromWarehouseCode;

        [Column("ToWarehouseUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _toWarehouseUuid;

        [Column("ToWarehouseCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _toWarehouseCode;

        [Column("ReferenceType",SqlDbType.Int,NotNull=true)]
        private int _referenceType;

        [Column("ReferenceUuid",SqlDbType.VarChar,NotNull=true)]
        private string _referenceUuid;

        [Column("ReferenceNum",SqlDbType.VarChar,NotNull=true)]
        private string _referenceNum;

        [Column("WarehouseTransferSourceCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _warehouseTransferSourceCode;

        [Column("UpdateDateUtc",SqlDbType.DateTime)]
        private DateTime? _updateDateUtc;

        [Column("EnterBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _enterBy;

        [Column("UpdateBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _updateBy;

        #endregion Fields - Generated 

        #region Properties - Generated 
		[IgnoreCompare] 
		public override string UniqueId => WarehouseTransferUuid; 
		public override void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(WarehouseTransferUuid)) 
				WarehouseTransferUuid = Guid.NewGuid().ToString(); 
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
		/// WarehouseTransfer uuid. <br> Display: false, Editable: false.
		/// </summary>
        public virtual string WarehouseTransferUuid
        {
            get
            {
				return _warehouseTransferUuid?.TrimEnd(); 
            }
            set
            {
				_warehouseTransferUuid = value.TruncateTo(50); 
				OnPropertyChanged("WarehouseTransferUuid", value);
            }
        }

		/// <summary>
		/// Readable WarehouseTransfer number, unique in same database and profile. <br> Parameter should pass ProfileNum-BatchNumber. <br> Title: WarehouseTransfer Number, Display: true, Editable: true
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
		/// TransferStatus (New/Closed). <br> Title: Type, Display: true, Editable: false
		/// </summary>
        public virtual int TransferStatus
        {
            get
            {
				return _transferStatus; 
            }
            set
            {
				_transferStatus = value; 
				OnPropertyChanged("TransferStatus", value);
            }
        }

		/// <summary>
		/// WarehouseTransfer type (Adjust/Damage/Cycle Count/Physical Count). <br> Title: Type, Display: true, Editable: true
		/// </summary>
        public virtual int WarehouseTransferType
        {
            get
            {
				return _warehouseTransferType; 
            }
            set
            {
				_warehouseTransferType = value; 
				OnPropertyChanged("WarehouseTransferType", value);
            }
        }

		/// <summary>
		/// WarehouseTransfer status. <br> Title: Status, Display: true, Editable: true
		/// </summary>
        public virtual int WarehouseTransferStatus
        {
            get
            {
				return _warehouseTransferStatus; 
            }
            set
            {
				_warehouseTransferStatus = value; 
				OnPropertyChanged("WarehouseTransferStatus", value);
            }
        }

		/// <summary>
		/// WarehouseTransfer date. <br> Title: Date, Display: true, Editable: true
		/// </summary>
        public virtual DateTime TransferDate
        {
            get
            {
				return _transferDate; 
            }
            set
            {
				_transferDate = value.Date.ToSqlSafeValue(); 
				OnPropertyChanged("TransferDate", value);
            }
        }

		/// <summary>
		/// WarehouseTransfer time. <br> Title: Time, Display: true, Editable: true
		/// </summary>
        public virtual TimeSpan TransferTime
        {
            get
            {
				return _transferTime; 
            }
            set
            {
				_transferTime = value.ToSqlSafeValue(); 
				OnPropertyChanged("TransferTime", value);
            }
        }

		/// <summary>
		/// WarehouseTransfer processor account. <br> Title: Processor, Display: true, Editable: true
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
		/// WarehouseTransfer date. <br> Title: Date, Display: true, Editable: true
		/// </summary>
        public virtual DateTime ReceiveDate
        {
            get
            {
				return _receiveDate; 
            }
            set
            {
				_receiveDate = value.Date.ToSqlSafeValue(); 
				OnPropertyChanged("ReceiveDate", value);
            }
        }

		/// <summary>
		/// WarehouseTransfer time. <br> Title: Time, Display: true, Editable: true
		/// </summary>
        public virtual TimeSpan ReceiveTime
        {
            get
            {
				return _receiveTime; 
            }
            set
            {
				_receiveTime = value.ToSqlSafeValue(); 
				OnPropertyChanged("ReceiveTime", value);
            }
        }

		/// <summary>
		/// WarehouseTransfer processor account. <br> Title: Processor, Display: true, Editable: true
		/// </summary>
        public virtual string ReceiveProcessor
        {
            get
            {
				return _receiveProcessor?.TrimEnd(); 
            }
            set
            {
				_receiveProcessor = value.TruncateTo(50); 
				OnPropertyChanged("ReceiveProcessor", value);
            }
        }

		/// <summary>
		/// (Readonly) Warehouse uuid, transfer from warehouse. <br> Display: false, Editable: false
		/// </summary>
        public virtual string FromWarehouseUuid
        {
            get
            {
				return _fromWarehouseUuid?.TrimEnd(); 
            }
            set
            {
				_fromWarehouseUuid = value.TruncateTo(50); 
				OnPropertyChanged("FromWarehouseUuid", value);
            }
        }

		/// <summary>
		/// Readable warehouse code, transfer from warehouse. <br> Title: Warehouse Code, Display: true, Editable: true
		/// </summary>
        public virtual string FromWarehouseCode
        {
            get
            {
				return _fromWarehouseCode?.TrimEnd(); 
            }
            set
            {
				_fromWarehouseCode = value.TruncateTo(50); 
				OnPropertyChanged("FromWarehouseCode", value);
            }
        }

		/// <summary>
		/// (Readonly) Warehouse uuid, transfer to warehouse. <br> Display: false, Editable: false
		/// </summary>
        public virtual string ToWarehouseUuid
        {
            get
            {
				return _toWarehouseUuid?.TrimEnd(); 
            }
            set
            {
				_toWarehouseUuid = value.TruncateTo(50); 
				OnPropertyChanged("ToWarehouseUuid", value);
            }
        }

		/// <summary>
		/// Readable warehouse code, transfer to warehouse. <br> Title: Warehouse Code, Display: true, Editable: true
		/// </summary>
        public virtual string ToWarehouseCode
        {
            get
            {
				return _toWarehouseCode?.TrimEnd(); 
            }
            set
            {
				_toWarehouseCode = value.TruncateTo(50); 
				OnPropertyChanged("ToWarehouseCode", value);
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
		/// (Readonly) WarehouseTransfer created from other entity number, use to prevent import duplicate WarehouseTransfer. <br> Title: Source Number, Display: false, Editable: false
		/// </summary>
        public virtual string WarehouseTransferSourceCode
        {
            get
            {
				return _warehouseTransferSourceCode?.TrimEnd(); 
            }
            set
            {
				_warehouseTransferSourceCode = value.TruncateTo(100); 
				OnPropertyChanged("WarehouseTransferSourceCode", value);
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
		/// (Readonly) User who created this WarehouseTransfer. <br> Title: Created By, Display: true, Editable: false
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
		private WarehouseTransferData Parent { get; set; }
		public WarehouseTransferData GetParent() => Parent;
		public WarehouseTransferHeader SetParent(WarehouseTransferData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			WarehouseTransferUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override WarehouseTransferHeader Clear()
        {
            base.Clear();
			_databaseNum = default(int); 
			_masterAccountNum = default(int); 
			_profileNum = default(int); 
			_warehouseTransferUuid = String.Empty; 
			_batchNumber = String.Empty; 
			_transferStatus = default(int); 
			_warehouseTransferType = default(int); 
			_warehouseTransferStatus = default(int); 
			_transferDate = new DateTime().MinValueSql(); 
			_transferTime = new TimeSpan().MinValueSql(); 
			_processor = String.Empty; 
			_receiveDate = new DateTime().MinValueSql(); 
			_receiveTime = new TimeSpan().MinValueSql(); 
			_receiveProcessor = String.Empty; 
			_fromWarehouseUuid = String.Empty; 
			_fromWarehouseCode = String.Empty; 
			_toWarehouseUuid = String.Empty; 
			_toWarehouseCode = String.Empty; 
			_referenceType = default(int); 
			_referenceUuid = String.Empty; 
			_referenceNum = String.Empty; 
			_warehouseTransferSourceCode = String.Empty; 
			_updateDateUtc = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_enterBy = String.Empty; 
			_updateBy = String.Empty; 
            ClearChildren();
            return this;
        }

        public override WarehouseTransferHeader CheckIntegrity()
        {
            CheckUniqueId();
            CheckIntegrityOthers();
            return this;
        }

        public virtual WarehouseTransferHeader ClearChildren()
        {
            return this;
        }

        public virtual WarehouseTransferHeader NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(WarehouseTransferHeader data)
        {
            if (data is null) return;
            return;
        }


		public override WarehouseTransferHeader ConvertDbFieldsToData()
		{
			base.ConvertDbFieldsToData();
			return this;
		}
		public override WarehouseTransferHeader ConvertDataFieldsToDb()
		{
			base.ConvertDataFieldsToDb();
			UpdateDateUtc =DateTime.UtcNow;
			return this;
		}

        #endregion Methods - Generated 
    }
}



