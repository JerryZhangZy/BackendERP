

              
              
    

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
    /// Represents a ApInvoiceHeader.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("ApInvoiceHeader")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("ApInvoiceUuid")]
    [DtoName("ApInvoiceHeaderDto")]
    public partial class ApInvoiceHeader : TableRepository<ApInvoiceHeader, long>
    {

        public ApInvoiceHeader() : base() {}
        public ApInvoiceHeader(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("DatabaseNum",SqlDbType.Int,NotNull=true)]
        private int _databaseNum;

        [Column("MasterAccountNum",SqlDbType.Int,NotNull=true)]
        private int _masterAccountNum;

        [Column("ProfileNum",SqlDbType.Int,NotNull=true)]
        private int _profileNum;

        [Column("ApInvoiceUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _apInvoiceUuid;

        [Column("ApInvoiceNum",SqlDbType.VarChar,NotNull=true)]
        private string _apInvoiceNum;

        [Column("PoUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _poUuid;

        [Column("PoNum",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _poNum;

        [Column("TransUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _transUuid;

        [Column("TransNum",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _transNum;

        [Column("ApInvoiceType",SqlDbType.Int,IsDefault=true)]
        private int? _apInvoiceType;

        [Column("ApInvoiceStatus",SqlDbType.Int,IsDefault=true)]
        private int? _apInvoiceStatus;

        [Column("ApInvoiceDate",SqlDbType.Date,NotNull=true)]
        private DateTime _apInvoiceDate;

        [Column("ApInvoiceTime",SqlDbType.Time,NotNull=true)]
        private TimeSpan _apInvoiceTime;

        [Column("VendorUuid",SqlDbType.VarChar,IsDefault=true)]
        private string _vendorUuid;

        [Column("VendorCode",SqlDbType.VarChar)]
        private string _vendorCode;

        [Column("VendorName",SqlDbType.NVarChar)]
        private string _vendorName;

        [Column("VendorInvoiceNum",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _vendorInvoiceNum;

        [Column("VendorInvoiceDate",SqlDbType.Date)]
        private DateTime? _vendorInvoiceDate;

        [Column("DueDate",SqlDbType.Date)]
        private DateTime? _dueDate;

        [Column("BillDate",SqlDbType.Date)]
        private DateTime? _billDate;

        [Column("Currency",SqlDbType.VarChar)]
        private string _currency;

        [Column("TotalAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _totalAmount;

        [Column("PaidAmount",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _paidAmount;

        [Column("CreditAmount",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _creditAmount;

        [Column("Balance",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _balance;

        [Column("CreditAccount",SqlDbType.BigInt,IsDefault=true)]
        private long? _creditAccount;

        [Column("DebitAccount",SqlDbType.BigInt,IsDefault=true)]
        private long? _debitAccount;

        [Column("UpdateDateUtc",SqlDbType.DateTime)]
        private DateTime? _updateDateUtc;

        [Column("EnterBy",SqlDbType.VarChar,NotNull=true)]
        private string _enterBy;

        [Column("UpdateBy",SqlDbType.VarChar,NotNull=true)]
        private string _updateBy;

        #endregion Fields - Generated 

        #region Properties - Generated 
		[IgnoreCompare] 
		public override string UniqueId => ApInvoiceUuid; 
		public override void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(ApInvoiceUuid)) 
				ApInvoiceUuid = Guid.NewGuid().ToString(); 
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
		/// (Readonly) ApInvoice uuid. <br> Display: false, Editable: false
		/// </summary>
        public virtual string ApInvoiceUuid
        {
            get
            {
				return _apInvoiceUuid?.TrimEnd(); 
            }
            set
            {
				_apInvoiceUuid = value.TruncateTo(50); 
				OnPropertyChanged("ApInvoiceUuid", value);
            }
        }

		/// <summary>
		/// Unique in this database, ProfileNum + ApInvoiceNum is DigitBridgeApInvoiceNum, which is global unique
		/// </summary>
        public virtual string ApInvoiceNum
        {
            get
            {
				return _apInvoiceNum?.TrimEnd(); 
            }
            set
            {
				_apInvoiceNum = value.TruncateTo(50); 
				OnPropertyChanged("ApInvoiceNum", value);
            }
        }

		/// <summary>
		/// Link to PoHeader uuid. <br> Display: false, Editable: false.
		/// </summary>
        public virtual string PoUuid
        {
            get
            {
				return _poUuid?.TrimEnd(); 
            }
            set
            {
				_poUuid = value.TruncateTo(50); 
				OnPropertyChanged("PoUuid", value);
            }
        }

		/// <summary>
		/// Link to PoHeader number, unique in same database and profile. <br /> Title: PoHeader Number, Display: true, Editable: false
		/// </summary>
        public virtual string PoNum
        {
            get
            {
				return _poNum?.TrimEnd(); 
            }
            set
            {
				_poNum = value.TruncateTo(50); 
				OnPropertyChanged("PoNum", value);
            }
        }

		/// <summary>
		/// Link to PoTransaction uuid. <br /> Display: false, Editable: false.
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
		/// Link to PoTransaction number, unique in same database and profile. <br> Title: PoHeader Number, Display: true, Editable: false
		/// </summary>
        public virtual int TransNum
        {
            get
            {
				return _transNum; 
            }
            set
            {
				_transNum = value; 
				OnPropertyChanged("TransNum", value);
            }
        }

		/// <summary>
		/// A/P Invoice type
		/// </summary>
        public virtual int? ApInvoiceType
        {
            get
            {
				if (!AllowNull && _apInvoiceType is null) 
					_apInvoiceType = default(int); 
				return _apInvoiceType; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_apInvoiceType = value; 
					OnPropertyChanged("ApInvoiceType", value);
				}
            }
        }

		/// <summary>
		/// A/P Invoice status
		/// </summary>
        public virtual int? ApInvoiceStatus
        {
            get
            {
				if (!AllowNull && _apInvoiceStatus is null) 
					_apInvoiceStatus = default(int); 
				return _apInvoiceStatus; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_apInvoiceStatus = value; 
					OnPropertyChanged("ApInvoiceStatus", value);
				}
            }
        }

		/// <summary>
		/// A/P Invoice date
		/// </summary>
        public virtual DateTime ApInvoiceDate
        {
            get
            {
				return _apInvoiceDate; 
            }
            set
            {
				_apInvoiceDate = value.Date.ToSqlSafeValue(); 
				OnPropertyChanged("ApInvoiceDate", value);
            }
        }

		/// <summary>
		/// A/P Invoice time
		/// </summary>
        public virtual TimeSpan ApInvoiceTime
        {
            get
            {
				return _apInvoiceTime; 
            }
            set
            {
				_apInvoiceTime = value.ToSqlSafeValue(); 
				OnPropertyChanged("ApInvoiceTime", value);
            }
        }

		/// <summary>
		/// reference Vendor Unique Guid
		/// </summary>
        public virtual string VendorUuid
        {
            get
            {
				if (!AllowNull && _vendorUuid is null) 
					_vendorUuid = String.Empty; 
				return _vendorUuid?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_vendorUuid = value.TruncateTo(50); 
					OnPropertyChanged("VendorUuid", value);
				}
            }
        }

		/// <summary>
		/// Vendor readable number, DatabaseNum + VendorCode is DigitBridgeVendorCode, which is global unique
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
		/// Vendor name
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
		/// Vendor Invoice number
		/// </summary>
        public virtual string VendorInvoiceNum
        {
            get
            {
				return _vendorInvoiceNum?.TrimEnd(); 
            }
            set
            {
				_vendorInvoiceNum = value.TruncateTo(50); 
				OnPropertyChanged("VendorInvoiceNum", value);
            }
        }

		/// <summary>
		/// Vendor Invoice date
		/// </summary>
        public virtual DateTime? VendorInvoiceDate
        {
            get
            {
				if (!AllowNull && _vendorInvoiceDate is null) 
					_vendorInvoiceDate = new DateTime().MinValueSql(); 
				return _vendorInvoiceDate; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_vendorInvoiceDate = (value is null) ? (DateTime?) null : value?.Date.ToSqlSafeValue(); 
					OnPropertyChanged("VendorInvoiceDate", value);
				}
            }
        }

		/// <summary>
		/// Balance Due date
		/// </summary>
        public virtual DateTime? DueDate
        {
            get
            {
				if (!AllowNull && _dueDate is null) 
					_dueDate = new DateTime().MinValueSql(); 
				return _dueDate; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_dueDate = (value is null) ? (DateTime?) null : value?.Date.ToSqlSafeValue(); 
					OnPropertyChanged("DueDate", value);
				}
            }
        }

		/// <summary>
		/// Next Billing date
		/// </summary>
        public virtual DateTime? BillDate
        {
            get
            {
				if (!AllowNull && _billDate is null) 
					_billDate = new DateTime().MinValueSql(); 
				return _billDate; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_billDate = (value is null) ? (DateTime?) null : value?.Date.ToSqlSafeValue(); 
					OnPropertyChanged("BillDate", value);
				}
            }
        }

		/// <summary>
		/// (Ignore) ApInvoice price in currency. <br> Title: Currency, Display: false, Editable: false
		/// </summary>
        public virtual string Currency
        {
            get
            {
				if (!AllowNull && _currency is null) 
					_currency = String.Empty; 
				return _currency?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_currency = value.TruncateTo(10); 
					OnPropertyChanged("Currency", value);
				}
            }
        }

		/// <summary>
		/// Total A/P invoice amount.
		/// </summary>
        public virtual decimal TotalAmount
        {
            get
            {
				return _totalAmount; 
            }
            set
            {
				_totalAmount = value; 
				OnPropertyChanged("TotalAmount", value);
            }
        }

		/// <summary>
		/// Total Paid amount
		/// </summary>
        public virtual decimal? PaidAmount
        {
            get
            {
				if (!AllowNull && _paidAmount is null) 
					_paidAmount = default(decimal); 
				return _paidAmount; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_paidAmount = value; 
					OnPropertyChanged("PaidAmount", value);
				}
            }
        }

		/// <summary>
		/// Total Credit amount
		/// </summary>
        public virtual decimal? CreditAmount
        {
            get
            {
				if (!AllowNull && _creditAmount is null) 
					_creditAmount = default(decimal); 
				return _creditAmount; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_creditAmount = value; 
					OnPropertyChanged("CreditAmount", value);
				}
            }
        }

		/// <summary>
		/// Current balance of invoice
		/// </summary>
        public virtual decimal? Balance
        {
            get
            {
				if (!AllowNull && _balance is null) 
					_balance = default(decimal); 
				return _balance; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_balance = value; 
					OnPropertyChanged("Balance", value);
				}
            }
        }

		/// <summary>
		/// G/L Credit account, A/P invoice total should specify G/L Credit account
		/// </summary>
        public virtual long? CreditAccount
        {
            get
            {
				if (!AllowNull && _creditAccount is null) 
					_creditAccount = default(long); 
				return _creditAccount; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_creditAccount = value; 
					OnPropertyChanged("CreditAccount", value);
				}
            }
        }

		/// <summary>
		/// G/L Debit account
		/// </summary>
        public virtual long? DebitAccount
        {
            get
            {
				if (!AllowNull && _debitAccount is null) 
					_debitAccount = default(long); 
				return _debitAccount; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_debitAccount = value; 
					OnPropertyChanged("DebitAccount", value);
				}
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
		private ApInvoiceData Parent { get; set; }
		public ApInvoiceData GetParent() => Parent;
		public ApInvoiceHeader SetParent(ApInvoiceData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			ApInvoiceUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override ApInvoiceHeader Clear()
        {
            base.Clear();
			_databaseNum = default(int); 
			_masterAccountNum = default(int); 
			_profileNum = default(int); 
			_apInvoiceUuid = String.Empty; 
			_apInvoiceNum = String.Empty; 
			_poUuid = String.Empty; 
			_poNum = String.Empty; 
			_transUuid = String.Empty; 
			_transNum = default(int); 
			_apInvoiceType = AllowNull ? (int?)null : default(int); 
			_apInvoiceStatus = AllowNull ? (int?)null : default(int); 
			_apInvoiceDate = new DateTime().MinValueSql(); 
			_apInvoiceTime = new TimeSpan().MinValueSql(); 
			_vendorUuid = AllowNull ? (string)null : String.Empty; 
			_vendorCode = AllowNull ? (string)null : String.Empty; 
			_vendorName = AllowNull ? (string)null : String.Empty; 
			_vendorInvoiceNum = String.Empty; 
			_vendorInvoiceDate = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_dueDate = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_billDate = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_currency = AllowNull ? (string)null : String.Empty; 
			_totalAmount = default(decimal); 
			_paidAmount = AllowNull ? (decimal?)null : default(decimal); 
			_creditAmount = AllowNull ? (decimal?)null : default(decimal); 
			_balance = AllowNull ? (decimal?)null : default(decimal); 
			_creditAccount = AllowNull ? (long?)null : default(long); 
			_debitAccount = AllowNull ? (long?)null : default(long); 
			_updateDateUtc = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_enterBy = String.Empty; 
			_updateBy = String.Empty; 
            ClearChildren();
            return this;
        }

        public override ApInvoiceHeader CheckIntegrity()
        {
            CheckUniqueId();
            CheckIntegrityOthers();
            return this;
        }

        public virtual ApInvoiceHeader ClearChildren()
        {
            return this;
        }

        public virtual ApInvoiceHeader NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(ApInvoiceHeader data)
        {
            if (data is null) return;
            return;
        }


		public override ApInvoiceHeader ConvertDbFieldsToData()
		{
			base.ConvertDbFieldsToData();
			return this;
		}
		public override ApInvoiceHeader ConvertDataFieldsToDb()
		{
			base.ConvertDataFieldsToDb();
			UpdateDateUtc =DateTime.UtcNow;
			return this;
		}

        #endregion Methods - Generated 
    }
}



