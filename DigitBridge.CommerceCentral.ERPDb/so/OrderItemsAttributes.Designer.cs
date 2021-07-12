
              

              
    

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
    /// Represents a OrderItemsAttributes.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("OrderItemsAttributes")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("OrderItemsUuid")]
    [DtoName("OrderItemsAttributesDto")]
    public partial class OrderItemsAttributes : TableRepository<OrderItemsAttributes, long>
    {

        public OrderItemsAttributes() : base() {}
        public OrderItemsAttributes(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("OrderItemsUuid",SqlDbType.VarChar,NotNull=true)]
        private string _orderItemsUuid;

        [Column("OrderUuid",SqlDbType.VarChar,NotNull=true)]
        private string _orderUuid;

        [Column("JsonFields",SqlDbType.NVarChar,NotNull=true,IsDefault=true)]
        private string _jsonFields;

        #endregion Fields - Generated 

        #region Properties - Generated 
		public override string UniqueId => OrderItemsUuid; 
		public void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(OrderItemsUuid)) 
				OrderItemsUuid = Guid.NewGuid().ToString(); 
		}
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

        [XmlIgnore, JsonIgnore, IgnoreCompare]
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


        [XmlIgnore, JsonIgnore, IgnoreCompare]
        protected CustomAttributes _Fields;
        [XmlIgnore, JsonIgnore, IgnoreCompare]
        public virtual CustomAttributes Fields
        {
            get
            {
				if (_Fields is null) 
					_Fields = new CustomAttributes(dbFactory, "OrderItemsAttributes"); 
				return _Fields; 
            }
            set
            {
				_Fields = (value is null) ? new CustomAttributes(dbFactory, "OrderItemsAttributes") : value; 
            }
        }


        #endregion Properties - Generated 

        #region Methods - Parent

		[XmlIgnore, JsonIgnore, IgnoreCompare]
		private OrderData Parent { get; set; }
		public OrderData GetParent() => Parent;
		public OrderItemsAttributes SetParent(OrderData parent)
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

        public override OrderItemsAttributes Clear()
        {
            base.Clear();
			_orderItemsUuid = String.Empty; 
			_orderUuid = String.Empty; 
			_jsonFields = String.Empty; 
			Fields.Clear(); 
            ClearChildren();
            return this;
        }

        public virtual OrderItemsAttributes ClearChildren()
        {
            return this;
        }

        public virtual OrderItemsAttributes NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(OrderItemsAttributes data)
        {
            if (data is null) return;
            return;
        }

		public IList<OrderItemsAttributes> FindByOrderUuid(string orderUuid)
		{
			return dbFactory.Find<OrderItemsAttributes>("WHERE OrderUuid = @0 ", orderUuid).ToList();
		}
		public long CountByOrderUuid(string orderUuid)
		{
			return dbFactory.Count<OrderItemsAttributes>("WHERE OrderUuid = @0 ", orderUuid);
		}
		public async Task<IList<OrderItemsAttributes>> FindByAsyncOrderUuid(string orderUuid)
		{
			return (await dbFactory.FindAsync<OrderItemsAttributes>("WHERE OrderUuid = @0 ", orderUuid)).ToList();
		}
		public async Task<long> CountByAsyncOrderUuid(string orderUuid)
		{
			return await dbFactory.CountAsync<OrderItemsAttributes>("WHERE OrderUuid = @0 ", orderUuid);
		}

		public override OrderItemsAttributes ConvertDbFieldsToData()
		{
			base.ConvertDbFieldsToData();
			Fields.LoadFromValueString(JsonFields);
			return this;
		}
		public override OrderItemsAttributes ConvertDataFieldsToDb()
		{
			base.ConvertDataFieldsToDb();
			JsonFields = Fields.ToValueString();
			return this;
		}

        #endregion Methods - Generated 
    }
}



