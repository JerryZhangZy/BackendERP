using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DigitBridge.CommerceCentral.ERPMdl
{ 
    public class SalesOrderImportForSystem10001 : IMessage, IImport<SalesOrderDataDto>
    {

        public SalesOrderImportForSystem10001(IDataBaseFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        protected IDataBaseFactory dbFactory { get; set; }

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

        public async Task PrePareData(SalesOrderDataDto dto)
        {
            CalculatDetail(dto);

            CalculateSummary(dto);

            await SetCustomerInfoAsync(dto);

            await SetInventoryAsync(dto);
        }

        #region Load customer

        protected async Task SetCustomerInfoAsync(SalesOrderDataDto dto)
        {
            var customer = await GetCustomerAsync(dto);
            if (customer == null) return;

            // load info from customer data 
            var header = dto.SalesOrderHeader;
            header.CustomerUuid = customer.CustomerUuid;
            header.CustomerCode = customer.CustomerCode;
            header.CustomerName = customer.CustomerName;
            header.Terms = customer.Terms;
            header.TermsDays = customer.TermsDays;

            if (string.IsNullOrEmpty(header.SalesRep))
            {
                header.SalesRep = customer.SalesRep;
                header.CommissionRate = customer.CommissionRate;
            }
            if (string.IsNullOrEmpty(header.SalesRep2))
            {
                header.SalesRep2 = customer.SalesRep2;
                header.CommissionRate2 = customer.CommissionRate2;
            }
            if (string.IsNullOrEmpty(header.SalesRep3))
            {
                header.SalesRep3 = customer.SalesRep3;
                header.CommissionRate3 = customer.CommissionRate3;
            }
            if (string.IsNullOrEmpty(header.SalesRep4))
            {
                header.SalesRep4 = customer.SalesRep4;
                header.CommissionRate4 = customer.CommissionRate4;
            }

            if (!customer.ItemMiscAmount.IsZero())
            {
                header.MiscAmount = dto.SalesOrderItems.Sum(i => i.OrderQty) * customer.ItemMiscAmount;
            }
            else if (!customer.OrderMiscAmount.IsZero())
            {
                header.MiscAmount = customer.OrderMiscAmount;
            }

        }

        protected async Task<Customer> GetCustomerAsync(SalesOrderDataDto dto)
        {
            var header = dto.SalesOrderHeader;
            var sourceCode = $"CommerceHub-{header.Merchant}-{header.SalesDivision}";
            var customer = await customerService.GetCustomerBySourceCodeAsync(header.MasterAccountNum.ToInt(), header.ProfileNum.ToInt(), sourceCode);
            if (customer == null)
            {
                //Add cusotmer;
            }
            return customer;
        }

        protected async Task<Customer> AddCustomerAsync(SalesOrderDataDto dto)
        {
            var header = dto.SalesOrderHeader;
            customerService.NewData();
            var newCustomer = customerService.Data;
            newCustomer.Customer.MasterAccountNum = header.MasterAccountNum.ToInt();
            newCustomer.Customer.ProfileNum = header.ProfileNum.ToInt();
            newCustomer.Customer.DatabaseNum = header.DatabaseNum.ToInt();
            newCustomer.Customer.CustomerUuid = Guid.NewGuid().ToString();
            newCustomer.Customer.CustomerCode = string.Empty;
            newCustomer.Customer.CustomerName = dto.SalesOrderHeaderInfo.BillToName;
            newCustomer.Customer.CustomerType = (int)CustomerType.ImportNewCustomer;
            newCustomer.Customer.CustomerStatus = (int)CustomerStatus.Active;
            newCustomer.Customer.FirstDate = DateTime.UtcNow.Date;
            newCustomer.Customer.ChannelNum = dto.SalesOrderHeaderInfo.ChannelNum.ToInt();
            newCustomer.Customer.ChannelAccountNum = dto.SalesOrderHeaderInfo.ChannelAccountNum.ToInt();
            newCustomer.Customer.SourceCode = $"CommerceHub-{header.Merchant}-{header.SalesDivision}";
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
            var success = await customerService.AddCustomerAsync(newCustomer);
            if (success)
            {
                return customerService.Data.Customer;
            }
            else
            {
                AddError($"Add customer failed.");
                this.Messages.Add(customerService.Messages);
                return null;
            }

        }

        #endregion

        #region Load sku

        protected async Task<IList<(string SKU, string UPC, string MerchantSku)>> GetChannelSkusAsync(int masterAccountNum, int profileNum, int channelNum)
        {
            var sql = $"select SKU,UPC,ChannelProductID as MerchantSku from func_GetChannelProductIDByChannel({ masterAccountNum}, { profileNum}, { channelNum})";
            using var scope = new ScopedTransaction(dbFactory);
            var result = await SqlQuery.ExecuteAsync(sql,
                    (string sku, string upc, string merchantSku) => (sku, upc, merchantSku));

            return result;
        }


        protected async Task SetInventoryAsync(SalesOrderDataDto dto)
        {
            if (dto == null || dto.SalesOrderItems == null || dto.SalesOrderItems.Count == 0)
            {
                AddError($"Sales Order items not found");
            }

            var header = dto.SalesOrderHeader;
            var masterAccountNum = dto.SalesOrderHeader.MasterAccountNum.ToInt();
            var profileNum = dto.SalesOrderHeader.ProfileNum.ToInt();


            // find product SKU for each item
            var channelSkus = await GetChannelSkusAsync(masterAccountNum, profileNum, dto.SalesOrderHeaderInfo.ChannelNum.ToInt());
            foreach (var soitem in dto.SalesOrderItems)
            {
                if (soitem.WarehouseCode.IsZero()) soitem.WarehouseCode = string.Empty;//TODO set default warehousecode.
                if (soitem == null) continue;
                var foundChannelSku = channelSkus.Where(i => i.MerchantSku == soitem.MerchantSku).FirstOrDefault();
                if (foundChannelSku.SKU.IsZero()) continue;

                soitem.SKU = foundChannelSku.SKU;
            }

            // find inventory data
            var find = dto.SalesOrderItems.Select(x => new InventoryFindClass() { SKU = x.SKU, WarehouseCode = x.WarehouseCode }).ToList();
            var notExistSkus = await inventoryService.FindNotExistSkuWarehouseAsync(find, masterAccountNum, profileNum);
            if (notExistSkus == null || notExistSkus.Count == 0)
                return;

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
            return;
        }

        #endregion

        #region Calculate logic

        protected void CalculateSummary(SalesOrderDataDto dto)
        {
            if (!dto.SalesOrderHeader.TaxRate.IsZero())
                return;

            var taxAmount = dto.SalesOrderItems.Where(i => i.Taxable.ToBool()).Sum(j => j.TaxAmount);
            var taxableAmount = dto.SalesOrderItems.Where(i => i.Taxable.ToBool()).Sum(j => j.TaxableAmount);
            dto.SalesOrderHeader.TaxRate = (taxAmount / taxableAmount).ToDecimal();
            var diffAmount = taxAmount - dto.SalesOrderHeader.TaxRate * taxableAmount;
            dto.SalesOrderHeader.MiscAmount += diffAmount;

            dto.SalesOrderHeader.ShippingAmount = dto.SalesOrderItems.Sum(i => i.ShippingAmount);
            //dto.SalesOrderHeader.ShippingCost = dto.SalesOrderItems.Sum(i => i.ShippingCost);

        }

        protected void CalculatDetail(SalesOrderDataDto dto)
        {
            foreach (var item in dto.SalesOrderItems)
            {
                item.ItemTotalAmount = item.ItemTotalAmount.ToDecimal();
                item.OrderQty = item.OrderQty.ToDecimal();
                item.Price = (item.ItemTotalAmount / item.OrderQty).ToDecimal();

                var diffAmount = item.ItemTotalAmount - item.Price * item.OrderQty;
                dto.SalesOrderHeader.MiscAmount += diffAmount;

                item.TaxAmount = item.TaxAmount.ToDecimal();
                item.Taxable = !item.TaxAmount.IsZero();
                item.TaxableAmount = item.Taxable.ToBool() ? item.Price * item.OrderQty : 0;
            }

        }

        #endregion

        #region Messages
        protected IList<MessageClass> _messages;
        [XmlIgnore, JsonIgnore]
        public virtual IList<MessageClass> Messages
        {
            get
            {
                if (_messages is null)
                    _messages = new List<MessageClass>();
                return _messages;
            }
            set { _messages = value; }
        }
        public IList<MessageClass> AddInfo(string message, string code = null) =>
             Messages.Add(message, MessageLevel.Info, code);
        public IList<MessageClass> AddWarning(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Warning, code);
        public IList<MessageClass> AddError(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Error, code);
        public IList<MessageClass> AddFatal(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Fatal, code);
        public IList<MessageClass> AddDebug(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Debug, code);

        #endregion Messages
    }
}
