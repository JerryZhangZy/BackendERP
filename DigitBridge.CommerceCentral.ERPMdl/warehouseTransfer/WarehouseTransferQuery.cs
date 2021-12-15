              
//-------------------------------------------------------------------------
// This document is generated by T4
// It will overwrite your changes, please keep it as it is
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using Helper = DigitBridge.CommerceCentral.ERPDb.WarehouseTransferHeaderHelper;
using ItemsHelper = DigitBridge.CommerceCentral.ERPDb.WarehouseTransferItemsHelper;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class WarehouseTransferQuery : QueryObject<WarehouseTransferQuery>
    {
        // Table prefix which use in this sql query
        protected static string PREFIX = Helper.TableAllies;
        protected static string ITEMSPREFIX = ItemsHelper.TableAllies;
         
        // Filter fields
        protected QueryFilter<string> _SKU = new QueryFilter<string>("SKU", "SKU", ITEMSPREFIX, FilterBy.eq, string.Empty);
        public QueryFilter<string> SKU => _SKU;

        protected QueryFilter<string> _WarehouseCode = new QueryFilter<string>("WarehouseCode", "WarehouseCode", ITEMSPREFIX, FilterBy.eq, string.Empty);
        public QueryFilter<string> WarehouseCode => _WarehouseCode;

        protected EnumQueryFilter<InventoryUpdateType> _UpdateType = new EnumQueryFilter<InventoryUpdateType>("WarehouseTransferType", "WarehouseTransferType", PREFIX, FilterBy.eq, 0);
        public EnumQueryFilter<InventoryUpdateType> UpdateType => _UpdateType;

        public WarehouseTransferQuery() : base(PREFIX)
        {
            AddFilter(_SKU);
            AddFilter(_WarehouseCode);
            AddFilter(_UpdateType);
        }

        public override void InitQueryFilter()
        {
            //_OrderDateFrom.FilterValue = DateTime.UtcNow.Date.AddDays(-30);
            //_OrderDateTo.FilterValue = DateTime.UtcNow.Date.AddDays(7);
        }
    }
}
