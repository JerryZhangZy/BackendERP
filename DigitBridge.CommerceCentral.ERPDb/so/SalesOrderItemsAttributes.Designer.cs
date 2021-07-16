
              

              
    

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
    /// Represents a SalesOrderItemsAttributes.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("SalesOrderItemsAttributes")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("OrderItemsUuid")]
    [DtoName("SalesOrderItemsAttributesDto")]
    public partial class SalesOrderItemsAttributes : TableRepository<SalesOrderItemsAttributes, long>
    {

        public SalesOrderItemsAttributes() : base() {}
        public SalesOrderItemsAttributes(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("OrderItemsUuid",SqlDbType.VarChar,NotNull=true)]
        private string _orderItemsUuid;

        [Column("OrderUuid",SqlDbType.VarChar,NotNull=true)]
        private string _orderUuid;

        [Column("JsonFields",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _jsonFields;

        #endregion Fields - Generated 

        #region Properties - Generated 
		[IgnoreCompare] 
		public override string UniqueId => OrderItemsUuid; 
		public void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(OrderItemsUuid)) 
				OrderItemsUuid = Guid.NewGuid().ToString(); 
		}
		/// <summary>
		/// Order item line uuid. <br> Display: false, Editable: false.
		/// </summary>
        public virtual string OrderItemsUuid
        {
            get
            {
				return _orderItemsUuid?.TrimEnd(); 
            }
            set
            {
				_orderItemsUuid = value.TruncateTo(50); 
				OnPropertyChanged("OrderItemsUuid", value);
            }
        }

		/// <summary>
		/// Order uuid. <br> Display: false, Editable: false.
		/// </summary>
        public virtual string OrderUuid
        {
            get
            {
				return _orderUuid?.TrimEnd(); 
            }
            set
            {
				_orderUuid = value.TruncateTo(50); 
				OnPropertyChanged("OrderUuid", value);
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
					_Fields = new CustomAttributes(dbFactory, "SalesOrderItemsAttributes"); 
				return _Fields; 
            }
            set
            {
				_Fields = (value is null) ? new CustomAttributes(dbFactory, "SalesOrderItemsAttributes") : value; 
            }
        }


        #endregion Properties - Generated 

        #region Methods - Parent

		[JsonIgnore, XmlIgnore, IgnoreCompare]
		private SalesOrderData Parent { get; set; }
		public SalesOrderData GetParent() => Parent;
		public SalesOrderItemsAttributes SetParent(SalesOrderData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			OrderItemsUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override SalesOrderItemsAttributes Clear()
        {
            base.Clear();
			_orderItemsUuid = String.Empty; 
			_orderUuid = String.Empty; 
			_jsonFields = String.Empty; 
			Fields.Clear(); 
            ClearChildren();
            return this;
        }

        public virtual SalesOrderItemsAttributes ClearChildren()
        {
            return this;
        }

        public virtual SalesOrderItemsAttributes NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(SalesOrderItemsAttributes data)
        {
            if (data is null) return;
            return;
        }

		public static IList<SalesOrderItemsAttributes> FindByOrderUuid(IDataBaseFactory dbFactory, string orderUuid)
		{
			return dbFactory.Find<SalesOrderItemsAttributes>("WHERE OrderUuid = @0 ", orderUuid).ToList();
		}
		public static long CountByOrderUuid(IDataBaseFactory dbFactory, string orderUuid)
		{
			return dbFactory.Count<SalesOrderItemsAttributes>("WHERE OrderUuid = @0 ", orderUuid);
		}
		public static async Task<IList<SalesOrderItemsAttributes>> FindByAsyncOrderUuid(IDataBaseFactory dbFactory, string orderUuid)
		{
			return (await dbFactory.FindAsync<SalesOrderItemsAttributes>("WHERE OrderUuid = @0 ", orderUuid)).ToList();
		}
		public static async Task<long> CountByAsyncOrderUuid(IDataBaseFactory dbFactory, string orderUuid)
		{
			return await dbFactory.CountAsync<SalesOrderItemsAttributes>("WHERE OrderUuid = @0 ", orderUuid);
		}

		public override SalesOrderItemsAttributes ConvertDbFieldsToData()
		{
			base.ConvertDbFieldsToData();
			Fields.LoadFromValueString(JsonFields);
			return this;
		}
		public override SalesOrderItemsAttributes ConvertDataFieldsToDb()
		{
			base.ConvertDataFieldsToDb();
			JsonFields = Fields.ToValueString();
			return this;
		}

        #endregion Methods - Generated 
    }
}



