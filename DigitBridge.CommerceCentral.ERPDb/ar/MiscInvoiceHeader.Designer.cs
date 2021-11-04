

              
              
    

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
    /// Represents a MiscInvoiceHeader.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("MiscInvoiceHeader")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("MiscInvoiceUuid")]
    [DtoName("MiscInvoiceHeaderDto")]
    public partial class MiscInvoiceHeader : TableRepository<MiscInvoiceHeader, long>
    {

        public MiscInvoiceHeader() : base() {}
        public MiscInvoiceHeader(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("DatabaseNum",SqlDbType.Int,NotNull=true)]
        private int _databaseNum;

        [Column("MasterAccountNum",SqlDbType.Int,NotNull=true)]
        private int _masterAccountNum;

        [Column("ProfileNum",SqlDbType.Int,NotNull=true)]
        private int _profileNum;

        [Column("MiscInvoiceUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _miscInvoiceUuid;

        [Column("MiscInvoiceNumber",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _miscInvoiceNumber;

        [Column("QboDocNumber",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _qboDocNumber;

        [Column("MiscInvoiceType",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _miscInvoiceType;

        [Column("MiscInvoiceStatus",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _miscInvoiceStatus;

        [Column("MiscInvoiceDate",SqlDbType.Date,NotNull=true)]
        private DateTime _miscInvoiceDate;

        [Column("MiscInvoiceTime",SqlDbType.Time,NotNull=true)]
        private TimeSpan _miscInvoiceTime;

        [Column("CustomerUuid",SqlDbType.VarChar,NotNull=true)]
        private string _customerUuid;

        [Column("CustomerCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _customerCode;

        [Column("CustomerName",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _customerName;

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

        [Column("PaidAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _paidAmount;

        [Column("CreditAmount",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _creditAmount;

        [Column("Balance",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _balance;

        [Column("InvoiceSourceCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _invoiceSourceCode;

        [Column("UpdateDateUtc",SqlDbType.DateTime)]
        private DateTime? _updateDateUtc;

        [Column("EnterBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _enterBy;

        [Column("UpdateBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _updateBy;

        #endregion Fields - Generated 

        #region Properties - Generated 
		[IgnoreCompare] 
		public override string UniqueId => MiscInvoiceUuid; 
		public override void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(MiscInvoiceUuid)) 
				MiscInvoiceUuid = Guid.NewGuid().ToString(); 
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
		/// Misc.Invoice uuid. <br> Display: false, Editable: false.
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
		/// Readable Misc. invoice number, unique in same database and profile. <br> Parameter should pass ProfileNum-OrderNumber. <br> Title: Order Number, Display: true, Editable: true
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
		/// Readable QboDocNumber, when push record to quickbook update number. <br> when push record to quickbook update number.
		/// </summary>
        public virtual string QboDocNumber
        {
            get
            {
				return _qboDocNumber?.TrimEnd(); 
            }
            set
            {
				_qboDocNumber = value.TruncateTo(50); 
				OnPropertyChanged("QboDocNumber", value);
            }
        }

		/// <summary>
		/// Invoice type. <br> Title: Type, Display: true, Editable: true
		/// </summary>
        public virtual int MiscInvoiceType
        {
            get
            {
				return _miscInvoiceType; 
            }
            set
            {
				_miscInvoiceType = value; 
				OnPropertyChanged("MiscInvoiceType", value);
            }
        }

		/// <summary>
		/// Invoice status. <br> Title: Status, Display: true, Editable: true
		/// </summary>
        public virtual int MiscInvoiceStatus
        {
            get
            {
				return _miscInvoiceStatus; 
            }
            set
            {
				_miscInvoiceStatus = value; 
				OnPropertyChanged("MiscInvoiceStatus", value);
            }
        }

		/// <summary>
		/// Invoice date. <br> Title: Date, Display: true, Editable: true
		/// </summary>
        public virtual DateTime MiscInvoiceDate
        {
            get
            {
				return _miscInvoiceDate; 
            }
            set
            {
				_miscInvoiceDate = value.Date.ToSqlSafeValue(); 
				OnPropertyChanged("MiscInvoiceDate", value);
            }
        }

		/// <summary>
		/// Invoice time. <br> Title: Time, Display: true, Editable: true
		/// </summary>
        public virtual TimeSpan MiscInvoiceTime
        {
            get
            {
				return _miscInvoiceTime; 
            }
            set
            {
				_miscInvoiceTime = value.ToSqlSafeValue(); 
				OnPropertyChanged("MiscInvoiceTime", value);
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
		/// Total Paid amount. <br> Display: true, Editable: false
		/// </summary>
        public virtual decimal PaidAmount
        {
            get
            {
				return _paidAmount; 
            }
            set
            {
				_paidAmount = value; 
				OnPropertyChanged("PaidAmount", value);
            }
        }

		/// <summary>
		/// Total Credit amount. <br> Display: true, Editable: false
		/// </summary>
        public virtual decimal CreditAmount
        {
            get
            {
				return _creditAmount; 
            }
            set
            {
				_creditAmount = value; 
				OnPropertyChanged("CreditAmount", value);
            }
        }

		/// <summary>
		/// Current balance of Invoice. <br> Display: true, Editable: false
		/// </summary>
        public virtual decimal Balance
        {
            get
            {
				return _balance; 
            }
            set
            {
				_balance = value; 
				OnPropertyChanged("Balance", value);
            }
        }

		/// <summary>
		/// (Readonly) Invoice created from other entity number, use to prevent import duplicate invoice. <br> Title: Source Number, Display: false, Editable: false
		/// </summary>
        public virtual string InvoiceSourceCode
        {
            get
            {
				return _invoiceSourceCode?.TrimEnd(); 
            }
            set
            {
				_invoiceSourceCode = value.TruncateTo(100); 
				OnPropertyChanged("InvoiceSourceCode", value);
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
                    _updateDateUtc = (value is null) ? (DateTime?) null : value?.ToSqlSafeValue(); 
					OnPropertyChanged("UpdateDateUtc", value);
				}
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
		private MiscInvoiceData Parent { get; set; }
		public MiscInvoiceData GetParent() => Parent;
		public MiscInvoiceHeader SetParent(MiscInvoiceData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			MiscInvoiceUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override MiscInvoiceHeader Clear()
        {
            base.Clear();
			_databaseNum = default(int); 
			_masterAccountNum = default(int); 
			_profileNum = default(int); 
			_miscInvoiceUuid = String.Empty; 
			_miscInvoiceNumber = String.Empty; 
			_qboDocNumber = String.Empty; 
			_miscInvoiceType = default(int); 
			_miscInvoiceStatus = default(int); 
			_miscInvoiceDate = new DateTime().MinValueSql(); 
			_miscInvoiceTime = new TimeSpan().MinValueSql(); 
			_customerUuid = String.Empty; 
			_customerCode = String.Empty; 
			_customerName = String.Empty; 
			_notes = String.Empty; 
			_paidBy = default(int); 
			_bankAccountUuid = String.Empty; 
			_bankAccountCode = String.Empty; 
			_checkNum = String.Empty; 
			_authCode = String.Empty; 
			_currency = String.Empty; 
			_totalAmount = default(decimal); 
			_paidAmount = default(decimal); 
			_creditAmount = default(decimal); 
			_balance = default(decimal); 
			_invoiceSourceCode = String.Empty; 
			_updateDateUtc = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_enterBy = String.Empty; 
			_updateBy = String.Empty; 
            ClearChildren();
            return this;
        }

        public override MiscInvoiceHeader CheckIntegrity()
        {
            CheckUniqueId();
            CheckIntegrityOthers();
            return this;
        }

        public virtual MiscInvoiceHeader ClearChildren()
        {
            return this;
        }

        public virtual MiscInvoiceHeader NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(MiscInvoiceHeader data)
        {
            if (data is null) return;
            return;
        }



        #endregion Methods - Generated 
    }
}


