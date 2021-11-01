              
              
    

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
    /// Represents a MiscInvoiceTransaction.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("MiscInvoiceTransaction")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("TransUuid")]
    [DtoName("MiscInvoiceTransactionDto")]
    public partial class MiscInvoiceTransaction : TableRepository<MiscInvoiceTransaction, long>
    {

        public MiscInvoiceTransaction() : base() {}
        public MiscInvoiceTransaction(IDataBaseFactory dbFactory): base(dbFactory) {}

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

        [Column("MiscInvoiceUuid",SqlDbType.VarChar,NotNull=true)]
        private string _miscInvoiceUuid;

        [Column("MiscInvoiceNumber",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _miscInvoiceNumber;

        [Column("TransType",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _transType;

        [Column("TransStatus",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _transStatus;

        [Column("TransDate",SqlDbType.Date,NotNull=true)]
        private DateTime _transDate;

        [Column("TransTime",SqlDbType.Time,NotNull=true)]
        private TimeSpan _transTime;

        [Column("Description",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _description;

        [Column("Notes",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _notes;

        [Column("PaidBy",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _paidBy;

        [Column("BankAccountUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _bankAccountUuid;

        [Column("BankAccountCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _bankAccountCode;

        [Column("CheckNum",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _checkNum;

        [Column("AuthCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _authCode;

        [Column("Currency",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _currency;

        [Column("TotalAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _totalAmount;

        [Column("CreditAccount",SqlDbType.BigInt,NotNull=true,IsDefault=true)]
        private long _creditAccount;

        [Column("DebitAccount",SqlDbType.BigInt,NotNull=true,IsDefault=true)]
        private long _debitAccount;

        [Column("TransSourceCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _transSourceCode;

        [Column("UpdateDateUtc",SqlDbType.DateTime)]
        private DateTime? _updateDateUtc;

        [Column("EnterBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _enterBy;

        [Column("UpdateBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
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
		/// Invoice Transaction uuid. <br> Display: false, Editable: false.
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
		/// Readable invoice transaction number, unique in same database and profile. <br> Parameter should pass ProfileNum-InvoiceNumber-TransNum. <br> Title: Order Number, Display: true, Editable: true
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
		/// Invoice uuid. <br> Display: false, Editable: false.
		/// </summary>
        public virtual string MiscInvoiceUuid
        {
            get
            {
				return _miscInvoiceUuid?.TrimEnd(); 
            }
            set
            {
				_miscInvoiceUuid = value.TruncateTo(50); 
				OnPropertyChanged("MiscInvoiceUuid", value);
            }
        }

		/// <summary>
		/// Readable invoice number, unique in same database and profile. <br> Parameter should pass ProfileNum-OrderNumber. <br> Title: Order Number, Display: true, Editable: true
		/// </summary>
        public virtual string MiscInvoiceNumber
        {
            get
            {
				return _miscInvoiceNumber?.TrimEnd(); 
            }
            set
            {
				_miscInvoiceNumber = value.TruncateTo(50); 
				OnPropertyChanged("MiscInvoiceNumber", value);
            }
        }

		/// <summary>
		/// Transaction type, payment, return. <br> Title: Type, Display: true, Editable: true
		/// </summary>
        public virtual int TransType
        {
            get
            {
				return _transType; 
            }
            set
            {
				_transType = value; 
				OnPropertyChanged("TransType", value);
            }
        }

		/// <summary>
		/// Transaction status. <br> Title: Status, Display: true, Editable: true
		/// </summary>
        public virtual int TransStatus
        {
            get
            {
				return _transStatus; 
            }
            set
            {
				_transStatus = value; 
				OnPropertyChanged("TransStatus", value);
            }
        }

		/// <summary>
		/// Invoice date. <br> Title: Date, Display: true, Editable: true
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
		/// Invoice time. <br> Title: Time, Display: true, Editable: true
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
		/// Description of Invoice Transaction. <br> Title: Description, Display: true, Editable: true
		/// </summary>
        public virtual string Description
        {
            get
            {
				return _description?.TrimEnd(); 
            }
            set
            {
				_description = value.TruncateTo(100); 
				OnPropertyChanged("Description", value);
            }
        }

		/// <summary>
		/// Notes of Invoice Transaction. <br> Title: Notes, Display: true, Editable: true
		/// </summary>
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

		/// <summary>
		/// Payment method number. <br> Title: Paid By, Display: true, Editable: true
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
		/// Payment bank account uuid.
		/// </summary>
        public virtual string BankAccountUuid
        {
            get
            {
				return _bankAccountUuid?.TrimEnd(); 
            }
            set
            {
				_bankAccountUuid = value.TruncateTo(50); 
				OnPropertyChanged("BankAccountUuid", value);
            }
        }

		/// <summary>
		/// Readable payment Bank account code. <br> Title: Bank, Display: true, Editable: true
		/// </summary>
        public virtual string BankAccountCode
        {
            get
            {
				return _bankAccountCode?.TrimEnd(); 
            }
            set
            {
				_bankAccountCode = value.TruncateTo(50); 
				OnPropertyChanged("BankAccountCode", value);
            }
        }

		/// <summary>
		/// Check number. <br> Title: Check No., Display: true, Editable: true
		/// </summary>
        public virtual string CheckNum
        {
            get
            {
				return _checkNum?.TrimEnd(); 
            }
            set
            {
				_checkNum = value.TruncateTo(100); 
				OnPropertyChanged("CheckNum", value);
            }
        }

		/// <summary>
		/// Auth code from merchant bank. <br> Title: Auth. No., Display: true, Editable: true
		/// </summary>
        public virtual string AuthCode
        {
            get
            {
				return _authCode?.TrimEnd(); 
            }
            set
            {
				_authCode = value.TruncateTo(100); 
				OnPropertyChanged("AuthCode", value);
            }
        }

		/// <summary>
		/// Currency code. <br> Title: Currency, Display: true, Editable: true
		/// </summary>
        public virtual string Currency
        {
            get
            {
				return _currency?.TrimEnd(); 
            }
            set
            {
				_currency = value.TruncateTo(10); 
				OnPropertyChanged("Currency", value);
            }
        }

		/// <summary>
		/// (Readonly) Total amount. Include every charge (tax, shipping, misc...). <br> Title: Total, Display: true, Editable: false
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
		/// G/L Credit account. <br> Display: true, Editable: true
		/// </summary>
        public virtual long CreditAccount
        {
            get
            {
				return _creditAccount; 
            }
            set
            {
				_creditAccount = value; 
				OnPropertyChanged("CreditAccount", value);
            }
        }

		/// <summary>
		/// G/L Debit account. <br> Display: true, Editable: true
		/// </summary>
        public virtual long DebitAccount
        {
            get
            {
				return _debitAccount; 
            }
            set
            {
				_debitAccount = value; 
				OnPropertyChanged("DebitAccount", value);
            }
        }

		/// <summary>
		/// (Readonly) Invoice transaction created from other entity number, use to prevent import duplicate invoice transaction. <br> Title: Source Number, Display: false, Editable: false
		/// </summary>
        public virtual string TransSourceCode
        {
            get
            {
				return _transSourceCode?.TrimEnd(); 
            }
            set
            {
				_transSourceCode = value.TruncateTo(100); 
				OnPropertyChanged("TransSourceCode", value);
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
		/// (Readonly) User who created this transaction. <br> Title: Created By, Display: true, Editable: false
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
		private MiscInvoiceData Parent { get; set; }
		public MiscInvoiceData GetParent() => Parent;
		public MiscInvoiceTransaction SetParent(MiscInvoiceData parent)
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

        public override MiscInvoiceTransaction Clear()
        {
            base.Clear();
			_databaseNum = default(int); 
			_masterAccountNum = default(int); 
			_profileNum = default(int); 
			_transUuid = String.Empty; 
			_transNum = default(int); 
			_miscInvoiceUuid = String.Empty; 
			_miscInvoiceNumber = String.Empty; 
			_transType = default(int); 
			_transStatus = default(int); 
			_transDate = new DateTime().MinValueSql(); 
			_transTime = new TimeSpan().MinValueSql(); 
			_description = String.Empty; 
			_notes = String.Empty; 
			_paidBy = default(int); 
			_bankAccountUuid = String.Empty; 
			_bankAccountCode = String.Empty; 
			_checkNum = String.Empty; 
			_authCode = String.Empty; 
			_currency = String.Empty; 
			_totalAmount = default(decimal); 
			_creditAccount = default(long); 
			_debitAccount = default(long); 
			_transSourceCode = String.Empty; 
			_updateDateUtc = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_enterBy = String.Empty; 
			_updateBy = String.Empty; 
            ClearChildren();
            return this;
        }

        public override MiscInvoiceTransaction CheckIntegrity()
        {
            CheckUniqueId();
            CheckIntegrityOthers();
            return this;
        }

        public virtual MiscInvoiceTransaction ClearChildren()
        {
            return this;
        }

        public virtual MiscInvoiceTransaction NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(MiscInvoiceTransaction data)
        {
            if (data is null) return;
            return;
        }

		public static IList<MiscInvoiceTransaction> FindByMiscInvoiceUuid(IDataBaseFactory dbFactory, string miscInvoiceUuid)
		{
			return dbFactory.Find<MiscInvoiceTransaction>("WHERE MiscInvoiceUuid = @0 ", miscInvoiceUuid).ToList();
		}
		public static long CountByMiscInvoiceUuid(IDataBaseFactory dbFactory, string miscInvoiceUuid)
		{
			return dbFactory.Count<MiscInvoiceTransaction>("WHERE MiscInvoiceUuid = @0 ", miscInvoiceUuid);
		}
		public static async Task<IList<MiscInvoiceTransaction>> FindByAsyncMiscInvoiceUuid(IDataBaseFactory dbFactory, string miscInvoiceUuid)
		{
			return (await dbFactory.FindAsync<MiscInvoiceTransaction>("WHERE MiscInvoiceUuid = @0 ", miscInvoiceUuid)).ToList();
		}
		public static async Task<long> CountByAsyncMiscInvoiceUuid(IDataBaseFactory dbFactory, string miscInvoiceUuid)
		{
			return await dbFactory.CountAsync<MiscInvoiceTransaction>("WHERE MiscInvoiceUuid = @0 ", miscInvoiceUuid);
		}


        #endregion Methods - Generated 
    }
}



