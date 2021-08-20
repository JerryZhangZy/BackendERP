              
              
    

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
    /// Represents a OrderShipmentHeader.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("OrderShipmentHeader")]
    [PrimaryKey("OrderShipmentNum", AutoIncrement = true)]
    [UniqueId("OrderShipmentUuid")]
    [DtoName("OrderShipmentHeaderDto")]
    public partial class OrderShipmentHeader : TableRepository<OrderShipmentHeader, long>
    {

        public OrderShipmentHeader() : base() {}
        public OrderShipmentHeader(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
		[ResultColumn(Name = "OrderShipmentNum", IncludeInAutoSelect = IncludeInAutoSelect.Yes)] 
		protected long _orderShipmentNum; 
		[XmlIgnore, IgnoreCompare] 
		public virtual long OrderShipmentNum
		{
			get => _orderShipmentNum;
			set => _orderShipmentNum = value;
		}
		[XmlIgnore, IgnoreCompare] 
		public override long RowNum
		{
			get => OrderShipmentNum.ToLong();
			set => OrderShipmentNum = value.ToLong();
		}
		[JsonIgnore, XmlIgnore, IgnoreCompare] 
		public override bool IsNew => OrderShipmentNum <= 0; 
        [Column("DatabaseNum",SqlDbType.Int,NotNull=true)]
        private int _databaseNum;

        [Column("MasterAccountNum",SqlDbType.Int,NotNull=true)]
        private int _masterAccountNum;

        [Column("ProfileNum",SqlDbType.Int,NotNull=true)]
        private int _profileNum;

        [Column("ChannelNum",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _channelNum;

        [Column("ChannelAccountNum",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _channelAccountNum;

        [Column("OrderDCAssignmentNum",SqlDbType.BigInt,IsDefault=true)]
        private long? _orderDCAssignmentNum;

        [Column("DistributionCenterNum",SqlDbType.Int,IsDefault=true)]
        private int? _distributionCenterNum;

        [Column("CentralOrderNum",SqlDbType.BigInt,IsDefault=true)]
        private long? _centralOrderNum;

        [Column("ChannelOrderID",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _channelOrderID;

        [Column("ShipmentID",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _shipmentID;

        [Column("WarehouseCode",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _warehouseCode;

        [Column("ShipmentType",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _shipmentType;

        [Column("ShipmentReferenceID",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _shipmentReferenceID;

        [Column("ShipmentDateUtc",SqlDbType.DateTime)]
        private DateTime? _shipmentDateUtc;

        [Column("ShippingCarrier",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _shippingCarrier;

        [Column("ShippingClass",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _shippingClass;

        [Column("ShippingCost",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _shippingCost;

        [Column("MainTrackingNumber",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _mainTrackingNumber;

        [Column("MainReturnTrackingNumber",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _mainReturnTrackingNumber;

        [Column("BillOfLadingID",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _billOfLadingID;

        [Column("TotalPackages",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _totalPackages;

        [Column("TotalShippedQty",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _totalShippedQty;

        [Column("TotalCanceledQty",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _totalCanceledQty;

        [Column("TotalWeight",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _totalWeight;

        [Column("TotalVolume",SqlDbType.Decimal,NotNull=true,IsDefault=true)]
        private decimal _totalVolume;

        [Column("WeightUnit",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _weightUnit;

        [Column("LengthUnit",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _lengthUnit;

        [Column("VolumeUnit",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _volumeUnit;

        [Column("ShipmentStatus",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _shipmentStatus;

        [Column("DBChannelOrderHeaderRowID",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _dBChannelOrderHeaderRowID;

        [Column("ProcessStatus",SqlDbType.Int,NotNull=true,IsDefault=true)]
        private int _processStatus;

        [Column("ProcessDateUtc",SqlDbType.DateTime,NotNull=true)]
        private DateTime _processDateUtc;

        [Column("OrderShipmentUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _orderShipmentUuid;

        #endregion Fields - Generated 

        #region Properties - Generated 
		[IgnoreCompare] 
		public override string UniqueId => OrderShipmentUuid; 
		public void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(OrderShipmentUuid)) 
				OrderShipmentUuid = Guid.NewGuid().ToString(); 
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
		/// (Readonly) The channel which sells the item. Refer to Master Account Channel Setting. <br> Title: Channel: Display: true, Editable: false
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
		/// (Readonly) The unique number of this profile’s channel account. <br> Title: Shipping Carrier: Display: false, Editable: false
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
		/// (Readonly) The unique number of Order DC Assignment. <br> Title: Assignment Number: Display: true, Editable: false
		/// </summary>
        public virtual long? OrderDCAssignmentNum
        {
            get
            {
				if (!AllowNull && _orderDCAssignmentNum is null) 
					_orderDCAssignmentNum = default(long); 
				return _orderDCAssignmentNum; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_orderDCAssignmentNum = value; 
					OnPropertyChanged("OrderDCAssignmentNum", value);
				}
            }
        }

		/// <summary>
		/// (Readonly) DC number. <br> Title: DC Number: Display: true, Editable: false
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
		/// (Readonly) CentralOrderNum. <br> Title: Central Order: Display: true, Editable: false
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
		/// (Readonly) This usually is the marketplace order ID, or merchant PO Number. <br> Title: Channel Order: Display: true, Editable: false
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
		/// (Readonly) Shipment ID. <br> Title: Shipment Id, Display: true, Editable: false
		/// </summary>
        public virtual string ShipmentID
        {
            get
            {
				return _shipmentID?.TrimEnd(); 
            }
            set
            {
				_shipmentID = value.TruncateTo(50); 
				OnPropertyChanged("ShipmentID", value);
            }
        }

		/// <summary>
		/// Warehouse Code. <br> Title: Warehouse Code, Display: true, Editable: true
		/// </summary>
        public virtual string WarehouseCode
        {
            get
            {
				return _warehouseCode?.TrimEnd(); 
            }
            set
            {
				_warehouseCode = value.TruncateTo(50); 
				OnPropertyChanged("WarehouseCode", value);
            }
        }

		/// <summary>
		/// Shipment Type. <br> Title: Shipment Type, Display: true, Editable: true
		/// </summary>
        public virtual int ShipmentType
        {
            get
            {
				return _shipmentType; 
            }
            set
            {
				_shipmentType = value; 
				OnPropertyChanged("ShipmentType", value);
            }
        }

		/// <summary>
		/// Ref Id. <br> Title: Reference, Display: true, Editable: true
		/// </summary>
        public virtual string ShipmentReferenceID
        {
            get
            {
				return _shipmentReferenceID?.TrimEnd(); 
            }
            set
            {
				_shipmentReferenceID = value.TruncateTo(50); 
				OnPropertyChanged("ShipmentReferenceID", value);
            }
        }

		/// <summary>
		/// Ship Date. <br> Title: Ship Date, Display: true, Editable: true
		/// </summary>
        public virtual DateTime? ShipmentDateUtc
        {
            get
            {
				if (!AllowNull && _shipmentDateUtc is null) 
					_shipmentDateUtc = new DateTime().MinValueSql(); 
				return _shipmentDateUtc; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_shipmentDateUtc = (value is null) ? (DateTime?) null : value?.Date.ToSqlSafeValue(); 
					OnPropertyChanged("ShipmentDateUtc", value);
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
				return _shippingCarrier?.TrimEnd(); 
            }
            set
            {
				_shippingCarrier = value.TruncateTo(50); 
				OnPropertyChanged("ShippingCarrier", value);
            }
        }

		/// <summary>
		/// Shipping Method. <br> Title: Shipping Method: Display: true, Editable: true
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
		/// Shipping fee. <br> Title: Shipping Fee, Display: true, Editable: true
		/// </summary>
        public virtual decimal ShippingCost
        {
            get
            {
				return _shippingCost; 
            }
            set
            {
				_shippingCost = value; 
				OnPropertyChanged("ShippingCost", value);
            }
        }

		/// <summary>
		/// Master TrackingNumber. <br> Title: Tracking Number, Display: true, Editable: true
		/// </summary>
        public virtual string MainTrackingNumber
        {
            get
            {
				return _mainTrackingNumber?.TrimEnd(); 
            }
            set
            {
				_mainTrackingNumber = value.TruncateTo(50); 
				OnPropertyChanged("MainTrackingNumber", value);
            }
        }

		/// <summary>
		/// Master Return TrackingNumber. <br> Title: Return Tracking Number, Display: true, Editable: true
		/// </summary>
        public virtual string MainReturnTrackingNumber
        {
            get
            {
				return _mainReturnTrackingNumber?.TrimEnd(); 
            }
            set
            {
				_mainReturnTrackingNumber = value.TruncateTo(50); 
				OnPropertyChanged("MainReturnTrackingNumber", value);
            }
        }

		/// <summary>
		/// Bill Of Lading ID. <br> Title: BOL Id, Display: true, Editable: true
		/// </summary>
        public virtual string BillOfLadingID
        {
            get
            {
				return _billOfLadingID?.TrimEnd(); 
            }
            set
            {
				_billOfLadingID = value.TruncateTo(50); 
				OnPropertyChanged("BillOfLadingID", value);
            }
        }

		/// <summary>
		/// Total Packages. <br> Title: Number of Package, Display: true, Editable: true
		/// </summary>
        public virtual int TotalPackages
        {
            get
            {
				return _totalPackages; 
            }
            set
            {
				_totalPackages = value; 
				OnPropertyChanged("TotalPackages", value);
            }
        }

		/// <summary>
		/// Total Shipped Qty. <br> Title: Shipped Qty, Display: true, Editable: true
		/// </summary>
        public virtual decimal TotalShippedQty
        {
            get
            {
				return _totalShippedQty; 
            }
            set
            {
				_totalShippedQty = value; 
				OnPropertyChanged("TotalShippedQty", value);
            }
        }

		/// <summary>
		/// Total Cancelled Qty. <br> Title: Cancelled Qty, Display: true, Editable: true
		/// </summary>
        public virtual decimal TotalCanceledQty
        {
            get
            {
				return _totalCanceledQty; 
            }
            set
            {
				_totalCanceledQty = value; 
				OnPropertyChanged("TotalCanceledQty", value);
            }
        }

		/// <summary>
		/// Total Weight. <br> Title: Weight, Display: true, Editable: true
		/// </summary>
        public virtual decimal TotalWeight
        {
            get
            {
				return _totalWeight; 
            }
            set
            {
				_totalWeight = value; 
				OnPropertyChanged("TotalWeight", value);
            }
        }

		/// <summary>
		/// Total Volume. <br> Title: Volume, Display: true, Editable: true
		/// </summary>
        public virtual decimal TotalVolume
        {
            get
            {
				return _totalVolume; 
            }
            set
            {
				_totalVolume = value; 
				OnPropertyChanged("TotalVolume", value);
            }
        }

		/// <summary>
		/// Weight Unit. <br> Title: Weight Unit, Display: true, Editable: true
		/// </summary>
        public virtual int WeightUnit
        {
            get
            {
				return _weightUnit; 
            }
            set
            {
				_weightUnit = value; 
				OnPropertyChanged("WeightUnit", value);
            }
        }

		/// <summary>
		/// Length Unit. <br> Title: Length Unit, Display: true, Editable: true
		/// </summary>
        public virtual int LengthUnit
        {
            get
            {
				return _lengthUnit; 
            }
            set
            {
				_lengthUnit = value; 
				OnPropertyChanged("LengthUnit", value);
            }
        }

		/// <summary>
		/// Volume Unit. <br> Title: Volume Unit, Display: true, Editable: true
		/// </summary>
        public virtual int VolumeUnit
        {
            get
            {
				return _volumeUnit; 
            }
            set
            {
				_volumeUnit = value; 
				OnPropertyChanged("VolumeUnit", value);
            }
        }

		/// <summary>
		/// Shipment Status. <br> Title: Shipment Status, Display: true, Editable: true
		/// </summary>
        public virtual int ShipmentStatus
        {
            get
            {
				return _shipmentStatus; 
            }
            set
            {
				_shipmentStatus = value; 
				OnPropertyChanged("ShipmentStatus", value);
            }
        }

		/// <summary>
		/// (Ignore) DBChannelOrderHeaderRowID. <br> Display: false, Editable: false
		/// </summary>
        public virtual string DBChannelOrderHeaderRowID
        {
            get
            {
				return _dBChannelOrderHeaderRowID?.TrimEnd(); 
            }
            set
            {
				_dBChannelOrderHeaderRowID = value.TruncateTo(50); 
				OnPropertyChanged("DBChannelOrderHeaderRowID", value);
            }
        }

		/// <summary>
		/// Process Status. <br> Title: Process Status, Display: true, Editable: false
		/// </summary>
        public virtual int ProcessStatus
        {
            get
            {
				return _processStatus; 
            }
            set
            {
				_processStatus = value; 
				OnPropertyChanged("ProcessStatus", value);
            }
        }

		/// <summary>
		/// Process Date. <br> Title: Process Date, Display: true, Editable: false
		/// </summary>
        public virtual DateTime ProcessDateUtc
        {
            get
            {
				return _processDateUtc; 
            }
            set
            {
				_processDateUtc = value.Date.ToSqlSafeValue(); 
				OnPropertyChanged("ProcessDateUtc", value);
            }
        }

		/// <summary>
		/// Shipment uuid. <br> Display: false, Editable: false.
		/// </summary>
        public virtual string OrderShipmentUuid
        {
            get
            {
				return _orderShipmentUuid?.TrimEnd(); 
            }
            set
            {
				_orderShipmentUuid = value.TruncateTo(50); 
				OnPropertyChanged("OrderShipmentUuid", value);
            }
        }



        #endregion Properties - Generated 

        #region Methods - Parent

		[JsonIgnore, XmlIgnore, IgnoreCompare]
		private OrderShipmentData Parent { get; set; }
		public OrderShipmentData GetParent() => Parent;
		public OrderShipmentHeader SetParent(OrderShipmentData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			OrderShipmentUuid = Guid.NewGuid().ToString(); 
			_orderShipmentNum = 0; 
            return;
        }

        public override OrderShipmentHeader Clear()
        {
            base.Clear();
			_orderShipmentNum = default(long); 
			_databaseNum = default(int); 
			_masterAccountNum = default(int); 
			_profileNum = default(int); 
			_channelNum = default(int); 
			_channelAccountNum = default(int); 
			_orderDCAssignmentNum = AllowNull ? (long?)null : default(long); 
			_distributionCenterNum = AllowNull ? (int?)null : default(int); 
			_centralOrderNum = AllowNull ? (long?)null : default(long); 
			_channelOrderID = String.Empty; 
			_shipmentID = String.Empty; 
			_warehouseCode = String.Empty; 
			_shipmentType = default(int); 
			_shipmentReferenceID = String.Empty; 
			_shipmentDateUtc = AllowNull ? (DateTime?)null : new DateTime().MinValueSql(); 
			_shippingCarrier = String.Empty; 
			_shippingClass = String.Empty; 
			_shippingCost = default(decimal); 
			_mainTrackingNumber = String.Empty; 
			_mainReturnTrackingNumber = String.Empty; 
			_billOfLadingID = String.Empty; 
			_totalPackages = default(int); 
			_totalShippedQty = default(decimal); 
			_totalCanceledQty = default(decimal); 
			_totalWeight = default(decimal); 
			_totalVolume = default(decimal); 
			_weightUnit = default(int); 
			_lengthUnit = default(int); 
			_volumeUnit = default(int); 
			_shipmentStatus = default(int); 
			_dBChannelOrderHeaderRowID = String.Empty; 
			_processStatus = default(int); 
			_processDateUtc = new DateTime().MinValueSql(); 
			_orderShipmentUuid = String.Empty; 
            ClearChildren();
            return this;
        }

        public virtual OrderShipmentHeader CheckIntegrity()
        {
            CheckUniqueId();
            return this;
        }

        public virtual OrderShipmentHeader ClearChildren()
        {
            return this;
        }

        public virtual OrderShipmentHeader NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(OrderShipmentHeader data)
        {
            if (data is null) return;
            return;
        }



        #endregion Methods - Generated 
    }
}



