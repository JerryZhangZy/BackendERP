
              

              
    

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
    /// Represents a InvoiceItemsAttributes.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("InvoiceItemsAttributes")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("InvoiceItemsUuid")]
    [DtoName("InvoiceItemsAttributesDto")]
    public partial class InvoiceItemsAttributes : TableRepository<InvoiceItemsAttributes, long>
    {

        public InvoiceItemsAttributes() : base() {}
        public InvoiceItemsAttributes(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("InvoiceItemsUuid",SqlDbType.VarChar,NotNull=true)]
        private string _invoiceItemsUuid;

        [Column("InvoiceUuid",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _invoiceUuid;

        [Column("JsonFields",SqlDbType.VarChar,NotNull=true,IsDefault=true)]
        private string _jsonFields;

        #endregion Fields - Generated 

        #region Properties - Generated 
		public override string UniqueId => InvoiceItemsUuid; 
		public void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(InvoiceItemsUuid)) 
				InvoiceItemsUuid = Guid.NewGuid().ToString(); 
		}
        public virtual string InvoiceItemsUuid
        {
            get
            {
				return _invoiceItemsUuid?.TrimEnd(); 
            }
            set
            {
				_invoiceItemsUuid = value.TruncateTo(50); 
            }
        }

        public virtual string InvoiceUuid
        {
            get
            {
				return _invoiceUuid?.TrimEnd(); 
            }
            set
            {
				_invoiceUuid = value.TruncateTo(50); 
            }
        }

        public virtual string JsonFields
        {
            get
            {
				return _jsonFields?.TrimEnd(); 
            }
            set
            {
				_jsonFields = value.TrimEnd(); 
            }
        }

        #endregion Properties - Generated 

        #region Methods - Parent

		[XmlIgnore, JsonIgnore, IgnoreCompare]
		private InvoiceData Parent { get; set; }
		public InvoiceData GetParent() => Parent;
		public InvoiceItemsAttributes SetParent(InvoiceData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			InvoiceItemsUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override InvoiceItemsAttributes Clear()
        {
			_invoiceItemsUuid = String.Empty; 
			_invoiceUuid = String.Empty; 
			_jsonFields = String.Empty; 
            ClearChildren();
            return this;
        }

        public virtual InvoiceItemsAttributes ClearChildren()
        {
            return this;
        }

        public virtual InvoiceItemsAttributes NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(InvoiceItemsAttributes data)
        {
            if (data is null) return;
            return;
        }

		public IList<InvoiceItemsAttributes> FindByInvoiceUuid(string invoiceUuid)
		{
			return dbFactory.Find<InvoiceItemsAttributes>("WHERE InvoiceUuid = @0 ", invoiceUuid).ToList();
		}
		public long CountByInvoiceUuid(string invoiceUuid)
		{
			return dbFactory.Count<InvoiceItemsAttributes>("WHERE InvoiceUuid = @0 ", invoiceUuid);
		}
		public async Task<IList<InvoiceItemsAttributes>> FindByAsyncInvoiceUuid(string invoiceUuid)
		{
			return (await dbFactory.FindAsync<InvoiceItemsAttributes>("WHERE InvoiceUuid = @0 ", invoiceUuid)).ToList();
		}
		public async Task<long> CountByAsyncInvoiceUuid(string invoiceUuid)
		{
			return await dbFactory.CountAsync<InvoiceItemsAttributes>("WHERE InvoiceUuid = @0 ", invoiceUuid);
		}
        #endregion Methods - Generated 
    }
}



