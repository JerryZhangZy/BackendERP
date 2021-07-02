
              

              
    

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
    /// Represents a OrderHeaderInfo.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("OrderHeaderInfo")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("OrderUuid")]
    [DtoName("OrderHeaderInfoDto")]
    public partial class OrderHeaderInfo : TableRepository<OrderHeaderInfo, long>
    {

        public OrderHeaderInfo() : base() {}
        public OrderHeaderInfo(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("OrderUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _orderUuid;

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

        [Column("WarehouseUuid",SqlDbType.VarChar)]
        private string _warehouseUuid;

        [Column("RefNum",SqlDbType.VarChar)]
        private string _refNum;

        [Column("CustomerPoNum",SqlDbType.VarChar)]
        private string _customerPoNum;

        [Column("EndBuyerUserID",SqlDbType.VarChar)]
        private string _endBuyerUserID;

        [Column("EndBuyerName",SqlDbType.NVarChar)]
        private string _endBuyerName;

        [Column("EndBuyerEmail",SqlDbType.VarChar)]
        private string _endBuyerEmail;

        [Column("ShipToName",SqlDbType.NVarChar)]
        private string _shipToName;

        [Column("ShipToFirstName",SqlDbType.NVarChar)]
        private string _shipToFirstName;

        [Column("ShipToLastName",SqlDbType.NVarChar)]
        private string _shipToLastName;

        [Column("ShipToSuffix",SqlDbType.NVarChar)]
        private string _shipToSuffix;

        [Column("ShipToCompany",SqlDbType.NVarChar)]
        private string _shipToCompany;

        [Column("ShipToCompanyJobTitle",SqlDbType.NVarChar)]
        private string _shipToCompanyJobTitle;

        [Column("ShipToAttention",SqlDbType.NVarChar)]
        private string _shipToAttention;

        [Column("ShipToAddressLine1",SqlDbType.NVarChar)]
        private string _shipToAddressLine1;

        [Column("ShipToAddressLine2",SqlDbType.NVarChar)]
        private string _shipToAddressLine2;

        [Column("ShipToAddressLine3",SqlDbType.NVarChar)]
        private string _shipToAddressLine3;

        [Column("ShipToCity",SqlDbType.NVarChar)]
        private string _shipToCity;

        [Column("ShipToState",SqlDbType.NVarChar)]
        private string _shipToState;

        [Column("ShipToStateFullName",SqlDbType.NVarChar)]
        private string _shipToStateFullName;

        [Column("ShipToPostalCode",SqlDbType.VarChar)]
        private string _shipToPostalCode;

        [Column("ShipToPostalCodeExt",SqlDbType.VarChar)]
        private string _shipToPostalCodeExt;

        [Column("ShipToCounty",SqlDbType.NVarChar)]
        private string _shipToCounty;

        [Column("ShipToCountry",SqlDbType.NVarChar)]
        private string _shipToCountry;

        [Column("ShipToEmail",SqlDbType.VarChar)]
        private string _shipToEmail;

        [Column("ShipToDaytimePhone",SqlDbType.VarChar)]
        private string _shipToDaytimePhone;

        [Column("ShipToNightPhone",SqlDbType.VarChar)]
        private string _shipToNightPhone;

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
		public override string UniqueId => OrderUuid; 
		public void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(OrderUuid)) 
				OrderUuid = Guid.NewGuid().ToString(); 
		}
        public virtual string OrderUuid
        {
            get
            {
				return _orderUuid?.TrimEnd(); 
            }
            set
            {
				_orderUuid = value.TruncateTo(50); 
            }
        }

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
					_centralFulfillmentNum = value; 
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
					_distributionCenterNum = value; 
            }
        }

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
					_centralOrderNum = value; 
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
            }
        }

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
					_secondaryChannelOrderID = value.TruncateTo(200); 
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
					_shippingAccount = value.TruncateTo(100); 
            }
        }

        public virtual string WarehouseUuid
        {
            get
            {
				if (!AllowNull && _warehouseUuid is null) 
					_warehouseUuid = String.Empty; 
				return _warehouseUuid?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_warehouseUuid = value.TruncateTo(50); 
            }
        }

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
					_refNum = value.TruncateTo(100); 
            }
        }

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
					_customerPoNum = value.TruncateTo(100); 
            }
        }

        public virtual string EndBuyerUserID
        {
            get
            {
				if (!AllowNull && _endBuyerUserID is null) 
					_endBuyerUserID = String.Empty; 
				return _endBuyerUserID?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_endBuyerUserID = value.TruncateTo(255); 
            }
        }

        public virtual string EndBuyerName
        {
            get
            {
				if (!AllowNull && _endBuyerName is null) 
					_endBuyerName = String.Empty; 
				return _endBuyerName?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_endBuyerName = value.TruncateTo(255); 
            }
        }

        public virtual string EndBuyerEmail
        {
            get
            {
				if (!AllowNull && _endBuyerEmail is null) 
					_endBuyerEmail = String.Empty; 
				return _endBuyerEmail?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_endBuyerEmail = value.TruncateTo(255); 
            }
        }

        public virtual string ShipToName
        {
            get
            {
				if (!AllowNull && _shipToName is null) 
					_shipToName = String.Empty; 
				return _shipToName?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_shipToName = value.TruncateTo(100); 
            }
        }

        public virtual string ShipToFirstName
        {
            get
            {
				if (!AllowNull && _shipToFirstName is null) 
					_shipToFirstName = String.Empty; 
				return _shipToFirstName?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_shipToFirstName = value.TruncateTo(50); 
            }
        }

        public virtual string ShipToLastName
        {
            get
            {
				if (!AllowNull && _shipToLastName is null) 
					_shipToLastName = String.Empty; 
				return _shipToLastName?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_shipToLastName = value.TruncateTo(50); 
            }
        }

        public virtual string ShipToSuffix
        {
            get
            {
				if (!AllowNull && _shipToSuffix is null) 
					_shipToSuffix = String.Empty; 
				return _shipToSuffix?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_shipToSuffix = value.TruncateTo(50); 
            }
        }

        public virtual string ShipToCompany
        {
            get
            {
				if (!AllowNull && _shipToCompany is null) 
					_shipToCompany = String.Empty; 
				return _shipToCompany?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_shipToCompany = value.TruncateTo(100); 
            }
        }

        public virtual string ShipToCompanyJobTitle
        {
            get
            {
				if (!AllowNull && _shipToCompanyJobTitle is null) 
					_shipToCompanyJobTitle = String.Empty; 
				return _shipToCompanyJobTitle?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_shipToCompanyJobTitle = value.TruncateTo(100); 
            }
        }

        public virtual string ShipToAttention
        {
            get
            {
				if (!AllowNull && _shipToAttention is null) 
					_shipToAttention = String.Empty; 
				return _shipToAttention?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_shipToAttention = value.TruncateTo(100); 
            }
        }

        public virtual string ShipToAddressLine1
        {
            get
            {
				if (!AllowNull && _shipToAddressLine1 is null) 
					_shipToAddressLine1 = String.Empty; 
				return _shipToAddressLine1?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_shipToAddressLine1 = value.TruncateTo(200); 
            }
        }

        public virtual string ShipToAddressLine2
        {
            get
            {
				if (!AllowNull && _shipToAddressLine2 is null) 
					_shipToAddressLine2 = String.Empty; 
				return _shipToAddressLine2?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_shipToAddressLine2 = value.TruncateTo(200); 
            }
        }

        public virtual string ShipToAddressLine3
        {
            get
            {
				if (!AllowNull && _shipToAddressLine3 is null) 
					_shipToAddressLine3 = String.Empty; 
				return _shipToAddressLine3?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_shipToAddressLine3 = value.TruncateTo(200); 
            }
        }

        public virtual string ShipToCity
        {
            get
            {
				if (!AllowNull && _shipToCity is null) 
					_shipToCity = String.Empty; 
				return _shipToCity?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_shipToCity = value.TruncateTo(100); 
            }
        }

        public virtual string ShipToState
        {
            get
            {
				if (!AllowNull && _shipToState is null) 
					_shipToState = String.Empty; 
				return _shipToState?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_shipToState = value.TruncateTo(50); 
            }
        }

        public virtual string ShipToStateFullName
        {
            get
            {
				if (!AllowNull && _shipToStateFullName is null) 
					_shipToStateFullName = String.Empty; 
				return _shipToStateFullName?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_shipToStateFullName = value.TruncateTo(100); 
            }
        }

        public virtual string ShipToPostalCode
        {
            get
            {
				if (!AllowNull && _shipToPostalCode is null) 
					_shipToPostalCode = String.Empty; 
				return _shipToPostalCode?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_shipToPostalCode = value.TruncateTo(50); 
            }
        }

        public virtual string ShipToPostalCodeExt
        {
            get
            {
				if (!AllowNull && _shipToPostalCodeExt is null) 
					_shipToPostalCodeExt = String.Empty; 
				return _shipToPostalCodeExt?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_shipToPostalCodeExt = value.TruncateTo(50); 
            }
        }

        public virtual string ShipToCounty
        {
            get
            {
				if (!AllowNull && _shipToCounty is null) 
					_shipToCounty = String.Empty; 
				return _shipToCounty?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_shipToCounty = value.TruncateTo(100); 
            }
        }

        public virtual string ShipToCountry
        {
            get
            {
				if (!AllowNull && _shipToCountry is null) 
					_shipToCountry = String.Empty; 
				return _shipToCountry?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_shipToCountry = value.TruncateTo(100); 
            }
        }

        public virtual string ShipToEmail
        {
            get
            {
				if (!AllowNull && _shipToEmail is null) 
					_shipToEmail = String.Empty; 
				return _shipToEmail?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_shipToEmail = value.TruncateTo(100); 
            }
        }

        public virtual string ShipToDaytimePhone
        {
            get
            {
				if (!AllowNull && _shipToDaytimePhone is null) 
					_shipToDaytimePhone = String.Empty; 
				return _shipToDaytimePhone?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_shipToDaytimePhone = value.TruncateTo(50); 
            }
        }

        public virtual string ShipToNightPhone
        {
            get
            {
				if (!AllowNull && _shipToNightPhone is null) 
					_shipToNightPhone = String.Empty; 
				return _shipToNightPhone?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_shipToNightPhone = value.TruncateTo(50); 
            }
        }

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
					_billToName = value.TruncateTo(100); 
            }
        }

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
					_billToFirstName = value.TruncateTo(50); 
            }
        }

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
					_billToLastName = value.TruncateTo(50); 
            }
        }

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
					_billToSuffix = value.TruncateTo(50); 
            }
        }

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
					_billToCompany = value.TruncateTo(100); 
            }
        }

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
					_billToCompanyJobTitle = value.TruncateTo(100); 
            }
        }

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
					_billToAttention = value.TruncateTo(100); 
            }
        }

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
					_billToAddressLine1 = value.TruncateTo(200); 
            }
        }

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
					_billToAddressLine2 = value.TruncateTo(200); 
            }
        }

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
					_billToAddressLine3 = value.TruncateTo(200); 
            }
        }

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
					_billToCity = value.TruncateTo(100); 
            }
        }

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
					_billToState = value.TruncateTo(50); 
            }
        }

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
					_billToStateFullName = value.TruncateTo(100); 
            }
        }

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
					_billToPostalCode = value.TruncateTo(50); 
            }
        }

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
					_billToPostalCodeExt = value.TruncateTo(50); 
            }
        }

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
					_billToCounty = value.TruncateTo(50); 
            }
        }

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
					_billToCountry = value.TruncateTo(100); 
            }
        }

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
					_billToEmail = value.TruncateTo(100); 
            }
        }

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
					_billToDaytimePhone = value.TruncateTo(50); 
            }
        }

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
					_billToNightPhone = value.TruncateTo(50); 
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
		private OrderData Parent { get; set; }
		public OrderData GetParent() => Parent;
		public OrderHeaderInfo SetParent(OrderData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			OrderUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override OrderHeaderInfo Clear()
        {
			_orderUuid = String.Empty; 
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
			_warehouseUuid = AllowNull ? (string)null : String.Empty; 
			_refNum = AllowNull ? (string)null : String.Empty; 
			_customerPoNum = AllowNull ? (string)null : String.Empty; 
			_endBuyerUserID = AllowNull ? (string)null : String.Empty; 
			_endBuyerName = AllowNull ? (string)null : String.Empty; 
			_endBuyerEmail = AllowNull ? (string)null : String.Empty; 
			_shipToName = AllowNull ? (string)null : String.Empty; 
			_shipToFirstName = AllowNull ? (string)null : String.Empty; 
			_shipToLastName = AllowNull ? (string)null : String.Empty; 
			_shipToSuffix = AllowNull ? (string)null : String.Empty; 
			_shipToCompany = AllowNull ? (string)null : String.Empty; 
			_shipToCompanyJobTitle = AllowNull ? (string)null : String.Empty; 
			_shipToAttention = AllowNull ? (string)null : String.Empty; 
			_shipToAddressLine1 = AllowNull ? (string)null : String.Empty; 
			_shipToAddressLine2 = AllowNull ? (string)null : String.Empty; 
			_shipToAddressLine3 = AllowNull ? (string)null : String.Empty; 
			_shipToCity = AllowNull ? (string)null : String.Empty; 
			_shipToState = AllowNull ? (string)null : String.Empty; 
			_shipToStateFullName = AllowNull ? (string)null : String.Empty; 
			_shipToPostalCode = AllowNull ? (string)null : String.Empty; 
			_shipToPostalCodeExt = AllowNull ? (string)null : String.Empty; 
			_shipToCounty = AllowNull ? (string)null : String.Empty; 
			_shipToCountry = AllowNull ? (string)null : String.Empty; 
			_shipToEmail = AllowNull ? (string)null : String.Empty; 
			_shipToDaytimePhone = AllowNull ? (string)null : String.Empty; 
			_shipToNightPhone = AllowNull ? (string)null : String.Empty; 
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

        public virtual OrderHeaderInfo ClearChildren()
        {
            return this;
        }

        public virtual OrderHeaderInfo NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(OrderHeaderInfo data)
        {
            return;
        }

        #endregion Methods - Generated 
    }
}



