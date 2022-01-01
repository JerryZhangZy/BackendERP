//-------------------------------------------------------------------------
// This document is generated by T4
// It will overwrite your changes, please keep it as it is
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
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
    /// Represents a PurchaseOrderService.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public interface IPurchaseOrderManager 
    {
        Task<byte[]> ExportAsync(PurchaseOrderPayload payload);

        byte[] Export(PurchaseOrderPayload payload);

        void Import(PurchaseOrderPayload payload, IFormFileCollection files);

        Task ImportAsync(PurchaseOrderPayload payload, IFormFileCollection files);
    }
}
