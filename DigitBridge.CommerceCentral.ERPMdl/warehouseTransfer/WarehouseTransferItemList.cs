using DigitBridge.Base.Common;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper = DigitBridge.CommerceCentral.ERPDb.WarehouseTransferHeaderHelper;
using ItemsHelper = DigitBridge.CommerceCentral.ERPDb.WarehouseTransferItemsHelper;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class WarehouseTransferItemList : SqlQueryBuilder<WarehouseTransferItemQuery>
    {
        public WarehouseTransferItemList(IDataBaseFactory dbFactory)
            : base(dbFactory)
        { }

        public WarehouseTransferItemList(IDataBaseFactory dbFactory, WarehouseTransferItemQuery query)
            : base(dbFactory, query)
        { }

        protected override string GetSQL_select()
        {
            this.SQL_Select = $@"
SELECT 
{Helper.TransferDate()},
{Helper.TransferTime()},
{Helper.ReceiveDate()},
{Helper.ReceiveTime()},
{Helper.WarehouseTransferStatus()},
{Helper.BatchNumber()},
{ItemsHelper.SKU()},
{ItemsHelper.FromWarehouseUuid()},
{ItemsHelper.FromWarehouseCode()},
{ItemsHelper.ToWarehouseUuid()},
{ItemsHelper.ToWarehouseCode()},
{Helper.InTransitToWarehouseCode()},
{ItemsHelper.TransferQty()}
";
            return this.SQL_Select;
        }

        protected override string GetSQL_from()
        {
            this.SQL_From = $@"
 FROM { Helper.TableName} { Helper.TableAllies}
            RIGHT JOIN { ItemsHelper.TableName}
            { ItemsHelper.TableAllies}
            ON({ ItemsHelper.TableAllies}.WarehouseTransferUuid = { Helper.TableAllies}.WarehouseTransferUuid)
 LEFT JOIN @UpdateType iut ON({ Helper.TableAllies}.WarehouseTransferType = iut.num)
";
            return this.SQL_From;
        }

        public override SqlParameter[] GetSqlParameters()
        {
            var paramList = base.GetSqlParameters().ToList();
            paramList.Add("@UpdateType".ToEnumParameter<InventoryUpdateType>());
            return paramList.ToArray();
        }

        public virtual WarehouseTransferItemPayload GetWarehouseTransferItemList(WarehouseTransferItemPayload payload)
        {
            if (payload == null)
                payload = new WarehouseTransferItemPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                payload.WarehouseTransferItemsCount = Count();
                result = ExcuteJson(sb);
                if (result)
                    payload.WarehouseTransferItemList = sb;
            }
            catch (Exception ex)
            {
                payload.WarehouseTransferItemsCount = 0;
                payload.WarehouseTransferItemList = null;
                return payload;
                throw;
            }
            return payload;
        }

        public virtual async Task<WarehouseTransferItemPayload> GetWarehouseTransferItemListAsync(WarehouseTransferItemPayload payload)
        {
            if (payload == null)
                payload = new WarehouseTransferItemPayload();

            this.LoadRequestParameter(payload);
            StringBuilder sb = new StringBuilder();
            var result = false;
            try
            {
                payload.WarehouseTransferItemsCount = await CountAsync();
                result = await ExcuteJsonAsync(sb);
                if (result)
                    payload.WarehouseTransferItemList = sb;
            }
            catch (Exception ex)
            {
                payload.WarehouseTransferItemsCount = 0;
                payload.WarehouseTransferItemList = null;
                return payload;
                throw;
            }
            return payload;
        }
    }
}
