

              
              
    

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
    /// Represents a EventProcessERP.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("EventProcessERP")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("EventUuid")]
    [DtoName("EventProcessERPDto")]
    public partial class EventProcessERP : TableRepository<EventProcessERP, long>
    {

        public EventProcessERP() : base() {}
        public EventProcessERP(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("DatabaseNum",SqlDbType.Int,NotNull=true)]
        private int _databaseNum;

        [Column("MasterAccountNum",SqlDbType.Int,NotNull=true)]
        private int _masterAccountNum;

        [Column("ProfileNum",SqlDbType.Int,NotNull=true)]
        private int _profileNum;

        [Column("EventUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _eventUuid;

        [Column("ChannelNum",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _channelNum;

        [Column("ChannelAccountNum",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _channelAccountNum;

        [Column("ERPEventProcessType",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _eRPEventProcessType;

        [Column("ProcessSource",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _processSource;

        [Column("ProcessUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _processUuid;

        [Column("ProcessData",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _processData;

        [Column("EventMessage",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _eventMessage;

        [Column("ActionStatus",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _actionStatus;

        [Column("ActionDate",SqlDbType.DateTime,NotNull=true,IsDefault=true)]
        private DateTime _actionDate;

        [Column("ProcessStatus",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _processStatus;

        [Column("ProcessDate",SqlDbType.DateTime,NotNull=true,IsDefault=true)]
        private DateTime _processDate;

        [Column("CloseStatus",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _closeStatus;

        [Column("CloseDate",SqlDbType.DateTime,NotNull=true,IsDefault=true)]
        private DateTime _closeDate;

        [Column("LastUpdateDate",SqlDbType.DateTime,NotNull=true,IsDefault=true)]
        private DateTime _lastUpdateDate;

        [Column("UpdateDateUtc",SqlDbType.DateTime,NotNull=true,IsDefault=true)]
        private DateTime _updateDateUtc;

        [Column("EnterBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _enterBy;

        [Column("UpdateBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _updateBy;

        #endregion Fields - Generated 

        #region Properties - Generated 
		[IgnoreCompare] 
		public override string UniqueId => EventUuid; 
		public override void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(EventUuid)) 
				EventUuid = Guid.NewGuid().ToString(); 
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
		/// Event uuid. <br> Display: false, Editable: false.
		/// </summary>
        public virtual string EventUuid
        {
            get
            {
				return _eventUuid?.TrimEnd(); 
            }
            set
            {
				_eventUuid = value.TruncateTo(50); 
				OnPropertyChanged("EventUuid", value);
            }
        }

		/// <summary>
		/// (Readonly) The channel which sells the item. Refer to Master Account Channel Setting. <br> Title: Channel: Display: true, Editable: false
		/// </summary>
        public virtual int ChannelNum
        {
            get
            {
				return _channelNum; 
            }
            set
            {
				_channelNum = value; 
				OnPropertyChanged("ChannelNum", value);
            }
        }

		/// <summary>
		/// (Readonly) The unique number of this profile’s channel account. <br> Title: Shipping Carrier: Display: false, Editable: false
		/// </summary>
        public virtual int ChannelAccountNum
        {
            get
            {
				return _channelAccountNum; 
            }
            set
            {
				_channelAccountNum = value; 
				OnPropertyChanged("ChannelAccountNum", value);
            }
        }

		/// <summary>
		/// ERP Event type. <br> Title: EventType, Display: true, Editable: true
		/// </summary>
        public virtual int ERPEventProcessType
        {
            get
            {
				return _eRPEventProcessType; 
            }
            set
            {
				_eRPEventProcessType = value; 
				OnPropertyChanged("ERPEventProcessType", value);
            }
        }

		/// <summary>
		/// process source.<br> Title:ProcessSource,Display:true,Editable:false
		/// </summary>
        public virtual string ProcessSource
        {
            get
            {
				return _processSource?.TrimEnd(); 
            }
            set
            {
				_processSource = value.TruncateTo(50); 
				OnPropertyChanged("ProcessSource", value);
            }
        }

		/// <summary>
		/// process uuid. <br> Display: false, Editable: false.
		/// </summary>
        public virtual string ProcessUuid
        {
            get
            {
				return _processUuid?.TrimEnd(); 
            }
            set
            {
				_processUuid = value.TruncateTo(50); 
				OnPropertyChanged("ProcessUuid", value);
            }
        }

		/// <summary>
		/// process data. <br> Display: false, Editable: false.
		/// </summary>
        public virtual string ProcessData
        {
            get
            {
				return _processData?.TrimEnd(); 
            }
            set
            {
				_processData = value.TrimEnd(); 
				OnPropertyChanged("ProcessData", value);
            }
        }

		/// <summary>
		/// event message. <br> Display: false, Editable: false.
		/// </summary>
        public virtual string EventMessage
        {
            get
            {
				return _eventMessage?.TrimEnd(); 
            }
            set
            {
				_eventMessage = value.TruncateTo(300); 
				OnPropertyChanged("EventMessage", value);
            }
        }

		/// <summary>
		/// Download Acknowledge Status. <br> Title: Type, Display: true, Editable: false
		/// </summary>
        public virtual int ActionStatus
        {
            get
            {
				return _actionStatus; 
            }
            set
            {
				_actionStatus = value; 
				OnPropertyChanged("ActionStatus", value);
            }
        }

		/// <summary>
		/// Download Acknowledge Date. <br> Title: Type, Display: true, Editable: false
		/// </summary>
        public virtual DateTime ActionDate
        {
            get
            {
				return _actionDate; 
            }
            set
            {
				_actionDate = value.Date.ToSqlSafeValue(); 
				OnPropertyChanged("ActionDate", value);
            }
        }

		/// <summary>
		/// Process Status. <br> Title: Type, Display: true, Editable: false
		/// </summary>
        public virtual int ProcessStatus
        {
            get
            {
				return _processStatus; 
            }
            set
            {
				_processStatus = value; 
				OnPropertyChanged("ProcessStatus", value);
            }
        }

		/// <summary>
		/// Process Date. <br> Title: Type, Display: true, Editable: false
		/// </summary>
        public virtual DateTime ProcessDate
        {
            get
            {
				return _processDate; 
            }
            set
            {
				_processDate = value.Date.ToSqlSafeValue(); 
				OnPropertyChanged("ProcessDate", value);
            }
        }

		/// <summary>
		/// Close Status. <br> Title: Type, Display: true, Editable: false
		/// </summary>
        public virtual int CloseStatus
        {
            get
            {
				return _closeStatus; 
            }
            set
            {
				_closeStatus = value; 
				OnPropertyChanged("CloseStatus", value);
            }
        }

		/// <summary>
		/// Close Date. <br> Title: Type, Display: true, Editable: false
		/// </summary>
        public virtual DateTime CloseDate
        {
            get
            {
				return _closeDate; 
            }
            set
            {
				_closeDate = value.Date.ToSqlSafeValue(); 
				OnPropertyChanged("CloseDate", value);
            }
        }

		/// <summary>
		/// Close Date. <br> Title: Type, Display: true, Editable: false
		/// </summary>
        public virtual DateTime LastUpdateDate
        {
            get
            {
				return _lastUpdateDate; 
            }
            set
            {
				_lastUpdateDate = value.Date.ToSqlSafeValue(); 
				OnPropertyChanged("LastUpdateDate", value);
            }
        }

		/// <summary>
		/// (Readonly) Last update date time. <br> Title: Update At, Display: true, Editable: false
		/// </summary>
        public virtual DateTime UpdateDateUtc
        {
            get
            {
				return _updateDateUtc; 
            }
            set
            {
				_updateDateUtc = value.Date.ToSqlSafeValue(); 
				OnPropertyChanged("UpdateDateUtc", value);
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
		private EventProcessERPData Parent { get; set; }
		public EventProcessERPData GetParent() => Parent;
		public EventProcessERP SetParent(EventProcessERPData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			EventUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override EventProcessERP Clear()
        {
            base.Clear();
			_databaseNum = default(int); 
			_masterAccountNum = default(int); 
			_profileNum = default(int); 
			_eventUuid = String.Empty; 
			_channelNum = default(int); 
			_channelAccountNum = default(int); 
			_eRPEventProcessType = default(int); 
			_processSource = String.Empty; 
			_processUuid = String.Empty; 
			_processData = String.Empty; 
			_eventMessage = String.Empty; 
			_actionStatus = default(int); 
			_actionDate = new DateTime().MinValueSql(); 
			_processStatus = default(int); 
			_processDate = new DateTime().MinValueSql(); 
			_closeStatus = default(int); 
			_closeDate = new DateTime().MinValueSql(); 
			_lastUpdateDate = new DateTime().MinValueSql(); 
			_updateDateUtc = new DateTime().MinValueSql(); 
			_enterBy = String.Empty; 
			_updateBy = String.Empty; 
            ClearChildren();
            return this;
        }

        public override EventProcessERP CheckIntegrity()
        {
            CheckUniqueId();
            CheckIntegrityOthers();
            return this;
        }

        public virtual EventProcessERP ClearChildren()
        {
            return this;
        }

        public virtual EventProcessERP NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(EventProcessERP data)
        {
            if (data is null) return;
            return;
        }



        #endregion Methods - Generated 
    }
}



