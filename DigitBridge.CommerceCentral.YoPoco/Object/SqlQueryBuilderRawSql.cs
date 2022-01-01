using DigitBridge.Base.Utility;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DigitBridge.CommerceCentral.YoPoco
{
    /// <summary>
    /// </summary>
    public partial class SqlQueryBuilderRawSql<TQueryObject> : SqlQueryBuilder<TQueryObject>, IMessage
        where TQueryObject : IQueryObject, new()
    {
        public SqlQueryBuilderRawSql(IDataBaseFactory dbFactory) : base(dbFactory) { }
        public SqlQueryBuilderRawSql(IDataBaseFactory dbFactory, TQueryObject QueryObject) : base(dbFactory, QueryObject) { }

        //protected virtual string GetSQL_select()
        //{
        //    return this.SQL_Select;
        //}
        protected override string GetSQL_from()
        {
            this.SQL_From = string.Empty;
            return this.SQL_From;
        }
        protected override string GetSQL_where()
        {
            this.SQL_Where = string.Empty;
            return this.SQL_Where;
        }
        protected virtual string GetSQL_orderBy()
        {
            this.SQL_OrderBy = string.Empty;
            return this.SQL_OrderBy;
        }
        protected override void AddDefaultOrderBy()
        {
        }
    }
}
