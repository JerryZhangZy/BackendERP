              
              
    

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
    /// Represents a ProductExtAttributes.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("ProductExtAttributes")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("ProductUuid")]
    [DtoName("ProductExtAttributesDto")]
    public partial class ProductExtAttributes : TableRepository<ProductExtAttributes, long>
    {

        public ProductExtAttributes() : base() {}
        public ProductExtAttributes(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("ProductUuid",SqlDbType.VarChar,NotNull=true)]
        private string _productUuid;

        [Column("JsonFields",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _jsonFields;

        #endregion Fields - Generated 

        #region Properties - Generated 
		[IgnoreCompare] 
		public override string UniqueId => ProductUuid; 
		public override void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(ProductUuid)) 
				ProductUuid = Guid.NewGuid().ToString(); 
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
					_Fields = new CustomAttributes(dbFactory, "ProductExtAttributes"); 
				return _Fields; 
            }
            set
            {
				_Fields = (value is null) ? new CustomAttributes(dbFactory, "ProductExtAttributes") : value; 
            }
        }


        #endregion Properties - Generated 

        #region Methods - Parent

		[JsonIgnore, XmlIgnore, IgnoreCompare]
		private InventoryData Parent { get; set; }
		public InventoryData GetParent() => Parent;
		public ProductExtAttributes SetParent(InventoryData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			ProductUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override ProductExtAttributes Clear()
        {
            base.Clear();
			_productUuid = String.Empty; 
			_jsonFields = String.Empty; 
			Fields.Clear(); 
            ClearChildren();
            return this;
        }

        public override ProductExtAttributes CheckIntegrity()
        {
            CheckUniqueId();
            CheckIntegrityOthers();
            return this;
        }

        public virtual ProductExtAttributes ClearChildren()
        {
            return this;
        }

        public virtual ProductExtAttributes NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(ProductExtAttributes data)
        {
            if (data is null) return;
            return;
        }


		public override ProductExtAttributes ConvertDbFieldsToData()
		{
			base.ConvertDbFieldsToData();
			Fields.LoadFromValueString(JsonFields);
			return this;
		}
		public override ProductExtAttributes ConvertDataFieldsToDb()
		{
			base.ConvertDataFieldsToDb();
			JsonFields = Fields.ToValueString();
			return this;
		}

        #endregion Methods - Generated 
    }
}



