
//-------------------------------------------------------------------------
// This document is generated by T4
// It will overwrite your changes, please keep it as it is
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using DigitBridge.Base.Utility;
using Newtonsoft.Json.Linq;

namespace DigitBridge.CommerceCentral.ERPEventSDK
{
    /// <summary>
    /// Request and Response payload object
    /// </summary>
    [Serializable()]
    public class EventERPPayload
    {
        public int MasterAccountNum { get; set; }

        public int ProfileNum { get; set; }

        public int DatabaseNum { get; set; }

        public IList<MessageClass> Messages { get; set; } = new List<MessageClass>();

        public bool Success { get; set; } = true;

        #region single Dto object

        /// <summary>
        /// (Request and Response Data) Single EventERP entity object which load by Number.
        /// </summary>
        //public EventERPDataDto EventERP { get; set; }
        //[JsonIgnore] public virtual bool HasEventERP => EventERP != null;
        //public bool ShouldSerializeEventERP() => HasEventERP;

        #endregion single Dto object


        #region list service
        public JArray EventERPList { get; set; }

        /// <summary>
        /// (Response Data) List result count which load filter and paging.
        /// </summary>
        public int EventERPListCount { get; set; }
        #endregion list service
    }
}

