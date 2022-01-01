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
using DigitBridge.CommerceCentral.ERPDb;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPMdl.selectList.customer
{
    [Serializable]
    public class SelectListDataItem
    {
        public string value;
        public string text;
        public long count;
    }
    public class system_systemCodeBase : SelectListBase
    {
        string SystemCodeName;
        public system_systemCodeBase(IDataBaseFactory dbFactory, string systemCodeName) : base(dbFactory)
        {
            SystemCodeName = systemCodeName;
        }
        public override string Name => "system_" + SystemCodeName;
        public override async Task<bool> GetSelectListAsync(SelectListPayload payload)
        {
            var systemCodes = await dbFactory.GetByAsync<SystemCodes>(
                $"SELECT * FROM SystemCodes WHERE MasterAccountNum={payload.MasterAccountNum} AND ProfileNum={payload.ProfileNum} AND SystemCodeName='{SystemCodeName}'");
            if (systemCodes != null)
            {
                var data = new List<SelectListDataItem>();
                foreach (var pair in ((List<Dictionary<string, object>>)systemCodes.Fields.Values["data"]))
                    if (pair["code"].ToString().StartsWith(payload.Term, StringComparison.CurrentCultureIgnoreCase))
                        data.Add(new SelectListDataItem()
                        {
                            value = pair["code"].ToString(),
                            text = pair["description"].ToString(),
                            count = 1
                        });
                payload.Data = new StringBuilder(JsonConvert.SerializeObject(data));
                payload.Success = true;
                return true;
            }
            payload.Success = false;
            return false;
        }
    }
    public class system_ColorPatternCode : system_systemCodeBase
    {
        public system_ColorPatternCode(IDataBaseFactory dbFactory) : base(dbFactory, "ColorPatternCode") { }
    }
    public class system_SizeType : system_systemCodeBase { public system_SizeType(IDataBaseFactory dbFactory) : base(dbFactory, "SizeType") { } }
    public class system_SizeCode : system_systemCodeBase { public system_SizeCode(IDataBaseFactory dbFactory) : base(dbFactory, "SizeCode") { } }
    public class system_WidthCode : system_systemCodeBase { public system_WidthCode(IDataBaseFactory dbFactory) : base(dbFactory, "WidthCode") { } }
    public class system_LengthCode : system_systemCodeBase { public system_LengthCode(IDataBaseFactory dbFactory) : base(dbFactory, "LengthCode") { } }
    public class system_ClassCode : system_systemCodeBase { public system_ClassCode(IDataBaseFactory dbFactory) : base(dbFactory, "ClassCode") { } }
    public class system_SubClassCode : system_systemCodeBase { public system_SubClassCode(IDataBaseFactory dbFactory) : base(dbFactory, "SubClassCode") { } }
    public class system_DivisionCode : system_systemCodeBase { public system_DivisionCode(IDataBaseFactory dbFactory) : base(dbFactory, "DivisionCode") { } }
    public class system_DepartmentCode : system_systemCodeBase { public system_DepartmentCode(IDataBaseFactory dbFactory) : base(dbFactory, "DepartmentCode") { } }
    public class system_GroupCode : system_systemCodeBase { public system_GroupCode(IDataBaseFactory dbFactory) : base(dbFactory, "GroupCode") { } }
    public class system_SubGroupCode : system_systemCodeBase { public system_SubGroupCode(IDataBaseFactory dbFactory) : base(dbFactory, "SubGroupCode") { } }
    public class system_CategoryCode : system_systemCodeBase { public system_CategoryCode(IDataBaseFactory dbFactory) : base(dbFactory, "CategoryCode") { } }
    public class system_Model : system_systemCodeBase { public system_Model(IDataBaseFactory dbFactory) : base(dbFactory, "Model") { } }
    public class system_UOM : system_systemCodeBase { public system_UOM(IDataBaseFactory dbFactory) : base(dbFactory, "UOM") { } }
    public class system_Terms : system_systemCodeBase { public system_Terms(IDataBaseFactory dbFactory) : base(dbFactory, "Terms") { } }
    public class system_BusinessType : system_systemCodeBase { public system_BusinessType(IDataBaseFactory dbFactory) : base(dbFactory, "BusinessType") { } }
}
