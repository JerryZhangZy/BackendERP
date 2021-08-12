    

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
    /// Represents a default ChannelOrderService Validator class.
    /// </summary>
    public partial class ChannelOrderServiceValidatorDefault : IValidator<ChannelOrderData,ChannelOrderDataDto>, IMessage
    {
        public virtual bool IsValid { get; set; }
        public ChannelOrderServiceValidatorDefault() { }
        public ChannelOrderServiceValidatorDefault(IMessage serviceMessage, IDataBaseFactory dbFactory) 
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
            var pl = payload as ChannelOrderPayload;
            var dto = pl.ChannelOrder;

            if (processingMode == ProcessingMode.Add)
            {
                //For Add mode is,set MasterAccountNum, ProfileNum and DatabaseNum from payload to dto
                dto.OrderHeader.MasterAccountNum = pl.MasterAccountNum;
                dto.OrderHeader.ProfileNum = pl.ProfileNum;
                dto.OrderHeader.DatabaseNum = pl.DatabaseNum;
            }
            else
            {
                using (var tx = new ScopedTransaction(dbFactory))
                {
                    //For other mode is,check number is belong to MasterAccountNum, ProfileNum and DatabaseNum from payload
                    if (!string.IsNullOrEmpty(number))
                        isValid = SalesOrderHelper.ExistNumber(number, pl.MasterAccountNum, pl.ProfileNum);
                    else if (!dto.OrderHeader.RowNum.IsZero())
                        isValid = SalesOrderHelper.ExistRowNum(dto.OrderHeader.RowNum.ToLong(), pl.MasterAccountNum, pl.ProfileNum);
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
            var pl = payload as ChannelOrderPayload;
            var dto = pl.ChannelOrder;

            if (processingMode == ProcessingMode.Add)
            {
                //For Add mode is,set MasterAccountNum, ProfileNum and DatabaseNum from payload to dto
                dto.OrderHeader.MasterAccountNum = pl.MasterAccountNum;
                dto.OrderHeader.ProfileNum = pl.ProfileNum;
                dto.OrderHeader.DatabaseNum = pl.DatabaseNum;
            }
            else
            {
                using (var tx = new ScopedTransaction(dbFactory))
                {
                    //For other mode is,check number is belong to MasterAccountNum, ProfileNum and DatabaseNum from payload
                    if (!string.IsNullOrEmpty(number))
                        isValid = await SalesOrderHelper.ExistNumberAsync(number, pl.MasterAccountNum, pl.ProfileNum);
                    else if (!dto.OrderHeader.RowNum.IsZero())
                        isValid = await SalesOrderHelper.ExistRowNumAsync(dto.OrderHeader.RowNum.ToLong(), pl.MasterAccountNum, pl.ProfileNum);
                    if (!isValid)
                        AddError($"Data not found.");
                }
            }
            IsValid = isValid;
            return isValid;
        }

        #region validate data

        public virtual bool Validate(ChannelOrderData data, ProcessingMode processingMode = ProcessingMode.Edit)
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
        protected virtual bool ValidateAllMode(ChannelOrderData data)
        {
            var dbFactory = data.dbFactory;
            if (string.IsNullOrEmpty(data.OrderHeader.CentralOrderUuid))
            {
                IsValid = false;
                AddError($"Unique Id cannot be empty.");
                return IsValid;
            }
            //if (string.IsNullOrEmpty(data.OrderHeader.CustomerUuid))
            //{
            //    IsValid = false;
            //    AddError($"Customer cannot be empty.");
            //    return IsValid;
            //}
            return true;

        }

        protected virtual bool ValidateAdd(ChannelOrderData data)
        {
            var dbFactory = data.dbFactory;
            if (data.OrderHeader.RowNum != 0 && dbFactory.Exists<OrderHeader>(data.OrderHeader.RowNum))
            {
                IsValid = false;
                AddError($"RowNum: {data.OrderHeader.RowNum} is duplicate.");
                return IsValid;
            }
            return true;

        }

        protected virtual bool ValidateEdit(ChannelOrderData data)
        {
            var dbFactory = data.dbFactory;
            if (data.OrderHeader.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.OrderHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.OrderHeader.RowNum != 0 && !dbFactory.Exists<OrderHeader>(data.OrderHeader.RowNum))
            {
                IsValid = false;
                AddError($"RowNum: {data.OrderHeader.RowNum} not found.");
                return IsValid;
            }
            return true;
        }

        protected virtual bool ValidateDelete(ChannelOrderData data)
        {
            var dbFactory = data.dbFactory;
            if (data.OrderHeader.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.OrderHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.OrderHeader.RowNum != 0 && !dbFactory.Exists<OrderHeader>(data.OrderHeader.RowNum))
            {
                IsValid = false;
                AddError($"RowNum: {data.OrderHeader.RowNum} not found.");
                return IsValid;
            }
            return true;
        }

        #endregion

        #region Async validate data

        public virtual async Task<bool> ValidateAsync(ChannelOrderData data, ProcessingMode processingMode = ProcessingMode.Edit)
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

        protected virtual async Task<bool> ValidateAllModeAsync(ChannelOrderData data)
        {
            var dbFactory = data.dbFactory;
            if (string.IsNullOrEmpty(data.OrderHeader.CentralOrderUuid))
            {
                IsValid = false;
                AddError($"Unique Id cannot be empty.");
                return IsValid;
            }
            //if (string.IsNullOrEmpty(data.OrderHeader.CustomerUuid))
            //{
            //    IsValid = false;
            //    AddError($"Customer cannot be empty.");
            //    return IsValid;
            //}
            return true;

        }

        protected virtual async Task<bool> ValidateAddAsync(ChannelOrderData data)
        {
            var dbFactory = data.dbFactory;
            if (data.OrderHeader.RowNum != 0 && (await dbFactory.ExistsAsync<OrderHeader>(data.OrderHeader.RowNum)))
            {
                IsValid = false;
                AddError($"RowNum: {data.OrderHeader.RowNum} is duplicate.");
                return IsValid;
            }
            return true;

        }

        protected virtual async Task<bool> ValidateEditAsync(ChannelOrderData data)
        {
            var dbFactory = data.dbFactory;
            if (data.OrderHeader.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.OrderHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.OrderHeader.RowNum != 0 && !(await dbFactory.ExistsAsync<OrderHeader>(data.OrderHeader.RowNum)))
            {
                IsValid = false;
                AddError($"RowNum: {data.OrderHeader.RowNum} not found.");
                return IsValid;
            }
            return true;
        }

        protected virtual async Task<bool> ValidateDeleteAsync(ChannelOrderData data)
        {
            var dbFactory = data.dbFactory;
            if (data.OrderHeader.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.OrderHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.OrderHeader.RowNum != 0 && !(await dbFactory.ExistsAsync<OrderHeader>(data.OrderHeader.RowNum)))
            {
                IsValid = false;
                AddError($"RowNum: {data.OrderHeader.RowNum} not found.");
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
        public virtual bool Validate(ChannelOrderDataDto dto, ProcessingMode processingMode = ProcessingMode.Edit)
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
                dto.OrderHeader.CentralOrderUuid = Guid.NewGuid().ToString();
                if (dto.OrderLine != null && dto.OrderLine.Count > 0)
                {
                    foreach (var detailItem in dto.OrderLine)
                        detailItem.CentralOrderLineUuid = Guid.NewGuid().ToString();
                }
  
            }
            else if (processingMode == ProcessingMode.Edit)
            {
                if (dto.OrderHeader.RowNum.IsZero())
                {
                    isValid = false;
                    AddError("OrderHeader.RowNum is required.");
                }
                // This property should not be changed.
                dto.OrderHeader.MasterAccountNum = null;
                dto.OrderHeader.ProfileNum = null;
                dto.OrderHeader.DatabaseNum = null;
                dto.OrderHeader.CentralOrderUuid = null;
                // TODO 
                //dto.SalesOrderHeader.OrderNumber = null;
                if (dto.OrderLine != null && dto.OrderLine.Count > 0)
                {
                    foreach (var detailItem in dto.OrderLine)
                        detailItem.CentralOrderLineUuid = null;
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
        public virtual async Task<bool> ValidateAsync(ChannelOrderDataDto dto, ProcessingMode processingMode = ProcessingMode.Edit)
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
                dto.OrderHeader.CentralOrderUuid = Guid.NewGuid().ToString();
                if (dto.OrderLine != null && dto.OrderLine.Count > 0)
                {
                    foreach (var detailItem in dto.OrderLine)
                        detailItem.CentralOrderLineUuid = Guid.NewGuid().ToString();
                }
  
            }
            else if (processingMode == ProcessingMode.Edit)
            {
                if (dto.OrderHeader.RowNum.IsZero())
                {
                    isValid = false;
                    AddError("OrderHeader.RowNum is required.");
                }
                // This property should not be changed.
                dto.OrderHeader.MasterAccountNum = null;
                dto.OrderHeader.ProfileNum = null;
                dto.OrderHeader.DatabaseNum = null;
                dto.OrderHeader.CentralOrderUuid = null;
                // TODO 
                //dto.SalesOrderHeader.OrderNumber = null;
                if (dto.OrderLine != null && dto.OrderLine.Count > 0)
                {
                    foreach (var detailItem in dto.OrderLine)
                        detailItem.CentralOrderLineUuid = null;
                }
  
            }
            IsValid=isValid;
            return isValid;
        }
        #endregion
    }
}



