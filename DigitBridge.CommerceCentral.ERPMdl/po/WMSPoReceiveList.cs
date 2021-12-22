using System;
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
    public class WMSPoReceiveList
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
        public WMSPoReceiveList(IDataBaseFactory dbFactory)
        {
            SetDataBaseFactory(dbFactory);
        }
        public virtual async Task<WMSPoReceiveListPayload> GetWMSPoReceiveListAsync(int masterAccountNum, int profileNum, IList<string> wmsBatchNums)
        {
            var payload = new WMSPoReceiveListPayload();
            try
            {
                using (var trs = new ScopedTransaction(dbFactory))
                {
                    (payload.Success, payload.WMSPoReceiveProcessesList) = await EventProcessERPHelper.GetWMSPoReceiveListAsync(masterAccountNum, profileNum, wmsBatchNums);
                }
            }
            catch (Exception e)
            {
                payload.Messages.AddError(e.Message);
            }
            return payload;
        }
    }
}
