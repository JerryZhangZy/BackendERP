    

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

using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    /// <summary>
    /// Represents a IVendorAddressService.
    /// NOTE: This interface is generated from a T4 template once only - if you want re-generate it, you should delete this file first.
    /// </summary>
    public interface IVendorAddressService : IService<VendorAddressService, VendorAddressData, VendorAddressDataDto>
    {

        bool Add(VendorAddressDataDto dto);
        Task<bool> AddAsync(VendorAddressDataDto dto);
        
        bool Update(VendorAddressDataDto dto);
        Task<bool> UpdateAsync(VendorAddressDataDto dto);

    }
}



