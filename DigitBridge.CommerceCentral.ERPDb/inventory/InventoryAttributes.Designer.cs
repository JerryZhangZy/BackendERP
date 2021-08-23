              
              
    

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
using System.Text;
using Newtonsoft.Json;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a InventoryAttributes.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("InventoryAttributes")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("InventoryUuid")]
    [DtoName("InventoryAttributesDto")]
    public partial class InventoryAttributes : TableRepository<InventoryAttributes, long>
    {

        public InventoryAttributes() : base() {}
        public InventoryAttributes(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("ProductUuid",SqlDbType.VarChar,NotNull=true)]
        private string _productUuid;

        [Column("InventoryUuid",SqlDbType.VarChar,NotNull=true)]
        private string _inventoryUuid;

        [Column("JsonFields",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _jsonFields;

        #endregion Fields - Generated 

        #region Properties - Generated 
		[IgnoreCompare] 
		public override string UniqueId => InventoryUuid; 
		public override void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(InventoryUuid)) 
				InventoryUuid = Guid.NewGuid().ToString(); 
		}
		/// <summary>
		/// (Readonly) Product uuid. load from ProductBasic data. <br> Display: false, Editable: false
		/// </summary>
        public virtual string ProductUuid
        {
            get
            {
				return _productUuid?.TrimEnd(); 
            }
            set
            {
				_productUuid = value.TruncateTo(50); 
				OnPropertyChanged("ProductUuid", value);
            }
        }

		/// <summary>
		/// (Readonly) Inventory Item Line uuid, load from inventory data. <br> Display: false, Editable: false
		/// </summary>
        public virtual string InventoryUuid
        {
            get
            {
				return _inventoryUuid?.TrimEnd(); 
            }
            set
            {
				_inventoryUuid = value.TruncateTo(50); 
				OnPropertyChanged("InventoryUuid", value);
            }
        }

		/// <summary>
		/// (Ignore) JSON string
		/// </summary>
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public virtual string JsonFields
        {
            get
            {
				return _jsonFields?.TrimEnd(); 
            }
            set
            {
				_jsonFields = value.TrimEnd(); 
				OnPropertyChanged("JsonFields", value);
            }
        }


        [JsonIgnore, XmlIgnore, IgnoreCompare]
        protected CustomAttributes _Fields;
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public virtual CustomAttributes Fields
        {
            get
            {
				if (_Fields is null) 
					_Fields = new CustomAttributes(dbFactory, "InventoryAttributes"); 
				return _Fields; 
            }
            set
            {
				_Fields = (value is null) ? new CustomAttributes(dbFactory, "InventoryAttributes") : value; 
            }
        }


        #endregion Properties - Generated 

        #region Methods - Parent

		[JsonIgnore, XmlIgnore, IgnoreCompare]
		private InventoryData Parent { get; set; }
		public InventoryData GetParent() => Parent;
		public InventoryAttributes SetParent(InventoryData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			InventoryUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override InventoryAttributes Clear()
        {
            base.Clear();
			_productUuid = String.Empty; 
			_inventoryUuid = String.Empty; 
			_jsonFields = String.Empty; 
			Fields.Clear(); 
            ClearChildren();
            return this;
        }

        public override InventoryAttributes CheckIntegrity()
        {
            CheckUniqueId();
            CheckIntegrityOthers();
            return this;
        }

        public virtual InventoryAttributes ClearChildren()
        {
            return this;
        }

        public virtual InventoryAttributes NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(InventoryAttributes data)
        {
            if (data is null) return;
            return;
        }

		public static IList<InventoryAttributes> FindByProductUuid(IDataBaseFactory dbFactory, string productUuid)
		{
			return dbFactory.Find<InventoryAttributes>("WHERE ProductUuid = @0 ", productUuid).ToList();
		}
		public static long CountByProductUuid(IDataBaseFactory dbFactory, string productUuid)
		{
			return dbFactory.Count<InventoryAttributes>("WHERE ProductUuid = @0 ", productUuid);
		}
		public static async Task<IList<InventoryAttributes>> FindByAsyncProductUuid(IDataBaseFactory dbFactory, string productUuid)
		{
			return (await dbFactory.FindAsync<InventoryAttributes>("WHERE ProductUuid = @0 ", productUuid)).ToList();
		}
		public static async Task<long> CountByAsyncProductUuid(IDataBaseFactory dbFactory, string productUuid)
		{
			return await dbFactory.CountAsync<InventoryAttributes>("WHERE ProductUuid = @0 ", productUuid);
		}

		public override InventoryAttributes ConvertDbFieldsToData()
		{
			base.ConvertDbFieldsToData();
			Fields.LoadFromValueString(JsonFields);
			return this;
		}
		public override InventoryAttributes ConvertDataFieldsToDb()
		{
			base.ConvertDataFieldsToDb();
			JsonFields = Fields.ToValueString();
			return this;
		}

        #endregion Methods - Generated 
    }
}



