
              

              
    

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
    /// Represents a SalesOrderHeaderInfo.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("SalesOrderHeaderInfo")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("SalesOrderUuid")]
    [DtoName("SalesOrderHeaderInfoDto")]
    public partial class SalesOrderHeaderInfo : TableRepository<SalesOrderHeaderInfo, long>
    {

        public SalesOrderHeaderInfo() : base() {}
        public SalesOrderHeaderInfo(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("SalesOrderUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _salesOrderUuid;

        [Column("CentralFulfillmentNum",SqlDbType.BigInt,NotNull=true,IsDefault=true)]
        private long _centralFulfillmentNum;

        [Column("ShippingCarrier",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _shippingCarrier;

        [Column("ShippingClass",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _shippingClass;

        [Column("DistributionCenterNum",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _distributionCenterNum;

        [Column("CentralOrderNum",SqlDbType.BigInt,NotNull=true,IsDefault=true)]
        private long _centralOrderNum;

        [Column("ChannelNum",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _channelNum;

        [Column("ChannelAccountNum",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _channelAccountNum;

        [Column("ChannelOrderID",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _channelOrderID;

        [Column("SecondaryChannelOrderID",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _secondaryChannelOrderID;

        [Column("ShippingAccount",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _shippingAccount;

        [Column("WarehouseUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _warehouseUuid;

        [Column("RefNum",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _refNum;

        [Column("CustomerPoNum",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _customerPoNum;

        [Column("EndBuyerUserID",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _endBuyerUserID;

        [Column("EndBuyerName",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _endBuyerName;

        [Column("EndBuyerEmail",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _endBuyerEmail;

        [Column("ShipToName",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _shipToName;

        [Column("ShipToFirstName",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _shipToFirstName;

        [Column("ShipToLastName",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _shipToLastName;

        [Column("ShipToSuffix",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _shipToSuffix;

        [Column("ShipToCompany",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _shipToCompany;

        [Column("ShipToCompanyJobTitle",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _shipToCompanyJobTitle;

        [Column("ShipToAttention",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _shipToAttention;

        [Column("ShipToAddressLine1",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _shipToAddressLine1;

        [Column("ShipToAddressLine2",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _shipToAddressLine2;

        [Column("ShipToAddressLine3",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _shipToAddressLine3;

        [Column("ShipToCity",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _shipToCity;

        [Column("ShipToState",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _shipToState;

        [Column("ShipToStateFullName",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _shipToStateFullName;

        [Column("ShipToPostalCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _shipToPostalCode;

        [Column("ShipToPostalCodeExt",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _shipToPostalCodeExt;

        [Column("ShipToCounty",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _shipToCounty;

        [Column("ShipToCountry",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _shipToCountry;

        [Column("ShipToEmail",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _shipToEmail;

        [Column("ShipToDaytimePhone",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _shipToDaytimePhone;

        [Column("ShipToNightPhone",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _shipToNightPhone;

        [Column("BillToName",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _billToName;

        [Column("BillToFirstName",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _billToFirstName;

        [Column("BillToLastName",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _billToLastName;

        [Column("BillToSuffix",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _billToSuffix;

        [Column("BillToCompany",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _billToCompany;

        [Column("BillToCompanyJobTitle",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _billToCompanyJobTitle;

        [Column("BillToAttention",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _billToAttention;

        [Column("BillToAddressLine1",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _billToAddressLine1;

        [Column("BillToAddressLine2",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _billToAddressLine2;

        [Column("BillToAddressLine3",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _billToAddressLine3;

        [Column("BillToCity",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _billToCity;

        [Column("BillToState",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _billToState;

        [Column("BillToStateFullName",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _billToStateFullName;

        [Column("BillToPostalCode",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _billToPostalCode;

        [Column("BillToPostalCodeExt",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _billToPostalCodeExt;

        [Column("BillToCounty",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _billToCounty;

        [Column("BillToCountry",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _billToCountry;

        [Column("BillToEmail",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _billToEmail;

        [Column("BillToDaytimePhone",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _billToDaytimePhone;

        [Column("BillToNightPhone",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _billToNightPhone;

        [Column("UpdateDateUtc",SqlDbType.DateTime)]
        private DateTime? _updateDateUtc;

        [Column("EnterBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _enterBy;

        [Column("UpdateBy",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _updateBy;

        #endregion Fields - Generated 

        #region Properties - Generated 
		[IgnoreCompare] 
		public override string UniqueId => SalesOrderUuid; 
		public void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(SalesOrderUuid)) 
				SalesOrderUuid = Guid.NewGuid().ToString(); 
		}
        public virtual string SalesOrderUuid
        {
            get
            {
				return _salesOrderUuid?.TrimEnd(); 
            }
            set
            {
				_salesOrderUuid = value.TruncateTo(50); 
				OnPropertyChanged("SalesOrderUuid", value);
            }
        }

        public virtual long CentralFulfillmentNum
        {
            get
            {
				return _centralFulfillmentNum; 
            }
            set
            {
				_centralFulfillmentNum = value; 
				OnPropertyChanged("CentralFulfillmentNum", value);
            }
        }

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

        public virtual int DistributionCenterNum
        {
            get
            {
				return _distributionCenterNum; 
            }
            set
            {
				_distributionCenterNum = value; 
				OnPropertyChanged("DistributionCenterNum", value);
            }
        }

        public virtual long CentralOrderNum
        {
            get
            {
				return _centralOrderNum; 
            }
            set
            {
				_centralOrderNum = value; 
				OnPropertyChanged("CentralOrderNum", value);
            }
        }

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

        public virtual string SecondaryChannelOrderID
        {
            get
            {
				return _secondaryChannelOrderID?.TrimEnd(); 
            }
            set
            {
				_secondaryChannelOrderID = value.TruncateTo(200); 
				OnPropertyChanged("SecondaryChannelOrderID", value);
            }
        }

        public virtual string ShippingAccount
        {
            get
            {
				return _shippingAccount?.TrimEnd(); 
            }
            set
            {
				_shippingAccount = value.TruncateTo(100); 
				OnPropertyChanged("ShippingAccount", value);
            }
        }

        public virtual string WarehouseUuid
        {
            get
            {
				return _warehouseUuid?.TrimEnd(); 
            }
            set
            {
				_warehouseUuid = value.TruncateTo(50); 
				OnPropertyChanged("WarehouseUuid", value);
            }
        }

        public virtual string RefNum
        {
            get
            {
				return _refNum?.TrimEnd(); 
            }
            set
            {
				_refNum = value.TruncateTo(100); 
				OnPropertyChanged("RefNum", value);
            }
        }

        public virtual string CustomerPoNum
        {
            get
            {
				return _customerPoNum?.TrimEnd(); 
            }
            set
            {
				_customerPoNum = value.TruncateTo(100); 
				OnPropertyChanged("CustomerPoNum", value);
            }
        }

        public virtual string EndBuyerUserID
        {
            get
            {
				return _endBuyerUserID?.TrimEnd(); 
            }
            set
            {
				_endBuyerUserID = value.TruncateTo(255); 
				OnPropertyChanged("EndBuyerUserID", value);
            }
        }

        public virtual string EndBuyerName
        {
            get
            {
				return _endBuyerName?.TrimEnd(); 
            }
            set
            {
				_endBuyerName = value.TruncateTo(255); 
				OnPropertyChanged("EndBuyerName", value);
            }
        }

        public virtual string EndBuyerEmail
        {
            get
            {
				return _endBuyerEmail?.TrimEnd(); 
            }
            set
            {
				_endBuyerEmail = value.TruncateTo(255); 
				OnPropertyChanged("EndBuyerEmail", value);
            }
        }

        public virtual string ShipToName
        {
            get
            {
				return _shipToName?.TrimEnd(); 
            }
            set
            {
				_shipToName = value.TruncateTo(100); 
				OnPropertyChanged("ShipToName", value);
            }
        }

        public virtual string ShipToFirstName
        {
            get
            {
				return _shipToFirstName?.TrimEnd(); 
            }
            set
            {
				_shipToFirstName = value.TruncateTo(50); 
				OnPropertyChanged("ShipToFirstName", value);
            }
        }

        public virtual string ShipToLastName
        {
            get
            {
				return _shipToLastName?.TrimEnd(); 
            }
            set
            {
				_shipToLastName = value.TruncateTo(50); 
				OnPropertyChanged("ShipToLastName", value);
            }
        }

        public virtual string ShipToSuffix
        {
            get
            {
				return _shipToSuffix?.TrimEnd(); 
            }
            set
            {
				_shipToSuffix = value.TruncateTo(50); 
				OnPropertyChanged("ShipToSuffix", value);
            }
        }

        public virtual string ShipToCompany
        {
            get
            {
				return _shipToCompany?.TrimEnd(); 
            }
            set
            {
				_shipToCompany = value.TruncateTo(100); 
				OnPropertyChanged("ShipToCompany", value);
            }
        }

        public virtual string ShipToCompanyJobTitle
        {
            get
            {
				return _shipToCompanyJobTitle?.TrimEnd(); 
            }
            set
            {
				_shipToCompanyJobTitle = value.TruncateTo(100); 
				OnPropertyChanged("ShipToCompanyJobTitle", value);
            }
        }

        public virtual string ShipToAttention
        {
            get
            {
				return _shipToAttention?.TrimEnd(); 
            }
            set
            {
				_shipToAttention = value.TruncateTo(100); 
				OnPropertyChanged("ShipToAttention", value);
            }
        }

        public virtual string ShipToAddressLine1
        {
            get
            {
				return _shipToAddressLine1?.TrimEnd(); 
            }
            set
            {
				_shipToAddressLine1 = value.TruncateTo(200); 
				OnPropertyChanged("ShipToAddressLine1", value);
            }
        }

        public virtual string ShipToAddressLine2
        {
            get
            {
				return _shipToAddressLine2?.TrimEnd(); 
            }
            set
            {
				_shipToAddressLine2 = value.TruncateTo(200); 
				OnPropertyChanged("ShipToAddressLine2", value);
            }
        }

        public virtual string ShipToAddressLine3
        {
            get
            {
				return _shipToAddressLine3?.TrimEnd(); 
            }
            set
            {
				_shipToAddressLine3 = value.TruncateTo(200); 
				OnPropertyChanged("ShipToAddressLine3", value);
            }
        }

        public virtual string ShipToCity
        {
            get
            {
				return _shipToCity?.TrimEnd(); 
            }
            set
            {
				_shipToCity = value.TruncateTo(100); 
				OnPropertyChanged("ShipToCity", value);
            }
        }

        public virtual string ShipToState
        {
            get
            {
				return _shipToState?.TrimEnd(); 
            }
            set
            {
				_shipToState = value.TruncateTo(50); 
				OnPropertyChanged("ShipToState", value);
            }
        }

        public virtual string ShipToStateFullName
        {
            get
            {
				return _shipToStateFullName?.TrimEnd(); 
            }
            set
            {
				_shipToStateFullName = value.TruncateTo(100); 
				OnPropertyChanged("ShipToStateFullName", value);
            }
        }

        public virtual string ShipToPostalCode
        {
            get
            {
				return _shipToPostalCode?.TrimEnd(); 
            }
            set
            {
				_shipToPostalCode = value.TruncateTo(50); 
				OnPropertyChanged("ShipToPostalCode", value);
            }
        }

        public virtual string ShipToPostalCodeExt
        {
            get
            {
				return _shipToPostalCodeExt?.TrimEnd(); 
            }
            set
            {
				_shipToPostalCodeExt = value.TruncateTo(50); 
				OnPropertyChanged("ShipToPostalCodeExt", value);
            }
        }

        public virtual string ShipToCounty
        {
            get
            {
				return _shipToCounty?.TrimEnd(); 
            }
            set
            {
				_shipToCounty = value.TruncateTo(100); 
				OnPropertyChanged("ShipToCounty", value);
            }
        }

        public virtual string ShipToCountry
        {
            get
            {
				return _shipToCountry?.TrimEnd(); 
            }
            set
            {
				_shipToCountry = value.TruncateTo(100); 
				OnPropertyChanged("ShipToCountry", value);
            }
        }

        public virtual string ShipToEmail
        {
            get
            {
				return _shipToEmail?.TrimEnd(); 
            }
            set
            {
				_shipToEmail = value.TruncateTo(100); 
				OnPropertyChanged("ShipToEmail", value);
            }
        }

        public virtual string ShipToDaytimePhone
        {
            get
            {
				return _shipToDaytimePhone?.TrimEnd(); 
            }
            set
            {
				_shipToDaytimePhone = value.TruncateTo(50); 
				OnPropertyChanged("ShipToDaytimePhone", value);
            }
        }

        public virtual string ShipToNightPhone
        {
            get
            {
				return _shipToNightPhone?.TrimEnd(); 
            }
            set
            {
				_shipToNightPhone = value.TruncateTo(50); 
				OnPropertyChanged("ShipToNightPhone", value);
            }
        }

        public virtual string BillToName
        {
            get
            {
				return _billToName?.TrimEnd(); 
            }
            set
            {
				_billToName = value.TruncateTo(100); 
				OnPropertyChanged("BillToName", value);
            }
        }

        public virtual string BillToFirstName
        {
            get
            {
				return _billToFirstName?.TrimEnd(); 
            }
            set
            {
				_billToFirstName = value.TruncateTo(50); 
				OnPropertyChanged("BillToFirstName", value);
            }
        }

        public virtual string BillToLastName
        {
            get
            {
				return _billToLastName?.TrimEnd(); 
            }
            set
            {
				_billToLastName = value.TruncateTo(50); 
				OnPropertyChanged("BillToLastName", value);
            }
        }

        public virtual string BillToSuffix
        {
            get
            {
				return _billToSuffix?.TrimEnd(); 
            }
            set
            {
				_billToSuffix = value.TruncateTo(50); 
				OnPropertyChanged("BillToSuffix", value);
            }
        }

        public virtual string BillToCompany
        {
            get
            {
				return _billToCompany?.TrimEnd(); 
            }
            set
            {
				_billToCompany = value.TruncateTo(100); 
				OnPropertyChanged("BillToCompany", value);
            }
        }

        public virtual string BillToCompanyJobTitle
        {
            get
            {
				return _billToCompanyJobTitle?.TrimEnd(); 
            }
            set
            {
				_billToCompanyJobTitle = value.TruncateTo(100); 
				OnPropertyChanged("BillToCompanyJobTitle", value);
            }
        }

        public virtual string BillToAttention
        {
            get
            {
				return _billToAttention?.TrimEnd(); 
            }
            set
            {
				_billToAttention = value.TruncateTo(100); 
				OnPropertyChanged("BillToAttention", value);
            }
        }

        public virtual string BillToAddressLine1
        {
            get
            {
				return _billToAddressLine1?.TrimEnd(); 
            }
            set
            {
				_billToAddressLine1 = value.TruncateTo(200); 
				OnPropertyChanged("BillToAddressLine1", value);
            }
        }

        public virtual string BillToAddressLine2
        {
            get
            {
				return _billToAddressLine2?.TrimEnd(); 
            }
            set
            {
				_billToAddressLine2 = value.TruncateTo(200); 
				OnPropertyChanged("BillToAddressLine2", value);
            }
        }

        public virtual string BillToAddressLine3
        {
            get
            {
				return _billToAddressLine3?.TrimEnd(); 
            }
            set
            {
				_billToAddressLine3 = value.TruncateTo(200); 
				OnPropertyChanged("BillToAddressLine3", value);
            }
        }

        public virtual string BillToCity
        {
            get
            {
				return _billToCity?.TrimEnd(); 
            }
            set
            {
				_billToCity = value.TruncateTo(100); 
				OnPropertyChanged("BillToCity", value);
            }
        }

        public virtual string BillToState
        {
            get
            {
				return _billToState?.TrimEnd(); 
            }
            set
            {
				_billToState = value.TruncateTo(50); 
				OnPropertyChanged("BillToState", value);
            }
        }

        public virtual string BillToStateFullName
        {
            get
            {
				return _billToStateFullName?.TrimEnd(); 
            }
            set
            {
				_billToStateFullName = value.TruncateTo(100); 
				OnPropertyChanged("BillToStateFullName", value);
            }
        }

        public virtual string BillToPostalCode
        {
            get
            {
				return _billToPostalCode?.TrimEnd(); 
            }
            set
            {
				_billToPostalCode = value.TruncateTo(50); 
				OnPropertyChanged("BillToPostalCode", value);
            }
        }

        public virtual string BillToPostalCodeExt
        {
            get
            {
				return _billToPostalCodeExt?.TrimEnd(); 
            }
            set
            {
				_billToPostalCodeExt = value.TruncateTo(50); 
				OnPropertyChanged("BillToPostalCodeExt", value);
            }
        }

        public virtual string BillToCounty
        {
            get
            {
				return _billToCounty?.TrimEnd(); 
            }
            set
            {
				_billToCounty = value.TruncateTo(50); 
				OnPropertyChanged("BillToCounty", value);
            }
        }

        public virtual string BillToCountry
        {
            get
            {
				return _billToCountry?.TrimEnd(); 
            }
            set
            {
				_billToCountry = value.TruncateTo(100); 
				OnPropertyChanged("BillToCountry", value);
            }
        }

        public virtual string BillToEmail
        {
            get
            {
				return _billToEmail?.TrimEnd(); 
            }
            set
            {
				_billToEmail = value.TruncateTo(100); 
				OnPropertyChanged("BillToEmail", value);
            }
        }

        public virtual string BillToDaytimePhone
        {
            get
            {
				return _billToDaytimePhone?.TrimEnd(); 
            }
            set
            {
				_billToDaytimePhone = value.TruncateTo(50); 
				OnPropertyChanged("BillToDaytimePhone", value);
            }
        }

        public virtual string BillToNightPhone
        {
            get
            {
				return _billToNightPhone?.TrimEnd(); 
            }
            set
            {
				_billToNightPhone = value.TruncateTo(50); 
				OnPropertyChanged("BillToNightPhone", value);
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
				{
					_updateDateUtc = (value is null) ? (DateTime?) null : value?.Date.ToSqlSafeValue(); 
					OnPropertyChanged("UpdateDateUtc", value);
				}
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
				OnPropertyChanged("EnterBy", value);
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
				OnPropertyChanged("UpdateBy", value);
            }
        }



        #endregion Properties - Generated 

        #region Methods - Parent

		[XmlIgnore, JsonIgnore, IgnoreCompare]
		private SalesOrderData Parent { get; set; }
		public SalesOrderData GetParent() => Parent;
		public SalesOrderHeaderInfo SetParent(SalesOrderData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			SalesOrderUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override SalesOrderHeaderInfo Clear()
        {
            base.Clear();
			_salesOrderUuid = String.Empty; 
			_centralFulfillmentNum = default(long); 
			_shippingCarrier = String.Empty; 
			_shippingClass = String.Empty; 
			_distributionCenterNum = default(int); 
			_centralOrderNum = default(long); 
			_channelNum = default(int); 
			_channelAccountNum = default(int); 
			_channelOrderID = String.Empty; 
			_secondaryChannelOrderID = String.Empty; 
			_shippingAccount = String.Empty; 
			_warehouseUuid = String.Empty; 
			_refNum = String.Empty; 
			_customerPoNum = String.Empty; 
			_endBuyerUserID = String.Empty; 
			_endBuyerName = String.Empty; 
			_endBuyerEmail = String.Empty; 
			_shipToName = String.Empty; 
			_shipToFirstName = String.Empty; 
			_shipToLastName = String.Empty; 
			_shipToSuffix = String.Empty; 
			_shipToCompany = String.Empty; 
			_shipToCompanyJobTitle = String.Empty; 
			_shipToAttention = String.Empty; 
			_shipToAddressLine1 = String.Empty; 
			_shipToAddressLine2 = String.Empty; 
			_shipToAddressLine3 = String.Empty; 
			_shipToCity = String.Empty; 
			_shipToState = String.Empty; 
			_shipToStateFullName = String.Empty; 
			_shipToPostalCode = String.Empty; 
			_shipToPostalCodeExt = String.Empty; 
			_shipToCounty = String.Empty; 
			_shipToCountry = String.Empty; 
			_shipToEmail = String.Empty; 
			_shipToDaytimePhone = String.Empty; 
			_shipToNightPhone = String.Empty; 
			_billToName = String.Empty; 
			_billToFirstName = String.Empty; 
			_billToLastName = String.Empty; 
			_billToSuffix = String.Empty; 
			_billToCompany = String.Empty; 
			_billToCompanyJobTitle = String.Empty; 
			_billToAttention = String.Empty; 
			_billToAddressLine1 = String.Empty; 
			_billToAddressLine2 = String.Empty; 
			_billToAddressLine3 = String.Empty; 
			_billToCity = String.Empty; 
			_billToState = String.Empty; 
			_billToStateFullName = String.Empty; 
			_billToPostalCode = String.Empty; 
			_billToPostalCodeExt = String.Empty; 
			_billToCounty = String.Empty; 
			_billToCountry = String.Empty; 
			_billToEmail = String.Empty; 
			_billToDaytimePhone = String.Empty; 
			_billToNightPhone = String.Empty; 
			_updateDateUtc = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_enterBy = String.Empty; 
			_updateBy = String.Empty; 
            ClearChildren();
            return this;
        }

        public virtual SalesOrderHeaderInfo ClearChildren()
        {
            return this;
        }

        public virtual SalesOrderHeaderInfo NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(SalesOrderHeaderInfo data)
        {
            if (data is null) return;
            return;
        }



        #endregion Methods - Generated 
    }
}



