using System.Data.Common;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public class SqlSererMsDataDatabaseProvider : SqlServerDatabaseProvider
    {
        public override DbProviderFactory GetFactory()
            => GetFactory("Microsoft.Data.SqlClient.SqlClientFactory, Microsoft.Data.SqlClient");
    }
}