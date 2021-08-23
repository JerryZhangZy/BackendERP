
              
    

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
    public partial class InvoiceReturnItems
    {
        public virtual async Task<List<InvoiceReturnItems>> GetReturnItems(List<string> transUuids)
        {
            if (transUuids == null || transUuids.Count == 0) return null;
            var sql = $"SELECT * FROM InvoiceReturnItems where TransUuid in ('{string.Join("','", transUuids)}') ";
            return (await dbFactory.FindAsync<InvoiceReturnItems>(sql)).ToList();
        }
    }
}



