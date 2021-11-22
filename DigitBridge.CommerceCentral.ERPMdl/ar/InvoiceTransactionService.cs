

//-------------------------------------------------------------------------
// This document is generated by T4
// It will only generate once, if you want re-generate it, you need delete this file first.
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

using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.Base.Common;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public partial class InvoiceTransactionService
    {

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override InvoiceTransactionService Init()
        {
            base.Init();
            SetDtoMapper(new InvoiceTransactionDataDtoMapperDefault());
            SetCalculator(new InvoiceTransactionServiceCalculatorDefault(this, this.dbFactory));
            AddValidator(new InvoiceTransactionServiceValidatorDefault(this, this.dbFactory));
            return this;
        }

        #region load original invoice
        [XmlIgnore, JsonIgnore]
        protected InvoiceService _invoiceService;
        [XmlIgnore, JsonIgnore]
        public InvoiceService InvoiceService
        {
            get
            {
                if (_invoiceService is null)
                    _invoiceService = new InvoiceService(dbFactory);
                return _invoiceService;
            }
        }

        /// <summary>
        /// Load Original InvoiceData.
        /// </summary>
        public async Task<bool> LoadInvoiceAsync(string invoiceNumber, int profileNum, int masterAccountNum)
        {
            // load invoice data
            InvoiceService.List();
            var success = await InvoiceService.GetDataByNumberAsync(masterAccountNum, profileNum, invoiceNumber);
            if (!success) return false;

            if (Data == null)
                NewData();
            Data.InvoiceData = InvoiceService.Data;
            Data.InvoiceTransaction.InvoiceUuid = Data.InvoiceData.InvoiceHeader.InvoiceUuid;

            return success;
        }

        /// <summary>
        /// Load Original InvoiceData.
        /// </summary>
        public bool LoadInvoice(string invoiceNumber, int profileNum, int masterAccountNum)
        {
            // load invoice data
            InvoiceService.List();
            var success = InvoiceService.GetDataByNumber(masterAccountNum, profileNum, invoiceNumber);
            if (!success) return false;

            if (Data == null)
                NewData();
            Data.InvoiceData = InvoiceService.Data;
            Data.InvoiceTransaction.InvoiceUuid = Data.InvoiceData.InvoiceHeader.InvoiceUuid;

            return success;
        }

        /// <summary>
        /// Load Invoice data.
        /// </summary>
        /// <param name="invoiceUuid"></param>
        public bool LoadInvoice(string invoiceUuid)
        {
            // load invoice data
            InvoiceService.List();
            var success = InvoiceService.GetDataById(invoiceUuid);
            if (!success) return false;

            if (Data == null)
                NewData();
            Data.InvoiceData = InvoiceService.Data;
            Data.InvoiceTransaction.InvoiceUuid = InvoiceService.Data.InvoiceHeader.InvoiceUuid;

            return success;
        }

        /// <summary>
        /// Load Invoice data.
        /// </summary>
        /// <param name="invoiceUuid"></param>
        public async Task<bool> LoadInvoiceAsync(string invoiceUuid)
        {
            // load invoice data
            InvoiceService.List();
            var success = await InvoiceService.GetDataByIdAsync(invoiceUuid);
            if (!success) return false;

            if (Data == null)
                NewData();
            Data.InvoiceData = InvoiceService.Data;
            Data.InvoiceTransaction.InvoiceUuid = InvoiceService.Data.InvoiceHeader.InvoiceUuid;

            return success;
        }
        #endregion load original invoice

        #region Load Return qty

        /// <summary>
        /// Load returned qty for each trans return item 
        /// </summary>
        /// <returns></returns>
        protected virtual void LoadReturnedQty()
        {
            if (this.Data.InvoiceReturnItems == null || this.Data.InvoiceReturnItems.Count == 0)
                return;

            var returnItems = InvoiceTransactionHelper.GetReturnItemsByInvoiceUuid(dbFactory, this.Data.InvoiceData.UniqueId);
            if (returnItems == null || returnItems.Count == 0)
                return;

            foreach (var item in this.Data.InvoiceReturnItems)
            {
                item.ReturnedQty = returnItems.Where(i => i.sku == item.SKU && i.rowNum != item.RowNum).Sum(j => j.returnQty).ToQty();//+ item.ReturnQty;
            }
        }

        /// <summary>
        /// Load 
        /// </summary>
        /// <param name="insvoideData"></param>
        /// <returns></returns>
        public virtual async Task<bool> LoadReturnedQtyAsync(InvoiceData insvoideData)
        {
            if (insvoideData == null || string.IsNullOrEmpty(insvoideData.InvoiceHeader.InvoiceUuid))
                return false;
            var invoiceItemUuids = insvoideData.InvoiceItems
                .Select(x => (x == null || x.IsEmpty) ? null : x.InvoiceItemsUuid)
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList();
            var rtns = await GetInvoiceItemsReturnedQtyAsync(invoiceItemUuids);
            if (rtns == null || rtns.Count <= 0)
                return false;

            foreach (var item in insvoideData.InvoiceItems)
            {
                if (item == null || item.IsEmpty) continue;
                item.TotalReturnQty = 0;
                if (rtns.TryGetValue(item.InvoiceItemsUuid, out var qty))
                    item.TotalReturnQty = qty;
            }
            return true;
        }

        public async Task<IDictionary<string, decimal>> GetInvoiceItemsReturnedQtyAsync(IList<string> invoiceItemUuids)
        {
            using (var trs = new ScopedTransaction(dbFactory))
                return await InvoiceTransactionHelper.GetInvoiceItemsReturnedQtyAsync(invoiceItemUuids);
        }

        #endregion

        #region Add transaction

        /// <summary>
        /// Add new data.
        /// </summary>
        public virtual async Task<bool> AddAsync(InvoiceTransactionData data = null)
        {
            if (data != null)
                _data = data;

            if (Data is null)
            {
                AddError("transaction data is required.");
                return false;
            }

            // set Add mode 
            _ProcessMode = ProcessingMode.Add;


            //load invoice data.
            if (!(await LoadInvoiceAsync(Data.InvoiceTransaction.InvoiceNumber, Data.InvoiceTransaction.ProfileNum, Data.InvoiceTransaction.MasterAccountNum)))
                return false;

            //Load returned qty for each trans return item
            if (Data.InvoiceTransaction.TransType == (int)TransTypeEnum.Return)
                LoadReturnedQty();

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            if (!await SaveDataAsync())
            {
                AddError($"Save trans failed for InvoiceNumber:{Data.InvoiceTransaction.InvoiceNumber} ");
                return false;
            } 

            //save trans success. then pay invoice. 
            if (!await InvoiceService.UpdateInvoicePaidAmountAsync(Data.InvoiceTransaction))
            {
                AddError($"Update invoice paidAmount and balance failed for InvoiceNumber:{Data.InvoiceTransaction.InvoiceNumber}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(InvoiceTransactionDataDto dto)
        {
            if (dto is null)
            {
                AddError("InvoiceTransaction is required.");
                return false;
            }
            // set Add mode and clear data
            Add();

            if (!Validate(dto))
                return false;

            // load data from dto
            FromDto(dto);

            //load invoice data.
            if (!(LoadInvoice(dto.InvoiceTransaction.InvoiceNumber, dto.InvoiceTransaction.ProfileNum.Value, dto.InvoiceTransaction.MasterAccountNum.Value)))
                return false;

            //Load returned qty for each trans return item 
            if (dto.InvoiceTransaction.TransType == (int)TransTypeEnum.Return)
                LoadReturnedQty();

            // validate data for Add processing
            if (!Validate())
                return false;

            if (!SaveData())
            {
                AddError($"Save trans failed for InvoiceNumber:{Data.InvoiceTransaction.InvoiceNumber} ");
                return false;
            } 

            //save trans success. then pay invoice. 
            if (!InvoiceService.UpdateInvoicePaidAmount(Data.InvoiceTransaction))
            {
                AddError($"Update invoice paidAmount and balance failed for InvoiceNumber:{Data.InvoiceTransaction.InvoiceNumber}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual async Task<bool> AddAsync(InvoiceTransactionDataDto dto)
        {
            if (dto is null)
            {
                AddError("InvoiceTransaction is required.");
                return false;
            }
            // set Add mode and clear data
            Add();

            if (!(await ValidateAsync(dto)))
                return false;

            // load data from dto
            FromDto(dto);

            //load invoice data.
            if (!(await LoadInvoiceAsync(dto.InvoiceTransaction.InvoiceNumber, dto.InvoiceTransaction.ProfileNum.Value, dto.InvoiceTransaction.MasterAccountNum.Value)))
                return false;

            //Load returned qty for each trans return item
            if (dto.InvoiceTransaction.TransType == (int)TransTypeEnum.Return)
                LoadReturnedQty();

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            if (!await SaveDataAsync())
            {
                AddError($"Save trans failed for InvoiceNumber:{Data.InvoiceTransaction.InvoiceNumber} ");
                return false;
            } 

            //save trans success. then pay invoice. 
            if (!await InvoiceService.UpdateInvoicePaidAmountAsync(Data.InvoiceTransaction))
            {
                AddError($"Update invoice paidAmount and balance failed for InvoiceNumber:{Data.InvoiceTransaction.InvoiceNumber}");
                return false;
            }

            return true;
        }

        public virtual bool Add(InvoiceTransactionPayload payload)
        {
            if (payload is null || !payload.HasInvoiceTransaction)
            {
                AddError("InvoiceTransaction is required.");
                return false;
            }

            var transDataDto = payload.InvoiceTransaction;

            // set Add mode and clear data
            Add();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(transDataDto))
                return false;

            // load data from dto
            FromDto(transDataDto);

            //load invoice data.
            if (!LoadInvoice(transDataDto.InvoiceTransaction.InvoiceNumber, payload.ProfileNum, payload.MasterAccountNum))
                return false;

            //Load returned qty for each trans return item 
            if (transDataDto.InvoiceTransaction.TransType == (int)TransTypeEnum.Return)
                LoadReturnedQty();

            // validate data for Add processing
            if (!Validate())
                return false;

            if (!SaveData())
            {
                AddError($"Save trans failed for InvoiceNumber:{Data.InvoiceTransaction.InvoiceNumber} ");
                return false;
            } 

            //save trans success. then pay invoice. 
            if (!InvoiceService.UpdateInvoicePaidAmount(Data.InvoiceTransaction))
            {
                AddError($"Update invoice paidAmount and balance failed for InvoiceNumber:{Data.InvoiceTransaction.InvoiceNumber}");
                return false;
            }

            return true;
        }

        public virtual async Task<bool> AddAsync(InvoiceTransactionPayload payload)
        {
            if (payload is null || !payload.HasInvoiceTransaction)
            {
                AddError($"InvoiceTransaction is required.");
                return false;
            }

            var transDataDto = payload.InvoiceTransaction;

            // set Add mode and clear data
            Add();

            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(transDataDto)))
                return false;

            // load data from dto
            FromDto(transDataDto);

            //load invoice data.
            if (!(await LoadInvoiceAsync(transDataDto.InvoiceTransaction.InvoiceNumber, payload.ProfileNum, payload.MasterAccountNum)))
                return false;

            //Load returned qty for each trans return item
            if (transDataDto.InvoiceTransaction.TransType == (int)TransTypeEnum.Return)
                LoadReturnedQty();

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            if (!await SaveDataAsync())
            {
                AddError($"Save trans failed for InvoiceNumber:{Data.InvoiceTransaction.InvoiceNumber} ");
                return false;
            } 

            //save trans success. then pay invoice. 
            if (!await InvoiceService.UpdateInvoicePaidAmountAsync(Data.InvoiceTransaction))
            {
                AddError($"Update invoice paidAmount and balance failed for InvoiceNumber:{Data.InvoiceTransaction.InvoiceNumber}");
                return false;
            }

            return true;
        }

        #endregion

        #region update transaction 

        /// <summary>
        /// Update data from Dto object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(InvoiceTransactionDataDto dto)
        {
            if (dto is null || !dto.HasInvoiceTransaction)
            {
                AddError("InvoiceTransaction is required.");
                return false;
            }
            //set edit mode before validate
            Edit();
            if (!Validate(dto))
                return false;

            // load data 
            GetData(dto.InvoiceTransaction.RowNum.ToLong());

            // Keep a copy of Original Paid Amount
            Data.InvoiceTransaction.OriginalPaidAmount = Data.InvoiceTransaction.TotalAmount;
            lastTransUuidBeforeUpdate = Data.InvoiceTransaction.TransUuid;
            lastPaidByBeforeUpdate = Data.InvoiceTransaction.PaidBy;
            lastAuthCodeBeforeUpdate = Data.InvoiceTransaction.AuthCode;

            // load data from dto
            FromDto(dto);

            //load invoice data.
            if (!LoadInvoice(Data.InvoiceTransaction.InvoiceNumber, dto.InvoiceTransaction.ProfileNum.Value, dto.InvoiceTransaction.MasterAccountNum.Value))
                return false;

            //Load returned qty for each trans return item
            if (Data.InvoiceTransaction.TransType == (int)TransTypeEnum.Return)
                LoadReturnedQty();

            // validate data for Add processing
            if (!Validate())
                return false;

            if (!SaveData())
            {
                AddError($"Save trans failed for InvoiceNumber:{Data.InvoiceTransaction.InvoiceNumber} ");
                return false;
            } 

            //save trans success. then pay invoice. 
            if (!InvoiceService.UpdateInvoicePaidAmount(Data.InvoiceTransaction))
            {
                AddError($"Update invoice paidAmount and balance failed for InvoiceNumber:{Data.InvoiceTransaction.InvoiceNumber}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(InvoiceTransactionDataDto dto)
        {
            if (dto is null || !dto.HasInvoiceTransaction)
            {
                AddError("InvoiceTransaction is required.");
                return false;
            }
            //set edit mode before validate
            Edit();
            if (!(await ValidateAsync(dto)))
                return false;

            // load data 
            await GetDataAsync(dto.InvoiceTransaction.RowNum.ToLong());

            // Keep a copy of Original Paid Amount
            Data.InvoiceTransaction.OriginalPaidAmount = Data.InvoiceTransaction.TotalAmount;
            lastTransUuidBeforeUpdate = Data.InvoiceTransaction.TransUuid;
            lastPaidByBeforeUpdate = Data.InvoiceTransaction.PaidBy;
            lastAuthCodeBeforeUpdate = Data.InvoiceTransaction.AuthCode;

            // load data from dto
            FromDto(dto);

            //load invoice data.
            if (!(await LoadInvoiceAsync(Data.InvoiceTransaction.InvoiceNumber, Data.InvoiceTransaction.ProfileNum, Data.InvoiceTransaction.MasterAccountNum)))
                return false;

            //Load returned qty for each trans return item 
            if (Data.InvoiceTransaction.TransType == (int)TransTypeEnum.Return)
                LoadReturnedQty();

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            if (!await SaveDataAsync())
            {
                AddError($"Save trans failed for InvoiceNumber:{Data.InvoiceTransaction.InvoiceNumber} ");
                return false;
            } 

            //save trans success. then pay invoice. 
            if (!await InvoiceService.UpdateInvoicePaidAmountAsync(Data.InvoiceTransaction))
            {
                AddError($"Update invoice paidAmount and balance failed for InvoiceNumber:{Data.InvoiceTransaction.InvoiceNumber}");
                return false;
            }

            return true;
        }

        protected int lastPaidByBeforeUpdate;
        protected string lastAuthCodeBeforeUpdate;
        protected string lastTransUuidBeforeUpdate;
        /// <summary>
        /// Update data from Payload object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(InvoiceTransactionPayload payload)
        {
            if (payload is null || !payload.HasInvoiceTransaction || payload.InvoiceTransaction.InvoiceTransaction.RowNum.ToLong() <= 0)
            {
                AddError("InvoiceTransaction is required.");
                return false;
            }
            //set edit mode before validate
            Edit();

            if (!ValidateAccount(payload))
                return false;

            if (!Validate(payload.InvoiceTransaction))
                return false;

            // load data 
            GetData(payload.InvoiceTransaction.InvoiceTransaction.RowNum.ToLong());

            // Keep a copy of Original Paid Amount
            Data.InvoiceTransaction.OriginalPaidAmount = Data.InvoiceTransaction.TotalAmount;
            lastTransUuidBeforeUpdate = Data.InvoiceTransaction.TransUuid;
            lastPaidByBeforeUpdate = Data.InvoiceTransaction.PaidBy;
            lastAuthCodeBeforeUpdate = Data.InvoiceTransaction.AuthCode;

            // load data from dto
            FromDto(payload.InvoiceTransaction);

            //load invoice data.
            if (!LoadInvoice(Data.InvoiceTransaction.InvoiceNumber, payload.ProfileNum, payload.MasterAccountNum))
                return false;

            //Load returned qty for each trans return item 
            if (Data.InvoiceTransaction.TransType == (int)TransTypeEnum.Return)
                LoadReturnedQty();

            // validate data for Add processing
            if (!Validate())
                return false;

            if (!SaveData())
            {
                AddError($"Save trans failed for InvoiceNumber:{Data.InvoiceTransaction.InvoiceNumber} ");
                return false;
            } 

            //save trans success. then pay invoice. 
            if (!InvoiceService.UpdateInvoicePaidAmount(Data.InvoiceTransaction))
            {
                AddError($"Update invoice paidAmount and balance failed for InvoiceNumber:{Data.InvoiceTransaction.InvoiceNumber}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(InvoiceTransactionPayload payload)
        {
            if (payload is null || !payload.HasInvoiceTransaction)
            {
                AddError("InvoiceTransaction is required.");
                return false;
            }

            //set edit mode before validate
            Edit();

            if (!(await ValidateAccountAsync(payload)))
                return false;

            if (!(await ValidateAsync(payload.InvoiceTransaction)))
                return false;

            // load data 
            if (!(await GetDataAsync(payload.InvoiceTransaction.InvoiceTransaction.RowNum.ToLong())))
                return false;

            // Keep a copy of Original Paid Amount
            Data.InvoiceTransaction.OriginalPaidAmount = Data.InvoiceTransaction.TotalAmount;
            lastTransUuidBeforeUpdate = Data.InvoiceTransaction.TransUuid;
            lastPaidByBeforeUpdate = Data.InvoiceTransaction.PaidBy;
            lastAuthCodeBeforeUpdate = Data.InvoiceTransaction.AuthCode;

            // load data from dto
            FromDto(payload.InvoiceTransaction);

            //load invoice data.
            if (!(await LoadInvoiceAsync(Data.InvoiceTransaction.InvoiceNumber, payload.ProfileNum, payload.MasterAccountNum)))
                return false;

            //Load returned qty for each trans return item 
            if (Data.InvoiceTransaction.TransType == (int)TransTypeEnum.Return)
                LoadReturnedQty();

            // validate data for Add processing
            if (!(await ValidateAsync()))
                return false;

            if (!await SaveDataAsync())
            {
                AddError($"Save trans failed for InvoiceNumber:{Data.InvoiceTransaction.InvoiceNumber} ");
                return false;
            }

            //save trans success. then pay invoice. 
            if (!await InvoiceService.UpdateInvoicePaidAmountAsync(Data.InvoiceTransaction))
            {
                AddError($"Update invoice paidAmount and balance failed for InvoiceNumber:{Data.InvoiceTransaction.InvoiceNumber}");
                return false;
            }

            return true;
        }

        #endregion

        #region get by invoice number 
        protected virtual async Task<List<InvoiceTransactionData>> GetDataListAsync(int masterAccountNum, int profileNum, string invoiceNumber, TransTypeEnum transType, int? transNum = null)
        {
            List();
            if (string.IsNullOrEmpty(invoiceNumber)) return null;
            //LoadInvoice(invoiceNumber, profileNum, masterAccountNum);
            return await _data.GetDataListAsync(invoiceNumber, masterAccountNum, profileNum, transType, transNum);
        }
        protected virtual async Task<List<InvoiceTransactionDataDto>> GetDtoListAsync(int masterAccountNum, int profileNum, string invoiceNumber, TransTypeEnum transType, int? transNum = null)
        {
            if (string.IsNullOrEmpty(invoiceNumber)) return null;
            var dataList = await GetDataListAsync(masterAccountNum, profileNum, invoiceNumber, transType, transNum);
            if (dataList == null || dataList.Count == 0) return null;
            var dtoList = new List<InvoiceTransactionDataDto>();
            foreach (var dataItem in dataList)
            {
                var dtoItem = new InvoiceTransactionDataDto();
                dtoList.Add(this.DtoMapper.WriteDto(dataItem, dtoItem));
            }
            return dtoList;
        }

        /// <summary>
        /// Get InvoiceHeader by invoiceNumber//TODO move to invoice service
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        protected async Task<InvoiceHeaderDto> GetInvoiceHeaderAsync(int masterAccountNum, int profileNum, string invoiceNumber)
        {
            var invoiceHeader = await new InvoiceHeader(_dbFactory).GetByInvoiceNumberAsync(invoiceNumber, masterAccountNum, profileNum);
            var dto = new InvoiceHeaderDto();
            if (invoiceHeader != null)
                new InvoiceDataDtoMapperDefault().WriteInvoiceHeader(invoiceHeader, dto);
            return dto;
        }
        protected async Task<List<InvoiceHeaderDto>> GetInvoiceHeadersByCustomerAsync(int masterAccountNum, int profileNum, string customerCode)
        {
            var invoiceHeaders = await new InvoiceHeader(_dbFactory).GetByCustomerCodeAsync(customerCode, masterAccountNum, profileNum);
            List<InvoiceHeaderDto> result = new List<InvoiceHeaderDto>();
            var mapper = new InvoiceDataDtoMapperDefault();
            invoiceHeaders.ForEach(invoice =>
            {
                var dto = new InvoiceHeaderDto();
                if (invoiceHeaders.Any())
                {
                    mapper.WriteInvoiceHeader(invoice, dto);
                    result.Add(dto);
                }
            });

            return result;
        }
        protected async Task<bool> GetByNumberAsync(InvoiceTransactionPayload payload, string invoiceNumber, TransTypeEnum transType, int transNum)
        {
            return await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, invoiceNumber, transType, transNum);
        }
        protected async Task<bool> GetByNumberAsync(int masterAccountNum, int profileNum, string invoiceNumber, TransTypeEnum transType, int transNum)
        {
            //List();
            var success = await base.GetByNumberAsync(masterAccountNum, profileNum, invoiceNumber, (int)transType, transNum);
            await LoadInvoiceAsync(invoiceNumber, profileNum, masterAccountNum);
            return success;
        }
        #endregion


        /// <summary>
        /// Delete invoice by invoice number
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByNumberAsync(InvoiceTransactionPayload payload, string invoiceNumber, TransTypeEnum transType, int transNum)
        {
            //set delete mode
            Delete();
            //load data
            var success = await GetByNumberAsync(payload.MasterAccountNum, payload.ProfileNum, invoiceNumber, (int)transType, transNum);
            success = success && DeleteData();
            return success;
        }

        /// <summary>
        /// Delete data by number
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public virtual bool DeleteByNumber(InvoiceTransactionPayload payload, string invoiceNumber, TransTypeEnum transType, int transNum)
        {
            if (string.IsNullOrEmpty(invoiceNumber))
                return false;
            //set delete mode
            Delete();
            //load data
            var success = GetByNumber(payload.MasterAccountNum, payload.ProfileNum, invoiceNumber, (int)transType, transNum);
            success = success && DeleteData();
            return success;
        }

        #region copy invoice info to invoicereturn/invoicepayment for NewReturn/ NewPayment

        protected void CopyInvoiceHeaderToTrans()
        {
            var invoiceHeader = Data.InvoiceData.InvoiceHeader;
            Data.InvoiceTransaction = new InvoiceTransaction()
            {
                DatabaseNum = invoiceHeader.DatabaseNum,
                MasterAccountNum = invoiceHeader.MasterAccountNum,
                ProfileNum = invoiceHeader.ProfileNum,

                Currency = invoiceHeader.Currency,
                ExchangeRate = 1,
                InvoiceNumber = invoiceHeader.InvoiceNumber,
                InvoiceUuid = invoiceHeader.InvoiceUuid,
                TaxRate = invoiceHeader.TaxRate,
                DiscountRate = invoiceHeader.DiscountRate,
                DiscountAmount = invoiceHeader.DiscountAmount,

                SubTotalAmount = 0,
                SalesAmount = 0,
                TotalAmount = 0,
                TaxableAmount = 0,
                NonTaxableAmount = 0,
                TaxAmount = 0,
                ShippingAmount = 0,
                ShippingTaxAmount = 0,
                MiscAmount = 0,
                MiscTaxAmount = 0,
                ChargeAndAllowanceAmount = 0
            };
        }
        protected void CopyInvoiceItemsToReturnItems()
        {
            var returnItems = new List<InvoiceReturnItems>();
            foreach (var item in Data.InvoiceData.InvoiceItems)
            {
                // Data.InvoiceReturnItems.
                var returnItem = new InvoiceReturnItems()
                {
                    Currency = item.Currency,
                    DamageWarehouseCode = item.WarehouseCode,
                    DamageWarehouseUuid = item.WarehouseUuid,
                    Description = item.Description,
                    InvoiceDiscountAmount = item.DiscountAmount,
                    InvoiceDiscountPrice = item.DiscountPrice,
                    InvoiceItemsUuid = item.InvoiceItemsUuid,
                    InvoiceUuid = item.InvoiceUuid,
                    InvoiceWarehouseCode = item.WarehouseCode,
                    InvoiceWarehouseUuid = item.WarehouseUuid,
                    PutBackWarehouseCode = item.WarehouseCode,
                    PutBackWarehouseUuid = item.WarehouseUuid,

                    SKU = item.SKU,
                    ProductUuid = item.ProductUuid,
                    InventoryUuid = item.InventoryUuid,
                    WarehouseCode = item.WarehouseCode,
                    WarehouseUuid = item.WarehouseUuid,
                    LotNum = item.LotNum,
                    Notes = item.Notes,
                    UOM = item.UOM,
                    PackType = item.PackType,
                    PackQty = item.PackQty,
                    ReturnedQty = item.TotalReturnQty,
                    ReceivePack = 0,
                    StockPack = 0,
                    NonStockPack = 0,
                    ReturnQty = 0,
                    ReceiveQty = 0,
                    StockQty = 0,
                    NonStockQty = 0,

                    Price = item.DiscountPrice,
                    ReturnItemType = item.InvoiceItemType,
                    TaxRate = item.TaxRate,
                    IsAr = item.IsAr,
                    Stockable = item.Stockable,
                    ExtAmount = 0,
                    TaxableAmount = 0,
                    NonTaxableAmount = 0,
                    TaxAmount = 0,
                    ShippingAmount = 0,
                    ShippingTaxAmount = 0,
                    MiscAmount = 0,
                    MiscTaxAmount = 0,
                    ChargeAndAllowanceAmount = 0
                };
                returnItems.Add(returnItem);
            }
            Data.InvoiceReturnItems = returnItems;
        }

        #endregion

        public async override Task<bool> GetDataByIdAsync(string id)
        {
            if (!await base.GetDataByIdAsync(id))
                return false;

            var trans = Data.InvoiceTransaction;
            return await LoadInvoiceAsync(trans.InvoiceNumber, trans.ProfileNum, trans.MasterAccountNum);
        }   
    }
}