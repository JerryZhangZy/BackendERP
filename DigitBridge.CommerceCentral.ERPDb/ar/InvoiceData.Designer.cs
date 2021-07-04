

              
    

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
    /// Represents a InvoiceData.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public partial class InvoiceData : StructureRepository<InvoiceData>
    {
        public InvoiceData() : base() {}
        public InvoiceData(IDataBaseFactory dbFactory): base(dbFactory) {}

        [XmlIgnore, JsonIgnore]
        public new bool IsNew => InvoiceHeader.IsNew;

        [XmlIgnore, JsonIgnore]
        public new string UniqueId => InvoiceHeader.UniqueId;

        #region CRUD Methods

        public override bool Equals(InvoiceData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!string.IsNullOrWhiteSpace(UniqueId) && !string.IsNullOrWhiteSpace(other.UniqueId) && !UniqueId.Equals(other.UniqueId)) return false;
            return ChildrenEquals(other);
        }
        public virtual bool ChildrenEquals(InvoiceData other)
        {
			if (InvoiceHeader == null && other.InvoiceHeader != null || InvoiceHeader != null && other.InvoiceHeader == null) 
				return false; 
			if (InvoiceHeader != null && other.InvoiceHeader != null && !InvoiceHeader.Equals(other.InvoiceHeader)) 
				return false; 
			if (InvoiceHeaderInfo == null && other.InvoiceHeaderInfo != null || InvoiceHeaderInfo != null && other.InvoiceHeaderInfo == null) 
				return false; 
			if (InvoiceHeaderInfo != null && other.InvoiceHeaderInfo != null && !InvoiceHeaderInfo.Equals(other.InvoiceHeaderInfo)) 
				return false; 
			if (InvoiceHeaderAttributes == null && other.InvoiceHeaderAttributes != null || InvoiceHeaderAttributes != null && other.InvoiceHeaderAttributes == null) 
				return false; 
			if (InvoiceHeaderAttributes != null && other.InvoiceHeaderAttributes != null && !InvoiceHeaderAttributes.Equals(other.InvoiceHeaderAttributes)) 
				return false; 
			if (InvoiceItems == null && other.InvoiceItems != null || InvoiceItems != null && other.InvoiceItems == null) 
				return false; 
			if (InvoiceItems != null && other.InvoiceItems != null && !InvoiceItems.EqualsList(other.InvoiceItems)) 
				return false; 
			if (InvoiceItemsAttributes == null && other.InvoiceItemsAttributes != null || InvoiceItemsAttributes != null && other.InvoiceItemsAttributes == null) 
				return false; 
			if (InvoiceItemsAttributes != null && other.InvoiceItemsAttributes != null && !InvoiceItemsAttributes.EqualsList(other.InvoiceItemsAttributes)) 
				return false; 
            return true;
        }

        // Check Children table Integrity
        public virtual InvoiceData CheckIntegrity()
        {
			if (InvoiceHeader is null) return this; 
			InvoiceHeader.CheckUniqueId(); 
			CheckIntegrityInvoiceHeaderInfo(); 
			CheckIntegrityInvoiceHeaderAttributes(); 
			CheckIntegrityInvoiceItems(); 
			CheckIntegrityInvoiceItemsAttributes(); 
            return this;
        }

        partial void ClearOthers();
        public override void Clear()
        {
			InvoiceHeader?.Clear(); 
			InvoiceHeaderInfo?.Clear(); 
			InvoiceHeaderAttributes?.Clear(); 
			InvoiceItems = new List<InvoiceItems>(); 
			ClearInvoiceItemsDeleted(); 
			InvoiceItemsAttributes = new List<InvoiceItemsAttributes>(); 
			ClearOthers(); 
			if (_OnClear != null)
				_OnClear(this);
            return;
        }

        public override void New()
        {
            Clear();
			InvoiceHeader = NewInvoiceHeader(); 
			InvoiceHeaderInfo = NewInvoiceHeaderInfo(); 
			InvoiceHeaderAttributes = NewInvoiceHeaderAttributes(); 
			InvoiceItems = new List<InvoiceItems>(); 
			AddInvoiceItems(NewInvoiceItems()); 
			InvoiceItems.ToList().ForEach(x => x?.NewChildren()); 
			ClearInvoiceItemsDeleted(); 
            return;
        }

        public virtual void CopyFrom(InvoiceData data)
        {
			CopyInvoiceHeaderFrom(data); 
			CopyInvoiceHeaderInfoFrom(data); 
			CopyInvoiceHeaderAttributesFrom(data); 
			CopyInvoiceItemsFrom(data); 
            CheckIntegrity();
            return;
        }

        public override InvoiceData Clone()
        {
			var newData = new InvoiceData(); 
			newData.New(); 
			newData?.CopyFrom(this); 
			newData.InvoiceHeader.ClearMetaData(); 
			newData.InvoiceHeaderInfo.ClearMetaData(); 
			newData.InvoiceHeaderAttributes.ClearMetaData(); 
			newData.InvoiceItems.ClearMetaData(); 
			newData.InvoiceItemsAttributes.ClearMetaData(); 
            newData.CheckIntegrity();
            return newData;
        }

        public override bool Get(long RowNum)
        {
			var obj = GetInvoiceHeader(RowNum); 
			if (obj is null) return false; 
			InvoiceHeader = obj; 
			GetOthers(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        public override bool GetById(string InvoiceUuid)
        {
			var obj = GetInvoiceHeaderByInvoiceUuid(InvoiceUuid); 
			if (obj is null) return false; 
			InvoiceHeader = obj; 
			GetOthers(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        protected virtual void GetOthers()
        {
            
			if (string.IsNullOrEmpty(InvoiceHeader.InvoiceUuid)) return; 
			InvoiceHeaderInfo = GetInvoiceHeaderInfoByInvoiceUuid(InvoiceHeader.InvoiceUuid); 
			InvoiceHeaderAttributes = GetInvoiceHeaderAttributesByInvoiceUuid(InvoiceHeader.InvoiceUuid); 
			InvoiceItems = GetInvoiceItemsByInvoiceUuid(InvoiceHeader.InvoiceUuid); 
			InvoiceItemsAttributes = GetInvoiceItemsAttributesByInvoiceUuid(InvoiceHeader.InvoiceUuid); 
        }

        public override bool Save()
        {
			if (InvoiceHeader is null || string.IsNullOrEmpty(InvoiceHeader.InvoiceUuid)) return false; 
			CheckIntegrity();
			if (_OnBeforeSave != null)
				if (!_OnBeforeSave(this)) return false;
			dbFactory.Begin();
			InvoiceHeader.SetDataBaseFactory(dbFactory);
			if (!InvoiceHeader.Save()) return false;

			if (InvoiceHeaderInfo != null) 
				InvoiceHeaderInfo.SetDataBaseFactory(dbFactory)?.Save();

			if (InvoiceHeaderAttributes != null) 
				InvoiceHeaderAttributes.SetDataBaseFactory(dbFactory)?.Save();

			if (InvoiceItems != null) 
				InvoiceItems.SetDataBaseFactory(dbFactory)?.Save();
			var delInvoiceItems = _InvoiceItemsDeleted;
			if (delInvoiceItems != null)
				delInvoiceItems.SetDataBaseFactory(dbFactory)?.Delete();

			if (InvoiceItemsAttributes != null) 
				InvoiceItemsAttributes.SetDataBaseFactory(dbFactory)?.Save();
			var delChildrenInvoiceItemsAttributes = InvoiceItemsAttributesDeleted;
			if (delChildrenInvoiceItemsAttributes != null)
				delChildrenInvoiceItemsAttributes.SetDataBaseFactory(dbFactory)?.Delete();

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
			if (InvoiceHeader is null || string.IsNullOrEmpty(InvoiceHeader.InvoiceUuid)) return false; 
			if (_OnBeforeDelete != null)
				if (!_OnBeforeDelete(this)) return false;
			dbFactory.Begin(); 
			InvoiceHeader.SetDataBaseFactory(dbFactory); 
			if (InvoiceHeader.Delete() <= 0) return false; 
			if (InvoiceHeaderInfo != null) 
				InvoiceHeaderInfo?.SetDataBaseFactory(dbFactory)?.Delete(); 
			if (InvoiceHeaderAttributes != null) 
				InvoiceHeaderAttributes?.SetDataBaseFactory(dbFactory)?.Delete(); 
			if (InvoiceItems != null) 
				InvoiceItems?.SetDataBaseFactory(dbFactory)?.Delete(); 
			if (InvoiceItemsAttributes != null) 
				InvoiceItemsAttributes?.SetDataBaseFactory(dbFactory)?.Delete(); 
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
			var obj = await GetInvoiceHeaderAsync(RowNum); 
			if (obj is null) return false; 
			InvoiceHeader = obj; 
			await GetOthersAsync(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        public override async Task<bool> GetByIdAsync(string InvoiceUuid)
        {
			var obj = await GetInvoiceHeaderByInvoiceUuidAsync(InvoiceUuid); 
			if (obj is null) return false; 
			InvoiceHeader = obj; 
			await GetOthersAsync(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        protected virtual async Task GetOthersAsync()
        {
            
			if (string.IsNullOrEmpty(InvoiceHeader.InvoiceUuid)) return; 
			InvoiceHeaderInfo = await GetInvoiceHeaderInfoByInvoiceUuidAsync(InvoiceHeader.InvoiceUuid); 
			InvoiceHeaderAttributes = await GetInvoiceHeaderAttributesByInvoiceUuidAsync(InvoiceHeader.InvoiceUuid); 
			InvoiceItems = await GetInvoiceItemsByInvoiceUuidAsync(InvoiceHeader.InvoiceUuid); 
			InvoiceItemsAttributes = await GetInvoiceItemsAttributesByInvoiceUuidAsync(InvoiceHeader.InvoiceUuid); 
        }

        public override async Task<bool> SaveAsync()
        {
			if (InvoiceHeader is null || string.IsNullOrEmpty(InvoiceHeader.InvoiceUuid)) return false; 
			CheckIntegrity(); 
			if (_OnBeforeSave != null)
				if (!_OnBeforeSave(this)) return false;
			dbFactory.Begin(); 
			InvoiceHeader.SetDataBaseFactory(dbFactory); 
			if (!(await InvoiceHeader.SaveAsync().ConfigureAwait(false))) return false; 
			if (InvoiceHeaderInfo != null) 
				await InvoiceHeaderInfo.SetDataBaseFactory(dbFactory).SaveAsync().ConfigureAwait(false); 

			if (InvoiceHeaderAttributes != null) 
				await InvoiceHeaderAttributes.SetDataBaseFactory(dbFactory).SaveAsync().ConfigureAwait(false); 

			if (InvoiceItems != null) 
				await InvoiceItems.SetDataBaseFactory(dbFactory).SaveAsync().ConfigureAwait(false); 
			var delInvoiceItems = _InvoiceItemsDeleted;
			if (delInvoiceItems != null)
				await delInvoiceItems.SetDataBaseFactory(dbFactory).DeleteAsync().ConfigureAwait(false);

			if (InvoiceItemsAttributes != null) 
				await InvoiceItemsAttributes.SetDataBaseFactory(dbFactory).SaveAsync().ConfigureAwait(false); 
			var delInvoiceItemsAttributes = InvoiceItemsAttributesDeleted;
			if (delInvoiceItemsAttributes != null)
				await delInvoiceItemsAttributes.SetDataBaseFactory(dbFactory).DeleteAsync().ConfigureAwait(false);

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
			if (InvoiceHeader is null || string.IsNullOrEmpty(InvoiceHeader.InvoiceUuid)) return false; 
			if (_OnBeforeDelete != null)
				if (!_OnBeforeDelete(this)) return false;
			dbFactory.Begin(); 
			InvoiceHeader.SetDataBaseFactory(dbFactory); 
			if ((await InvoiceHeader.DeleteAsync().ConfigureAwait(false)) <= 0) return false; 
			if (InvoiceHeaderInfo != null) 
				await InvoiceHeaderInfo.SetDataBaseFactory(dbFactory).DeleteAsync().ConfigureAwait(false); 
			if (InvoiceHeaderAttributes != null) 
				await InvoiceHeaderAttributes.SetDataBaseFactory(dbFactory).DeleteAsync().ConfigureAwait(false); 
			if (InvoiceItems != null) 
				await InvoiceItems.SetDataBaseFactory(dbFactory).DeleteAsync().ConfigureAwait(false); 
			if (InvoiceItemsAttributes != null) 
				await InvoiceItemsAttributes.SetDataBaseFactory(dbFactory).DeleteAsync().ConfigureAwait(false); 
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


        #region InvoiceHeader - Generated 
    

        // one to one children
        protected InvoiceHeader _InvoiceHeader;

        public virtual InvoiceHeader InvoiceHeader 
        { 
            get => _InvoiceHeader;
            set => _InvoiceHeader = value?.SetParent(this); 
        }

        public virtual void CopyInvoiceHeaderFrom(InvoiceData data) => 
            InvoiceHeader?.CopyFrom(data.InvoiceHeader, new string[] {"InvoiceUuid"});

        public virtual InvoiceHeader NewInvoiceHeader() => new InvoiceHeader(dbFactory).SetParent(this);

        public virtual InvoiceHeader GetInvoiceHeader(long RowNum) =>
            (RowNum <= 0) ? null : dbFactory.Get<InvoiceHeader>(RowNum);

        public virtual InvoiceHeader GetInvoiceHeaderByInvoiceUuid(string InvoiceUuid) =>
            (string.IsNullOrEmpty(InvoiceUuid)) ? null : dbFactory.GetById<InvoiceHeader>(InvoiceUuid);

        public virtual bool SaveInvoiceHeader(InvoiceHeader data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteInvoiceHeader(InvoiceHeader data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<InvoiceHeader> GetInvoiceHeaderAsync(long RowNum) =>
            (RowNum <= 0) ? null : await dbFactory.GetAsync<InvoiceHeader>(RowNum);

        public virtual async Task<InvoiceHeader> GetInvoiceHeaderByInvoiceUuidAsync(string InvoiceUuid) =>
            (string.IsNullOrEmpty(InvoiceUuid)) ? null : await dbFactory.GetByIdAsync<InvoiceHeader>(InvoiceUuid);

        public virtual async Task<bool> SaveInvoiceHeaderAsync(InvoiceHeader data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteInvoiceHeaderAsync(InvoiceHeader data) =>
            (data is null) ? 0 : await data.DeleteAsync();




        #endregion InvoiceHeader - Generated 

        #region InvoiceHeaderInfo - Generated 
    

        // one to one children
        protected InvoiceHeaderInfo _InvoiceHeaderInfo;

        public virtual InvoiceHeaderInfo InvoiceHeaderInfo 
        { 
            get => _InvoiceHeaderInfo;
            set => _InvoiceHeaderInfo = value?.SetParent(this); 
        }

        public virtual void CopyInvoiceHeaderInfoFrom(InvoiceData data) => 
            InvoiceHeaderInfo?.CopyFrom(data.InvoiceHeaderInfo, new string[] {"InvoiceUuid"});

        public virtual InvoiceHeaderInfo NewInvoiceHeaderInfo() => new InvoiceHeaderInfo(dbFactory).SetParent(this);

        public virtual InvoiceHeaderInfo GetInvoiceHeaderInfo(long RowNum) =>
            (RowNum <= 0) ? null : dbFactory.Get<InvoiceHeaderInfo>(RowNum);

        public virtual InvoiceHeaderInfo GetInvoiceHeaderInfoByInvoiceUuid(string InvoiceUuid) =>
            (string.IsNullOrEmpty(InvoiceUuid)) ? null : dbFactory.GetById<InvoiceHeaderInfo>(InvoiceUuid);

        public virtual bool SaveInvoiceHeaderInfo(InvoiceHeaderInfo data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteInvoiceHeaderInfo(InvoiceHeaderInfo data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<InvoiceHeaderInfo> GetInvoiceHeaderInfoAsync(long RowNum) =>
            (RowNum <= 0) ? null : await dbFactory.GetAsync<InvoiceHeaderInfo>(RowNum);

        public virtual async Task<InvoiceHeaderInfo> GetInvoiceHeaderInfoByInvoiceUuidAsync(string InvoiceUuid) =>
            (string.IsNullOrEmpty(InvoiceUuid)) ? null : await dbFactory.GetByIdAsync<InvoiceHeaderInfo>(InvoiceUuid);

        public virtual async Task<bool> SaveInvoiceHeaderInfoAsync(InvoiceHeaderInfo data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteInvoiceHeaderInfoAsync(InvoiceHeaderInfo data) =>
            (data is null) ? 0 : await data.DeleteAsync();

        public virtual InvoiceHeaderInfo CheckIntegrityInvoiceHeaderInfo()
        {
            if (InvoiceHeaderInfo is null || InvoiceHeader is null) 
                return InvoiceHeaderInfo;
            InvoiceHeaderInfo.SetParent(this);
            if (InvoiceHeaderInfo.InvoiceUuid != InvoiceHeader.InvoiceUuid)
                InvoiceHeaderInfo.InvoiceUuid = InvoiceHeader.InvoiceUuid;
            return InvoiceHeaderInfo;
        }



        #endregion InvoiceHeaderInfo - Generated 

        #region InvoiceHeaderAttributes - Generated 
    

        // one to one children
        protected InvoiceHeaderAttributes _InvoiceHeaderAttributes;

        public virtual InvoiceHeaderAttributes InvoiceHeaderAttributes 
        { 
            get => _InvoiceHeaderAttributes;
            set => _InvoiceHeaderAttributes = value?.SetParent(this); 
        }

        public virtual void CopyInvoiceHeaderAttributesFrom(InvoiceData data) => 
            InvoiceHeaderAttributes?.CopyFrom(data.InvoiceHeaderAttributes, new string[] {"InvoiceUuid"});

        public virtual InvoiceHeaderAttributes NewInvoiceHeaderAttributes() => new InvoiceHeaderAttributes(dbFactory).SetParent(this);

        public virtual InvoiceHeaderAttributes GetInvoiceHeaderAttributes(long RowNum) =>
            (RowNum <= 0) ? null : dbFactory.Get<InvoiceHeaderAttributes>(RowNum);

        public virtual InvoiceHeaderAttributes GetInvoiceHeaderAttributesByInvoiceUuid(string InvoiceUuid) =>
            (string.IsNullOrEmpty(InvoiceUuid)) ? null : dbFactory.GetById<InvoiceHeaderAttributes>(InvoiceUuid);

        public virtual bool SaveInvoiceHeaderAttributes(InvoiceHeaderAttributes data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteInvoiceHeaderAttributes(InvoiceHeaderAttributes data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<InvoiceHeaderAttributes> GetInvoiceHeaderAttributesAsync(long RowNum) =>
            (RowNum <= 0) ? null : await dbFactory.GetAsync<InvoiceHeaderAttributes>(RowNum);

        public virtual async Task<InvoiceHeaderAttributes> GetInvoiceHeaderAttributesByInvoiceUuidAsync(string InvoiceUuid) =>
            (string.IsNullOrEmpty(InvoiceUuid)) ? null : await dbFactory.GetByIdAsync<InvoiceHeaderAttributes>(InvoiceUuid);

        public virtual async Task<bool> SaveInvoiceHeaderAttributesAsync(InvoiceHeaderAttributes data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteInvoiceHeaderAttributesAsync(InvoiceHeaderAttributes data) =>
            (data is null) ? 0 : await data.DeleteAsync();

        public virtual InvoiceHeaderAttributes CheckIntegrityInvoiceHeaderAttributes()
        {
            if (InvoiceHeaderAttributes is null || InvoiceHeader is null) 
                return InvoiceHeaderAttributes;
            InvoiceHeaderAttributes.SetParent(this);
            if (InvoiceHeaderAttributes.InvoiceUuid != InvoiceHeader.InvoiceUuid)
                InvoiceHeaderAttributes.InvoiceUuid = InvoiceHeader.InvoiceUuid;
            return InvoiceHeaderAttributes;
        }



        #endregion InvoiceHeaderAttributes - Generated 

        #region InvoiceItems - Generated 
        // One to many children
        protected IList<InvoiceItems> _InvoiceItemsDeleted;
        public virtual InvoiceItems AddInvoiceItemsDeleted(InvoiceItems del) 
        {
            if (_InvoiceItemsDeleted is null)
                _InvoiceItemsDeleted = new List<InvoiceItems>();
            var lst = _InvoiceItemsDeleted.ToList();
            lst.Add(del);
            _InvoiceItemsDeleted = lst;
            return del;
        } 

        public virtual IList<InvoiceItems> AddInvoiceItemsDeleted(IList<InvoiceItems> del) 
        {
            if (_InvoiceItemsDeleted is null)
                _InvoiceItemsDeleted = new List<InvoiceItems>();
            var lst = _InvoiceItemsDeleted.ToList();
            lst.AddRange(del);
            _InvoiceItemsDeleted = lst;
            return del;
        } 

        public virtual void SetInvoiceItemsDeleted(IList<InvoiceItems> del) =>
            _InvoiceItemsDeleted = del;

        public virtual void ClearInvoiceItemsDeleted() =>
            _InvoiceItemsDeleted = null;


        protected IList<InvoiceItems> _InvoiceItems;

        public virtual IList<InvoiceItems> InvoiceItems 
        { 
            get 
            {
                if (_InvoiceItems is null)
                    _InvoiceItems = new List<InvoiceItems>();
                return _InvoiceItems;
            } 
            set
            {
                if (value != null)
                {
                    var valueList = value.ToList();
                    valueList.ForEach(i => i?.SetParent(this));
                    _InvoiceItems = valueList;
                }
                else
                    _InvoiceItems = null;
            } 
        }

        public virtual void CopyInvoiceItemsFrom(InvoiceData data) 
        {
            if  (data is null) return;
            var lstDeleted = InvoiceItems?.CopyFrom(data.InvoiceItems, new string[] {"InvoiceUuid"});
            SetInvoiceItemsDeleted(lstDeleted);
            foreach (var c in InvoiceItems)
                c?.CopyChildrenFrom(data.InvoiceItems?.FindByRowNum(c.RowNum));
        } 

        public virtual InvoiceItems NewInvoiceItems() => new InvoiceItems(dbFactory);

        public virtual InvoiceItems AddInvoiceItems(InvoiceItems obj) => 
            InvoiceItems.AddOrReplace(obj.SetParent(this));

        public virtual InvoiceItems RemoveInvoiceItems(InvoiceItems obj) => 
            AddInvoiceItemsDeleted(InvoiceItems.RemoveObject(obj.SetParent(this)));

        public virtual IList<InvoiceItems> GetInvoiceItemsByInvoiceUuid(string InvoiceUuid) =>
            (string.IsNullOrEmpty(InvoiceUuid)) 
                ? null 
                : dbFactory.Find<InvoiceItems>("WHERE InvoiceUuid = @0 ORDER BY Seq ", InvoiceUuid).ToList();

        public virtual bool SaveInvoiceItems(IList<InvoiceItems> data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteInvoiceItems(IList<InvoiceItems> data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<IList<InvoiceItems>> GetInvoiceItemsByInvoiceUuidAsync(string InvoiceUuid) =>
            (string.IsNullOrEmpty(InvoiceUuid)) 
                ? null
                : (await dbFactory.FindAsync<InvoiceItems>("WHERE InvoiceUuid = @0 ORDER BY Seq ", InvoiceUuid)).ToList();

        public virtual async Task<bool> SaveInvoiceItemsAsync(IList<InvoiceItems> data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteInvoiceItemsAsync(IList<InvoiceItems> data) =>
            (data is null) ? 0 : await data.DeleteAsync();

        public virtual IList<InvoiceItems> CheckIntegrityInvoiceItems()
        {
            if (InvoiceItems is null || InvoiceHeader is null) 
                return InvoiceItems;
            var seq = 0;
            InvoiceItems.RemoveEmpty();
            var children = InvoiceItems.ToList();
            foreach (var child in children.Where(x => x != null))
            {
                child.SetParent(this);
                if (child.InvoiceUuid != InvoiceHeader.InvoiceUuid)
                    child.InvoiceUuid = InvoiceHeader.InvoiceUuid;
                seq += 1;
                child.Seq = seq;
            }
            return children;
        }



        #endregion InvoiceItems - Generated 

        #region InvoiceItemsAttributes - Generated 
        // grand children
        protected IList<InvoiceItemsAttributes> _InvoiceItemsAttributes;

        protected IList<InvoiceItemsAttributes> InvoiceItemsAttributes 
        { 
            get 
            {
                _InvoiceItemsAttributes = InvoiceItems is null ? null : InvoiceItems.SelectMany(x => x.GetChildrenInvoiceItemsAttributes()).ToList();
                return _InvoiceItemsAttributes;
            } 
            set
            {
                _InvoiceItemsAttributes = value;
                if (InvoiceItems != null)
                    foreach (var par in InvoiceItems)
                        par.SetChildrenInvoiceItemsAttributes(_InvoiceItemsAttributes);
            } 
        }

        protected IList<InvoiceItemsAttributes> InvoiceItemsAttributesDeleted 
        { 
            get 
            {
                var deleted = new List<InvoiceItemsAttributes>();
                if (_InvoiceItemsDeleted != null)
                {
                    var del = _InvoiceItemsDeleted
                            .Where(x => x?.GetChildrenInvoiceItemsAttributes() != null)
                            .SelectMany(x => x?.GetChildrenInvoiceItemsAttributes());
                    if (del.Any())
                        deleted.AddRange(del.ToList());
                }
                if (InvoiceItems != null)
                {
                    var delChildren = InvoiceItems
                                    .Where(x => x?.GetChildrenDeletedInvoiceItemsAttributes() != null)
                                    .SelectMany(x => x?.GetChildrenDeletedInvoiceItemsAttributes());
                    if (delChildren.Any())
                        deleted.AddRange(delChildren.ToList());
                }
                return deleted;
            } 
        }

        public virtual IList<InvoiceItemsAttributes> GetInvoiceItemsAttributesByInvoiceUuid(string InvoiceUuid) =>
            (string.IsNullOrEmpty(InvoiceUuid)) 
                ? null 
                : dbFactory.Find<InvoiceItemsAttributes>("WHERE InvoiceUuid = @0 ORDER BY RowNum ", InvoiceUuid).ToList();

        public virtual bool SaveInvoiceItemsAttributes(IList<InvoiceItemsAttributes> data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteInvoiceItemsAttributes(IList<InvoiceItemsAttributes> data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<IList<InvoiceItemsAttributes>> GetInvoiceItemsAttributesByInvoiceUuidAsync(string InvoiceUuid) =>
            (string.IsNullOrEmpty(InvoiceUuid)) 
                ? null
                : (await dbFactory.FindAsync<InvoiceItemsAttributes>("WHERE InvoiceUuid = @0 ORDER BY RowNum ", InvoiceUuid)).ToList();

        public virtual async Task<bool> SaveInvoiceItemsAttributesAsync(IList<InvoiceItemsAttributes> data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteInvoiceItemsAttributesAsync(IList<InvoiceItemsAttributes> data) =>
            (data is null) ? 0 : await data.DeleteAsync();

        public virtual IList<InvoiceItemsAttributes> CheckIntegrityInvoiceItemsAttributes()
        {
            if (InvoiceItemsAttributes is null || InvoiceHeader is null) 
                return InvoiceItemsAttributes;
            var seq = 0;
            InvoiceItemsAttributes.RemoveEmpty();
            var children = InvoiceItemsAttributes.ToList();
            foreach (var child in children.Where(x => x != null))
            {
                child.SetParent(this);
                if (child.InvoiceUuid != InvoiceHeader.InvoiceUuid)
                    child.InvoiceUuid = InvoiceHeader.InvoiceUuid;
            }
            return children;
        }


        #endregion InvoiceItemsAttributes - Generated 


    }
}



