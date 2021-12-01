              
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
using Helper = DigitBridge.CommerceCentral.ERPDb.VendorHelper;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class VendorQuery : QueryObject<VendorQuery>
    {
        // Table prefix which use in this sql query
        protected static string PREFIX = Helper.TableAllies;

        // Filter fields
        protected QueryFilter<string> _VendorUuid = new QueryFilter<string>("VendorUuid", "VendorUuid", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> VendorUuid => _VendorUuid;

        protected QueryFilter<string> _VendorCode = new QueryFilter<string>("VendorCode", "VendorCode", PREFIX, FilterBy.bw, string.Empty);
        public QueryFilter<string> VendorCode => _VendorCode;

        protected QueryFilter<string> _VendorName = new QueryFilter<string>("VendorName", "VendorName", PREFIX, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> VendorName => _VendorName;

        protected QueryFilter<string> _Phone1 = new QueryFilter<string>("Phone1", "Phone1", PREFIX, FilterBy.bw, string.Empty);
        public QueryFilter<string> Phone1 => _Phone1;

        protected QueryFilter<string> _Email = new QueryFilter<string>("Email", "Email", PREFIX, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> Email => _Email;

        protected EnumQueryFilter<VendorType> _VendorType = new EnumQueryFilter<VendorType>("VendorType", "VendorType", PREFIX, FilterBy.eq, 0);
        public EnumQueryFilter<VendorType> VendorType => _VendorType;

        protected EnumQueryFilter<VendorStatus> _VendorStatus = new EnumQueryFilter<VendorStatus>("VendorStatus", "VendorStatus", PREFIX, FilterBy.eq, 0);
        public EnumQueryFilter<VendorStatus> VendorStatus => _VendorStatus;

        protected QueryFilter<string> _BusinessType = new QueryFilter<string>("BusinessType", "BusinessType", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> BusinessType => _BusinessType;

        protected QueryFilter<string> _Priority = new QueryFilter<string>("Priority", "Priority", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> Priority => _VendorUuid;

        protected QueryFilter<string> _Area = new QueryFilter<string>("Area", "Area", PREFIX, FilterBy.bw, string.Empty);
        public QueryFilter<string> Area => _Area;

        protected QueryFilter<string> _ClassCode = new QueryFilter<string>("ClassCode", "ClassCode", PREFIX, FilterBy.bw, string.Empty);
        public QueryFilter<string> ClassCode => _ClassCode;

        protected QueryFilter<string> _DepartmentCode = new QueryFilter<string>("DepartmentCode", "DepartmentCode", PREFIX, FilterBy.bw, string.Empty);
        public QueryFilter<string> DepartmentCode => _DepartmentCode;

        public VendorQuery() : base(PREFIX)
        {
            AddFilter(_VendorUuid);
            AddFilter(_VendorCode);
            AddFilter(_VendorName);
            AddFilter(_Phone1);
            AddFilter(_Email);
            AddFilter(_VendorType);
            AddFilter(_VendorStatus);
            AddFilter(_BusinessType);
            AddFilter(_Priority);
            AddFilter(_Area);
            AddFilter(_ClassCode);
            AddFilter(_DepartmentCode);
        }

        public override void InitQueryFilter()
        {
            //_OrderDateFrom.FilterValue = DateTime.UtcNow.Date.AddDays(-30);
            //_OrderDateTo.FilterValue = DateTime.UtcNow.Date.AddDays(7);
        }

        //public override void SetAvailableOrderByList(IList<string> orderByList)
        //{
        //    base.SetAvailableOrderByList();
        //    AddAvailableOrderByList(
        //        new KeyValuePair<string, string>("VendorCode", "VendorCode"),
        //        new KeyValuePair<string, string>("VendorName", "VendorName"),
        //        new KeyValuePair<string, string>("ClassCode", "ClassCode "),
        //        new KeyValuePair<string, string>("DepartmentCode", "DepartmentCode ")
        //        );
        //}

    }
}
