
              

              
    

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
    /// Represents a InvoiceHeaderAttributes.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("InvoiceHeaderAttributes")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("InvoiceUuid")]
    [DtoName("InvoiceHeaderAttributesDto")]
    public partial class InvoiceHeaderAttributes : TableRepository<InvoiceHeaderAttributes, long>
    {

        public InvoiceHeaderAttributes() : base() {}
        public InvoiceHeaderAttributes(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("InvoiceUuid",SqlDbType.VarChar,NotNull=true)]
        private string _invoiceUuid;

        [Column("JsonFields",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _jsonFields;

        #endregion Fields - Generated 

        #region Properties - Generated 
		[IgnoreCompare] 
		public override string UniqueId => InvoiceUuid; 
		public void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(InvoiceUuid)) 
				InvoiceUuid = Guid.NewGuid().ToString(); 
		}
		/// <summary>
		/// Invoice uuid. <br> Display: false, Editable: false.
		/// </summary>
        public virtual string InvoiceUuid
        {
            get
            {
				return _invoiceUuid?.TrimEnd(); 
            }
            set
            {
				_invoiceUuid = value.TruncateTo(50); 
				OnPropertyChanged("InvoiceUuid", value);
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
					_Fields = new CustomAttributes(dbFactory, "InvoiceHeaderAttributes"); 
				return _Fields; 
            }
            set
            {
				_Fields = (value is null) ? new CustomAttributes(dbFactory, "InvoiceHeaderAttributes") : value; 
            }
        }


        #endregion Properties - Generated 

        #region Methods - Parent

		[JsonIgnore, XmlIgnore, IgnoreCompare]
		private InvoiceData Parent { get; set; }
		public InvoiceData GetParent() => Parent;
		public InvoiceHeaderAttributes SetParent(InvoiceData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			InvoiceUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override InvoiceHeaderAttributes Clear()
        {
            base.Clear();
			_invoiceUuid = String.Empty; 
			_jsonFields = String.Empty; 
			Fields.Clear(); 
            ClearChildren();
            return this;
        }

        public virtual InvoiceHeaderAttributes CheckIntegrity()
        {
            return this;
        }

        public virtual InvoiceHeaderAttributes ClearChildren()
        {
            return this;
        }

        public virtual InvoiceHeaderAttributes NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(InvoiceHeaderAttributes data)
        {
            if (data is null) return;
            return;
        }


		public override InvoiceHeaderAttributes ConvertDbFieldsToData()
		{
			base.ConvertDbFieldsToData();
			Fields.LoadFromValueString(JsonFields);
			return this;
		}
		public override InvoiceHeaderAttributes ConvertDataFieldsToDb()
		{
			base.ConvertDataFieldsToDb();
			JsonFields = Fields.ToValueString();
			return this;
		}

        #endregion Methods - Generated 
    }
}



