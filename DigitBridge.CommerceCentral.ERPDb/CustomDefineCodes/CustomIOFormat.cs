              
    

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
using DigitBridge.Base.Common;

namespace DigitBridge.CommerceCentral.ERPDb
{
    public partial class CustomIOFormat
    {
        public CsvFormat GetFormatObject()
        {
            CsvFormat fmt = null;
            if (FormatType.EqualsIgnoreSpace(ActivityLogType.SalesOrder.ToString()))
            {
                fmt = new SalesOrderIOFormat();
                if (!string.IsNullOrEmpty(this.FormatObject))
                    fmt.LoadFormat(this.FormatObject.JsonToObject<SalesOrderIOFormat>());
                return fmt;
            }
            return fmt;
        }

        public void SetFormatObject(CsvFormat csvFormat)
        {
            CsvFormat fmt = null;
            if (FormatType.EqualsIgnoreSpace(ActivityLogType.SalesOrder.ToString()))
            {
                fmt = new SalesOrderIOFormat();
                fmt.LoadFormat(csvFormat);
                this.FormatObject = fmt.ObjectToString<CsvFormat>();
            }
        }
    }
}



