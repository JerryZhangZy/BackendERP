using DigitBridge.CommerceCentral.ERPDb;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPApi
{
    public class ExportSalesOrderPayload : FilterPayloadBase<SalesOrderFilter>
    {
        public ImportExportOptions Options { get; set; }
    }
}
