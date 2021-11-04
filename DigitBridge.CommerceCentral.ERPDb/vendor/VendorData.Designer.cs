              
    

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
    /// Represents a VendorData.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    public partial class VendorData : StructureRepository<VendorData>
    {
        public VendorData() : base() {}
        public VendorData(IDataBaseFactory dbFactory): base(dbFactory) {}

        [JsonIgnore, XmlIgnore]
        public new bool IsNew => Vendor.IsNew;

        [JsonIgnore, XmlIgnore]
        public new string UniqueId => Vendor.UniqueId;
        
		 [JsonIgnore, XmlIgnore] 
		public static string VendorTable ="Vendor ";
		
		 [JsonIgnore, XmlIgnore] 
		public static string VendorAddressTable ="VendorAddress ";
		
		 [JsonIgnore, XmlIgnore] 
		public static string VendorAttributesTable ="VendorAttributes ";
		
        #region CRUD Methods

        public override bool Equals(VendorData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!string.IsNullOrWhiteSpace(UniqueId) && !string.IsNullOrWhiteSpace(other.UniqueId) && !UniqueId.Equals(other.UniqueId)) return false;
            return ChildrenEquals(other);
        }
        public virtual bool ChildrenEquals(VendorData other)
        {
			if (Vendor == null && other.Vendor != null || Vendor != null && other.Vendor == null) 
				return false; 
			if (Vendor != null && other.Vendor != null && !Vendor.Equals(other.Vendor)) 
				return false; 
			if (VendorAddress == null && other.VendorAddress != null || VendorAddress != null && other.VendorAddress == null) 
				return false; 
			if (VendorAddress != null && other.VendorAddress != null && !VendorAddress.EqualsList(other.VendorAddress)) 
				return false; 
			if (VendorAttributes == null && other.VendorAttributes != null || VendorAttributes != null && other.VendorAttributes == null) 
				return false; 
			if (VendorAttributes != null && other.VendorAttributes != null && !VendorAttributes.Equals(other.VendorAttributes)) 
				return false; 
            return true;
        }

        // Check Children table Integrity
        public override VendorData CheckIntegrity()
        {
			if (Vendor is null) return this; 
			Vendor.CheckIntegrity(); 
			CheckIntegrityVendorAddress(); 
			CheckIntegrityVendorAttributes(); 
			CheckIntegrityOthers(); 
            return this;
        }

        partial void ClearOthers();
        public override void Clear()
        {
			Vendor?.Clear(); 
			VendorAddress = new List<VendorAddress>(); 
			ClearVendorAddressDeleted(); 
			VendorAttributes?.Clear(); 
			ClearOthers(); 
			if (_OnClear != null)
				_OnClear(this);
            return;
        }

        public override void New()
        {
            Clear();
			Vendor = NewVendor(); 
			VendorAddress = new List<VendorAddress>(); 
			AddVendorAddress(NewVendorAddress()); 
			ClearVendorAddressDeleted(); 
			VendorAttributes = NewVendorAttributes(); 
            return;
        }

        public virtual void CopyFrom(VendorData data)
        {
			CopyVendorFrom(data); 
			CopyVendorAddressFrom(data); 
			CopyVendorAttributesFrom(data); 
            CheckIntegrity();
            return;
        }

        public override VendorData Clone()
        {
			var newData = new VendorData(); 
			newData.New(); 
			newData?.CopyFrom(this); 
			newData.Vendor.ClearMetaData(); 
			newData.VendorAddress.ClearMetaData(); 
			newData.VendorAttributes.ClearMetaData(); 
            newData.CheckIntegrity();
            return newData;
        }

        public override bool Get(long RowNum)
        {
			var obj = GetVendor(RowNum); 
			if (obj is null) return false; 
			Vendor = obj; 
			GetOthers(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        public override bool GetById(string VendorUuid)
        {
			var obj = GetVendorByVendorUuid(VendorUuid); 
			if (obj is null) return false; 
			Vendor = obj; 
			GetOthers(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        protected virtual void GetOthers()
        {
            
			if (string.IsNullOrEmpty(Vendor.VendorUuid)) return; 
			VendorAddress = GetVendorAddressByVendorUuid(Vendor.VendorUuid); 
			VendorAttributes = GetVendorAttributesByVendorUuid(Vendor.VendorUuid); 
        }

        public override bool Save()
        {
			if (Vendor is null || string.IsNullOrEmpty(Vendor.VendorUuid)) return false; 
			CheckIntegrity();
			if (_OnBeforeSave != null)
				if (!_OnBeforeSave(this)) return false;
			dbFactory.Begin();

			 if (NeedSave(VendorTable))
			{
				Vendor.SetDataBaseFactory(dbFactory);
				if (!Vendor.Save()) return false;
			}

			 if (NeedSave(VendorAddressTable))
			{
				if (VendorAddress != null) 
					VendorAddress.SetDataBaseFactory(dbFactory)?.Save();
				var delVendorAddress = _VendorAddressDeleted;
				if (delVendorAddress != null)
					delVendorAddress.SetDataBaseFactory(dbFactory)?.Delete();
			}

			 if (NeedSave(VendorAttributesTable))
			{
				if (VendorAttributes != null) 
					VendorAttributes.SetDataBaseFactory(dbFactory)?.Save();
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
			if (Vendor is null || string.IsNullOrEmpty(Vendor.VendorUuid)) return false; 
			if (_OnBeforeDelete != null)
				if (!_OnBeforeDelete(this)) return false;
			dbFactory.Begin(); 

			 if (NeedDelete(VendorTable))
			{
				Vendor.SetDataBaseFactory(dbFactory); 
				if (Vendor.Delete() <= 0) return false; 
			}
			 if (NeedDelete(VendorAddressTable))
			{
				if (VendorAddress != null) 
					VendorAddress?.SetDataBaseFactory(dbFactory)?.Delete(); 
			}
			 if (NeedDelete(VendorAttributesTable))
			{
				if (VendorAttributes != null) 
					VendorAttributes?.SetDataBaseFactory(dbFactory)?.Delete(); 
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
			var obj = await GetVendorAsync(RowNum); 
			if (obj is null) return false; 
			Vendor = obj; 
			await GetOthersAsync(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        public override async Task<bool> GetByIdAsync(string VendorUuid)
        {
			var obj = await GetVendorByVendorUuidAsync(VendorUuid); 
			if (obj is null) return false; 
			Vendor = obj; 
			await GetOthersAsync(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        protected virtual async Task GetOthersAsync()
        {
            
			if (string.IsNullOrEmpty(Vendor.VendorUuid)) return; 
			VendorAddress = await GetVendorAddressByVendorUuidAsync(Vendor.VendorUuid); 
			VendorAttributes = await GetVendorAttributesByVendorUuidAsync(Vendor.VendorUuid); 
        }

        public override async Task<bool> SaveAsync()
        {
			if (Vendor is null || string.IsNullOrEmpty(Vendor.VendorUuid)) return false; 
			CheckIntegrity(); 
			if (_OnBeforeSave != null)
				if (!_OnBeforeSave(this)) return false;
			dbFactory.Begin(); 

			 if (NeedSave(VendorTable))
			{
				Vendor.SetDataBaseFactory(dbFactory); 
				if (!(await Vendor.SaveAsync())) return false; 
			}
			 if (NeedSave(VendorAddressTable))
			{
				if (VendorAddress != null) 
					await VendorAddress.SetDataBaseFactory(dbFactory).SaveAsync(); 
				var delVendorAddress = _VendorAddressDeleted;
				if (delVendorAddress != null)
					await delVendorAddress.SetDataBaseFactory(dbFactory).DeleteAsync();
			}

			 if (NeedSave(VendorAttributesTable))
			{
				if (VendorAttributes != null) 
					await VendorAttributes.SetDataBaseFactory(dbFactory).SaveAsync(); 
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
			if (Vendor is null || string.IsNullOrEmpty(Vendor.VendorUuid)) return false; 
			if (_OnBeforeDelete != null)
				if (!_OnBeforeDelete(this)) return false;
			dbFactory.Begin(); 
			 if (NeedDelete(VendorTable))
			{
			Vendor.SetDataBaseFactory(dbFactory); 
			if ((await Vendor.DeleteAsync()) <= 0) return false; 
			}
			 if (NeedDelete(VendorAddressTable))
			{
				if (VendorAddress != null) 
					await VendorAddress.SetDataBaseFactory(dbFactory).DeleteAsync(); 
			}
			 if (NeedDelete(VendorAttributesTable))
			{
				if (VendorAttributes != null) 
					await VendorAttributes.SetDataBaseFactory(dbFactory).DeleteAsync(); 
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


        #region Vendor - Generated 
    

        // one to one children
        protected Vendor _Vendor;

        public virtual Vendor Vendor 
        { 
            get => _Vendor;
            set => _Vendor = value?.SetParent(this); 
        }

        public virtual void CopyVendorFrom(VendorData data) => 
            Vendor?.CopyFrom(data.Vendor, new string[] {"VendorUuid"});

        public virtual Vendor NewVendor() => new Vendor(dbFactory).SetParent(this);

        public virtual Vendor GetVendor(long RowNum) =>
            (RowNum <= 0) ? null : dbFactory.Get<Vendor>(RowNum);

        public virtual Vendor GetVendorByVendorUuid(string VendorUuid) =>
            (string.IsNullOrEmpty(VendorUuid)) ? null : dbFactory.GetById<Vendor>(VendorUuid);

        public virtual bool SaveVendor(Vendor data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteVendor(Vendor data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<Vendor> GetVendorAsync(long RowNum) =>
            (RowNum <= 0) ? null : await dbFactory.GetAsync<Vendor>(RowNum);

        public virtual async Task<Vendor> GetVendorByVendorUuidAsync(string VendorUuid) =>
            (string.IsNullOrEmpty(VendorUuid)) ? null : await dbFactory.GetByIdAsync<Vendor>(VendorUuid);

        public virtual async Task<bool> SaveVendorAsync(Vendor data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteVendorAsync(Vendor data) =>
            (data is null) ? 0 : await data.DeleteAsync();




        #endregion Vendor - Generated 

        #region VendorAddress - Generated 
        // One to many children
        protected IList<VendorAddress> _VendorAddressDeleted;
        public virtual VendorAddress AddVendorAddressDeleted(VendorAddress del) 
        {
            if (_VendorAddressDeleted is null)
                _VendorAddressDeleted = new List<VendorAddress>();
            var lst = _VendorAddressDeleted.ToList();
            lst.Add(del);
            _VendorAddressDeleted = lst;
            return del;
        } 

        public virtual IList<VendorAddress> AddVendorAddressDeleted(IList<VendorAddress> del) 
        {
            if (_VendorAddressDeleted is null)
                _VendorAddressDeleted = new List<VendorAddress>();
            var lst = _VendorAddressDeleted.ToList();
            lst.AddRange(del);
            _VendorAddressDeleted = lst;
            return del;
        } 

        public virtual void SetVendorAddressDeleted(IList<VendorAddress> del) =>
            _VendorAddressDeleted = del;

        public virtual void ClearVendorAddressDeleted() =>
            _VendorAddressDeleted = null;


        protected IList<VendorAddress> _VendorAddress;

        public virtual IList<VendorAddress> VendorAddress 
        { 
            get 
            {
                if (_VendorAddress is null)
                    _VendorAddress = new List<VendorAddress>();
                return _VendorAddress;
            } 
            set
            {
                if (value != null)
                {
                    var valueList = value.ToList();
                    valueList.ForEach(i => i?.SetParent(this));
                    _VendorAddress = valueList;
                }
                else
                    _VendorAddress = null;
            } 
        }

        public virtual void CopyVendorAddressFrom(VendorData data) 
        {
            if  (data is null) return;
            var lstDeleted = VendorAddress?.CopyFrom(data.VendorAddress, new string[] {"VendorUuid"});
            SetVendorAddressDeleted(lstDeleted);
            foreach (var c in VendorAddress)
                c?.CopyChildrenFrom(data.VendorAddress?.FindByRowNum(c.RowNum));
        } 

        public virtual VendorAddress NewVendorAddress() => new VendorAddress(dbFactory);

        public virtual VendorAddress AddVendorAddress(VendorAddress obj) => 
            VendorAddress.AddOrReplace(obj.SetParent(this));

        public virtual VendorAddress RemoveVendorAddress(VendorAddress obj) => 
            AddVendorAddressDeleted(VendorAddress.RemoveObject(obj.SetParent(this)));

        public virtual IList<VendorAddress> GetVendorAddressByVendorUuid(string VendorUuid) =>
            (string.IsNullOrEmpty(VendorUuid)) 
                ? null 
                : dbFactory.Find<VendorAddress>("WHERE VendorUuid = @0 ORDER BY RowNum ", VendorUuid).ToList();

        public virtual bool SaveVendorAddress(IList<VendorAddress> data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteVendorAddress(IList<VendorAddress> data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<IList<VendorAddress>> GetVendorAddressByVendorUuidAsync(string VendorUuid) =>
            (string.IsNullOrEmpty(VendorUuid)) 
                ? null
                : (await dbFactory.FindAsync<VendorAddress>("WHERE VendorUuid = @0 ORDER BY RowNum ", VendorUuid)).ToList();

        public virtual async Task<bool> SaveVendorAddressAsync(IList<VendorAddress> data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteVendorAddressAsync(IList<VendorAddress> data) =>
            (data is null) ? 0 : await data.DeleteAsync();

        public virtual IList<VendorAddress> CheckIntegrityVendorAddress()
        {
            if (VendorAddress is null || Vendor is null) 
                return VendorAddress;
            var seq = 0;
            VendorAddress.RemoveEmpty();
            var children = VendorAddress.ToList();
            foreach (var child in children.Where(x => x != null))
            {
                child.SetParent(this);
                if (child.VendorUuid != Vendor.VendorUuid)
                    child.VendorUuid = Vendor.VendorUuid;
                child.CheckIntegrity();
            }
            return children;
        }



        #endregion VendorAddress - Generated 

        #region VendorAttributes - Generated 
    

        // one to one children
        protected VendorAttributes _VendorAttributes;

        public virtual VendorAttributes VendorAttributes 
        { 
            get => _VendorAttributes;
            set => _VendorAttributes = value?.SetParent(this); 
        }

        public virtual void CopyVendorAttributesFrom(VendorData data) => 
            VendorAttributes?.CopyFrom(data.VendorAttributes, new string[] {"VendorUuid"});

        public virtual VendorAttributes NewVendorAttributes() => new VendorAttributes(dbFactory).SetParent(this);

        public virtual VendorAttributes GetVendorAttributes(long RowNum) =>
            (RowNum <= 0) ? null : dbFactory.Get<VendorAttributes>(RowNum);

        public virtual VendorAttributes GetVendorAttributesByVendorUuid(string VendorUuid) =>
            (string.IsNullOrEmpty(VendorUuid)) ? null : dbFactory.GetById<VendorAttributes>(VendorUuid);

        public virtual bool SaveVendorAttributes(VendorAttributes data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteVendorAttributes(VendorAttributes data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<VendorAttributes> GetVendorAttributesAsync(long RowNum) =>
            (RowNum <= 0) ? null : await dbFactory.GetAsync<VendorAttributes>(RowNum);

        public virtual async Task<VendorAttributes> GetVendorAttributesByVendorUuidAsync(string VendorUuid) =>
            (string.IsNullOrEmpty(VendorUuid)) ? null : await dbFactory.GetByIdAsync<VendorAttributes>(VendorUuid);

        public virtual async Task<bool> SaveVendorAttributesAsync(VendorAttributes data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteVendorAttributesAsync(VendorAttributes data) =>
            (data is null) ? 0 : await data.DeleteAsync();

        public virtual VendorAttributes CheckIntegrityVendorAttributes()
        {
            if (VendorAttributes is null || Vendor is null) 
                return VendorAttributes;
            VendorAttributes.SetParent(this);
            if (VendorAttributes.VendorUuid != Vendor.VendorUuid)
                VendorAttributes.VendorUuid = Vendor.VendorUuid;
            VendorAttributes.CheckIntegrity();
            return VendorAttributes;
        }



        #endregion VendorAttributes - Generated 


    }
}



