using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DigitBridge.CommerceCentral.YoPoco
{
    internal static class AutoSelectHelper
    {
        private static Regex rxSelect = new Regex(@"\A\s*(SELECT|EXECUTE|CALL|WITH|SET|DECLARE)\s",
            RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Multiline);

        private static Regex rxFrom = new Regex(@"\A\s*FROM\s", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Multiline);

        public static string AddSelectClause<T>(IProvider provider, string sql, IMapper defaultMapper, IEnumerable<string> tableColumns = null)
        {
            if (sql.StartsWith(";"))
                return sql.Substring(1);

            if (!rxSelect.IsMatch(sql))
            {
                var pd = PocoData.ForType(typeof(T), defaultMapper);
                var tableName = provider.EscapeTableName(pd.TableInfo.TableName);
                string cols;
                if (tableColumns != null)
                {
                    cols = pd.Columns.Count != 0 ? string.Join(", ", (from c in pd.QueryColumns where tableColumns.Contains(c.ToLower(), StringComparer.OrdinalIgnoreCase) select tableName + "." + provider.EscapeSqlIdentifier(c)).ToArray()) : "NULL";
                }
                else
                {
                    cols = pd.Columns.Count != 0 ? string.Join(", ", (from c in pd.QueryColumns select tableName + "." + provider.EscapeSqlIdentifier(c)).ToArray()) : "NULL";
                }
                if (!rxFrom.IsMatch(sql))
                    sql = $"SELECT {cols} FROM {tableName} {sql}";
                else
                    sql = $"SELECT {cols} {sql}";
            }

            return sql;
        }
        public static string GetTableName<T>()
            => PocoData.ForType(typeof(T), null)?.TableInfo.TableName;

        public static string AddCountClause<T>(string sql)
        {
            if (sql.StartsWith(";"))
                return sql.Substring(1);

            if (rxSelect.IsMatch(sql)) return sql;

            var pd = PocoData.ForType(typeof(T), null);
            var tableName = pd.TableInfo.TableName;
            sql = !rxFrom.IsMatch(sql) ? $"SELECT COUNT(1) FROM {tableName} {sql}" : $"SELECT COUNT(1) {sql}";

            return sql;
        }

    }
}