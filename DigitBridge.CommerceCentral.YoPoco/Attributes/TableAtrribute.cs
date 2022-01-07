using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.YoPoco
{
    /// <summary>
    /// 表属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class TableAttribute : Attribute
    {
        private string _name;
        private string _schema;

        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get => _name; }
        /// <summary>
        /// 架构名
        /// </summary>
        public string Schema { get => _schema; }

        public TableAttribute(string name, string schema = "dbo")
        {
            _name = name;
            _schema = schema;
        }
    }
}
