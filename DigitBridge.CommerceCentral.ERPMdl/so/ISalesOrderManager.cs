using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;
using Microsoft.AspNetCore.Http;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    /// <summary>
    /// Represents a SalesOrderService.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public interface ISalesOrderManager
    {
        Task<byte[]> ExportAsync(SalesOrderPayload payload);
        byte[] Export(SalesOrderPayload payload);
        void Import(SalesOrderPayload payload, IFormFileCollection files);
        Task ImportAsync(SalesOrderPayload payload, IFormFileCollection files);
        Task<bool> CreateSalesOrderByChannelOrderIdAsync(string centralOrderUuid);
        Task<SalesOrderData> CreateSalesOrdersAsync(ChannelOrderData coData, DCAssignmentData dcAssigmentData);
    }
}



