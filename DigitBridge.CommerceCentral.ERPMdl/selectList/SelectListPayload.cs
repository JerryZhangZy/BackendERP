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

namespace DigitBridge.CommerceCentral.ERPMdl
{

    public class SelectListPayload
    {
        public static string ListAll = "ALL";
        public SelectListPayload()
        {
        }
        public SelectListPayload(string selectListName, string term) : this()
        {
            SelectListName = selectListName;
            Term = term;
        }
        public void ClearAll()
        {
            data.Clear();
            return;
        }

        #region properties
        public string Term { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public virtual bool With_Term { get { return !string.IsNullOrWhiteSpace(Term); } }
        public virtual bool ShouldSerializeterm() { return (With_Term); }

        public string SelectListName { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public virtual bool With_SelectListName { get { return !string.IsNullOrWhiteSpace(SelectListName); } }

        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public string Name { get { return SelectListName; } set { SelectListName = value; }  }
        public int Limit { get; set; }
        public int Count { get { return (data == null) ? 0 : data.Count; } }

        public IList<SelectListItem> data { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public virtual bool With_data { get { return data != null && data.Count > 0; } }
        public virtual bool ShouldSerializedata() { return (With_data); }
        #endregion properties

        public SelectListPayload ClearData()
        {
            data = new List<SelectListItem>();
            return this;
        }
        public SelectListPayload AddData(IList<SelectListItem> list)
        {
            data = list;
            return this;
        }
        public SelectListPayload AddData(SelectListItem obj)
        {
            if (obj == null) return this;
            if (data == null)
                data = new List<SelectListItem>();
            data.Add(obj);
            return this;
        }

        public SelectListPayload CopyValueFrom(SelectListPayload obj)
        {
            if (obj == null || this == obj) return this;
            if (With_SelectListName && !SelectListName.EqualsIgnoreSpace(obj.SelectListName)) return this;

            SelectListName = obj.SelectListName;
            Term = obj.Term;
            Limit = obj.Limit;
            data = new List<SelectListItem>();
            if (obj.With_data)
            {
                foreach (SelectListItem item in obj.data)
                    data.Add(item);
            }

            return this;
        }
        

    }
}
