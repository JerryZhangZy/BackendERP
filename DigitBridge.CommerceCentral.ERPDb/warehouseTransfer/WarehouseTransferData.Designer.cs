              
    

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
    /// Represents a WarehouseTransferData.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    public partial class WarehouseTransferData : StructureRepository<WarehouseTransferData>
    {
        public WarehouseTransferData() : base() {}
        public WarehouseTransferData(IDataBaseFactory dbFactory): base(dbFactory) {}

        [JsonIgnore, XmlIgnore]
        public new bool IsNew => WarehouseTransferHeader.IsNew;

        [JsonIgnore, XmlIgnore]
        public new string UniqueId => WarehouseTransferHeader.UniqueId;
        
		 [JsonIgnore, XmlIgnore] 
		public static string WarehouseTransferHeaderTable ="WarehouseTransferHeader ";
		
		 [JsonIgnore, XmlIgnore] 
		public static string WarehouseTransferItemsTable ="WarehouseTransferItems ";
		
        #region CRUD Methods

        public override bool Equals(WarehouseTransferData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!string.IsNullOrWhiteSpace(UniqueId) && !string.IsNullOrWhiteSpace(other.UniqueId) && !UniqueId.Equals(other.UniqueId)) return false;
            return ChildrenEquals(other);
        }
        public virtual bool ChildrenEquals(WarehouseTransferData other)
        {
			if (WarehouseTransferHeader == null && other.WarehouseTransferHeader != null || WarehouseTransferHeader != null && other.WarehouseTransferHeader == null) 
				return false; 
			if (WarehouseTransferHeader != null && other.WarehouseTransferHeader != null && !WarehouseTransferHeader.Equals(other.WarehouseTransferHeader)) 
				return false; 
			if (WarehouseTransferItems == null && other.WarehouseTransferItems != null || WarehouseTransferItems != null && other.WarehouseTransferItems == null) 
				return false; 
			if (WarehouseTransferItems != null && other.WarehouseTransferItems != null && !WarehouseTransferItems.EqualsList(other.WarehouseTransferItems)) 
				return false; 
            return true;
        }

        // Check Children table Integrity
        public override WarehouseTransferData CheckIntegrity()
        {
			if (WarehouseTransferHeader is null) return this; 
			WarehouseTransferHeader.CheckIntegrity(); 
			CheckIntegrityWarehouseTransferItems(); 
			CheckIntegrityOthers(); 
            return this;
        }

        partial void ClearOthers();
        public override void Clear()
        {
			WarehouseTransferHeader?.Clear(); 
			WarehouseTransferItems = new List<WarehouseTransferItems>(); 
			ClearWarehouseTransferItemsDeleted(); 
			ClearOthers(); 
			if (_OnClear != null)
				_OnClear(this);
            return;
        }

        public override void New()
        {
            Clear();
			WarehouseTransferHeader = NewWarehouseTransferHeader(); 
			WarehouseTransferItems = new List<WarehouseTransferItems>(); 
			AddWarehouseTransferItems(NewWarehouseTransferItems()); 
			ClearWarehouseTransferItemsDeleted(); 
            return;
        }

        public virtual void CopyFrom(WarehouseTransferData data)
        {
			CopyWarehouseTransferHeaderFrom(data); 
			CopyWarehouseTransferItemsFrom(data); 
            CheckIntegrity();
            return;
        }

        public override WarehouseTransferData Clone()
        {
			var newData = new WarehouseTransferData(); 
			newData.New(); 
			newData?.CopyFrom(this); 
			newData.WarehouseTransferHeader.ClearMetaData(); 
			newData.WarehouseTransferItems.ClearMetaData(); 
            newData.CheckIntegrity();
            return newData;
        }

        public override bool Get(long RowNum)
        {
			var obj = GetWarehouseTransferHeader(RowNum); 
			if (obj is null) return false; 
			WarehouseTransferHeader = obj; 
			GetOthers(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        public override bool GetById(string WarehouseTransferUuid)
        {
			var obj = GetWarehouseTransferHeaderByWarehouseTransferUuid(WarehouseTransferUuid); 
			if (obj is null) return false; 
			WarehouseTransferHeader = obj; 
			GetOthers(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        protected virtual void GetOthers()
        {
            
			if (string.IsNullOrEmpty(WarehouseTransferHeader.WarehouseTransferUuid)) return; 
			WarehouseTransferItems = GetWarehouseTransferItemsByWarehouseTransferUuid(WarehouseTransferHeader.WarehouseTransferUuid); 
        }

        public override bool Save()
        {
			if (WarehouseTransferHeader is null || string.IsNullOrEmpty(WarehouseTransferHeader.WarehouseTransferUuid)) return false; 
			CheckIntegrity();
			if (_OnBeforeSave != null)
				if (!_OnBeforeSave(this)) return false;
			dbFactory.Begin();

			 if (NeedSave(WarehouseTransferHeaderTable))
			{
				WarehouseTransferHeader.SetDataBaseFactory(dbFactory);
				if (!WarehouseTransferHeader.Save()) return false;
			}

			 if (NeedSave(WarehouseTransferItemsTable))
			{
				if (WarehouseTransferItems != null) 
					WarehouseTransferItems.SetDataBaseFactory(dbFactory)?.Save();
				var delWarehouseTransferItems = _WarehouseTransferItemsDeleted;
				if (delWarehouseTransferItems != null)
					delWarehouseTransferItems.SetDataBaseFactory(dbFactory)?.Delete();
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
			if (WarehouseTransferHeader is null || string.IsNullOrEmpty(WarehouseTransferHeader.WarehouseTransferUuid)) return false; 
			if (_OnBeforeDelete != null)
				if (!_OnBeforeDelete(this)) return false;
			dbFactory.Begin(); 

			 if (NeedDelete(WarehouseTransferHeaderTable))
			{
				WarehouseTransferHeader.SetDataBaseFactory(dbFactory); 
				if (WarehouseTransferHeader.Delete() <= 0) return false; 
			}
			 if (NeedDelete(WarehouseTransferItemsTable))
			{
				if (WarehouseTransferItems != null) 
					WarehouseTransferItems?.SetDataBaseFactory(dbFactory)?.Delete(); 
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
			var obj = await GetWarehouseTransferHeaderAsync(RowNum); 
			if (obj is null) return false; 
			WarehouseTransferHeader = obj; 
			await GetOthersAsync(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        public override async Task<bool> GetByIdAsync(string WarehouseTransferUuid)
        {
			var obj = await GetWarehouseTransferHeaderByWarehouseTransferUuidAsync(WarehouseTransferUuid); 
			if (obj is null) return false; 
			WarehouseTransferHeader = obj; 
			await GetOthersAsync(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        protected virtual async Task GetOthersAsync()
        {
            
			if (string.IsNullOrEmpty(WarehouseTransferHeader.WarehouseTransferUuid)) return; 
			WarehouseTransferItems = await GetWarehouseTransferItemsByWarehouseTransferUuidAsync(WarehouseTransferHeader.WarehouseTransferUuid); 
        }

        public override async Task<bool> SaveAsync()
        {
			if (WarehouseTransferHeader is null || string.IsNullOrEmpty(WarehouseTransferHeader.WarehouseTransferUuid)) return false; 
			CheckIntegrity(); 
			if (_OnBeforeSave != null)
				if (!_OnBeforeSave(this)) return false;
			dbFactory.Begin(); 

			 if (NeedSave(WarehouseTransferHeaderTable))
			{
				WarehouseTransferHeader.SetDataBaseFactory(dbFactory); 
				if (!(await WarehouseTransferHeader.SaveAsync())) return false; 
			}
			 if (NeedSave(WarehouseTransferItemsTable))
			{
				if (WarehouseTransferItems != null) 
					await WarehouseTransferItems.SetDataBaseFactory(dbFactory).SaveAsync(); 
				var delWarehouseTransferItems = _WarehouseTransferItemsDeleted;
				if (delWarehouseTransferItems != null)
					await delWarehouseTransferItems.SetDataBaseFactory(dbFactory).DeleteAsync();
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
			if (WarehouseTransferHeader is null || string.IsNullOrEmpty(WarehouseTransferHeader.WarehouseTransferUuid)) return false; 
			if (_OnBeforeDelete != null)
				if (!_OnBeforeDelete(this)) return false;
			dbFactory.Begin(); 
			 if (NeedDelete(WarehouseTransferHeaderTable))
			{
			WarehouseTransferHeader.SetDataBaseFactory(dbFactory); 
			if ((await WarehouseTransferHeader.DeleteAsync()) <= 0) return false; 
			}
			 if (NeedDelete(WarehouseTransferItemsTable))
			{
				if (WarehouseTransferItems != null) 
					await WarehouseTransferItems.SetDataBaseFactory(dbFactory).DeleteAsync(); 
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


        #region WarehouseTransferHeader - Generated 
    

        // one to one children
        protected WarehouseTransferHeader _WarehouseTransferHeader;

        public virtual WarehouseTransferHeader WarehouseTransferHeader 
        { 
            get => _WarehouseTransferHeader;
            set => _WarehouseTransferHeader = value?.SetParent(this); 
        }

        public virtual void CopyWarehouseTransferHeaderFrom(WarehouseTransferData data) => 
            WarehouseTransferHeader?.CopyFrom(data.WarehouseTransferHeader, new string[] {"WarehouseTransferUuid"});

        public virtual WarehouseTransferHeader NewWarehouseTransferHeader() => new WarehouseTransferHeader(dbFactory).SetParent(this);

        public virtual WarehouseTransferHeader GetWarehouseTransferHeader(long RowNum) =>
            (RowNum <= 0) ? null : dbFactory.Get<WarehouseTransferHeader>(RowNum);

        public virtual WarehouseTransferHeader GetWarehouseTransferHeaderByWarehouseTransferUuid(string WarehouseTransferUuid) =>
            (string.IsNullOrEmpty(WarehouseTransferUuid)) ? null : dbFactory.GetById<WarehouseTransferHeader>(WarehouseTransferUuid);

        public virtual bool SaveWarehouseTransferHeader(WarehouseTransferHeader data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteWarehouseTransferHeader(WarehouseTransferHeader data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<WarehouseTransferHeader> GetWarehouseTransferHeaderAsync(long RowNum) =>
            (RowNum <= 0) ? null : await dbFactory.GetAsync<WarehouseTransferHeader>(RowNum);

        public virtual async Task<WarehouseTransferHeader> GetWarehouseTransferHeaderByWarehouseTransferUuidAsync(string WarehouseTransferUuid) =>
            (string.IsNullOrEmpty(WarehouseTransferUuid)) ? null : await dbFactory.GetByIdAsync<WarehouseTransferHeader>(WarehouseTransferUuid);

        public virtual async Task<bool> SaveWarehouseTransferHeaderAsync(WarehouseTransferHeader data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteWarehouseTransferHeaderAsync(WarehouseTransferHeader data) =>
            (data is null) ? 0 : await data.DeleteAsync();




        #endregion WarehouseTransferHeader - Generated 

        #region WarehouseTransferItems - Generated 
        // One to many children
        protected IList<WarehouseTransferItems> _WarehouseTransferItemsDeleted;
        public virtual WarehouseTransferItems AddWarehouseTransferItemsDeleted(WarehouseTransferItems del) 
        {
            if (_WarehouseTransferItemsDeleted is null)
                _WarehouseTransferItemsDeleted = new List<WarehouseTransferItems>();
            var lst = _WarehouseTransferItemsDeleted.ToList();
            lst.Add(del);
            _WarehouseTransferItemsDeleted = lst;
            return del;
        } 

        public virtual IList<WarehouseTransferItems> AddWarehouseTransferItemsDeleted(IList<WarehouseTransferItems> del) 
        {
            if (_WarehouseTransferItemsDeleted is null)
                _WarehouseTransferItemsDeleted = new List<WarehouseTransferItems>();
            var lst = _WarehouseTransferItemsDeleted.ToList();
            lst.AddRange(del);
            _WarehouseTransferItemsDeleted = lst;
            return del;
        } 

        public virtual void SetWarehouseTransferItemsDeleted(IList<WarehouseTransferItems> del) =>
            _WarehouseTransferItemsDeleted = del;

        public virtual void ClearWarehouseTransferItemsDeleted() =>
            _WarehouseTransferItemsDeleted = null;


        protected IList<WarehouseTransferItems> _WarehouseTransferItems;

        public virtual IList<WarehouseTransferItems> WarehouseTransferItems 
        { 
            get 
            {
                if (_WarehouseTransferItems is null)
                    _WarehouseTransferItems = new List<WarehouseTransferItems>();
                return _WarehouseTransferItems;
            } 
            set
            {
                if (value != null)
                {
                    var valueList = value.ToList();
                    valueList.ForEach(i => i?.SetParent(this));
                    _WarehouseTransferItems = valueList;
                }
                else
                    _WarehouseTransferItems = null;
            } 
        }

        public virtual void CopyWarehouseTransferItemsFrom(WarehouseTransferData data) 
        {
            if  (data is null) return;
            var lstDeleted = WarehouseTransferItems?.CopyFrom(data.WarehouseTransferItems, new string[] {"WarehouseTransferUuid"});
            SetWarehouseTransferItemsDeleted(lstDeleted);
            foreach (var c in WarehouseTransferItems)
                c?.CopyChildrenFrom(data.WarehouseTransferItems?.FindByRowNum(c.RowNum));
        } 

        public virtual WarehouseTransferItems NewWarehouseTransferItems() => new WarehouseTransferItems(dbFactory);

        public virtual WarehouseTransferItems AddWarehouseTransferItems(WarehouseTransferItems obj) => 
            WarehouseTransferItems.AddOrReplace(obj.SetParent(this));

        public virtual WarehouseTransferItems RemoveWarehouseTransferItems(WarehouseTransferItems obj) => 
            AddWarehouseTransferItemsDeleted(WarehouseTransferItems.RemoveObject(obj.SetParent(this)));

        public virtual IList<WarehouseTransferItems> GetWarehouseTransferItemsByWarehouseTransferUuid(string WarehouseTransferUuid) =>
            (string.IsNullOrEmpty(WarehouseTransferUuid)) 
                ? null 
                : dbFactory.Find<WarehouseTransferItems>("WHERE WarehouseTransferUuid = @0 ORDER BY Seq ", WarehouseTransferUuid).ToList();

        public virtual bool SaveWarehouseTransferItems(IList<WarehouseTransferItems> data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteWarehouseTransferItems(IList<WarehouseTransferItems> data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<IList<WarehouseTransferItems>> GetWarehouseTransferItemsByWarehouseTransferUuidAsync(string WarehouseTransferUuid) =>
            (string.IsNullOrEmpty(WarehouseTransferUuid)) 
                ? null
                : (await dbFactory.FindAsync<WarehouseTransferItems>("WHERE WarehouseTransferUuid = @0 ORDER BY Seq ", WarehouseTransferUuid)).ToList();

        public virtual async Task<bool> SaveWarehouseTransferItemsAsync(IList<WarehouseTransferItems> data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteWarehouseTransferItemsAsync(IList<WarehouseTransferItems> data) =>
            (data is null) ? 0 : await data.DeleteAsync();

        public virtual IList<WarehouseTransferItems> CheckIntegrityWarehouseTransferItems()
        {
            if (WarehouseTransferItems is null || WarehouseTransferHeader is null) 
                return WarehouseTransferItems;
            var seq = 0;
            WarehouseTransferItems.RemoveEmpty();
            var children = WarehouseTransferItems.ToList();
            foreach (var child in children.Where(x => x != null))
            {
                child.SetParent(this);
                if (child.WarehouseTransferUuid != WarehouseTransferHeader.WarehouseTransferUuid)
                    child.WarehouseTransferUuid = WarehouseTransferHeader.WarehouseTransferUuid;
                seq += 1;
                child.Seq = seq;
                child.CheckIntegrity();
            }
            return children;
        }



        #endregion WarehouseTransferItems - Generated 


    }
}


