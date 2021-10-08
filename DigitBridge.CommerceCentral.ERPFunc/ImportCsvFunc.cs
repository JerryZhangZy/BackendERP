using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace DigitBridge.CommerceCentral.ERPFunc
{
    public static class ImportCsvFunc
    {
        [FunctionName("ImportWarehouseCsvFunc")]
        public static void ImportWarehouseCsvFunc([BlobTrigger("importwarehouses/{name}", Connection = "")]Stream file, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {file.Length} Bytes");
        }
    }
}
