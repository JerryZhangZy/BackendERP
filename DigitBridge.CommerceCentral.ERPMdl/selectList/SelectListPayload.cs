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
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPMdl
{

    public class SelectListPayload : PayloadBase
    {
        public static string ListAll = "ALL";
        public SelectListPayload()
        {
            Top = 20;
            Skip = 0;
            LoadAll = false;
        }
        public SelectListPayload(string selectListName, string term) : this()
        {
            SelectListName = selectListName;
            Term = term;
            Top = 20;
            Skip = 0;
            LoadAll = false;
        }
        public void ClearAll()
        {
            Data.Clear();
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

        /// <summary>
        /// (Response Data) List result which load filter and paging.
        /// </summary>
        [OpenApiPropertyDescription("(Response Data) List result.")]
        [JsonConverter(typeof(StringBuilderConverter))]
        public StringBuilder Data { get; set; }

        #endregion properties

        public SelectListPayload ClearData()
        {
            Data = new StringBuilder();
            return this;
        }

    }
}
