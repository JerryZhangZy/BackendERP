using DigitBridge.Base.Common;
using DigitBridge.CommerceCentral.YoPoco;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventHelper = DigitBridge.CommerceCentral.ERPDb.EventProcessERPHelper;
using Microsoft.Data.SqlClient;
using System.Data;
using DigitBridge.Base.Utility;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    /// <summary>
    /// </summary>
    public partial class SqlQueryBuilderForEventProcess<TQueryObject> : SqlQueryBuilder<TQueryObject>
        where TQueryObject : IQueryObject, new()
    {
        public SqlQueryBuilderForEventProcess(IDataBaseFactory dbFactory) : base(dbFactory) { }
        public SqlQueryBuilderForEventProcess(IDataBaseFactory dbFactory, TQueryObject QueryObject) : base(dbFactory, QueryObject) { }


        #region Get lock event uuid 

        /// <summary>
        /// get command text to lock eventprocess
        /// </summary>
        /// <param name="eventTableAllies"></param>
        /// <returns></returns>
        protected virtual string GetCommandTextToLock()
        {
            GetSQL_all();
            if (!HasSQL)
                return string.Empty;

            var sb = new StringBuilder();
            sb.AppendLine($"SELECT {EventHelper.TableAllies}.EventUuid into #EventUuidList");
            sb.AppendLine(SQL_From);
            sb.AppendLine(SQL_Where);
            //TODO set this condition to other place. eg. initfilter.
            //sb.AppendLine($"And {EventHelper.TableAllies}.ActionStatus={(int)EventProcessActionStatusEnum.Default}");
            sb.AppendLine(SQL_OrderBy);
            if (!LoadAll)
                sb.AppendLine(SQL_Paging);
            return sb.ToString();
        }

        /// <summary>
        /// get command text for query locked event process
        /// </summary>
        /// <returns></returns>
        protected virtual string GetCommandTextForLocked()
        {
            var sb = new StringBuilder();
            sb.AppendLine(SQL_Select);
            sb.AppendLine($" FROM {EventHelper.TableName} {EventHelper.TableAllies}");
            sb.AppendLine(SQL_Where);
            return sb.ToString();
        }

        protected virtual async Task<List<string>> GetLockedEventProcessAsync(EventProcessTypeEnum eventProcessType)
        {
            var subQuery = GetCommandTextToLock();
            var parameters = GetSqlParameters(); 
            using (var tx = new ScopedTransaction(dbFactory))
            {
                var result = await EventProcessERPHelper.LockEventProcessForQueryAsync(eventProcessType, subQuery, parameters);
                tx.Commit();
                return result;
            }
        }
        protected virtual List<string> GetLockedEventProcess(EventProcessTypeEnum eventProcessType)
        {
            var subQuery = GetCommandTextToLock();
            var parameters = GetSqlParameters();
            using (var tx = new ScopedTransaction(dbFactory))
            {
                var result = EventProcessERPHelper.LockEventProcessForQuery(eventProcessType, subQuery, parameters);
                tx.Commit();
                return result;
            }

        }

        #endregion

        #region Query result from lock event uuid
        /// <summary>
        /// get command text for query result by locked event process
        /// </summary>
        /// <returns></returns>
        protected virtual string GetCommandTextForQueryByLocked()
        {
            var sb = new StringBuilder();
            sb.Append(SQL_Select);
            sb.Append(SQL_From);
            sb.Append($"JOIN @LockedEventUuid lockedEventUuid ON({ EventHelper.TableAllies}.EventUuid = lockedEventUuid.item)");
            if (this.LoadJson)
                sb.AppendLine(" FOR JSON PATH ");
            return sb.ToString();

        }

        protected bool ExcuteJsonForQueryByLocked(StringBuilder sb, EventProcessTypeEnum eventProcessType)
        {
            var sql = GetCommandTextForQueryByLocked();
            if (sql.IsZero())
                return false;

            var eventUuids = GetLockedEventProcess(eventProcessType);
            if (eventUuids == null || eventUuids.Count == 0)
            {
                AddInfo("No data locked.");
                return false;
            }

            using (var trs = new ScopedTransaction(dbFactory))
            {
                return SqlQuery.QueryJson(sb, sql, CommandType.Text, eventUuids.ToParameter<string>("@LockedEventUuid"));
            }
        }
        protected async Task<bool> ExcuteJsonForQueryByLockedAsync(StringBuilder sb, EventProcessTypeEnum eventProcessType)
        {
            var sql = GetCommandTextForQueryByLocked();
            if (sql.IsZero())
                return false;

            var eventUuids = await GetLockedEventProcessAsync(eventProcessType);
            if (eventUuids == null || eventUuids.Count == 0)
            {
                AddInfo("No data locked.");
                return false;
            }

            using (var trs = new ScopedTransaction(dbFactory))
            {
                return await SqlQuery.QueryJsonAsync(sb, sql, CommandType.Text, eventUuids.ToParameter<string>("@LockedEventUuid"));
            }
        }
        #endregion

    }
}
