using System.Collections.Generic;
using System.Xml.Serialization;
using DigitBridge.Base.Common;
using Newtonsoft.Json;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    public class PoTransactionDataDto
    {
        public PoTransactionDataDto(){}

        public PoTransactionDataDto(PoHeader header)
        {
            PoTransaction = new PoTransactionDto()
            {
                PoNum = header.PoNum,
                PoUuid = header.PoUuid,
                VendorName = header.VendorName
            };
            PoTransactionItems = new List<PoTransactionItemsDto>();
            foreach (var line in header.PoLineList)
            {
                var item = new PoTransactionItemsDto()
                {
                    PoUuid = line.PoUuid,
                    PoItemUuid = line.PoItemUuid,
                    Seq = line.Sequence,
                    SKU = line.SKU,
                    TransQty = line.PoQty
                };
                PoTransactionItems.Add(item);
            }
        }
        
        public PoTransactionDto PoTransaction { get; set; }

        public IList<PoTransactionItemsDto> PoTransactionItems { get; set; }
    }

    public class PoTransactionDto
    {
        public string PoUuid { get; set; }
        
        public string PoNum { get; set; }
        
        public string VendorName { get; set; }

        public int TransStatus { get; set; } = (int)PoTransStatus.StockReceive;
    }

    public class PoTransactionItemsDto
    {
        public string PoUuid { get; set; }
        
        public string PoItemUuid { get; set; }
        
        public string SKU { get; set; }
        
        public int Seq { get; set; }
        
        public int TransQty { get; set; } 
    }
}