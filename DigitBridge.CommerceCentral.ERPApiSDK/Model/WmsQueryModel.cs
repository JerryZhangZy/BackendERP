using System;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    public class WmsQueryModel
    {
        public int MasterAccountNum { get; set; }
        
        public int ProfileNum { get; set; }

        public DateTime UpdateDateFrom { get; set; } = DateTime.Now.AddDays(-1);
        
        public DateTime UpdateDateTo { get; set; }=DateTime.Now;
    }
}