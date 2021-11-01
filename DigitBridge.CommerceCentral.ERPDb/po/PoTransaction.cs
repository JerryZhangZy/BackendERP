              
    

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
    public partial class PoTransaction
    {

        /// <summary>
        ///  Get poTransaction list by poNum
        /// </summary>
        /// <param name="poNum"></param>
        /// <param name="masterAccountNum"></param>
        /// <param name="profileNum"></param>
        /// <param name="transNum"></param>
        /// <returns></returns>
        public virtual async Task<List<PoTransaction>> GetByPoNumberAsync(string poNum, int masterAccountNum, int profileNum,int? transNum = null)
        {
            var sql = $"SELECT * FROM PoTransaction where PoNum='{poNum}' and masterAccountNum={masterAccountNum} and profileNum={profileNum}";
            if (transNum.HasValue)
                sql += $" and TransNum={transNum.Value}";
            return (await dbFactory.FindAsync<PoTransaction>(sql)).ToList();
        }
    }
}



