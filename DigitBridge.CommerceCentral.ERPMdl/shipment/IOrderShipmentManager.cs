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
    /// Represents a OrderShipmentService.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public interface IOrderShipmentManager
    {
        Task<byte[]> ExportAsync(OrderShipmentPayload payload);

        byte[] Export(OrderShipmentPayload payload);

        void Import(OrderShipmentPayload payload, IFormFileCollection files);

        Task ImportAsync(OrderShipmentPayload payload, IFormFileCollection files);

        Task<bool> CreateShipment(OrderShipmentPayload payload);
    }
}
