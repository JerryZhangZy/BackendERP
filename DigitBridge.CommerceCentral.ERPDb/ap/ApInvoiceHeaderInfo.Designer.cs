              
              
    

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
    /// Represents a ApInvoiceHeaderInfo.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("ApInvoiceHeaderInfo")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("ApInvoiceUuid")]
    [DtoName("ApInvoiceHeaderInfoDto")]
    public partial class ApInvoiceHeaderInfo : TableRepository<ApInvoiceHeaderInfo, long>
    {

        public ApInvoiceHeaderInfo() : base() {}
        public ApInvoiceHeaderInfo(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("ApInvoiceUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _apInvoiceUuid;

        [Column("PoUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _poUuid;

        [Column("ReceiveUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _receiveUuid;

        [Column("CentralFulfillmentNum",SqlDbType.BigInt)]
        private long? _centralFulfillmentNum;

        [Column("ShippingCarrier",SqlDbType.VarChar)]
        private string _shippingCarrier;

        [Column("ShippingClass",SqlDbType.VarChar)]
        private string _shippingClass;

        [Column("DistributionCenterNum",SqlDbType.Int)]
        private int? _distributionCenterNum;

        [Column("CentralOrderNum",SqlDbType.BigInt)]
        private long? _centralOrderNum;

        [Column("ChannelNum",SqlDbType.Int,NotNull=true)]
        private int _channelNum;

        [Column("ChannelAccountNum",SqlDbType.Int,NotNull=true)]
        private int _channelAccountNum;

        [Column("ChannelOrderID",SqlDbType.VarChar,NotNull=true)]
        private string _channelOrderID;

        [Column("SecondaryChannelOrderID",SqlDbType.VarChar)]
        private string _secondaryChannelOrderID;

        [Column("ShippingAccount",SqlDbType.VarChar)]
        private string _shippingAccount;

        [Column("RefNum",SqlDbType.VarChar)]
        private string _refNum;

        [Column("CustomerPoNum",SqlDbType.VarChar)]
        private string _customerPoNum;

        [Column("BillToName",SqlDbType.NVarChar)]
        private string _billToName;

        [Column("BillToFirstName",SqlDbType.NVarChar)]
        private string _billToFirstName;

        [Column("BillToLastName",SqlDbType.NVarChar)]
        private string _billToLastName;

        [Column("BillToSuffix",SqlDbType.NVarChar)]
        private string _billToSuffix;

        [Column("BillToCompany",SqlDbType.NVarChar)]
        private string _billToCompany;

        [Column("BillToCompanyJobTitle",SqlDbType.NVarChar)]
        private string _billToCompanyJobTitle;

        [Column("BillToAttention",SqlDbType.NVarChar)]
        private string _billToAttention;

        [Column("BillToAddressLine1",SqlDbType.NVarChar)]
        private string _billToAddressLine1;

        [Column("BillToAddressLine2",SqlDbType.NVarChar)]
        private string _billToAddressLine2;

        [Column("BillToAddressLine3",SqlDbType.NVarChar)]
        private string _billToAddressLine3;

        [Column("BillToCity",SqlDbType.NVarChar)]
        private string _billToCity;

        [Column("BillToState",SqlDbType.NVarChar)]
        private string _billToState;

        [Column("BillToStateFullName",SqlDbType.NVarChar)]
        private string _billToStateFullName;

        [Column("BillToPostalCode",SqlDbType.VarChar)]
        private string _billToPostalCode;

        [Column("BillToPostalCodeExt",SqlDbType.VarChar)]
        private string _billToPostalCodeExt;

        [Column("BillToCounty",SqlDbType.NVarChar)]
        private string _billToCounty;

        [Column("BillToCountry",SqlDbType.NVarChar)]
        private string _billToCountry;

        [Column("BillToEmail",SqlDbType.VarChar)]
        private string _billToEmail;

        [Column("BillToDaytimePhone",SqlDbType.VarChar)]
        private string _billToDaytimePhone;

        [Column("BillToNightPhone",SqlDbType.VarChar)]
        private string _billToNightPhone;

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
		/// Global Unique Guid for P/O
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
		/// Global Unique Guid for P/O Receive
		/// </summary>
        public virtual string ReceiveUuid
        {
            get
            {
				return _receiveUuid?.TrimEnd(); 
            }
            set
            {
				_receiveUuid = value.TruncateTo(50); 
				OnPropertyChanged("ReceiveUuid", value);
            }
        }

		/// <summary>
		/// CentralFulfillmentNum of dropship S/O
		/// </summary>
        public virtual long? CentralFulfillmentNum
        {
            get
            {
				if (!AllowNull && _centralFulfillmentNum is null) 
					_centralFulfillmentNum = default(long); 
				return _centralFulfillmentNum; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_centralFulfillmentNum = value; 
					OnPropertyChanged("CentralFulfillmentNum", value);
				}
            }
        }

		/// <summary>
		/// Shipping Carrier. <br> Title: Shipping Carrier: Display: true, Editable: true
		/// </summary>
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
				{
					_shippingCarrier = value.TruncateTo(50); 
					OnPropertyChanged("ShippingCarrier", value);
				}
            }
        }

		/// <summary>
		/// Shipping Method. <br> Title: Shipping Method: Display: true, Editable: true
		/// </summary>
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
				{
					_shippingClass = value.TruncateTo(50); 
					OnPropertyChanged("ShippingClass", value);
				}
            }
        }

		/// <summary>
		/// (Readonly) Original DC number. <br> Title: DC number: Display: false, Editable: false
		/// </summary>
        public virtual int? DistributionCenterNum
        {
            get
            {
				if (!AllowNull && _distributionCenterNum is null) 
					_distributionCenterNum = default(int); 
				return _distributionCenterNum; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_distributionCenterNum = value; 
					OnPropertyChanged("DistributionCenterNum", value);
				}
            }
        }

		/// <summary>
		/// CentralOrderNum is DigitBridgeOrderId, use same DatabaseNum
		/// </summary>
        public virtual long? CentralOrderNum
        {
            get
            {
				if (!AllowNull && _centralOrderNum is null) 
					_centralOrderNum = default(long); 
				return _centralOrderNum; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_centralOrderNum = value; 
					OnPropertyChanged("CentralOrderNum", value);
				}
            }
        }

		/// <summary>
		/// The channel which sells the item. Refer to Master Account Channel Setting
		/// </summary>
        public virtual int ChannelNum
        {
            get
            {
				return _channelNum; 
            }
            set
            {
				_channelNum = value; 
				OnPropertyChanged("ChannelNum", value);
            }
        }

		/// <summary>
		/// The unique number of this profile’s channel account
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
		/// This usually is the marketplace order ID, or merchant PO Number
		/// </summary>
        public virtual string ChannelOrderID
        {
            get
            {
				return _channelOrderID?.TrimEnd(); 
            }
            set
            {
				_channelOrderID = value.TruncateTo(130); 
				OnPropertyChanged("ChannelOrderID", value);
            }
        }

		/// <summary>
		/// Secondary identifier provided by the channel. This is a secondary marketplace-generated Order ID. It is not populated most of the time.
		/// </summary>
        public virtual string SecondaryChannelOrderID
        {
            get
            {
				if (!AllowNull && _secondaryChannelOrderID is null) 
					_secondaryChannelOrderID = String.Empty; 
				return _secondaryChannelOrderID?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_secondaryChannelOrderID = value.TruncateTo(200); 
					OnPropertyChanged("SecondaryChannelOrderID", value);
				}
            }
        }

		/// <summary>
		/// requested Vendor use Account to ship
		/// </summary>
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
				{
					_shippingAccount = value.TruncateTo(100); 
					OnPropertyChanged("ShippingAccount", value);
				}
            }
        }

		/// <summary>
		/// Reference Number
		/// </summary>
        public virtual string RefNum
        {
            get
            {
				if (!AllowNull && _refNum is null) 
					_refNum = String.Empty; 
				return _refNum?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_refNum = value.TruncateTo(100); 
					OnPropertyChanged("RefNum", value);
				}
            }
        }

		/// <summary>
		/// Customer P/O Number
		/// </summary>
        public virtual string CustomerPoNum
        {
            get
            {
				if (!AllowNull && _customerPoNum is null) 
					_customerPoNum = String.Empty; 
				return _customerPoNum?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_customerPoNum = value.TruncateTo(100); 
					OnPropertyChanged("CustomerPoNum", value);
				}
            }
        }

		/// <summary>
		/// Bill to name <br> Title: Bill to name: Display: true, Editable: true
		/// </summary>
        public virtual string BillToName
        {
            get
            {
				if (!AllowNull && _billToName is null) 
					_billToName = String.Empty; 
				return _billToName?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_billToName = value.TruncateTo(100); 
					OnPropertyChanged("BillToName", value);
				}
            }
        }

		/// <summary>
		/// (Ignore)
		/// </summary>
        public virtual string BillToFirstName
        {
            get
            {
				if (!AllowNull && _billToFirstName is null) 
					_billToFirstName = String.Empty; 
				return _billToFirstName?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_billToFirstName = value.TruncateTo(50); 
					OnPropertyChanged("BillToFirstName", value);
				}
            }
        }

		/// <summary>
		/// (Ignore)
		/// </summary>
        public virtual string BillToLastName
        {
            get
            {
				if (!AllowNull && _billToLastName is null) 
					_billToLastName = String.Empty; 
				return _billToLastName?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_billToLastName = value.TruncateTo(50); 
					OnPropertyChanged("BillToLastName", value);
				}
            }
        }

		/// <summary>
		/// (Ignore)
		/// </summary>
        public virtual string BillToSuffix
        {
            get
            {
				if (!AllowNull && _billToSuffix is null) 
					_billToSuffix = String.Empty; 
				return _billToSuffix?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_billToSuffix = value.TruncateTo(50); 
					OnPropertyChanged("BillToSuffix", value);
				}
            }
        }

		/// <summary>
		/// Bill to company name. <br> Title: Bill to company: Display: true, Editable: true
		/// </summary>
        public virtual string BillToCompany
        {
            get
            {
				if (!AllowNull && _billToCompany is null) 
					_billToCompany = String.Empty; 
				return _billToCompany?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_billToCompany = value.TruncateTo(100); 
					OnPropertyChanged("BillToCompany", value);
				}
            }
        }

		/// <summary>
		/// (Ignore)
		/// </summary>
        public virtual string BillToCompanyJobTitle
        {
            get
            {
				if (!AllowNull && _billToCompanyJobTitle is null) 
					_billToCompanyJobTitle = String.Empty; 
				return _billToCompanyJobTitle?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_billToCompanyJobTitle = value.TruncateTo(100); 
					OnPropertyChanged("BillToCompanyJobTitle", value);
				}
            }
        }

		/// <summary>
		/// Bill to contact <br> Title: Bill to contact: Display: true, Editable: true
		/// </summary>
        public virtual string BillToAttention
        {
            get
            {
				if (!AllowNull && _billToAttention is null) 
					_billToAttention = String.Empty; 
				return _billToAttention?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_billToAttention = value.TruncateTo(100); 
					OnPropertyChanged("BillToAttention", value);
				}
            }
        }

		/// <summary>
		/// Bill to address 1 <br> Title: Bill to address 1: Display: true, Editable: true
		/// </summary>
        public virtual string BillToAddressLine1
        {
            get
            {
				if (!AllowNull && _billToAddressLine1 is null) 
					_billToAddressLine1 = String.Empty; 
				return _billToAddressLine1?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_billToAddressLine1 = value.TruncateTo(200); 
					OnPropertyChanged("BillToAddressLine1", value);
				}
            }
        }

		/// <summary>
		/// Bill to address 2 <br> Title: Bill to address 2: Display: true, Editable: true
		/// </summary>
        public virtual string BillToAddressLine2
        {
            get
            {
				if (!AllowNull && _billToAddressLine2 is null) 
					_billToAddressLine2 = String.Empty; 
				return _billToAddressLine2?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_billToAddressLine2 = value.TruncateTo(200); 
					OnPropertyChanged("BillToAddressLine2", value);
				}
            }
        }

		/// <summary>
		/// Bill to address 3 <br> Title: Bill to address 3: Display: true, Editable: true
		/// </summary>
        public virtual string BillToAddressLine3
        {
            get
            {
				if (!AllowNull && _billToAddressLine3 is null) 
					_billToAddressLine3 = String.Empty; 
				return _billToAddressLine3?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_billToAddressLine3 = value.TruncateTo(200); 
					OnPropertyChanged("BillToAddressLine3", value);
				}
            }
        }

		/// <summary>
		/// Bill to city <br> Title: Bill to city: Display: true, Editable: true
		/// </summary>
        public virtual string BillToCity
        {
            get
            {
				if (!AllowNull && _billToCity is null) 
					_billToCity = String.Empty; 
				return _billToCity?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_billToCity = value.TruncateTo(100); 
					OnPropertyChanged("BillToCity", value);
				}
            }
        }

		/// <summary>
		/// Bill to state <br> Title: Bill to state: Display: true, Editable: true
		/// </summary>
        public virtual string BillToState
        {
            get
            {
				if (!AllowNull && _billToState is null) 
					_billToState = String.Empty; 
				return _billToState?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_billToState = value.TruncateTo(50); 
					OnPropertyChanged("BillToState", value);
				}
            }
        }

		/// <summary>
		/// (Ignore)
		/// </summary>
        public virtual string BillToStateFullName
        {
            get
            {
				if (!AllowNull && _billToStateFullName is null) 
					_billToStateFullName = String.Empty; 
				return _billToStateFullName?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_billToStateFullName = value.TruncateTo(100); 
					OnPropertyChanged("BillToStateFullName", value);
				}
            }
        }

		/// <summary>
		/// Bill to zip code <br> Title: Bill to zip Display: true, Editable: true
		/// </summary>
        public virtual string BillToPostalCode
        {
            get
            {
				if (!AllowNull && _billToPostalCode is null) 
					_billToPostalCode = String.Empty; 
				return _billToPostalCode?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_billToPostalCode = value.TruncateTo(50); 
					OnPropertyChanged("BillToPostalCode", value);
				}
            }
        }

		/// <summary>
		/// (Ignore)
		/// </summary>
        public virtual string BillToPostalCodeExt
        {
            get
            {
				if (!AllowNull && _billToPostalCodeExt is null) 
					_billToPostalCodeExt = String.Empty; 
				return _billToPostalCodeExt?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_billToPostalCodeExt = value.TruncateTo(50); 
					OnPropertyChanged("BillToPostalCodeExt", value);
				}
            }
        }

		/// <summary>
		/// Bill to county <br> Title: Bill to county: Display: true, Editable: true
		/// </summary>
        public virtual string BillToCounty
        {
            get
            {
				if (!AllowNull && _billToCounty is null) 
					_billToCounty = String.Empty; 
				return _billToCounty?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_billToCounty = value.TruncateTo(50); 
					OnPropertyChanged("BillToCounty", value);
				}
            }
        }

		/// <summary>
		/// Bill to country <br> Title: Bill to country: Display: true, Editable: true
		/// </summary>
        public virtual string BillToCountry
        {
            get
            {
				if (!AllowNull && _billToCountry is null) 
					_billToCountry = String.Empty; 
				return _billToCountry?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_billToCountry = value.TruncateTo(100); 
					OnPropertyChanged("BillToCountry", value);
				}
            }
        }

		/// <summary>
		/// Bill to email <br> Title: Bill to email: Display: true, Editable: true
		/// </summary>
        public virtual string BillToEmail
        {
            get
            {
				if (!AllowNull && _billToEmail is null) 
					_billToEmail = String.Empty; 
				return _billToEmail?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_billToEmail = value.TruncateTo(100); 
					OnPropertyChanged("BillToEmail", value);
				}
            }
        }

		/// <summary>
		/// Bill to phone <br> Title: Bill to phone: Display: true, Editable: true
		/// </summary>
        public virtual string BillToDaytimePhone
        {
            get
            {
				if (!AllowNull && _billToDaytimePhone is null) 
					_billToDaytimePhone = String.Empty; 
				return _billToDaytimePhone?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_billToDaytimePhone = value.TruncateTo(50); 
					OnPropertyChanged("BillToDaytimePhone", value);
				}
            }
        }

		/// <summary>
		/// (Ignore)
		/// </summary>
        public virtual string BillToNightPhone
        {
            get
            {
				if (!AllowNull && _billToNightPhone is null) 
					_billToNightPhone = String.Empty; 
				return _billToNightPhone?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_billToNightPhone = value.TruncateTo(50); 
					OnPropertyChanged("BillToNightPhone", value);
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
		private ApInvoiceData Parent { get; set; }
		public ApInvoiceData GetParent() => Parent;
		public ApInvoiceHeaderInfo SetParent(ApInvoiceData parent)
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

        public override ApInvoiceHeaderInfo Clear()
        {
            base.Clear();
			_apInvoiceUuid = String.Empty; 
			_poUuid = String.Empty; 
			_receiveUuid = String.Empty; 
			_centralFulfillmentNum = AllowNull ? (long?)null : default(long); 
			_shippingCarrier = AllowNull ? (string)null : String.Empty; 
			_shippingClass = AllowNull ? (string)null : String.Empty; 
			_distributionCenterNum = AllowNull ? (int?)null : default(int); 
			_centralOrderNum = AllowNull ? (long?)null : default(long); 
			_channelNum = default(int); 
			_channelAccountNum = default(int); 
			_channelOrderID = String.Empty; 
			_secondaryChannelOrderID = AllowNull ? (string)null : String.Empty; 
			_shippingAccount = AllowNull ? (string)null : String.Empty; 
			_refNum = AllowNull ? (string)null : String.Empty; 
			_customerPoNum = AllowNull ? (string)null : String.Empty; 
			_billToName = AllowNull ? (string)null : String.Empty; 
			_billToFirstName = AllowNull ? (string)null : String.Empty; 
			_billToLastName = AllowNull ? (string)null : String.Empty; 
			_billToSuffix = AllowNull ? (string)null : String.Empty; 
			_billToCompany = AllowNull ? (string)null : String.Empty; 
			_billToCompanyJobTitle = AllowNull ? (string)null : String.Empty; 
			_billToAttention = AllowNull ? (string)null : String.Empty; 
			_billToAddressLine1 = AllowNull ? (string)null : String.Empty; 
			_billToAddressLine2 = AllowNull ? (string)null : String.Empty; 
			_billToAddressLine3 = AllowNull ? (string)null : String.Empty; 
			_billToCity = AllowNull ? (string)null : String.Empty; 
			_billToState = AllowNull ? (string)null : String.Empty; 
			_billToStateFullName = AllowNull ? (string)null : String.Empty; 
			_billToPostalCode = AllowNull ? (string)null : String.Empty; 
			_billToPostalCodeExt = AllowNull ? (string)null : String.Empty; 
			_billToCounty = AllowNull ? (string)null : String.Empty; 
			_billToCountry = AllowNull ? (string)null : String.Empty; 
			_billToEmail = AllowNull ? (string)null : String.Empty; 
			_billToDaytimePhone = AllowNull ? (string)null : String.Empty; 
			_billToNightPhone = AllowNull ? (string)null : String.Empty; 
			_updateDateUtc = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_enterBy = String.Empty; 
			_updateBy = String.Empty; 
            ClearChildren();
            return this;
        }

        public override ApInvoiceHeaderInfo CheckIntegrity()
        {
            CheckUniqueId();
            CheckIntegrityOthers();
            return this;
        }

        public virtual ApInvoiceHeaderInfo ClearChildren()
        {
            return this;
        }

        public virtual ApInvoiceHeaderInfo NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(ApInvoiceHeaderInfo data)
        {
            if (data is null) return;
            return;
        }



        #endregion Methods - Generated 
    }
}



