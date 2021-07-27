using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace DigitBridge.CommerceCentral.ERPDb
{
    /// <summary>
    /// Request paging information
    /// </summary>
    [Serializable()]
    public class InventoryLogPayload : PayloadBase
    {
        public IList<string> LogUuids { get; set; } = new List<string>();
        [JsonIgnore] 
        public virtual bool HasLogUuids => LogUuids != null && LogUuids.Count > 0;
        public bool ShouldSerializeLogUuids() => HasLogUuids;

        public IList<InventoryLogDto> InventoryLogs { get; set; } = new List<InventoryLogDto>();
        [JsonIgnore] public virtual bool HasInventoryLogs => InventoryLogs != null && InventoryLogs.Count > 0;
        public bool ShouldSerializeInventoryLogs() => HasInventoryLogs;


        public override IDictionary<string, Action<string>> GetOtherParameters()
        {
            return new Dictionary<string, Action<string>>
            {
                { "logUuids", val => LogUuids = val.Split(",").ToList() }
            };
        }

    }
}
