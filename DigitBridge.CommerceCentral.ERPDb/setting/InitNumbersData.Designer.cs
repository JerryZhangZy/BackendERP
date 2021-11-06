              
    

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
    /// Represents a InitNumbersData.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    public partial class InitNumbersData : StructureRepository<InitNumbersData>
    {
        public InitNumbersData() : base() {}
        public InitNumbersData(IDataBaseFactory dbFactory): base(dbFactory) {}

        [JsonIgnore, XmlIgnore]
        public new bool IsNew => InitNumbers.IsNew;

        [JsonIgnore, XmlIgnore]
        public new string UniqueId => InitNumbers.UniqueId;
        
		 [JsonIgnore, XmlIgnore] 
		public static string InitNumbersTable ="InitNumbers ";
		
        #region CRUD Methods

        public override bool Equals(InitNumbersData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!string.IsNullOrWhiteSpace(UniqueId) && !string.IsNullOrWhiteSpace(other.UniqueId) && !UniqueId.Equals(other.UniqueId)) return false;
            return ChildrenEquals(other);
        }
        public virtual bool ChildrenEquals(InitNumbersData other)
        {
			if (InitNumbers == null && other.InitNumbers != null || InitNumbers != null && other.InitNumbers == null) 
				return false; 
			if (InitNumbers != null && other.InitNumbers != null && !InitNumbers.Equals(other.InitNumbers)) 
				return false; 
            return true;
        }

        // Check Children table Integrity
        public override InitNumbersData CheckIntegrity()
        {
			if (InitNumbers is null) return this; 
			InitNumbers.CheckIntegrity(); 
			CheckIntegrityOthers(); 
            return this;
        }

        partial void ClearOthers();
        public override void Clear()
        {
			InitNumbers?.Clear(); 
			ClearOthers(); 
			if (_OnClear != null)
				_OnClear(this);
            return;
        }

        public override void New()
        {
            Clear();
			InitNumbers = NewInitNumbers(); 
            return;
        }

        public virtual void CopyFrom(InitNumbersData data)
        {
			CopyInitNumbersFrom(data); 
            CheckIntegrity();
            return;
        }

        public override InitNumbersData Clone()
        {
			var newData = new InitNumbersData(); 
			newData.New(); 
			newData?.CopyFrom(this); 
			newData.InitNumbers.ClearMetaData(); 
            newData.CheckIntegrity();
            return newData;
        }

        public override bool Get(long RowNum)
        {
			var obj = GetInitNumbers(RowNum); 
			if (obj is null) return false; 
			InitNumbers = obj; 
			GetOthers(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        public override bool GetById(string InitNumbersUuid)
        {
			var obj = GetInitNumbersByInitNumbersUuid(InitNumbersUuid); 
			if (obj is null) return false; 
			InitNumbers = obj; 
			GetOthers(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        protected virtual void GetOthers()
        {
            
			if (string.IsNullOrEmpty(InitNumbers.InitNumbersUuid)) return; 
        }

        public override bool Save()
        {
			if (InitNumbers is null || string.IsNullOrEmpty(InitNumbers.InitNumbersUuid)) return false; 
			CheckIntegrity();
			if (_OnBeforeSave != null)
				if (!_OnBeforeSave(this)) return false;
			dbFactory.Begin();

			 if (NeedSave(InitNumbersTable))
			{
				InitNumbers.SetDataBaseFactory(dbFactory);
				if (!InitNumbers.Save()) return false;
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
			if (InitNumbers is null || string.IsNullOrEmpty(InitNumbers.InitNumbersUuid)) return false; 
			if (_OnBeforeDelete != null)
				if (!_OnBeforeDelete(this)) return false;
			dbFactory.Begin(); 

			 if (NeedDelete(InitNumbersTable))
			{
				InitNumbers.SetDataBaseFactory(dbFactory); 
				if (InitNumbers.Delete() <= 0) return false; 
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
			var obj = await GetInitNumbersAsync(RowNum); 
			if (obj is null) return false; 
			InitNumbers = obj; 
			await GetOthersAsync(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        public override async Task<bool> GetByIdAsync(string InitNumbersUuid)
        {
			var obj = await GetInitNumbersByInitNumbersUuidAsync(InitNumbersUuid); 
			if (obj is null) return false; 
			InitNumbers = obj; 
			await GetOthersAsync(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        protected virtual async Task GetOthersAsync()
        {
            
			if (string.IsNullOrEmpty(InitNumbers.InitNumbersUuid)) return; 
        }

        public override async Task<bool> SaveAsync()
        {
			if (InitNumbers is null || string.IsNullOrEmpty(InitNumbers.InitNumbersUuid)) return false; 
			CheckIntegrity(); 
			if (_OnBeforeSave != null)
				if (!_OnBeforeSave(this)) return false;
			dbFactory.Begin(); 

			 if (NeedSave(InitNumbersTable))
			{
				InitNumbers.SetDataBaseFactory(dbFactory); 
				if (!(await InitNumbers.SaveAsync())) return false; 
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
			if (InitNumbers is null || string.IsNullOrEmpty(InitNumbers.InitNumbersUuid)) return false; 
			if (_OnBeforeDelete != null)
				if (!_OnBeforeDelete(this)) return false;
			dbFactory.Begin(); 
			 if (NeedDelete(InitNumbersTable))
			{
			InitNumbers.SetDataBaseFactory(dbFactory); 
			if ((await InitNumbers.DeleteAsync()) <= 0) return false; 
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


        #region InitNumbers - Generated 
    

        // one to one children
        protected InitNumbers _InitNumbers;

        public virtual InitNumbers InitNumbers 
        { 
            get => _InitNumbers;
            set => _InitNumbers = value?.SetParent(this); 
        }

        public virtual void CopyInitNumbersFrom(InitNumbersData data) => 
            InitNumbers?.CopyFrom(data.InitNumbers, new string[] {"InitNumbersUuid"});

        public virtual InitNumbers NewInitNumbers() => new InitNumbers(dbFactory).SetParent(this);

        public virtual InitNumbers GetInitNumbers(long RowNum) =>
            (RowNum <= 0) ? null : dbFactory.Get<InitNumbers>(RowNum);

        public virtual InitNumbers GetInitNumbersByInitNumbersUuid(string InitNumbersUuid) =>
            (string.IsNullOrEmpty(InitNumbersUuid)) ? null : dbFactory.GetById<InitNumbers>(InitNumbersUuid);

        public virtual bool SaveInitNumbers(InitNumbers data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteInitNumbers(InitNumbers data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<InitNumbers> GetInitNumbersAsync(long RowNum) =>
            (RowNum <= 0) ? null : await dbFactory.GetAsync<InitNumbers>(RowNum);

        public virtual async Task<InitNumbers> GetInitNumbersByInitNumbersUuidAsync(string InitNumbersUuid) =>
            (string.IsNullOrEmpty(InitNumbersUuid)) ? null : await dbFactory.GetByIdAsync<InitNumbers>(InitNumbersUuid);

        public virtual async Task<bool> SaveInitNumbersAsync(InitNumbers data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteInitNumbersAsync(InitNumbers data) =>
            (data is null) ? 0 : await data.DeleteAsync();




        #endregion InitNumbers - Generated 


    }
}


