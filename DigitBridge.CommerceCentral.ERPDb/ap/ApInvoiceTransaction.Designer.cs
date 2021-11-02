

              
              
    

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
    /// Represents a ApInvoiceTransaction.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("ApInvoiceTransaction")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("TransUuid")]
    [DtoName("ApInvoiceTransactionDto")]
    public partial class ApInvoiceTransaction : TableRepository<ApInvoiceTransaction, long>
    {

        public ApInvoiceTransaction() : base() {}
        public ApInvoiceTransaction(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("DatabaseNum",SqlDbType.Int,NotNull=true)]
        private int _databaseNum;

        [Column("MasterAccountNum",SqlDbType.Int,NotNull=true)]
        private int _masterAccountNum;

        [Column("ProfileNum",SqlDbType.Int,NotNull=true)]
        private int _profileNum;

        [Column("TransUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _transUuid;

        [Column("TransNum",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _transNum;

        [Column("ApInvoiceUuid",SqlDbType.VarChar,NotNull=true)]
        private string _apInvoiceUuid;

        [Column("ApInvoiceNum",SqlDbType.VarChar,NotNull=true)]
        private string _apInvoiceNum;

        [Column("TransType",SqlDbType.Int,IsDefault=true)]
        private int? _transType;

        [Column("TransStatus",SqlDbType.Int,IsDefault=true)]
        private int? _transStatus;

        [Column("TransDate",SqlDbType.Date,NotNull=true)]
        private DateTime _transDate;

        [Column("TransTime",SqlDbType.Time,NotNull=true)]
        private TimeSpan _transTime;

        [Column("Description",SqlDbType.NVarChar,IsDefault=true)]
        private string _description;

        [Column("Notes",SqlDbType.NVarChar,IsDefault=true)]
        private string _notes;

        [Column("PaidBy",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _paidBy;

        [Column("BankAccountUuid",SqlDbType.VarChar)]
        private string _bankAccountUuid;

        [Column("CheckNum",SqlDbType.VarChar)]
        private string _checkNum;

        [Column("AuthCode",SqlDbType.VarChar)]
        private string _authCode;

        [Column("Currency",SqlDbType.VarChar)]
        private string _currency;

        [Column("ExchangeRate",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _exchangeRate;

        [Column("Amount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _amount;

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
		public override string UniqueId => TransUuid; 
		public override void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(TransUuid)) 
				TransUuid = Guid.NewGuid().ToString(); 
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
		/// Global Unique Guid for ApInvoice Transaction
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
		/// Transaction number
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
		/// Global Unique Guid for ApInvoice
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
		/// Transaction type
		/// </summary>
        public virtual int? TransType
        {
            get
            {
				if (!AllowNull && _transType is null) 
					_transType = default(int); 
				return _transType; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_transType = value; 
					OnPropertyChanged("TransType", value);
				}
            }
        }

		/// <summary>
		/// Transaction status
		/// </summary>
        public virtual int? TransStatus
        {
            get
            {
				if (!AllowNull && _transStatus is null) 
					_transStatus = default(int); 
				return _transStatus; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_transStatus = value; 
					OnPropertyChanged("TransStatus", value);
				}
            }
        }

		/// <summary>
		/// Transaction date
		/// </summary>
        public virtual DateTime TransDate
        {
            get
            {
				return _transDate; 
            }
            set
            {
				_transDate = value.Date.ToSqlSafeValue(); 
				OnPropertyChanged("TransDate", value);
            }
        }

		/// <summary>
		/// Transaction time
		/// </summary>
        public virtual TimeSpan TransTime
        {
            get
            {
				return _transTime; 
            }
            set
            {
				_transTime = value.ToSqlSafeValue(); 
				OnPropertyChanged("TransTime", value);
            }
        }

		/// <summary>
		/// Description of ApInvoice Transaction
		/// </summary>
        public virtual string Description
        {
            get
            {
				if (!AllowNull && _description is null) 
					_description = String.Empty; 
				return _description?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_description = value.TruncateTo(200); 
					OnPropertyChanged("Description", value);
				}
            }
        }

		/// <summary>
		/// Notes of ApInvoice Transaction
		/// </summary>
        public virtual string Notes
        {
            get
            {
				if (!AllowNull && _notes is null) 
					_notes = String.Empty; 
				return _notes?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_notes = value.TruncateTo(500); 
					OnPropertyChanged("Notes", value);
				}
            }
        }

		/// <summary>
		/// Payment method number
		/// </summary>
        public virtual int PaidBy
        {
            get
            {
				return _paidBy; 
            }
            set
            {
				_paidBy = value; 
				OnPropertyChanged("PaidBy", value);
            }
        }

		/// <summary>
		/// Global Unique Guid for Bank account
		/// </summary>
        public virtual string BankAccountUuid
        {
            get
            {
				if (!AllowNull && _bankAccountUuid is null) 
					_bankAccountUuid = String.Empty; 
				return _bankAccountUuid?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_bankAccountUuid = value.TruncateTo(50); 
					OnPropertyChanged("BankAccountUuid", value);
				}
            }
        }

		/// <summary>
		/// Check number
		/// </summary>
        public virtual string CheckNum
        {
            get
            {
				if (!AllowNull && _checkNum is null) 
					_checkNum = String.Empty; 
				return _checkNum?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_checkNum = value.TruncateTo(100); 
					OnPropertyChanged("CheckNum", value);
				}
            }
        }

		/// <summary>
		/// Auth code from merchant bank
		/// </summary>
        public virtual string AuthCode
        {
            get
            {
				if (!AllowNull && _authCode is null) 
					_authCode = String.Empty; 
				return _authCode?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_authCode = value.TruncateTo(100); 
					OnPropertyChanged("AuthCode", value);
				}
            }
        }

		/// <summary>
		/// (Ignore)
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
		/// Realtime Exchange Rate when process transaction
		/// </summary>
        public virtual decimal ExchangeRate
        {
            get
            {
				return _exchangeRate; 
            }
            set
            {
				_exchangeRate = value; 
				OnPropertyChanged("ExchangeRate", value);
            }
        }

		/// <summary>
		/// Total order amount. Include every charge. Related to VAT. For US orders, tax should not be included. Refer to tax info to find more detail. Reference calculation
		/// </summary>
        public virtual decimal Amount
        {
            get
            {
				return _amount; 
            }
            set
            {
				_amount = value; 
				OnPropertyChanged("Amount", value);
            }
        }

		/// <summary>
		/// G/L Credit account
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
		private ApTransactionData Parent { get; set; }
		public ApTransactionData GetParent() => Parent;
		public ApInvoiceTransaction SetParent(ApTransactionData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			TransUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override ApInvoiceTransaction Clear()
        {
            base.Clear();
			_databaseNum = default(int); 
			_masterAccountNum = default(int); 
			_profileNum = default(int); 
			_transUuid = String.Empty; 
			_transNum = default(int); 
			_apInvoiceUuid = String.Empty; 
			_apInvoiceNum = String.Empty; 
			_transType = AllowNull ? (int?)null : default(int); 
			_transStatus = AllowNull ? (int?)null : default(int); 
			_transDate = new DateTime().MinValueSql(); 
			_transTime = new TimeSpan().MinValueSql(); 
			_description = AllowNull ? (string)null : String.Empty; 
			_notes = AllowNull ? (string)null : String.Empty; 
			_paidBy = default(int); 
			_bankAccountUuid = AllowNull ? (string)null : String.Empty; 
			_checkNum = AllowNull ? (string)null : String.Empty; 
			_authCode = AllowNull ? (string)null : String.Empty; 
			_currency = AllowNull ? (string)null : String.Empty; 
			_exchangeRate = default(decimal); 
			_amount = default(decimal); 
			_creditAccount = AllowNull ? (long?)null : default(long); 
			_debitAccount = AllowNull ? (long?)null : default(long); 
			_updateDateUtc = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_enterBy = String.Empty; 
			_updateBy = String.Empty; 
            ClearChildren();
            return this;
        }

        public override ApInvoiceTransaction CheckIntegrity()
        {
            CheckUniqueId();
            CheckIntegrityOthers();
            return this;
        }

        public virtual ApInvoiceTransaction ClearChildren()
        {
            return this;
        }

        public virtual ApInvoiceTransaction NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(ApInvoiceTransaction data)
        {
            if (data is null) return;
            return;
        }

		public static IList<ApInvoiceTransaction> FindByApInvoiceUuid(IDataBaseFactory dbFactory, string apInvoiceUuid)
		{
			return dbFactory.Find<ApInvoiceTransaction>("WHERE ApInvoiceUuid = @0 ", apInvoiceUuid).ToList();
		}
		public static long CountByApInvoiceUuid(IDataBaseFactory dbFactory, string apInvoiceUuid)
		{
			return dbFactory.Count<ApInvoiceTransaction>("WHERE ApInvoiceUuid = @0 ", apInvoiceUuid);
		}
		public static async Task<IList<ApInvoiceTransaction>> FindByAsyncApInvoiceUuid(IDataBaseFactory dbFactory, string apInvoiceUuid)
		{
			return (await dbFactory.FindAsync<ApInvoiceTransaction>("WHERE ApInvoiceUuid = @0 ", apInvoiceUuid)).ToList();
		}
		public static async Task<long> CountByAsyncApInvoiceUuid(IDataBaseFactory dbFactory, string apInvoiceUuid)
		{
			return await dbFactory.CountAsync<ApInvoiceTransaction>("WHERE ApInvoiceUuid = @0 ", apInvoiceUuid);
		}


        #endregion Methods - Generated 
    }
}



