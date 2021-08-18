using DigitBridge.CommerceCentral.YoPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPMdl.Manager
{
    public class SingleMessageData : StructureRepository<SingleMessageData>
    {
		public long RowNum { get; set; }
		public int DatabaseNum { get; set; }
		public int MasterAccountNum { get; set; }
		public int ProfileNum { get; set; }
		public int ChannelNum { get; set; }
		public int ChannelAccountNum { get; set; }
		public int ErpEventType { get; set; }
		public string ProcessSource { get; set; }
		public string ProcessUuid { get; set; }
		public int ActionStatus = 0;
		public DateTime ActionDateUtc = DateTime.UtcNow;
		public string EventMessage = "";
	}

	public class MessageData : StructureRepository<MessageData>
	{
        public List<SingleMessageData> Messages { get; set; }
    }
}
