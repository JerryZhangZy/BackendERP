              
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
using Helper = DigitBridge.CommerceCentral.ERPDb.InitNumbersHelper;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class InitNumbersQuery : QueryObject<InitNumbersQuery>
    {
        // Table prefix which use in this sql query
        protected static string PREFIX = Helper.TableAllies;

        // Filter fields
        protected QueryFilter<string> _InitNumbersUuid = new QueryFilter<string>("InitNumbersUuid", "InitNumbersUuid", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> InitNumbersUuid => _InitNumbersUuid;


        protected QueryFilter<string> _CustomerUuid = new QueryFilter<string>("CustomerUuid", "CustomerUuid", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> CustomerUuid => _CustomerUuid;


        protected QueryFilter<int> _InActive = new QueryFilter<int>("InActive", "InActive", PREFIX, FilterBy.eq, 0);
        public QueryFilter<int> InActive => _InActive;


        protected QueryFilter<string> _Type = new QueryFilter<string>("Type", "Type", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> Type => _Type;


        public InitNumbersQuery() : base(PREFIX)
        {
            AddFilter(_InitNumbersUuid);
            AddFilter(_CustomerUuid);
            AddFilter(_InActive);
            AddFilter(_Type);

        }

        public override void InitQueryFilter()
        {
            //_OrderDateFrom.FilterValue = DateTime.UtcNow.Date.AddDays(-30);
            //_OrderDateTo.FilterValue = DateTime.UtcNow.Date.AddDays(7);
        }
    }
}
