

              
              
    

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
using DigitBridge.CommerceCentral.ERPDb;

namespace DigitBridge.QuickBooks.Integration
{
    /// <summary>
    /// Represents a QuickBooksChnlAccSetting.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("QuickBooksChnlAccSetting")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("ChnlAccSettingUuid")]
    [DtoName("QuickBooksChnlAccSettingDto")]
    public partial class QuickBooksChnlAccSetting : TableRepository<QuickBooksChnlAccSetting, long>
    {

        public QuickBooksChnlAccSetting() : base() {}
        public QuickBooksChnlAccSetting(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("DatabaseNum",SqlDbType.Int,NotNull=true)]
        private int _databaseNum;

        [Column("MasterAccountNum",SqlDbType.Int,NotNull=true)]
        private int _masterAccountNum;

        [Column("ProfileNum",SqlDbType.Int,NotNull=true)]
        private int _profileNum;

        [Column("SettingUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _settingUuid;

        [Column("ChnlAccSettingUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _chnlAccSettingUuid;

        [Column("ChannelAccountName",SqlDbType.NVarChar,NotNull=true)]
        private string _channelAccountName;

        [Column("ChannelAccountNum",SqlDbType.Int,NotNull=true)]
        private int _channelAccountNum;

        [Column("JsonFields",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _jsonFields;

        [Column("LastUpdate",SqlDbType.DateTime,IsDefault=true)]
        private DateTime? _lastUpdate;

        [Column("DailySummaryLastExport",SqlDbType.DateTime)]
        private DateTime? _dailySummaryLastExport;

        #endregion Fields - Generated 

        #region Properties - Generated 
		[IgnoreCompare] 
		public override string UniqueId => ChnlAccSettingUuid; 
		public override void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(ChnlAccSettingUuid)) 
				ChnlAccSettingUuid = Guid.NewGuid().ToString(); 
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
		/// Setting uuid. <br> Display: false, Editable: false.
		/// </summary>
        public virtual string SettingUuid
        {
            get
            {
				return _settingUuid?.TrimEnd(); 
            }
            set
            {
				_settingUuid = value.TruncateTo(50); 
				OnPropertyChanged("SettingUuid", value);
            }
        }

		/// <summary>
		/// ChnlAccSetting uuid. <br> Display: false, Editable: false.
		/// </summary>
        public virtual string ChnlAccSettingUuid
        {
            get
            {
				return _chnlAccSettingUuid?.TrimEnd(); 
            }
            set
            {
				_chnlAccSettingUuid = value.TruncateTo(50); 
				OnPropertyChanged("ChnlAccSettingUuid", value);
            }
        }

		/// <summary>
		/// Central Channel Account name, Max 10 chars ( because of qbo doc Number 21 chars restrictions )
		/// </summary>
        public virtual string ChannelAccountName
        {
            get
            {
				return _channelAccountName?.TrimEnd(); 
            }
            set
            {
				_channelAccountName = value.TruncateTo(150); 
				OnPropertyChanged("ChannelAccountName", value);
            }
        }

		/// <summary>
		/// Central Channel Account Number
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
		/// (Ignore) JSON string.
		/// </summary>
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public virtual string JsonFields
        {
            get
            {
				return _jsonFields?.TrimEnd(); 
            }
            set
            {
				_jsonFields = value.TrimEnd(); 
				OnPropertyChanged("JsonFields", value);
            }
        }

		/// <summary>
		/// 
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
		/// Last DateTime that the system exported the orders in this ChnlAcc
		/// </summary>
        public virtual DateTime? DailySummaryLastExport
        {
            get
            {
				if (!AllowNull && _dailySummaryLastExport is null) 
					_dailySummaryLastExport = new DateTime().MinValueSql(); 
				return _dailySummaryLastExport; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_dailySummaryLastExport = (value is null) ? (DateTime?) null : value?.Date.ToSqlSafeValue(); 
					OnPropertyChanged("DailySummaryLastExport", value);
				}
            }
        }


        [JsonIgnore, XmlIgnore, IgnoreCompare]
        protected CustomAttributes _Fields;
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public virtual CustomAttributes Fields
        {
            get
            {
				if (_Fields is null) 
					_Fields = new CustomAttributes(dbFactory, "QuickBooksChnlAccSetting"); 
				return _Fields; 
            }
            set
            {
				_Fields = (value is null) ? new CustomAttributes(dbFactory, "QuickBooksChnlAccSetting") : value; 
            }
        }


        #endregion Properties - Generated 

        #region Methods - Parent

		[JsonIgnore, XmlIgnore, IgnoreCompare]
		private QuickBooksSettingInfoData Parent { get; set; }
		public QuickBooksSettingInfoData GetParent() => Parent;
		public QuickBooksChnlAccSetting SetParent(QuickBooksSettingInfoData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			ChnlAccSettingUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override QuickBooksChnlAccSetting Clear()
        {
            base.Clear();
			_databaseNum = default(int); 
			_masterAccountNum = default(int); 
			_profileNum = default(int); 
			_settingUuid = String.Empty; 
			_chnlAccSettingUuid = String.Empty; 
			_channelAccountName = String.Empty; 
			_channelAccountNum = default(int); 
			_jsonFields = String.Empty; 
			Fields.Clear(); 
			_lastUpdate = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_dailySummaryLastExport = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
            ClearChildren();
            return this;
        }

        public override QuickBooksChnlAccSetting CheckIntegrity()
        {
            CheckUniqueId();
            CheckIntegrityOthers();
            return this;
        }

        public virtual QuickBooksChnlAccSetting ClearChildren()
        {
            return this;
        }

        public virtual QuickBooksChnlAccSetting NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(QuickBooksChnlAccSetting data)
        {
            if (data is null) return;
            return;
        }

        #endregion Methods - Generated 
    }
}



