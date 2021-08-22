              
              
    

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
    /// Represents a OrderShipmentPackage.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("OrderShipmentPackage")]
    [PrimaryKey("OrderShipmentPackageNum", AutoIncrement = true)]
    [UniqueId("OrderShipmentPackageUuid")]
    [DtoName("OrderShipmentPackageDto")]
    public partial class OrderShipmentPackage : TableRepository<OrderShipmentPackage, long>
    {

        public OrderShipmentPackage() : base() {}
        public OrderShipmentPackage(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
		[ResultColumn(Name = "OrderShipmentPackageNum", IncludeInAutoSelect = IncludeInAutoSelect.Yes)] 
		protected long _orderShipmentPackageNum; 
		[XmlIgnore, IgnoreCompare] 
		public virtual long OrderShipmentPackageNum
		{
			get => _orderShipmentPackageNum;
			set => _orderShipmentPackageNum = value;
		}
		[XmlIgnore, IgnoreCompare] 
		public override long RowNum
		{
			get => OrderShipmentPackageNum.ToLong();
			set => OrderShipmentPackageNum = value.ToLong();
		}
		[JsonIgnore, XmlIgnore, IgnoreCompare] 
		public override bool IsNew => OrderShipmentPackageNum <= 0; 
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

        [Column("OrderShipmentNum",SqlDbType.BigInt,IsDefault=true)]
        private long? _orderShipmentNum;

        [Column("PackageID",SqlDbType.NVarChar,IsDefault=true)]
        private string _packageID;

        [Column("PackageType",SqlDbType.Int,IsDefault=true)]
        private int? _packageType;

        [Column("PackagePatternNum",SqlDbType.Int,IsDefault=true)]
        private int? _packagePatternNum;

        [Column("PackageTrackingNumber",SqlDbType.VarChar,IsDefault=true)]
        private string _packageTrackingNumber;

        [Column("PackageReturnTrackingNumber",SqlDbType.VarChar,IsDefault=true)]
        private string _packageReturnTrackingNumber;

        [Column("PackageWeight",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _packageWeight;

        [Column("PackageLength",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _packageLength;

        [Column("PackageWidth",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _packageWidth;

        [Column("PackageHeight",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _packageHeight;

        [Column("PackageVolume",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _packageVolume;

        [Column("PackageQty",SqlDbType.Decimal,IsDefault=true)]
        private decimal? _packageQty;

        [Column("ParentPackageNum",SqlDbType.BigInt,IsDefault=true)]
        private long? _parentPackageNum;

        [Column("HasChildPackage",SqlDbType.Bit,IsDefault=true)]
        private bool? _hasChildPackage;

        [Column("OrderShipmentUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _orderShipmentUuid;

        [Column("OrderShipmentPackageUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _orderShipmentPackageUuid;

        #endregion Fields - Generated 

        #region Properties - Generated 
		[IgnoreCompare] 
		public override string UniqueId => OrderShipmentPackageUuid; 
		public override void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(OrderShipmentPackageUuid)) 
				OrderShipmentPackageUuid = Guid.NewGuid().ToString(); 
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
		/// (Readonly) Shipment Unique Number. Required, <br> Title: Shipment Number Display: true, Editable: false.
		/// </summary>
        public virtual long? OrderShipmentNum
        {
            get
            {
				if (!AllowNull && _orderShipmentNum is null) 
					_orderShipmentNum = default(long); 
				return _orderShipmentNum; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_orderShipmentNum = value; 
					OnPropertyChanged("OrderShipmentNum", value);
				}
            }
        }

		/// <summary>
		/// (Readonly) Package ID. <br> Title: Package Id, Display: true, Editable: false
		/// </summary>
        public virtual string PackageID
        {
            get
            {
				if (!AllowNull && _packageID is null) 
					_packageID = String.Empty; 
				return _packageID?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_packageID = value.TruncateTo(50); 
					OnPropertyChanged("PackageID", value);
				}
            }
        }

		/// <summary>
		/// Package Type. <br> Title: Package Type, Display: true, Editable: true
		/// </summary>
        public virtual int? PackageType
        {
            get
            {
				if (!AllowNull && _packageType is null) 
					_packageType = default(int); 
				return _packageType; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_packageType = value; 
					OnPropertyChanged("PackageType", value);
				}
            }
        }

		/// <summary>
		/// Package Pattern. <br> Title: Package Pattern, Display: true, Editable: true
		/// </summary>
        public virtual int? PackagePatternNum
        {
            get
            {
				if (!AllowNull && _packagePatternNum is null) 
					_packagePatternNum = default(int); 
				return _packagePatternNum; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_packagePatternNum = value; 
					OnPropertyChanged("PackagePatternNum", value);
				}
            }
        }

		/// <summary>
		/// Package TrackingNumber. <br> Title: Tracking Number, Display: true, Editable: true
		/// </summary>
        public virtual string PackageTrackingNumber
        {
            get
            {
				if (!AllowNull && _packageTrackingNumber is null) 
					_packageTrackingNumber = String.Empty; 
				return _packageTrackingNumber?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_packageTrackingNumber = value.TruncateTo(50); 
					OnPropertyChanged("PackageTrackingNumber", value);
				}
            }
        }

		/// <summary>
		/// Return TrackingNumber. <br> Title: Return Tracking Number, Display: true, Editable: true
		/// </summary>
        public virtual string PackageReturnTrackingNumber
        {
            get
            {
				if (!AllowNull && _packageReturnTrackingNumber is null) 
					_packageReturnTrackingNumber = String.Empty; 
				return _packageReturnTrackingNumber?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_packageReturnTrackingNumber = value.TruncateTo(50); 
					OnPropertyChanged("PackageReturnTrackingNumber", value);
				}
            }
        }

		/// <summary>
		/// Weight. <br> Title: Weight, Display: true, Editable: true
		/// </summary>
        public virtual decimal? PackageWeight
        {
            get
            {
				if (!AllowNull && _packageWeight is null) 
					_packageWeight = default(decimal); 
				return _packageWeight; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_packageWeight = value; 
					OnPropertyChanged("PackageWeight", value);
				}
            }
        }

		/// <summary>
		/// Length. <br> Title: Length, Display: true, Editable: true
		/// </summary>
        public virtual decimal? PackageLength
        {
            get
            {
				if (!AllowNull && _packageLength is null) 
					_packageLength = default(decimal); 
				return _packageLength; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_packageLength = value; 
					OnPropertyChanged("PackageLength", value);
				}
            }
        }

		/// <summary>
		/// Width. <br> Title: Width, Display: true, Editable: true
		/// </summary>
        public virtual decimal? PackageWidth
        {
            get
            {
				if (!AllowNull && _packageWidth is null) 
					_packageWidth = default(decimal); 
				return _packageWidth; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_packageWidth = value; 
					OnPropertyChanged("PackageWidth", value);
				}
            }
        }

		/// <summary>
		/// Height. <br> Title: Height, Display: true, Editable: true
		/// </summary>
        public virtual decimal? PackageHeight
        {
            get
            {
				if (!AllowNull && _packageHeight is null) 
					_packageHeight = default(decimal); 
				return _packageHeight; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_packageHeight = value; 
					OnPropertyChanged("PackageHeight", value);
				}
            }
        }

		/// <summary>
		/// Volume. <br> Title: Volume, Display: true, Editable: true
		/// </summary>
        public virtual decimal? PackageVolume
        {
            get
            {
				if (!AllowNull && _packageVolume is null) 
					_packageVolume = default(decimal); 
				return _packageVolume; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_packageVolume = value; 
					OnPropertyChanged("PackageVolume", value);
				}
            }
        }

		/// <summary>
		/// Qty. <br> Title: Qty, Display: true, Editable: true
		/// </summary>
        public virtual decimal? PackageQty
        {
            get
            {
				if (!AllowNull && _packageQty is null) 
					_packageQty = default(decimal); 
				return _packageQty; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_packageQty = value; 
					OnPropertyChanged("PackageQty", value);
				}
            }
        }

		/// <summary>
		/// Parent Package Num. <br> Title: Parent Package, Display: true, Editable: true
		/// </summary>
        public virtual long? ParentPackageNum
        {
            get
            {
				if (!AllowNull && _parentPackageNum is null) 
					_parentPackageNum = default(long); 
				return _parentPackageNum; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_parentPackageNum = value; 
					OnPropertyChanged("ParentPackageNum", value);
				}
            }
        }

		/// <summary>
		/// Has Child Package. <br> Title: Has Child, Display: true, Editable: true
		/// </summary>
        public virtual bool? HasChildPackage
        {
            get
            {
				if (!AllowNull && _hasChildPackage is null) 
					_hasChildPackage = false; 
				return _hasChildPackage; 
            }
            set
            {
				if (value != null || AllowNull) 
				{
					_hasChildPackage = value; 
					OnPropertyChanged("HasChildPackage", value);
				}
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

		/// <summary>
		/// Shipment Pachage uuid. <br> Display: false, Editable: false.
		/// </summary>
        public virtual string OrderShipmentPackageUuid
        {
            get
            {
				return _orderShipmentPackageUuid?.TrimEnd(); 
            }
            set
            {
				_orderShipmentPackageUuid = value.TruncateTo(50); 
				OnPropertyChanged("OrderShipmentPackageUuid", value);
            }
        }



        #endregion Properties - Generated 

        #region Methods - Parent

		[JsonIgnore, XmlIgnore, IgnoreCompare]
		private OrderShipmentData Parent { get; set; }
		public OrderShipmentData GetParent() => Parent;
		public OrderShipmentPackage SetParent(OrderShipmentData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent

		#region Methods - Children OrderShipmentShippedItem
		protected IList<OrderShipmentShippedItem> _OrderShipmentShippedItemDeleted;
		public IList<OrderShipmentShippedItem> AddOrderShipmentShippedItemDeleted(IList<OrderShipmentShippedItem> del)
		{
			if (_OrderShipmentShippedItemDeleted is null)
				_OrderShipmentShippedItemDeleted = new List<OrderShipmentShippedItem>();
			foreach (var d in del)
				_OrderShipmentShippedItemDeleted.Add(d);
			return del;
		}
		public OrderShipmentShippedItem AddOrderShipmentShippedItemDeleted(OrderShipmentShippedItem del)
		{
			if (_OrderShipmentShippedItemDeleted is null)
				_OrderShipmentShippedItemDeleted = new List<OrderShipmentShippedItem>();
			_OrderShipmentShippedItemDeleted.Add(del);
			return del;
		}
		public void SetOrderShipmentShippedItemDeleted(IList<OrderShipmentShippedItem> del) =>
			_OrderShipmentShippedItemDeleted = del;
		public void ClearOrderShipmentShippedItemDeleted() =>
			_OrderShipmentShippedItemDeleted.Clear();
		protected IList<OrderShipmentShippedItem> _OrderShipmentShippedItem;
		[IgnoreCompare]
		public IList<OrderShipmentShippedItem> OrderShipmentShippedItem
		{
			get
			{
				if (_OrderShipmentShippedItem is null)
					OrderShipmentShippedItem = new List<OrderShipmentShippedItem>();
				return _OrderShipmentShippedItem;
			}
			set
			{
				_OrderShipmentShippedItem = value;
				CheckIntegrityOrderShipmentShippedItem();
			}
		}
		public IList<OrderShipmentShippedItem> SetChildrenOrderShipmentShippedItem(IList<OrderShipmentShippedItem> children)
		{
			var childrenList = children.ToList();
			OrderShipmentShippedItem = childrenList.Where(x => !string.IsNullOrEmpty(OrderShipmentPackageUuid) && x.OrderShipmentPackageUuid == OrderShipmentPackageUuid).ToList();
			return OrderShipmentShippedItem;
		}
		public IList<OrderShipmentShippedItem> GetChildrenOrderShipmentShippedItem()
		{
			return OrderShipmentShippedItem;
		}
		public IList<OrderShipmentShippedItem> GetChildrenDeletedOrderShipmentShippedItem()
		{
			return _OrderShipmentShippedItemDeleted;
		}
		public IList<OrderShipmentShippedItem> CheckIntegrityOrderShipmentShippedItem()
		{
			if (OrderShipmentShippedItem == null)
				return OrderShipmentShippedItem;
			foreach (var child in OrderShipmentShippedItem.Where(x => x != null))
				CheckIntegrityOrderShipmentShippedItem(child);
			return OrderShipmentShippedItem;
		}
		public OrderShipmentShippedItem CheckIntegrityOrderShipmentShippedItem(OrderShipmentShippedItem child)
		{
			if (child == null)
				return child;
			CheckUniqueId();
			child.SetParent(Parent);
			if (child.OrderShipmentPackageUuid != OrderShipmentPackageUuid) child.OrderShipmentPackageUuid = OrderShipmentPackageUuid;
			return child;
		}
		public IList<OrderShipmentShippedItem> LoadOrderShipmentShippedItem()
		{
			if (string.IsNullOrEmpty(OrderShipmentPackageUuid)) return null;
			OrderShipmentShippedItem = dbFactory.Find<OrderShipmentShippedItem>("WHERE OrderShipmentPackageUuid = @0 ORDER BY RowNum ", OrderShipmentPackageUuid).ToList();
			return OrderShipmentShippedItem;
		}
		public async Task<IList<OrderShipmentShippedItem>> LoadOrderShipmentShippedItemAsync()
		{
			if (string.IsNullOrEmpty(OrderShipmentPackageUuid)) return null;
			OrderShipmentShippedItem = (await dbFactory.FindAsync<OrderShipmentShippedItem>("WHERE OrderShipmentPackageUuid = @0 ORDER BY RowNum ", OrderShipmentPackageUuid)).ToList();
			return OrderShipmentShippedItem;
		}
		public OrderShipmentShippedItem NewOrderShipmentShippedItem()
		{
			CheckUniqueId();
			var child = new OrderShipmentShippedItem(dbFactory);
			child.SetParent(Parent);
			child.OrderShipmentPackageUuid = OrderShipmentPackageUuid;
			return child;
		}
		public IList<OrderShipmentShippedItem> AddOrderShipmentShippedItem(OrderShipmentShippedItem child)
		{
			if (child == null)
				child = NewOrderShipmentShippedItem();
			CheckIntegrityOrderShipmentShippedItem(child);
			OrderShipmentShippedItem.AddOrReplace(child);
			return OrderShipmentShippedItem;
		}
		public IList<OrderShipmentShippedItem> RemoveOrderShipmentShippedItem(OrderShipmentShippedItem child)
		{
			if (child == null) return null;
			OrderShipmentShippedItem.Remove(child);
			return OrderShipmentShippedItem;
		}
		#endregion Methods - Children OrderShipmentShippedItem

        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			OrderShipmentPackageUuid = Guid.NewGuid().ToString(); 
			_orderShipmentPackageNum = 0; 
            return;
        }

        public override OrderShipmentPackage Clear()
        {
            base.Clear();
			_orderShipmentPackageNum = default(long); 
			_databaseNum = default(int); 
			_masterAccountNum = default(int); 
			_profileNum = default(int); 
			_channelNum = default(int); 
			_channelAccountNum = default(int); 
			_orderShipmentNum = AllowNull ? (long?)null : default(long); 
			_packageID = AllowNull ? (string)null : String.Empty; 
			_packageType = AllowNull ? (int?)null : default(int); 
			_packagePatternNum = AllowNull ? (int?)null : default(int); 
			_packageTrackingNumber = AllowNull ? (string)null : String.Empty; 
			_packageReturnTrackingNumber = AllowNull ? (string)null : String.Empty; 
			_packageWeight = AllowNull ? (decimal?)null : default(decimal); 
			_packageLength = AllowNull ? (decimal?)null : default(decimal); 
			_packageWidth = AllowNull ? (decimal?)null : default(decimal); 
			_packageHeight = AllowNull ? (decimal?)null : default(decimal); 
			_packageVolume = AllowNull ? (decimal?)null : default(decimal); 
			_packageQty = AllowNull ? (decimal?)null : default(decimal); 
			_parentPackageNum = AllowNull ? (long?)null : default(long); 
			_hasChildPackage = AllowNull ? (bool?)null : false; 
			_orderShipmentUuid = String.Empty; 
			_orderShipmentPackageUuid = String.Empty; 
            ClearChildren();
            return this;
        }

        public override OrderShipmentPackage CheckIntegrity()
        {
            CheckUniqueId();
			CheckIntegrityOrderShipmentShippedItem();
            CheckIntegrityOthers();
            return this;
        }

        public virtual OrderShipmentPackage ClearChildren()
        {
			OrderShipmentShippedItem = new List<OrderShipmentShippedItem>();
            return this;
        }

        public virtual OrderShipmentPackage NewChildren()
        {
			OrderShipmentShippedItem = new List<OrderShipmentShippedItem>();
            return this;
        }

        public virtual void CopyChildrenFrom(OrderShipmentPackage data)
        {
            if (data is null) return;
			var lstDeleted = OrderShipmentShippedItem?.CopyFrom(data.OrderShipmentShippedItem); 
			SetOrderShipmentShippedItemDeleted(lstDeleted); 
			CheckIntegrityOrderShipmentShippedItem(); 
            return;
        }

		public static IList<OrderShipmentPackage> FindByOrderShipmentUuid(IDataBaseFactory dbFactory, string orderShipmentUuid)
		{
			return dbFactory.Find<OrderShipmentPackage>("WHERE OrderShipmentUuid = @0 ORDER BY OrderShipmentPackageNum ", orderShipmentUuid).ToList();
		}
		public static long CountByOrderShipmentUuid(IDataBaseFactory dbFactory, string orderShipmentUuid)
		{
			return dbFactory.Count<OrderShipmentPackage>("WHERE OrderShipmentUuid = @0 ", orderShipmentUuid);
		}
		public static async Task<IList<OrderShipmentPackage>> FindByAsyncOrderShipmentUuid(IDataBaseFactory dbFactory, string orderShipmentUuid)
		{
			return (await dbFactory.FindAsync<OrderShipmentPackage>("WHERE OrderShipmentUuid = @0 ORDER BY OrderShipmentPackageNum ", orderShipmentUuid)).ToList();
		}
		public static async Task<long> CountByAsyncOrderShipmentUuid(IDataBaseFactory dbFactory, string orderShipmentUuid)
		{
			return await dbFactory.CountAsync<OrderShipmentPackage>("WHERE OrderShipmentUuid = @0 ", orderShipmentUuid);
		}


        #endregion Methods - Generated 
    }
}



