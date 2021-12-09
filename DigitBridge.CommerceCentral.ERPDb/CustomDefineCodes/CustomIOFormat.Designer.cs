

              
              
    

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
    /// Represents a CustomIOFormat.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("CustomIOFormat")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("CustomIOFormatUuid")]
    [DtoName("CustomIOFormatDto")]
    public partial class CustomIOFormat : TableRepository<CustomIOFormat, long>
    {

        public CustomIOFormat() : base() {}
        public CustomIOFormat(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("DatabaseNum",SqlDbType.Int,NotNull=true)]
        private int _databaseNum;

        [Column("MasterAccountNum",SqlDbType.Int,NotNull=true)]
        private int _masterAccountNum;

        [Column("ProfileNum",SqlDbType.Int,NotNull=true)]
        private int _profileNum;

        [Column("CustomIOFormatUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _customIOFormatUuid;

        [Column("FormatType",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _formatType;

        [Column("FormatNumber",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _formatNumber;

        [Column("FormatName",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _formatName;

        [Column("Description",SqlDbType.NVarChar,NotNull=true)]
        private string _description;

        [Column("FormatObject",SqlDbType.VarChar)]
        private string _formatObject;

        [Column("UpdateDateUtc",SqlDbType.DateTime)]
        private DateTime? _updateDateUtc;

        [Column("EnterBy",SqlDbType.VarChar,NotNull=true)]
        private string _enterBy;

        [Column("UpdateBy",SqlDbType.VarChar,NotNull=true)]
        private string _updateBy;

        #endregion Fields - Generated 

        #region Properties - Generated 
		[IgnoreCompare] 
		public override string UniqueId => CustomIOFormatUuid; 
		public override void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(CustomIOFormatUuid)) 
				CustomIOFormatUuid = Guid.NewGuid().ToString(); 
		}
		/// <summary>
		/// Each database has its own default value.
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
		/// 
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
		/// 
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
		/// Global Unique Guid for Code
		/// </summary>
        public virtual string CustomIOFormatUuid
        {
            get
            {
				return _customIOFormatUuid?.TrimEnd(); 
            }
            set
            {
				_customIOFormatUuid = value.TruncateTo(50); 
				OnPropertyChanged("CustomIOFormatUuid", value);
            }
        }

		/// <summary>
		/// Entity type, for example: SalesOrder, Invoice, Inventory
		/// </summary>
        public virtual string FormatType
        {
            get
            {
				return _formatType?.TrimEnd(); 
            }
            set
            {
				_formatType = value.TruncateTo(50); 
				OnPropertyChanged("FormatType", value);
            }
        }

		/// <summary>
		/// Format number,
		/// </summary>
        public virtual int FormatNumber
        {
            get
            {
				return _formatNumber; 
            }
            set
            {
				_formatNumber = value; 
				OnPropertyChanged("FormatNumber", value);
            }
        }

		/// <summary>
		/// Format name,
		/// </summary>
        public virtual string FormatName
        {
            get
            {
				return _formatName?.TrimEnd(); 
            }
            set
            {
				_formatName = value.TruncateTo(50); 
				OnPropertyChanged("FormatName", value);
            }
        }

		/// <summary>
		/// Format description,
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
		/// JSON string, format define
		/// </summary>
        public virtual string FormatObject
        {
            get
            {
				if (!AllowNull && _formatObject is null) 
					_formatObject = String.Empty; 
				return _formatObject?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_formatObject = value.TrimEnd(); 
					OnPropertyChanged("FormatObject", value);
				}
            }
        }

		/// <summary>
		/// 
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
		/// 
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
		/// 
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
		private CustomIOFormatData Parent { get; set; }
		public CustomIOFormatData GetParent() => Parent;
		public CustomIOFormat SetParent(CustomIOFormatData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			CustomIOFormatUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override CustomIOFormat Clear()
        {
            base.Clear();
			_databaseNum = default(int); 
			_masterAccountNum = default(int); 
			_profileNum = default(int); 
			_customIOFormatUuid = String.Empty; 
			_formatType = String.Empty; 
			_formatNumber = default(int); 
			_formatName = String.Empty; 
			_description = String.Empty; 
			_formatObject = AllowNull ? (string)null : String.Empty; 
			_updateDateUtc = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_enterBy = String.Empty; 
			_updateBy = String.Empty; 
            ClearChildren();
            return this;
        }

        public override CustomIOFormat CheckIntegrity()
        {
            CheckUniqueId();
            CheckIntegrityOthers();
            return this;
        }

        public virtual CustomIOFormat ClearChildren()
        {
            return this;
        }

        public virtual CustomIOFormat NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(CustomIOFormat data)
        {
            if (data is null) return;
            return;
        }


		public override CustomIOFormat ConvertDbFieldsToData()
		{
			base.ConvertDbFieldsToData();
			return this;
		}
		public override CustomIOFormat ConvertDataFieldsToDb()
		{
			base.ConvertDataFieldsToDb();
			UpdateDateUtc =DateTime.UtcNow;
			return this;
		}

        #endregion Methods - Generated 
    }
}


