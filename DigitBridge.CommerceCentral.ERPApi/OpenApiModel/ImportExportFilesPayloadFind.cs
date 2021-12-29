using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPApi
{
    [Serializable()]
    public class ImportExportFilesPayloadFind
    {
        [OpenApiPropertyDescription("(Response) list of export uuids.")]
        public List<string> ExportUuids { get; set; }
    }
}
