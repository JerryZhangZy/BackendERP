              
    

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
    public partial class PoHeader
    {
        /// <summary>
        /// get PoHeader by PoNum
        /// </summary>
        /// <param name="poNum"></param>
        /// <returns></returns>
        public virtual async Task<PoHeader> GetByPoNumAsync(string poNum, int masterAccountNum, int profileNum)
        {
            return (await dbFactory.FindAsync<PoHeader>($"SELECT TOP 1 * FROM PoHeader where PoNum='{poNum}' and masterAccountNum={masterAccountNum} and profileNum={profileNum}")).FirstOrDefault();
        }
        
        /// <summary>
        /// get PoHeader by PoNum
        /// </summary>
        /// <param name="poNum"></param>
        /// <returns></returns>
        public virtual PoHeader GetByPoNum(string poNum, int masterAccountNum, int profileNum)
        {
            return (dbFactory.Find<PoHeader>($"SELECT TOP 1 * FROM PoHeader where PoNum='{poNum}' and masterAccountNum={masterAccountNum} and profileNum={profileNum}")).FirstOrDefault();
        }

    }
}



