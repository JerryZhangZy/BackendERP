
              
    

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class CustomerAddress
    {
        [IgnoreCompare]
        public override bool IsEmpty => (
            string.IsNullOrWhiteSpace(AddressCode) && 
            string.IsNullOrWhiteSpace(AddressLine1) && 
            string.IsNullOrWhiteSpace(PostalCode)
        );

    }
}



