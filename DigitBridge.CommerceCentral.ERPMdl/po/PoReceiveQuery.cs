              
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
using Helper = DigitBridge.CommerceCentral.ERPDb.PoTransactionHelper;

namespace DigitBridge.CommerceCentral.ERPMdl
{


    public class PoReceiveQuery : QueryObject<PoReceiveQuery>
    {
        protected static string PREFIX = ERPDb.PoTransactionHelper.TableAllies;

        // Table prefix which use in this sql query
        protected static string ReturnItemPREFIX = ERPDb.PoTransactionHelper.TableAllies;
        protected static string TranSactionPREFIX = ERPDb.PoTransactionHelper.TableAllies;
        // Filter fields 

        protected QueryFilter<string> _TransUuid = new QueryFilter<string>("TransUuid", "TransUuid", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> TransUuid => _TransUuid;


        protected QueryFilter<long> _TransNum = new QueryFilter<long>("TransNum", "TransNum", PREFIX, FilterBy.eq, 0);
        public QueryFilter<long> TransNum => _TransNum;


        protected QueryFilter<string> _PoUuid = new QueryFilter<string>("PoUuid", "PoUuid", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> PoUuid => _PoUuid;


        protected QueryFilter<string> _PoNumFrom = new QueryFilter<string>("_PoNumFrom", "PoNum", InvoiceHeaderHelper.TableAllies, FilterBy.ge, string.Empty, isNVarChar: true);
        public QueryFilter<string> PoNumFrom => _PoNumFrom;

        protected QueryFilter<string> _PoNumTo = new QueryFilter<string>("_PoNumTo", "PoNum", InvoiceHeaderHelper.TableAllies, FilterBy.le, string.Empty, isNVarChar: true);
        public QueryFilter<string> PoNumTo => _PoNumTo;



        protected QueryFilter<int> _TransType = new QueryFilter<int>("TransType", "TransType", PREFIX, FilterBy.eq, -1);
        public QueryFilter<int> TransType => _TransType;


        protected QueryFilter<int> _TransStatus = new QueryFilter<int>("TransStatus", "TransStatus", PREFIX, FilterBy.eq, -1);
        public QueryFilter<int> TransStatus => _TransStatus;






        protected QueryFilter<DateTime> _TransDateFrom = new QueryFilter<DateTime>("TransDateFrom", "TransDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        public QueryFilter<DateTime> TransDateFrom => _TransDateFrom;

        protected QueryFilter<DateTime> _TransDateTo = new QueryFilter<DateTime>("TransDateTo", "TransDate", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime, isDate: true);
        public QueryFilter<DateTime> TransDateTo => _TransDateTo;



        protected QueryFilter<string> _VendorUuid = new QueryFilter<string>("VendorUuid", "VendorUuid", PREFIX, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> VendorUuid => _VendorUuid;

        protected QueryFilter<string> _VendorName = new QueryFilter<string>("VendorName", "VendorName", PREFIX, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> VendorName => _VendorName;


        protected QueryFilter<string> _VendorInvoiceNumFrom = new QueryFilter<string>("_PoNumFrom", "VendorInvoiceNum", InvoiceHeaderHelper.TableAllies, FilterBy.ge, string.Empty, isNVarChar: true);
        public QueryFilter<string> VendorInvoiceNumFrom => _VendorInvoiceNumFrom;

        protected QueryFilter<string> _VendorInvoiceNumTo = new QueryFilter<string>("_VendorInvoiceNumTo", "VendorInvoiceNum", InvoiceHeaderHelper.TableAllies, FilterBy.le, string.Empty, isNVarChar: true);
        public QueryFilter<string> VendorInvoiceNumTo => _VendorInvoiceNumTo;



        protected QueryFilter<DateTime> _VendorInvoiceDateFrom = new QueryFilter<DateTime>("VendorInvoiceDateFrom", "VendorInvoiceDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        public QueryFilter<DateTime> VendorInvoiceDateFrom => _VendorInvoiceDateFrom;

        protected QueryFilter<DateTime> _VendorInvoiceDateTo = new QueryFilter<DateTime>("VendorInvoiceDateTo", "VendorInvoiceDate", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime, isDate: true);
        public QueryFilter<DateTime> VendorInvoiceDateTo => _VendorInvoiceDateTo;




        protected QueryFilter<DateTime> _DueDateFrom = new QueryFilter<DateTime>("DueDateFrom", "DueDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        public QueryFilter<DateTime> DueDateFrom => _DueDateFrom;

        protected QueryFilter<DateTime> _DueDateTo = new QueryFilter<DateTime>("DueDateTo", "DueDate", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime, isDate: true);
        public QueryFilter<DateTime> DueDateTo => _DueDateTo;



        public PoReceiveQuery() : base(TranSactionPREFIX)
        {
            AddFilter(_VendorName);
            AddFilter(_TransUuid);
            AddFilter(_TransNum);
            AddFilter(_PoUuid);
            AddFilter(_PoNumFrom);
            AddFilter(_PoNumTo);
            AddFilter(_TransType);
            AddFilter(_TransStatus);

            AddFilter(_TransDateFrom);
            AddFilter(_TransDateTo);

            AddFilter(_VendorUuid);
            AddFilter(_VendorInvoiceNumFrom);
            AddFilter(_VendorInvoiceNumTo);

            AddFilter(_VendorInvoiceDateFrom);
            AddFilter(_VendorInvoiceDateTo);
            AddFilter(_DueDateFrom);
            AddFilter(_DueDateTo);


        }
        public override void InitQueryFilter()
        {
            _TransDateFrom.FilterValue = DateTime.UtcNow.Date.AddDays(-30);
            _TransDateTo.FilterValue = DateTime.UtcNow.Date;

        }
        protected override void SetAvailableOrderByList()
        {
            base.SetAvailableOrderByList();
            AddAvailableOrderByList(
                new KeyValuePair<string, string>("TransDate", "TransDate DESC"),
                new KeyValuePair<string, string>("TransNum", "TransNum DESC"),
                new KeyValuePair<string, string>("VendorCode", "VendorCode"),
                new KeyValuePair<string, string>("VendorInvoiceDate", "VendorInvoiceDate DESC"),
                new KeyValuePair<string, string>("VendorInvoiceNum", "VendorInvoiceNum DESC")
                );

        }
    }
}
