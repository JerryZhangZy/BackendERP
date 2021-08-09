    

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
    /// Represents a default InvoiceService Validator class.
    /// </summary>
    public partial class InvoiceServiceValidatorDefault : IValidator<InvoiceData,InvoiceDataDto>, IMessage
    {
        public virtual bool IsValid { get; set; }
        public InvoiceServiceValidatorDefault() { }
        public InvoiceServiceValidatorDefault(IMessage serviceMessage) { ServiceMessage = serviceMessage; }

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

        public virtual bool ValidatePayload(InvoiceData data, IPayload payload, ProcessingMode processingMode = ProcessingMode.Edit)
        { 
            var isValid = true;
            var pl = payload as SalesOrderPayload;//TODO replace SalesOrderPayload to your payload
            if (processingMode == ProcessingMode.Add)
            {
                //TODO 
            }
            else
            {
                //check MasterAccountNum, ProfileNum and DatabaseNum between data and payload
                if (data.InvoiceHeader.MasterAccountNum != pl.MasterAccountNum ||
                    data.InvoiceHeader.ProfileNum != pl.ProfileNum)
                    isValid = false;
                AddError($"Invalid request.");
            }
            IsValid=isValid;
            return isValid;
        }

        public virtual bool Validate(InvoiceData data, ProcessingMode processingMode = ProcessingMode.Edit)
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
        protected virtual bool ValidateAllMode(InvoiceData data)
        {
            var dbFactory = data.dbFactory;
            if (string.IsNullOrEmpty(data.InvoiceHeader.InvoiceUuid))
            {
                IsValid = false;
                AddError($"Unique Id cannot be empty.");
                return IsValid;
            }
            //if (string.IsNullOrEmpty(data.InvoiceHeader.CustomerUuid))
            //{
            //    IsValid = false;
            //    AddError($"Customer cannot be empty.");
            //    return IsValid;
            //}
            return true;

        }

        protected virtual bool ValidateAdd(InvoiceData data)
        {
            var dbFactory = data.dbFactory;
            if (data.InvoiceHeader.RowNum != 0 && dbFactory.Exists<InvoiceHeader>(data.InvoiceHeader.RowNum))
            {
                IsValid = false;
                AddError($"RowNum: {data.InvoiceHeader.RowNum} is duplicate.");
                return IsValid;
            }
            return true;

        }

        protected virtual bool ValidateEdit(InvoiceData data)
        {
            var dbFactory = data.dbFactory;
            if (data.InvoiceHeader.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.InvoiceHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.InvoiceHeader.RowNum != 0 && !dbFactory.Exists<InvoiceHeader>(data.InvoiceHeader.RowNum))
            {
                IsValid = false;
                AddError($"RowNum: {data.InvoiceHeader.RowNum} not found.");
                return IsValid;
            }
            return true;
        }

        protected virtual bool ValidateDelete(InvoiceData data)
        {
            var dbFactory = data.dbFactory;
            if (data.InvoiceHeader.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.InvoiceHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.InvoiceHeader.RowNum != 0 && !dbFactory.Exists<InvoiceHeader>(data.InvoiceHeader.RowNum))
            {
                IsValid = false;
                AddError($"RowNum: {data.InvoiceHeader.RowNum} not found.");
                return IsValid;
            }
            return true;
        }


        #region Async Methods

        public virtual async Task<bool> ValidateAsync(InvoiceData data, ProcessingMode processingMode = ProcessingMode.Edit)
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

        protected virtual async Task<bool> ValidateAllModeAsync(InvoiceData data)
        {
            var dbFactory = data.dbFactory;
            if (string.IsNullOrEmpty(data.InvoiceHeader.InvoiceUuid))
            {
                IsValid = false;
                AddError($"Unique Id cannot be empty.");
                return IsValid;
            }
            //if (string.IsNullOrEmpty(data.InvoiceHeader.CustomerUuid))
            //{
            //    IsValid = false;
            //    AddError($"Customer cannot be empty.");
            //    return IsValid;
            //}
            return true;

        }

        protected virtual async Task<bool> ValidateAddAsync(InvoiceData data)
        {
            var dbFactory = data.dbFactory;
            if (data.InvoiceHeader.RowNum != 0 && (await dbFactory.ExistsAsync<InvoiceHeader>(data.InvoiceHeader.RowNum)))
            {
                IsValid = false;
                AddError($"RowNum: {data.InvoiceHeader.RowNum} is duplicate.");
                return IsValid;
            }
            return true;

        }

        protected virtual async Task<bool> ValidateEditAsync(InvoiceData data)
        {
            var dbFactory = data.dbFactory;
            if (data.InvoiceHeader.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.InvoiceHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.InvoiceHeader.RowNum != 0 && !(await dbFactory.ExistsAsync<InvoiceHeader>(data.InvoiceHeader.RowNum)))
            {
                IsValid = false;
                AddError($"RowNum: {data.InvoiceHeader.RowNum} not found.");
                return IsValid;
            }
            return true;
        }

        protected virtual async Task<bool> ValidateDeleteAsync(InvoiceData data)
        {
            var dbFactory = data.dbFactory;
            if (data.InvoiceHeader.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.InvoiceHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.InvoiceHeader.RowNum != 0 && !(await dbFactory.ExistsAsync<InvoiceHeader>(data.InvoiceHeader.RowNum)))
            {
                IsValid = false;
                AddError($"RowNum: {data.InvoiceHeader.RowNum} not found.");
                return IsValid;
            }
            return true;
        }

        #endregion Async Methods

        #region Validate dto (invoke this before data loaded)
        /// <summary>
        /// Copy MasterAccountNum, ProfileNum and DatabaseNum to dto, then validate dto.
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="dbFactory"></param>
        /// <param name="processingMode"></param>
        /// <returns></returns>
        public virtual bool Validate(IPayload payload, IDataBaseFactory dbFactory, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            IsValid = true;
            //var pl = (InvoiceHeaderPayload)payload;
            //if (pl is null || !pl.InvoiceHeader)
            //{
            //    IsValid = false;
            //    AddError($"No data found");
            //    return IsValid;
            //}

            //var dto = pl.InvoiceHeader;
            //if (processingMode == ProcessingMode.Add)
            //{
            //    //copy MasterAccountNum, ProfileNum and DatabaseNum from payload to dto
            //    dto.InvoiceHeader.MasterAccountNum = pl.MasterAccountNum;
            //    dto.InvoiceHeader.ProfileNum = pl.ProfileNum;
            //    dto.InvoiceHeader.DatabaseNum = pl.DatabaseNum;
            //}

            //IsValid= Validate(dto, dbFactory, processingMode);
            return IsValid;
        }
        /// <summary>
        /// Validate dto.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="dbFactory"></param>
        /// <param name="processingMode"></param>
        /// <returns></returns>
        public virtual bool Validate(InvoiceDataDto dto, IDataBaseFactory dbFactory, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            var isValid = true;
            if (dto is null)
            {
                isValid = false;
                AddError($"No data found");
            }
            if (processingMode == ProcessingMode.Add)
            {
                //Init property
                dto.InvoiceHeader.InvoiceUuid = new Guid().ToString(); 
                
                if (dto.InvoiceItems != null && dto.InvoiceItems.Count > 0)
                {
                    foreach (var detailItem in dto.InvoiceItems)
                    {
                        detailItem.InvoiceItemsUuid = new Guid().ToString();
                    }
                }
                  
            } 
            else
            {
                if (!dto.InvoiceHeader.RowNum.HasValue)
                {
                    isValid = false;
                    AddError("InvoiceHeader.RowNum is required.");
                }
                if (dto.InvoiceHeader.RowNum.ToLong() <= 0)
                {
                    isValid = false;
                    AddError("InvoiceHeader.RowNum is invalid."); 
                }
                // This property should not be changed.
                dto.InvoiceHeader.MasterAccountNum = null;
                dto.InvoiceHeader.ProfileNum = null;
                dto.InvoiceHeader.DatabaseNum = null;
                dto.InvoiceHeader.InvoiceUuid = null;
                // TODO 
                //dto.SalesOrderHeader.OrderNumber = null;
            }
            IsValid=isValid;
            return isValid;
        }
        #endregion

        #region Validate dto async (invoke this before data loaded)
        /// <summary>
        /// Copy MasterAccountNum, ProfileNum and DatabaseNum to dto, then validate dto.
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="dbFactory"></param>
        /// <param name="processingMode"></param>
        /// <returns></returns>
        public virtual async Task<bool> ValidateAsync(IPayload payload, IDataBaseFactory dbFactory, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            IsValid = true;
            //var pl = (InvoiceHeaderPayload)payload;
            //if (pl is null || !pl.InvoiceHeader)
            //{
            //    IsValid = false;
            //    AddError($"No data found");
            //    return IsValid;
            //}

            //var dto = pl.InvoiceHeader;
            //if (processingMode == ProcessingMode.Add)
            //{
            //    //copy MasterAccountNum, ProfileNum and DatabaseNum from payload to dto
            //    dto.InvoiceHeader.MasterAccountNum = pl.MasterAccountNum;
            //    dto.InvoiceHeader.ProfileNum = pl.ProfileNum;
            //    dto.InvoiceHeader.DatabaseNum = pl.DatabaseNum;
            //}

            //IsValid= await ValidateAsync(dto, dbFactory, processingMode);
            return IsValid;
        }
        /// <summary>
        /// Validate dto.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="dbFactory"></param>
        /// <param name="processingMode"></param>
        /// <returns></returns>
        public virtual async Task<bool> ValidateAsync(InvoiceDataDto dto, IDataBaseFactory dbFactory, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            var isValid = true;
            if (dto is null)
            {
                isValid = false;
                AddError($"No data found");
            }
            if (processingMode == ProcessingMode.Add)
            {
                //Init property 
                  dto.InvoiceHeader.InvoiceUuid = new Guid().ToString(); 
                
                if (dto.InvoiceItems != null && dto.InvoiceItems.Count > 0)
                {
                    foreach (var detailItem in dto.InvoiceItems)
                    { 
                        detailItem.InvoiceItemsUuid = new Guid().ToString();
                    }
                }
                  
 
                
            } 
            else
            {
                if (!dto.InvoiceHeader.RowNum.HasValue)
                {
                    isValid = false;
                    AddError("InvoiceHeader.RowNum is required.");
                }
                if (dto.InvoiceHeader.RowNum.ToLong() <= 0)
                {
                    isValid = false;
                    AddError("InvoiceHeader.RowNum is invalid."); 
                }
                // This property should not be changed.
                dto.InvoiceHeader.MasterAccountNum = null;
                dto.InvoiceHeader.ProfileNum = null;
                dto.InvoiceHeader.DatabaseNum = null;
                dto.InvoiceHeader.InvoiceUuid = null;
                //TODO set uuid to null 
                //dto.InvoiceHeader.OrderNumber = null;
            }
            IsValid=isValid;
            return isValid;
        }
        #endregion
    }
}



