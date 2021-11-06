using System;
using System.Collections;
using System.Collections.Generic;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    public class PoHeader
    {
        public int CentralPoNum { get; set; }
        
        public int CentralDatabaseNum { get; set; }
        
        public string PoNum { get; set; }
        
        public string PoUuid { get; set; }
        
        public string VendorName { get; set; }
        
        public DateTime PoDate { get; set; }
        
        public DateTime CancelAfterDate { get; set; }
        
        public string Terms { get; set; }
        
        public string WarehouseCode { get; set; }
        
        public DateTime RequestShipDate { get; set; }
        
        public DateTime ArrivalDueDate { get; set; }
        
        public string PublicNote { get; set; }
        
        public string PrivateNote { get; set; }
        
        public IList<PoLine> PoLineList { get; set; }
    }

    public class PoLine
    {
        public string SKU { get; set; }
        
        public string Title { get; set; }
        public string PoUuid { get; set; }
        
        public string PoItemUuid { get; set; }
        
        public double PoPrice { get; set; }
        
        public int PoQty { get; set; }
        
        public int QtyForOther { get; set; }
        
        public DateTime LineRequestShipDate { get; set; }
        
        public DateTime LineArrivalDueDate { get; set; }
        
        public string LinePublicNote { get; set; }
        
        public string LinePrivateNote { get; set; }
        
        public  int Sequence { get; set; }
        
        public string OriginaLineId { get; set; }
        
        public int HumanReceiveQty { get; set; }
        
        public int HumanAdjustQty { get; set; }
        
        public DateTime EnterDate { get; set; }
    }
}