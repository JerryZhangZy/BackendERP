              
    

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
using DigitBridge.Base.Common;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class EventProcessERP
    {
        public EventProcessActionStatusEnum ActionStatusEnum 
        {
            get => ActionStatus.ToEnum<EventProcessActionStatusEnum>(EventProcessActionStatusEnum.Pending);
            set => ActionStatus = (int)value;
        }

        public EventProcessProcessStatusEnum ProcessStatusEnum
        {
            get => ProcessStatus.ToEnum<EventProcessProcessStatusEnum>(EventProcessProcessStatusEnum.Pending);
            set => ProcessStatus = (int)value;
        }

        public EventCloseStatusEnum CloseStatusEnum
        {
            get => CloseStatus.ToEnum<EventCloseStatusEnum>(EventCloseStatusEnum.Open);
            set => CloseStatus = (int)value;
        }

    }
}



