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
using DigitBridge.CommerceCentral.ERPDb;
using Microsoft.AspNetCore.Http;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public partial class so_customerCode : DemListAdoBase
    {
        protected new string ListFor = "so_cus_id";

        public so_customerCode() : base() { }
        public so_customerCode(string SqlSelectFromStatement, bool loadAll)
            : base(SqlSelectFromStatement, loadAll) { }
        public so_customerCode(string SqlSelectFromStatement, bool loadAll, string term)
            : base(SqlSelectFromStatement, loadAll, term) { }
        public so_customerCode(SelectListPayload obj) : base(obj) { }


        #region method to override by each child class
        public override string GetSqlFromView()
        {
            if (!string.IsNullOrWhiteSpace(this.SqlSelectFromStatement))
            {
                return string.Format("( {0} )", this.SqlSelectFromStatement);
            }
            else
            {
                return "orders";
            }
        }

        public override string GetSQLForAll()
        {
            this.SQL = string.Format("SELECT i.*, COALESCE(d.cus_nm,'') AS descrip FROM ( " +
                    "SELECT o1.cus_id AS cd, COUNT(1) AS cnt " +
                    "FROM {0} o1 " +
                    "WHERE " +
                    "COALESCE(o1.cus_id,'') != '' " +
                    "GROUP BY o1.cus_id " +
                ") i " +
                "LEFT JOIN customer d ON (d.cus_id = i.cd) " +
                "ORDER BY i.cd "
                , GetSqlFromView()
            );
            return this.SQL;
        }
        public override string GetSQLForStartValue()
        {
            this.SQL = string.Format("SELECT i.*, COALESCE(d.cus_nm,'') AS descrip FROM ( " +
                    "SELECT TOP {0} o1.cus_id AS cd, COUNT(1) AS cnt " +
                    "FROM {1} o1 " +
                    "WHERE " +
                    "COALESCE(o1.cus_id,'') != '' AND " +
                    "COALESCE(o1.cus_id,'') like '{2}' " +
                    "GROUP BY o1.cus_id " +
                ") i " +
                "LEFT JOIN customer d ON (d.cus_id = i.cd) " +
                "ORDER BY i.cd "
                , this.LoadRecords
                , this.GetSqlFromView()
                , this.StartFromValue
            );

            return this.SQL;
        }
        #endregion
    }
}
