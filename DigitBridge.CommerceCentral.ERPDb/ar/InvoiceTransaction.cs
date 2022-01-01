



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
    public partial class InvoiceTransaction
    {
        /// <summary>
        ///  Get invoiceTransaction list by invoice number
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <param name="transType"></param>
        /// <param name="transNum"></param>
        /// <returns></returns>
        public virtual async Task<List<InvoiceTransaction>> GetByInvoiceNumberAsync(string invoiceNumber, int masterAccountNum, int profileNum, TransTypeEnum transType, int? transNum = null)
        {
            var sql = $"SELECT * FROM InvoiceTransaction where InvoiceNumber='{invoiceNumber}' and masterAccountNum={masterAccountNum} and profileNum={profileNum} and TransType={(int)transType}";
            if (transNum.HasValue)
                sql += $" and TransNum={transNum.Value}";
            return (await dbFactory.FindAsync<InvoiceTransaction>(sql)).ToList();
        }

        public virtual async Task<List<InvoiceTransaction>> GetTransactionsByCheckNumOrAuthCode(int masterAccountNum, int profileNum, string checkNumOrAuthCode)
        {
            string query = $@"
SELECT RowNum FROM InvoiceTransaction
WHERE MasterAccountNum={masterAccountNum} 
    AND ProfileNum={profileNum}
    AND (CheckNum='{checkNumOrAuthCode}' OR AuthCode='{checkNumOrAuthCode}')
";
            
            var rowNums = dbFactory.Db.Query<int>(query).ToList();
            List<InvoiceTransaction> result = new List<InvoiceTransaction>();
            if (rowNums.Any())
            {
                rowNums.ForEach(r => {
                    var trans = Get(dbFactory, r);
                    result.Add(trans);
                });
            }

            return result;
        }

        public decimal OriginalPaidAmount { get; set; }

        public string CustomerCode { get; set; }

    }
}



