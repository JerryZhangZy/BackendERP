﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using Newtonsoft.Json;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class WMSOrderShipmentList
    {
        #region DataBase
        [JsonIgnore]
        protected IDataBaseFactory _dbFactory;

        [JsonIgnore]
        public IDataBaseFactory dbFactory
        {
            get
            {
                if (_dbFactory is null)
                    _dbFactory = DataBaseFactory.CreateDefault();
                return _dbFactory;
            }
        }

        public void SetDataBaseFactory(IDataBaseFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }
        #endregion DataBase
        public WMSOrderShipmentList(IDataBaseFactory dbFactory)
        {
            SetDataBaseFactory(dbFactory);
        }
        public virtual async Task<StringBuilder> GetWMSShipmentListAsync(int masterAccountNum, int profileNum, IList<string> shipmentIDs)
        {
            using (var trs = new ScopedTransaction(dbFactory))
            {
                return await EventProcessERPHelper.GetWMSOrderShipmentListAsync(masterAccountNum, profileNum, shipmentIDs);
            }

        }
    }
}
