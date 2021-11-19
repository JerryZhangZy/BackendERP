
    

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
    /// Represents a IWarehouseService.
    /// NOTE: This interface is generated from a T4 template once only - if you want re-generate it, you should delete this file first.
    /// </summary>
    public interface IWarehouseService : IService<WarehouseService, WarehouseData, WarehouseDataDto>
    {

        bool Add(WarehouseDataDto dto);
        Task<bool> AddAsync(WarehouseDataDto dto);
        
        bool Update(WarehouseDataDto dto);
        Task<bool> UpdateAsync(WarehouseDataDto dto);

        Task<IList<DistributionCenter>> GetWarehouseList(int masterAccountNum, int profileNum);
    }
}



