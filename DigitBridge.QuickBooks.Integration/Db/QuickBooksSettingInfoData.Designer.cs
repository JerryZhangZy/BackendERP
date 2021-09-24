              
    

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
        public new bool IsNew => QuickBooksChnlAccSetting.IsNew;

        [JsonIgnore, XmlIgnore]
        public new string UniqueId => QuickBooksChnlAccSetting.UniqueId;
        
		 [JsonIgnore, XmlIgnore] 
		public static string QuickBooksChnlAccSettingTable ="QuickBooksChnlAccSetting ";
		
		 [JsonIgnore, XmlIgnore] 
		public static string QuickBooksIntegrationSettingTable ="QuickBooksIntegrationSetting ";
		
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
			if (QuickBooksChnlAccSetting == null && other.QuickBooksChnlAccSetting != null || QuickBooksChnlAccSetting != null && other.QuickBooksChnlAccSetting == null) 
				return false; 
			if (QuickBooksChnlAccSetting != null && other.QuickBooksChnlAccSetting != null && !QuickBooksChnlAccSetting.Equals(other.QuickBooksChnlAccSetting)) 
				return false; 
			if (QuickBooksIntegrationSetting == null && other.QuickBooksIntegrationSetting != null || QuickBooksIntegrationSetting != null && other.QuickBooksIntegrationSetting == null) 
				return false; 
			if (QuickBooksIntegrationSetting != null && other.QuickBooksIntegrationSetting != null && !QuickBooksIntegrationSetting.Equals(other.QuickBooksIntegrationSetting)) 
				return false; 
            return true;
        }

        // Check Children table Integrity
        public override QuickBooksSettingInfoData CheckIntegrity()
        {
			if (QuickBooksChnlAccSetting is null) return this; 
			QuickBooksChnlAccSetting.CheckIntegrity(); 
			CheckIntegrityQuickBooksIntegrationSetting(); 
			CheckIntegrityOthers(); 
            return this;
        }

        partial void ClearOthers();
        public override void Clear()
        {
			QuickBooksChnlAccSetting?.Clear(); 
			QuickBooksIntegrationSetting?.Clear(); 
			ClearOthers(); 
			if (_OnClear != null)
				_OnClear(this);
            return;
        }

        public override void New()
        {
            Clear();
			QuickBooksChnlAccSetting = NewQuickBooksChnlAccSetting(); 
			QuickBooksIntegrationSetting = NewQuickBooksIntegrationSetting(); 
            return;
        }

        public virtual void CopyFrom(QuickBooksSettingInfoData data)
        {
			CopyQuickBooksChnlAccSettingFrom(data); 
			CopyQuickBooksIntegrationSettingFrom(data); 
            CheckIntegrity();
            return;
        }

        public override QuickBooksSettingInfoData Clone()
        {
			var newData = new QuickBooksSettingInfoData(); 
			newData.New(); 
			newData?.CopyFrom(this); 
			newData.QuickBooksChnlAccSetting.ClearMetaData(); 
			newData.QuickBooksIntegrationSetting.ClearMetaData(); 
            newData.CheckIntegrity();
            return newData;
        }

        public override bool Get(long RowNum)
        {
			var obj = GetQuickBooksChnlAccSetting(RowNum); 
			if (obj is null) return false; 
			QuickBooksChnlAccSetting = obj; 
			GetOthers(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        public override bool GetById(string SettingUuid)
        {
			var obj = GetQuickBooksChnlAccSettingBySettingUuid(SettingUuid); 
			if (obj is null) return false; 
			QuickBooksChnlAccSetting = obj; 
			GetOthers(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        protected virtual void GetOthers()
        {
            
			if (string.IsNullOrEmpty(QuickBooksChnlAccSetting.SettingUuid)) return; 
			QuickBooksIntegrationSetting = GetQuickBooksIntegrationSettingBySettingUuid(QuickBooksChnlAccSetting.SettingUuid); 
        }

        public override bool Save()
        {
			if (QuickBooksChnlAccSetting is null || string.IsNullOrEmpty(QuickBooksChnlAccSetting.SettingUuid)) return false; 
			CheckIntegrity();
			if (_OnBeforeSave != null)
				if (!_OnBeforeSave(this)) return false;
			dbFactory.Begin();

			 if (NeedSave(QuickBooksChnlAccSettingTable))
			{
				QuickBooksChnlAccSetting.SetDataBaseFactory(dbFactory);
				if (!QuickBooksChnlAccSetting.Save()) return false;
			}

			 if (NeedSave(QuickBooksIntegrationSettingTable))
			{
				if (QuickBooksIntegrationSetting != null) 
					QuickBooksIntegrationSetting.SetDataBaseFactory(dbFactory)?.Save();
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
			if (QuickBooksChnlAccSetting is null || string.IsNullOrEmpty(QuickBooksChnlAccSetting.SettingUuid)) return false; 
			if (_OnBeforeDelete != null)
				if (!_OnBeforeDelete(this)) return false;
			dbFactory.Begin(); 

			 if (NeedDelete(QuickBooksChnlAccSettingTable))
			{
				QuickBooksChnlAccSetting.SetDataBaseFactory(dbFactory); 
				if (QuickBooksChnlAccSetting.Delete() <= 0) return false; 
			}
			 if (NeedDelete(QuickBooksIntegrationSettingTable))
			{
				if (QuickBooksIntegrationSetting != null) 
					QuickBooksIntegrationSetting?.SetDataBaseFactory(dbFactory)?.Delete(); 
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
			var obj = await GetQuickBooksChnlAccSettingAsync(RowNum); 
			if (obj is null) return false; 
			QuickBooksChnlAccSetting = obj; 
			await GetOthersAsync(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        public override async Task<bool> GetByIdAsync(string SettingUuid)
        {
			var obj = await GetQuickBooksChnlAccSettingBySettingUuidAsync(SettingUuid); 
			if (obj is null) return false; 
			QuickBooksChnlAccSetting = obj; 
			await GetOthersAsync(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        protected virtual async Task GetOthersAsync()
        {
            
			if (string.IsNullOrEmpty(QuickBooksChnlAccSetting.SettingUuid)) return; 
			QuickBooksIntegrationSetting = await GetQuickBooksIntegrationSettingBySettingUuidAsync(QuickBooksChnlAccSetting.SettingUuid); 
        }

        public override async Task<bool> SaveAsync()
        {
			if (QuickBooksChnlAccSetting is null || string.IsNullOrEmpty(QuickBooksChnlAccSetting.SettingUuid)) return false; 
			CheckIntegrity(); 
			if (_OnBeforeSave != null)
				if (!_OnBeforeSave(this)) return false;
			dbFactory.Begin(); 

			 if (NeedSave(QuickBooksChnlAccSettingTable))
			{
				QuickBooksChnlAccSetting.SetDataBaseFactory(dbFactory); 
				if (!(await QuickBooksChnlAccSetting.SaveAsync())) return false; 
			}
			 if (NeedSave(QuickBooksIntegrationSettingTable))
			{
				if (QuickBooksIntegrationSetting != null) 
					await QuickBooksIntegrationSetting.SetDataBaseFactory(dbFactory).SaveAsync(); 
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
			if (QuickBooksChnlAccSetting is null || string.IsNullOrEmpty(QuickBooksChnlAccSetting.SettingUuid)) return false; 
			if (_OnBeforeDelete != null)
				if (!_OnBeforeDelete(this)) return false;
			dbFactory.Begin(); 
			 if (NeedDelete(QuickBooksChnlAccSettingTable))
			{
			QuickBooksChnlAccSetting.SetDataBaseFactory(dbFactory); 
			if ((await QuickBooksChnlAccSetting.DeleteAsync()) <= 0) return false; 
			}
			 if (NeedDelete(QuickBooksIntegrationSettingTable))
			{
				if (QuickBooksIntegrationSetting != null) 
					await QuickBooksIntegrationSetting.SetDataBaseFactory(dbFactory).DeleteAsync(); 
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


        #region QuickBooksChnlAccSetting - Generated 
    

        // one to one children
        protected QuickBooksChnlAccSetting _QuickBooksChnlAccSetting;

        public virtual QuickBooksChnlAccSetting QuickBooksChnlAccSetting 
        { 
            get => _QuickBooksChnlAccSetting;
            set => _QuickBooksChnlAccSetting = value?.SetParent(this); 
        }

        public virtual void CopyQuickBooksChnlAccSettingFrom(QuickBooksSettingInfoData data) => 
            QuickBooksChnlAccSetting?.CopyFrom(data.QuickBooksChnlAccSetting, new string[] {"SettingUuid"});

        public virtual QuickBooksChnlAccSetting NewQuickBooksChnlAccSetting() => new QuickBooksChnlAccSetting(dbFactory).SetParent(this);

        public virtual QuickBooksChnlAccSetting GetQuickBooksChnlAccSetting(long RowNum) =>
            (RowNum <= 0) ? null : dbFactory.Get<QuickBooksChnlAccSetting>(RowNum);

        public virtual QuickBooksChnlAccSetting GetQuickBooksChnlAccSettingBySettingUuid(string SettingUuid) =>
            (string.IsNullOrEmpty(SettingUuid)) ? null : dbFactory.GetById<QuickBooksChnlAccSetting>(SettingUuid);

        public virtual bool SaveQuickBooksChnlAccSetting(QuickBooksChnlAccSetting data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteQuickBooksChnlAccSetting(QuickBooksChnlAccSetting data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<QuickBooksChnlAccSetting> GetQuickBooksChnlAccSettingAsync(long RowNum) =>
            (RowNum <= 0) ? null : await dbFactory.GetAsync<QuickBooksChnlAccSetting>(RowNum);

        public virtual async Task<QuickBooksChnlAccSetting> GetQuickBooksChnlAccSettingBySettingUuidAsync(string SettingUuid) =>
            (string.IsNullOrEmpty(SettingUuid)) ? null : await dbFactory.GetByIdAsync<QuickBooksChnlAccSetting>(SettingUuid);

        public virtual async Task<bool> SaveQuickBooksChnlAccSettingAsync(QuickBooksChnlAccSetting data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteQuickBooksChnlAccSettingAsync(QuickBooksChnlAccSetting data) =>
            (data is null) ? 0 : await data.DeleteAsync();




        #endregion QuickBooksChnlAccSetting - Generated 

        #region QuickBooksIntegrationSetting - Generated 
    

        // one to one children
        protected QuickBooksIntegrationSetting _QuickBooksIntegrationSetting;

        public virtual QuickBooksIntegrationSetting QuickBooksIntegrationSetting 
        { 
            get => _QuickBooksIntegrationSetting;
            set => _QuickBooksIntegrationSetting = value?.SetParent(this); 
        }

        public virtual void CopyQuickBooksIntegrationSettingFrom(QuickBooksSettingInfoData data) => 
            QuickBooksIntegrationSetting?.CopyFrom(data.QuickBooksIntegrationSetting, new string[] {"SettingUuid"});

        public virtual QuickBooksIntegrationSetting NewQuickBooksIntegrationSetting() => new QuickBooksIntegrationSetting(dbFactory).SetParent(this);

        public virtual QuickBooksIntegrationSetting GetQuickBooksIntegrationSetting(long RowNum) =>
            (RowNum <= 0) ? null : dbFactory.Get<QuickBooksIntegrationSetting>(RowNum);

        public virtual QuickBooksIntegrationSetting GetQuickBooksIntegrationSettingBySettingUuid(string SettingUuid) =>
            (string.IsNullOrEmpty(SettingUuid)) ? null : dbFactory.GetById<QuickBooksIntegrationSetting>(SettingUuid);

        public virtual bool SaveQuickBooksIntegrationSetting(QuickBooksIntegrationSetting data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteQuickBooksIntegrationSetting(QuickBooksIntegrationSetting data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<QuickBooksIntegrationSetting> GetQuickBooksIntegrationSettingAsync(long RowNum) =>
            (RowNum <= 0) ? null : await dbFactory.GetAsync<QuickBooksIntegrationSetting>(RowNum);

        public virtual async Task<QuickBooksIntegrationSetting> GetQuickBooksIntegrationSettingBySettingUuidAsync(string SettingUuid) =>
            (string.IsNullOrEmpty(SettingUuid)) ? null : await dbFactory.GetByIdAsync<QuickBooksIntegrationSetting>(SettingUuid);

        public virtual async Task<bool> SaveQuickBooksIntegrationSettingAsync(QuickBooksIntegrationSetting data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteQuickBooksIntegrationSettingAsync(QuickBooksIntegrationSetting data) =>
            (data is null) ? 0 : await data.DeleteAsync();

        public virtual QuickBooksIntegrationSetting CheckIntegrityQuickBooksIntegrationSetting()
        {
            if (QuickBooksIntegrationSetting is null || QuickBooksChnlAccSetting is null) 
                return QuickBooksIntegrationSetting;
            QuickBooksIntegrationSetting.SetParent(this);
            if (QuickBooksIntegrationSetting.SettingUuid != QuickBooksChnlAccSetting.SettingUuid)
                QuickBooksIntegrationSetting.SettingUuid = QuickBooksChnlAccSetting.SettingUuid;
            QuickBooksIntegrationSetting.CheckIntegrity();
            return QuickBooksIntegrationSetting;
        }



        #endregion QuickBooksIntegrationSetting - Generated 


    }
}



