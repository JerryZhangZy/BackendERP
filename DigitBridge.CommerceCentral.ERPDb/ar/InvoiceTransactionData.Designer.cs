

              
    

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
    /// Represents a InvoiceTransactionData.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    public partial class InvoiceTransactionData : StructureRepository<InvoiceTransactionData>
    {
        public InvoiceTransactionData() : base() {}
        public InvoiceTransactionData(IDataBaseFactory dbFactory): base(dbFactory) {}

        [JsonIgnore, XmlIgnore]
        public new bool IsNew => InvoiceTransaction.IsNew;

        [JsonIgnore, XmlIgnore]
        public new string UniqueId => InvoiceTransaction.UniqueId;

        #region CRUD Methods

        public override bool Equals(InvoiceTransactionData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!string.IsNullOrWhiteSpace(UniqueId) && !string.IsNullOrWhiteSpace(other.UniqueId) && !UniqueId.Equals(other.UniqueId)) return false;
            return ChildrenEquals(other);
        }
        public virtual bool ChildrenEquals(InvoiceTransactionData other)
        {
			if (InvoiceTransaction == null && other.InvoiceTransaction != null || InvoiceTransaction != null && other.InvoiceTransaction == null) 
				return false; 
			if (InvoiceTransaction != null && other.InvoiceTransaction != null && !InvoiceTransaction.Equals(other.InvoiceTransaction)) 
				return false; 
			if (InvoiceReturnItems == null && other.InvoiceReturnItems != null || InvoiceReturnItems != null && other.InvoiceReturnItems == null) 
				return false; 
			if (InvoiceReturnItems != null && other.InvoiceReturnItems != null && !InvoiceReturnItems.EqualsList(other.InvoiceReturnItems)) 
				return false; 
            return true;
        }

        // Check Children table Integrity
        public virtual InvoiceTransactionData CheckIntegrity()
        {
			if (InvoiceTransaction is null) return this; 
			InvoiceTransaction.CheckUniqueId(); 
			CheckIntegrityInvoiceReturnItems(); 
            return this;
        }

        partial void ClearOthers();
        public override void Clear()
        {
			InvoiceTransaction?.Clear(); 
			InvoiceReturnItems = new List<InvoiceReturnItems>(); 
			ClearInvoiceReturnItemsDeleted(); 
			ClearOthers(); 
			if (_OnClear != null)
				_OnClear(this);
            return;
        }

        public override void New()
        {
            Clear();
			InvoiceTransaction = NewInvoiceTransaction(); 
			InvoiceReturnItems = new List<InvoiceReturnItems>(); 
			AddInvoiceReturnItems(NewInvoiceReturnItems()); 
			ClearInvoiceReturnItemsDeleted(); 
            return;
        }

        public virtual void CopyFrom(InvoiceTransactionData data)
        {
			CopyInvoiceTransactionFrom(data); 
			CopyInvoiceReturnItemsFrom(data); 
            CheckIntegrity();
            return;
        }

        public override InvoiceTransactionData Clone()
        {
			var newData = new InvoiceTransactionData(); 
			newData.New(); 
			newData?.CopyFrom(this); 
			newData.InvoiceTransaction.ClearMetaData(); 
			newData.InvoiceReturnItems.ClearMetaData(); 
            newData.CheckIntegrity();
            return newData;
        }

        public override bool Get(long RowNum)
        {
			var obj = GetInvoiceTransaction(RowNum); 
			if (obj is null) return false; 
			InvoiceTransaction = obj; 
			GetOthers(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        public override bool GetById(string TransUuid)
        {
			var obj = GetInvoiceTransactionByTransUuid(TransUuid); 
			if (obj is null) return false; 
			InvoiceTransaction = obj; 
			GetOthers(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        protected virtual void GetOthers()
        {
            
			if (string.IsNullOrEmpty(InvoiceTransaction.TransUuid)) return; 
			InvoiceReturnItems = GetInvoiceReturnItemsByTransUuid(InvoiceTransaction.TransUuid); 
        }

        public override bool Save()
        {
			if (InvoiceTransaction is null || string.IsNullOrEmpty(InvoiceTransaction.TransUuid)) return false; 
			CheckIntegrity();
			if (_OnBeforeSave != null)
				if (!_OnBeforeSave(this)) return false;
			dbFactory.Begin();
			InvoiceTransaction.SetDataBaseFactory(dbFactory);
			if (!InvoiceTransaction.Save()) return false;

			if (InvoiceReturnItems != null) 
				InvoiceReturnItems.SetDataBaseFactory(dbFactory)?.Save();
			var delInvoiceReturnItems = _InvoiceReturnItemsDeleted;
			if (delInvoiceReturnItems != null)
				delInvoiceReturnItems.SetDataBaseFactory(dbFactory)?.Delete();

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
			if (InvoiceTransaction is null || string.IsNullOrEmpty(InvoiceTransaction.TransUuid)) return false; 
			if (_OnBeforeDelete != null)
				if (!_OnBeforeDelete(this)) return false;
			dbFactory.Begin(); 
			InvoiceTransaction.SetDataBaseFactory(dbFactory); 
			if (InvoiceTransaction.Delete() <= 0) return false; 
			if (InvoiceReturnItems != null) 
				InvoiceReturnItems?.SetDataBaseFactory(dbFactory)?.Delete(); 
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
			var obj = await GetInvoiceTransactionAsync(RowNum); 
			if (obj is null) return false; 
			InvoiceTransaction = obj; 
			await GetOthersAsync(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        public override async Task<bool> GetByIdAsync(string TransUuid)
        {
			var obj = await GetInvoiceTransactionByTransUuidAsync(TransUuid); 
			if (obj is null) return false; 
			InvoiceTransaction = obj; 
			await GetOthersAsync(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        protected virtual async Task GetOthersAsync()
        {
            
			if (string.IsNullOrEmpty(InvoiceTransaction.TransUuid)) return; 
			InvoiceReturnItems = await GetInvoiceReturnItemsByTransUuidAsync(InvoiceTransaction.TransUuid); 
        }

        public override async Task<bool> SaveAsync()
        {
			if (InvoiceTransaction is null || string.IsNullOrEmpty(InvoiceTransaction.TransUuid)) return false; 
			CheckIntegrity(); 
			if (_OnBeforeSave != null)
				if (!_OnBeforeSave(this)) return false;
			dbFactory.Begin(); 
			InvoiceTransaction.SetDataBaseFactory(dbFactory); 
			if (!(await InvoiceTransaction.SaveAsync().ConfigureAwait(false))) return false; 
			if (InvoiceReturnItems != null) 
				await InvoiceReturnItems.SetDataBaseFactory(dbFactory).SaveAsync().ConfigureAwait(false); 
			var delInvoiceReturnItems = _InvoiceReturnItemsDeleted;
			if (delInvoiceReturnItems != null)
				await delInvoiceReturnItems.SetDataBaseFactory(dbFactory).DeleteAsync().ConfigureAwait(false);

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
			if (InvoiceTransaction is null || string.IsNullOrEmpty(InvoiceTransaction.TransUuid)) return false; 
			if (_OnBeforeDelete != null)
				if (!_OnBeforeDelete(this)) return false;
			dbFactory.Begin(); 
			InvoiceTransaction.SetDataBaseFactory(dbFactory); 
			if ((await InvoiceTransaction.DeleteAsync().ConfigureAwait(false)) <= 0) return false; 
			if (InvoiceReturnItems != null) 
				await InvoiceReturnItems.SetDataBaseFactory(dbFactory).DeleteAsync().ConfigureAwait(false); 
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


        #region InvoiceTransaction - Generated 
    

        // one to one children
        protected InvoiceTransaction _InvoiceTransaction;

        public virtual InvoiceTransaction InvoiceTransaction 
        { 
            get => _InvoiceTransaction;
            set => _InvoiceTransaction = value?.SetParent(this); 
        }

        public virtual void CopyInvoiceTransactionFrom(InvoiceTransactionData data) => 
            InvoiceTransaction?.CopyFrom(data.InvoiceTransaction, new string[] {"TransUuid"});

        public virtual InvoiceTransaction NewInvoiceTransaction() => new InvoiceTransaction(dbFactory).SetParent(this);

        public virtual InvoiceTransaction GetInvoiceTransaction(long RowNum) =>
            (RowNum <= 0) ? null : dbFactory.Get<InvoiceTransaction>(RowNum);

        public virtual InvoiceTransaction GetInvoiceTransactionByTransUuid(string TransUuid) =>
            (string.IsNullOrEmpty(TransUuid)) ? null : dbFactory.GetById<InvoiceTransaction>(TransUuid);

        public virtual bool SaveInvoiceTransaction(InvoiceTransaction data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteInvoiceTransaction(InvoiceTransaction data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<InvoiceTransaction> GetInvoiceTransactionAsync(long RowNum) =>
            (RowNum <= 0) ? null : await dbFactory.GetAsync<InvoiceTransaction>(RowNum);

        public virtual async Task<InvoiceTransaction> GetInvoiceTransactionByTransUuidAsync(string TransUuid) =>
            (string.IsNullOrEmpty(TransUuid)) ? null : await dbFactory.GetByIdAsync<InvoiceTransaction>(TransUuid);

        public virtual async Task<bool> SaveInvoiceTransactionAsync(InvoiceTransaction data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteInvoiceTransactionAsync(InvoiceTransaction data) =>
            (data is null) ? 0 : await data.DeleteAsync();




        #endregion InvoiceTransaction - Generated 

        #region InvoiceReturnItems - Generated 
        // One to many children
        protected IList<InvoiceReturnItems> _InvoiceReturnItemsDeleted;
        public virtual InvoiceReturnItems AddInvoiceReturnItemsDeleted(InvoiceReturnItems del) 
        {
            if (_InvoiceReturnItemsDeleted is null)
                _InvoiceReturnItemsDeleted = new List<InvoiceReturnItems>();
            var lst = _InvoiceReturnItemsDeleted.ToList();
            lst.Add(del);
            _InvoiceReturnItemsDeleted = lst;
            return del;
        } 

        public virtual IList<InvoiceReturnItems> AddInvoiceReturnItemsDeleted(IList<InvoiceReturnItems> del) 
        {
            if (_InvoiceReturnItemsDeleted is null)
                _InvoiceReturnItemsDeleted = new List<InvoiceReturnItems>();
            var lst = _InvoiceReturnItemsDeleted.ToList();
            lst.AddRange(del);
            _InvoiceReturnItemsDeleted = lst;
            return del;
        } 

        public virtual void SetInvoiceReturnItemsDeleted(IList<InvoiceReturnItems> del) =>
            _InvoiceReturnItemsDeleted = del;

        public virtual void ClearInvoiceReturnItemsDeleted() =>
            _InvoiceReturnItemsDeleted = null;


        protected IList<InvoiceReturnItems> _InvoiceReturnItems;

        public virtual IList<InvoiceReturnItems> InvoiceReturnItems 
        { 
            get 
            {
                if (_InvoiceReturnItems is null)
                    _InvoiceReturnItems = new List<InvoiceReturnItems>();
                return _InvoiceReturnItems;
            } 
            set
            {
                if (value != null)
                {
                    var valueList = value.ToList();
                    valueList.ForEach(i => i?.SetParent(this));
                    _InvoiceReturnItems = valueList;
                }
                else
                    _InvoiceReturnItems = null;
            } 
        }

        public virtual void CopyInvoiceReturnItemsFrom(InvoiceTransactionData data) 
        {
            if  (data is null) return;
            var lstDeleted = InvoiceReturnItems?.CopyFrom(data.InvoiceReturnItems, new string[] {"TransUuid"});
            SetInvoiceReturnItemsDeleted(lstDeleted);
            foreach (var c in InvoiceReturnItems)
                c?.CopyChildrenFrom(data.InvoiceReturnItems?.FindByRowNum(c.RowNum));
        } 

        public virtual InvoiceReturnItems NewInvoiceReturnItems() => new InvoiceReturnItems(dbFactory);

        public virtual InvoiceReturnItems AddInvoiceReturnItems(InvoiceReturnItems obj) => 
            InvoiceReturnItems.AddOrReplace(obj.SetParent(this));

        public virtual InvoiceReturnItems RemoveInvoiceReturnItems(InvoiceReturnItems obj) => 
            AddInvoiceReturnItemsDeleted(InvoiceReturnItems.RemoveObject(obj.SetParent(this)));

        public virtual IList<InvoiceReturnItems> GetInvoiceReturnItemsByTransUuid(string TransUuid) =>
            (string.IsNullOrEmpty(TransUuid)) 
                ? null 
                : dbFactory.Find<InvoiceReturnItems>("WHERE TransUuid = @0 ORDER BY Seq ", TransUuid).ToList();

        public virtual bool SaveInvoiceReturnItems(IList<InvoiceReturnItems> data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteInvoiceReturnItems(IList<InvoiceReturnItems> data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<IList<InvoiceReturnItems>> GetInvoiceReturnItemsByTransUuidAsync(string TransUuid) =>
            (string.IsNullOrEmpty(TransUuid)) 
                ? null
                : (await dbFactory.FindAsync<InvoiceReturnItems>("WHERE TransUuid = @0 ORDER BY Seq ", TransUuid)).ToList();

        public virtual async Task<bool> SaveInvoiceReturnItemsAsync(IList<InvoiceReturnItems> data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteInvoiceReturnItemsAsync(IList<InvoiceReturnItems> data) =>
            (data is null) ? 0 : await data.DeleteAsync();

        public virtual IList<InvoiceReturnItems> CheckIntegrityInvoiceReturnItems()
        {
            if (InvoiceReturnItems is null || InvoiceTransaction is null) 
                return InvoiceReturnItems;
            var seq = 0;
            InvoiceReturnItems.RemoveEmpty();
            var children = InvoiceReturnItems.ToList();
            foreach (var child in children.Where(x => x != null))
            {
                child.SetParent(this);
                if (child.TransUuid != InvoiceTransaction.TransUuid)
                    child.TransUuid = InvoiceTransaction.TransUuid;
                seq += 1;
                child.Seq = seq;
            }
            return children;
        }



        #endregion InvoiceReturnItems - Generated 


    }
}



