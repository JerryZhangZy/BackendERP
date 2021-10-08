using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.AzureStorage
{
    public interface IAzureEntity<T>
    {
        IDictionary<string, object> GetAzureData();

        T ToEntity(TableEntity entity);
    }
}
