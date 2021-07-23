

              
    

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
    /// Represents a InventoryLogData.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    public partial class InventoryLogData : StructureRepository<InventoryLogData>
    {
        public InventoryLogData() : base() {}
        public InventoryLogData(IDataBaseFactory dbFactory): base(dbFactory) {}

        [JsonIgnore, XmlIgnore]
        public new bool IsNew => InventoryLog.IsNew;

        [JsonIgnore, XmlIgnore]
        public new string UniqueId => InventoryLog.UniqueId;

        #region CRUD Methods

        public override bool Equals(InventoryLogData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!string.IsNullOrWhiteSpace(UniqueId) && !string.IsNullOrWhiteSpace(other.UniqueId) && !UniqueId.Equals(other.UniqueId)) return false;
            return ChildrenEquals(other);
        }
        public virtual bool ChildrenEquals(InventoryLogData other)
        {
			if (InventoryLog == null && other.InventoryLog != null || InventoryLog != null && other.InventoryLog == null) 
				return false; 
			if (InventoryLog != null && other.InventoryLog != null && !InventoryLog.Equals(other.InventoryLog)) 
				return false; 
            return true;
        }

        // Check Children table Integrity
        public virtual InventoryLogData CheckIntegrity()
        {
			if (InventoryLog is null) return this; 
			InventoryLog.CheckUniqueId(); 
            return this;
        }

        partial void ClearOthers();
        public override void Clear()
        {
			InventoryLog?.Clear(); 
			ClearOthers(); 
			if (_OnClear != null)
				_OnClear(this);
            return;
        }

        public override void New()
        {
            Clear();
			InventoryLog = NewInventoryLog(); 
            return;
        }

        public virtual void CopyFrom(InventoryLogData data)
        {
			CopyInventoryLogFrom(data); 
            CheckIntegrity();
            return;
        }

        public override InventoryLogData Clone()
        {
			var newData = new InventoryLogData(); 
			newData.New(); 
			newData?.CopyFrom(this); 
			newData.InventoryLog.ClearMetaData(); 
            newData.CheckIntegrity();
            return newData;
        }

        public override bool Get(long RowNum)
        {
			var obj = GetInventoryLog(RowNum); 
			if (obj is null) return false; 
			InventoryLog = obj; 
			GetOthers(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        public override bool GetById(string InventoryLogUuid)
        {
			var obj = GetInventoryLogByInventoryLogUuid(InventoryLogUuid); 
			if (obj is null) return false; 
			InventoryLog = obj; 
			GetOthers(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        protected virtual void GetOthers()
        {
            
			if (string.IsNullOrEmpty(InventoryLog.InventoryLogUuid)) return; 
        }

        public override bool Save()
        {
			if (InventoryLog is null || string.IsNullOrEmpty(InventoryLog.InventoryLogUuid)) return false; 
			CheckIntegrity();
			if (_OnBeforeSave != null)
				if (!_OnBeforeSave(this)) return false;
			dbFactory.Begin();
			InventoryLog.SetDataBaseFactory(dbFactory);
			if (!InventoryLog.Save()) return false;

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
			if (InventoryLog is null || string.IsNullOrEmpty(InventoryLog.InventoryLogUuid)) return false; 
			if (_OnBeforeDelete != null)
				if (!_OnBeforeDelete(this)) return false;
			dbFactory.Begin(); 
			InventoryLog.SetDataBaseFactory(dbFactory); 
			if (InventoryLog.Delete() <= 0) return false; 
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
			var obj = await GetInventoryLogAsync(RowNum); 
			if (obj is null) return false; 
			InventoryLog = obj; 
			await GetOthersAsync(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        public override async Task<bool> GetByIdAsync(string InventoryLogUuid)
        {
			var obj = await GetInventoryLogByInventoryLogUuidAsync(InventoryLogUuid); 
			if (obj is null) return false; 
			InventoryLog = obj; 
			await GetOthersAsync(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        protected virtual async Task GetOthersAsync()
        {
            
			if (string.IsNullOrEmpty(InventoryLog.InventoryLogUuid)) return; 
        }

        public override async Task<bool> SaveAsync()
        {
			if (InventoryLog is null || string.IsNullOrEmpty(InventoryLog.InventoryLogUuid)) return false; 
			CheckIntegrity(); 
			if (_OnBeforeSave != null)
				if (!_OnBeforeSave(this)) return false;
			dbFactory.Begin(); 
			InventoryLog.SetDataBaseFactory(dbFactory); 
			if (!(await InventoryLog.SaveAsync().ConfigureAwait(false))) return false; 
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
			if (InventoryLog is null || string.IsNullOrEmpty(InventoryLog.InventoryLogUuid)) return false; 
			if (_OnBeforeDelete != null)
				if (!_OnBeforeDelete(this)) return false;
			dbFactory.Begin(); 
			InventoryLog.SetDataBaseFactory(dbFactory); 
			if ((await InventoryLog.DeleteAsync().ConfigureAwait(false)) <= 0) return false; 
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


        #region InventoryLog - Generated 
    

        // one to one children
        protected InventoryLog _InventoryLog;

        public virtual InventoryLog InventoryLog 
        { 
            get => _InventoryLog;
            set => _InventoryLog = value?.SetParent(this); 
        }

        public virtual void CopyInventoryLogFrom(InventoryLogData data) => 
            InventoryLog?.CopyFrom(data.InventoryLog, new string[] {"InventoryLogUuid"});

        public virtual InventoryLog NewInventoryLog() => new InventoryLog(dbFactory).SetParent(this);

        public virtual InventoryLog GetInventoryLog(long RowNum) =>
            (RowNum <= 0) ? null : dbFactory.Get<InventoryLog>(RowNum);

        public virtual InventoryLog GetInventoryLogByInventoryLogUuid(string InventoryLogUuid) =>
            (string.IsNullOrEmpty(InventoryLogUuid)) ? null : dbFactory.GetById<InventoryLog>(InventoryLogUuid);

        public virtual bool SaveInventoryLog(InventoryLog data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteInventoryLog(InventoryLog data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<InventoryLog> GetInventoryLogAsync(long RowNum) =>
            (RowNum <= 0) ? null : await dbFactory.GetAsync<InventoryLog>(RowNum);

        public virtual async Task<InventoryLog> GetInventoryLogByInventoryLogUuidAsync(string InventoryLogUuid) =>
            (string.IsNullOrEmpty(InventoryLogUuid)) ? null : await dbFactory.GetByIdAsync<InventoryLog>(InventoryLogUuid);

        public virtual async Task<bool> SaveInventoryLogAsync(InventoryLog data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteInventoryLogAsync(InventoryLog data) =>
            (data is null) ? 0 : await data.DeleteAsync();




        #endregion InventoryLog - Generated 


    }
}



