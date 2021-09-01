

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
    /// Represents a default SalesOrderService Validator class.
    /// </summary>
    public partial class SalesOrderServiceValidatorDefault : IValidator<SalesOrderData, SalesOrderDataDto>, IMessage
    {
        public virtual bool IsValid { get; set; }
        public SalesOrderServiceValidatorDefault() { }
        public SalesOrderServiceValidatorDefault(IMessage serviceMessage, IDataBaseFactory dbFactory)
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
            var pl = payload as SalesOrderPayload;
            var dto = pl.SalesOrder;

            if (processingMode == ProcessingMode.Add)
            {
                //For Add mode is,set MasterAccountNum, ProfileNum and DatabaseNum from payload to dto
                dto.SalesOrderHeader.MasterAccountNum = pl.MasterAccountNum;
                dto.SalesOrderHeader.ProfileNum = pl.ProfileNum;
                dto.SalesOrderHeader.DatabaseNum = pl.DatabaseNum;
            }
            else
            {
                using (var tx = new ScopedTransaction(dbFactory))
                {
                    //For other mode is,check number is belong to MasterAccountNum, ProfileNum and DatabaseNum from payload
                    if (!string.IsNullOrEmpty(number))
                        isValid = SalesOrderHelper.ExistNumber(number, pl.MasterAccountNum, pl.ProfileNum);
                    else if (!dto.SalesOrderHeader.RowNum.IsZero())
                        isValid = SalesOrderHelper.ExistRowNum(dto.SalesOrderHeader.RowNum.ToLong(), pl.MasterAccountNum, pl.ProfileNum);
                    if (!isValid)
                        AddError($"Data not found.");
                }
            }
            IsValid = isValid;
            return isValid;
        }

        public virtual async Task<bool> ValidateAccountAsync(IPayload payload, string number = null, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            var isValid = true;
            var pl = payload as SalesOrderPayload;
            var dto = pl.SalesOrder;

            if (processingMode == ProcessingMode.Add)
            {
                //For Add mode is,set MasterAccountNum, ProfileNum and DatabaseNum from payload to dto
                dto.SalesOrderHeader.MasterAccountNum = pl.MasterAccountNum;
                dto.SalesOrderHeader.ProfileNum = pl.ProfileNum;
                dto.SalesOrderHeader.DatabaseNum = pl.DatabaseNum;
            }
            else
            {
                using (var tx = new ScopedTransaction(dbFactory))
                {
                    //For other mode is,check number is belong to MasterAccountNum, ProfileNum and DatabaseNum from payload
                    if (!string.IsNullOrEmpty(number))
                        isValid = await SalesOrderHelper.ExistNumberAsync(number, pl.MasterAccountNum, pl.ProfileNum);
                    else if (!dto.SalesOrderHeader.RowNum.IsZero())
                        isValid = await SalesOrderHelper.ExistRowNumAsync(dto.SalesOrderHeader.RowNum.ToLong(), pl.MasterAccountNum, pl.ProfileNum);
                    if (!isValid)
                        AddError($"Data not found.");
                }
            }
            IsValid = isValid;
            return isValid;
        }

        #region validate data

        public virtual bool Validate(SalesOrderData data, ProcessingMode processingMode = ProcessingMode.Edit)
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
        protected virtual bool ValidateAllMode(SalesOrderData data)
        {
            var dbFactory = data.dbFactory;
            if (string.IsNullOrEmpty(data.SalesOrderHeader.SalesOrderUuid))
            {
                IsValid = false;
                AddError($"Unique Id cannot be empty.");
                return IsValid;
            }
            if (string.IsNullOrEmpty(data.SalesOrderHeader.CustomerCode))
            {
                IsValid = false;
                AddError($"CustomerCode cannot be empty.");
                return IsValid;
            }

            if (data.SalesOrderItems != null && data.SalesOrderItems.Count > 0)
            {
                if (data.SalesOrderItems.Count(i => string.IsNullOrEmpty(i.SKU)) > 0)
                {
                    IsValid = false;
                    AddError($"SKU cannot be empty.");
                    return IsValid;
                }
                //TODO  check logic
                else if (data.SalesOrderItems.Count > data.SalesOrderItems.Select(i => i.SKU).Distinct().Count())
                {
                    IsValid = false;
                    AddError($"SKU is duplicate.");
                    return IsValid;
                }
            }

            return true;

        }

        protected virtual bool ValidateAdd(SalesOrderData data)
        {
            var dbFactory = data.dbFactory;
            if (data.SalesOrderHeader.RowNum != 0 && dbFactory.Exists<SalesOrderHeader>(data.SalesOrderHeader.RowNum))
            {
                IsValid = false;
                AddError($"RowNum: {data.SalesOrderHeader.RowNum} is duplicate.");
                return IsValid;
            }


            //if (string.IsNullOrEmpty(data.SalesOrderHeader.OrderNumber))
            //{
            //    data.SalesOrderHeader.OrderNumber = NumberGenerate.Generate();
            //}
            if (!string.IsNullOrEmpty(data.SalesOrderHeader.OrderNumber))
            {
                using (var tx = new ScopedTransaction(dbFactory))
                {
                    if (SalesOrderHelper.ExistNumber(data.SalesOrderHeader.OrderNumber, data.SalesOrderHeader.ProfileNum.ToInt()))
                    {
                        IsValid = false;
                        AddError("Order Number is duplicate.");
                        return IsValid;
                    }
                }
            }
            return true;

        }

        protected virtual bool ValidateEdit(SalesOrderData data)
        {
            var dbFactory = data.dbFactory;
            if (data.SalesOrderHeader.RowNum.IsZero())
            {
                IsValid = false;
                AddError($"RowNum: {data.SalesOrderHeader.RowNum} not found.");
                return IsValid;
            }
            else if (!dbFactory.Exists<SalesOrderHeader>(data.SalesOrderHeader.RowNum))
            {
                IsValid = false;
                AddError($"RowNum: {data.SalesOrderHeader.RowNum} not found.");
                return IsValid;
            }
            return true;
        }

        protected virtual bool ValidateDelete(SalesOrderData data)
        {
            var dbFactory = data.dbFactory;
            if (data.SalesOrderHeader.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.SalesOrderHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.SalesOrderHeader.RowNum != 0 && !dbFactory.Exists<SalesOrderHeader>(data.SalesOrderHeader.RowNum))
            {
                IsValid = false;
                AddError($"RowNum: {data.SalesOrderHeader.RowNum} not found.");
                return IsValid;
            }
            return true;
        }

        #endregion

        #region Async validate data

        public virtual async Task<bool> ValidateAsync(SalesOrderData data, ProcessingMode processingMode = ProcessingMode.Edit)
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

        protected virtual async Task<bool> ValidateAllModeAsync(SalesOrderData data)
        {
            var dbFactory = data.dbFactory;
            if (string.IsNullOrEmpty(data.SalesOrderHeader.SalesOrderUuid))
            {
                IsValid = false;
                AddError($"Unique Id cannot be empty.");
                return IsValid;
            }
            if (string.IsNullOrEmpty(data.SalesOrderHeader.CustomerCode))
            {
                IsValid = false;
                AddError($"CustomerCode cannot be empty.");
                return IsValid;
            }
            if (data.SalesOrderItems != null && data.SalesOrderItems.Count > 0)
            {
                if (data.SalesOrderItems.Count(i => string.IsNullOrEmpty(i.SKU)) > 0)
                {
                    IsValid = false;
                    AddError($"SKU cannot be empty.");
                    return IsValid;
                }
                //TODO  check logic
                else if (data.SalesOrderItems.Count > data.SalesOrderItems.Select(i => i.SKU).Distinct().Count())
                {
                    IsValid = false;
                    AddError($"SKU is duplicate.");
                    return IsValid;
                }
            }
            return true;

        }

        protected virtual async Task<bool> ValidateAddAsync(SalesOrderData data)
        {
            var dbFactory = data.dbFactory;
            if (data.SalesOrderHeader.RowNum != 0 && (await dbFactory.ExistsAsync<SalesOrderHeader>(data.SalesOrderHeader.RowNum)))
            {
                IsValid = false;
                AddError($"RowNum: {data.SalesOrderHeader.RowNum} is duplicate.");
                return IsValid;
            }

            //if (string.IsNullOrEmpty(data.SalesOrderHeader.OrderNumber))
            //{
            //    data.SalesOrderHeader.OrderNumber = NumberGenerate.Generate();
            //}
            if (!string.IsNullOrEmpty(data.SalesOrderHeader.OrderNumber))
            {
                using (var tx = new ScopedTransaction(dbFactory))
                {
                    if (await SalesOrderHelper.ExistNumberAsync(data.SalesOrderHeader.OrderNumber, data.SalesOrderHeader.ProfileNum.ToInt()))
                    {
                        IsValid = false;
                        AddError("Order Number is duplicate.");
                        return IsValid;
                    }
                }
            }
            return true;

        }

        protected virtual async Task<bool> ValidateEditAsync(SalesOrderData data)
        {
            var dbFactory = data.dbFactory;
            if (data.SalesOrderHeader.RowNum.IsZero())
            {
                IsValid = false;
                AddError($"RowNum: {data.SalesOrderHeader.RowNum} not found.");
                return IsValid;
            }
            else if (!(await dbFactory.ExistsAsync<SalesOrderHeader>(data.SalesOrderHeader.RowNum)))
            {
                IsValid = false;
                AddError($"RowNum: {data.SalesOrderHeader.RowNum} not found.");
                return IsValid;
            }
            return true;
        }

        protected virtual async Task<bool> ValidateDeleteAsync(SalesOrderData data)
        {
            var dbFactory = data.dbFactory;
            if (data.SalesOrderHeader.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.SalesOrderHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.SalesOrderHeader.RowNum != 0 && !(await dbFactory.ExistsAsync<SalesOrderHeader>(data.SalesOrderHeader.RowNum)))
            {
                IsValid = false;
                AddError($"RowNum: {data.SalesOrderHeader.RowNum} not found.");
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
        public virtual bool Validate(SalesOrderDataDto dto, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            var isValid = true;
            if (dto is null)
            {
                isValid = false;
                AddError($"Data not found");
            }
            if (processingMode == ProcessingMode.Edit)
            {
                if (dto.SalesOrderHeader.RowNum.IsZero())
                {
                    isValid = false;
                    AddError("SalesOrderHeader.RowNum is required.");
                }
                // This property should not be changed.
                dto.SalesOrderHeader.MasterAccountNum = null;
                dto.SalesOrderHeader.ProfileNum = null;
                dto.SalesOrderHeader.DatabaseNum = null;
                dto.SalesOrderHeader.SalesOrderUuid = null;
                dto.SalesOrderHeader.OrderNumber = null;
                if (dto.SalesOrderItems != null && dto.SalesOrderItems.Count > 0)
                {
                    foreach (var detailItem in dto.SalesOrderItems)
                        detailItem.SalesOrderItemsUuid = null;
                }
            }
            IsValid = isValid;
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
        public virtual async Task<bool> ValidateAsync(SalesOrderDataDto dto, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            var isValid = true;
            if (dto is null)
            {
                isValid = false;
                AddError($"Data not found");
            }
            if (processingMode == ProcessingMode.Edit)
            {
                if (dto.SalesOrderHeader.RowNum.IsZero())
                {
                    isValid = false;
                    AddError("SalesOrderHeader.RowNum is required.");
                }
                // This property should not be changed.
                dto.SalesOrderHeader.MasterAccountNum = null;
                dto.SalesOrderHeader.ProfileNum = null;
                dto.SalesOrderHeader.DatabaseNum = null;
                dto.SalesOrderHeader.SalesOrderUuid = null;
                dto.SalesOrderHeader.OrderNumber = null;
                if (dto.SalesOrderItems != null && dto.SalesOrderItems.Count > 0)
                {
                    foreach (var detailItem in dto.SalesOrderItems)
                        detailItem.SalesOrderItemsUuid = null;
                }
            }
            IsValid = isValid;
            return isValid;
        }
        #endregion
    }
}



