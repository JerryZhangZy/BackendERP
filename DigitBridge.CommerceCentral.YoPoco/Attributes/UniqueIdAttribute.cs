using System;

namespace DigitBridge.CommerceCentral.YoPoco
{
    /// <summary>
    ///     Is an attribute, which when applied to a POCO class, specifies unique key column. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class UniqueIdAttribute : Attribute
    {
        /// <summary>
        ///     The column name.
        /// </summary>
        /// <returns>
        ///     The column name.
        /// </returns>
        public string Value { get; }

        /// <summary>
        ///     Constructs a new instance of the <seealso cref="UniqueIdAttribute" />.
        /// </summary>
        /// <param name="UniqueId">The name of the primary key column.</param>
        public UniqueIdAttribute(string name)
            => Value = name;
    }
}