
              
    

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class InventoryLog
    {
        public int GetBatchNum()
        {
            return dbFactory.Db.ExecuteScalar<int>("select max(BatchNum) from InventoryLog") + 1;;
        }

        public Inventory GetInventory(int rowNum)
        {
            return dbFactory.Get<Inventory>(rowNum);
        }

        public Inventory GetInventoryBySku(int databaseNum,int masterAccountNum,int profileNum)
        {
            var sql = new Sql($"SELECT Top 1 * FROM Inventory WHERE DatabaseNum={databaseNum} AND MasterAccountNum={masterAccountNum} AND ProfileNum={profileNum}");
            return dbFactory.Db.FirstOrDefault<Inventory>(sql);
        }

        public IList<InventoryLog> GetInventoryLogByLogUuid(string uuid)
        {
            var sql = new Sql($"SELECT * FROM InventoryLog WHERE LogUuid='{uuid}'");
            return dbFactory.Db.Query<InventoryLog>(sql).ToList();;
        }

        public IList<InventoryLog> GetInventoryLogByBatchNum(int batchNum)
        {
            var sql = new Sql($"SELECT * FROM InventoryLog WHERE BatchNum={batchNum}");
            return dbFactory.Db.Query<InventoryLog>(sql).ToList();;
        }

        public bool AddInventoryLogList(List<InventoryLog> logList)
        {
            return logList.Save();
        }
        public bool UpdateInventoryLogList(IList<InventoryLog> logList)
        {
            return logList.Save();
        }

        public  int DeleteInventoryLogByLogUuid(string logUuid)
        {
            var sql = new Sql($"DELETE FROM InventoryLog WHERE LogUuid='{logUuid}'");
            return dbFactory.Db.Execute(sql);
        }
    }
}



