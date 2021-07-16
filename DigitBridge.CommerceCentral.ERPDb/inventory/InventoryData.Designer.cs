

              
    

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
    /// Represents a InventoryData.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public partial class InventoryData : StructureRepository<InventoryData>
    {
        public InventoryData() : base() {}
        public InventoryData(IDataBaseFactory dbFactory): base(dbFactory) {}

        [XmlIgnore, JsonIgnore]
        public new bool IsNew => ProductBasic.IsNew;

        [XmlIgnore, JsonIgnore]
        public new string UniqueId => ProductBasic.UniqueId;

        #region CRUD Methods

        public override bool Equals(InventoryData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (!string.IsNullOrWhiteSpace(UniqueId) && !string.IsNullOrWhiteSpace(other.UniqueId) && !UniqueId.Equals(other.UniqueId)) return false;
            return ChildrenEquals(other);
        }
        public virtual bool ChildrenEquals(InventoryData other)
        {
			if (ProductBasic == null && other.ProductBasic != null || ProductBasic != null && other.ProductBasic == null) 
				return false; 
			if (ProductBasic != null && other.ProductBasic != null && !ProductBasic.Equals(other.ProductBasic)) 
				return false; 
			if (ProductExt == null && other.ProductExt != null || ProductExt != null && other.ProductExt == null) 
				return false; 
			if (ProductExt != null && other.ProductExt != null && !ProductExt.Equals(other.ProductExt)) 
				return false; 
			if (ProductExtAttributes == null && other.ProductExtAttributes != null || ProductExtAttributes != null && other.ProductExtAttributes == null) 
				return false; 
			if (ProductExtAttributes != null && other.ProductExtAttributes != null && !ProductExtAttributes.Equals(other.ProductExtAttributes)) 
				return false; 
			if (Inventory == null && other.Inventory != null || Inventory != null && other.Inventory == null) 
				return false; 
			if (Inventory != null && other.Inventory != null && !Inventory.EqualsList(other.Inventory)) 
				return false; 
			if (InventoryAttributes == null && other.InventoryAttributes != null || InventoryAttributes != null && other.InventoryAttributes == null) 
				return false; 
			if (InventoryAttributes != null && other.InventoryAttributes != null && !InventoryAttributes.EqualsList(other.InventoryAttributes)) 
				return false; 
            return true;
        }

        // Check Children table Integrity
        public virtual InventoryData CheckIntegrity()
        {
			if (ProductBasic is null) return this; 
			ProductBasic.CheckUniqueId(); 
			CheckIntegrityProductExt(); 
			CheckIntegrityProductExtAttributes(); 
			CheckIntegrityInventory(); 
			CheckIntegrityInventoryAttributes(); 
            return this;
        }

        partial void ClearOthers();
        public override void Clear()
        {
			ProductBasic?.Clear(); 
			ProductExt?.Clear(); 
			ProductExtAttributes?.Clear(); 
			Inventory = new List<Inventory>(); 
			ClearInventoryDeleted(); 
			InventoryAttributes = new List<InventoryAttributes>(); 
			ClearOthers(); 
			if (_OnClear != null)
				_OnClear(this);
            return;
        }

        public override void New()
        {
            Clear();
			ProductBasic = NewProductBasic(); 
			ProductExt = NewProductExt(); 
			ProductExtAttributes = NewProductExtAttributes(); 
			Inventory = new List<Inventory>(); 
			AddInventory(NewInventory()); 
			Inventory.ToList().ForEach(x => x?.NewChildren()); 
			ClearInventoryDeleted(); 
            return;
        }

        public virtual void CopyFrom(InventoryData data)
        {
			CopyProductBasicFrom(data); 
			CopyProductExtFrom(data); 
			CopyProductExtAttributesFrom(data); 
			CopyInventoryFrom(data); 
            CheckIntegrity();
            return;
        }

        public override InventoryData Clone()
        {
			var newData = new InventoryData(); 
			newData.New(); 
			newData?.CopyFrom(this); 
			newData.ProductBasic.ClearMetaData(); 
			newData.ProductExt.ClearMetaData(); 
			newData.ProductExtAttributes.ClearMetaData(); 
			newData.Inventory.ClearMetaData(); 
			newData.InventoryAttributes.ClearMetaData(); 
            newData.CheckIntegrity();
            return newData;
        }

        public override bool Get(long RowNum)
        {
			var obj = GetProductBasic(RowNum); 
			if (obj is null) return false; 
			ProductBasic = obj; 
			GetOthers(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        public override bool GetById(string ProductUuid)
        {
			var obj = GetProductBasicByProductUuid(ProductUuid); 
			if (obj is null) return false; 
			ProductBasic = obj; 
			GetOthers(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        protected virtual void GetOthers()
        {
            
			if (string.IsNullOrEmpty(ProductBasic.ProductUuid)) return; 
			ProductExt = GetProductExtByProductUuid(ProductBasic.ProductUuid); 
			ProductExtAttributes = GetProductExtAttributesByProductUuid(ProductBasic.ProductUuid); 
			Inventory = GetInventoryByProductUuid(ProductBasic.ProductUuid); 
			InventoryAttributes = GetInventoryAttributesByProductUuid(ProductBasic.ProductUuid); 
        }

        public override bool Save()
        {
			if (ProductBasic is null || string.IsNullOrEmpty(ProductBasic.ProductUuid)) return false; 
			CheckIntegrity();
			if (_OnBeforeSave != null)
				if (!_OnBeforeSave(this)) return false;
			dbFactory.Begin();
			ProductBasic.SetDataBaseFactory(dbFactory);
			if (!ProductBasic.Save()) return false;

			if (ProductExt != null) 
				ProductExt.SetDataBaseFactory(dbFactory)?.Save();

			if (ProductExtAttributes != null) 
				ProductExtAttributes.SetDataBaseFactory(dbFactory)?.Save();

			if (Inventory != null) 
				Inventory.SetDataBaseFactory(dbFactory)?.Save();
			var delInventory = _InventoryDeleted;
			if (delInventory != null)
				delInventory.SetDataBaseFactory(dbFactory)?.Delete();

			if (InventoryAttributes != null) 
				InventoryAttributes.SetDataBaseFactory(dbFactory)?.Save();
			var delChildrenInventoryAttributes = InventoryAttributesDeleted;
			if (delChildrenInventoryAttributes != null)
				delChildrenInventoryAttributes.SetDataBaseFactory(dbFactory)?.Delete();

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
			if (ProductBasic is null || string.IsNullOrEmpty(ProductBasic.ProductUuid)) return false; 
			if (_OnBeforeDelete != null)
				if (!_OnBeforeDelete(this)) return false;
			dbFactory.Begin(); 
			ProductBasic.SetDataBaseFactory(dbFactory); 
			if (ProductBasic.Delete() <= 0) return false; 
			if (ProductExt != null) 
				ProductExt?.SetDataBaseFactory(dbFactory)?.Delete(); 
			if (ProductExtAttributes != null) 
				ProductExtAttributes?.SetDataBaseFactory(dbFactory)?.Delete(); 
			if (Inventory != null) 
				Inventory?.SetDataBaseFactory(dbFactory)?.Delete(); 
			if (InventoryAttributes != null) 
				InventoryAttributes?.SetDataBaseFactory(dbFactory)?.Delete(); 
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
			var obj = await GetProductBasicAsync(RowNum); 
			if (obj is null) return false; 
			ProductBasic = obj; 
			await GetOthersAsync(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        public override async Task<bool> GetByIdAsync(string ProductUuid)
        {
			var obj = await GetProductBasicByProductUuidAsync(ProductUuid); 
			if (obj is null) return false; 
			ProductBasic = obj; 
			await GetOthersAsync(); 
			if (_OnAfterLoad != null)
				_OnAfterLoad(this);
            return true;
        }

        protected virtual async Task GetOthersAsync()
        {
            
			if (string.IsNullOrEmpty(ProductBasic.ProductUuid)) return; 
			ProductExt = await GetProductExtByProductUuidAsync(ProductBasic.ProductUuid); 
			ProductExtAttributes = await GetProductExtAttributesByProductUuidAsync(ProductBasic.ProductUuid); 
			Inventory = await GetInventoryByProductUuidAsync(ProductBasic.ProductUuid); 
			InventoryAttributes = await GetInventoryAttributesByProductUuidAsync(ProductBasic.ProductUuid); 
        }

        public override async Task<bool> SaveAsync()
        {
			if (ProductBasic is null || string.IsNullOrEmpty(ProductBasic.ProductUuid)) return false; 
			CheckIntegrity(); 
			if (_OnBeforeSave != null)
				if (!_OnBeforeSave(this)) return false;
			dbFactory.Begin(); 
			ProductBasic.SetDataBaseFactory(dbFactory); 
			if (!(await ProductBasic.SaveAsync().ConfigureAwait(false))) return false; 
			if (ProductExt != null) 
				await ProductExt.SetDataBaseFactory(dbFactory).SaveAsync().ConfigureAwait(false); 

			if (ProductExtAttributes != null) 
				await ProductExtAttributes.SetDataBaseFactory(dbFactory).SaveAsync().ConfigureAwait(false); 

			if (Inventory != null) 
				await Inventory.SetDataBaseFactory(dbFactory).SaveAsync().ConfigureAwait(false); 
			var delInventory = _InventoryDeleted;
			if (delInventory != null)
				await delInventory.SetDataBaseFactory(dbFactory).DeleteAsync().ConfigureAwait(false);

			if (InventoryAttributes != null) 
				await InventoryAttributes.SetDataBaseFactory(dbFactory).SaveAsync().ConfigureAwait(false); 
			var delInventoryAttributes = InventoryAttributesDeleted;
			if (delInventoryAttributes != null)
				await delInventoryAttributes.SetDataBaseFactory(dbFactory).DeleteAsync().ConfigureAwait(false);

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
			if (ProductBasic is null || string.IsNullOrEmpty(ProductBasic.ProductUuid)) return false; 
			if (_OnBeforeDelete != null)
				if (!_OnBeforeDelete(this)) return false;
			dbFactory.Begin(); 
			ProductBasic.SetDataBaseFactory(dbFactory); 
			if ((await ProductBasic.DeleteAsync().ConfigureAwait(false)) <= 0) return false; 
			if (ProductExt != null) 
				await ProductExt.SetDataBaseFactory(dbFactory).DeleteAsync().ConfigureAwait(false); 
			if (ProductExtAttributes != null) 
				await ProductExtAttributes.SetDataBaseFactory(dbFactory).DeleteAsync().ConfigureAwait(false); 
			if (Inventory != null) 
				await Inventory.SetDataBaseFactory(dbFactory).DeleteAsync().ConfigureAwait(false); 
			if (InventoryAttributes != null) 
				await InventoryAttributes.SetDataBaseFactory(dbFactory).DeleteAsync().ConfigureAwait(false); 
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


        #region ProductBasic - Generated 
    

        // one to one children
        protected ProductBasic _ProductBasic;

        public virtual ProductBasic ProductBasic 
        { 
            get => _ProductBasic;
            set => _ProductBasic = value?.SetParent(this); 
        }

        public virtual void CopyProductBasicFrom(InventoryData data) => 
            ProductBasic?.CopyFrom(data.ProductBasic, new string[] {"ProductUuid"});

        public virtual ProductBasic NewProductBasic() => new ProductBasic(dbFactory).SetParent(this);

        public virtual ProductBasic GetProductBasic(long RowNum) =>
            (RowNum <= 0) ? null : dbFactory.Get<ProductBasic>(RowNum);

        public virtual ProductBasic GetProductBasicByProductUuid(string ProductUuid) =>
            (string.IsNullOrEmpty(ProductUuid)) ? null : dbFactory.GetById<ProductBasic>(ProductUuid);

        public virtual bool SaveProductBasic(ProductBasic data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteProductBasic(ProductBasic data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<ProductBasic> GetProductBasicAsync(long RowNum) =>
            (RowNum <= 0) ? null : await dbFactory.GetAsync<ProductBasic>(RowNum);

        public virtual async Task<ProductBasic> GetProductBasicByProductUuidAsync(string ProductUuid) =>
            (string.IsNullOrEmpty(ProductUuid)) ? null : await dbFactory.GetByIdAsync<ProductBasic>(ProductUuid);

        public virtual async Task<bool> SaveProductBasicAsync(ProductBasic data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteProductBasicAsync(ProductBasic data) =>
            (data is null) ? 0 : await data.DeleteAsync();




        #endregion ProductBasic - Generated 

        #region ProductExt - Generated 
    

        // one to one children
        protected ProductExt _ProductExt;

        public virtual ProductExt ProductExt 
        { 
            get => _ProductExt;
            set => _ProductExt = value?.SetParent(this); 
        }

        public virtual void CopyProductExtFrom(InventoryData data) => 
            ProductExt?.CopyFrom(data.ProductExt, new string[] {"ProductUuid"});

        public virtual ProductExt NewProductExt() => new ProductExt(dbFactory).SetParent(this);

        public virtual ProductExt GetProductExt(long RowNum) =>
            (RowNum <= 0) ? null : dbFactory.Get<ProductExt>(RowNum);

        public virtual ProductExt GetProductExtByProductUuid(string ProductUuid) =>
            (string.IsNullOrEmpty(ProductUuid)) ? null : dbFactory.GetById<ProductExt>(ProductUuid);

        public virtual bool SaveProductExt(ProductExt data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteProductExt(ProductExt data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<ProductExt> GetProductExtAsync(long RowNum) =>
            (RowNum <= 0) ? null : await dbFactory.GetAsync<ProductExt>(RowNum);

        public virtual async Task<ProductExt> GetProductExtByProductUuidAsync(string ProductUuid) =>
            (string.IsNullOrEmpty(ProductUuid)) ? null : await dbFactory.GetByIdAsync<ProductExt>(ProductUuid);

        public virtual async Task<bool> SaveProductExtAsync(ProductExt data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteProductExtAsync(ProductExt data) =>
            (data is null) ? 0 : await data.DeleteAsync();

        public virtual ProductExt CheckIntegrityProductExt()
        {
            if (ProductExt is null || ProductBasic is null) 
                return ProductExt;
            ProductExt.SetParent(this);
            if (ProductExt.ProductUuid != ProductBasic.ProductUuid)
                ProductExt.ProductUuid = ProductBasic.ProductUuid;
            return ProductExt;
        }



        #endregion ProductExt - Generated 

        #region ProductExtAttributes - Generated 
    

        // one to one children
        protected ProductExtAttributes _ProductExtAttributes;

        public virtual ProductExtAttributes ProductExtAttributes 
        { 
            get => _ProductExtAttributes;
            set => _ProductExtAttributes = value?.SetParent(this); 
        }

        public virtual void CopyProductExtAttributesFrom(InventoryData data) => 
            ProductExtAttributes?.CopyFrom(data.ProductExtAttributes, new string[] {"ProductUuid"});

        public virtual ProductExtAttributes NewProductExtAttributes() => new ProductExtAttributes(dbFactory).SetParent(this);

        public virtual ProductExtAttributes GetProductExtAttributes(long RowNum) =>
            (RowNum <= 0) ? null : dbFactory.Get<ProductExtAttributes>(RowNum);

        public virtual ProductExtAttributes GetProductExtAttributesByProductUuid(string ProductUuid) =>
            (string.IsNullOrEmpty(ProductUuid)) ? null : dbFactory.GetById<ProductExtAttributes>(ProductUuid);

        public virtual bool SaveProductExtAttributes(ProductExtAttributes data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteProductExtAttributes(ProductExtAttributes data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<ProductExtAttributes> GetProductExtAttributesAsync(long RowNum) =>
            (RowNum <= 0) ? null : await dbFactory.GetAsync<ProductExtAttributes>(RowNum);

        public virtual async Task<ProductExtAttributes> GetProductExtAttributesByProductUuidAsync(string ProductUuid) =>
            (string.IsNullOrEmpty(ProductUuid)) ? null : await dbFactory.GetByIdAsync<ProductExtAttributes>(ProductUuid);

        public virtual async Task<bool> SaveProductExtAttributesAsync(ProductExtAttributes data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteProductExtAttributesAsync(ProductExtAttributes data) =>
            (data is null) ? 0 : await data.DeleteAsync();

        public virtual ProductExtAttributes CheckIntegrityProductExtAttributes()
        {
            if (ProductExtAttributes is null || ProductBasic is null) 
                return ProductExtAttributes;
            ProductExtAttributes.SetParent(this);
            if (ProductExtAttributes.ProductUuid != ProductBasic.ProductUuid)
                ProductExtAttributes.ProductUuid = ProductBasic.ProductUuid;
            return ProductExtAttributes;
        }



        #endregion ProductExtAttributes - Generated 

        #region Inventory - Generated 
        // One to many children
        protected IList<Inventory> _InventoryDeleted;
        public virtual Inventory AddInventoryDeleted(Inventory del) 
        {
            if (_InventoryDeleted is null)
                _InventoryDeleted = new List<Inventory>();
            var lst = _InventoryDeleted.ToList();
            lst.Add(del);
            _InventoryDeleted = lst;
            return del;
        } 

        public virtual IList<Inventory> AddInventoryDeleted(IList<Inventory> del) 
        {
            if (_InventoryDeleted is null)
                _InventoryDeleted = new List<Inventory>();
            var lst = _InventoryDeleted.ToList();
            lst.AddRange(del);
            _InventoryDeleted = lst;
            return del;
        } 

        public virtual void SetInventoryDeleted(IList<Inventory> del) =>
            _InventoryDeleted = del;

        public virtual void ClearInventoryDeleted() =>
            _InventoryDeleted = null;


        protected IList<Inventory> _Inventory;

        public virtual IList<Inventory> Inventory 
        { 
            get 
            {
                if (_Inventory is null)
                    _Inventory = new List<Inventory>();
                return _Inventory;
            } 
            set
            {
                if (value != null)
                {
                    var valueList = value.ToList();
                    valueList.ForEach(i => i?.SetParent(this));
                    _Inventory = valueList;
                }
                else
                    _Inventory = null;
            } 
        }

        public virtual void CopyInventoryFrom(InventoryData data) 
        {
            if  (data is null) return;
            var lstDeleted = Inventory?.CopyFrom(data.Inventory, new string[] {"ProductUuid"});
            SetInventoryDeleted(lstDeleted);
            foreach (var c in Inventory)
                c?.CopyChildrenFrom(data.Inventory?.FindByRowNum(c.RowNum));
        } 

        public virtual Inventory NewInventory() => new Inventory(dbFactory);

        public virtual Inventory AddInventory(Inventory obj) => 
            Inventory.AddOrReplace(obj.SetParent(this));

        public virtual Inventory RemoveInventory(Inventory obj) => 
            AddInventoryDeleted(Inventory.RemoveObject(obj.SetParent(this)));

        public virtual IList<Inventory> GetInventoryByProductUuid(string ProductUuid) =>
            (string.IsNullOrEmpty(ProductUuid)) 
                ? null 
                : dbFactory.Find<Inventory>("WHERE ProductUuid = @0 ORDER BY RowNum ", ProductUuid).ToList();

        public virtual bool SaveInventory(IList<Inventory> data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteInventory(IList<Inventory> data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<IList<Inventory>> GetInventoryByProductUuidAsync(string ProductUuid) =>
            (string.IsNullOrEmpty(ProductUuid)) 
                ? null
                : (await dbFactory.FindAsync<Inventory>("WHERE ProductUuid = @0 ORDER BY RowNum ", ProductUuid)).ToList();

        public virtual async Task<bool> SaveInventoryAsync(IList<Inventory> data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteInventoryAsync(IList<Inventory> data) =>
            (data is null) ? 0 : await data.DeleteAsync();

        public virtual IList<Inventory> CheckIntegrityInventory()
        {
            if (Inventory is null || ProductBasic is null) 
                return Inventory;
            var seq = 0;
            Inventory.RemoveEmpty();
            var children = Inventory.ToList();
            foreach (var child in children.Where(x => x != null))
            {
                child.SetParent(this);
                if (child.ProductUuid != ProductBasic.ProductUuid)
                    child.ProductUuid = ProductBasic.ProductUuid;
            }
            return children;
        }



        #endregion Inventory - Generated 

        #region InventoryAttributes - Generated 
        // grand children
        protected IList<InventoryAttributes> _InventoryAttributes;

        protected IList<InventoryAttributes> InventoryAttributes 
        { 
            get 
            {
                _InventoryAttributes = Inventory is null ? null : Inventory.SelectMany(x => x.GetChildrenInventoryAttributes()).ToList();
                return _InventoryAttributes;
            } 
            set
            {
                _InventoryAttributes = value;
                if (Inventory != null)
                    foreach (var par in Inventory)
                        par.SetChildrenInventoryAttributes(_InventoryAttributes);
            } 
        }

        protected IList<InventoryAttributes> InventoryAttributesDeleted 
        { 
            get 
            {
                var deleted = new List<InventoryAttributes>();
                if (_InventoryDeleted != null)
                {
                    var del = _InventoryDeleted
                            .Where(x => x?.GetChildrenInventoryAttributes() != null)
                            .SelectMany(x => x?.GetChildrenInventoryAttributes());
                    if (del.Any())
                        deleted.AddRange(del.ToList());
                }
                if (Inventory != null)
                {
                    var delChildren = Inventory
                                    .Where(x => x?.GetChildrenDeletedInventoryAttributes() != null)
                                    .SelectMany(x => x?.GetChildrenDeletedInventoryAttributes());
                    if (delChildren.Any())
                        deleted.AddRange(delChildren.ToList());
                }
                return deleted;
            } 
        }

        public virtual IList<InventoryAttributes> GetInventoryAttributesByProductUuid(string ProductUuid) =>
            (string.IsNullOrEmpty(ProductUuid)) 
                ? null 
                : dbFactory.Find<InventoryAttributes>("WHERE ProductUuid = @0 ORDER BY RowNum ", ProductUuid).ToList();

        public virtual bool SaveInventoryAttributes(IList<InventoryAttributes> data) =>
            (data is null) ? false : data.Save();

        public virtual int DeleteInventoryAttributes(IList<InventoryAttributes> data) =>
            (data is null) ? 0 : data.Delete();

        public virtual async Task<IList<InventoryAttributes>> GetInventoryAttributesByProductUuidAsync(string ProductUuid) =>
            (string.IsNullOrEmpty(ProductUuid)) 
                ? null
                : (await dbFactory.FindAsync<InventoryAttributes>("WHERE ProductUuid = @0 ORDER BY RowNum ", ProductUuid)).ToList();

        public virtual async Task<bool> SaveInventoryAttributesAsync(IList<InventoryAttributes> data) =>
            (data is null) ? false : await data.SaveAsync();

        public virtual async Task<int> DeleteInventoryAttributesAsync(IList<InventoryAttributes> data) =>
            (data is null) ? 0 : await data.DeleteAsync();

        public virtual IList<InventoryAttributes> CheckIntegrityInventoryAttributes()
        {
            if (InventoryAttributes is null || ProductBasic is null) 
                return InventoryAttributes;
            var seq = 0;
            InventoryAttributes.RemoveEmpty();
            var children = InventoryAttributes.ToList();
            foreach (var child in children.Where(x => x != null))
            {
                child.SetParent(this);
                if (child.ProductUuid != ProductBasic.ProductUuid)
                    child.ProductUuid = ProductBasic.ProductUuid;
            }
            return children;
        }


        #endregion InventoryAttributes - Generated 


    }
}



