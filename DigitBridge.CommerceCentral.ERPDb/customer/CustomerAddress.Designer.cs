
              

              
    

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
    /// Represents a CustomerAddress.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("CustomerAddress")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("AddressUuid")]
    [DtoName("CustomerAddressDto")]
    public partial class CustomerAddress : TableRepository<CustomerAddress, long>
    {

        public CustomerAddress() : base() {}
        public CustomerAddress(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("AddressUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _addressUuid;

        [Column("CustomerUuid",SqlDbType.VarChar,NotNull=true)]
        private string _customerUuid;

        [Column("AddressCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _addressCode;

        [Column("AddressType",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _addressType;

        [Column("Description",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _description;

        [Column("Name",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _name;

        [Column("FirstName",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _firstName;

        [Column("LastName",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _lastName;

        [Column("Suffix",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _suffix;

        [Column("Company",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _company;

        [Column("CompanyJobTitle",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _companyJobTitle;

        [Column("Attention",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _attention;

        [Column("AddressLine1",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _addressLine1;

        [Column("AddressLine2",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _addressLine2;

        [Column("AddressLine3",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _addressLine3;

        [Column("City",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _city;

        [Column("State",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _state;

        [Column("StateFullName",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _stateFullName;

        [Column("PostalCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _postalCode;

        [Column("PostalCodeExt",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _postalCodeExt;

        [Column("County",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _county;

        [Column("Country",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _country;

        [Column("Email",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _email;

        [Column("DaytimePhone",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _daytimePhone;

        [Column("NightPhone",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _nightPhone;

        [Column("UpdateDateUtc",SqlDbType.DateTime)]
        private DateTime? _updateDateUtc;

        [Column("EnterBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _enterBy;

        [Column("UpdateBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _updateBy;

        #endregion Fields - Generated 

        #region Properties - Generated 
		[IgnoreCompare] 
		public override string UniqueId => AddressUuid; 
		public void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(AddressUuid)) 
				AddressUuid = Guid.NewGuid().ToString(); 
		}
		/// <summary>
		/// Customer Address uuid. <br> Display: false, Editable: false.
		/// </summary>
        public virtual string AddressUuid
        {
            get
            {
				return _addressUuid?.TrimEnd(); 
            }
            set
            {
				_addressUuid = value.TruncateTo(50); 
				OnPropertyChanged("AddressUuid", value);
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
		/// Address code, human readable. <br> Title: Address Code, Display: true, Editable: true.
		/// </summary>
        public virtual string AddressCode
        {
            get
            {
				return _addressCode?.TrimEnd(); 
            }
            set
            {
				_addressCode = value.TruncateTo(50); 
				OnPropertyChanged("AddressCode", value);
            }
        }

		/// <summary>
		/// Address type, billing, shipping, store. <br> Title: Address Type, Display: true, Editable: true.
		/// </summary>
        public virtual int AddressType
        {
            get
            {
				return _addressType; 
            }
            set
            {
				_addressType = value; 
				OnPropertyChanged("AddressType", value);
            }
        }

		/// <summary>
		/// Address description. <br> Title: Address Description, Display: true, Editable: true.
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
		/// Name. <br> Title: Name, Display: true, Editable: true.
		/// </summary>
        public virtual string Name
        {
            get
            {
				return _name?.TrimEnd(); 
            }
            set
            {
				_name = value.TruncateTo(100); 
				OnPropertyChanged("Name", value);
            }
        }

		/// <summary>
		/// First Name. <br> Title: First Name, Display: true, Editable: true.
		/// </summary>
        public virtual string FirstName
        {
            get
            {
				return _firstName?.TrimEnd(); 
            }
            set
            {
				_firstName = value.TruncateTo(50); 
				OnPropertyChanged("FirstName", value);
            }
        }

		/// <summary>
		/// Last Name. <br> Title: Last Name, Display: true, Editable: true.
		/// </summary>
        public virtual string LastName
        {
            get
            {
				return _lastName?.TrimEnd(); 
            }
            set
            {
				_lastName = value.TruncateTo(50); 
				OnPropertyChanged("LastName", value);
            }
        }

		/// <summary>
		/// Suffix <br> Title: Suffix, Display: true, Editable: true.
		/// </summary>
        public virtual string Suffix
        {
            get
            {
				return _suffix?.TrimEnd(); 
            }
            set
            {
				_suffix = value.TruncateTo(50); 
				OnPropertyChanged("Suffix", value);
            }
        }

		/// <summary>
		/// Company Name <br> Title: Company, Display: true, Editable: true.
		/// </summary>
        public virtual string Company
        {
            get
            {
				return _company?.TrimEnd(); 
            }
            set
            {
				_company = value.TruncateTo(100); 
				OnPropertyChanged("Company", value);
            }
        }

		/// <summary>
		/// Job Title <br> Title: Job Title, Display: true, Editable: true.
		/// </summary>
        public virtual string CompanyJobTitle
        {
            get
            {
				return _companyJobTitle?.TrimEnd(); 
            }
            set
            {
				_companyJobTitle = value.TruncateTo(100); 
				OnPropertyChanged("CompanyJobTitle", value);
            }
        }

		/// <summary>
		/// Attention <br> Title: Attention, Display: true, Editable: true.
		/// </summary>
        public virtual string Attention
        {
            get
            {
				return _attention?.TrimEnd(); 
            }
            set
            {
				_attention = value.TruncateTo(100); 
				OnPropertyChanged("Attention", value);
            }
        }

		/// <summary>
		/// Address Line 1 <br> Title: Address Line 1, Display: true, Editable: true.
		/// </summary>
        public virtual string AddressLine1
        {
            get
            {
				return _addressLine1?.TrimEnd(); 
            }
            set
            {
				_addressLine1 = value.TruncateTo(200); 
				OnPropertyChanged("AddressLine1", value);
            }
        }

		/// <summary>
		/// Address Line 2 <br> Title: Address Line 2, Display: true, Editable: true.
		/// </summary>
        public virtual string AddressLine2
        {
            get
            {
				return _addressLine2?.TrimEnd(); 
            }
            set
            {
				_addressLine2 = value.TruncateTo(200); 
				OnPropertyChanged("AddressLine2", value);
            }
        }

		/// <summary>
		/// Address Line 3 <br> Title: Address Line 3, Display: true, Editable: true.
		/// </summary>
        public virtual string AddressLine3
        {
            get
            {
				return _addressLine3?.TrimEnd(); 
            }
            set
            {
				_addressLine3 = value.TruncateTo(200); 
				OnPropertyChanged("AddressLine3", value);
            }
        }

		/// <summary>
		/// City <br> Title: City, Display: true, Editable: true.
		/// </summary>
        public virtual string City
        {
            get
            {
				return _city?.TrimEnd(); 
            }
            set
            {
				_city = value.TruncateTo(100); 
				OnPropertyChanged("City", value);
            }
        }

		/// <summary>
		/// State <br> Title: State, Display: true, Editable: true.
		/// </summary>
        public virtual string State
        {
            get
            {
				return _state?.TrimEnd(); 
            }
            set
            {
				_state = value.TruncateTo(50); 
				OnPropertyChanged("State", value);
            }
        }

		/// <summary>
		/// State Full Name <br> Title: State Name, Display: true, Editable: true.
		/// </summary>
        public virtual string StateFullName
        {
            get
            {
				return _stateFullName?.TrimEnd(); 
            }
            set
            {
				_stateFullName = value.TruncateTo(100); 
				OnPropertyChanged("StateFullName", value);
            }
        }

		/// <summary>
		/// PostalCode <br> Title: Postal Code, Display: true, Editable: true.
		/// </summary>
        public virtual string PostalCode
        {
            get
            {
				return _postalCode?.TrimEnd(); 
            }
            set
            {
				_postalCode = value.TruncateTo(50); 
				OnPropertyChanged("PostalCode", value);
            }
        }

		/// <summary>
		/// PostalCodeExt <br> Title: Postal Code Ext., Display: true, Editable: true.
		/// </summary>
        public virtual string PostalCodeExt
        {
            get
            {
				return _postalCodeExt?.TrimEnd(); 
            }
            set
            {
				_postalCodeExt = value.TruncateTo(50); 
				OnPropertyChanged("PostalCodeExt", value);
            }
        }

		/// <summary>
		/// County <br> Title: County, Display: true, Editable: true.
		/// </summary>
        public virtual string County
        {
            get
            {
				return _county?.TrimEnd(); 
            }
            set
            {
				_county = value.TruncateTo(100); 
				OnPropertyChanged("County", value);
            }
        }

		/// <summary>
		/// Country <br> Title: Country, Display: true, Editable: true.
		/// </summary>
        public virtual string Country
        {
            get
            {
				return _country?.TrimEnd(); 
            }
            set
            {
				_country = value.TruncateTo(100); 
				OnPropertyChanged("Country", value);
            }
        }

		/// <summary>
		/// Email <br> Title: Email, Display: true, Editable: true.
		/// </summary>
        public virtual string Email
        {
            get
            {
				return _email?.TrimEnd(); 
            }
            set
            {
				_email = value.TruncateTo(100); 
				OnPropertyChanged("Email", value);
            }
        }

		/// <summary>
		/// DaytimePhone <br> Title: Phone, Display: true, Editable: true.
		/// </summary>
        public virtual string DaytimePhone
        {
            get
            {
				return _daytimePhone?.TrimEnd(); 
            }
            set
            {
				_daytimePhone = value.TruncateTo(50); 
				OnPropertyChanged("DaytimePhone", value);
            }
        }

		/// <summary>
		/// NightPhone <br> Title: Phone 2, Display: true, Editable: true.
		/// </summary>
        public virtual string NightPhone
        {
            get
            {
				return _nightPhone?.TrimEnd(); 
            }
            set
            {
				_nightPhone = value.TruncateTo(50); 
				OnPropertyChanged("NightPhone", value);
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
		public CustomerAddress SetParent(CustomerData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			AddressUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override CustomerAddress Clear()
        {
            base.Clear();
			_addressUuid = String.Empty; 
			_customerUuid = String.Empty; 
			_addressCode = String.Empty; 
			_addressType = default(int); 
			_description = String.Empty; 
			_name = String.Empty; 
			_firstName = String.Empty; 
			_lastName = String.Empty; 
			_suffix = String.Empty; 
			_company = String.Empty; 
			_companyJobTitle = String.Empty; 
			_attention = String.Empty; 
			_addressLine1 = String.Empty; 
			_addressLine2 = String.Empty; 
			_addressLine3 = String.Empty; 
			_city = String.Empty; 
			_state = String.Empty; 
			_stateFullName = String.Empty; 
			_postalCode = String.Empty; 
			_postalCodeExt = String.Empty; 
			_county = String.Empty; 
			_country = String.Empty; 
			_email = String.Empty; 
			_daytimePhone = String.Empty; 
			_nightPhone = String.Empty; 
			_updateDateUtc = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_enterBy = String.Empty; 
			_updateBy = String.Empty; 
            ClearChildren();
            return this;
        }

        public virtual CustomerAddress CheckIntegrity()
        {
            CheckUniqueId();
            return this;
        }

        public virtual CustomerAddress ClearChildren()
        {
            return this;
        }

        public virtual CustomerAddress NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(CustomerAddress data)
        {
            if (data is null) return;
            return;
        }

		public static IList<CustomerAddress> FindByCustomerUuid(IDataBaseFactory dbFactory, string customerUuid)
		{
			return dbFactory.Find<CustomerAddress>("WHERE CustomerUuid = @0 ", customerUuid).ToList();
		}
		public static long CountByCustomerUuid(IDataBaseFactory dbFactory, string customerUuid)
		{
			return dbFactory.Count<CustomerAddress>("WHERE CustomerUuid = @0 ", customerUuid);
		}
		public static async Task<IList<CustomerAddress>> FindByAsyncCustomerUuid(IDataBaseFactory dbFactory, string customerUuid)
		{
			return (await dbFactory.FindAsync<CustomerAddress>("WHERE CustomerUuid = @0 ", customerUuid)).ToList();
		}
		public static async Task<long> CountByAsyncCustomerUuid(IDataBaseFactory dbFactory, string customerUuid)
		{
			return await dbFactory.CountAsync<CustomerAddress>("WHERE CustomerUuid = @0 ", customerUuid);
		}


        #endregion Methods - Generated 
    }
}



