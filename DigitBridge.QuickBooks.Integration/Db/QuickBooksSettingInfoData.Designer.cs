              
    

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

namespace DigitBridge.QuickBooks.Integration
{
    /// <summary>
    /// Represents a QuickBooksSettingInfoData.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    public partial class QuickBooksSettingInfoData : StructureRepository<QuickBooksSettingInfoData>
    {
        public QuickBooksSettingInfoData() : base() {}
        public QuickBooksSettingInfoData(IDataBaseFactory dbFactory): base(dbFactory) {}

        [JsonIgnore, XmlIgnore]
        public new bool IsNew => QuickBooksSettingInfo.IsNew;

        [JsonIgnore, XmlIgnore]
        public new string UniqueId => QuickBooksSettingInfo.UniqueId;
        
		 [JsonIgnore, XmlIgnore] 
		public static string QuickBooksSettingInfoTable ="QuickBooksSettingInfo ";
		
        #region CRUD Methods

        public override bool Equals(QuickBooksSettingInfoData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!string.IsNullOrWhiteSpace(UniqueId) && !string.IsNullOrWhiteSpace(other.UniqueId) && !UniqueId.Equals(other.UniqueId)) return false;
            return ChildrenEquals(other);
        }
        public virtual bool ChildrenEquals(QuickBooksSettingInfoData other)
        {
			if (QuickBooksSettingInfo == null && other.QuickBooksSettingInfo != null || QuickBooksSettingInfo != null && other.QuickBooksSettingInfo == null) 
				return false; 
			if (QuickBooksSettingInfo != null && other.QuickBooksSettingInfo != null && !QuickBooksSettingInfo.Equals(other.QuickBooksSettingInfo)) 
				return false; 
            return true;
        }

        // Check Children table Integrity
        public override QuickBooksSettingInfoData CheckIntegrity()
        {
			if (QuickBooksSettingInfo is null) return this; 
			QuickBooksSettingInfo.CheckIntegrity(); 
			CheckIntegrityOthers(); 
            return this;
        }

        partial void ClearOthers();
        public override void Clear()
        {
			QuickBooksSettingInfo?.Clear(); 
			ClearOthers(); 
			if (_OnClear != null)
				_OnClear(this);
            return;
        }

        public override void New()
        {
            Clear();
			QuickBooksSettingInfo = NewQuickBooksSettingInfo(); 
            return;
        }

        public virtual void CopyFrom(QuickBooksSettingInfoData data)
        {
			CopyQuickBooksSettingInfoFrom(data); 
            CheckIntegrity();
            return;
        }

        public override QuickBooksSettingInfoData Clone()
        {
			var newData = new QuickBooksSettingInfoData(); 
			newData.New(); 
			newData?.CopyFrom(this); 
			newData.QuickBooksSettingInfo.ClearMetaData(); 
            newData.CheckIntegrity();
            return newData;
        }

