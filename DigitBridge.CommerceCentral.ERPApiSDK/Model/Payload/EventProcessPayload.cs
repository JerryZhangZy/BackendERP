using DigitBridge.Base.Common;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace DigitBridge.CommerceCentral.ERPApiSDK
{
    /// <summary>
    /// Fault event proceess.
    /// </summary>
    public class AcknowledgePayload : ResponsePayloadBase
    {
        public IList<string> ProcessUuids { get; set; }
    }

    public class ProcessResult
    {
        public string ProcessUuid { get; set; }
        /// <summary>
        /// EventProcessProcessStatusEnum
        /// </summary>
        public EventProcessProcessStatusEnum ProcessStatus { get; set; }
        public JObject ProcessData { get; set; }

        public JObject EventMessage { get; set; }
    }

    /// <summary>
    /// Fault event proceess.
    /// </summary>
    public class AcknowledgeProcessPayload : ResponsePayloadBase
    {
        public IList<ProcessResult> ProcessResults { get; set; }

    }

}
