using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    [Serializable]
    public class PoLineModel
    {
        public string PoItemUuid { get; set; }
        public string SKU { get; set; }
        public string Title { get; set; }
        public decimal PoPrice { get; set; }
        public int PoQty { get; set; }
        public int QtyForOther { get; set; }
        public DateTime? LineRequestShipDate { get; set; }
        public DateTime? LineArrivalDueDate { get; set; }
        public string LinePublicNote { get; set; }
        public string WarehouseCode { get; set; }
        //public string LinePrivateNote { get; set; }
        public int Sequence { get; set; }
        public int HumanReceiveQty { get; set; }
        //public int HumanAdjustQty { get; set; }
        public DateTime EnterDate { get; set; }
    }
}
