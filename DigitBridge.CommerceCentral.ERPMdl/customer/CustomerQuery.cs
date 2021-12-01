using System;
using System.Collections.Generic;
using System.Text;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class CustomerQuery : QueryObject<CustomerQuery>
    {
        // Table prefix which use in this sql query
        protected static string PREFIX = CustomerHelper.TableAllies;
        protected static string PREFIX_ADDRESS = DigitBridge.CommerceCentral.ERPDb.CustomerAddressHelper.TableAllies;

        // Filter fields

        protected QueryFilter<string> _CustomerUuid = new QueryFilter<string>("CustomerUuid", "CustomerUuid", PREFIX, FilterBy.eq, string.Empty);
        public QueryFilter<string> CustomerUuid => _CustomerUuid;

        protected QueryFilter<string> _CustomerCode = new QueryFilter<string>("CustomerCode", "CustomerCode", PREFIX, FilterBy.bw, string.Empty);
        public QueryFilter<string> CustomerCode => _CustomerCode;

        protected QueryFilter<string> _CustomerName = new QueryFilter<string>("CustomerName", "CustomerName", PREFIX, FilterBy.bw, string.Empty);
        public QueryFilter<string> CustomerName => _CustomerName;

        protected QueryFilter<string> _Contact = new QueryFilter<string>("Contact", "Contact", PREFIX, FilterBy.bw, string.Empty);
        public QueryFilter<string> Contact => _Contact;

        protected QueryFilter<string> _Phone1 = new QueryFilter<string>("Phone1", "Phone1", PREFIX, FilterBy.bw, string.Empty);
        public QueryFilter<string> Phone1 => _Phone1;

        protected QueryFilter<string> _Email = new QueryFilter<string>("Email", "Email", PREFIX, FilterBy.bw, string.Empty);
        public QueryFilter<string> Email => _Email;

        protected QueryFilter<string> _WebSite = new QueryFilter<string>("WebSite", "WebSite", PREFIX, FilterBy.bw, string.Empty);
        public QueryFilter<string> WebSite => _WebSite;

        protected QueryFilter<int> _CustomerType = new QueryFilter<int>("CustomerType", "CustomerType", PREFIX, FilterBy.eq, 0);
        public QueryFilter<int> CustomerType => _CustomerType;

        protected QueryFilter<int> _CustomerStatus = new QueryFilter<int>("CustomerStatus", "CustomerStatus", PREFIX, FilterBy.eq, 0);
        public QueryFilter<int> CustomerStatus => _CustomerStatus;

        protected QueryFilter<string> _BusinessType = new QueryFilter<string>("BusinessType", "BusinessType", PREFIX, FilterBy.bw, string.Empty);
        public QueryFilter<string> BusinessType => _BusinessType;

        protected QueryFilter<string> _FirstDate = new QueryFilter<string>("FirstDate", "FirstDate", PREFIX, FilterBy.bw, string.Empty);
        public QueryFilter<string> FirstDate => _FirstDate;

        protected QueryFilter<string> _Priority = new QueryFilter<string>("Priority", "Priority", PREFIX, FilterBy.bw, string.Empty);
        public QueryFilter<string> Priority => _Priority;

        protected QueryFilter<string> _Area = new QueryFilter<string>("Area", "Area", PREFIX, FilterBy.bw, string.Empty);
        public QueryFilter<string> Area => _Area;

        protected QueryFilter<string> _Region = new QueryFilter<string>("Region", "Region", PREFIX, FilterBy.bw, string.Empty);
        public QueryFilter<string> Region => _Region;

        protected QueryFilter<string> _Districtn = new QueryFilter<string>("Districtn", "Districtn", PREFIX, FilterBy.bw, string.Empty);
        public QueryFilter<string> Districtn => _Districtn;

        protected QueryFilter<string> _Zone = new QueryFilter<string>("Zone", "Zone", PREFIX, FilterBy.bw, string.Empty);
        public QueryFilter<string> Zone => _Zone;

        protected QueryFilter<string> _ClassCode = new QueryFilter<string>("ClassCode", "ClassCode", PREFIX, FilterBy.bw, string.Empty);
        public QueryFilter<string> ClassCode => _ClassCode;

        protected QueryFilter<string> _DepartmentCode = new QueryFilter<string>("DepartmentCode", "DepartmentCode", PREFIX, FilterBy.bw, string.Empty);
        public QueryFilter<string> DepartmentCode => _DepartmentCode;

        protected QueryFilter<string> _DivisionCode = new QueryFilter<string>("DivisionCode", "DivisionCode", PREFIX, FilterBy.bw, string.Empty);
        public QueryFilter<string> DivisionCode => _DivisionCode;

        protected QueryFilter<string> _SourceCode = new QueryFilter<string>("SourceCode", "SourceCode", PREFIX, FilterBy.bw, string.Empty);
        public QueryFilter<string> SourceCode => _SourceCode;


        //protected EnumQueryFilter<BusinessType> _BusinessType = new EnumQueryFilter<BusinessType>("BusinessType", "BusinessType", PREFIX, FilterBy.eq, -1);
        //public EnumQueryFilter<BusinessType> BusinessType => _BusinessType;

        public CustomerQuery() : base(PREFIX)
        {
            AddFilter(_CustomerUuid);
            AddFilter(_CustomerCode);
            AddFilter(_CustomerName);
            AddFilter(_Contact);
            AddFilter(_Phone1);
            AddFilter(_Email);
            AddFilter(_WebSite);
            AddFilter(_CustomerType);
            AddFilter(_CustomerStatus);
            AddFilter(_BusinessType);
            AddFilter(_FirstDate);
            AddFilter(_Priority);
            AddFilter(_Area);
            AddFilter(_Region);
            AddFilter(_Districtn);
            AddFilter(_Zone);
            AddFilter(_ClassCode);
            AddFilter(_DepartmentCode);
            AddFilter(_DivisionCode);
            AddFilter(_SourceCode);
        }
        public override void InitQueryFilter()
        {
        }

        //public override void SetAvailableOrderByList(IList<string> orderByList)
        //{
        //    base.SetAvailableOrderByList();
        //    AddAvailableOrderByList(
        //        new KeyValuePair<string, string>("CustomerCode", "CustomerCode"),
        //        new KeyValuePair<string, string>("CustomerName", "CustomerName"),
        //        new KeyValuePair<string, string>("BusinessType", "BusinessType "),
        //        new KeyValuePair<string, string>("ClassCode", "ClassCode"),
        //        new KeyValuePair<string, string>("DepartmentCode", "DepartmentCode ")
        //        );

        //}


    }
}
