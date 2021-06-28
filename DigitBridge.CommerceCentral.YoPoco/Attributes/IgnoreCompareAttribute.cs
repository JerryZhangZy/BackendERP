using System;

namespace DigitBridge.CommerceCentral.YoPoco
{
    /// <summary>
    ///     Represents an attribute which can decorate a POCO property to ensure PetaPoco does not map column, and therefore
    ///     ignores the column.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnoreCompareAttribute : Attribute
    {
    }
}