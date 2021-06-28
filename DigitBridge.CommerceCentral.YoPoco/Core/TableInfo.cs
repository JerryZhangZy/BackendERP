using System;
using System.Linq;
using System.Reflection;

namespace DigitBridge.CommerceCentral.YoPoco
{
    /// <summary>
    ///     Use by IMapper to override table bindings for an object
    /// </summary>
    public class TableInfo
    {
        /// <summary>
        ///     The database table name
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        ///     The name of the primary key column of the table
        /// </summary>
        public string PrimaryKey { get; set; }

        /// <summary>
        ///     True if the primary key column is an auto-incrementing
        /// </summary>
        public bool AutoIncrement { get; set; }

        /// <summary>
        ///     The name of the sequence used for auto-incrementing Oracle primary key fields
        /// </summary>
        public string SequenceName { get; set; }

        /// <summary>
        ///     The name of the Unique Id column of the table
        /// </summary>
        public string UniqueId { get; set; }

        /// <summary>
        ///     Creates and populates a TableInfo from the attributes of a POCO
        /// </summary>
        /// <param name="t">The POCO type</param>
        /// <returns>A TableInfo instance</returns>
        public static TableInfo FromPoco(Type t)
        {
            var ti = new TableInfo();
            PopulateTableNameFromPoco(t, ref ti, out _);
            PopulatePrimaryKeyFromPoco(t, ref ti, out _, out _);
            return ti;
        }


        internal static void PopulateTableNameFromPoco(Type t, ref TableInfo ti, out TableNameAttribute tblAttr)
        {
            ti ??= new TableInfo();
            tblAttr = t.GetCustomAttributes(typeof(TableNameAttribute), true).FirstOrDefault() as TableNameAttribute;
            ti.TableName = tblAttr?.Value ?? t.Name;
        }

        internal static void PopulatePrimaryKeyFromPoco(Type t, ref TableInfo ti, out PrimaryKeyAttribute pkAttr, out PropertyInfo idProp)
        {
            ti ??= new TableInfo();
            pkAttr = t.GetCustomAttributes(typeof(PrimaryKeyAttribute), true).FirstOrDefault() as PrimaryKeyAttribute;
            idProp = null;

            ti.PrimaryKey = pkAttr?.Value;
            ti.SequenceName = pkAttr?.SequenceName;
            ti.AutoIncrement = pkAttr?.AutoIncrement ?? false;

            #region PrimaryKey should declare explicitly
            //if (String.IsNullOrWhiteSpace(ti.PrimaryKey))
            //{
            //    bool isIdProp(PropertyInfo p)
            //    {
            //        bool hasName(string name) => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase);
            //        return hasName("id")
            //            || hasName(t.Name + "id")
            //            || hasName(t.Name + "_id");
            //    }

            //    idProp = t.GetProperties().FirstOrDefault(isIdProp) as PropertyInfo;
            //    if (idProp != null)
            //    {
            //        ti.PrimaryKey = idProp.Name;
            //        ti.AutoIncrement = idProp.PropertyType.IsValueType;
            //    }
            //}
            #endregion PrimaryKey should declare explicitly
        }
        internal static void PopulateUniqueIdFromPoco(Type t, ref TableInfo ti, out UniqueIdAttribute pkAttr)
        {
            ti ??= new TableInfo();
            pkAttr = t.GetCustomAttributes(typeof(UniqueIdAttribute), true).FirstOrDefault() as UniqueIdAttribute;
            ti.UniqueId = pkAttr?.Value;
        }
    }
}