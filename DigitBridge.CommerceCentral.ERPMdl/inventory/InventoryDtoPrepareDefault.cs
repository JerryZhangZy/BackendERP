

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
    /// Represents a default InventoryService Calculator class.
    /// </summary>
    public partial class InventoryDtoPrepareDefault : IPrepare<InventoryService, InventoryData, InventoryDataDto>
    {
        public InventoryDtoPrepareDefault(InventoryService salesOrderService)
        {
            _salesOrderService = salesOrderService;
        }

        protected InventoryService _salesOrderService;
        protected InventoryService Service 
        { 
            get => _salesOrderService; 
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

        private InventoryService _inventoryService;

        protected InventoryService inventoryService
        {
            get
            {
                if (_inventoryService is null)
                    _inventoryService = new InventoryService(dbFactory);
                return _inventoryService;
            }
        }

        #endregion

        private DateTime now = DateTime.UtcNow;

        /// <summary>
        /// Check Dto data, fill customer and inventory info.
        /// </summary>
        public virtual async Task<bool> PrepareDtoAsync(InventoryDataDto dto)
        {
            if (dto == null || !dto.HasInventory)
                return false;

            foreach(var inventory in dto.Inventory)
            {
                inventory.SKU = dto.ProductBasic.SKU;
                inventory.ProductUuid = dto.ProductBasic.ProductUuid;
                inventory.Currency = dto.ProductExt.Currency;
            }

            // Load customer info to Dto 
            //if (!await CheckCustomerAsync(dto))
            //{
            //    AddError($"Cannot find or create customer for {dto.InventoryHeader.OrderSourceCode}.");
            //}

            //// Load inventory info to Dto 
            //// check sku and warehouse exist, otherwise add new SKU and Warehouse
            //if (!await CheckInventoryAsync(dto))
            //{
            //    AddError($"Cannot find or create SKU for {dto.InventoryHeader.OrderSourceCode}.");
            //}

            return true;
        }

        //#region GetDataWithCache
        ///// <summary>
        ///// get inventory data
        ///// </summary>
        ///// <param name="data"></param>
        ///// <param name="sku"></param>
        ///// <returns></returns>
        //public virtual InventoryData GetInventoryData(InventoryData data, string sku)
        //{
        //    var key = data.InventoryHeader.MasterAccountNum + "_" + data.InventoryHeader.ProfileNum + '_' + sku;
        //    return data.GetCache(key, () =>
        //    {
        //        if (inventoryService.GetByNumber(data.InventoryHeader.MasterAccountNum, data.InventoryHeader.ProfileNum, sku))
        //            return inventoryService.Data;
        //        return null;
        //    });
        //}
        ///// <summary>
        ///// get inventory data
        ///// </summary>
        ///// <param name="data"></param>
        ///// <param name="sku"></param>
        ///// <returns></returns>
        //public virtual InventoryData GetInventoryData_InventoryUuid(InventoryData data, string inventoryUuid)
        //{
        //    var key = data.InventoryHeader.MasterAccountNum + "_" + data.InventoryHeader.ProfileNum + '_' + inventoryUuid;
        //    return data.GetCache(key, () =>
        //    {
        //        if (inventoryService.GetDataById(inventoryUuid))
        //            return inventoryService.Data;
        //        return null;
        //    });
        //}
        ///// <summary>
        ///// Get Customer Data by customerCode
        ///// </summary>
        ///// <param name="data"></param>
        ///// <param name="customerCode"></param>
        ///// <returns></returns>
        //public virtual CustomerData GetCustomerData(InventoryData data, string customerCode)
        //{
        //    var key = data.InventoryHeader.MasterAccountNum + "_" + data.InventoryHeader.ProfileNum + '_' + customerCode;
        //    return data.GetCache(key, () =>
        //    {
        //        if (customerService.GetByNumber(data.InventoryHeader.MasterAccountNum, data.InventoryHeader.ProfileNum, customerCode))
        //            return customerService.Data;
        //        return null;
        //    });
        //}
        //#endregion

        ///// <summary>
        ///// Load info from customer to Dto
        ///// Try use customer uuid, code, name or phone find customer
        ///// If customer code not exist, add new customer. 
        ///// </summary>
        //protected async Task<bool> CheckCustomerAsync(InventoryDataDto dto)
        //{
        //    if (!string.IsNullOrEmpty(dto.InventoryHeader.CustomerCode))
        //        return true;

        //    // try to find customer
        //    var find = new CustomerFindClass()
        //    {
        //        MasterAccountNum = dto.InventoryHeader.MasterAccountNum.ToInt(),
        //        ProfileNum = dto.InventoryHeader.ProfileNum.ToInt(),
        //        CustomerUuid = dto.InventoryHeader.CustomerUuid,
        //        CustomerCode = dto.InventoryHeader.CustomerCode,
        //        ChannelNum = dto.InventoryHeaderInfo.ChannelNum.ToInt(),
        //        ChannelAccountNum = dto.InventoryHeaderInfo.ChannelAccountNum.ToInt(),
        //        CustomerName = dto.InventoryHeader.CustomerName,
        //        Phone1 = dto.InventoryHeaderInfo.BillToDaytimePhone,
        //        Email = dto.InventoryHeaderInfo.BillToEmail,
        //    };

        //    // if not found exist customer, add new customer
        //    if (!(await customerService.GetCustomerByCustomerFindAsync(find)))
        //    {
        //        await AddNewCustomerFromDtoAsync(dto, customerService);
        //    }

        //    // load info from customer data
        //    var customer = customerService.Data.Customer;

        //    dto.InventoryHeader.CustomerUuid = customer.CustomerUuid;
        //    dto.InventoryHeader.CustomerCode = customer.CustomerCode;
        //    dto.InventoryHeader.CustomerName = customer.CustomerName;
        //    dto.InventoryHeader.Terms = customer.Terms;
        //    dto.InventoryHeader.TermsDays = customer.TermsDays;

        //    if (string.IsNullOrEmpty(dto.InventoryHeader.SalesRep))
        //    {
        //        dto.InventoryHeader.SalesRep = customer.SalesRep;
        //        dto.InventoryHeader.CommissionRate = customer.CommissionRate;
        //    }
        //    if (string.IsNullOrEmpty(dto.InventoryHeader.SalesRep2))
        //    {
        //        dto.InventoryHeader.SalesRep2 = customer.SalesRep2;
        //        dto.InventoryHeader.CommissionRate2 = customer.CommissionRate2;
        //    }
        //    if (string.IsNullOrEmpty(dto.InventoryHeader.SalesRep3))
        //    {
        //        dto.InventoryHeader.SalesRep3 = customer.SalesRep3;
        //        dto.InventoryHeader.CommissionRate3 = customer.CommissionRate3;
        //    }
        //    if (string.IsNullOrEmpty(dto.InventoryHeader.SalesRep4))
        //    {
        //        dto.InventoryHeader.SalesRep4 = customer.SalesRep4;
        //        dto.InventoryHeader.CommissionRate4 = customer.CommissionRate4;
        //    }
        //    return true;
        //}

        //protected async Task<bool> AddNewCustomerFromDtoAsync(InventoryDataDto dto, CustomerService service)
        //{
        //    customerService.NewData();
        //    var newCustomer = customerService.Data;
        //    newCustomer.Customer.MasterAccountNum = dto.InventoryHeader.MasterAccountNum.ToInt();
        //    newCustomer.Customer.ProfileNum = dto.InventoryHeader.ProfileNum.ToInt();
        //    newCustomer.Customer.DatabaseNum = dto.InventoryHeader.DatabaseNum.ToInt();
        //    newCustomer.Customer.CustomerUuid = Guid.NewGuid().ToString();
        //    newCustomer.Customer.CustomerCode = string.Empty;
        //    newCustomer.Customer.CustomerName = dto.InventoryHeaderInfo.BillToName;
        //    newCustomer.Customer.CustomerType = (int)CustomerType.ImportNewCustomer;
        //    newCustomer.Customer.CustomerStatus = (int)CustomerStatus.Active;
        //    newCustomer.Customer.FirstDate = DateTime.UtcNow.Date;
        //    newCustomer.Customer.ChannelNum = dto.InventoryHeaderInfo.ChannelNum.ToInt();
        //    newCustomer.Customer.ChannelAccountNum = dto.InventoryHeaderInfo.ChannelAccountNum.ToInt();
        //    newCustomer.AddCustomerAddress(new CustomerAddress()
        //    {
        //        AddressCode = AddressCodeType.Ship,
        //        Name = dto.InventoryHeaderInfo.ShipToName,
        //        Company = dto.InventoryHeaderInfo.ShipToCompany,
        //        AddressLine1 = dto.InventoryHeaderInfo.ShipToAddressLine1,
        //        AddressLine2 = dto.InventoryHeaderInfo.ShipToAddressLine2,
        //        AddressLine3 = dto.InventoryHeaderInfo.ShipToAddressLine3,
        //        City = dto.InventoryHeaderInfo.ShipToCity,
        //        State = dto.InventoryHeaderInfo.ShipToState,
        //        StateFullName = dto.InventoryHeaderInfo.ShipToStateFullName,
        //        PostalCode = dto.InventoryHeaderInfo.ShipToPostalCode,
        //        PostalCodeExt = dto.InventoryHeaderInfo.ShipToPostalCodeExt,
        //        County = dto.InventoryHeaderInfo.ShipToCounty,
        //        Country = dto.InventoryHeaderInfo.ShipToCountry,
        //        Email = dto.InventoryHeaderInfo.ShipToEmail,
        //        DaytimePhone = dto.InventoryHeaderInfo.ShipToDaytimePhone,
        //        NightPhone = dto.InventoryHeaderInfo.ShipToNightPhone,
        //    });
        //    newCustomer.AddCustomerAddress(new CustomerAddress()
        //    {
        //        AddressCode = AddressCodeType.Bill,
        //        Name = dto.InventoryHeaderInfo.BillToName,
        //        Company = dto.InventoryHeaderInfo.BillToCompany,
        //        AddressLine1 = dto.InventoryHeaderInfo.BillToAddressLine1,
        //        AddressLine2 = dto.InventoryHeaderInfo.BillToAddressLine2,
        //        AddressLine3 = dto.InventoryHeaderInfo.BillToAddressLine3,
        //        City = dto.InventoryHeaderInfo.BillToCity,
        //        State = dto.InventoryHeaderInfo.BillToState,
        //        StateFullName = dto.InventoryHeaderInfo.BillToStateFullName,
        //        PostalCode = dto.InventoryHeaderInfo.BillToPostalCode,
        //        PostalCodeExt = dto.InventoryHeaderInfo.BillToPostalCodeExt,
        //        County = dto.InventoryHeaderInfo.BillToCounty,
        //        Country = dto.InventoryHeaderInfo.BillToCountry,
        //        Email = dto.InventoryHeaderInfo.BillToEmail,
        //        DaytimePhone = dto.InventoryHeaderInfo.BillToDaytimePhone,
        //        NightPhone = dto.InventoryHeaderInfo.BillToNightPhone,
        //    });
        //    return await customerService.AddCustomerAsync(newCustomer);
        //}

        ///// <summary>
        ///// Load info from inventory to Dto
        ///// Try use inventory uuid, sku, upc or phone find customer
        ///// If customer code not exist, add new customer. 
        ///// </summary>
        //protected async Task<bool> CheckInventoryAsync(InventoryDataDto dto)
        //{
        //    if (dto == null || dto.InventoryItems == null || dto.InventoryItems.Count == 0)
        //    {
        //        AddError($"Sales Order items not found");
        //        return false;
        //    }

        //    var header = dto.InventoryHeader;
        //    var masterAccountNum = dto.InventoryHeader.MasterAccountNum.ToInt();
        //    var profileNum = dto.InventoryHeader.ProfileNum.ToInt();

        //    // find product SKU for each item
        //    var findSku = dto.InventoryItems.Select(x => new ProductFindClass() 
        //        { 
        //            SKU = x.SKU,
        //            UPC = x.SKU,
        //        }
        //    ).ToList();
        //    findSku = (await inventoryService.FindSkuByProductFindAsync(findSku, masterAccountNum, profileNum)).ToList();
        //    foreach (var item in dto.InventoryItems)
        //    {
        //        if (item == null) continue;
        //        var sku = findSku.FindBySku(item.SKU);
        //        if (sku == null || sku.FoundSKU.IsZero()) continue;
        //        item.SKU = sku.FoundSKU;
        //    }

        //    // find inventory data
        //    var find = dto.InventoryItems.Select(x => new InventoryFindClass() { SKU = x.SKU, WarehouseCode = x.WarehouseCode }).ToList();
        //    var notExistSkus = await inventoryService.FindNotExistSkuWarehouseAsync(find, masterAccountNum, profileNum);
        //    if (notExistSkus == null || notExistSkus.Count == 0)
        //        return true;

        //    var rtn = true;
        //    foreach (var item in dto.InventoryItems)
        //    {
        //        if (item == null || item.SKU.IsZero()) continue;

        //        if (notExistSkus.FindBySkuWarehouse(item.SKU, item.WarehouseCode) != null)
        //        {
        //            await inventoryService.AddNewProductOrInventoryAsync(new ProductBasic()
        //            {
        //                DatabaseNum = header.DatabaseNum.ToInt(),
        //                MasterAccountNum = header.MasterAccountNum.ToInt(),
        //                ProfileNum = header.ProfileNum.ToInt(),
        //                SKU = item.SKU,
        //            });
        //        }
        //    }
        //    return rtn;
        //}
    }
}



