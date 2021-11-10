using DigitBridge.Base.Common;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DigitBridge.CommerceCentral.ERPDb
{
    ///// <summary>
    ///// Fault event proceess.
    ///// </summary>
    //public class EventProcessRequestPayload
    //{
    //    /// <summary>
    //    /// EventUuid
    //    /// </summary>
    //    public string EventUuid { get; set; }

    //    /// <summary>
    //    /// Handle result,Default is false.
    //    /// </summary>
    //    public bool Success { get; set; } = false;

    //    /// <summary>
    //    /// Fault message.
    //    /// </summary>
    //    public StringBuilder Message { get; set; }

    //}
    /// <summary>
    /// Fault event proceess.
    /// </summary>
    public class EventProcessResponsePayload : ResponsePayloadBase
    {
        //put more response here.
    }


    /// <summary>
    /// Fault event proceess.
    /// </summary>
    public class AcknowledgePayload: PayloadBase
    {
        /// <summary>
        /// (Request Body Data) List of received EventUuid.
        /// </summary>
        [OpenApiPropertyDescription("(Request Body Data) List of EventUuid which have been received.")]
        public IList<string> ProcessUuids { get; set; }
        [JsonIgnore] public virtual bool HasProcessUuids => ProcessUuids != null && ProcessUuids.Count > 0;

        /// <summary>
        /// EventProcessType 
        /// </summary>
        [JsonIgnore] 
        public EventProcessTypeEnum EventProcessType { get; set; }
    }

    public class ProcessResult
    {
        public string ProcessUuid { get; set; }
        public int ProcessStatus { get; set; }
        public JObject ProcessData { get; set; }
    }

    /// <summary>
    /// Fault event proceess.
    /// </summary>
    public class AcknowledgeProcessPayload : PayloadBase
    {
        /// <summary>
        /// (Request Body Data) List of received EventUuid.
        /// </summary>
        [OpenApiPropertyDescription("(Request Body Data) List of EventUuid which have been received.")]
        public IList<ProcessResult> ProcessResults { get; set; }
        [JsonIgnore] public virtual bool HasProcessResults => ProcessResults != null && ProcessResults.Count > 0;

        /// <summary>
        /// EventProcessType 
        /// </summary>
        [JsonIgnore]
        public EventProcessTypeEnum EventProcessType { get; set; }
    }

}
