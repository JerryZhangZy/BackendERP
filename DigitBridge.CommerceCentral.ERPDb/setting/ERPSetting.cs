using System;
using System.ComponentModel.DataAnnotations;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Represents a ERP setting Class.
    /// </summary>
    public class ERPSetting
    {
        public bool TaxForShippingAndHandling { get; set; } = false;
        public string UniqueId { get; set; }
        public DateTime? EnterDateUtc { get; set; }
        public Guid DigitBridgeGuid { get; set; }

    }
}



