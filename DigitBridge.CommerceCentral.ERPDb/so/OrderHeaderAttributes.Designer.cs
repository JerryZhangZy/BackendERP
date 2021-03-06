
              

              
    

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
    /// Represents a OrderHeaderAttributes.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [Serializable()]
    [ExplicitColumns]
    [TableName("OrderHeaderAttributes")]
    [PrimaryKey("RowNum", AutoIncrement = true)]
    [UniqueId("OrderUuid")]
    [DtoName("OrderHeaderAttributesDto")]
    public partial class OrderHeaderAttributes : TableRepository<OrderHeaderAttributes, long>
    {

        public OrderHeaderAttributes() : base() {}
        public OrderHeaderAttributes(IDataBaseFactory dbFactory): base(dbFactory) {}

        #region Fields - Generated 
        [Column("OrderUuid",SqlDbType.VarChar,NotNull=true)]
        private string _orderUuid;

        [Column("JsonFields",SqlDbType.VarChar)]
        private string _jsonFields;

        #endregion Fields - Generated 

        #region Properties - Generated 
		public override string UniqueId => OrderUuid; 
		public void CheckUniqueId() 
		{
			if (string.IsNullOrEmpty(OrderUuid)) 
				OrderUuid = Guid.NewGuid().ToString(); 
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
            }
        }

        public virtual string JsonFields
        {
            get
            {
				if (!AllowNull && _jsonFields is null) 
					_jsonFields = String.Empty; 
				return _jsonFields?.TrimEnd(); 
            }
            set
            {
				if (value != null || AllowNull) 
					_jsonFields = value.TrimEnd(); 
            }
        }

        #endregion Properties - Generated 

        #region Methods - Parent

		[XmlIgnore, JsonIgnore, IgnoreCompare]
		private OrderData Parent { get; set; }
		public OrderData GetParent() => Parent;
		public OrderHeaderAttributes SetParent(OrderData parent)
		{
			Parent = parent;
			return this;
		}
        #endregion Methods - Parent


        #region Methods - Generated 
        public override void ClearMetaData()
        {
			base.ClearMetaData(); 
			OrderUuid = Guid.NewGuid().ToString(); 
            return;
        }

        public override OrderHeaderAttributes Clear()
        {
			_orderUuid = String.Empty; 
			_jsonFields = AllowNull ? (string)null : String.Empty; 
            ClearChildren();
            return this;
        }

        public virtual OrderHeaderAttributes ClearChildren()
        {
            return this;
        }

        public virtual OrderHeaderAttributes NewChildren()
        {
            return this;
        }

        public virtual void CopyChildrenFrom(OrderHeaderAttributes data)
        {
            return;
        }

        #endregion Methods - Generated 
    }
}



