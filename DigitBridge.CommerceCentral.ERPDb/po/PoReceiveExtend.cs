using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// This is a internal class
    /// </summary>
    public class PoReceiveExtend
    {
        /// <summary>
        /// PoNum
        /// </summary> 
        public string PoNum { get; set; }
        /// <summary>
        /// PoUuid
        /// </summary> 
        public string PoUuid { get; set; }
        /// <summary>
        /// VendorCode
        /// </summary> 
        public string VendorCode { get; set; }
        /// <summary>
        /// VendorName
        /// </summary> 
        public string VendorName { get; set; }
        /// <summary>
        /// VendorUuid
        /// </summary> 
        public string VendorUuid { get; set; }
        /// <summary>
        /// Po item unique key erp provided.
        /// </summary>
        public string PoItemUuid { get; set; }
    }
}
