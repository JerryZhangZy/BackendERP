    

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
    /// Represents a default CustomIOFormatService Validator class.
    /// </summary>
    public partial class CustomIOFormatServiceValidatorDefault : IValidator<CustomIOFormatData,CustomIOFormatDataDto>, IMessage
    {
        public virtual bool IsValid { get; set; }
        public CustomIOFormatServiceValidatorDefault() { }
        public CustomIOFormatServiceValidatorDefault(IMessage serviceMessage, IDataBaseFactory dbFactory) 
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
            var pl = payload as CustomIOFormatPayload;
            var dto = pl.CustomIOFormat;

            if (processingMode == ProcessingMode.Add)
            {
                //For Add mode is,set MasterAccountNum, ProfileNum and DatabaseNum from payload to dto
                dto.CustomIOFormat.MasterAccountNum = pl.MasterAccountNum;
                dto.CustomIOFormat.ProfileNum = pl.ProfileNum;
                dto.CustomIOFormat.DatabaseNum = pl.DatabaseNum;
            }
            else
            {
                //For other mode is,check number is belong to MasterAccountNum, ProfileNum and DatabaseNum from payload
                using (var tx = new ScopedTransaction(dbFactory))
                {
                    if (!string.IsNullOrEmpty(number))
                        isValid = CustomIOFormatHelper.ExistNumber(number, pl.MasterAccountNum, pl.ProfileNum);
                    else if(!dto.CustomIOFormat.RowNum.IsZero())
                        isValid = CustomIOFormatHelper.ExistRowNum(dto.CustomIOFormat.RowNum.ToLong(), pl.MasterAccountNum, pl.ProfileNum); 
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
            var pl = payload as CustomIOFormatPayload;
            var dto = pl.CustomIOFormat;

            if (processingMode == ProcessingMode.Add)
            {
                //For Add mode is,set MasterAccountNum, ProfileNum and DatabaseNum from payload to dto
                dto.CustomIOFormat.MasterAccountNum = pl.MasterAccountNum;
                dto.CustomIOFormat.ProfileNum = pl.ProfileNum;
                dto.CustomIOFormat.DatabaseNum = pl.DatabaseNum;
            }
            else
            {
                //For other mode is,check number is belong to MasterAccountNum, ProfileNum and DatabaseNum from payload
                using (var tx = new ScopedTransaction(dbFactory))
                {
                    if (!string.IsNullOrEmpty(number))
                        isValid = await CustomIOFormatHelper.ExistNumberAsync(number, pl.MasterAccountNum, pl.ProfileNum);
                    else if(!dto.CustomIOFormat.RowNum.IsZero())
                        isValid = await CustomIOFormatHelper.ExistRowNumAsync(dto.CustomIOFormat.RowNum.ToLong(), pl.MasterAccountNum, pl.ProfileNum); 
                }
                if (!isValid)
                    AddError($"Data not found.");
            }
            IsValid = isValid;
            return isValid;
        }

        #region validate data

        public virtual bool Validate(CustomIOFormatData data, ProcessingMode processingMode = ProcessingMode.Edit)
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
        protected virtual bool ValidateAllMode(CustomIOFormatData data)
        {
            var dbFactory = data.dbFactory;
            if (string.IsNullOrEmpty(data.CustomIOFormat.CustomIOFormatUuid))
            {
                IsValid = false;
                AddError($"Unique Id cannot be empty.");
                return IsValid;
            }
            //if (string.IsNullOrEmpty(data.CustomIOFormat.CustomerUuid))
            //{
            //    IsValid = false;
            //    AddError($"Customer cannot be empty.");
            //    return IsValid;
            //}
            return true;

        }

        protected virtual bool ValidateAdd(CustomIOFormatData data)
        {
            var dbFactory = data.dbFactory;
            if (data.CustomIOFormat.RowNum != 0 && dbFactory.Exists<CustomIOFormat>(data.CustomIOFormat.RowNum))
            {
                IsValid = false;
                AddError($"RowNum: {data.CustomIOFormat.RowNum} is duplicate.");
                return IsValid;
            }
            return true;

        }

        protected virtual bool ValidateEdit(CustomIOFormatData data)
        {
            var dbFactory = data.dbFactory;
            if (data.CustomIOFormat.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.CustomIOFormat.RowNum} not found.");
                return IsValid;
            }

            if (data.CustomIOFormat.RowNum != 0 && !dbFactory.Exists<CustomIOFormat>(data.CustomIOFormat.RowNum))
            {
                IsValid = false;
                AddError($"RowNum: {data.CustomIOFormat.RowNum} not found.");
                return IsValid;
            }
            return true;
        }

        protected virtual bool ValidateDelete(CustomIOFormatData data)
        {
            var dbFactory = data.dbFactory;
            if (data.CustomIOFormat.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.CustomIOFormat.RowNum} not found.");
                return IsValid;
            }

            if (data.CustomIOFormat.RowNum != 0 && !dbFactory.Exists<CustomIOFormat>(data.CustomIOFormat.RowNum))
            {
                IsValid = false;
                AddError($"RowNum: {data.CustomIOFormat.RowNum} not found.");
                return IsValid;
            }
            return true;
        }

        #endregion

        #region Async validate data

        public virtual async Task<bool> ValidateAsync(CustomIOFormatData data, ProcessingMode processingMode = ProcessingMode.Edit)
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

        protected virtual async Task<bool> ValidateAllModeAsync(CustomIOFormatData data)
        {
            var dbFactory = data.dbFactory;
            if (string.IsNullOrEmpty(data.CustomIOFormat.CustomIOFormatUuid))
            {
                IsValid = false;
                AddError($"Unique Id cannot be empty.");
                return IsValid;
            }
            //if (string.IsNullOrEmpty(data.CustomIOFormat.CustomerUuid))
            //{
            //    IsValid = false;
            //    AddError($"Customer cannot be empty.");
            //    return IsValid;
            //}
            return true;

        }

        protected virtual async Task<bool> ValidateAddAsync(CustomIOFormatData data)
        {
            var dbFactory = data.dbFactory;
            if (data.CustomIOFormat.RowNum != 0 && (await dbFactory.ExistsAsync<CustomIOFormat>(data.CustomIOFormat.RowNum)))
            {
                IsValid = false;
                AddError($"RowNum: {data.CustomIOFormat.RowNum} is duplicate.");
                return IsValid;
            }
            return true;

        }

        protected virtual async Task<bool> ValidateEditAsync(CustomIOFormatData data)
        {
            var dbFactory = data.dbFactory;
            if (data.CustomIOFormat.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.CustomIOFormat.RowNum} not found.");
                return IsValid;
            }

            if (data.CustomIOFormat.RowNum != 0 && !(await dbFactory.ExistsAsync<CustomIOFormat>(data.CustomIOFormat.RowNum)))
            {
                IsValid = false;
                AddError($"RowNum: {data.CustomIOFormat.RowNum} not found.");
                return IsValid;
            }
            return true;
        }

        protected virtual async Task<bool> ValidateDeleteAsync(CustomIOFormatData data)
        {
            var dbFactory = data.dbFactory;
            if (data.CustomIOFormat.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.CustomIOFormat.RowNum} not found.");
                return IsValid;
            }

            if (data.CustomIOFormat.RowNum != 0 && !(await dbFactory.ExistsAsync<CustomIOFormat>(data.CustomIOFormat.RowNum)))
            {
                IsValid = false;
                AddError($"RowNum: {data.CustomIOFormat.RowNum} not found.");
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
        public virtual bool Validate(CustomIOFormatDataDto dto, ProcessingMode processingMode = ProcessingMode.Edit)
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
                dto.CustomIOFormat.CustomIOFormatUuid = Guid.NewGuid().ToString();
  
            }
            else if (processingMode == ProcessingMode.Edit)
            {
                if (dto.CustomIOFormat.RowNum.IsZero())
                {
                    isValid = false;
                    AddError("CustomIOFormat.RowNum is required.");
                }
                // This property should not be changed.
                dto.CustomIOFormat.MasterAccountNum = null;
                dto.CustomIOFormat.ProfileNum = null;
                dto.CustomIOFormat.DatabaseNum = null;
                dto.CustomIOFormat.CustomIOFormatUuid = null;
                // TODO 
                //dto.SalesOrderHeader.OrderNumber = null;
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
        public virtual async Task<bool> ValidateAsync(CustomIOFormatDataDto dto, ProcessingMode processingMode = ProcessingMode.Edit)
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
                dto.CustomIOFormat.CustomIOFormatUuid = Guid.NewGuid().ToString();
  
            }
            else if (processingMode == ProcessingMode.Edit)
            {
                if (dto.CustomIOFormat.RowNum.IsZero())
                {
                    isValid = false;
                    AddError("CustomIOFormat.RowNum is required.");
                }
                // This property should not be changed.
                dto.CustomIOFormat.MasterAccountNum = null;
                dto.CustomIOFormat.ProfileNum = null;
                dto.CustomIOFormat.DatabaseNum = null;
                dto.CustomIOFormat.CustomIOFormatUuid = null;
                // TODO 
                //dto.SalesOrderHeader.OrderNumber = null;
  
            }
            IsValid=isValid;
            return isValid;
        }
        #endregion
    }
}



