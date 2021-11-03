

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Data.SqlClient;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class ApTransactionData
    {
        public ApInvoiceData ApInvoiceData { get; set; }

        #region Get list by ap number

        /// <summary>
        /// Get trans list
        /// </summary>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <param name="number"></param>
        /// <param name="transType"></param>
        /// <param name="transNum"></param>
        /// <returns></returns>
        public async Task<List<ApInvoiceTransaction>> GetListByNumberAsync(int masterAccountNum, int profileNum, string number, int? transType = null, int? transNum = null)
        {
            var sql = @"
SELECT * FROM ApInvoiceTransaction
WHERE MasterAccountNum = @0
AND ProfileNum = @1
AND ApInvoiceNum = @2 
";
            var paras = new List<SqlParameter>()
            {
                new SqlParameter("@0",masterAccountNum),
                new SqlParameter("@1",profileNum),
                new SqlParameter("@2",number)
            };

            if (transType.HasValue)
            {
                sql += " AND TransType=@3";
                paras.Add(new SqlParameter("@3", transType.Value));
            }
            if (transNum.HasValue)
            {
                sql += " AND TransNum=@4";
                paras.Add(new SqlParameter("@4", transNum.Value));
            }

            return (await dbFactory.FindAsync<ApInvoiceTransaction>(sql, paras.ToArray())).ToList();
        }

        #endregion



        #region get data by number 

        public override bool GetByNumber(int masterAccountNum, int profileNum, string number, int transType, int? transNum = null)
        {
            var sql = @"
SELECT TOP 1 * FROM ApInvoiceTransaction
WHERE MasterAccountNum = @0
AND ProfileNum = @1
AND ApInvoiceNum = @2 
AND TransType=@3
";
            var paras = new List<SqlParameter>()
            {
                new SqlParameter("@0",masterAccountNum),
                new SqlParameter("@1",profileNum),
                new SqlParameter("@2",number),
                new SqlParameter("@3", transType)
            };
             
            if (transNum.HasValue)
            {
                sql += " AND TransNum=@4";
                paras.Add(new SqlParameter("@4", transNum.Value));
            }

            var obj = dbFactory.GetBy<ApInvoiceTransaction>(sql, paras.ToArray());
            if (obj is null) return false;
            ApInvoiceTransaction = obj;
            GetOthers();
            if (_OnAfterLoad != null)
                _OnAfterLoad(this);
            return true;
        }
        public override async Task<bool> GetByNumberAsync(int masterAccountNum, int profileNum, string number, int transType, int? transNum = null)
        {
            var sql = @"
SELECT TOP 1 * FROM ApInvoiceTransaction
WHERE MasterAccountNum = @0
AND ProfileNum = @1
AND ApInvoiceNum = @2 
AND TransType=@3
";
            var paras = new List<SqlParameter>()
            {
                new SqlParameter("@0",masterAccountNum),
                new SqlParameter("@1",profileNum),
                new SqlParameter("@2",number),
                new SqlParameter("@3", transType)
            }; 
            if (transNum.HasValue)
            {
                sql += " AND TransNum=@4";
                paras.Add(new SqlParameter("@4", transNum.Value));
            }

            var obj = await dbFactory.GetByAsync<ApInvoiceTransaction>(sql, paras.ToArray());
            if (obj is null) return false;
            ApInvoiceTransaction = obj;
            await GetOthersAsync();
            if (_OnAfterLoad != null)
                _OnAfterLoad(this);
            return true;
        }
        #endregion
    }
}



