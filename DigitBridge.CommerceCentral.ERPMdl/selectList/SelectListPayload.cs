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
        public virtual bool With_term { get { return !string.IsNullOrWhiteSpace(Term); } }
        public virtual bool ShouldSerializeterm() { return (With_term); }

        public string SelectListName { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public virtual bool With_SelectListName { get { return !string.IsNullOrWhiteSpace(SelectListName); } }

        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public string name { get { return SelectListName; } set { SelectListName = value; }  }
        public int max_rec { get; set; }
        public int Count { get { return (data == null) ? 0 : data.Count; } }

        public IList<NameValueClass> filter { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public virtual bool With_filter { get { return filter != null && filter.Count > 0; } }
        public virtual bool ShouldSerializefilter() { return (With_filter); }

        public IList<SelectListItemClass> data { get; set; }
        [JsonIgnore, XmlIgnore, IgnoreCompare]
        public virtual bool With_data { get { return data != null && data.Count > 0; } }
        public virtual bool ShouldSerializedata() { return (With_data); }
        #endregion properties

        public SelectListPayload ClearData()
        {
            data = new List<SelectListItemClass>();
            return this;
        }
        public SelectListPayload AddData(IList<SelectListItemClass> list)
        {
            data = list;
            return this;
        }
        public SelectListPayload AddData(SelectListItemClass obj)
        {
            if (obj == null) return this;
            if (data == null)
                data = new List<SelectListItemClass>();
            data.Add(obj);
            return this;
        }

        public SelectListPayload CopyValueFrom(SelectListPayload obj)
        {
            if (obj == null || this == obj) return this;
            if (With_listFor && !SelectListName.EqualsIgnoreSpace(obj.SelectListName)) return this;

            SelectListName = obj.SelectListName;
            Term = obj.Term;
            max_rec = obj.max_rec;
            data = new List<SelectListItemClass>();
            if (obj.With_data)
            {
                foreach (SelectListItemClass item in obj.data)
                    data.Add(item);
            }

            filter = new List<NameValueClass>();
            if (obj.With_filter)
            {
                foreach (NameValueClass item in obj.filter)
                    filter.Add(item);
            }
            return this;
        }
        

    }
}
