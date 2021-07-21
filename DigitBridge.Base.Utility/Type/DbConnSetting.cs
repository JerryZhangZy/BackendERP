using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.Base.Utility
{ 
    public class DbConnSetting
    {
        public string ConnString { get; set; }
        public bool UseAzureManagedIdentity { get; set; }
        public string TenantId { get; set; }

        public int DatabaseNum { get; set; }
    }


}
