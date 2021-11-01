    

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
    /// Represents a default MiscInvoiceService Validator class.
    /// </summary>
    public partial class MiscInvoiceServiceValidatorDefault : IValidator<MiscInvoiceData,MiscInvoiceDataDto>, IMessage
    {
        public virtual bool IsValid { get; set; }
        public MiscInvoiceServiceValidatorDefault() { }
        public MiscInvoiceServiceValidatorDefault(IMessage serviceMessage, IDataBaseFactory dbFactory) 
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
            var pl = payload as MiscInvoicePayload;
            var dto = pl.MiscInvoice;

            if (processingMode == ProcessingMode.Add)
            {
                //For Add mode is,set MasterAccountNum, ProfileNum and DatabaseNum from payload to dto
                dto.MiscInvoiceHeader.MasterAccountNum = pl.MasterAccountNum;
                dto.MiscInvoiceHeader.ProfileNum = pl.ProfileNum;
                dto.MiscInvoiceHeader.DatabaseNum = pl.DatabaseNum;
            }
            else
            {
                //For other mode is,check number is belong to MasterAccountNum, ProfileNum and DatabaseNum from payload
                using (var tx = new ScopedTransaction(dbFactory))
                {
                    if (!string.IsNullOrEmpty(number))
                        isValid = MiscInvoiceHelper.ExistNumber(number, pl.MasterAccountNum, pl.ProfileNum);
                    else if(!dto.MiscInvoiceHeader.RowNum.IsZero())
                        isValid = MiscInvoiceHelper.ExistRowNum(dto.MiscInvoiceHeader.RowNum.ToLong(), pl.MasterAccountNum, pl.ProfileNum); 
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
            var pl = payload as MiscInvoicePayload;
            var dto = pl.MiscInvoice;

            if (processingMode == ProcessingMode.Add)
            {
                //For Add mode is,set MasterAccountNum, ProfileNum and DatabaseNum from payload to dto
                dto.MiscInvoiceHeader.MasterAccountNum = pl.MasterAccountNum;
                dto.MiscInvoiceHeader.ProfileNum = pl.ProfileNum;
                dto.MiscInvoiceHeader.DatabaseNum = pl.DatabaseNum;
            }
            else
            {
                //For other mode is,check number is belong to MasterAccountNum, ProfileNum and DatabaseNum from payload
                using (var tx = new ScopedTransaction(dbFactory))
                {
                    if (!string.IsNullOrEmpty(number))
                        isValid = await MiscInvoiceHelper.ExistNumberAsync(number, pl.MasterAccountNum, pl.ProfileNum);
                    else if(!dto.MiscInvoiceHeader.RowNum.IsZero())
                        isValid = await MiscInvoiceHelper.ExistRowNumAsync(dto.MiscInvoiceHeader.RowNum.ToLong(), pl.MasterAccountNum, pl.ProfileNum); 
                }
                if (!isValid)
                    AddError($"Data not found.");
            }
            IsValid = isValid;
            return isValid;
        }

        #region validate data

        public virtual bool Validate(MiscInvoiceData data, ProcessingMode processingMode = ProcessingMode.Edit)
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
        protected virtual bool ValidateAllMode(MiscInvoiceData data)
        {
            var dbFactory = data.dbFactory;
            if (string.IsNullOrEmpty(data.MiscInvoiceHeader.MiscInvoiceUuid))
            {
                IsValid = false;
                AddError($"Unique Id cannot be empty.");
                return IsValid;
            }
            //if (string.IsNullOrEmpty(data.MiscInvoiceHeader.CustomerUuid))
            //{
            //    IsValid = false;
            //    AddError($"Customer cannot be empty.");
            //    return IsValid;
            //}
            return true;

        }

        protected virtual bool ValidateAdd(MiscInvoiceData data)
        {
            var dbFactory = data.dbFactory;
            if (data.MiscInvoiceHeader.RowNum != 0 && dbFactory.Exists<MiscInvoiceHeader>(data.MiscInvoiceHeader.RowNum))
            {
                IsValid = false;
                AddError($"RowNum: {data.MiscInvoiceHeader.RowNum} is duplicate.");
                return IsValid;
            }
            return true;

        }

        protected virtual bool ValidateEdit(MiscInvoiceData data)
        {
            var dbFactory = data.dbFactory;
            if (data.MiscInvoiceHeader.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.MiscInvoiceHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.MiscInvoiceHeader.RowNum != 0 && !dbFactory.Exists<MiscInvoiceHeader>(data.MiscInvoiceHeader.RowNum))
            {
                IsValid = false;
                AddError($"RowNum: {data.MiscInvoiceHeader.RowNum} not found.");
                return IsValid;
            }
            return true;
        }

        protected virtual bool ValidateDelete(MiscInvoiceData data)
        {
            var dbFactory = data.dbFactory;
            if (data.MiscInvoiceHeader.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.MiscInvoiceHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.MiscInvoiceHeader.RowNum != 0 && !dbFactory.Exists<MiscInvoiceHeader>(data.MiscInvoiceHeader.RowNum))
            {
                IsValid = false;
                AddError($"RowNum: {data.MiscInvoiceHeader.RowNum} not found.");
                return IsValid;
            }
            return true;
        }

        #endregion

        #region Async validate data

        public virtual async Task<bool> ValidateAsync(MiscInvoiceData data, ProcessingMode processingMode = ProcessingMode.Edit)
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

        protected virtual async Task<bool> ValidateAllModeAsync(MiscInvoiceData data)
        {
            var dbFactory = data.dbFactory;
            if (string.IsNullOrEmpty(data.MiscInvoiceHeader.MiscInvoiceUuid))
            {
                IsValid = false;
                AddError($"Unique Id cannot be empty.");
                return IsValid;
            }
            //if (string.IsNullOrEmpty(data.MiscInvoiceHeader.CustomerUuid))
            //{
            //    IsValid = false;
            //    AddError($"Customer cannot be empty.");
            //    return IsValid;
            //}
            return true;

        }

        protected virtual async Task<bool> ValidateAddAsync(MiscInvoiceData data)
        {
            var dbFactory = data.dbFactory;
            if (data.MiscInvoiceHeader.RowNum != 0 && (await dbFactory.ExistsAsync<MiscInvoiceHeader>(data.MiscInvoiceHeader.RowNum)))
            {
                IsValid = false;
                AddError($"RowNum: {data.MiscInvoiceHeader.RowNum} is duplicate.");
                return IsValid;
            }
            return true;

        }

        protected virtual async Task<bool> ValidateEditAsync(MiscInvoiceData data)
        {
            var dbFactory = data.dbFactory;
            if (data.MiscInvoiceHeader.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.MiscInvoiceHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.MiscInvoiceHeader.RowNum != 0 && !(await dbFactory.ExistsAsync<MiscInvoiceHeader>(data.MiscInvoiceHeader.RowNum)))
            {
                IsValid = false;
                AddError($"RowNum: {data.MiscInvoiceHeader.RowNum} not found.");
                return IsValid;
            }
            return true;
        }

        protected virtual async Task<bool> ValidateDeleteAsync(MiscInvoiceData data)
        {
            var dbFactory = data.dbFactory;
            if (data.MiscInvoiceHeader.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.MiscInvoiceHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.MiscInvoiceHeader.RowNum != 0 && !(await dbFactory.ExistsAsync<MiscInvoiceHeader>(data.MiscInvoiceHeader.RowNum)))
            {
                IsValid = false;
                AddError($"RowNum: {data.MiscInvoiceHeader.RowNum} not found.");
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
        public virtual bool Validate(MiscInvoiceDataDto dto, ProcessingMode processingMode = ProcessingMode.Edit)
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
                dto.MiscInvoiceHeader.MiscInvoiceUuid = Guid.NewGuid().ToString();
                if (dto.MiscInvoiceTransaction != null && dto.MiscInvoiceTransaction.Count > 0)
                {
                    foreach (var detailItem in dto.MiscInvoiceTransaction)
                        detailItem.TransUuid = Guid.NewGuid().ToString();
                }
  
            }
            else if (processingMode == ProcessingMode.Edit)
            {
                if (dto.MiscInvoiceHeader.RowNum.IsZero())
                {
                    isValid = false;
                    AddError("MiscInvoiceHeader.RowNum is required.");
                }
                // This property should not be changed.
                dto.MiscInvoiceHeader.MasterAccountNum = null;
                dto.MiscInvoiceHeader.ProfileNum = null;
                dto.MiscInvoiceHeader.DatabaseNum = null;
                dto.MiscInvoiceHeader.MiscInvoiceUuid = null;
                // TODO 
                //dto.SalesOrderHeader.OrderNumber = null;
                if (dto.MiscInvoiceTransaction != null && dto.MiscInvoiceTransaction.Count > 0)
                {
                    foreach (var detailItem in dto.MiscInvoiceTransaction)
                        detailItem.TransUuid = null;
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
        public virtual async Task<bool> ValidateAsync(MiscInvoiceDataDto dto, ProcessingMode processingMode = ProcessingMode.Edit)
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
                dto.MiscInvoiceHeader.MiscInvoiceUuid = Guid.NewGuid().ToString();
                if (dto.MiscInvoiceTransaction != null && dto.MiscInvoiceTransaction.Count > 0)
                {
                    foreach (var detailItem in dto.MiscInvoiceTransaction)
                        detailItem.TransUuid = Guid.NewGuid().ToString();
                }
  
            }
            else if (processingMode == ProcessingMode.Edit)
            {
                if (dto.MiscInvoiceHeader.RowNum.IsZero())
                {
                    isValid = false;
                    AddError("MiscInvoiceHeader.RowNum is required.");
                }
                // This property should not be changed.
                dto.MiscInvoiceHeader.MasterAccountNum = null;
                dto.MiscInvoiceHeader.ProfileNum = null;
                dto.MiscInvoiceHeader.DatabaseNum = null;
                dto.MiscInvoiceHeader.MiscInvoiceUuid = null;
                // TODO 
                //dto.SalesOrderHeader.OrderNumber = null;
                if (dto.MiscInvoiceTransaction != null && dto.MiscInvoiceTransaction.Count > 0)
                {
                    foreach (var detailItem in dto.MiscInvoiceTransaction)
                        detailItem.TransUuid = null;
                }
  
            }
            IsValid=isValid;
            return isValid;
        }
        #endregion
    }
}



