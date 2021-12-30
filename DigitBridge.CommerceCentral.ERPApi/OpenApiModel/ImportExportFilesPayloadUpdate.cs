using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPApi
{
    [Serializable()]
    public class ImportExportFilesPayloadUpdate
    {
        [OpenApiPropertyDescription("(Request) export uuid.")]
        public List<string> ExportUuid { get; set; }
    }
}
