using DigitBridge.Base.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPEventSDK
{
    public class AddErpEventDto
    {
        public int MasterAccountNum { get; set; }

        public int ProfileNum { get; set; }

        public string ProcessUuid { get; set; }
                
        //public string ProcessData { get; set; }

        //public string ProcessSource { get; set; }

    }

    public class UpdateErpEventDto
    {
        public int MasterAccountNum { get; set; }

        public int ProfileNum { get; set; }

        public string EventUuid { get; set; }

        public int ActionStatus { get; set; }

        public string EventMessage { get; set; }
    }

}
