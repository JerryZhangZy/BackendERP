using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    public class WMSPoReceiveItem
    {
        /// <summary>
        /// WMS batch num
        /// </summary>
        public string WMSBatchNum { get; set; }

        /// <summary>
        /// VendorCode
        /// </summary>
        public string VendorCode { get; set; }

        /// <summary>
        /// Po unique key erp provided.
        /// </summary>
        public string PoUuid { get; set; }

        /// <summary>
        /// Po item unique key erp provided.
        /// </summary>
        public string PoItemUuid { get; set; }

        /// <summary>
        /// WMS Received WarehouseCode
        /// </summary>
        public string WarehouseCode { get; set; }

        /// <summary>
        /// Item Sku
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// WMS Received item quantity
        /// </summary>
        public decimal Qty { get; set; }
    }
    public class WMSPoReceivePayload : ResponsePayloadBase
    {
        /// <summary>
        /// The uuid list of po item
        /// </summary>
        public List<string> PoItemUuidList { get; set; }

        /// <summary>
        /// the uuid of Po transaction
        /// </summary>
        public string TransUuid { get; set; }
    }

    #region Get wms PoReceive list 

    public class WmsPoReceiveListPayload : ResponsePayloadBase
    {
        public IList<WMSPoReceiveProcess> WMSPoReceiveProcessesList;
    }
    public class WMSPoReceiveProcess
    {
        /// <summary>
        /// The time of po transation tranferred from PoReceive.
        /// </summary>
        public DateTime? ProcessDate { get; set; }

        /// <summary>
        /// The unique number of the request wms batch num.
        /// </summary>
        public string WMSBatchNum { get; set; }

        /// <summary>
        /// Process status 
        /// </summary>
        public int ProcessStatus { get; set; }

        /// <summary>
        /// Process status  description
        /// </summary>
        public string ProcessStatusText { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string EventMessage { get; set; }

        /// <summary>
        /// LastUpdateDate
        /// </summary>
        public DateTime LastUpdateDate { get; set; }
        /// <summary>
        /// UpdateDateUtc
        /// </summary>
        public DateTime UpdateDateUtc { get; set; }
        /// <summary>
        /// EnterDateUtc: the date wms upload PoReceive.
        /// </summary>
        public DateTime EnterDateUtc { get; set; }
        public string EnterBy { get; set; }
        public string UpdateBy { get; set; }
    }
    #endregion
}
