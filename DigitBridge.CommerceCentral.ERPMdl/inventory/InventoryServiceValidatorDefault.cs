    

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
    /// Represents a default InventoryService Validator class.
    /// </summary>
    public partial class InventoryServiceValidatorDefault : IValidator<InventoryData,InventoryDataDto>, IMessage
    {
        public virtual bool IsValid { get; set; }
        public InventoryServiceValidatorDefault() { }
        public InventoryServiceValidatorDefault(IMessage serviceMessage, IDataBaseFactory dbFactory) 
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
            var pl = payload as InventoryPayload;
            var dto = pl.Inventory;

            if (processingMode == ProcessingMode.Add)
            {
                //For Add mode is,set MasterAccountNum, ProfileNum and DatabaseNum from payload to dto
                dto.ProductBasic.MasterAccountNum = pl.MasterAccountNum;
                dto.ProductBasic.ProfileNum = pl.ProfileNum;
                dto.ProductBasic.DatabaseNum = pl.DatabaseNum;
            }
            else
            {
                //For other mode is,check number is belong to MasterAccountNum, ProfileNum and DatabaseNum from payload
                using (var tx = new ScopedTransaction(dbFactory))
                {
                    if (number == null)
                        isValid = InventoryServiceHelper.ExistId(dto.ProductBasic.ProductUuid, pl.MasterAccountNum, pl.ProfileNum);
                    else
                        isValid = InventoryServiceHelper.ExistNumber(number, pl.MasterAccountNum, pl.ProfileNum);
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
            var pl = payload as InventoryPayload;
            var dto = pl.Inventory;

            if (processingMode == ProcessingMode.Add)
            {
                //For Add mode is,set MasterAccountNum, ProfileNum and DatabaseNum from payload to dto
                dto.ProductBasic.MasterAccountNum = pl.MasterAccountNum;
                dto.ProductBasic.ProfileNum = pl.ProfileNum;
                dto.ProductBasic.DatabaseNum = pl.DatabaseNum;
            }
            else
            {
                //For other mode is,check number is belong to MasterAccountNum, ProfileNum and DatabaseNum from payload
                using (var tx = new ScopedTransaction(dbFactory))
                {
                    if (number == null)
                        isValid = await InventoryServiceHelper.ExistIdAsync(dto.ProductBasic.ProductUuid, pl.MasterAccountNum, pl.ProfileNum).ConfigureAwait(false);
                    else
                        isValid = await InventoryServiceHelper.ExistNumberAsync(number, pl.MasterAccountNum, pl.ProfileNum).ConfigureAwait(false);
                }
                if (!isValid)
                    AddError($"Data not found.");
            }
            IsValid = isValid;
            return isValid;
        }

        #region validate data

        public virtual bool Validate(InventoryData data, ProcessingMode processingMode = ProcessingMode.Edit)
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
        protected virtual bool ValidateAllMode(InventoryData data)
        {
            var dbFactory = data.dbFactory;
            if (string.IsNullOrEmpty(data.ProductBasic.ProductUuid))
            {
                IsValid = false;
                AddError($"Unique Id cannot be empty.");
                return IsValid;
            }
            //if (string.IsNullOrEmpty(data.ProductBasic.CustomerUuid))
            //{
            //    IsValid = false;
            //    AddError($"Customer cannot be empty.");
            //    return IsValid;
            //}
            return true;

        }

        protected virtual bool ValidateAdd(InventoryData data)
        {
            var dbFactory = data.dbFactory;
            if (data.ProductBasic.RowNum != 0 && dbFactory.Exists<ProductBasic>(data.ProductBasic.RowNum))
            {
                IsValid = false;
                AddError($"RowNum: {data.ProductBasic.RowNum} is duplicate.");
            }
            ValidateAddData(data);
            return IsValid;

        }
        protected virtual bool ValidateAddData(InventoryData data)
        {
            var dbFactory = data.dbFactory;
            #region Valid ProductBasic
            if (string.IsNullOrEmpty(data.ProductBasic.SKU) || dbFactory.Db.ExecuteScalar<int>($"SELECT COUNT(1) FROM ProductBasic WHERE SKU='{data.ProductBasic.SKU}' AND MasterAccountNum={data.ProductBasic.MasterAccountNum} AND ProfileNum={data.ProductBasic.ProfileNum}") > 0)
            {
                IsValid = false;
                AddError($"SKU must be unique.");
            }
            #endregion

            #region Valid Inventory
            if (data.Inventory != null && data.Inventory.Count > 0)
            {
                var addressList = data.Inventory.ToList();
                foreach (var inv in data.Inventory)
                {
                    if (string.IsNullOrEmpty(inv.InventoryUuid) || addressList.Count(r => r.InventoryUuid == inv.InventoryUuid) > 1)
                    {
                        IsValid = false;
                        AddError($"Inventory.InventoryUuid cannot be empty and must be unique.");
                    }
                }
            }
            #endregion
            return IsValid;
        }


        protected virtual bool ValidateEdit(InventoryData data)
        {
            var dbFactory = data.dbFactory;
            if (data.ProductBasic.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.ProductBasic.RowNum} not found.");
                return IsValid;
            }

            if (data.ProductBasic.RowNum != 0 && !dbFactory.Exists<ProductBasic>(data.ProductBasic.RowNum))
            {
                IsValid = false;
                AddError($"RowNum: {data.ProductBasic.RowNum} not found.");
                return IsValid;
            }
            return true;
        }

        protected virtual bool ValidateDelete(InventoryData data)
        {
            var dbFactory = data.dbFactory;
            if (data.ProductBasic.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.ProductBasic.RowNum} not found.");
                return IsValid;
            }

            if (data.ProductBasic.RowNum != 0 && !dbFactory.Exists<ProductBasic>(data.ProductBasic.RowNum))
            {
                IsValid = false;
                AddError($"RowNum: {data.ProductBasic.RowNum} not found.");
                return IsValid;
            }
            return true;
        }

        #endregion

        #region Async validate data

        public virtual async Task<bool> ValidateAsync(InventoryData data, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            Clear();
            if (!(await ValidateAllModeAsync(data).ConfigureAwait(false)))
                return false;

            return processingMode switch
            {
                ProcessingMode.Add => await ValidateAddAsync(data).ConfigureAwait(false),
                ProcessingMode.Edit => await ValidateEditAsync(data).ConfigureAwait(false),
                ProcessingMode.List => false,
                ProcessingMode.Delete => await ValidateDeleteAsync(data).ConfigureAwait(false),
                ProcessingMode.Void => await ValidateDeleteAsync(data).ConfigureAwait(false),
                ProcessingMode.Cancel => await ValidateDeleteAsync(data).ConfigureAwait(false),
                _ => false,
            };
        }

        protected virtual async Task<bool> ValidateAllModeAsync(InventoryData data)
        {
            var dbFactory = data.dbFactory;
            if (string.IsNullOrEmpty(data.ProductBasic.ProductUuid))
            {
                IsValid = false;
                AddError($"Unique Id cannot be empty.");
                return IsValid;
            }
            //if (string.IsNullOrEmpty(data.ProductBasic.CustomerUuid))
            //{
            //    IsValid = false;
            //    AddError($"Customer cannot be empty.");
            //    return IsValid;
            //}
            return true;

        }

        protected virtual async Task<bool> ValidateAddAsync(InventoryData data)
        {
            var dbFactory = data.dbFactory;
            if (data.ProductBasic.RowNum != 0 && (await dbFactory.ExistsAsync<ProductBasic>(data.ProductBasic.RowNum)))
            {
                IsValid = false;
                AddError($"RowNum: {data.ProductBasic.RowNum} is duplicate.");
            }
            ValidateAddData(data);
            return IsValid;
        }

        protected virtual async Task<bool> ValidateEditAsync(InventoryData data)
        {
            var dbFactory = data.dbFactory;
            if (data.ProductBasic.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.ProductBasic.RowNum} not found.");
                return IsValid;
            }

            if (data.ProductBasic.RowNum != 0 && !(await dbFactory.ExistsAsync<ProductBasic>(data.ProductBasic.RowNum)))
            {
                IsValid = false;
                AddError($"RowNum: {data.ProductBasic.RowNum} not found.");
                return IsValid;
            }
            return true;
        }

        protected virtual async Task<bool> ValidateDeleteAsync(InventoryData data)
        {
            var dbFactory = data.dbFactory;
            if (data.ProductBasic.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.ProductBasic.RowNum} not found.");
                return IsValid;
            }

            if (data.ProductBasic.RowNum != 0 && !(await dbFactory.ExistsAsync<ProductBasic>(data.ProductBasic.RowNum)))
            {
                IsValid = false;
                AddError($"RowNum: {data.ProductBasic.RowNum} not found.");
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
        public virtual bool Validate(InventoryDataDto dto, ProcessingMode processingMode = ProcessingMode.Edit)
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
                dto.ProductBasic.ProductUuid = Guid.NewGuid().ToString();
                if (dto.Inventory != null && dto.Inventory.Count > 0)
                {
                    foreach (var detailItem in dto.Inventory)
                        detailItem.InventoryUuid = Guid.NewGuid().ToString();
                }
  
            }
            else if (processingMode == ProcessingMode.Edit)
            {
                if (dto.ProductBasic.RowNum.IsZero())
                {
                    isValid = false;
                    AddError("ProductBasic.RowNum is required.");
                }
                // This property should not be changed.
                dto.ProductBasic.MasterAccountNum = null;
                dto.ProductBasic.ProfileNum = null;
                dto.ProductBasic.DatabaseNum = null;
                dto.ProductBasic.ProductUuid = null;
                // TODO 
                //dto.SalesOrderHeader.OrderNumber = null;
                if (dto.Inventory != null && dto.Inventory.Count > 0)
                {
                    foreach (var detailItem in dto.Inventory)
                        detailItem.InventoryUuid = null;
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
        public virtual async Task<bool> ValidateAsync(InventoryDataDto dto, ProcessingMode processingMode = ProcessingMode.Edit)
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
                dto.ProductBasic.ProductUuid = Guid.NewGuid().ToString();
                if (dto.Inventory != null && dto.Inventory.Count > 0)
                {
                    foreach (var detailItem in dto.Inventory)
                        detailItem.InventoryUuid = Guid.NewGuid().ToString();
                }
  
            }
            else if (processingMode == ProcessingMode.Edit)
            {
                if (dto.ProductBasic.RowNum.IsZero())
                {
                    isValid = false;
                    AddError("ProductBasic.RowNum is required.");
                }
                // This property should not be changed.
                dto.ProductBasic.MasterAccountNum = null;
                dto.ProductBasic.ProfileNum = null;
                dto.ProductBasic.DatabaseNum = null;
                dto.ProductBasic.ProductUuid = null;
                // TODO 
                //dto.SalesOrderHeader.OrderNumber = null;
                if (dto.Inventory != null && dto.Inventory.Count > 0)
                {
                    foreach (var detailItem in dto.Inventory)
                        detailItem.InventoryUuid = null;
                }
            }
            IsValid=isValid;
            return isValid;
        }
        #endregion
    }
}



