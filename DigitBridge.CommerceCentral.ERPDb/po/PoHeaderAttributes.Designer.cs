              
              
    

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
    /// Represents a PoHeaderAttributes.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("PoHeaderAttributes")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("PoUuid")]
    [DtoName("PoHeaderAttributesDto")]
    public partial class PoHeaderAttributes : TableRepository<PoHeaderAttributes, long>
    {

        public PoHeaderAttributes() : base() {}
        public PoHeaderAttributes(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("PoUuid",SqlDbType.VarChar,NotNull=true)]
        private string _poUuid;

        [Column("JsonFields",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _jsonFields;

        #endregion Fields - Generated 

        #region Properties - Generated 
		[IgnoreCompare] 
		public override string UniqueId => PoUuid; 
		public override void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(PoUuid)) 
				PoUuid = Guid.NewGuid().ToString(); 
		}
		/// <summary>
		/// Global Unique Guid for P/O. <br> Display: false, Editable: false.
		/// </summary>
        public virtual string PoUuid
        {
            get
            {
				return _poUuid?.TrimEnd(); 
            }
            set
            {
				_poUuid = value.TruncateTo(50); 
				OnPropertyChanged("PoUuid", value);
            }
        }

		/// <summary>
		/// (Ignore) JSON string.
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
					_Fields = new CustomAttributes(dbFactory, "PoHeaderAttributes"); 
				return _Fields; 
            }
            set
            {
				_Fields = (value is null) ? new CustomAttributes(dbFactory, "PoHeaderAttributes") : value; 
            }
        }


        #endregion Properties - Generated 

        #region Methods - Parent

		[JsonIgnore, XmlIgnore, IgnoreCompare]
		private PurchaseOrderData Parent { get; set; }
		public PurchaseOrderData GetParent() => Parent;
		public PoHeaderAttributes SetParent(PurchaseOrderData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			PoUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override PoHeaderAttributes Clear()
        {
            base.Clear();
			_poUuid = String.Empty; 
			_jsonFields = String.Empty; 
			Fields.Clear(); 
            ClearChildren();
            return this;
        }

        public override PoHeaderAttributes CheckIntegrity()
        {
            CheckUniqueId();
            CheckIntegrityOthers();
            return this;
        }

        public virtual PoHeaderAttributes ClearChildren()
        {
            return this;
        }

        public virtual PoHeaderAttributes NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(PoHeaderAttributes data)
        {
            if (data is null) return;
            return;
        }


		public override PoHeaderAttributes ConvertDbFieldsToData()
		{
			base.ConvertDbFieldsToData();
			Fields.LoadFromValueString(JsonFields);
			return this;
		}
		public override PoHeaderAttributes ConvertDataFieldsToDb()
		{
			base.ConvertDataFieldsToDb();
			JsonFields = Fields.ToValueString();
			return this;
		}

        #endregion Methods - Generated 
    }
}