        public override bool Get(long RowNum)
        {
			var obj = GetQuickBooksSettingInfo(RowNum); 
			if (obj is null) return false; 
			QuickBooksSettingInfo = obj; 
			GetOthers(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        public override bool GetById(string SettingUuid)
        {
			var obj = GetQuickBooksSettingInfoBySettingUuid(SettingUuid); 
			if (obj is null) return false; 
			QuickBooksSettingInfo = obj; 
			GetOthers(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        protected virtual void GetOthers()
        {
            
			if (string.IsNullOrEmpty(QuickBooksSettingInfo.SettingUuid)) return; 
        }

        public override bool Save()
        {
			if (QuickBooksSettingInfo is null || string.IsNullOrEmpty(QuickBooksSettingInfo.SettingUuid)) return false; 
			CheckIntegrity();
			if (_OnBeforeSave != null)
				if (!_OnBeforeSave(this)) return false;
			dbFactory.Begin();

			 if (NeedSave(QuickBooksSettingInfoTable))
			{
				QuickBooksSettingInfo.SetDataBaseFactory(dbFactory);
				if (!QuickBooksSettingInfo.Save()) return false;
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
			if (QuickBooksSettingInfo is null || string.IsNullOrEmpty(QuickBooksSettingInfo.SettingUuid)) return false; 
			if (_OnBeforeDelete != null)
				if (!_OnBeforeDelete(this)) return false;
			dbFactory.Begin(); 

			 if (NeedDelete(QuickBooksSettingInfoTable))
			{
				QuickBooksSettingInfo.SetDataBaseFactory(dbFactory); 
				if (QuickBooksSettingInfo.Delete() <= 0) return false; 
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
			var obj = await GetQuickBooksSettingInfoAsync(RowNum); 
			if (obj is null) return false; 
			QuickBooksSettingInfo = obj; 
			await GetOthersAsync(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        public override async Task<bool> GetByIdAsync(string SettingUuid)
        {
			var obj = await GetQuickBooksSettingInfoBySettingUuidAsync(SettingUuid); 
			if (obj is null) return false; 
			QuickBooksSettingInfo = obj; 
			await GetOthersAsync(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        protected virtual async Task GetOthersAsync()
        {
            
			if (string.IsNullOrEmpty(QuickBooksSettingInfo.SettingUuid)) return; 
        }

        public override async Task<bool> SaveAsync()
        {
			if (QuickBooksSettingInfo is null || string.IsNullOrEmpty(QuickBooksSettingInfo.SettingUuid)) return false; 
			CheckIntegrity(); 
			if (_OnBeforeSave != null)
				if (!_OnBeforeSave(this)) return false;
			dbFactory.Begin(); 

			 if (NeedSave(QuickBooksSettingInfoTable))
			{
				QuickBooksSettingInfo.SetDataBaseFactory(dbFactory); 
				if (!(await QuickBooksSettingInfo.SaveAsync())) return false; 
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
			if (QuickBooksSettingInfo is null || string.IsNullOrEmpty(QuickBooksSettingInfo.SettingUuid)) return false; 
			if (_OnBeforeDelete != null)
				if (!_OnBeforeDelete(this)) return false;
			dbFactory.Begin(); 
			 if (NeedDelete(QuickBooksSettingInfoTable))
			{
			QuickBooksSettingInfo.SetDataBaseFactory(dbFactory); 
			if ((await QuickBooksSettingInfo.DeleteAsync()) <= 0) return false; 
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


        #region QuickBooksSettingInfo - Generated 
    

        // one to one children
        protected QuickBooksSettingInfo _QuickBooksSettingInfo;

        public virtual QuickBooksSettingInfo QuickBooksSettingInfo 
        { 
            get => _QuickBooksSettingInfo;
            set => _QuickBooksSettingInfo = value?.SetParent(this); 
        }

        public virtual void CopyQuickBooksSettingInfoFrom(QuickBooksSettingInfoData data) => 
            QuickBooksSettingInfo?.CopyFrom(data.QuickBooksSettingInfo, new string[] {"SettingUuid"});

        public virtual QuickBooksSettingInfo NewQuickBooksSettingInfo() => new QuickBooksSettingInfo(dbFactory).SetParent(this);

        public virtual QuickBooksSettingInfo GetQuickBooksSettingInfo(long RowNum) =>
            (RowNum <= 0) ? null : dbFactory.Get<QuickBooksSettingInfo>(RowNum);

        public virtual QuickBooksSettingInfo GetQuickBooksSettingInfoBySettingUuid(string SettingUuid) =>
            (string.IsNullOrEmpty(SettingUuid)) ? null : dbFactory.GetById<QuickBooksSettingInfo>(SettingUuid);

        public virtual bool SaveQuickBooksSettingInfo(QuickBooksSettingInfo data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteQuickBooksSettingInfo(QuickBooksSettingInfo data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<QuickBooksSettingInfo> GetQuickBooksSettingInfoAsync(long RowNum) =>
            (RowNum <= 0) ? null : await dbFactory.GetAsync<QuickBooksSettingInfo>(RowNum);

        public virtual async Task<QuickBooksSettingInfo> GetQuickBooksSettingInfoBySettingUuidAsync(string SettingUuid) =>
            (string.IsNullOrEmpty(SettingUuid)) ? null : await dbFactory.GetByIdAsync<QuickBooksSettingInfo>(SettingUuid);

        public virtual async Task<bool> SaveQuickBooksSettingInfoAsync(QuickBooksSettingInfo data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteQuickBooksSettingInfoAsync(QuickBooksSettingInfo data) =>
            (data is null) ? 0 : await data.DeleteAsync();




        #endregion QuickBooksSettingInfo - Generated 


    }
}



