    

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class PurchaseOrderData
    {

        public override void CheckIntegrityOthers()
        {
            foreach (var child in PoItemsRef.Where(x => x != null))
            {
                child.SetParent(this);
                if (child.DatabaseNum != PoHeader.DatabaseNum)
                    child.DatabaseNum = PoHeader.DatabaseNum;
                if (child.MasterAccountNum != PoHeader.MasterAccountNum)
                    child.MasterAccountNum = PoHeader.MasterAccountNum;
                if (child.ProfileNum != PoHeader.ProfileNum)
                    child.ProfileNum = PoHeader.ProfileNum;
            }

        }
    }
}



