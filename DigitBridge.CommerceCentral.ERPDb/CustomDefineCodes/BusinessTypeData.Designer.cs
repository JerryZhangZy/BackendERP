              
    

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
    /// Represents a BusinessTypeData.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    public partial class BusinessTypeData : StructureRepository<BusinessTypeData>
    {
        public BusinessTypeData() : base() {}
        public BusinessTypeData(IDataBaseFactory dbFactory): base(dbFactory) {}

        [JsonIgnore, XmlIgnore]
        public new bool IsNew => BusinessType.IsNew;

        [JsonIgnore, XmlIgnore]
        public new string UniqueId => BusinessType.UniqueId;
        
		 [JsonIgnore, XmlIgnore] 
		public static string BusinessTypeTable ="BusinessType ";
		
        #region CRUD Methods

        public override bool Equals(BusinessTypeData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!string.IsNullOrWhiteSpace(UniqueId) && !string.IsNullOrWhiteSpace(other.UniqueId) && !UniqueId.Equals(other.UniqueId)) return false;
            return ChildrenEquals(other);
        }
        public virtual bool ChildrenEquals(BusinessTypeData other)
        {
			if (BusinessType == null && other.BusinessType != null || BusinessType != null && other.BusinessType == null) 
				return false; 
			if (BusinessType != null && other.BusinessType != null && !BusinessType.Equals(other.BusinessType)) 
				return false; 
            return true;
        }

        // Check Children table Integrity
        public override BusinessTypeData CheckIntegrity()
        {
			if (BusinessType is null) return this; 
			BusinessType.CheckIntegrity(); 
			CheckIntegrityOthers(); 
            return this;
        }

        partial void ClearOthers();
        public override void Clear()
        {
			BusinessType?.Clear(); 
			ClearOthers(); 
			if (_OnClear != null)
				_OnClear(this);
            return;
        }

        public override void New()
        {
            Clear();
			BusinessType = NewBusinessType(); 
            return;
        }

        public virtual void CopyFrom(BusinessTypeData data)
        {
			CopyBusinessTypeFrom(data); 
            CheckIntegrity();
            return;
        }

        public override BusinessTypeData Clone()
        {
			var newData = new BusinessTypeData(); 
			newData.New(); 
			newData?.CopyFrom(this); 
			newData.BusinessType.ClearMetaData(); 
            newData.CheckIntegrity();
            return newData;
        }

        public override bool Get(long RowNum)
        {
			var obj = GetBusinessType(RowNum); 
			if (obj is null) return false; 
			BusinessType = obj; 
			GetOthers(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        public override bool GetById(string BusinessTypeUuid)
        {
			var obj = GetBusinessTypeByBusinessTypeUuid(BusinessTypeUuid); 
			if (obj is null) return false; 
			BusinessType = obj; 
			GetOthers(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        protected virtual void GetOthers()
        {
            
			if (string.IsNullOrEmpty(BusinessType.BusinessTypeUuid)) return; 
        }

        public override bool Save()
        {
			if (BusinessType is null || string.IsNullOrEmpty(BusinessType.BusinessTypeUuid)) return false; 
			CheckIntegrity();
			if (_OnBeforeSave != null)
				if (!_OnBeforeSave(this)) return false;
			dbFactory.Begin();

			 if (NeedSave(BusinessTypeTable))
			{
				BusinessType.SetDataBaseFactory(dbFactory);
				if (!BusinessType.Save()) return false;
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
			if (BusinessType is null || string.IsNullOrEmpty(BusinessType.BusinessTypeUuid)) return false; 
			if (_OnBeforeDelete != null)
				if (!_OnBeforeDelete(this)) return false;
			dbFactory.Begin(); 

			 if (NeedDelete(BusinessTypeTable))
			{
				BusinessType.SetDataBaseFactory(dbFactory); 
				if (BusinessType.Delete() <= 0) return false; 
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
			var obj = await GetBusinessTypeAsync(RowNum); 
			if (obj is null) return false; 
			BusinessType = obj; 
			await GetOthersAsync(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        public override async Task<bool> GetByIdAsync(string BusinessTypeUuid)
        {
			var obj = await GetBusinessTypeByBusinessTypeUuidAsync(BusinessTypeUuid); 
			if (obj is null) return false; 
			BusinessType = obj; 
			await GetOthersAsync(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        protected virtual async Task GetOthersAsync()
        {
            
			if (string.IsNullOrEmpty(BusinessType.BusinessTypeUuid)) return; 
        }

        public override async Task<bool> SaveAsync()
        {
			if (BusinessType is null || string.IsNullOrEmpty(BusinessType.BusinessTypeUuid)) return false; 
			CheckIntegrity(); 
			if (_OnBeforeSave != null)
				if (!_OnBeforeSave(this)) return false;
			dbFactory.Begin(); 

			 if (NeedSave(BusinessTypeTable))
			{
				BusinessType.SetDataBaseFactory(dbFactory); 
				if (!(await BusinessType.SaveAsync())) return false; 
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
			if (BusinessType is null || string.IsNullOrEmpty(BusinessType.BusinessTypeUuid)) return false; 
			if (_OnBeforeDelete != null)
				if (!_OnBeforeDelete(this)) return false;
			dbFactory.Begin(); 
			 if (NeedDelete(BusinessTypeTable))
			{
			BusinessType.SetDataBaseFactory(dbFactory); 
			if ((await BusinessType.DeleteAsync()) <= 0) return false; 
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


        #region BusinessType - Generated 
    

        // one to one children
        protected BusinessType _BusinessType;

        public virtual BusinessType BusinessType 
        { 
            get => _BusinessType;
            set => _BusinessType = value?.SetParent(this); 
        }

        public virtual void CopyBusinessTypeFrom(BusinessTypeData data) => 
            BusinessType?.CopyFrom(data.BusinessType, new string[] {"BusinessTypeUuid"});

        public virtual BusinessType NewBusinessType() => new BusinessType(dbFactory).SetParent(this);

        public virtual BusinessType GetBusinessType(long RowNum) =>
            (RowNum <= 0) ? null : dbFactory.Get<BusinessType>(RowNum);

        public virtual BusinessType GetBusinessTypeByBusinessTypeUuid(string BusinessTypeUuid) =>
            (string.IsNullOrEmpty(BusinessTypeUuid)) ? null : dbFactory.GetById<BusinessType>(BusinessTypeUuid);

        public virtual bool SaveBusinessType(BusinessType data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteBusinessType(BusinessType data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<BusinessType> GetBusinessTypeAsync(long RowNum) =>
            (RowNum <= 0) ? null : await dbFactory.GetAsync<BusinessType>(RowNum);

        public virtual async Task<BusinessType> GetBusinessTypeByBusinessTypeUuidAsync(string BusinessTypeUuid) =>
            (string.IsNullOrEmpty(BusinessTypeUuid)) ? null : await dbFactory.GetByIdAsync<BusinessType>(BusinessTypeUuid);

        public virtual async Task<bool> SaveBusinessTypeAsync(BusinessType data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteBusinessTypeAsync(BusinessType data) =>
            (data is null) ? 0 : await data.DeleteAsync();




        #endregion BusinessType - Generated 


    }
}


