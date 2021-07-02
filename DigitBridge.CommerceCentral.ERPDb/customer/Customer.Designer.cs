



    

              

              
    

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

        [Column("Digit_seller_id",SqlDbType.VarChar,IsDefault=true)]
        private string _digit_seller_id;

        [Column("CustomerUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _customerUuid;

        [Column("CustomerNum",SqlDbType.VarChar)]
        private string _customerNum;

        [Column("CustomerName",SqlDbType.NVarChar)]
        private string _customerName;

        [Column("Contact",SqlDbType.NVarChar)]
        private string _contact;

        [Column("Phone1",SqlDbType.VarChar)]
        private string _phone1;

        [Column("Phone2",SqlDbType.VarChar)]
        private string _phone2;

        [Column("Phone3",SqlDbType.VarChar)]
        private string _phone3;

        [Column("Phone4",SqlDbType.VarChar)]
        private string _phone4;

        [Column("Email",SqlDbType.VarChar)]
        private string _email;

        [Column("CustomerType",SqlDbType.Int,IsDefault=true)]
        private int? _customerType;

        [Column("CustomerStatus",SqlDbType.Int,IsDefault=true)]
        private int? _customerStatus;

        [Column("BusinessType",SqlDbType.VarChar)]
        private string _businessType;

        [Column("PriceRule",SqlDbType.VarChar)]
        private string _priceRule;

        [Column("FirstDate",SqlDbType.Date,NotNull=true)]
        private DateTime _firstDate;

        [Column("Currency",SqlDbType.VarChar)]
        private string _currency;

        [Column("CreditLimit",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _creditLimit;

        [Column("TaxRate",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _taxRate;

        [Column("DiscountRate",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _discountRate;

        [Column("ShippingCarrier",SqlDbType.VarChar)]
        private string _shippingCarrier;

        [Column("ShippingClass",SqlDbType.VarChar)]
        private string _shippingClass;

        [Column("ShippingAccount",SqlDbType.VarChar)]
        private string _shippingAccount;

        [Column("Priority",SqlDbType.VarChar)]
        private string _priority;

        [Column("Area",SqlDbType.VarChar)]
        private string _area;

        [Column("TaxId",SqlDbType.VarChar)]
        private string _taxId;

        [Column("ResaleLicense",SqlDbType.VarChar)]
        private string _resaleLicense;

        [Column("ClassCode",SqlDbType.VarChar)]
        private string _classCode;

        [Column("DepartmentCode",SqlDbType.VarChar)]
        private string _departmentCode;

        [Column("UpdateDateUtc",SqlDbType.DateTime)]
        private DateTime? _updateDateUtc;

        [Column("EnterBy",SqlDbType.VarChar,NotNull=true)]
        private string _enterBy;

        [Column("UpdateBy",SqlDbType.VarChar,NotNull=true)]
        private string _updateBy;

        #endregion Fields - Generated 

        #region Properties - Generated 
		public override string UniqueId => CustomerUuid; 
		public void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(CustomerUuid)) 
				CustomerUuid = Guid.NewGuid().ToString(); 
		}
        public virtual int DatabaseNum
        {
            get
            {
				return _databaseNum; 
            }
            set
            {
				_databaseNum = value; 
            }
        }

        public virtual int MasterAccountNum
        {
            get
            {
				return _masterAccountNum; 
            }
            set
            {
				_masterAccountNum = value; 
            }
        }

        public virtual int ProfileNum
        {
            get
            {
				return _profileNum; 
            }
            set
            {
				_profileNum = value; 
            }
        }

        public virtual string Digit_seller_id
        {
            get
            {
				if (!AllowNull && _digit_seller_id is null) 
					_digit_seller_id = String.Empty; 
				return _digit_seller_id?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_digit_seller_id = value.TruncateTo(50); 
            }
        }

        public virtual string CustomerUuid
        {
            get
            {
				return _customerUuid?.TrimEnd(); 
            }
            set
            {
				_customerUuid = value.TruncateTo(50); 
            }
        }

        public virtual string CustomerNum
        {
            get
            {
				if (!AllowNull && _customerNum is null) 
					_customerNum = String.Empty; 
				return _customerNum?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_customerNum = value.TruncateTo(50); 
            }
        }

        public virtual string CustomerName
        {
            get
            {
				if (!AllowNull && _customerName is null) 
					_customerName = String.Empty; 
				return _customerName?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_customerName = value.TruncateTo(200); 
            }
        }

        public virtual string Contact
        {
            get
            {
				if (!AllowNull && _contact is null) 
					_contact = String.Empty; 
				return _contact?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_contact = value.TruncateTo(200); 
            }
        }

        public virtual string Phone1
        {
            get
            {
				if (!AllowNull && _phone1 is null) 
					_phone1 = String.Empty; 
				return _phone1?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_phone1 = value.TruncateTo(50); 
            }
        }

        public virtual string Phone2
        {
            get
            {
				if (!AllowNull && _phone2 is null) 
					_phone2 = String.Empty; 
				return _phone2?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_phone2 = value.TruncateTo(50); 
            }
        }

        public virtual string Phone3
        {
            get
            {
				if (!AllowNull && _phone3 is null) 
					_phone3 = String.Empty; 
				return _phone3?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_phone3 = value.TruncateTo(50); 
            }
        }

        public virtual string Phone4
        {
            get
            {
				if (!AllowNull && _phone4 is null) 
					_phone4 = String.Empty; 
				return _phone4?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_phone4 = value.TruncateTo(50); 
            }
        }

        public virtual string Email
        {
            get
            {
				if (!AllowNull && _email is null) 
					_email = String.Empty; 
				return _email?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_email = value.TruncateTo(200); 
            }
        }

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
					_customerType = value; 
            }
        }

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
					_customerStatus = value; 
            }
        }

        public virtual string BusinessType
        {
            get
            {
				if (!AllowNull && _businessType is null) 
					_businessType = String.Empty; 
				return _businessType?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_businessType = value.TruncateTo(50); 
            }
        }

        public virtual string PriceRule
        {
            get
            {
				if (!AllowNull && _priceRule is null) 
					_priceRule = String.Empty; 
				return _priceRule?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_priceRule = value.TruncateTo(50); 
            }
        }

        public virtual DateTime FirstDate
        {
            get
            {
				return _firstDate; 
            }
            set
            {
				_firstDate = value.Date.ToSqlSafeValue(); 
            }
        }

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
					_currency = value.TruncateTo(10); 
            }
        }

        public virtual decimal CreditLimit
        {
            get
            {
				return _creditLimit; 
            }
            set
            {
				_creditLimit = value; 
            }
        }

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
					_taxRate = value; 
            }
        }

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
					_discountRate = value; 
            }
        }

        public virtual string ShippingCarrier
        {
            get
            {
				if (!AllowNull && _shippingCarrier is null) 
					_shippingCarrier = String.Empty; 
				return _shippingCarrier?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_shippingCarrier = value.TruncateTo(50); 
            }
        }

        public virtual string ShippingClass
        {
            get
            {
				if (!AllowNull && _shippingClass is null) 
					_shippingClass = String.Empty; 
				return _shippingClass?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_shippingClass = value.TruncateTo(50); 
            }
        }

        public virtual string ShippingAccount
        {
            get
            {
				if (!AllowNull && _shippingAccount is null) 
					_shippingAccount = String.Empty; 
				return _shippingAccount?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_shippingAccount = value.TruncateTo(50); 
            }
        }

        public virtual string Priority
        {
            get
            {
				if (!AllowNull && _priority is null) 
					_priority = String.Empty; 
				return _priority?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_priority = value.TruncateTo(10); 
            }
        }

        public virtual string Area
        {
            get
            {
				if (!AllowNull && _area is null) 
					_area = String.Empty; 
				return _area?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_area = value.TruncateTo(20); 
            }
        }

        public virtual string TaxId
        {
            get
            {
				if (!AllowNull && _taxId is null) 
					_taxId = String.Empty; 
				return _taxId?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_taxId = value.TruncateTo(50); 
            }
        }

        public virtual string ResaleLicense
        {
            get
            {
				if (!AllowNull && _resaleLicense is null) 
					_resaleLicense = String.Empty; 
				return _resaleLicense?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_resaleLicense = value.TruncateTo(50); 
            }
        }

        public virtual string ClassCode
        {
            get
            {
				if (!AllowNull && _classCode is null) 
					_classCode = String.Empty; 
				return _classCode?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_classCode = value.TruncateTo(50); 
            }
        }

        public virtual string DepartmentCode
        {
            get
            {
				if (!AllowNull && _departmentCode is null) 
					_departmentCode = String.Empty; 
				return _departmentCode?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_departmentCode = value.TruncateTo(50); 
            }
        }

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
					_updateDateUtc = (value is null) ? (DateTime?) null : value?.Date.ToSqlSafeValue(); 
            }
        }

        public virtual string EnterBy
        {
            get
            {
				return _enterBy?.TrimEnd(); 
            }
            set
            {
				_enterBy = value.TruncateTo(100); 
            }
        }

        public virtual string UpdateBy
        {
            get
            {
				return _updateBy?.TrimEnd(); 
            }
            set
            {
				_updateBy = value.TruncateTo(100); 
            }
        }

        #endregion Properties - Generated 

        #region Methods - Parent

		[XmlIgnore, JsonIgnore, IgnoreCompare]
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
			_databaseNum = default(int); 
			_masterAccountNum = default(int); 
			_profileNum = default(int); 
			_digit_seller_id = AllowNull ? (string)null : String.Empty; 
			_customerUuid = String.Empty; 
			_customerNum = AllowNull ? (string)null : String.Empty; 
			_customerName = AllowNull ? (string)null : String.Empty; 
			_contact = AllowNull ? (string)null : String.Empty; 
			_phone1 = AllowNull ? (string)null : String.Empty; 
			_phone2 = AllowNull ? (string)null : String.Empty; 
			_phone3 = AllowNull ? (string)null : String.Empty; 
			_phone4 = AllowNull ? (string)null : String.Empty; 
			_email = AllowNull ? (string)null : String.Empty; 
			_customerType = AllowNull ? (int?)null : default(int); 
			_customerStatus = AllowNull ? (int?)null : default(int); 
			_businessType = AllowNull ? (string)null : String.Empty; 
			_priceRule = AllowNull ? (string)null : String.Empty; 
			_firstDate = new DateTime().MinValueSql(); 
			_currency = AllowNull ? (string)null : String.Empty; 
			_creditLimit = default(decimal); 
			_taxRate = AllowNull ? (decimal?)null : default(decimal); 
			_discountRate = AllowNull ? (decimal?)null : default(decimal); 
			_shippingCarrier = AllowNull ? (string)null : String.Empty; 
			_shippingClass = AllowNull ? (string)null : String.Empty; 
			_shippingAccount = AllowNull ? (string)null : String.Empty; 
			_priority = AllowNull ? (string)null : String.Empty; 
			_area = AllowNull ? (string)null : String.Empty; 
			_taxId = AllowNull ? (string)null : String.Empty; 
			_resaleLicense = AllowNull ? (string)null : String.Empty; 
			_classCode = AllowNull ? (string)null : String.Empty; 
			_departmentCode = AllowNull ? (string)null : String.Empty; 
			_updateDateUtc = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_enterBy = String.Empty; 
			_updateBy = String.Empty; 
            ClearChildren();
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
            return;
        }

        #endregion Methods - Generated 
    }
}



