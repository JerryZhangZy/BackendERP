

//-------------------------------------------------------------------------
// This document is generated by T4
// It will overwrite your changes, please keep it as it is
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    /// <summary>
    /// Represents a default SalesOrderService Calculator class.
    /// </summary>
    public partial class VendorDtoPrepareDefault : IPrepare<VendorService, VendorData, VendorDataDto>
    {
        private int masterAccountNum;
        private int  profileNum;
        public VendorDtoPrepareDefault(VendorService vendorService, int masterAccountNum,int profileNum)
        {
            this.masterAccountNum = masterAccountNum;
            this.profileNum = profileNum;
            _vendorService = vendorService;
        }

        protected VendorService _vendorService;
        protected VendorService Service 
        { 
            get => _vendorService; 
        }
        protected IDataBaseFactory dbFactory 
        { 
            get => Service.dbFactory; 
        }
        #region message
        [XmlIgnore, JsonIgnore]
        protected IList<MessageClass> Messages
        {
            get => Service.Messages;
        }
        protected IList<MessageClass> AddInfo(string message, string code = null) => Service.AddInfo(message, code);
        protected IList<MessageClass> AddWarning(string message, string code = null) => Service.AddWarning(message, code);
        protected IList<MessageClass> AddError(string message, string code = null) => Service.AddError(message, code);
        protected IList<MessageClass> AddFatal(string message, string code = null) => Service.AddFatal(message, code);
        protected IList<MessageClass> AddDebug(string message, string code = null) => Service.AddDebug(message, code);

        #endregion message

        #region Service Property

       

    

        #endregion

        private DateTime now = DateTime.UtcNow;

        /// <summary>
        /// Check Dto data, fill customer and inventory info.
        /// </summary>
        public virtual async Task<bool> PrepareDtoAsync(VendorDataDto dto)
        {
            if (dto == null || dto.Vendor == null)
                return false;

            dto.Vendor.MasterAccountNum = this.masterAccountNum;
            dto.Vendor.ProfileNum = this.profileNum;

            return true;
        }

 

    }
}



