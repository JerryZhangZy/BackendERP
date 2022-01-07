              
    

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
    /// Represents a PoTransactionData.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    public partial class PoTransactionData : StructureRepository<PoTransactionData>
    {
        public PoTransactionData() : base() {}
        public PoTransactionData(IDataBaseFactory dbFactory): base(dbFactory) {}

        [JsonIgnore, XmlIgnore]
        public new bool IsNew => PoTransaction.IsNew;

        [JsonIgnore, XmlIgnore]
        public new string UniqueId => PoTransaction.UniqueId;
        
		 [JsonIgnore, XmlIgnore] 
		public static string PoTransactionTable ="PoTransaction ";
		
		 [JsonIgnore, XmlIgnore] 
		public static string PoTransactionItemsTable ="PoTransactionItems ";
		
        #region CRUD Methods

        public override bool Equals(PoTransactionData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!string.IsNullOrWhiteSpace(UniqueId) && !string.IsNullOrWhiteSpace(other.UniqueId) && !UniqueId.Equals(other.UniqueId)) return false;
            return ChildrenEquals(other);
        }
        public virtual bool ChildrenEquals(PoTransactionData other)
        {
			if (PoTransaction == null && other.PoTransaction != null || PoTransaction != null && other.PoTransaction == null) 
				return false; 
			if (PoTransaction != null && other.PoTransaction != null && !PoTransaction.Equals(other.PoTransaction)) 
				return false; 
			if (PoTransactionItems == null && other.PoTransactionItems != null || PoTransactionItems != null && other.PoTransactionItems == null) 
				return false; 
			if (PoTransactionItems != null && other.PoTransactionItems != null && !PoTransactionItems.EqualsList(other.PoTransactionItems)) 
				return false; 
            return true;
        }

        // Check Children table Integrity
        public override PoTransactionData CheckIntegrity()
        {
			if (PoTransaction is null) return this; 
			PoTransaction.CheckIntegrity(); 
			CheckIntegrityPoTransactionItems(); 
			CheckIntegrityOthers(); 
            return this;
        }

        partial void ClearOthers();
        public override void Clear()
        {
			PoTransaction?.Clear(); 
			PoTransactionItems = new List<PoTransactionItems>(); 
			ClearPoTransactionItemsDeleted(); 
			ClearOthers(); 
			if (_OnClear != null)
				_OnClear(this);
            return;
        }

        public override void New()
        {
            Clear();
			PoTransaction = NewPoTransaction(); 
			PoTransactionItems = new List<PoTransactionItems>(); 
			AddPoTransactionItems(NewPoTransactionItems()); 
			ClearPoTransactionItemsDeleted(); 
            return;
        }

        public virtual void CopyFrom(PoTransactionData data)
        {
			CopyPoTransactionFrom(data); 
			CopyPoTransactionItemsFrom(data); 
            CheckIntegrity();
            return;
        }

        public override PoTransactionData Clone()
        {
			var newData = new PoTransactionData(); 
			newData.New(); 
			newData?.CopyFrom(this); 
			newData.PoTransaction.ClearMetaData(); 
			newData.PoTransactionItems.ClearMetaData(); 
            newData.CheckIntegrity();
            return newData;
        }

        public override bool Get(long RowNum)
        {
			var obj = GetPoTransaction(RowNum); 
			if (obj is null) return false; 
			PoTransaction = obj; 
			GetOthers(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        public override bool GetById(string TransUuid)
        {
			var obj = GetPoTransactionByTransUuid(TransUuid); 
			if (obj is null) return false; 
			PoTransaction = obj; 
			GetOthers(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        protected virtual void GetOthers()
        {
            
			if (string.IsNullOrEmpty(PoTransaction.TransUuid)) return; 
			PoTransactionItems = GetPoTransactionItemsByTransUuid(PoTransaction.TransUuid); 
        }

        public override bool Save()
        {
			if (PoTransaction is null || string.IsNullOrEmpty(PoTransaction.TransUuid)) return false; 
			CheckIntegrity();
			if (_OnBeforeSave != null)
				if (!_OnBeforeSave(this)) return false;
			dbFactory.Begin();

			 if (NeedSave(PoTransactionTable))
			{
				PoTransaction.SetDataBaseFactory(dbFactory);
				if (!PoTransaction.Save()) return false;
			}

			 if (NeedSave(PoTransactionItemsTable))
			{
				if (PoTransactionItems != null) 
					PoTransactionItems.SetDataBaseFactory(dbFactory)?.Save();
				var delPoTransactionItems = _PoTransactionItemsDeleted;
				if (delPoTransactionItems != null)
					delPoTransactionItems.SetDataBaseFactory(dbFactory)?.Delete();
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
			if (PoTransaction is null || string.IsNullOrEmpty(PoTransaction.TransUuid)) return false; 
			if (_OnBeforeDelete != null)
				if (!_OnBeforeDelete(this)) return false;
			dbFactory.Begin(); 

			 if (NeedDelete(PoTransactionTable))
			{
				PoTransaction.SetDataBaseFactory(dbFactory); 
				if (PoTransaction.Delete() <= 0) return false; 
			}
			 if (NeedDelete(PoTransactionItemsTable))
			{
				if (PoTransactionItems != null) 
					PoTransactionItems?.SetDataBaseFactory(dbFactory)?.Delete(); 
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
			var obj = await GetPoTransactionAsync(RowNum); 
			if (obj is null) return false; 
			PoTransaction = obj; 
			await GetOthersAsync(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        public override async Task<bool> GetByIdAsync(string TransUuid)
        {
			var obj = await GetPoTransactionByTransUuidAsync(TransUuid); 
			if (obj is null) return false; 
			PoTransaction = obj; 
			await GetOthersAsync(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        protected virtual async Task GetOthersAsync()
        {
            
			if (string.IsNullOrEmpty(PoTransaction.TransUuid)) return; 
			PoTransactionItems = await GetPoTransactionItemsByTransUuidAsync(PoTransaction.TransUuid); 
        }

        public override async Task<bool> SaveAsync()
        {
			if (PoTransaction is null || string.IsNullOrEmpty(PoTransaction.TransUuid)) return false; 
			CheckIntegrity(); 
			if (_OnBeforeSave != null)
				if (!_OnBeforeSave(this)) return false;
			dbFactory.Begin(); 

			 if (NeedSave(PoTransactionTable))
			{
				PoTransaction.SetDataBaseFactory(dbFactory); 
				if (!(await PoTransaction.SaveAsync())) return false; 
			}
			 if (NeedSave(PoTransactionItemsTable))
			{
				if (PoTransactionItems != null) 
					await PoTransactionItems.SetDataBaseFactory(dbFactory).SaveAsync(); 
				var delPoTransactionItems = _PoTransactionItemsDeleted;
				if (delPoTransactionItems != null)
					await delPoTransactionItems.SetDataBaseFactory(dbFactory).DeleteAsync();
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
			if (PoTransaction is null || string.IsNullOrEmpty(PoTransaction.TransUuid)) return false; 
			if (_OnBeforeDelete != null)
				if (!_OnBeforeDelete(this)) return false;
			dbFactory.Begin(); 
			 if (NeedDelete(PoTransactionTable))
			{
			PoTransaction.SetDataBaseFactory(dbFactory); 
			if ((await PoTransaction.DeleteAsync()) <= 0) return false; 
			}
			 if (NeedDelete(PoTransactionItemsTable))
			{
				if (PoTransactionItems != null) 
					await PoTransactionItems.SetDataBaseFactory(dbFactory).DeleteAsync(); 
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


        #region PoTransaction - Generated 
    

        // one to one children
        protected PoTransaction _PoTransaction;

        public virtual PoTransaction PoTransaction 
        { 
            get => _PoTransaction;
            set => _PoTransaction = value?.SetParent(this); 
        }

        public virtual void CopyPoTransactionFrom(PoTransactionData data) => 
            PoTransaction?.CopyFrom(data.PoTransaction, new string[] {"TransUuid"});

        public virtual PoTransaction NewPoTransaction() => new PoTransaction(dbFactory).SetParent(this);

        public virtual PoTransaction GetPoTransaction(long RowNum) =>
            (RowNum <= 0) ? null : dbFactory.Get<PoTransaction>(RowNum);

        public virtual PoTransaction GetPoTransactionByTransUuid(string TransUuid) =>
            (string.IsNullOrEmpty(TransUuid)) ? null : dbFactory.GetById<PoTransaction>(TransUuid);

        public virtual bool SavePoTransaction(PoTransaction data) =>
            (data is null) ? false : data.Save();

        public virtual int DeletePoTransaction(PoTransaction data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<PoTransaction> GetPoTransactionAsync(long RowNum) =>
            (RowNum <= 0) ? null : await dbFactory.GetAsync<PoTransaction>(RowNum);

        public virtual async Task<PoTransaction> GetPoTransactionByTransUuidAsync(string TransUuid) =>
            (string.IsNullOrEmpty(TransUuid)) ? null : await dbFactory.GetByIdAsync<PoTransaction>(TransUuid);

        public virtual async Task<bool> SavePoTransactionAsync(PoTransaction data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeletePoTransactionAsync(PoTransaction data) =>
            (data is null) ? 0 : await data.DeleteAsync();




        #endregion PoTransaction - Generated 

        #region PoTransactionItems - Generated 
        // One to many children
        protected IList<PoTransactionItems> _PoTransactionItemsDeleted;
        public virtual PoTransactionItems AddPoTransactionItemsDeleted(PoTransactionItems del) 
        {
            if (_PoTransactionItemsDeleted is null)
                _PoTransactionItemsDeleted = new List<PoTransactionItems>();
            var lst = _PoTransactionItemsDeleted.ToList();
            lst.Add(del);
            _PoTransactionItemsDeleted = lst;
            return del;
        } 

        public virtual IList<PoTransactionItems> AddPoTransactionItemsDeleted(IList<PoTransactionItems> del) 
        {
            if (_PoTransactionItemsDeleted is null)
                _PoTransactionItemsDeleted = new List<PoTransactionItems>();
            var lst = _PoTransactionItemsDeleted.ToList();
            lst.AddRange(del);
            _PoTransactionItemsDeleted = lst;
            return del;
        } 

        public virtual void SetPoTransactionItemsDeleted(IList<PoTransactionItems> del) =>
            _PoTransactionItemsDeleted = del;

        public virtual void ClearPoTransactionItemsDeleted() =>
            _PoTransactionItemsDeleted = null;


        protected IList<PoTransactionItems> _PoTransactionItems;

        public virtual IList<PoTransactionItems> PoTransactionItems 
        { 
            get 
            {
                if (_PoTransactionItems is null)
                    _PoTransactionItems = new List<PoTransactionItems>();
                return _PoTransactionItems;
            } 
            set
            {
                if (value != null)
                {
                    var valueList = value.ToList();
                    valueList.ForEach(i => i?.SetParent(this));
                    _PoTransactionItems = valueList;
                }
                else
                    _PoTransactionItems = null;
            } 
        }

        public virtual void CopyPoTransactionItemsFrom(PoTransactionData data) 
        {
            if  (data is null) return;
            var lstDeleted = PoTransactionItems?.CopyFrom(data.PoTransactionItems, new string[] {"TransUuid"});
            SetPoTransactionItemsDeleted(lstDeleted);
            foreach (var c in PoTransactionItems)
                c?.CopyChildrenFrom(data.PoTransactionItems?.FindByRowNum(c.RowNum));
        } 

        public virtual PoTransactionItems NewPoTransactionItems() => new PoTransactionItems(dbFactory);

        public virtual PoTransactionItems AddPoTransactionItems(PoTransactionItems obj) => 
            PoTransactionItems.AddOrReplace(obj.SetParent(this));

        public virtual PoTransactionItems RemovePoTransactionItems(PoTransactionItems obj) => 
            AddPoTransactionItemsDeleted(PoTransactionItems.RemoveObject(obj.SetParent(this)));

        public virtual IList<PoTransactionItems> GetPoTransactionItemsByTransUuid(string TransUuid) =>
            (string.IsNullOrEmpty(TransUuid)) 
                ? null 
                : dbFactory.Find<PoTransactionItems>("WHERE TransUuid = @0 ORDER BY Seq ", TransUuid).ToList();

        public virtual bool SavePoTransactionItems(IList<PoTransactionItems> data) =>
            (data is null) ? false : data.Save();

        public virtual int DeletePoTransactionItems(IList<PoTransactionItems> data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<IList<PoTransactionItems>> GetPoTransactionItemsByTransUuidAsync(string TransUuid) =>
            (string.IsNullOrEmpty(TransUuid)) 
                ? null
                : (await dbFactory.FindAsync<PoTransactionItems>("WHERE TransUuid = @0 ORDER BY Seq ", TransUuid)).ToList();

        public virtual async Task<bool> SavePoTransactionItemsAsync(IList<PoTransactionItems> data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeletePoTransactionItemsAsync(IList<PoTransactionItems> data) =>
            (data is null) ? 0 : await data.DeleteAsync();

        public virtual IList<PoTransactionItems> CheckIntegrityPoTransactionItems()
        {
            if (PoTransactionItems is null || PoTransaction is null) 
                return PoTransactionItems;
            var seq = 0;
            PoTransactionItems.RemoveEmpty();
            var children = PoTransactionItems.ToList();
            foreach (var child in children.Where(x => x != null))
            {
                child.SetParent(this);
                if (child.TransUuid != PoTransaction.TransUuid)
                    child.TransUuid = PoTransaction.TransUuid;
                seq += 1;
                child.Seq = seq;
                child.CheckIntegrity();
            }
            return children;
        }



        #endregion PoTransactionItems - Generated 


    }
}



