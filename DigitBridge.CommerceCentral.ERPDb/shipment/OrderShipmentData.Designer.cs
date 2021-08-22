              
    

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
    /// Represents a OrderShipmentData.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    public partial class OrderShipmentData : StructureRepository<OrderShipmentData>
    {
        public OrderShipmentData() : base() {}
        public OrderShipmentData(IDataBaseFactory dbFactory): base(dbFactory) {}

        [JsonIgnore, XmlIgnore]
        public new bool IsNew => OrderShipmentHeader.IsNew;

        [JsonIgnore, XmlIgnore]
        public new string UniqueId => OrderShipmentHeader.UniqueId;
        
		 [JsonIgnore, XmlIgnore] 
		public static string OrderShipmentHeaderTable ="OrderShipmentHeader ";
		
		 [JsonIgnore, XmlIgnore] 
		public static string OrderShipmentCanceledItemTable ="OrderShipmentCanceledItem ";
		
		 [JsonIgnore, XmlIgnore] 
		public static string OrderShipmentPackageTable ="OrderShipmentPackage ";
		
		 [JsonIgnore, XmlIgnore] 
		public static string OrderShipmentShippedItemTable ="OrderShipmentShippedItem ";
		
        #region CRUD Methods

        public override bool Equals(OrderShipmentData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!string.IsNullOrWhiteSpace(UniqueId) && !string.IsNullOrWhiteSpace(other.UniqueId) && !UniqueId.Equals(other.UniqueId)) return false;
            return ChildrenEquals(other);
        }
        public virtual bool ChildrenEquals(OrderShipmentData other)
        {
			if (OrderShipmentHeader == null && other.OrderShipmentHeader != null || OrderShipmentHeader != null && other.OrderShipmentHeader == null) 
				return false; 
			if (OrderShipmentHeader != null && other.OrderShipmentHeader != null && !OrderShipmentHeader.Equals(other.OrderShipmentHeader)) 
				return false; 
			if (OrderShipmentCanceledItem == null && other.OrderShipmentCanceledItem != null || OrderShipmentCanceledItem != null && other.OrderShipmentCanceledItem == null) 
				return false; 
			if (OrderShipmentCanceledItem != null && other.OrderShipmentCanceledItem != null && !OrderShipmentCanceledItem.EqualsList(other.OrderShipmentCanceledItem)) 
				return false; 
			if (OrderShipmentPackage == null && other.OrderShipmentPackage != null || OrderShipmentPackage != null && other.OrderShipmentPackage == null) 
				return false; 
			if (OrderShipmentPackage != null && other.OrderShipmentPackage != null && !OrderShipmentPackage.EqualsList(other.OrderShipmentPackage)) 
				return false; 
			if (OrderShipmentShippedItem == null && other.OrderShipmentShippedItem != null || OrderShipmentShippedItem != null && other.OrderShipmentShippedItem == null) 
				return false; 
			if (OrderShipmentShippedItem != null && other.OrderShipmentShippedItem != null && !OrderShipmentShippedItem.EqualsList(other.OrderShipmentShippedItem)) 
				return false; 
            return true;
        }

        // Check Children table Integrity
        public override OrderShipmentData CheckIntegrity()
        {
			if (OrderShipmentHeader is null) return this; 
			OrderShipmentHeader.CheckUniqueId(); 
			CheckIntegrityOrderShipmentCanceledItem(); 
			CheckIntegrityOrderShipmentPackage(); 
			CheckIntegrityOrderShipmentShippedItem(); 
			CheckIntegrityOthers(); 
            return this;
        }

        partial void ClearOthers();
        public override void Clear()
        {
			OrderShipmentHeader?.Clear(); 
			OrderShipmentCanceledItem = new List<OrderShipmentCanceledItem>(); 
			ClearOrderShipmentCanceledItemDeleted(); 
			OrderShipmentPackage = new List<OrderShipmentPackage>(); 
			ClearOrderShipmentPackageDeleted(); 
			OrderShipmentShippedItem = new List<OrderShipmentShippedItem>(); 
			ClearOthers(); 
			if (_OnClear != null)
				_OnClear(this);
            return;
        }

        public override void New()
        {
            Clear();
			OrderShipmentHeader = NewOrderShipmentHeader(); 
			OrderShipmentCanceledItem = new List<OrderShipmentCanceledItem>(); 
			AddOrderShipmentCanceledItem(NewOrderShipmentCanceledItem()); 
			ClearOrderShipmentCanceledItemDeleted(); 
			OrderShipmentPackage = new List<OrderShipmentPackage>(); 
			AddOrderShipmentPackage(NewOrderShipmentPackage()); 
			OrderShipmentPackage.ToList().ForEach(x => x?.NewChildren()); 
			ClearOrderShipmentPackageDeleted(); 
            return;
        }

        public virtual void CopyFrom(OrderShipmentData data)
        {
			CopyOrderShipmentHeaderFrom(data); 
			CopyOrderShipmentCanceledItemFrom(data); 
			CopyOrderShipmentPackageFrom(data); 
            CheckIntegrity();
            return;
        }

        public override OrderShipmentData Clone()
        {
			var newData = new OrderShipmentData(); 
			newData.New(); 
			newData?.CopyFrom(this); 
			newData.OrderShipmentHeader.ClearMetaData(); 
			newData.OrderShipmentCanceledItem.ClearMetaData(); 
			newData.OrderShipmentPackage.ClearMetaData(); 
			newData.OrderShipmentShippedItem.ClearMetaData(); 
            newData.CheckIntegrity();
            return newData;
        }

        public override bool Get(long RowNum)
        {
			var obj = GetOrderShipmentHeader(RowNum); 
			if (obj is null) return false; 
			OrderShipmentHeader = obj; 
			GetOthers(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        public override bool GetById(string OrderShipmentUuid)
        {
			var obj = GetOrderShipmentHeaderByOrderShipmentUuid(OrderShipmentUuid); 
			if (obj is null) return false; 
			OrderShipmentHeader = obj; 
			GetOthers(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        protected virtual void GetOthers()
        {
            
			if (string.IsNullOrEmpty(OrderShipmentHeader.OrderShipmentUuid)) return; 
			OrderShipmentCanceledItem = GetOrderShipmentCanceledItemByOrderShipmentUuid(OrderShipmentHeader.OrderShipmentUuid); 
			OrderShipmentPackage = GetOrderShipmentPackageByOrderShipmentUuid(OrderShipmentHeader.OrderShipmentUuid); 
			OrderShipmentShippedItem = GetOrderShipmentShippedItemByOrderShipmentUuid(OrderShipmentHeader.OrderShipmentUuid); 
        }

        public override bool Save()
        {
			if (OrderShipmentHeader is null || string.IsNullOrEmpty(OrderShipmentHeader.OrderShipmentUuid)) return false; 
			CheckIntegrity();
			if (_OnBeforeSave != null)
				if (!_OnBeforeSave(this)) return false;
			dbFactory.Begin();

			 if (NeedSave(OrderShipmentHeaderTable))
			{
				OrderShipmentHeader.SetDataBaseFactory(dbFactory);
				if (!OrderShipmentHeader.Save()) return false;
			}

			 if (NeedSave(OrderShipmentCanceledItemTable))
			{
				if (OrderShipmentCanceledItem != null) 
					OrderShipmentCanceledItem.SetDataBaseFactory(dbFactory)?.Save();
				var delOrderShipmentCanceledItem = _OrderShipmentCanceledItemDeleted;
				if (delOrderShipmentCanceledItem != null)
					delOrderShipmentCanceledItem.SetDataBaseFactory(dbFactory)?.Delete();
			}

			 if (NeedSave(OrderShipmentPackageTable))
			{
				if (OrderShipmentPackage != null) 
					OrderShipmentPackage.SetDataBaseFactory(dbFactory)?.Save();
				var delOrderShipmentPackage = _OrderShipmentPackageDeleted;
				if (delOrderShipmentPackage != null)
					delOrderShipmentPackage.SetDataBaseFactory(dbFactory)?.Delete();
			}

			 if (NeedSave(OrderShipmentShippedItemTable))
			{
				if (OrderShipmentShippedItem != null) 
					OrderShipmentShippedItem.SetDataBaseFactory(dbFactory)?.Save();
				var delChildrenOrderShipmentShippedItem = OrderShipmentShippedItemDeleted;
				if (delChildrenOrderShipmentShippedItem != null)
					delChildrenOrderShipmentShippedItem.SetDataBaseFactory(dbFactory)?.Delete();
			}

			if (_OnSave != null)
			{
				if (!_OnSave(dbFactory, this))
				{
					dbFactory.Abort();
					return false;
				}
			}
			dbFactory.Commit(); 
			if (_OnAfterSave != null)
				_OnAfterSave(this);
            return true;
        }

        public override bool Delete()
        {
			if (OrderShipmentHeader is null || string.IsNullOrEmpty(OrderShipmentHeader.OrderShipmentUuid)) return false; 
			if (_OnBeforeDelete != null)
				if (!_OnBeforeDelete(this)) return false;
			dbFactory.Begin(); 

			 if (NeedDelete(OrderShipmentHeaderTable))
			{
				OrderShipmentHeader.SetDataBaseFactory(dbFactory); 
				if (OrderShipmentHeader.Delete() <= 0) return false; 
			}
			 if (NeedDelete(OrderShipmentCanceledItemTable))
			{
				if (OrderShipmentCanceledItem != null) 
					OrderShipmentCanceledItem?.SetDataBaseFactory(dbFactory)?.Delete(); 
			}
			 if (NeedDelete(OrderShipmentPackageTable))
			{
				if (OrderShipmentPackage != null) 
					OrderShipmentPackage?.SetDataBaseFactory(dbFactory)?.Delete(); 
			}
			 if (NeedDelete(OrderShipmentShippedItemTable))
			{
				if (OrderShipmentShippedItem != null) 
					OrderShipmentShippedItem?.SetDataBaseFactory(dbFactory)?.Delete(); 
			}
			if (_OnDelete != null)
			{
				if (!_OnDelete(dbFactory, this))
				{
					dbFactory.Abort();
					return false;
				}
			}
			dbFactory.Commit(); 
			if (_OnAfterDelete != null)
				_OnAfterDelete(this);
            return true;
        }


        public override async Task<bool> GetAsync(long RowNum)
        {
			var obj = await GetOrderShipmentHeaderAsync(RowNum); 
			if (obj is null) return false; 
			OrderShipmentHeader = obj; 
			await GetOthersAsync(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        public override async Task<bool> GetByIdAsync(string OrderShipmentUuid)
        {
			var obj = await GetOrderShipmentHeaderByOrderShipmentUuidAsync(OrderShipmentUuid); 
			if (obj is null) return false; 
			OrderShipmentHeader = obj; 
			await GetOthersAsync(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        protected virtual async Task GetOthersAsync()
        {
            
			if (string.IsNullOrEmpty(OrderShipmentHeader.OrderShipmentUuid)) return; 
			OrderShipmentCanceledItem = await GetOrderShipmentCanceledItemByOrderShipmentUuidAsync(OrderShipmentHeader.OrderShipmentUuid); 
			OrderShipmentPackage = await GetOrderShipmentPackageByOrderShipmentUuidAsync(OrderShipmentHeader.OrderShipmentUuid); 
			OrderShipmentShippedItem = await GetOrderShipmentShippedItemByOrderShipmentUuidAsync(OrderShipmentHeader.OrderShipmentUuid); 
        }

        public override async Task<bool> SaveAsync()
        {
			if (OrderShipmentHeader is null || string.IsNullOrEmpty(OrderShipmentHeader.OrderShipmentUuid)) return false; 
			CheckIntegrity(); 
			if (_OnBeforeSave != null)
				if (!_OnBeforeSave(this)) return false;
			dbFactory.Begin(); 

			 if (NeedSave(OrderShipmentHeaderTable))
			{
				OrderShipmentHeader.SetDataBaseFactory(dbFactory); 
				if (!(await OrderShipmentHeader.SaveAsync().ConfigureAwait(false))) return false; 
			}
			 if (NeedSave(OrderShipmentCanceledItemTable))
			{
				if (OrderShipmentCanceledItem != null) 
					await OrderShipmentCanceledItem.SetDataBaseFactory(dbFactory).SaveAsync().ConfigureAwait(false); 
				var delOrderShipmentCanceledItem = _OrderShipmentCanceledItemDeleted;
				if (delOrderShipmentCanceledItem != null)
					await delOrderShipmentCanceledItem.SetDataBaseFactory(dbFactory).DeleteAsync().ConfigureAwait(false);
			}

			 if (NeedSave(OrderShipmentPackageTable))
			{
				if (OrderShipmentPackage != null) 
					await OrderShipmentPackage.SetDataBaseFactory(dbFactory).SaveAsync().ConfigureAwait(false); 
				var delOrderShipmentPackage = _OrderShipmentPackageDeleted;
				if (delOrderShipmentPackage != null)
					await delOrderShipmentPackage.SetDataBaseFactory(dbFactory).DeleteAsync().ConfigureAwait(false);
			}

			 if (NeedSave(OrderShipmentShippedItemTable))
			{
				if (OrderShipmentShippedItem != null) 
					await OrderShipmentShippedItem.SetDataBaseFactory(dbFactory).SaveAsync().ConfigureAwait(false); 
				var delOrderShipmentShippedItem = OrderShipmentShippedItemDeleted;
				if (delOrderShipmentShippedItem != null)
					await delOrderShipmentShippedItem.SetDataBaseFactory(dbFactory).DeleteAsync().ConfigureAwait(false);
			}

			if (_OnSave != null)
			{
				if (!_OnSave(dbFactory, this))
				{
					dbFactory.Abort();
					return false;
				}
			}
			dbFactory.Commit(); 
			if (_OnAfterSave != null)
				_OnAfterSave(this);
            return true;
        }

        public override async Task<bool> DeleteAsync()
        {
			if (OrderShipmentHeader is null || string.IsNullOrEmpty(OrderShipmentHeader.OrderShipmentUuid)) return false; 
			if (_OnBeforeDelete != null)
				if (!_OnBeforeDelete(this)) return false;
			dbFactory.Begin(); 
			 if (NeedDelete(OrderShipmentHeaderTable))
			{
			OrderShipmentHeader.SetDataBaseFactory(dbFactory); 
			if ((await OrderShipmentHeader.DeleteAsync().ConfigureAwait(false)) <= 0) return false; 
			}
			 if (NeedDelete(OrderShipmentCanceledItemTable))
			{
				if (OrderShipmentCanceledItem != null) 
					await OrderShipmentCanceledItem.SetDataBaseFactory(dbFactory).DeleteAsync().ConfigureAwait(false); 
			}
			 if (NeedDelete(OrderShipmentPackageTable))
			{
				if (OrderShipmentPackage != null) 
					await OrderShipmentPackage.SetDataBaseFactory(dbFactory).DeleteAsync().ConfigureAwait(false); 
			}
			 if (NeedDelete(OrderShipmentShippedItemTable))
			{
				if (OrderShipmentShippedItem != null) 
					await OrderShipmentShippedItem.SetDataBaseFactory(dbFactory).DeleteAsync().ConfigureAwait(false); 
			}
			if (_OnDelete != null)
			{
				if (!_OnDelete(dbFactory, this))
				{
					dbFactory.Abort();
					return false;
				}
			}
			dbFactory.Commit(); 
			if (_OnAfterDelete != null)
				_OnAfterDelete(this);
            return true;
        }

        #endregion CRUD Methods


        #region OrderShipmentHeader - Generated 
    

        // one to one children
        protected OrderShipmentHeader _OrderShipmentHeader;

        public virtual OrderShipmentHeader OrderShipmentHeader 
        { 
            get => _OrderShipmentHeader;
            set => _OrderShipmentHeader = value?.SetParent(this); 
        }

        public virtual void CopyOrderShipmentHeaderFrom(OrderShipmentData data) => 
            OrderShipmentHeader?.CopyFrom(data.OrderShipmentHeader, new string[] {"OrderShipmentUuid"});

        public virtual OrderShipmentHeader NewOrderShipmentHeader() => new OrderShipmentHeader(dbFactory).SetParent(this);

        public virtual OrderShipmentHeader GetOrderShipmentHeader(long RowNum) =>
            (RowNum <= 0) ? null : dbFactory.Get<OrderShipmentHeader>(RowNum);

        public virtual OrderShipmentHeader GetOrderShipmentHeaderByOrderShipmentUuid(string OrderShipmentUuid) =>
            (string.IsNullOrEmpty(OrderShipmentUuid)) ? null : dbFactory.GetById<OrderShipmentHeader>(OrderShipmentUuid);

        public virtual bool SaveOrderShipmentHeader(OrderShipmentHeader data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteOrderShipmentHeader(OrderShipmentHeader data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<OrderShipmentHeader> GetOrderShipmentHeaderAsync(long RowNum) =>
            (RowNum <= 0) ? null : await dbFactory.GetAsync<OrderShipmentHeader>(RowNum);

        public virtual async Task<OrderShipmentHeader> GetOrderShipmentHeaderByOrderShipmentUuidAsync(string OrderShipmentUuid) =>
            (string.IsNullOrEmpty(OrderShipmentUuid)) ? null : await dbFactory.GetByIdAsync<OrderShipmentHeader>(OrderShipmentUuid);

        public virtual async Task<bool> SaveOrderShipmentHeaderAsync(OrderShipmentHeader data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteOrderShipmentHeaderAsync(OrderShipmentHeader data) =>
            (data is null) ? 0 : await data.DeleteAsync();




        #endregion OrderShipmentHeader - Generated 

        #region OrderShipmentCanceledItem - Generated 
        // One to many children
        protected IList<OrderShipmentCanceledItem> _OrderShipmentCanceledItemDeleted;
        public virtual OrderShipmentCanceledItem AddOrderShipmentCanceledItemDeleted(OrderShipmentCanceledItem del) 
        {
            if (_OrderShipmentCanceledItemDeleted is null)
                _OrderShipmentCanceledItemDeleted = new List<OrderShipmentCanceledItem>();
            var lst = _OrderShipmentCanceledItemDeleted.ToList();
            lst.Add(del);
            _OrderShipmentCanceledItemDeleted = lst;
            return del;
        } 

        public virtual IList<OrderShipmentCanceledItem> AddOrderShipmentCanceledItemDeleted(IList<OrderShipmentCanceledItem> del) 
        {
            if (_OrderShipmentCanceledItemDeleted is null)
                _OrderShipmentCanceledItemDeleted = new List<OrderShipmentCanceledItem>();
            var lst = _OrderShipmentCanceledItemDeleted.ToList();
            lst.AddRange(del);
            _OrderShipmentCanceledItemDeleted = lst;
            return del;
        } 

        public virtual void SetOrderShipmentCanceledItemDeleted(IList<OrderShipmentCanceledItem> del) =>
            _OrderShipmentCanceledItemDeleted = del;

        public virtual void ClearOrderShipmentCanceledItemDeleted() =>
            _OrderShipmentCanceledItemDeleted = null;


        protected IList<OrderShipmentCanceledItem> _OrderShipmentCanceledItem;

        public virtual IList<OrderShipmentCanceledItem> OrderShipmentCanceledItem 
        { 
            get 
            {
                if (_OrderShipmentCanceledItem is null)
                    _OrderShipmentCanceledItem = new List<OrderShipmentCanceledItem>();
                return _OrderShipmentCanceledItem;
            } 
            set
            {
                if (value != null)
                {
                    var valueList = value.ToList();
                    valueList.ForEach(i => i?.SetParent(this));
                    _OrderShipmentCanceledItem = valueList;
                }
                else
                    _OrderShipmentCanceledItem = null;
            } 
        }

        public virtual void CopyOrderShipmentCanceledItemFrom(OrderShipmentData data) 
        {
            if  (data is null) return;
            var lstDeleted = OrderShipmentCanceledItem?.CopyFrom(data.OrderShipmentCanceledItem, new string[] {"OrderShipmentUuid"});
            SetOrderShipmentCanceledItemDeleted(lstDeleted);
            foreach (var c in OrderShipmentCanceledItem)
                c?.CopyChildrenFrom(data.OrderShipmentCanceledItem?.FindByRowNum(c.RowNum));
        } 

        public virtual OrderShipmentCanceledItem NewOrderShipmentCanceledItem() => new OrderShipmentCanceledItem(dbFactory);

        public virtual OrderShipmentCanceledItem AddOrderShipmentCanceledItem(OrderShipmentCanceledItem obj) => 
            OrderShipmentCanceledItem.AddOrReplace(obj.SetParent(this));

        public virtual OrderShipmentCanceledItem RemoveOrderShipmentCanceledItem(OrderShipmentCanceledItem obj) => 
            AddOrderShipmentCanceledItemDeleted(OrderShipmentCanceledItem.RemoveObject(obj.SetParent(this)));

        public virtual IList<OrderShipmentCanceledItem> GetOrderShipmentCanceledItemByOrderShipmentUuid(string OrderShipmentUuid) =>
            (string.IsNullOrEmpty(OrderShipmentUuid)) 
                ? null 
                : dbFactory.Find<OrderShipmentCanceledItem>("WHERE OrderShipmentUuid = @0 ORDER BY RowNum ", OrderShipmentUuid).ToList();

        public virtual bool SaveOrderShipmentCanceledItem(IList<OrderShipmentCanceledItem> data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteOrderShipmentCanceledItem(IList<OrderShipmentCanceledItem> data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<IList<OrderShipmentCanceledItem>> GetOrderShipmentCanceledItemByOrderShipmentUuidAsync(string OrderShipmentUuid) =>
            (string.IsNullOrEmpty(OrderShipmentUuid)) 
                ? null
                : (await dbFactory.FindAsync<OrderShipmentCanceledItem>("WHERE OrderShipmentUuid = @0 ORDER BY RowNum ", OrderShipmentUuid)).ToList();

        public virtual async Task<bool> SaveOrderShipmentCanceledItemAsync(IList<OrderShipmentCanceledItem> data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteOrderShipmentCanceledItemAsync(IList<OrderShipmentCanceledItem> data) =>
            (data is null) ? 0 : await data.DeleteAsync();

        public virtual IList<OrderShipmentCanceledItem> CheckIntegrityOrderShipmentCanceledItem()
        {
            if (OrderShipmentCanceledItem is null || OrderShipmentHeader is null) 
                return OrderShipmentCanceledItem;
            var seq = 0;
            OrderShipmentCanceledItem.RemoveEmpty();
            var children = OrderShipmentCanceledItem.ToList();
            foreach (var child in children.Where(x => x != null))
            {
                child.SetParent(this);
                if (child.OrderShipmentUuid != OrderShipmentHeader.OrderShipmentUuid)
                    child.OrderShipmentUuid = OrderShipmentHeader.OrderShipmentUuid;
            }
            return children;
        }



        #endregion OrderShipmentCanceledItem - Generated 

        #region OrderShipmentPackage - Generated 
        // One to many children
        protected IList<OrderShipmentPackage> _OrderShipmentPackageDeleted;
        public virtual OrderShipmentPackage AddOrderShipmentPackageDeleted(OrderShipmentPackage del) 
        {
            if (_OrderShipmentPackageDeleted is null)
                _OrderShipmentPackageDeleted = new List<OrderShipmentPackage>();
            var lst = _OrderShipmentPackageDeleted.ToList();
            lst.Add(del);
            _OrderShipmentPackageDeleted = lst;
            return del;
        } 

        public virtual IList<OrderShipmentPackage> AddOrderShipmentPackageDeleted(IList<OrderShipmentPackage> del) 
        {
            if (_OrderShipmentPackageDeleted is null)
                _OrderShipmentPackageDeleted = new List<OrderShipmentPackage>();
            var lst = _OrderShipmentPackageDeleted.ToList();
            lst.AddRange(del);
            _OrderShipmentPackageDeleted = lst;
            return del;
        } 

        public virtual void SetOrderShipmentPackageDeleted(IList<OrderShipmentPackage> del) =>
            _OrderShipmentPackageDeleted = del;

        public virtual void ClearOrderShipmentPackageDeleted() =>
            _OrderShipmentPackageDeleted = null;


        protected IList<OrderShipmentPackage> _OrderShipmentPackage;

        public virtual IList<OrderShipmentPackage> OrderShipmentPackage 
        { 
            get 
            {
                if (_OrderShipmentPackage is null)
                    _OrderShipmentPackage = new List<OrderShipmentPackage>();
                return _OrderShipmentPackage;
            } 
            set
            {
                if (value != null)
                {
                    var valueList = value.ToList();
                    valueList.ForEach(i => i?.SetParent(this));
                    _OrderShipmentPackage = valueList;
                }
                else
                    _OrderShipmentPackage = null;
            } 
        }

        public virtual void CopyOrderShipmentPackageFrom(OrderShipmentData data) 
        {
            if  (data is null) return;
            var lstDeleted = OrderShipmentPackage?.CopyFrom(data.OrderShipmentPackage, new string[] {"OrderShipmentUuid"});
            SetOrderShipmentPackageDeleted(lstDeleted);
            foreach (var c in OrderShipmentPackage)
                c?.CopyChildrenFrom(data.OrderShipmentPackage?.FindByRowNum(c.RowNum));
        } 

        public virtual OrderShipmentPackage NewOrderShipmentPackage() => new OrderShipmentPackage(dbFactory);

        public virtual OrderShipmentPackage AddOrderShipmentPackage(OrderShipmentPackage obj) => 
            OrderShipmentPackage.AddOrReplace(obj.SetParent(this));

        public virtual OrderShipmentPackage RemoveOrderShipmentPackage(OrderShipmentPackage obj) => 
            AddOrderShipmentPackageDeleted(OrderShipmentPackage.RemoveObject(obj.SetParent(this)));

        public virtual IList<OrderShipmentPackage> GetOrderShipmentPackageByOrderShipmentUuid(string OrderShipmentUuid) =>
            (string.IsNullOrEmpty(OrderShipmentUuid)) 
                ? null 
                : dbFactory.Find<OrderShipmentPackage>("WHERE OrderShipmentUuid = @0 ORDER BY RowNum ", OrderShipmentUuid).ToList();

        public virtual bool SaveOrderShipmentPackage(IList<OrderShipmentPackage> data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteOrderShipmentPackage(IList<OrderShipmentPackage> data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<IList<OrderShipmentPackage>> GetOrderShipmentPackageByOrderShipmentUuidAsync(string OrderShipmentUuid) =>
            (string.IsNullOrEmpty(OrderShipmentUuid)) 
                ? null
                : (await dbFactory.FindAsync<OrderShipmentPackage>("WHERE OrderShipmentUuid = @0 ORDER BY RowNum ", OrderShipmentUuid)).ToList();

        public virtual async Task<bool> SaveOrderShipmentPackageAsync(IList<OrderShipmentPackage> data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteOrderShipmentPackageAsync(IList<OrderShipmentPackage> data) =>
            (data is null) ? 0 : await data.DeleteAsync();

        public virtual IList<OrderShipmentPackage> CheckIntegrityOrderShipmentPackage()
        {
            if (OrderShipmentPackage is null || OrderShipmentHeader is null) 
                return OrderShipmentPackage;
            var seq = 0;
            OrderShipmentPackage.RemoveEmpty();
            var children = OrderShipmentPackage.ToList();
            foreach (var child in children.Where(x => x != null))
            {
                child.SetParent(this);
                if (child.OrderShipmentUuid != OrderShipmentHeader.OrderShipmentUuid)
                    child.OrderShipmentUuid = OrderShipmentHeader.OrderShipmentUuid;
            }
            return children;
        }



        #endregion OrderShipmentPackage - Generated 

        #region OrderShipmentShippedItem - Generated 
        // grand children
        protected IList<OrderShipmentShippedItem> _OrderShipmentShippedItem;

        public IList<OrderShipmentShippedItem> OrderShipmentShippedItem 
        { 
            get 
            {
                _OrderShipmentShippedItem = OrderShipmentPackage is null ? null : OrderShipmentPackage.SelectMany(x => x.GetChildrenOrderShipmentShippedItem()).ToList();
                return _OrderShipmentShippedItem;
            } 
            set
            {
                _OrderShipmentShippedItem = value;
                if (OrderShipmentPackage != null)
                    foreach (var par in OrderShipmentPackage)
                        par.SetChildrenOrderShipmentShippedItem(_OrderShipmentShippedItem);
            } 
        }

        protected IList<OrderShipmentShippedItem> OrderShipmentShippedItemDeleted 
        { 
            get 
            {
                var deleted = new List<OrderShipmentShippedItem>();
                if (_OrderShipmentPackageDeleted != null)
                {
                    var del = _OrderShipmentPackageDeleted
                            .Where(x => x?.GetChildrenOrderShipmentShippedItem() != null)
                            .SelectMany(x => x?.GetChildrenOrderShipmentShippedItem());
                    if (del.Any())
                        deleted.AddRange(del.ToList());
                }
                if (OrderShipmentPackage != null)
                {
                    var delChildren = OrderShipmentPackage
                                    .Where(x => x?.GetChildrenDeletedOrderShipmentShippedItem() != null)
                                    .SelectMany(x => x?.GetChildrenDeletedOrderShipmentShippedItem());
                    if (delChildren.Any())
                        deleted.AddRange(delChildren.ToList());
                }
                return deleted;
            } 
        }

        public virtual IList<OrderShipmentShippedItem> GetOrderShipmentShippedItemByOrderShipmentUuid(string OrderShipmentUuid) =>
            (string.IsNullOrEmpty(OrderShipmentUuid)) 
                ? null 
                : dbFactory.Find<OrderShipmentShippedItem>("WHERE OrderShipmentUuid = @0 ORDER BY RowNum ", OrderShipmentUuid).ToList();

        public virtual bool SaveOrderShipmentShippedItem(IList<OrderShipmentShippedItem> data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteOrderShipmentShippedItem(IList<OrderShipmentShippedItem> data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<IList<OrderShipmentShippedItem>> GetOrderShipmentShippedItemByOrderShipmentUuidAsync(string OrderShipmentUuid) =>
            (string.IsNullOrEmpty(OrderShipmentUuid)) 
                ? null
                : (await dbFactory.FindAsync<OrderShipmentShippedItem>("WHERE OrderShipmentUuid = @0 ORDER BY RowNum ", OrderShipmentUuid)).ToList();

        public virtual async Task<bool> SaveOrderShipmentShippedItemAsync(IList<OrderShipmentShippedItem> data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteOrderShipmentShippedItemAsync(IList<OrderShipmentShippedItem> data) =>
            (data is null) ? 0 : await data.DeleteAsync();

        public virtual IList<OrderShipmentShippedItem> CheckIntegrityOrderShipmentShippedItem()
        {
            if (OrderShipmentShippedItem is null || OrderShipmentHeader is null) 
                return OrderShipmentShippedItem;
            var seq = 0;
            OrderShipmentShippedItem.RemoveEmpty();
            var children = OrderShipmentShippedItem.ToList();
            foreach (var child in children.Where(x => x != null))
            {
                child.SetParent(this);
                if (child.OrderShipmentUuid != OrderShipmentHeader.OrderShipmentUuid)
                    child.OrderShipmentUuid = OrderShipmentHeader.OrderShipmentUuid;
            }
            return children;
        }


        #endregion OrderShipmentShippedItem - Generated 


    }
}



