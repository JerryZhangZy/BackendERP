using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.QuickBooks.Integration
{
    public static class QboUtil
    {
        public static string ToCustomField(this string fieldValue)
        {
            if (string.IsNullOrEmpty(fieldValue))
                return string.Empty;
            if (fieldValue.Length > QboMappingConsts.CustomFieldMaxLength)
                return fieldValue.Substring(0, QboMappingConsts.CustomFieldMaxLength);
            return fieldValue;
        }
        public static string ToShipMethodRef(this string val)
        {
            if (string.IsNullOrEmpty(val))
                return string.Empty;
            if (val.Length > QboMappingConsts.ShipMethodRefMaxLength)
                return val.Substring(0, QboMappingConsts.ShipMethodRefMaxLength);
            return val;
        }
    }
}
