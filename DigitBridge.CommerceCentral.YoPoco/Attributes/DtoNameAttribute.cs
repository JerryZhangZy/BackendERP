using System;
using System.Data;

namespace DigitBridge.CommerceCentral.YoPoco
{
    /// <summary>
    ///     Represents an attribute which can decorate a POCO property to mark the property as a column. It may also optionally
    ///     supply the DB column name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DtoNameAttribute : Attribute
    {
        /// <summary>
        ///     The SQL name of the column
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Constructs a new instance of the <seealso cref="DtoNameAttribute" />.
        /// </summary>
        public DtoNameAttribute()
        {
        }

        /// <summary>
        ///     Constructs a new instance of the <seealso cref="DtoNameAttribute" />.
        /// </summary>
        /// <param name="name">The name of the column.</param>
        public DtoNameAttribute(string name)
        {
            Name = name;
        }

    }
}