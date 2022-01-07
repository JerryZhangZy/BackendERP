using DigitBridge.Base.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public class AddEventDto
    {
        public int MasterAccountNum { get; set; }

        public int ProfileNum { get; set; }

        public string ProcessUuid { get; set; }
                
        //public string ProcessData { get; set; }

        //public string ProcessSource { get; set; }

    }

    public class UpdateEventDto
    {
        public int MasterAccountNum { get; set; }

        public int ProfileNum { get; set; }

        public string EventUuid { get; set; }

        public int ActionStatus { get; set; }

        public string EventMessage { get; set; }
    }

    public static class EventDtoExtension
    {        
        public static EventERPDataDto ToEventERPDataDto(this AddEventDto eventdto,ErpEventType eventType)
        {
            return new EventERPDataDto
            {
                Event_ERP = new Event_ERPDto
                {
                    ActionDateUtc=DateTime.UtcNow,
                    ActionStatus=int.MaxValue,
                    ProcessUuid = eventdto.ProcessUuid,
                    ERPEventType=(int)eventType
                }
            };
        }

        public static EventERPDataDto ToEventERPDataDto(this UpdateEventDto eventdto)
        {
            return new EventERPDataDto
            {
                Event_ERP = new Event_ERPDto
                {
                    EventUuid = eventdto.EventUuid,
                    ActionStatus=eventdto.ActionStatus,
                    EventMessage=eventdto.EventMessage,
                    UpdateDateUtc=DateTime.UtcNow,
                    ActionDateUtc=DateTime.UtcNow
                }
            };
        }
    }
}
