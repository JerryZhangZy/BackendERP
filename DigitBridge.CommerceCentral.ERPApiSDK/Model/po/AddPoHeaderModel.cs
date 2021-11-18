using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    [Serializable]
    public class AddPoHeaderModel
    {
        public string PoUuid { get; set; }
        public int DatabaseNum { get; set; }
        public string PoNumber { get; set; }
        public string VendorName { get; set; }
        public string VendorCode { get; set; }
        public DateTime PoDate { get; set; }
        public DateTime? CancelAfterDate { get; set; }
        public string Terms { get; set; }
        public string WarehousCode { get; set; }
        public DateTime? RequestShipDate { get; set; }
        public DateTime? ArrivalDueDate { get; set; }
        public string PublicNote { get; set; }
        public string PrivateNote { get; set; }
        public List<PoLineModel> PoLineList { get; set; }
    }
}
