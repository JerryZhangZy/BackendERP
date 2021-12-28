

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
    public partial class SalesOrderDtoPrepareDefault : IPrepare<SalesOrderService, SalesOrderData, SalesOrderDataDto>
    {
        public SalesOrderDtoPrepareDefault(SalesOrderService salesOrderService)
        {
            _salesOrderService = salesOrderService;
        }

        protected SalesOrderService _salesOrderService;
        protected SalesOrderService Service
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

        private CustomerService _customerService;

        protected CustomerService customerService
        {
            get
            {
                if (_customerService is null)
                    _customerService = new CustomerService(dbFactory);
                return _customerService;
            }
        }

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
        public virtual async Task<bool> PrepareDtoAsync(SalesOrderDataDto dto)
        {
            if (dto == null || dto.SalesOrderHeader == null)
                return false;

            // Load customer info to Dto 
            if (!await CheckCustomerAsync(dto))
            {
                AddError($"Cannot find or create customer for {dto.SalesOrderHeader.OrderSourceCode}.");
            }

            // Load inventory info to Dto 
            // check sku and warehouse exist, otherwise add new SKU and Warehouse
            if (!await CheckInventoryAsync(dto))
            {
                AddError($"Cannot find or create SKU for {dto.SalesOrderHeader.OrderSourceCode}.");
            }

            return true;
        }

        #region GetDataWithCache
        /// <summary>
        /// get inventory data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sku"></param>
        /// <returns></returns>
        public virtual InventoryData GetInventoryData(SalesOrderData data, string sku)
        {
            var key = data.SalesOrderHeader.MasterAccountNum + "_" + data.SalesOrderHeader.ProfileNum + '_' + sku;
            return data.GetCache(key, () =>
            {
                if (inventoryService.GetByNumber(data.SalesOrderHeader.MasterAccountNum, data.SalesOrderHeader.ProfileNum, sku))
                    return inventoryService.Data;
                return null;
            });
        }
        /// <summary>
        /// get inventory data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sku"></param>
        /// <returns></returns>
        public virtual InventoryData GetInventoryData_InventoryUuid(SalesOrderData data, string inventoryUuid)
        {
            var key = data.SalesOrderHeader.MasterAccountNum + "_" + data.SalesOrderHeader.ProfileNum + '_' + inventoryUuid;
            return data.GetCache(key, () =>
            {
                if (inventoryService.GetDataById(inventoryUuid))
                    return inventoryService.Data;
                return null;
            });
        }
        /// <summary>
        /// Get Customer Data by customerCode
        /// </summary>
        /// <param name="data"></param>
        /// <param name="customerCode"></param>
        /// <returns></returns>
        public virtual CustomerData GetCustomerData(SalesOrderData data, string customerCode)
        {
            var key = data.SalesOrderHeader.MasterAccountNum + "_" + data.SalesOrderHeader.ProfileNum + '_' + customerCode;
            return data.GetCache(key, () =>
            {
                if (customerService.GetByNumber(data.SalesOrderHeader.MasterAccountNum, data.SalesOrderHeader.ProfileNum, customerCode))
                    return customerService.Data;
                return null;
            });
        }
        #endregion

        /// <summary>
        /// Load info from customer to Dto
        /// Try use customer uuid, code, name or phone find customer
        /// If customer code not exist, add new customer. 
        /// </summary>
        protected async Task<bool> CheckCustomerAsync(SalesOrderDataDto dto)
        {
            if (!string.IsNullOrEmpty(dto.SalesOrderHeader.CustomerCode))
                return true;

            // try to find customer
            var find = new CustomerFindClass()
            {
                MasterAccountNum = dto.SalesOrderHeader.MasterAccountNum.ToInt(),
                ProfileNum = dto.SalesOrderHeader.ProfileNum.ToInt(),
                CustomerUuid = dto.SalesOrderHeader.CustomerUuid,
                CustomerCode = dto.SalesOrderHeader.CustomerCode,
                ChannelNum = dto.SalesOrderHeaderInfo.ChannelNum.ToInt(),
                ChannelAccountNum = dto.SalesOrderHeaderInfo.ChannelAccountNum.ToInt(),
                CustomerName = dto.SalesOrderHeader.CustomerName,
                Phone1 = dto.SalesOrderHeaderInfo.BillToDaytimePhone,
                Email = dto.SalesOrderHeaderInfo.BillToEmail,
            };

            // if not found exist customer, add new customer
            if (!(await customerService.GetCustomerByCustomerFindAsync(find)))
            {
                await AddNewCustomerFromDtoAsync(dto, customerService);
            }

            // load info from customer data
            var customer = customerService.Data.Customer;

            dto.SalesOrderHeader.CustomerUuid = customer.CustomerUuid;
            dto.SalesOrderHeader.CustomerCode = customer.CustomerCode;
            dto.SalesOrderHeader.CustomerName = customer.CustomerName;
            dto.SalesOrderHeader.Terms = customer.Terms;
            dto.SalesOrderHeader.TermsDays = customer.TermsDays;

            if (string.IsNullOrEmpty(dto.SalesOrderHeader.SalesRep))
            {
                dto.SalesOrderHeader.SalesRep = customer.SalesRep;
                dto.SalesOrderHeader.CommissionRate = customer.CommissionRate;
            }
            if (string.IsNullOrEmpty(dto.SalesOrderHeader.SalesRep2))
            {
                dto.SalesOrderHeader.SalesRep2 = customer.SalesRep2;
                dto.SalesOrderHeader.CommissionRate2 = customer.CommissionRate2;
            }
            if (string.IsNullOrEmpty(dto.SalesOrderHeader.SalesRep3))
            {
                dto.SalesOrderHeader.SalesRep3 = customer.SalesRep3;
                dto.SalesOrderHeader.CommissionRate3 = customer.CommissionRate3;
            }
            if (string.IsNullOrEmpty(dto.SalesOrderHeader.SalesRep4))
            {
                dto.SalesOrderHeader.SalesRep4 = customer.SalesRep4;
                dto.SalesOrderHeader.CommissionRate4 = customer.CommissionRate4;
            }

            if (!customer.ItemMiscAmount.IsZero())
            {
                dto.SalesOrderHeader.MiscAmount = dto.SalesOrderItems.Sum(i => i.OrderQty) * customer.ItemMiscAmount;
            }
            else if (!customer.OrderMiscAmount.IsZero())
            {
                dto.SalesOrderHeader.MiscAmount = customer.OrderMiscAmount;
            }

            return true;
        }

        protected async Task<bool> AddNewCustomerFromDtoAsync(SalesOrderDataDto dto, CustomerService service)
        {
            customerService.NewData();
            var newCustomer = customerService.Data;
            newCustomer.Customer.MasterAccountNum = dto.SalesOrderHeader.MasterAccountNum.ToInt();
            newCustomer.Customer.ProfileNum = dto.SalesOrderHeader.ProfileNum.ToInt();
            newCustomer.Customer.DatabaseNum = dto.SalesOrderHeader.DatabaseNum.ToInt();
            newCustomer.Customer.CustomerUuid = Guid.NewGuid().ToString();
            newCustomer.Customer.CustomerCode = string.Empty;
            newCustomer.Customer.CustomerName = dto.SalesOrderHeaderInfo.BillToName;
            newCustomer.Customer.CustomerType = (int)CustomerType.ImportNewCustomer;
            newCustomer.Customer.CustomerStatus = (int)CustomerStatus.Active;
            newCustomer.Customer.FirstDate = DateTime.UtcNow.Date;
            newCustomer.Customer.ChannelNum = dto.SalesOrderHeaderInfo.ChannelNum.ToInt();
            newCustomer.Customer.ChannelAccountNum = dto.SalesOrderHeaderInfo.ChannelAccountNum.ToInt();
            newCustomer.AddCustomerAddress(new CustomerAddress()
            {
                AddressCode = AddressCodeType.Ship,
                Name = dto.SalesOrderHeaderInfo.ShipToName,
                Company = dto.SalesOrderHeaderInfo.ShipToCompany,
                AddressLine1 = dto.SalesOrderHeaderInfo.ShipToAddressLine1,
                AddressLine2 = dto.SalesOrderHeaderInfo.ShipToAddressLine2,
                AddressLine3 = dto.SalesOrderHeaderInfo.ShipToAddressLine3,
                City = dto.SalesOrderHeaderInfo.ShipToCity,
                State = dto.SalesOrderHeaderInfo.ShipToState,
                StateFullName = dto.SalesOrderHeaderInfo.ShipToStateFullName,
                PostalCode = dto.SalesOrderHeaderInfo.ShipToPostalCode,
                PostalCodeExt = dto.SalesOrderHeaderInfo.ShipToPostalCodeExt,
                County = dto.SalesOrderHeaderInfo.ShipToCounty,
                Country = dto.SalesOrderHeaderInfo.ShipToCountry,
                Email = dto.SalesOrderHeaderInfo.ShipToEmail,
                DaytimePhone = dto.SalesOrderHeaderInfo.ShipToDaytimePhone,
                NightPhone = dto.SalesOrderHeaderInfo.ShipToNightPhone,
            });
            newCustomer.AddCustomerAddress(new CustomerAddress()
            {
                AddressCode = AddressCodeType.Bill,
                Name = dto.SalesOrderHeaderInfo.BillToName,
                Company = dto.SalesOrderHeaderInfo.BillToCompany,
                AddressLine1 = dto.SalesOrderHeaderInfo.BillToAddressLine1,
                AddressLine2 = dto.SalesOrderHeaderInfo.BillToAddressLine2,
                AddressLine3 = dto.SalesOrderHeaderInfo.BillToAddressLine3,
                City = dto.SalesOrderHeaderInfo.BillToCity,
                State = dto.SalesOrderHeaderInfo.BillToState,
                StateFullName = dto.SalesOrderHeaderInfo.BillToStateFullName,
                PostalCode = dto.SalesOrderHeaderInfo.BillToPostalCode,
                PostalCodeExt = dto.SalesOrderHeaderInfo.BillToPostalCodeExt,
                County = dto.SalesOrderHeaderInfo.BillToCounty,
                Country = dto.SalesOrderHeaderInfo.BillToCountry,
                Email = dto.SalesOrderHeaderInfo.BillToEmail,
                DaytimePhone = dto.SalesOrderHeaderInfo.BillToDaytimePhone,
                NightPhone = dto.SalesOrderHeaderInfo.BillToNightPhone,
            });
            return await customerService.AddCustomerAsync(newCustomer);
        }

        /// <summary>
        /// Load info from inventory to Dto
        /// Try use inventory uuid, sku, upc or phone find customer
        /// If customer code not exist, add new customer. 
        /// </summary>
        protected async Task<bool> CheckInventoryAsync(SalesOrderDataDto dto)
        {
            if (dto == null || dto.SalesOrderItems == null || dto.SalesOrderItems.Count == 0)
            {
                AddError($"Sales Order items not found");
                return false;
            }

            var header = dto.SalesOrderHeader;
            var masterAccountNum = dto.SalesOrderHeader.MasterAccountNum.ToInt();
            var profileNum = dto.SalesOrderHeader.ProfileNum.ToInt();

            var skuTitles = dto.SalesOrderItems.Where(i => !i.HasSKU).Select(j => j.SKUTitle);

            if (skuTitles.Count() > 0)
            {
                FindSKUByTitle(skuTitles);
            }

            // find product SKU for each item
            var findSku = dto.SalesOrderItems.Select(x => new ProductFindClass()
            {
                SKU = x.SKU,
                UPC = x.SKU,
            }
            ).ToList();
            findSku = (await inventoryService.FindSkuByProductFindAsync(findSku, masterAccountNum, profileNum)).ToList();
            foreach (var item in dto.SalesOrderItems)
            {
                if (item.WarehouseCode.IsZero()) item.WarehouseCode = string.Empty;//TODO set default warehousecode.
                if (item == null) continue;
                var sku = findSku.FindBySku(item.SKU);
                if (sku == null || sku.FoundSKU.IsZero()) continue;
                item.SKU = sku.FoundSKU;
            }

            // find inventory data
            var find = dto.SalesOrderItems.Select(x => new InventoryFindClass() { SKU = x.SKU, WarehouseCode = x.WarehouseCode }).ToList();
            var notExistSkus = await inventoryService.FindNotExistSkuWarehouseAsync(find, masterAccountNum, profileNum);
            if (notExistSkus == null || notExistSkus.Count == 0)
                return true;

            var rtn = true;
            foreach (var item in dto.SalesOrderItems)
            {
                if (item == null || item.SKU.IsZero()) continue;

                if (notExistSkus.FindBySkuWarehouse(item.SKU, item.WarehouseCode) != null)
                {
                    await inventoryService.AddNewProductOrInventoryAsync(new ProductBasic()
                    {
                        DatabaseNum = header.DatabaseNum.ToInt(),
                        MasterAccountNum = header.MasterAccountNum.ToInt(),
                        ProfileNum = header.ProfileNum.ToInt(),
                        SKU = item.SKU,
                    });
                }
            }
            return rtn;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="skuTitles"></param>
        protected void FindSKUByTitle(IEnumerable<string> skuTitles)
        {

        }

    }
}



