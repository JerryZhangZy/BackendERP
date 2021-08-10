




              

              
    

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
    /// Represents a Customer.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("Customer")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("CustomerUuid")]
    [DtoName("CustomerDto")]
    public partial class Customer : TableRepository<Customer, long>
    {

        public Customer() : base() {}
        public Customer(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("DatabaseNum",SqlDbType.Int,NotNull=true)]
        private int _databaseNum;

        [Column("MasterAccountNum",SqlDbType.Int,NotNull=true)]
        private int _masterAccountNum;

        [Column("ProfileNum",SqlDbType.Int,NotNull=true)]
        private int _profileNum;

        [Column("Digit_seller_id",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _digit_seller_id;

        [Column("CustomerUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _customerUuid;

        [Column("CustomerCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _customerCode;

        [Column("CustomerName",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _customerName;

        [Column("Contact",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _contact;

        [Column("Contact2",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _contact2;

        [Column("Contact3",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _contact3;

        [Column("Phone1",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _phone1;

        [Column("Phone2",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _phone2;

        [Column("Phone3",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _phone3;

        [Column("Phone4",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _phone4;

        [Column("Email",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _email;

        [Column("WebSite",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _webSite;

        [Column("CustomerType",SqlDbType.Int,IsDefault=true)]
        private int? _customerType;

        [Column("CustomerStatus",SqlDbType.Int,IsDefault=true)]
        private int? _customerStatus;

        [Column("BusinessType",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _businessType;

        [Column("PriceRule",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _priceRule;

        [Column("FirstDate",SqlDbType.Date,NotNull=true)]
        private DateTime _firstDate;

        [Column("Currency",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _currency;

        [Column("CreditLimit",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _creditLimit;

        [Column("TaxRate",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _taxRate;

        [Column("DiscountRate",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _discountRate;

        [Column("ShippingCarrier",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _shippingCarrier;

        [Column("ShippingClass",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _shippingClass;

        [Column("ShippingAccount",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _shippingAccount;

        [Column("Priority",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _priority;

        [Column("Area",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _area;

        [Column("Region",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _region;

        [Column("Districtn",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _districtn;

        [Column("Zone",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _zone;

        [Column("TaxId",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _taxId;

        [Column("ResaleLicense",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _resaleLicense;

        [Column("ClassCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _classCode;

        [Column("DepartmentCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _departmentCode;

        [Column("DivisionCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _divisionCode;

        [Column("SourceCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _sourceCode;

        [Column("Terms",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _terms;

        [Column("TermsDays",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _termsDays;

        [Column("UpdateDateUtc",SqlDbType.DateTime)]
        private DateTime? _updateDateUtc;

        [Column("EnterBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _enterBy;

        [Column("UpdateBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _updateBy;

        #endregion Fields - Generated 

        #region Properties - Generated 
		[IgnoreCompare] 
		public override string UniqueId => CustomerUuid; 
		public void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(CustomerUuid)) 
				CustomerUuid = Guid.NewGuid().ToString(); 
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
		/// Digit bridge seller_id. <br> Title: Digit Seller Id, Display: true, Editable: true.
		/// </summary>
        public virtual string Digit_seller_id
        {
            get
            {
				return _digit_seller_id?.TrimEnd(); 
            }
            set
            {
				_digit_seller_id = value.TruncateTo(50); 
				OnPropertyChanged("Digit_seller_id", value);
            }
        }

		/// <summary>
		/// Customer uuid. <br> Display: false, Editable: false.
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
		/// Readable customer number, unique in same database and profile. <br> Parameter should pass ProfileNum-CustomerCode. <br> Title: Customer Number, Display: true, Editable: true
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
		/// Customer name. <br> Title: Name, Display: true, Editable: true
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
		/// Customer contact person. <br> Title: Contact, Display: true, Editable: true
		/// </summary>
        public virtual string Contact
        {
            get
            {
				return _contact?.TrimEnd(); 
            }
            set
            {
				_contact = value.TruncateTo(200); 
				OnPropertyChanged("Contact", value);
            }
        }

		/// <summary>
		/// Customer contact person 2. <br> Title: Contact 2, Display: true, Editable: true
		/// </summary>
        public virtual string Contact2
        {
            get
            {
				return _contact2?.TrimEnd(); 
            }
            set
            {
				_contact2 = value.TruncateTo(200); 
				OnPropertyChanged("Contact2", value);
            }
        }

		/// <summary>
		/// Customer contact person 3. <br> Title: Contact 3, Display: true, Editable: true
		/// </summary>
        public virtual string Contact3
        {
            get
            {
				return _contact3?.TrimEnd(); 
            }
            set
            {
				_contact3 = value.TruncateTo(200); 
				OnPropertyChanged("Contact3", value);
            }
        }

		/// <summary>
		/// Customer phone 1. <br> Title: Phone, Display: true, Editable: true
		/// </summary>
        public virtual string Phone1
        {
            get
            {
				return _phone1?.TrimEnd(); 
            }
            set
            {
				_phone1 = value.TruncateTo(50); 
				OnPropertyChanged("Phone1", value);
            }
        }

		/// <summary>
		/// Customer phone 2. <br> Title: Phone 2, Display: true, Editable: true
		/// </summary>
        public virtual string Phone2
        {
            get
            {
				return _phone2?.TrimEnd(); 
            }
            set
            {
				_phone2 = value.TruncateTo(50); 
				OnPropertyChanged("Phone2", value);
            }
        }

		/// <summary>
		/// Customer phone 3. <br> Title: Phone 3, Display: true, Editable: true
		/// </summary>
        public virtual string Phone3
        {
            get
            {
				return _phone3?.TrimEnd(); 
            }
            set
            {
				_phone3 = value.TruncateTo(50); 
				OnPropertyChanged("Phone3", value);
            }
        }

		/// <summary>
		/// Customer phone 4. <br> Title: Fax, Display: true, Editable: true
		/// </summary>
        public virtual string Phone4
        {
            get
            {
				return _phone4?.TrimEnd(); 
            }
            set
            {
				_phone4 = value.TruncateTo(50); 
				OnPropertyChanged("Phone4", value);
            }
        }

		/// <summary>
		/// Customer email. <br> Title: Email, Display: true, Editable: true
		/// </summary>
        public virtual string Email
        {
            get
            {
				return _email?.TrimEnd(); 
            }
            set
            {
				_email = value.TruncateTo(200); 
				OnPropertyChanged("Email", value);
            }
        }

		/// <summary>
		/// Customer WebSite. <br> Title: WebSite, Display: true, Editable: true
		/// </summary>
        public virtual string WebSite
        {
            get
            {
				return _webSite?.TrimEnd(); 
            }
            set
            {
				_webSite = value.TruncateTo(200); 
				OnPropertyChanged("WebSite", value);
            }
        }

		/// <summary>
		/// Customer type. <br> Title: Type, Display: true, Editable: true
		/// </summary>
        public virtual int? CustomerType
        {
            get
            {
				if (!AllowNull && _customerType is null) 
					_customerType = default(int); 
				return _customerType; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_customerType = value; 
					OnPropertyChanged("CustomerType", value);
				}
            }
        }

		/// <summary>
		/// Customer status. <br> Title: Status, Display: true, Editable: true
		/// </summary>
        public virtual int? CustomerStatus
        {
            get
            {
				if (!AllowNull && _customerStatus is null) 
					_customerStatus = default(int); 
				return _customerStatus; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_customerStatus = value; 
					OnPropertyChanged("CustomerStatus", value);
				}
            }
        }

		/// <summary>
		/// Customer business type. <br> Title: Business Type, Display: true, Editable: true
		/// </summary>
        public virtual string BusinessType
        {
            get
            {
				return _businessType?.TrimEnd(); 
            }
            set
            {
				_businessType = value.TruncateTo(50); 
				OnPropertyChanged("BusinessType", value);
            }
        }

		/// <summary>
		/// Customer default price rule. <br> Title: Price Rule, Display: true, Editable: true
		/// </summary>
        public virtual string PriceRule
        {
            get
            {
				return _priceRule?.TrimEnd(); 
            }
            set
            {
				_priceRule = value.TruncateTo(50); 
				OnPropertyChanged("PriceRule", value);
            }
        }

		/// <summary>
		/// Customer create date. <br> Title: Since, Display: true, Editable: true
		/// </summary>
        public virtual DateTime FirstDate
        {
            get
            {
				return _firstDate; 
            }
            set
            {
				_firstDate = value.Date.ToSqlSafeValue(); 
				OnPropertyChanged("FirstDate", value);
            }
        }

		/// <summary>
		/// Customer default Currency. <br> Title: Currency, Display: true, Editable: true
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
		/// Customer Credit Limit. <br> Title: Credit Limit, Display: true, Editable: true
		/// </summary>
        public virtual decimal CreditLimit
        {
            get
            {
				return _creditLimit; 
            }
            set
            {
				_creditLimit = value; 
				OnPropertyChanged("CreditLimit", value);
            }
        }

		/// <summary>
		/// Default Tax rate. <br> Title: Tax Rate, Display: true, Editable: true
		/// </summary>
        public virtual decimal? TaxRate
        {
            get
            {
				if (!AllowNull && _taxRate is null) 
					_taxRate = default(decimal); 
				return _taxRate; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_taxRate = value; 
					OnPropertyChanged("TaxRate", value);
				}
            }
        }

		/// <summary>
		/// Customer default discount rate. <br> Title: Discount Rate, Display: true, Editable: true
		/// </summary>
        public virtual decimal? DiscountRate
        {
            get
            {
				if (!AllowNull && _discountRate is null) 
					_discountRate = default(decimal); 
				return _discountRate; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_discountRate = value; 
					OnPropertyChanged("DiscountRate", value);
				}
            }
        }

		/// <summary>
		/// Customer default ShippingCarrier. <br> Title: Shipping Carrier, Display: true, Editable: true
		/// </summary>
        public virtual string ShippingCarrier
        {
            get
            {
				return _shippingCarrier?.TrimEnd(); 
            }
            set
            {
				_shippingCarrier = value.TruncateTo(50); 
				OnPropertyChanged("ShippingCarrier", value);
            }
        }

		/// <summary>
		/// Customer default ShippingClass. <br> Title: Shipping Method, Display: true, Editable: true
		/// </summary>
        public virtual string ShippingClass
        {
            get
            {
				return _shippingClass?.TrimEnd(); 
            }
            set
            {
				_shippingClass = value.TruncateTo(50); 
				OnPropertyChanged("ShippingClass", value);
            }
        }

		/// <summary>
		/// Customer default Shipping Account. <br> Title: Shipping Account, Display: true, Editable: true
		/// </summary>
        public virtual string ShippingAccount
        {
            get
            {
				return _shippingAccount?.TrimEnd(); 
            }
            set
            {
				_shippingAccount = value.TruncateTo(50); 
				OnPropertyChanged("ShippingAccount", value);
            }
        }

		/// <summary>
		/// Customer Priority. <br> Title: Priority, Display: true, Editable: true
		/// </summary>
        public virtual string Priority
        {
            get
            {
				return _priority?.TrimEnd(); 
            }
            set
            {
				_priority = value.TruncateTo(10); 
				OnPropertyChanged("Priority", value);
            }
        }

		/// <summary>
		/// Customer Area. <br> Title: Area, Display: true, Editable: true
		/// </summary>
        public virtual string Area
        {
            get
            {
				return _area?.TrimEnd(); 
            }
            set
            {
				_area = value.TruncateTo(20); 
				OnPropertyChanged("Area", value);
            }
        }

		/// <summary>
		/// Customer Region. <br> Title: Region, Display: true, Editable: true
		/// </summary>
        public virtual string Region
        {
            get
            {
				return _region?.TrimEnd(); 
            }
            set
            {
				_region = value.TruncateTo(20); 
				OnPropertyChanged("Region", value);
            }
        }

		/// <summary>
		/// Customer Districtn. <br> Districtn: Area, Display: true, Editable: true
		/// </summary>
        public virtual string Districtn
        {
            get
            {
				return _districtn?.TrimEnd(); 
            }
            set
            {
				_districtn = value.TruncateTo(20); 
				OnPropertyChanged("Districtn", value);
            }
        }

		/// <summary>
		/// Customer Zone. <br> Title: Zone, Display: true, Editable: true
		/// </summary>
        public virtual string Zone
        {
            get
            {
				return _zone?.TrimEnd(); 
            }
            set
            {
				_zone = value.TruncateTo(20); 
				OnPropertyChanged("Zone", value);
            }
        }

		/// <summary>
		/// Customer Tax Id. <br> Title: Tax Id, Display: true, Editable: true
		/// </summary>
        public virtual string TaxId
        {
            get
            {
				return _taxId?.TrimEnd(); 
            }
            set
            {
				_taxId = value.TruncateTo(50); 
				OnPropertyChanged("TaxId", value);
            }
        }

		/// <summary>
		/// Customer Resale License number. <br> Title: Resale License, Display: true, Editable: true
		/// </summary>
        public virtual string ResaleLicense
        {
            get
            {
				return _resaleLicense?.TrimEnd(); 
            }
            set
            {
				_resaleLicense = value.TruncateTo(50); 
				OnPropertyChanged("ResaleLicense", value);
            }
        }

		/// <summary>
		/// Customer Class. <br> Title: Class, Display: true, Editable: true
		/// </summary>
        public virtual string ClassCode
        {
            get
            {
				return _classCode?.TrimEnd(); 
            }
            set
            {
				_classCode = value.TruncateTo(50); 
				OnPropertyChanged("ClassCode", value);
            }
        }

		/// <summary>
		/// Customer Department. <br> Title: Department, Display: true, Editable: true
		/// </summary>
        public virtual string DepartmentCode
        {
            get
            {
				return _departmentCode?.TrimEnd(); 
            }
            set
            {
				_departmentCode = value.TruncateTo(50); 
				OnPropertyChanged("DepartmentCode", value);
            }
        }

		/// <summary>
		/// Customer Division. <br> Title: Division, Display: true, Editable: true
		/// </summary>
        public virtual string DivisionCode
        {
            get
            {
				return _divisionCode?.TrimEnd(); 
            }
            set
            {
				_divisionCode = value.TruncateTo(50); 
				OnPropertyChanged("DivisionCode", value);
            }
        }

		/// <summary>
		/// Customer Source. <br> Title: Source, Display: true, Editable: true
		/// </summary>
        public virtual string SourceCode
        {
            get
            {
				return _sourceCode?.TrimEnd(); 
            }
            set
            {
				_sourceCode = value.TruncateTo(50); 
				OnPropertyChanged("SourceCode", value);
            }
        }

		/// <summary>
		/// Payment terms. <br> Title: Terms, Display: true, Editable: true
		/// </summary>
        public virtual string Terms
        {
            get
            {
				return _terms?.TrimEnd(); 
            }
            set
            {
				_terms = value.TruncateTo(50); 
				OnPropertyChanged("Terms", value);
            }
        }

		/// <summary>
		/// Payment terms days. <br> Title: Days, Display: true, Editable: true
		/// </summary>
        public virtual int TermsDays
        {
            get
            {
				return _termsDays; 
            }
            set
            {
				_termsDays = value; 
				OnPropertyChanged("TermsDays", value);
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
		private CustomerData Parent { get; set; }
		public CustomerData GetParent() => Parent;
		public Customer SetParent(CustomerData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			CustomerUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override Customer Clear()
        {
            base.Clear();
			_databaseNum = default(int); 
			_masterAccountNum = default(int); 
			_profileNum = default(int); 
			_digit_seller_id = String.Empty; 
			_customerUuid = String.Empty; 
			_customerCode = String.Empty; 
			_customerName = String.Empty; 
			_contact = String.Empty; 
			_contact2 = String.Empty; 
			_contact3 = String.Empty; 
			_phone1 = String.Empty; 
			_phone2 = String.Empty; 
			_phone3 = String.Empty; 
			_phone4 = String.Empty; 
			_email = String.Empty; 
			_webSite = String.Empty; 
			_customerType = AllowNull ? (int?)null : default(int); 
			_customerStatus = AllowNull ? (int?)null : default(int); 
			_businessType = String.Empty; 
			_priceRule = String.Empty; 
			_firstDate = new DateTime().MinValueSql(); 
			_currency = String.Empty; 
			_creditLimit = default(decimal); 
			_taxRate = AllowNull ? (decimal?)null : default(decimal); 
			_discountRate = AllowNull ? (decimal?)null : default(decimal); 
			_shippingCarrier = String.Empty; 
			_shippingClass = String.Empty; 
			_shippingAccount = String.Empty; 
			_priority = String.Empty; 
			_area = String.Empty; 
			_region = String.Empty; 
			_districtn = String.Empty; 
			_zone = String.Empty; 
			_taxId = String.Empty; 
			_resaleLicense = String.Empty; 
			_classCode = String.Empty; 
			_departmentCode = String.Empty; 
			_divisionCode = String.Empty; 
			_sourceCode = String.Empty; 
			_terms = String.Empty; 
			_termsDays = default(int); 
			_updateDateUtc = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_enterBy = String.Empty; 
			_updateBy = String.Empty; 
            ClearChildren();
            return this;
        }

        public virtual Customer CheckIntegrity()
        {
            return this;
        }

        public virtual Customer ClearChildren()
        {
            return this;
        }

        public virtual Customer NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(Customer data)
        {
            if (data is null) return;
            return;
        }



        #endregion Methods - Generated 
    }
}



