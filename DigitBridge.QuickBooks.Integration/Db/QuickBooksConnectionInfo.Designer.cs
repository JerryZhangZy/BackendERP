

              
              
    

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

namespace DigitBridge.QuickBooks.Integration
{
    /// <summary>
    /// Represents a QuickBooksConnectionInfo.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("QuickBooksConnectionInfo")]
    [PrimaryKey("ConnectionProfileNum", AutoIncrement = true)]
    [UniqueId("ConnectionUuid")]
    [DtoName("QuickBooksConnectionInfoDto")]
    public partial class QuickBooksConnectionInfo : TableRepository<QuickBooksConnectionInfo, long>
    {

        public QuickBooksConnectionInfo() : base() {}
        public QuickBooksConnectionInfo(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("MasterAccountNum",SqlDbType.Int,NotNull=true)]
        private int _masterAccountNum;

        [Column("ProfileNum",SqlDbType.Int,NotNull=true)]
        private int _profileNum;

        [Column("RequestState",SqlDbType.NVarChar)]
        private string _requestState;

        [Column("QboOAuthTokenStatus",SqlDbType.Int,IsDefault=true)]
        private int? _qboOAuthTokenStatus;

        [Column("LastRefreshTokUpdate",SqlDbType.DateTime,IsDefault=true)]
        private DateTime? _lastRefreshTokUpdate;

        [Column("LastAccessTokUpdate",SqlDbType.DateTime,IsDefault=true)]
        private DateTime? _lastAccessTokUpdate;

        [Column("EnterDate",SqlDbType.DateTime,IsDefault=true)]
        private DateTime? _enterDate;

        [Column("LastUpdate",SqlDbType.DateTime)]
        private DateTime? _lastUpdate;

        [Column("DatabaseNum",SqlDbType.Int,NotNull=true)]
        private int _databaseNum;

        [Column("ConnectionUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _connectionUuid;

        #endregion Fields - Generated 

        #region Properties - Generated 
		[IgnoreCompare] 
		public override string UniqueId => ConnectionUuid; 
		public override void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(ConnectionUuid)) 
				ConnectionUuid = Guid.NewGuid().ToString(); 
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
		/// RequestState
		/// </summary>
        public virtual string RequestState
        {
            get
            {
				if (!AllowNull && _requestState is null) 
					_requestState = String.Empty; 
				return _requestState?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_requestState = value.TruncateTo(200); 
					OnPropertyChanged("RequestState", value);
				}
            }
        }

		/// <summary>
		/// 0: Uninitiated, 1: Success 2: Error
		/// </summary>
        public virtual int? QboOAuthTokenStatus
        {
            get
            {
				if (!AllowNull && _qboOAuthTokenStatus is null) 
					_qboOAuthTokenStatus = default(int); 
				return _qboOAuthTokenStatus; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_qboOAuthTokenStatus = value; 
					OnPropertyChanged("QboOAuthTokenStatus", value);
				}
            }
        }

		/// <summary>
		/// LastRefreshTokUpdate
		/// </summary>
        public virtual DateTime? LastRefreshTokUpdate
        {
            get
            {
				if (!AllowNull && _lastRefreshTokUpdate is null) 
					_lastRefreshTokUpdate = new DateTime().MinValueSql(); 
				return _lastRefreshTokUpdate; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_lastRefreshTokUpdate = (value is null) ? (DateTime?) null : value?.Date.ToSqlSafeValue(); 
					OnPropertyChanged("LastRefreshTokUpdate", value);
				}
            }
        }

		/// <summary>
		/// LastAccessTokUpdate
		/// </summary>
        public virtual DateTime? LastAccessTokUpdate
        {
            get
            {
				if (!AllowNull && _lastAccessTokUpdate is null) 
					_lastAccessTokUpdate = new DateTime().MinValueSql(); 
				return _lastAccessTokUpdate; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_lastAccessTokUpdate = (value is null) ? (DateTime?) null : value?.Date.ToSqlSafeValue(); 
					OnPropertyChanged("LastAccessTokUpdate", value);
				}
            }
        }

		/// <summary>
		/// (Radonly) Created Date time. <br> Title: Created At, Display: true, Editable: false
		/// </summary>
        public virtual DateTime? EnterDate
        {
            get
            {
				if (!AllowNull && _enterDate is null) 
					_enterDate = new DateTime().MinValueSql(); 
				return _enterDate; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_enterDate = (value is null) ? (DateTime?) null : value?.Date.ToSqlSafeValue(); 
					OnPropertyChanged("EnterDate", value);
				}
            }
        }

		/// <summary>
		/// (Radonly) LastUpdate Date time. <br> Title: Created At, Display: true, Editable: false
		/// </summary>
        public virtual DateTime? LastUpdate
        {
            get
            {
				if (!AllowNull && _lastUpdate is null) 
					_lastUpdate = new DateTime().MinValueSql(); 
				return _lastUpdate; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_lastUpdate = (value is null) ? (DateTime?) null : value?.Date.ToSqlSafeValue(); 
					OnPropertyChanged("LastUpdate", value);
				}
            }
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
		/// Shipment uuid. <br> Display: false, Editable: false.
		/// </summary>
        public virtual string ConnectionUuid
        {
            get
            {
				return _connectionUuid?.TrimEnd(); 
            }
            set
            {
				_connectionUuid = value.TruncateTo(50); 
				OnPropertyChanged("ConnectionUuid", value);
            }
        }



        #endregion Properties - Generated 

        #region Methods - Parent

		[JsonIgnore, XmlIgnore, IgnoreCompare]
		private QuickBooksConnectionInfoData Parent { get; set; }
		public QuickBooksConnectionInfoData GetParent() => Parent;
		public QuickBooksConnectionInfo SetParent(QuickBooksConnectionInfoData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			ConnectionUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override QuickBooksConnectionInfo Clear()
        {
            base.Clear();
			_masterAccountNum = default(int); 
			_profileNum = default(int); 
			_requestState = AllowNull ? (string)null : String.Empty; 
			_qboOAuthTokenStatus = AllowNull ? (int?)null : default(int); 
			_lastRefreshTokUpdate = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_lastAccessTokUpdate = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_enterDate = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_lastUpdate = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_databaseNum = default(int); 
			_connectionUuid = String.Empty; 
            ClearChildren();
            return this;
        }

        public override QuickBooksConnectionInfo CheckIntegrity()
        {
            CheckUniqueId();
            CheckIntegrityOthers();
            return this;
        }

        public virtual QuickBooksConnectionInfo ClearChildren()
        {
            return this;
        }

        public virtual QuickBooksConnectionInfo NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(QuickBooksConnectionInfo data)
        {
            if (data is null) return;
            return;
        }



        #endregion Methods - Generated 
    }
}



