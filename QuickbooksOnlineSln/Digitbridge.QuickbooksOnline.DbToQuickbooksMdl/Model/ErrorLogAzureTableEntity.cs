using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Digitbridge.QuickbooksOnline.DbToQuickbooksMdl.Model
{
    public class ErrorLogAzureTableEntity : TableEntity
    {
        public ErrorLogAzureTableEntity() { }
        public ErrorLogAzureTableEntity(string rowKey, string partitionKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
            Timestamp = DateTime.Now;
        }
        // Customized Fields
        public string ErrorMsg { get; set; }
        public string ExceptionMsg { get; set; }
    }
}

