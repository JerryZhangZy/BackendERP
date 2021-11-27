using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitBridge.Base.Common;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public partial class PoReceiveService : PoTransactionService, IPoReceiveService
    {
        private bool _firstAPReceiveStatus = false;

        public PoReceiveService(IDataBaseFactory dbFactory) : base(dbFactory)
        {
        }

        #region Service Property

        private InventoryLogService _inventoryLogService;

        protected InventoryLogService InventoryLogService
        {
            get
            {
                if (_inventoryLogService == null)
                    _inventoryLogService = new InventoryLogService(dbFactory);
                return _inventoryLogService;
            }
        } 

        private ApInvoiceService _apInvoiceService;

        protected ApInvoiceService ApInvoiceService
        {
            get
            {
                if (_apInvoiceService == null)
                    _apInvoiceService = new ApInvoiceService(dbFactory);
                return _apInvoiceService;
            }
        }

        private InventoryService _inventoryService;

        protected InventoryService InventoryService
        {
            get
            {
                if (_inventoryService == null)
                    _inventoryService = new InventoryService(dbFactory);
                return _inventoryService;
            }
        }

        #endregion

        #region override methods

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override PoTransactionService Init()
        {
            SetDtoMapper(new PoTransactionDataDtoMapperDefault());
            SetCalculator(new PoReceiveServiceCalculatorDefault(this, this.dbFactory));
            AddValidator(new PoReceiveServiceValidatorDefault(this, this.dbFactory));
            return this;
        }

        /// <summary>
        /// Before update data (Add/Update/Delete). call this function to update relative data.
        /// For example: before save shipment, rollback instock in inventory table according to shipment table.
        /// Mostly, inside this function should call SQL script update other table depend on current database table records.
        /// </summary>
        public override async Task BeforeSaveAsync()
        {
            try
            {
                await base.BeforeSaveAsync();
                if (ProcessMode != ProcessingMode.Delete)
                {
                    if (this.Data?.PoTransaction != null)
                    {
                        await InventoryService.UpdateOpenPoQtyFromPoTransactionItemAsync(this.Data.PoTransaction.TransUuid, true);
                        await PurchaseOrderService.UpdateReceivedQtyFromPoTransactionItemAsync(this.Data.PoTransaction.TransUuid, true);
                        await InventoryLogService.RollbackPoReceiveAsync(this.Data.PoTransaction.TransUuid);
                    }
                }
                else
                {
                    await PurchaseOrderService.UpdateReceivedQtyFromPoTransactionItemAsync(this.Data.PoTransaction.TransUuid, true);
                    await InventoryService.UpdateOpenPoQtyFromPoTransactionItemAsync(this.Data.PoTransaction.TransUuid, true);
                    await InventoryLogService.UpdateByPoReceiveAsync(_data, false);
                }
            }
            catch (Exception)
            {
                AddWarning("Updating relative data caused an error before save.");
            }
        }

        /// <summary>
        /// Before update data (Add/Update/Delete). call this function to update relative data.
        /// For example: before save shipment, rollback instock in inventory table according to shipment table.
        /// Mostly, inside this function should call SQL script update other table depend on current database table records.
        /// </summary>
        public override void BeforeSave()
        {
            try
            {
                base.BeforeSave();
                if (this.Data?.PoTransaction != null)
                {
                    //inventoryService.UpdateOpenSoQtyFromSalesOrderItem(this.Data.SalesOrderHeader.SalesOrderUuid, true);
                }
            }
            catch (Exception)
            {
                AddWarning("Updating relative data caused an error before save.");
            }
        }

        /// <summary>
        /// After save data (Add/Update/Delete), doesn't matter success or not, call this function to update relative data.
        /// For example: after save shipment, update instock in inventory table according to shipment table.
        /// Mostly, inside this function should call SQL script update other table depend on current database table records.
        /// So that, if update not success, database records will not change, this update still use then same data. 
        /// </summary>
        public override async Task AfterSaveAsync()
        {
            try
            {
                await base.AfterSaveAsync();
                if (ProcessMode != ProcessingMode.Delete)
                {
                    if (this.Data?.PoTransaction != null)
                    {
                        await PurchaseOrderService.UpdateReceivedQtyFromPoTransactionItemAsync(this.Data.PoTransaction.TransUuid);
                        await InventoryService.UpdateOpenPoQtyFromPoTransactionItemAsync(this.Data.PoTransaction.TransUuid);
                        await InventoryLogService.UpdateByPoReceiveAsync(_data, true);
                    }
                }

            }
            catch (Exception)
            {
                AddWarning("Updating relative data caused an error after save.");
            }
        }

        /// <summary>
        /// After save data (Add/Update/Delete), doesn't matter success or not, call this function to update relative data.
        /// For example: after save shipment, update instock in inventory table according to shipment table.
        /// Mostly, inside this function should call SQL script update other table depend on current database table records.
        /// So that, if update not success, database records will not change, this update still use then same data. 
        /// </summary>
        public override void AfterSave()
        {
            try
            {
                base.AfterSave();
                if (this.Data?.PoTransaction != null)
                {
                    //inventoryService.UpdateOpenSoQtyFromSalesOrderItem(this.Data.SalesOrderHeader.SalesOrderUuid);
                }
            }
            catch (Exception)
            {
                AddWarning("Updating relative data caused an error after save.");
            }
        }

        /// <summary>
        /// Only save success (Add/Update/Delete), call this function to update relative data.
        /// For example: add activity log records.
        /// </summary>
        public override async Task SaveSuccessAsync()
        {
            try
            {
                await base.SaveSuccessAsync();
            }
            catch (Exception)
            {
                AddWarning("Updating relative data caused an error after save success.");
            }
        }

        /// <summary>
        /// Only save success (Add/Update/Delete), call this function to update relative data.
        /// For example: add activity log records.
        /// </summary>
        public override void SaveSuccess()
        {
            try
            {
                base.SaveSuccess();
            }
            catch (Exception)
            {
                AddWarning("Updating relative data caused an error after save success.");
            }
        }

        /// <summary>
        /// Sub class should override this method to return new ActivityLog object for service
        /// </summary>
        protected override ActivityLog GetActivityLog() =>
            new ActivityLog(dbFactory)
            {
                Type = (int)ActivityLogType.PoReceive,
                Action = (int)this.ProcessMode,
                LogSource = "PoReceiveService",

                MasterAccountNum = this.Data.PoTransaction.MasterAccountNum,
                ProfileNum = this.Data.PoTransaction.ProfileNum,
                DatabaseNum = this.Data.PoTransaction.DatabaseNum,
                ProcessUuid = this.Data.PoTransaction.TransUuid,
                ProcessNumber = this.Data.PoTransaction.TransNum.ToString(),
                ChannelNum = 0,
                ChannelAccountNum = 0,

                LogMessage = string.Empty
            };

        #endregion override methods


        public virtual async Task GetPaymentWithPoHeaderAsync(PoReceivePayload payload, string poNum,
            int? transNum = null)
        {
            payload.PoTransactions =
                await GetPoTransactionDataDtoListAsync(payload.MasterAccountNum, payload.ProfileNum, poNum, transNum);
            payload.PoHeader = await GetPoHeaderAsync(payload.MasterAccountNum, payload.ProfileNum, poNum);
            payload.Success = true;
            payload.Messages = this.Messages;
        }

        public async Task<bool> GetByNumberAsync(PoReceivePayload payload, string poNumber, int transNum)
        {
            return await base.GetByNumberAsync(payload, poNumber, transNum);
        }



        public async Task<bool> GetByNumberAsync(int masterAccountNum, int profileNum, string poNumber, int transNum)
        {
            return await base.GetByNumberAsync(masterAccountNum, profileNum, poNumber, transNum);
        }


        public async Task<bool> GetByNumberAsync(PoReceivePayload payload, int transNum)
        {
            var poTransactions = dbFactory.Db.Query<PoTransaction>($@"SELECT * FROM PoTransaction WHERE MasterAccountNum=@0 AND ProfileNum=@1 AND TransNum=@2", payload.MasterAccountNum.ToSqlParameter("MasterAccountNum"), payload.ProfileNum.ToSqlParameter("ProfileNum"), transNum.ToSqlParameter("transNum")).ToList();
            if (poTransactions.Count == 0)
            {
                this.Messages.Add(new MessageClass() { Message = "transNum is not found" });
                return false;
            }
            return await base.GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, transNum.ToString());
        }

        /// <summary>
        /// Delete invoice by invoice number
        /// </summary>
        /// <param name="poNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByNumberAsync(PoReceivePayload payload, int transNum)
        {
            return await base.DeleteByNumberAsync(payload, transNum.ToString());
        }

        /// <summary>
        /// Delete data by number
        /// </summary>
        /// <param name="poNumber"></param>
        /// <returns></returns>
        public virtual bool DeleteByNumber(PoReceivePayload payload, string poNumber, int transNum)
        {
            return base.DeleteByNumber(payload, poNumber, transNum);
        }

        //public virtual async Task<bool> DeleteByNumberAsync(PoReceivePayload payload,  int transNum)
        //{
        //    return await base.DeleteByNumberAsync(payload, transNum);
        //}

        public void SetFirstAPReceiveStatus(bool isAp = false)
        {
            _firstAPReceiveStatus = isAp;
        }

        public bool Add(PoReceivePayload payload)
        {
            if (!payload.HasPoTransaction)
            {
                AddError("PoTransaction cannot be blank");
                return false;
            }

            var transdata = payload.PoTransaction;
            if (!transdata.HasPoTransaction || !transdata.HasPoTransactionItems)
            {
                AddError("PoTransaction cannot be blank");
                return false;
            }

            var list = SplitPoTransaction(transdata);
            payload.PoTransactions = new List<PoTransactionDataDto>();
            foreach (var dto in list)
            {
                payload.PoTransaction = dto;
                if (base.Add(payload))
                {
                    payload.PoTransactions.Add(ToDto());
                }
            }

            payload.PoTransaction = null;
            payload.Messages = Messages;
            return true;
        }



        public async Task<bool> AddAsync(PoReceivePayload payload)
        {
            if (!payload.HasPoTransaction)
            {
                AddError("PoTransaction cannot be blank");
                return false;
            }

            var transdata = payload.PoTransaction;
            if (!transdata.HasPoTransaction || !transdata.HasPoTransactionItems)
            {
                AddError("PoTransaction cannot be blank");
                return false;
            }


            payload.PoTransactions = new List<PoTransactionDataDto>();
            if (await base.AddAsync(payload))
                payload.Success = true;

            payload.Messages = Messages;
            return true;
        }

        public async Task<bool> AddListAsync(PoReceivePayload payload)
        {
            if (!payload.HasPoTransactions)
            {
                AddError("PoTransaction cannot be blank");
                return false;
            }

            var list = SplitPoTransactionsForVendor(payload.PoTransactions);
            payload.PoTransactions = new List<PoTransactionDataDto>();
            foreach (var dto in list)
            {
                payload.PoTransaction = dto;
                if (await base.AddAsync(payload))
                {
                    payload.PoTransactions.Add(ToDto());
                }
            }

            payload.PoTransaction = null;
            payload.Messages = Messages;
            return true;
        }

        public bool Update(PoReceivePayload payload)
        {
            return base.Update(payload);
        }

        public async Task<bool> UpdateAsync(PoReceivePayload payload)
        {
            if (!payload.HasPoTransaction)
            {
                AddError("PoTransaction cannot be blank");
                return false;
            }

            var transdata = payload.PoTransaction;
            if (!transdata.HasPoTransaction || !transdata.HasPoTransactionItems)
            {
                AddError("PoTransaction cannot be blank");
                return false;
            }

            return await base.UpdateAsync(payload);

        }

        public async Task<bool> ClosePoReceiveAsync(PoReceivePayload payload, int transNum)
        {

            Edit();
            if (!await GetByNumberAsync(payload, transNum))
            {
                AddError("PoTransaction cannot be find");
                return false;
            }
            base.Data.PoTransaction.TransStatus = (int)PoTransStatus.Closed;

            if (await base.SaveDataAsync())
            {
                await ApInvoiceService.CreateOrUpdateApInvoiceByPoReceiveAsync(payload.MasterAccountNum, payload.ProfileNum, _data);
                await InventoryService.UpdatAvgCostByPoReceiveAsync(_data);
                return true;
            }

            return false;

        }




        public override async Task<bool> SaveDataAsync()
        {
            Data.FirstAPReceiveStatus = _firstAPReceiveStatus;
            return await base.SaveDataAsync();
        }



        public override async Task<bool> DeleteDataAsync()
        {
            return await base.DeleteDataAsync();
        }

 

        /// <summary>
        /// Create new P/O receive data for one P/O
        /// This will load open P/O Items for one P/O
        /// </summary>
        public async Task<bool> NewReceiveAsync(PoReceivePayload payload, string poNum)
        {
            NewData();

            if (!LoadPurchaseOrderData(poNum, payload.ProfileNum, payload.MasterAccountNum))
                return false;

            CopyPoHeaderToReceive();
            CopyPoItemsToReceiveItems();

            return true;
        }

        /// <summary>
        /// Create new P/O receive data for one vendor
        /// This will load multiple Open P/O Items for one vendor
        /// </summary>
        public async Task<bool> NewReceiveForVendorAsync(PoReceivePayload payload, string vendorCode)
        {

            List<string> poNums = dbFactory.Db.Query<string>($@"select  distinct ph.[PoNum] from [dbo].[PoHeader] ph  
                  LEFT JOIN  [dbo].[PoItems] poi on ph.PoUuid=poi.PoUuid 
                  where    ph.MasterAccountNum=@0 AND  ph.ProfileNum=@1 AND (poi.PoQty-poi.ReceivedQty-poi.CancelledQty)>0 AND ph.VendorCode=@2",
                   payload.MasterAccountNum.ToSqlParameter("MasterAccountNum"), payload.ProfileNum.ToSqlParameter("ProfileNum"), vendorCode.ToSqlParameter("VendorCode")).ToList();

            var transactions = new List<PoTransactionDataDto>();
            foreach (var num in poNums)
            {
                if (await NewReceiveAsync(payload, num))
                    transactions.Add(ToDto());

            }
            if (transactions.Count == 0)
            {
                AddWarning($@"{vendorCode} there is no purchase record");
                return false;
            }
            payload.PoTransactions = new List<PoTransactionDataDto>();
            payload.PoTransactions.Add(MergePoTransactions(transactions, false));
            return true;
        }





        //
        // public async Task<bool> ExistPoUuidAsync(string poUuid)
        // {
        //     return await dbFactory.ExistsAsync<PoTransaction>("PoUuid=@0", poUuid.ToSqlParameter("@0"));
        // }
        //
        // private VendorService _vendorService;
        //
        // protected VendorService VendorService
        // {
        //     get
        //     {
        //         if (_vendorService == null)
        //             _vendorService = new VendorService(dbFactory);
        //         return _vendorService;
        //     }
        // }

        private IList<PoTransactionDataDto> SplitPoTransactionsForVendor(IList<PoTransactionDataDto> dtolist)
        {

            foreach (var transdata in dtolist)
            {
                if (!transdata.HasPoTransaction || !transdata.HasPoTransactionItems)
                {
                    AddError("PoTransaction cannot be blank");
                    continue;
                }
            }
            var vendorPoTransactionList = new List<PoTransactionDataDto>();
            var vendorPoTransactions = dtolist.GroupBy(r => r.PoTransaction.VendorCode);
            foreach (var vendorItem in vendorPoTransactions)
            {
                vendorPoTransactionList.Add(MergePoTransactions(vendorItem.ToList(), true));
            }
            return vendorPoTransactionList;
        }


        private PoTransactionDataDto MergePoTransactions(List<PoTransactionDataDto> list, bool filterZeroTransQty = true)
        {
            //var list = new List<PoTransactionDataDto>();

            var poTransactionItems = new List<PoTransactionItemsDto>();
            foreach (var po in list)
            {
                foreach (var poItem in po.PoTransactionItems)
                {

                    if (!filterZeroTransQty)
                    {
                        poTransactionItems.Add(poItem);
                    }
                    else
                    {
                        if (poItem.TransQty > 0)
                            poTransactionItems.Add(poItem);
                    }

                }
            }


            var poTransaction = new PoTransactionDto()
            {

                PoUuid = list.Count > 1 ? "" : list[0].PoTransaction.PoUuid,
                PoNum = list.Count > 1 ? "" : list[0].PoTransaction.PoNum,
                ProfileNum = list[0].PoTransaction.ProfileNum,
                MasterAccountNum = list[0].PoTransaction.MasterAccountNum,
                DatabaseNum = list[0].PoTransaction.DatabaseNum,
                VendorCode = list[0].PoTransaction.VendorCode,
                VendorName = list[0].PoTransaction.VendorName,
                VendorUuid = list[0].PoTransaction.VendorUuid,
                Currency = list[0].PoTransaction.Currency,
                Description = list[0].PoTransaction.Description,
            };

            if (ProcessMode == ProcessingMode.Add)
            {
                poTransaction.TransStatus = _firstAPReceiveStatus ? (int)PoTransStatus.APReceive : (int)PoTransStatus.StockReceive;
            }


            var dto = new PoTransactionDataDto()
            {
                PoTransaction = poTransaction,
                PoTransactionItems = poTransactionItems
            };


            return dto;

        }



        private IList<PoTransactionDataDto> SplitPoTransaction(PoTransactionDataDto dto)
        {
            var list = new List<PoTransactionDataDto>();
            var poUUids = dto.PoTransactionItems.Select(r => r.PoUuid).Distinct();
            var totalQty = dto.PoTransactionItems.Sum(r => r.TransQty.ToInt());
            var toalTaxAmount = dto.PoTransaction.TaxAmount.ToAmount();
            var totalShipmentAmount = dto.PoTransaction.ShippingAmount.ToAmount();
            var totalShipmentTaxAmount = dto.PoTransaction.ShippingTaxAmount.ToAmount();
            var totalMiscAmount = dto.PoTransaction.MiscAmount.ToAmount();
            var totalMiscTaxAmount = dto.PoTransaction.MiscTaxAmount.ToAmount();
            var totalDiscountAmount = dto.PoTransaction.DiscountAmount.ToAmount();
            foreach (var poUUid in poUUids)
            {
                var poTransactionItems =
                    dto.PoTransactionItems.Where(r => r.PoUuid == poUUid && r.TransQty > 0).ToList();
                var currentQty = poTransactionItems.Sum(r => r.TransQty);
                var potransaction = new PoTransactionDto()
                {
                    RowNum = dto.PoTransaction.RowNum,
                    PoUuid = poUUid,
                    PoNum = dto.PoTransaction.PoNum,
                    ProfileNum = dto.PoTransaction.ProfileNum,
                    MasterAccountNum = dto.PoTransaction.MasterAccountNum,
                    VendorCode = dto.PoTransaction.VendorCode,
                    VendorName = dto.PoTransaction.VendorName,
                    VendorUuid = dto.PoTransaction.VendorUuid,
                    DatabaseNum = dto.PoTransaction.DatabaseNum,
                    Currency = dto.PoTransaction.Currency,
                    ChargeAndAllowanceAmount = dto.PoTransaction.ChargeAndAllowanceAmount,
                    Description = dto.PoTransaction.Description,
                    DiscountAmount = totalQty > 0 ? totalDiscountAmount / totalQty * currentQty : 0,
                    DiscountRate = dto.PoTransaction.DiscountRate,
                    ShippingAmount = totalQty > 0 ? totalShipmentAmount / totalQty * currentQty : 0,
                    ShippingTaxAmount = totalQty > 0 ? totalShipmentTaxAmount / totalQty * currentQty : 0,
                    MiscAmount = totalQty > 0 ? totalMiscAmount / totalQty * currentQty : 0,
                    MiscTaxAmount = totalQty > 0 ? totalMiscTaxAmount / totalQty * currentQty : 0,
                    TaxAmount = totalQty > 0 ? toalTaxAmount / totalQty * currentQty : 0,
                    TransStatus = dto.PoTransaction.TransStatus,
                    TransType = dto.PoTransaction.TransType,
                    VendorInvoiceDate = dto.PoTransaction.VendorInvoiceDate,
                    VendorInvoiceNum = dto.PoTransaction.VendorInvoiceNum
                };
                if (ProcessMode == ProcessingMode.Add)
                {
                    potransaction.TransStatus = _firstAPReceiveStatus ? (int)PoTransStatus.APReceive : (int)PoTransStatus.StockReceive;
                }
                list.Add(new PoTransactionDataDto()
                {
                    PoTransaction = potransaction,
                    PoTransactionItems = poTransactionItems
                });
            }

            return list;
        }  
    }
}