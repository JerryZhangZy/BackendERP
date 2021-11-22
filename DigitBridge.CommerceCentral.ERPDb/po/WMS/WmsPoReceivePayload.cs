

//-------------------------------------------------------------------------
// This document is generated by T4
// It will overwrite your changes, please keep it as it is
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DigitBridge.Base.Utility;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Response payload object
    /// </summary>
    [Serializable()]
    public class WmsPoReceivePayload : ResponsePayloadBase
    {
        public WmsPoReceivePayload(bool success = false)
        {
            this.Success = success;
        }
        /// <summary>
        /// The uuid of po Purchase order
        /// </summary>
        public string PoUuid { get; set; }

        /// <summary>
        /// the uuid of Po transaction
        /// </summary>
        public string TransUuid { get; set; }
    }
}

