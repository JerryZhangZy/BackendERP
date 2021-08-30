using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPMdl.Model
{
   
	public class CentralEventErpType
	{
		public int DatabaseNum { get; set; }
		public int MasterAccountNum { get; set; }
		public int ProfileNum { get; set; }
		public int ChannelNum { get; set; }
		public int ChannelAccountNum { get; set; }
		public int ErpEventType { get; set; }
		public string ProcessSource { get; set; }
		public string ProcessUuid { get; set; }
		public string ProcessData = "";
		public string EventMessage = "";
	}
}
