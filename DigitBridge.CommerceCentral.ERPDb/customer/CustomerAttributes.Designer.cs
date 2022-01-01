              
              
    

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
    /// Represents a CustomerAttributes.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("CustomerAttributes")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("CustomerUuid")]
    [DtoName("CustomerAttributesDto")]
    public partial class CustomerAttributes : TableRepository<CustomerAttributes, long>
    {

        public CustomerAttributes() : base() {}
        public CustomerAttributes(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("CustomerUuid",SqlDbType.VarChar,NotNull=true)]
        private string _customerUuid;

        [Column("JsonFields",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _jsonFields;

        #endregion Fields - Generated 

        #region Properties - Generated 
		[IgnoreCompare] 
		public override string UniqueId => CustomerUuid; 
		public override void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(CustomerUuid)) 
				CustomerUuid = Guid.NewGuid().ToString(); 
		}
		/// <summary>
		/// Customer uuid. <br> Display: false, Editable: false.
		/// </summary>
        public virtual string CustomerUuid
        {
            get
            {
				return _customerUuid?.TrimEnd(); 
            }
            set
            {
				_customerUuid = value.TruncateTo(50); 
				OnPropertyChanged("CustomerUuid", value);
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
					_Fields = new CustomAttributes(dbFactory, "CustomerAttributes"); 
				return _Fields; 
            }
            set
            {
				_Fields = (value is null) ? new CustomAttributes(dbFactory, "CustomerAttributes") : value; 
            }
        }


        #endregion Properties - Generated 

        #region Methods - Parent

		[JsonIgnore, XmlIgnore, IgnoreCompare]
		private CustomerData Parent { get; set; }
		public CustomerData GetParent() => Parent;
		public CustomerAttributes SetParent(CustomerData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			CustomerUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override CustomerAttributes Clear()
        {
            base.Clear();
			_customerUuid = String.Empty; 
			_jsonFields = String.Empty; 
			Fields.Clear(); 
            ClearChildren();
            return this;
        }

        public override CustomerAttributes CheckIntegrity()
        {
            CheckUniqueId();
            CheckIntegrityOthers();
            return this;
        }

        public virtual CustomerAttributes ClearChildren()
        {
            return this;
        }

        public virtual CustomerAttributes NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(CustomerAttributes data)
        {
            if (data is null) return;
            return;
        }


		public override CustomerAttributes ConvertDbFieldsToData()
		{
			base.ConvertDbFieldsToData();
			Fields.LoadFromValueString(JsonFields);
			return this;
		}
		public override CustomerAttributes ConvertDataFieldsToDb()
		{
			base.ConvertDataFieldsToDb();
			JsonFields = Fields.ToValueString();
			return this;
		}

        #endregion Methods - Generated 
    }
}



