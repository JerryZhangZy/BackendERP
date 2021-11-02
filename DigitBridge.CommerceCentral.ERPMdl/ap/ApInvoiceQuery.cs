              
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
using Helper = DigitBridge.CommerceCentral.ERPDb.ApInvoiceHeaderHelper;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class ApInvoiceQuery : QueryObject<ApInvoiceQuery>
    {
        // Table prefix which use in this sql query
        protected static string PREFIX = Helper.TableAllies;

        // Filter fields
        protected QueryFilter<string> _ApInvoiceUuid = new QueryFilter<string>("ApInvoiceUuid", "ApInvoiceUuid", PREFIX, FilterBy.eq, string.Empty, isNVarChar: true);
        public QueryFilter<string> ApInvoiceUuid => _ApInvoiceUuid;


 

        protected QueryFilter<string> _ApInvoiceNumFrom = new QueryFilter<string>("_ApInvoiceNumFrom", "ApInvoiceNum", InvoiceHeaderHelper.TableAllies, FilterBy.ge, string.Empty, isNVarChar: true);
        public QueryFilter<string> ApInvoiceNumFrom => _ApInvoiceNumFrom;

        protected QueryFilter<string> _ApInvoiceNumTo = new QueryFilter<string>("_ApInvoiceNumTo", "ApInvoiceNum", InvoiceHeaderHelper.TableAllies, FilterBy.le, string.Empty, isNVarChar: true);
        public QueryFilter<string> ApInvoiceNumTo => _ApInvoiceNumTo;



        protected QueryFilter<long> _ApInvoiceType = new QueryFilter<long>("ApInvoiceType", "ApInvoiceType", PREFIX, FilterBy.eq, 0);
        public QueryFilter<long> ApInvoiceType => _ApInvoiceType;


        protected QueryFilter<long> _ApInvoiceStatus = new QueryFilter<long>("ApInvoiceStatus", "ApInvoiceStatus", PREFIX, FilterBy.eq, 0);
        public QueryFilter<long> ApInvoiceStatus => _ApInvoiceStatus;


 

        protected QueryFilter<DateTime> _ApInvoiceDateFrom = new QueryFilter<DateTime>("ApInvoiceDateFrom", "ApInvoiceDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        public QueryFilter<DateTime> ApInvoiceDateFrom => _ApInvoiceDateFrom;

        protected QueryFilter<DateTime> _ApInvoiceDateTo = new QueryFilter<DateTime>("ApInvoiceDateTo", "ApInvoiceDate", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime, isDate: true);
        public QueryFilter<DateTime> ApInvoiceDateTo => _ApInvoiceDateTo;

 

        protected QueryFilter<string> _VendorUuid = new QueryFilter<string>("VendorUuid", "VendorUuid", PREFIX, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> VendorUuid => _VendorUuid;


        protected QueryFilter<string> _VendorNum = new QueryFilter<string>("VendorNum", "VendorNum", PREFIX, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> VendorNum => _VendorNum;

        protected QueryFilter<string> _VendorName = new QueryFilter<string>("VendorName", "VendorName", PREFIX, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> VendorName => _VendorName;


        protected QueryFilter<string> _VendorInvoiceNum = new QueryFilter<string>("VendorInvoiceNum", "VendorInvoiceNum", PREFIX, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> VendorInvoiceNum => _VendorInvoiceNum;


        //protected QueryFilter<DateTime> _VendorInvoiceDate = new QueryFilter<DateTime>("VendorInvoiceDate ", "VendorInvoiceDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        //public QueryFilter<DateTime> VendorInvoiceDate => _VendorInvoiceDate;



        protected QueryFilter<DateTime> _VendorInvoiceDateFrom = new QueryFilter<DateTime>("VendorInvoiceDateFrom", "VendorInvoiceDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        public QueryFilter<DateTime> VendorInvoiceDateFrom => _VendorInvoiceDateFrom;

        protected QueryFilter<DateTime> _VendorInvoiceDateTo = new QueryFilter<DateTime>("VendorInvoiceDateTo", "VendorInvoiceDate", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime, isDate: true);
        public QueryFilter<DateTime> VendorInvoiceDateTo => _VendorInvoiceDateTo;



        protected QueryFilter<DateTime> _DueDateFrom = new QueryFilter<DateTime>("DueDateFrom", "DueDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        public QueryFilter<DateTime> DueDateFrom => _DueDateFrom;

        protected QueryFilter<DateTime> _DueDateTo = new QueryFilter<DateTime>("DueDateTo", "DueDate", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime, isDate: true);
        public QueryFilter<DateTime> DueDateTo => _DueDateTo;


 
 


        protected QueryFilter<DateTime> _BillDateFrom = new QueryFilter<DateTime>("BillDateFrom", "BillDate", PREFIX, FilterBy.ge, SqlQuery._SqlMinDateTime, isDate: true);
        public QueryFilter<DateTime> BillDateFrom => _BillDateFrom;

        protected QueryFilter<DateTime> _BillDateTo = new QueryFilter<DateTime>("BillDateTo", "BillDate", PREFIX, FilterBy.le, SqlQuery._AppMaxDateTime, isDate: true);
        public QueryFilter<DateTime> BillDateTo => _BillDateTo;



        protected QueryFilter<string> _PoUuid = new QueryFilter<string>("PoUuid", "PoUuid", PREFIX, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> PoUuid => _PoUuid;


        protected QueryFilter<string> _PoNum = new QueryFilter<string>("PoNum", "PoNum", PREFIX, FilterBy.bw, string.Empty, isNVarChar: true);
        public QueryFilter<string> PoNum => _PoNum;

        public ApInvoiceQuery() : base(PREFIX)
        {
            AddFilter(_ApInvoiceUuid);

            AddFilter(_ApInvoiceNumFrom);
            AddFilter(_ApInvoiceNumTo);

            AddFilter(_ApInvoiceType);
            AddFilter(_ApInvoiceStatus);

            AddFilter(_ApInvoiceDateFrom);
            AddFilter(_ApInvoiceDateTo);


            AddFilter(_VendorUuid);
            AddFilter(_VendorNum);
            AddFilter(_VendorName);
            AddFilter(_VendorInvoiceNum);

            AddFilter(_VendorInvoiceDateFrom);
            AddFilter(_VendorInvoiceDateTo);



            AddFilter(_DueDateFrom);
            AddFilter(_DueDateTo);

            AddFilter(_BillDateFrom);
            AddFilter(_BillDateTo);

            AddFilter(_PoUuid);
            AddFilter(_PoNum);
            
        }

        public override void InitQueryFilter()
        {
            _ApInvoiceDateFrom.FilterValue = DateTime.Today.AddDays(-30);
            _ApInvoiceDateTo.FilterValue = DateTime.Today.AddDays(7);

 
        }
    }

   
}
