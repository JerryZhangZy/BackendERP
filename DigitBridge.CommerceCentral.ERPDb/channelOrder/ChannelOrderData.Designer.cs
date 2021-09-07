              
    

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
    /// Represents a ChannelOrderData.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    public partial class ChannelOrderData : StructureRepository<ChannelOrderData>
    {
        public ChannelOrderData() : base() {}
        public ChannelOrderData(IDataBaseFactory dbFactory): base(dbFactory) {}

        [JsonIgnore, XmlIgnore]
        public new bool IsNew => OrderHeader.IsNew;

        [JsonIgnore, XmlIgnore]
        public new string UniqueId => OrderHeader.UniqueId;
        
		 [JsonIgnore, XmlIgnore] 
		public static string OrderHeaderTable ="OrderHeader ";
		
		 [JsonIgnore, XmlIgnore] 
		public static string OrderLineTable ="OrderLine ";
		
		 [JsonIgnore, XmlIgnore] 
		public static string OrderLineMerchantExtTable ="OrderLineMerchantExt ";
		
        #region CRUD Methods

        public override bool Equals(ChannelOrderData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!string.IsNullOrWhiteSpace(UniqueId) && !string.IsNullOrWhiteSpace(other.UniqueId) && !UniqueId.Equals(other.UniqueId)) return false;
            return ChildrenEquals(other);
        }
        public virtual bool ChildrenEquals(ChannelOrderData other)
        {
			if (OrderHeader == null && other.OrderHeader != null || OrderHeader != null && other.OrderHeader == null) 
				return false; 
			if (OrderHeader != null && other.OrderHeader != null && !OrderHeader.Equals(other.OrderHeader)) 
				return false; 
			if (OrderLine == null && other.OrderLine != null || OrderLine != null && other.OrderLine == null) 
				return false; 
			if (OrderLine != null && other.OrderLine != null && !OrderLine.EqualsList(other.OrderLine)) 
				return false; 
			if (OrderLineMerchantExt == null && other.OrderLineMerchantExt != null || OrderLineMerchantExt != null && other.OrderLineMerchantExt == null) 
				return false; 
			if (OrderLineMerchantExt != null && other.OrderLineMerchantExt != null && !OrderLineMerchantExt.EqualsList(other.OrderLineMerchantExt)) 
				return false; 
            return true;
        }

        // Check Children table Integrity
        public override ChannelOrderData CheckIntegrity()
        {
			if (OrderHeader is null) return this; 
			OrderHeader.CheckIntegrity(); 
			CheckIntegrityOrderLine(); 
			CheckIntegrityOrderLineMerchantExt(); 
			CheckIntegrityOthers(); 
            return this;
        }

        partial void ClearOthers();
        public override void Clear()
        {
			OrderHeader?.Clear(); 
			OrderLine = new List<OrderLine>(); 
			ClearOrderLineDeleted(); 
			OrderLineMerchantExt = new List<OrderLineMerchantExt>(); 
			ClearOthers(); 
			if (_OnClear != null)
				_OnClear(this);
            return;
        }

        public override void New()
        {
            Clear();
			OrderHeader = NewOrderHeader(); 
			OrderLine = new List<OrderLine>(); 
			AddOrderLine(NewOrderLine()); 
			OrderLine.ToList().ForEach(x => x?.NewChildren()); 
			ClearOrderLineDeleted(); 
            return;
        }

        public virtual void CopyFrom(ChannelOrderData data)
        {
			CopyOrderHeaderFrom(data); 
			CopyOrderLineFrom(data); 
            CheckIntegrity();
            return;
        }

        public override ChannelOrderData Clone()
        {
			var newData = new ChannelOrderData(); 
			newData.New(); 
			newData?.CopyFrom(this); 
			newData.OrderHeader.ClearMetaData(); 
			newData.OrderLine.ClearMetaData(); 
			newData.OrderLineMerchantExt.ClearMetaData(); 
            newData.CheckIntegrity();
            return newData;
        }

        public override bool Get(long RowNum)
        {
			var obj = GetOrderHeader(RowNum); 
			if (obj is null) return false; 
			OrderHeader = obj; 
			GetOthers(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        public override bool GetById(string CentralOrderUuid)
        {
			var obj = GetOrderHeaderByCentralOrderUuid(CentralOrderUuid); 
			if (obj is null) return false; 
			OrderHeader = obj; 
			GetOthers(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        protected virtual void GetOthers()
        {
            
			if (string.IsNullOrEmpty(OrderHeader.CentralOrderUuid)) return; 
			OrderLine = GetOrderLineByCentralOrderUuid(OrderHeader.CentralOrderUuid); 
			OrderLineMerchantExt = GetOrderLineMerchantExtByCentralOrderUuid(OrderHeader.CentralOrderUuid); 
        }

        public override bool Save()
        {
			if (OrderHeader is null || string.IsNullOrEmpty(OrderHeader.CentralOrderUuid)) return false; 
			CheckIntegrity();
			if (_OnBeforeSave != null)
				if (!_OnBeforeSave(this)) return false;
			dbFactory.Begin();

			 if (NeedSave(OrderHeaderTable))
			{
				OrderHeader.SetDataBaseFactory(dbFactory);
				if (!OrderHeader.Save()) return false;
			}

			 if (NeedSave(OrderLineTable))
			{
				if (OrderLine != null) 
					OrderLine.SetDataBaseFactory(dbFactory)?.Save();
				var delOrderLine = _OrderLineDeleted;
				if (delOrderLine != null)
					delOrderLine.SetDataBaseFactory(dbFactory)?.Delete();
			}

			 if (NeedSave(OrderLineMerchantExtTable))
			{
				if (OrderLineMerchantExt != null) 
					OrderLineMerchantExt.SetDataBaseFactory(dbFactory)?.Save();
				var delChildrenOrderLineMerchantExt = OrderLineMerchantExtDeleted;
				if (delChildrenOrderLineMerchantExt != null)
					delChildrenOrderLineMerchantExt.SetDataBaseFactory(dbFactory)?.Delete();
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
			if (OrderHeader is null || string.IsNullOrEmpty(OrderHeader.CentralOrderUuid)) return false; 
			if (_OnBeforeDelete != null)
				if (!_OnBeforeDelete(this)) return false;
			dbFactory.Begin(); 

			 if (NeedDelete(OrderHeaderTable))
			{
				OrderHeader.SetDataBaseFactory(dbFactory); 
				if (OrderHeader.Delete() <= 0) return false; 
			}
			 if (NeedDelete(OrderLineTable))
			{
				if (OrderLine != null) 
					OrderLine?.SetDataBaseFactory(dbFactory)?.Delete(); 
			}
			 if (NeedDelete(OrderLineMerchantExtTable))
			{
				if (OrderLineMerchantExt != null) 
					OrderLineMerchantExt?.SetDataBaseFactory(dbFactory)?.Delete(); 
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
			var obj = await GetOrderHeaderAsync(RowNum); 
			if (obj is null) return false; 
			OrderHeader = obj; 
			await GetOthersAsync(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        public override async Task<bool> GetByIdAsync(string CentralOrderUuid)
        {
			var obj = await GetOrderHeaderByCentralOrderUuidAsync(CentralOrderUuid); 
			if (obj is null) return false; 
			OrderHeader = obj; 
			await GetOthersAsync(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        protected virtual async Task GetOthersAsync()
        {
            
			if (string.IsNullOrEmpty(OrderHeader.CentralOrderUuid)) return; 
			OrderLine = await GetOrderLineByCentralOrderUuidAsync(OrderHeader.CentralOrderUuid); 
			OrderLineMerchantExt = await GetOrderLineMerchantExtByCentralOrderUuidAsync(OrderHeader.CentralOrderUuid); 
        }

        public override async Task<bool> SaveAsync()
        {
			if (OrderHeader is null || string.IsNullOrEmpty(OrderHeader.CentralOrderUuid)) return false; 
			CheckIntegrity(); 
			if (_OnBeforeSave != null)
				if (!_OnBeforeSave(this)) return false;
			dbFactory.Begin(); 

			 if (NeedSave(OrderHeaderTable))
			{
				OrderHeader.SetDataBaseFactory(dbFactory); 
				if (!(await OrderHeader.SaveAsync())) return false; 
			}
			 if (NeedSave(OrderLineTable))
			{
				if (OrderLine != null) 
					await OrderLine.SetDataBaseFactory(dbFactory).SaveAsync(); 
				var delOrderLine = _OrderLineDeleted;
				if (delOrderLine != null)
					await delOrderLine.SetDataBaseFactory(dbFactory).DeleteAsync();
			}

			 if (NeedSave(OrderLineMerchantExtTable))
			{
				if (OrderLineMerchantExt != null) 
					await OrderLineMerchantExt.SetDataBaseFactory(dbFactory).SaveAsync(); 
				var delOrderLineMerchantExt = OrderLineMerchantExtDeleted;
				if (delOrderLineMerchantExt != null)
					await delOrderLineMerchantExt.SetDataBaseFactory(dbFactory).DeleteAsync();
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
			if (OrderHeader is null || string.IsNullOrEmpty(OrderHeader.CentralOrderUuid)) return false; 
			if (_OnBeforeDelete != null)
				if (!_OnBeforeDelete(this)) return false;
			dbFactory.Begin(); 
			 if (NeedDelete(OrderHeaderTable))
			{
			OrderHeader.SetDataBaseFactory(dbFactory); 
			if ((await OrderHeader.DeleteAsync()) <= 0) return false; 
			}
			 if (NeedDelete(OrderLineTable))
			{
				if (OrderLine != null) 
					await OrderLine.SetDataBaseFactory(dbFactory).DeleteAsync(); 
			}
			 if (NeedDelete(OrderLineMerchantExtTable))
			{
				if (OrderLineMerchantExt != null) 
					await OrderLineMerchantExt.SetDataBaseFactory(dbFactory).DeleteAsync(); 
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


        #region OrderHeader - Generated 
    

        // one to one children
        protected OrderHeader _OrderHeader;

        public virtual OrderHeader OrderHeader 
        { 
            get => _OrderHeader;
            set => _OrderHeader = value?.SetParent(this); 
        }

        public virtual void CopyOrderHeaderFrom(ChannelOrderData data) => 
            OrderHeader?.CopyFrom(data.OrderHeader, new string[] {"CentralOrderUuid"});

        public virtual OrderHeader NewOrderHeader() => new OrderHeader(dbFactory).SetParent(this);

        public virtual OrderHeader GetOrderHeader(long RowNum) =>
            (RowNum <= 0) ? null : dbFactory.Get<OrderHeader>(RowNum);

        public virtual OrderHeader GetOrderHeaderByCentralOrderUuid(string CentralOrderUuid) =>
            (string.IsNullOrEmpty(CentralOrderUuid)) ? null : dbFactory.GetById<OrderHeader>(CentralOrderUuid);

        public virtual bool SaveOrderHeader(OrderHeader data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteOrderHeader(OrderHeader data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<OrderHeader> GetOrderHeaderAsync(long RowNum) =>
            (RowNum <= 0) ? null : await dbFactory.GetAsync<OrderHeader>(RowNum);

        public virtual async Task<OrderHeader> GetOrderHeaderByCentralOrderUuidAsync(string CentralOrderUuid) =>
            (string.IsNullOrEmpty(CentralOrderUuid)) ? null : await dbFactory.GetByIdAsync<OrderHeader>(CentralOrderUuid);

        public virtual async Task<bool> SaveOrderHeaderAsync(OrderHeader data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteOrderHeaderAsync(OrderHeader data) =>
            (data is null) ? 0 : await data.DeleteAsync();




        #endregion OrderHeader - Generated 

        #region OrderLine - Generated 
        // One to many children
        protected IList<OrderLine> _OrderLineDeleted;
        public virtual OrderLine AddOrderLineDeleted(OrderLine del) 
        {
            if (_OrderLineDeleted is null)
                _OrderLineDeleted = new List<OrderLine>();
            var lst = _OrderLineDeleted.ToList();
            lst.Add(del);
            _OrderLineDeleted = lst;
            return del;
        } 

        public virtual IList<OrderLine> AddOrderLineDeleted(IList<OrderLine> del) 
        {
            if (_OrderLineDeleted is null)
                _OrderLineDeleted = new List<OrderLine>();
            var lst = _OrderLineDeleted.ToList();
            lst.AddRange(del);
            _OrderLineDeleted = lst;
            return del;
        } 

        public virtual void SetOrderLineDeleted(IList<OrderLine> del) =>
            _OrderLineDeleted = del;

        public virtual void ClearOrderLineDeleted() =>
            _OrderLineDeleted = null;


        protected IList<OrderLine> _OrderLine;

        public virtual IList<OrderLine> OrderLine 
        { 
            get 
            {
                if (_OrderLine is null)
                    _OrderLine = new List<OrderLine>();
                return _OrderLine;
            } 
            set
            {
                if (value != null)
                {
                    var valueList = value.ToList();
                    valueList.ForEach(i => i?.SetParent(this));
                    _OrderLine = valueList;
                }
                else
                    _OrderLine = null;
            } 
        }

        public virtual void CopyOrderLineFrom(ChannelOrderData data) 
        {
            if  (data is null) return;
            var lstDeleted = OrderLine?.CopyFrom(data.OrderLine, new string[] {"CentralOrderUuid"});
            SetOrderLineDeleted(lstDeleted);
            foreach (var c in OrderLine)
                c?.CopyChildrenFrom(data.OrderLine?.FindByRowNum(c.RowNum));
        } 

        public virtual OrderLine NewOrderLine() => new OrderLine(dbFactory);

        public virtual OrderLine AddOrderLine(OrderLine obj) => 
            OrderLine.AddOrReplace(obj.SetParent(this));

        public virtual OrderLine RemoveOrderLine(OrderLine obj) => 
            AddOrderLineDeleted(OrderLine.RemoveObject(obj.SetParent(this)));

        public virtual IList<OrderLine> GetOrderLineByCentralOrderUuid(string CentralOrderUuid) =>
            (string.IsNullOrEmpty(CentralOrderUuid)) 
                ? null 
                : dbFactory.Find<OrderLine>("WHERE CentralOrderUuid = @0 ORDER BY RowNum ", CentralOrderUuid).ToList();

        public virtual bool SaveOrderLine(IList<OrderLine> data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteOrderLine(IList<OrderLine> data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<IList<OrderLine>> GetOrderLineByCentralOrderUuidAsync(string CentralOrderUuid) =>
            (string.IsNullOrEmpty(CentralOrderUuid)) 
                ? null
                : (await dbFactory.FindAsync<OrderLine>("WHERE CentralOrderUuid = @0 ORDER BY RowNum ", CentralOrderUuid)).ToList();

        public virtual async Task<bool> SaveOrderLineAsync(IList<OrderLine> data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteOrderLineAsync(IList<OrderLine> data) =>
            (data is null) ? 0 : await data.DeleteAsync();

        public virtual IList<OrderLine> CheckIntegrityOrderLine()
        {
            if (OrderLine is null || OrderHeader is null) 
                return OrderLine;
            var seq = 0;
            OrderLine.RemoveEmpty();
            var children = OrderLine.ToList();
            foreach (var child in children.Where(x => x != null))
            {
                child.SetParent(this);
                if (child.CentralOrderUuid != OrderHeader.CentralOrderUuid)
                    child.CentralOrderUuid = OrderHeader.CentralOrderUuid;
                child.CheckIntegrity();
            }
            return children;
        }



        #endregion OrderLine - Generated 

        #region OrderLineMerchantExt - Generated 
        // grand children
        protected IList<OrderLineMerchantExt> _OrderLineMerchantExt;

        public IList<OrderLineMerchantExt> OrderLineMerchantExt 
        { 
            get 
            {
                _OrderLineMerchantExt = OrderLine is null ? null : OrderLine.SelectMany(x => x.GetChildrenOrderLineMerchantExt()).ToList();
                return _OrderLineMerchantExt;
            } 
            set
            {
                _OrderLineMerchantExt = value;
                if (OrderLine != null)
                    foreach (var par in OrderLine)
                        par.SetChildrenOrderLineMerchantExt(_OrderLineMerchantExt);
            } 
        }

        protected IList<OrderLineMerchantExt> OrderLineMerchantExtDeleted 
        { 
            get 
            {
                var deleted = new List<OrderLineMerchantExt>();
                if (_OrderLineDeleted != null)
                {
                    var del = _OrderLineDeleted
                            .Where(x => x?.GetChildrenOrderLineMerchantExt() != null)
                            .SelectMany(x => x?.GetChildrenOrderLineMerchantExt());
                    if (del.Any())
                        deleted.AddRange(del.ToList());
                }
                if (OrderLine != null)
                {
                    var delChildren = OrderLine
                                    .Where(x => x?.GetChildrenDeletedOrderLineMerchantExt() != null)
                                    .SelectMany(x => x?.GetChildrenDeletedOrderLineMerchantExt());
                    if (delChildren.Any())
                        deleted.AddRange(delChildren.ToList());
                }
                return deleted;
            } 
        }

        public virtual IList<OrderLineMerchantExt> GetOrderLineMerchantExtByCentralOrderUuid(string CentralOrderUuid) =>
            (string.IsNullOrEmpty(CentralOrderUuid)) 
                ? null 
                : dbFactory.Find<OrderLineMerchantExt>("WHERE CentralOrderUuid = @0 ORDER BY RowNum ", CentralOrderUuid).ToList();

        public virtual bool SaveOrderLineMerchantExt(IList<OrderLineMerchantExt> data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteOrderLineMerchantExt(IList<OrderLineMerchantExt> data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<IList<OrderLineMerchantExt>> GetOrderLineMerchantExtByCentralOrderUuidAsync(string CentralOrderUuid) =>
            (string.IsNullOrEmpty(CentralOrderUuid)) 
                ? null
                : (await dbFactory.FindAsync<OrderLineMerchantExt>("WHERE CentralOrderUuid = @0 ORDER BY RowNum ", CentralOrderUuid)).ToList();

        public virtual async Task<bool> SaveOrderLineMerchantExtAsync(IList<OrderLineMerchantExt> data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteOrderLineMerchantExtAsync(IList<OrderLineMerchantExt> data) =>
            (data is null) ? 0 : await data.DeleteAsync();

        public virtual IList<OrderLineMerchantExt> CheckIntegrityOrderLineMerchantExt()
        {
            if (OrderLineMerchantExt is null || OrderHeader is null) 
                return OrderLineMerchantExt;
            var seq = 0;
            OrderLineMerchantExt.RemoveEmpty();
            var children = OrderLineMerchantExt.ToList();
            foreach (var child in children.Where(x => x != null))
            {
                child.SetParent(this);
                if (child.CentralOrderUuid != OrderHeader.CentralOrderUuid)
                    child.CentralOrderUuid = OrderHeader.CentralOrderUuid;
                child.CheckIntegrity();
            }
            return children;
        }


        #endregion OrderLineMerchantExt - Generated 


    }
}



