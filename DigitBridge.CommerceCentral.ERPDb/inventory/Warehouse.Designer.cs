
              

              
    

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
    /// Represents a Warehouse.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("Warehouse")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("WarehouseUuid")]
    [DtoName("WarehouseDto")]
    public partial class Warehouse : TableRepository<Warehouse, long>
    {

        public Warehouse() : base() {}
        public Warehouse(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("DatabaseNum",SqlDbType.Int,NotNull=true)]
        private int _databaseNum;

        [Column("MasterAccountNum",SqlDbType.Int,NotNull=true)]
        private int _masterAccountNum;

        [Column("ProfileNum",SqlDbType.Int,NotNull=true)]
        private int _profileNum;

        [Column("WarehouseUuid",SqlDbType.VarChar)]
        private string _warehouseUuid;

        [Column("DistributionCenterNum",SqlDbType.Int)]
        private int? _distributionCenterNum;

        [Column("DistributionCenterName",SqlDbType.NVarChar)]
        private string _distributionCenterName;

        [Column("DistributionCenterCode",SqlDbType.NVarChar)]
        private string _distributionCenterCode;

        [Column("DistributionCenterType",SqlDbType.Int)]
        private int? _distributionCenterType;

        [Column("WarehouseType",SqlDbType.Int,IsDefault=true)]
        private int? _warehouseType;

        [Column("WarehouseStatus",SqlDbType.Int,IsDefault=true)]
        private int? _warehouseStatus;

        [Column("Priority",SqlDbType.Int,IsDefault=true)]
        private int? _priority;

        [Column("WarehouseNum",SqlDbType.VarChar)]
        private string _warehouseNum;

        [Column("WarehouseName",SqlDbType.NVarChar)]
        private string _warehouseName;

        [Column("CustomerUuid",SqlDbType.VarChar)]
        private string _customerUuid;

        [Column("VendorUuid",SqlDbType.VarChar)]
        private string _vendorUuid;

        [Column("ShippingCarrier",SqlDbType.VarChar)]
        private string _shippingCarrier;

        [Column("ShippingClass",SqlDbType.VarChar)]
        private string _shippingClass;

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

        [Column("ShipFromName",SqlDbType.NVarChar)]
        private string _shipFromName;

        [Column("ShipFromFirstName",SqlDbType.NVarChar)]
        private string _shipFromFirstName;

        [Column("ShipFromLastName",SqlDbType.NVarChar)]
        private string _shipFromLastName;

        [Column("ShipFromSuffix",SqlDbType.NVarChar)]
        private string _shipFromSuffix;

        [Column("ShipFromCompany",SqlDbType.NVarChar)]
        private string _shipFromCompany;

        [Column("ShipFromCompanyJobTitle",SqlDbType.NVarChar)]
        private string _shipFromCompanyJobTitle;

        [Column("ShipFromAttention",SqlDbType.NVarChar)]
        private string _shipFromAttention;

        [Column("ShipFromAddressLine1",SqlDbType.NVarChar)]
        private string _shipFromAddressLine1;

        [Column("ShipFromAddressLine2",SqlDbType.NVarChar)]
        private string _shipFromAddressLine2;

        [Column("ShipFromAddressLine3",SqlDbType.NVarChar)]
        private string _shipFromAddressLine3;

        [Column("ShipFromCity",SqlDbType.NVarChar)]
        private string _shipFromCity;

        [Column("ShipFromState",SqlDbType.NVarChar)]
        private string _shipFromState;

        [Column("ShipFromStateFullName",SqlDbType.NVarChar)]
        private string _shipFromStateFullName;

        [Column("ShipFromPostalCode",SqlDbType.VarChar)]
        private string _shipFromPostalCode;

        [Column("ShipFromPostalCodeExt",SqlDbType.VarChar)]
        private string _shipFromPostalCodeExt;

        [Column("ShipFromCounty",SqlDbType.NVarChar)]
        private string _shipFromCounty;

        [Column("ShipFromCountry",SqlDbType.NVarChar)]
        private string _shipFromCountry;

        [Column("ShipFromEmail",SqlDbType.VarChar)]
        private string _shipFromEmail;

        [Column("ShipFromDaytimePhone",SqlDbType.VarChar)]
        private string _shipFromDaytimePhone;

        [Column("ShipFromNightPhone",SqlDbType.VarChar)]
        private string _shipFromNightPhone;

        [Column("UpdateDateUtc",SqlDbType.DateTime)]
        private DateTime? _updateDateUtc;

        [Column("EnterBy",SqlDbType.VarChar,NotNull=true)]
        private string _enterBy;

        [Column("UpdateBy",SqlDbType.VarChar,NotNull=true)]
        private string _updateBy;

        #endregion Fields - Generated 

        #region Properties - Generated 
		public override string UniqueId => WarehouseUuid; 
		public void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(WarehouseUuid)) 
				WarehouseUuid = Guid.NewGuid().ToString(); 
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
				OnPropertyChanged("DatabaseNum", value);
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
				OnPropertyChanged("MasterAccountNum", value);
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
				OnPropertyChanged("ProfileNum", value);
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
				{
					_warehouseUuid = value.TruncateTo(50); 
					OnPropertyChanged("WarehouseUuid", value);
				}
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
				{
					_distributionCenterNum = value; 
					OnPropertyChanged("DistributionCenterNum", value);
				}
            }
        }

        public virtual string DistributionCenterName
        {
            get
            {
				if (!AllowNull && _distributionCenterName is null) 
					_distributionCenterName = String.Empty; 
				return _distributionCenterName?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_distributionCenterName = value.TruncateTo(200); 
					OnPropertyChanged("DistributionCenterName", value);
				}
            }
        }

        public virtual string DistributionCenterCode
        {
            get
            {
				if (!AllowNull && _distributionCenterCode is null) 
					_distributionCenterCode = String.Empty; 
				return _distributionCenterCode?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_distributionCenterCode = value.TruncateTo(50); 
					OnPropertyChanged("DistributionCenterCode", value);
				}
            }
        }

        public virtual int? DistributionCenterType
        {
            get
            {
				if (!AllowNull && _distributionCenterType is null) 
					_distributionCenterType = default(int); 
				return _distributionCenterType; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_distributionCenterType = value; 
					OnPropertyChanged("DistributionCenterType", value);
				}
            }
        }

        public virtual int? WarehouseType
        {
            get
            {
				if (!AllowNull && _warehouseType is null) 
					_warehouseType = default(int); 
				return _warehouseType; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_warehouseType = value; 
					OnPropertyChanged("WarehouseType", value);
				}
            }
        }

        public virtual int? WarehouseStatus
        {
            get
            {
				if (!AllowNull && _warehouseStatus is null) 
					_warehouseStatus = default(int); 
				return _warehouseStatus; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_warehouseStatus = value; 
					OnPropertyChanged("WarehouseStatus", value);
				}
            }
        }

        public virtual int? Priority
        {
            get
            {
				if (!AllowNull && _priority is null) 
					_priority = default(int); 
				return _priority; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_priority = value; 
					OnPropertyChanged("Priority", value);
				}
            }
        }

        public virtual string WarehouseNum
        {
            get
            {
				if (!AllowNull && _warehouseNum is null) 
					_warehouseNum = String.Empty; 
				return _warehouseNum?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_warehouseNum = value.TruncateTo(50); 
					OnPropertyChanged("WarehouseNum", value);
				}
            }
        }

        public virtual string WarehouseName
        {
            get
            {
				if (!AllowNull && _warehouseName is null) 
					_warehouseName = String.Empty; 
				return _warehouseName?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_warehouseName = value.TruncateTo(200); 
					OnPropertyChanged("WarehouseName", value);
				}
            }
        }

        public virtual string CustomerUuid
        {
            get
            {
				if (!AllowNull && _customerUuid is null) 
					_customerUuid = String.Empty; 
				return _customerUuid?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_customerUuid = value.TruncateTo(50); 
					OnPropertyChanged("CustomerUuid", value);
				}
            }
        }

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
				{
					_shipToName = value.TruncateTo(100); 
					OnPropertyChanged("ShipToName", value);
				}
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
				{
					_shipToFirstName = value.TruncateTo(50); 
					OnPropertyChanged("ShipToFirstName", value);
				}
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
				{
					_shipToLastName = value.TruncateTo(50); 
					OnPropertyChanged("ShipToLastName", value);
				}
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
				{
					_shipToSuffix = value.TruncateTo(50); 
					OnPropertyChanged("ShipToSuffix", value);
				}
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
				{
					_shipToCompany = value.TruncateTo(100); 
					OnPropertyChanged("ShipToCompany", value);
				}
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
				{
					_shipToCompanyJobTitle = value.TruncateTo(100); 
					OnPropertyChanged("ShipToCompanyJobTitle", value);
				}
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
				{
					_shipToAttention = value.TruncateTo(100); 
					OnPropertyChanged("ShipToAttention", value);
				}
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
				{
					_shipToAddressLine1 = value.TruncateTo(200); 
					OnPropertyChanged("ShipToAddressLine1", value);
				}
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
				{
					_shipToAddressLine2 = value.TruncateTo(200); 
					OnPropertyChanged("ShipToAddressLine2", value);
				}
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
				{
					_shipToAddressLine3 = value.TruncateTo(200); 
					OnPropertyChanged("ShipToAddressLine3", value);
				}
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
				{
					_shipToCity = value.TruncateTo(100); 
					OnPropertyChanged("ShipToCity", value);
				}
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
				{
					_shipToState = value.TruncateTo(50); 
					OnPropertyChanged("ShipToState", value);
				}
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
				{
					_shipToStateFullName = value.TruncateTo(100); 
					OnPropertyChanged("ShipToStateFullName", value);
				}
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
				{
					_shipToPostalCode = value.TruncateTo(50); 
					OnPropertyChanged("ShipToPostalCode", value);
				}
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
				{
					_shipToPostalCodeExt = value.TruncateTo(50); 
					OnPropertyChanged("ShipToPostalCodeExt", value);
				}
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
				{
					_shipToCounty = value.TruncateTo(100); 
					OnPropertyChanged("ShipToCounty", value);
				}
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
				{
					_shipToCountry = value.TruncateTo(100); 
					OnPropertyChanged("ShipToCountry", value);
				}
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
				{
					_shipToEmail = value.TruncateTo(100); 
					OnPropertyChanged("ShipToEmail", value);
				}
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
				{
					_shipToDaytimePhone = value.TruncateTo(50); 
					OnPropertyChanged("ShipToDaytimePhone", value);
				}
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
				{
					_shipToNightPhone = value.TruncateTo(50); 
					OnPropertyChanged("ShipToNightPhone", value);
				}
            }
        }

        public virtual string ShipFromName
        {
            get
            {
				if (!AllowNull && _shipFromName is null) 
					_shipFromName = String.Empty; 
				return _shipFromName?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_shipFromName = value.TruncateTo(100); 
					OnPropertyChanged("ShipFromName", value);
				}
            }
        }

        public virtual string ShipFromFirstName
        {
            get
            {
				if (!AllowNull && _shipFromFirstName is null) 
					_shipFromFirstName = String.Empty; 
				return _shipFromFirstName?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_shipFromFirstName = value.TruncateTo(50); 
					OnPropertyChanged("ShipFromFirstName", value);
				}
            }
        }

        public virtual string ShipFromLastName
        {
            get
            {
				if (!AllowNull && _shipFromLastName is null) 
					_shipFromLastName = String.Empty; 
				return _shipFromLastName?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_shipFromLastName = value.TruncateTo(50); 
					OnPropertyChanged("ShipFromLastName", value);
				}
            }
        }

        public virtual string ShipFromSuffix
        {
            get
            {
				if (!AllowNull && _shipFromSuffix is null) 
					_shipFromSuffix = String.Empty; 
				return _shipFromSuffix?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_shipFromSuffix = value.TruncateTo(50); 
					OnPropertyChanged("ShipFromSuffix", value);
				}
            }
        }

        public virtual string ShipFromCompany
        {
            get
            {
				if (!AllowNull && _shipFromCompany is null) 
					_shipFromCompany = String.Empty; 
				return _shipFromCompany?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_shipFromCompany = value.TruncateTo(100); 
					OnPropertyChanged("ShipFromCompany", value);
				}
            }
        }

        public virtual string ShipFromCompanyJobTitle
        {
            get
            {
				if (!AllowNull && _shipFromCompanyJobTitle is null) 
					_shipFromCompanyJobTitle = String.Empty; 
				return _shipFromCompanyJobTitle?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_shipFromCompanyJobTitle = value.TruncateTo(100); 
					OnPropertyChanged("ShipFromCompanyJobTitle", value);
				}
            }
        }

        public virtual string ShipFromAttention
        {
            get
            {
				if (!AllowNull && _shipFromAttention is null) 
					_shipFromAttention = String.Empty; 
				return _shipFromAttention?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_shipFromAttention = value.TruncateTo(100); 
					OnPropertyChanged("ShipFromAttention", value);
				}
            }
        }

        public virtual string ShipFromAddressLine1
        {
            get
            {
				if (!AllowNull && _shipFromAddressLine1 is null) 
					_shipFromAddressLine1 = String.Empty; 
				return _shipFromAddressLine1?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_shipFromAddressLine1 = value.TruncateTo(200); 
					OnPropertyChanged("ShipFromAddressLine1", value);
				}
            }
        }

        public virtual string ShipFromAddressLine2
        {
            get
            {
				if (!AllowNull && _shipFromAddressLine2 is null) 
					_shipFromAddressLine2 = String.Empty; 
				return _shipFromAddressLine2?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_shipFromAddressLine2 = value.TruncateTo(200); 
					OnPropertyChanged("ShipFromAddressLine2", value);
				}
            }
        }

        public virtual string ShipFromAddressLine3
        {
            get
            {
				if (!AllowNull && _shipFromAddressLine3 is null) 
					_shipFromAddressLine3 = String.Empty; 
				return _shipFromAddressLine3?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_shipFromAddressLine3 = value.TruncateTo(200); 
					OnPropertyChanged("ShipFromAddressLine3", value);
				}
            }
        }

        public virtual string ShipFromCity
        {
            get
            {
				if (!AllowNull && _shipFromCity is null) 
					_shipFromCity = String.Empty; 
				return _shipFromCity?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_shipFromCity = value.TruncateTo(100); 
					OnPropertyChanged("ShipFromCity", value);
				}
            }
        }

        public virtual string ShipFromState
        {
            get
            {
				if (!AllowNull && _shipFromState is null) 
					_shipFromState = String.Empty; 
				return _shipFromState?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_shipFromState = value.TruncateTo(50); 
					OnPropertyChanged("ShipFromState", value);
				}
            }
        }

        public virtual string ShipFromStateFullName
        {
            get
            {
				if (!AllowNull && _shipFromStateFullName is null) 
					_shipFromStateFullName = String.Empty; 
				return _shipFromStateFullName?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_shipFromStateFullName = value.TruncateTo(100); 
					OnPropertyChanged("ShipFromStateFullName", value);
				}
            }
        }

        public virtual string ShipFromPostalCode
        {
            get
            {
				if (!AllowNull && _shipFromPostalCode is null) 
					_shipFromPostalCode = String.Empty; 
				return _shipFromPostalCode?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_shipFromPostalCode = value.TruncateTo(50); 
					OnPropertyChanged("ShipFromPostalCode", value);
				}
            }
        }

        public virtual string ShipFromPostalCodeExt
        {
            get
            {
				if (!AllowNull && _shipFromPostalCodeExt is null) 
					_shipFromPostalCodeExt = String.Empty; 
				return _shipFromPostalCodeExt?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_shipFromPostalCodeExt = value.TruncateTo(50); 
					OnPropertyChanged("ShipFromPostalCodeExt", value);
				}
            }
        }

        public virtual string ShipFromCounty
        {
            get
            {
				if (!AllowNull && _shipFromCounty is null) 
					_shipFromCounty = String.Empty; 
				return _shipFromCounty?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_shipFromCounty = value.TruncateTo(50); 
					OnPropertyChanged("ShipFromCounty", value);
				}
            }
        }

        public virtual string ShipFromCountry
        {
            get
            {
				if (!AllowNull && _shipFromCountry is null) 
					_shipFromCountry = String.Empty; 
				return _shipFromCountry?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_shipFromCountry = value.TruncateTo(100); 
					OnPropertyChanged("ShipFromCountry", value);
				}
            }
        }

        public virtual string ShipFromEmail
        {
            get
            {
				if (!AllowNull && _shipFromEmail is null) 
					_shipFromEmail = String.Empty; 
				return _shipFromEmail?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_shipFromEmail = value.TruncateTo(100); 
					OnPropertyChanged("ShipFromEmail", value);
				}
            }
        }

        public virtual string ShipFromDaytimePhone
        {
            get
            {
				if (!AllowNull && _shipFromDaytimePhone is null) 
					_shipFromDaytimePhone = String.Empty; 
				return _shipFromDaytimePhone?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_shipFromDaytimePhone = value.TruncateTo(50); 
					OnPropertyChanged("ShipFromDaytimePhone", value);
				}
            }
        }

        public virtual string ShipFromNightPhone
        {
            get
            {
				if (!AllowNull && _shipFromNightPhone is null) 
					_shipFromNightPhone = String.Empty; 
				return _shipFromNightPhone?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_shipFromNightPhone = value.TruncateTo(50); 
					OnPropertyChanged("ShipFromNightPhone", value);
				}
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

        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			WarehouseUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override Warehouse Clear()
        {
            base.Clear();
			_databaseNum = default(int); 
			_masterAccountNum = default(int); 
			_profileNum = default(int); 
			_warehouseUuid = AllowNull ? (string)null : String.Empty; 
			_distributionCenterNum = AllowNull ? (int?)null : default(int); 
			_distributionCenterName = AllowNull ? (string)null : String.Empty; 
			_distributionCenterCode = AllowNull ? (string)null : String.Empty; 
			_distributionCenterType = AllowNull ? (int?)null : default(int); 
			_warehouseType = AllowNull ? (int?)null : default(int); 
			_warehouseStatus = AllowNull ? (int?)null : default(int); 
			_priority = AllowNull ? (int?)null : default(int); 
			_warehouseNum = AllowNull ? (string)null : String.Empty; 
			_warehouseName = AllowNull ? (string)null : String.Empty; 
			_customerUuid = AllowNull ? (string)null : String.Empty; 
			_vendorUuid = AllowNull ? (string)null : String.Empty; 
			_shippingCarrier = AllowNull ? (string)null : String.Empty; 
			_shippingClass = AllowNull ? (string)null : String.Empty; 
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
			_shipFromName = AllowNull ? (string)null : String.Empty; 
			_shipFromFirstName = AllowNull ? (string)null : String.Empty; 
			_shipFromLastName = AllowNull ? (string)null : String.Empty; 
			_shipFromSuffix = AllowNull ? (string)null : String.Empty; 
			_shipFromCompany = AllowNull ? (string)null : String.Empty; 
			_shipFromCompanyJobTitle = AllowNull ? (string)null : String.Empty; 
			_shipFromAttention = AllowNull ? (string)null : String.Empty; 
			_shipFromAddressLine1 = AllowNull ? (string)null : String.Empty; 
			_shipFromAddressLine2 = AllowNull ? (string)null : String.Empty; 
			_shipFromAddressLine3 = AllowNull ? (string)null : String.Empty; 
			_shipFromCity = AllowNull ? (string)null : String.Empty; 
			_shipFromState = AllowNull ? (string)null : String.Empty; 
			_shipFromStateFullName = AllowNull ? (string)null : String.Empty; 
			_shipFromPostalCode = AllowNull ? (string)null : String.Empty; 
			_shipFromPostalCodeExt = AllowNull ? (string)null : String.Empty; 
			_shipFromCounty = AllowNull ? (string)null : String.Empty; 
			_shipFromCountry = AllowNull ? (string)null : String.Empty; 
			_shipFromEmail = AllowNull ? (string)null : String.Empty; 
			_shipFromDaytimePhone = AllowNull ? (string)null : String.Empty; 
			_shipFromNightPhone = AllowNull ? (string)null : String.Empty; 
			_updateDateUtc = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_enterBy = String.Empty; 
			_updateBy = String.Empty; 
            ClearChildren();
            return this;
        }

        public virtual Warehouse ClearChildren()
        {
            return this;
        }

        public virtual Warehouse NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(Warehouse data)
        {
            if (data is null) return;
            return;
        }

		public static IList<Warehouse> FindByDistributionCenterNum(IDataBaseFactory dbFactory, int? distributionCenterNum)
		{
			return dbFactory.Find<Warehouse>("WHERE DistributionCenterNum = @0 ORDER BY Priority ", distributionCenterNum).ToList();
		}
		public static long CountByDistributionCenterNum(IDataBaseFactory dbFactory, int? distributionCenterNum)
		{
			return dbFactory.Count<Warehouse>("WHERE DistributionCenterNum = @0 ", distributionCenterNum);
		}
		public static async Task<IList<Warehouse>> FindByAsyncDistributionCenterNum(IDataBaseFactory dbFactory, int? distributionCenterNum)
		{
			return (await dbFactory.FindAsync<Warehouse>("WHERE DistributionCenterNum = @0 ORDER BY Priority ", distributionCenterNum)).ToList();
		}
		public static async Task<long> CountByAsyncDistributionCenterNum(IDataBaseFactory dbFactory, int? distributionCenterNum)
		{
			return await dbFactory.CountAsync<Warehouse>("WHERE DistributionCenterNum = @0 ", distributionCenterNum);
		}


        #endregion Methods - Generated 
    }
}


