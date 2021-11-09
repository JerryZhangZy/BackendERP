              
    

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
    /// Represents a VendorAddressData.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    public partial class VendorAddressData : StructureRepository<VendorAddressData>
    {
        public VendorAddressData() : base() {}
        public VendorAddressData(IDataBaseFactory dbFactory): base(dbFactory) {}

        [JsonIgnore, XmlIgnore]
        public new bool IsNew => VendorAddress.IsNew;

        [JsonIgnore, XmlIgnore]
        public new string UniqueId => VendorAddress.UniqueId;
        
			 [JsonIgnore, XmlIgnore] 
			public static string VendorAddressTable ="VendorAddress ";
			
        #region CRUD Methods

        public override bool Equals(VendorAddressData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!string.IsNullOrWhiteSpace(UniqueId) && !string.IsNullOrWhiteSpace(other.UniqueId) && !UniqueId.Equals(other.UniqueId)) return false;
            return ChildrenEquals(other);
        }
        public virtual bool ChildrenEquals(VendorAddressData other)
        {
			if (VendorAddress == null && other.VendorAddress != null || VendorAddress != null && other.VendorAddress == null) 
				return false; 
			if (VendorAddress != null && other.VendorAddress != null && !VendorAddress.Equals(other.VendorAddress)) 
				return false; 
            return true;
        }

        // Check Children table Integrity
        public override VendorAddressData CheckIntegrity()
        {
			if (VendorAddress is null) return this; 
			VendorAddress.CheckIntegrity(); 
			CheckIntegrityOthers(); 
            return this;
        }

        partial void ClearOthers();
        public override void Clear()
        {
			VendorAddress?.Clear(); 
			ClearOthers(); 
			if (_OnClear != null)
				_OnClear(this);
            return;
        }

        public override void New()
        {
            Clear();
			VendorAddress = NewVendorAddress(); 
            return;
        }

        public virtual void CopyFrom(VendorAddressData data)
        {
			CopyVendorAddressFrom(data); 
            CheckIntegrity();
            return;
        }

        public override VendorAddressData Clone()
        {
			var newData = new VendorAddressData(); 
			newData.New(); 
			newData?.CopyFrom(this); 
			newData.VendorAddress.ClearMetaData(); 
            newData.CheckIntegrity();
            return newData;
        }

        public override bool Get(long RowNum)
        {
			var obj = GetVendorAddress(RowNum); 
			if (obj is null) return false; 
			VendorAddress = obj; 
			GetOthers(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        public override bool GetById(string AddressUuid)
        {
			var obj = GetVendorAddressByAddressUuid(AddressUuid); 
			if (obj is null) return false; 
			VendorAddress = obj; 
			GetOthers(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        protected virtual void GetOthers()
        {
            
			if (string.IsNullOrEmpty(VendorAddress.AddressUuid)) return; 
        }

        public override bool Save()
        {
			if (VendorAddress is null || string.IsNullOrEmpty(VendorAddress.AddressUuid)) return false; 
			CheckIntegrity();
			if (_OnBeforeSave != null)
				if (!_OnBeforeSave(this)) return false;
			dbFactory.Begin();

			 if (NeedSave(VendorAddressTable))
			{
				VendorAddress.SetDataBaseFactory(dbFactory);
				if (!VendorAddress.Save()) return false;
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
			if (VendorAddress is null || string.IsNullOrEmpty(VendorAddress.AddressUuid)) return false; 
			if (_OnBeforeDelete != null)
				if (!_OnBeforeDelete(this)) return false;
			dbFactory.Begin(); 

			 if (NeedDelete(VendorAddressTable))
			{
				VendorAddress.SetDataBaseFactory(dbFactory); 
				if (VendorAddress.Delete() <= 0) return false; 
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
			var obj = await GetVendorAddressAsync(RowNum); 
			if (obj is null) return false; 
			VendorAddress = obj; 
			await GetOthersAsync(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        public override async Task<bool> GetByIdAsync(string AddressUuid)
        {
			var obj = await GetVendorAddressByAddressUuidAsync(AddressUuid); 
			if (obj is null) return false; 
			VendorAddress = obj; 
			await GetOthersAsync(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        protected virtual async Task GetOthersAsync()
        {
            
			if (string.IsNullOrEmpty(VendorAddress.AddressUuid)) return; 
        }

        public override async Task<bool> SaveAsync()
        {
			if (VendorAddress is null || string.IsNullOrEmpty(VendorAddress.AddressUuid)) return false; 
			CheckIntegrity(); 
			if (_OnBeforeSave != null)
				if (!_OnBeforeSave(this)) return false;
			dbFactory.Begin(); 

			 if (NeedSave(VendorAddressTable))
			{
				VendorAddress.SetDataBaseFactory(dbFactory); 
				if (!(await VendorAddress.SaveAsync())) return false; 
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
			if (VendorAddress is null || string.IsNullOrEmpty(VendorAddress.AddressUuid)) return false; 
			if (_OnBeforeDelete != null)
				if (!_OnBeforeDelete(this)) return false;
			dbFactory.Begin(); 
			 if (NeedDelete(VendorAddressTable))
			{
			VendorAddress.SetDataBaseFactory(dbFactory); 
			if ((await VendorAddress.DeleteAsync()) <= 0) return false; 
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


        #region VendorAddress - Generated 
    

        // one to one children
        protected VendorAddress _VendorAddress;

        public virtual VendorAddress VendorAddress 
        { 
            get => _VendorAddress;
            set => _VendorAddress = value; 
        }

        public virtual void CopyVendorAddressFrom(VendorAddressData data) => 
            VendorAddress?.CopyFrom(data.VendorAddress, new string[] {"AddressUuid"});

        public virtual VendorAddress NewVendorAddress() => new VendorAddress(dbFactory);

        public virtual VendorAddress GetVendorAddress(long RowNum) =>
            (RowNum <= 0) ? null : dbFactory.Get<VendorAddress>(RowNum);

        public virtual VendorAddress GetVendorAddressByAddressUuid(string AddressUuid) =>
            (string.IsNullOrEmpty(AddressUuid)) ? null : dbFactory.GetById<VendorAddress>(AddressUuid);

        public virtual bool SaveVendorAddress(VendorAddress data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteVendorAddress(VendorAddress data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<VendorAddress> GetVendorAddressAsync(long RowNum) =>
            (RowNum <= 0) ? null : await dbFactory.GetAsync<VendorAddress>(RowNum);

        public virtual async Task<VendorAddress> GetVendorAddressByAddressUuidAsync(string AddressUuid) =>
            (string.IsNullOrEmpty(AddressUuid)) ? null : await dbFactory.GetByIdAsync<VendorAddress>(AddressUuid);

        public virtual async Task<bool> SaveVendorAddressAsync(VendorAddress data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteVendorAddressAsync(VendorAddress data) =>
            (data is null) ? 0 : await data.DeleteAsync();




        #endregion VendorAddress - Generated 


    }
}



