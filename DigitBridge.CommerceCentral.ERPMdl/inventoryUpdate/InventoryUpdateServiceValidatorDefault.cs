    

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
    /// Represents a default InventoryUpdateService Validator class.
    /// </summary>
    public partial class InventoryUpdateServiceValidatorDefault : IValidator<InventoryUpdateData,InventoryUpdateDataDto>, IMessage
    {
        public virtual bool IsValid { get; set; }
        public InventoryUpdateServiceValidatorDefault() { }
        public InventoryUpdateServiceValidatorDefault(IMessage serviceMessage, IDataBaseFactory dbFactory) 
        { 
            this.ServiceMessage = serviceMessage; 
            this.dbFactory = dbFactory;
        }

        protected IDataBaseFactory dbFactory { get; set; }

        #region message
        [XmlIgnore, JsonIgnore]
        public virtual IList<MessageClass> Messages 
        { 
            get
            {
                if (ServiceMessage != null)
                    return ServiceMessage.Messages;

                if (_Messages == null)
                    _Messages = new List<MessageClass>();
                return _Messages;
            }
            set
            {
                if (ServiceMessage != null)
                    ServiceMessage.Messages = value;
                else
                    _Messages = value;
            }
        }
        protected IList<MessageClass> _Messages;
        public IMessage ServiceMessage { get; set; }
        public IList<MessageClass> AddInfo(string message, string code = null) =>
             ServiceMessage != null ? ServiceMessage.AddInfo(message, code) : Messages.AddInfo(message, code);
        public IList<MessageClass> AddWarning(string message, string code = null) =>
             ServiceMessage != null ? ServiceMessage.AddWarning(message, code) : Messages.AddWarning(message, code);
        public IList<MessageClass> AddError(string message, string code = null) =>
             ServiceMessage != null ? ServiceMessage.AddError(message, code) : Messages.AddError(message, code);
        public IList<MessageClass> AddFatal(string message, string code = null) =>
             ServiceMessage != null ? ServiceMessage.AddFatal(message, code) : Messages.AddFatal(message, code);
        public IList<MessageClass> AddDebug(string message, string code = null) =>
             ServiceMessage != null ? ServiceMessage.AddDebug(message, code) : Messages.AddDebug(message, code);

        #endregion message

        public virtual void Clear()
        {
            IsValid = true;
            Messages = new List<MessageClass>();
        }

        public virtual bool ValidateAccount(IPayload payload, string number = null, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            var isValid = true;
            var pl = payload as InventoryUpdatePayload;
            var dto = pl.InventoryUpdate;

            if (processingMode == ProcessingMode.Add)
            {
                //For Add mode is,set MasterAccountNum, ProfileNum and DatabaseNum from payload to dto
                dto.InventoryUpdateHeader.MasterAccountNum = pl.MasterAccountNum;
                dto.InventoryUpdateHeader.ProfileNum = pl.ProfileNum;
                dto.InventoryUpdateHeader.DatabaseNum = pl.DatabaseNum;
            }
            else
            {
                //For other mode is,check number is belong to MasterAccountNum, ProfileNum and DatabaseNum from payload
                using (var tx = new ScopedTransaction(dbFactory))
                {
                    if (!string.IsNullOrEmpty(number))
                        isValid = InventoryUpdateHelper.ExistNumber(number, pl.MasterAccountNum, pl.ProfileNum);
                    else if(!dto.InventoryUpdateHeader.RowNum.IsZero())
                        isValid = InventoryUpdateHelper.ExistRowNum(dto.InventoryUpdateHeader.RowNum.ToLong(), pl.MasterAccountNum, pl.ProfileNum); 
                }
                if (!isValid)
                    AddError($"Data not found.");
            }
            IsValid = isValid;
            return isValid;
        }

        public virtual async Task<bool> ValidateAccountAsync(IPayload payload, string number = null, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            var isValid = true;
            var pl = payload as InventoryUpdatePayload;
            var dto = pl.InventoryUpdate;

            if (processingMode == ProcessingMode.Add)
            {
                //For Add mode is,set MasterAccountNum, ProfileNum and DatabaseNum from payload to dto
                dto.InventoryUpdateHeader.MasterAccountNum = pl.MasterAccountNum;
                dto.InventoryUpdateHeader.ProfileNum = pl.ProfileNum;
                dto.InventoryUpdateHeader.DatabaseNum = pl.DatabaseNum;
            }
            else
            {
                //For other mode is,check number is belong to MasterAccountNum, ProfileNum and DatabaseNum from payload
                using (var tx = new ScopedTransaction(dbFactory))
                {
                    if (!string.IsNullOrEmpty(number))
                        isValid = await InventoryUpdateHelper.ExistNumberAsync(number, pl.MasterAccountNum, pl.ProfileNum);
                    else if(!dto.InventoryUpdateHeader.RowNum.IsZero())
                        isValid = await InventoryUpdateHelper.ExistRowNumAsync(dto.InventoryUpdateHeader.RowNum.ToLong(), pl.MasterAccountNum, pl.ProfileNum); 
                }
                if (!isValid)
                    AddError($"Data not found.");
            }
            IsValid = isValid;
            return isValid;
        }

        #region validate data

        public virtual bool Validate(InventoryUpdateData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            Clear();
            if (!ValidateAllMode(data))
                return false;

            return processingMode switch
            {
                ProcessingMode.Add => ValidateAdd(data),
                ProcessingMode.Edit => ValidateEdit(data),
                ProcessingMode.List => false,
                ProcessingMode.Delete => ValidateDelete(data),
                ProcessingMode.Void => ValidateDelete(data),
                ProcessingMode.Cancel => ValidateDelete(data),
                _ => false,
            };
        }
        protected virtual bool ValidateAllMode(InventoryUpdateData data)
        {
            var dbFactory = data.dbFactory;
            if (string.IsNullOrEmpty(data.InventoryUpdateHeader.InventoryUpdateUuid))
            {
                IsValid = false;
                AddError($"Unique Id cannot be empty.");
                return IsValid;
            }
            //if (string.IsNullOrEmpty(data.InventoryUpdateHeader.CustomerUuid))
            //{
            //    IsValid = false;
            //    AddError($"Customer cannot be empty.");
            //    return IsValid;
            //}
            return true;

        }

        protected virtual bool ValidateAdd(InventoryUpdateData data)
        {
            var dbFactory = data.dbFactory;
            if (data.InventoryUpdateHeader.RowNum != 0 && dbFactory.Exists<InventoryUpdateHeader>(data.InventoryUpdateHeader.RowNum))
            {
                IsValid = false;
                AddError($"RowNum: {data.InventoryUpdateHeader.RowNum} is duplicate.");
            }
            if (!string.IsNullOrEmpty(data.InventoryUpdateHeader.BatchNumber) && dbFactory.Exists<InventoryUpdateHeader>("BatchNumber=@0", data.InventoryUpdateHeader.BatchNumber.ToParameter("0")))
            {
                IsValid = false;
                AddError($"BatchNumber: {data.InventoryUpdateHeader.BatchNumber} is duplicate.");
            }
            //if (!dbFactory.ExistUniqueId<DistributionCenter>(data.InventoryUpdateHeader.WarehouseUuid))
            //{
            //    IsValid = false;
            //    AddError($"WarehouseUuid: {data.InventoryUpdateHeader.BatchNumber} not found.");
            //}
            if (data.InventoryUpdateItems != null && data.InventoryUpdateItems.Count > 0) { 
                foreach(var item in data.InventoryUpdateItems)
                {
                    if (!item.InventoryUuid.IsZero())
                    {
                        if (!dbFactory.ExistUniqueId<Inventory>(item.InventoryUuid))
                        {
                            IsValid = false;
                            AddError($"InventoryUuid: {item.InventoryUuid} not found.");
                        }
                    }
                    else
                    {
                        if (!dbFactory.Exists<Inventory>("SKU=@0 AND WarehouseCode=@1", item.SKU.ToParameter("0"), item.WarehouseCode.ToParameter("1")))
                        {
                            IsValid = false;
                            AddError($"Inventory SKU {item.SKU} With WarehouseCode {item.WarehouseCode} not found.");
                        }
                    }
                }
            }
            return IsValid;

        }

        protected virtual bool ValidateEdit(InventoryUpdateData data)
        {
            var dbFactory = data.dbFactory;
            if (data.InventoryUpdateHeader.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.InventoryUpdateHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.InventoryUpdateHeader.RowNum != 0 && !dbFactory.Exists<InventoryUpdateHeader>(data.InventoryUpdateHeader.RowNum))
            {
                IsValid = false;
                AddError($"RowNum: {data.InventoryUpdateHeader.RowNum} not found.");
                return IsValid;
            }
            return true;
        }

        protected virtual bool ValidateDelete(InventoryUpdateData data)
        {
            var dbFactory = data.dbFactory;
            if (data.InventoryUpdateHeader.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.InventoryUpdateHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.InventoryUpdateHeader.RowNum != 0 && !dbFactory.Exists<InventoryUpdateHeader>(data.InventoryUpdateHeader.RowNum))
            {
                IsValid = false;
                AddError($"RowNum: {data.InventoryUpdateHeader.RowNum} not found.");
                return IsValid;
            }
            return true;
        }

        #endregion

        #region Async validate data

        public virtual async Task<bool> ValidateAsync(InventoryUpdateData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            Clear();
            if (!(await ValidateAllModeAsync(data)))
                return false;

            return processingMode switch
            {
                ProcessingMode.Add => await ValidateAddAsync(data),
                ProcessingMode.Edit => await ValidateEditAsync(data),
                ProcessingMode.List => false,
                ProcessingMode.Delete => await ValidateDeleteAsync(data),
                ProcessingMode.Void => await ValidateDeleteAsync(data),
                ProcessingMode.Cancel => await ValidateDeleteAsync(data),
                _ => false,
            };
        }

        protected virtual async Task<bool> ValidateAllModeAsync(InventoryUpdateData data)
        {
            var dbFactory = data.dbFactory;
            if (string.IsNullOrEmpty(data.InventoryUpdateHeader.InventoryUpdateUuid))
            {
                IsValid = false;
                AddError($"Unique Id cannot be empty.");
                return IsValid;
            }
            //if (string.IsNullOrEmpty(data.InventoryUpdateHeader.CustomerUuid))
            //{
            //    IsValid = false;
            //    AddError($"Customer cannot be empty.");
            //    return IsValid;
            //}
            return true;

        }

        protected virtual async Task<bool> ValidateAddAsync(InventoryUpdateData data)
        {
            var dbFactory = data.dbFactory;
            if (data.InventoryUpdateHeader.RowNum != 0 && (await dbFactory.ExistsAsync<InventoryUpdateHeader>(data.InventoryUpdateHeader.RowNum)))
            {
                IsValid = false;
                AddError($"RowNum: {data.InventoryUpdateHeader.RowNum} is duplicate.");
                return IsValid;
            }
            if (!string.IsNullOrEmpty(data.InventoryUpdateHeader.BatchNumber) && await dbFactory.ExistsAsync<InventoryUpdateHeader>("BatchNumber=@0",data.InventoryUpdateHeader.BatchNumber.ToParameter("0")))
            {
                IsValid = false;
                AddError($"BatchNumber: {data.InventoryUpdateHeader.BatchNumber} is duplicate.");
            }
            //if (!await dbFactory.ExistUniqueIdAsync<DistributionCenter>(data.InventoryUpdateHeader.WarehouseUuid))
            //{
            //    IsValid = false;
            //    AddError($"WarehouseUuid: {data.InventoryUpdateHeader.BatchNumber} not found.");
            //}
            if (data.InventoryUpdateItems != null && data.InventoryUpdateItems.Count > 0)
            {
                foreach (var item in data.InventoryUpdateItems)
                {
                    if (!item.InventoryUuid.IsZero())
                    {
                        if (!await dbFactory.ExistUniqueIdAsync<Inventory>(item.InventoryUuid))
                        {
                            IsValid = false;
                            AddError($"InventoryUuid: {item.InventoryUuid} not found.");
                        }
                    }
                    else
                    {
                        if (!await dbFactory.ExistsAsync<Inventory>("SKU=@0 AND WarehouseCode=@1",item.SKU.ToParameter("0"),item.WarehouseCode.ToParameter("1")))
                        {
                            IsValid = false;
                            AddError($"Inventory SKU {item.SKU} With WarehouseCode {item.WarehouseCode} not found.");
                        }
                    }
                }
            }
            return IsValid;

        }

        protected virtual async Task<bool> ValidateEditAsync(InventoryUpdateData data)
        {
            var dbFactory = data.dbFactory;
            if (data.InventoryUpdateHeader.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.InventoryUpdateHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.InventoryUpdateHeader.RowNum != 0 && !(await dbFactory.ExistsAsync<InventoryUpdateHeader>(data.InventoryUpdateHeader.RowNum)))
            {
                IsValid = false;
                AddError($"RowNum: {data.InventoryUpdateHeader.RowNum} not found.");
                return IsValid;
            }
            return true;
        }

        protected virtual async Task<bool> ValidateDeleteAsync(InventoryUpdateData data)
        {
            var dbFactory = data.dbFactory;
            if (data.InventoryUpdateHeader.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.InventoryUpdateHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.InventoryUpdateHeader.RowNum != 0 && !(await dbFactory.ExistsAsync<InventoryUpdateHeader>(data.InventoryUpdateHeader.RowNum)))
            {
                IsValid = false;
                AddError($"RowNum: {data.InventoryUpdateHeader.RowNum} not found.");
                return IsValid;
            }
            return true;
        }

        #endregion Async validate data

        #region Validate dto (invoke this before data loaded)
        /// <summary>
        /// Validate dto.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="dbFactory"></param>
        /// <param name="processingMode"></param>
        /// <returns></returns>
        public virtual bool Validate(InventoryUpdateDataDto dto, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            var isValid = true;
            if (dto is null)
            {
                isValid = false;
                AddError($"Data not found");
            }
            if (processingMode == ProcessingMode.Add)
            {
                //for Add mode, always reset uuid
                dto.InventoryUpdateHeader.InventoryUpdateUuid = null;
                if (dto.InventoryUpdateItems != null && dto.InventoryUpdateItems.Count > 0)
                {
                    foreach (var detailItem in dto.InventoryUpdateItems)
                        detailItem.InventoryUpdateItemsUuid = null;
                }
  
            }
            else if (processingMode == ProcessingMode.Edit)
            {
                if (dto.InventoryUpdateHeader.RowNum.IsZero())
                {
                    isValid = false;
                    AddError("InventoryUpdateHeader.RowNum is required.");
                }
                // This property should not be changed.
                dto.InventoryUpdateHeader.MasterAccountNum = null;
                dto.InventoryUpdateHeader.ProfileNum = null;
                dto.InventoryUpdateHeader.DatabaseNum = null;
                dto.InventoryUpdateHeader.InventoryUpdateUuid = null;
                // TODO 
                //dto.SalesOrderHeader.OrderNumber = null;
                if (dto.InventoryUpdateItems != null && dto.InventoryUpdateItems.Count > 0)
                {
                    foreach (var detailItem in dto.InventoryUpdateItems)
                    {
                        detailItem.InventoryUpdateItemsUuid = null;
                        detailItem.WarehouseCode = null;
                        detailItem.WarehouseUuid = null;
                        detailItem.InventoryUuid = null;
                        detailItem.ProductUuid = null;
                    }
                }
            }
            IsValid=isValid;
            return isValid;
        }
        #endregion

        #region async Validate dto (invoke this before data loaded)
        /// <summary>
        /// Validate dto.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="dbFactory"></param>
        /// <param name="processingMode"></param>
        /// <returns></returns>
        public virtual async Task<bool> ValidateAsync(InventoryUpdateDataDto dto, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            var isValid = true;
            if (dto is null)
            {
                isValid = false;
                AddError($"Data not found");
            }
            if (processingMode == ProcessingMode.Add)
            {
                //for Add mode, always reset uuid
                dto.InventoryUpdateHeader.InventoryUpdateUuid =null;
                if (dto.InventoryUpdateItems != null && dto.InventoryUpdateItems.Count > 0)
                {
                    foreach (var detailItem in dto.InventoryUpdateItems)
                        detailItem.InventoryUpdateItemsUuid = null ;
                }
  
            }
            else if (processingMode == ProcessingMode.Edit)
            {
                if (dto.InventoryUpdateHeader.RowNum.IsZero())
                {
                    isValid = false;
                    AddError("InventoryUpdateHeader.RowNum is required.");
                }
                // This property should not be changed.
                dto.InventoryUpdateHeader.MasterAccountNum = null;
                dto.InventoryUpdateHeader.ProfileNum = null;
                dto.InventoryUpdateHeader.DatabaseNum = null;
                dto.InventoryUpdateHeader.InventoryUpdateUuid = null;
                // TODO 
                //dto.SalesOrderHeader.OrderNumber = null;
                if (dto.InventoryUpdateItems != null && dto.InventoryUpdateItems.Count > 0)
                {
                    foreach (var detailItem in dto.InventoryUpdateItems)
                    {
                        detailItem.InventoryUpdateItemsUuid = null;
                        detailItem.WarehouseCode = null;
                        detailItem.WarehouseUuid = null;
                        detailItem.InventoryUuid = null;
                        detailItem.ProductUuid = null;
                    }
                }
  
            }
            IsValid=isValid;
            return isValid;
        }
        #endregion
    }
}



