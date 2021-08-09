    

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
    /// Represents a default OrderShipmentService Validator class.
    /// </summary>
    public partial class OrderShipmentServiceValidatorDefault : IValidator<OrderShipmentData,OrderShipmentDataDto>, IMessage
    {
        public virtual bool IsValid { get; set; }
        public OrderShipmentServiceValidatorDefault() { }
        public OrderShipmentServiceValidatorDefault(IMessage serviceMessage) { ServiceMessage = serviceMessage; }

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

        public virtual bool ValidatePayload(OrderShipmentData data, IPayload payload, ProcessingMode processingMode = ProcessingMode.Edit)
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
                if (data.OrderShipmentHeader.MasterAccountNum != pl.MasterAccountNum ||
                    data.OrderShipmentHeader.ProfileNum != pl.ProfileNum)
                    isValid = false;
                AddError($"Invalid request.");
            }
            IsValid=isValid;
            return isValid;
        }

        public virtual bool Validate(OrderShipmentData data, ProcessingMode processingMode = ProcessingMode.Edit)
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
        protected virtual bool ValidateAllMode(OrderShipmentData data)
        {
            var dbFactory = data.dbFactory;
            if (string.IsNullOrEmpty(data.OrderShipmentHeader.OrderShipmentUuid))
            {
                IsValid = false;
                AddError($"Unique Id cannot be empty.");
                return IsValid;
            }
            //if (string.IsNullOrEmpty(data.OrderShipmentHeader.CustomerUuid))
            //{
            //    IsValid = false;
            //    AddError($"Customer cannot be empty.");
            //    return IsValid;
            //}
            return true;

        }

        protected virtual bool ValidateAdd(OrderShipmentData data)
        {
            var dbFactory = data.dbFactory;
            if (data.OrderShipmentHeader.RowNum != 0 && dbFactory.Exists<OrderShipmentHeader>(data.OrderShipmentHeader.RowNum))
            {
                IsValid = false;
                AddError($"RowNum: {data.OrderShipmentHeader.RowNum} is duplicate.");
                return IsValid;
            }
            return true;

        }

        protected virtual bool ValidateEdit(OrderShipmentData data)
        {
            var dbFactory = data.dbFactory;
            if (data.OrderShipmentHeader.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.OrderShipmentHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.OrderShipmentHeader.RowNum != 0 && !dbFactory.Exists<OrderShipmentHeader>(data.OrderShipmentHeader.RowNum))
            {
                IsValid = false;
                AddError($"RowNum: {data.OrderShipmentHeader.RowNum} not found.");
                return IsValid;
            }
            return true;
        }

        protected virtual bool ValidateDelete(OrderShipmentData data)
        {
            var dbFactory = data.dbFactory;
            if (data.OrderShipmentHeader.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.OrderShipmentHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.OrderShipmentHeader.RowNum != 0 && !dbFactory.Exists<OrderShipmentHeader>(data.OrderShipmentHeader.RowNum))
            {
                IsValid = false;
                AddError($"RowNum: {data.OrderShipmentHeader.RowNum} not found.");
                return IsValid;
            }
            return true;
        }


        #region Async Methods

        public virtual async Task<bool> ValidateAsync(OrderShipmentData data, ProcessingMode processingMode = ProcessingMode.Edit)
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

        protected virtual async Task<bool> ValidateAllModeAsync(OrderShipmentData data)
        {
            var dbFactory = data.dbFactory;
            if (string.IsNullOrEmpty(data.OrderShipmentHeader.OrderShipmentUuid))
            {
                IsValid = false;
                AddError($"Unique Id cannot be empty.");
                return IsValid;
            }
            //if (string.IsNullOrEmpty(data.OrderShipmentHeader.CustomerUuid))
            //{
            //    IsValid = false;
            //    AddError($"Customer cannot be empty.");
            //    return IsValid;
            //}
            return true;

        }

        protected virtual async Task<bool> ValidateAddAsync(OrderShipmentData data)
        {
            var dbFactory = data.dbFactory;
            if (data.OrderShipmentHeader.RowNum != 0 && (await dbFactory.ExistsAsync<OrderShipmentHeader>(data.OrderShipmentHeader.RowNum)))
            {
                IsValid = false;
                AddError($"RowNum: {data.OrderShipmentHeader.RowNum} is duplicate.");
                return IsValid;
            }
            return true;

        }

        protected virtual async Task<bool> ValidateEditAsync(OrderShipmentData data)
        {
            var dbFactory = data.dbFactory;
            if (data.OrderShipmentHeader.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.OrderShipmentHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.OrderShipmentHeader.RowNum != 0 && !(await dbFactory.ExistsAsync<OrderShipmentHeader>(data.OrderShipmentHeader.RowNum)))
            {
                IsValid = false;
                AddError($"RowNum: {data.OrderShipmentHeader.RowNum} not found.");
                return IsValid;
            }
            return true;
        }

        protected virtual async Task<bool> ValidateDeleteAsync(OrderShipmentData data)
        {
            var dbFactory = data.dbFactory;
            if (data.OrderShipmentHeader.RowNum == 0)
            {
                IsValid = false;
                AddError($"RowNum: {data.OrderShipmentHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.OrderShipmentHeader.RowNum != 0 && !(await dbFactory.ExistsAsync<OrderShipmentHeader>(data.OrderShipmentHeader.RowNum)))
            {
                IsValid = false;
                AddError($"RowNum: {data.OrderShipmentHeader.RowNum} not found.");
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
            var isValid = true;
            //TODO 
            //var pl = (OrderShipmentHeaderPayload)payload;
            //if (pl is null || !pl.Has OrderShipmentHeader)
            //{
            //    isValid = false;
            //    AddError($"No data found");
            //}
            //else
            //{
            //    var dto = pl.SalesOrder;
            //    //No matter what processingMode is,copy MasterAccountNum, ProfileNum and DatabaseNum from payload to dto
            //    dto.OrderShipmentHeader.MasterAccountNum = pl.MasterAccountNum;
            //    dto.OrderShipmentHeader.ProfileNum = pl.ProfileNum;
            //    dto.OrderShipmentHeader.DatabaseNum = pl.DatabaseNum;
            //    isValid = Validate(dto, dbFactory, processingMode);
            //}
            return isValid;
        }
        /// <summary>
        /// Validate dto.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="dbFactory"></param>
        /// <param name="processingMode"></param>
        /// <returns></returns>
        public virtual bool Validate(OrderShipmentDataDto dto, IDataBaseFactory dbFactory, ProcessingMode processingMode = ProcessingMode.Edit)
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
                //if (string.IsNullOrEmpty(dto.OrderShipmentHeader.OrderShipmentUuid))
                //{
                    dto.OrderShipmentHeader.OrderShipmentUuid = new Guid().ToString();
                //} 
                
                if (dto.OrderShipmentPackage != null && dto.OrderShipmentPackage.Count > 0)
                {
                    foreach (var detailItem in dto.OrderShipmentPackage)
                    {
                        //if (string.IsNullOrEmpty(detailItem.OrderShipmentPackageUuid))
                        //{
                            detailItem.OrderShipmentPackageUuid = new Guid().ToString();
                        //}
                    }
                }
                  
            }
            if (processingMode == ProcessingMode.Edit)
            {
                if (!dto.OrderShipmentHeader.RowNum.HasValue)
                {
                    isValid = false;
                    AddError("OrderShipmentHeader.RowNum is required.");
                }
                if (dto.OrderShipmentHeader.RowNum.ToLong() <= 0)
                {
                    isValid = false;
                    AddError("OrderShipmentHeader.RowNum is invalid."); 
                }
                // This property should not be changed.
                dto.OrderShipmentHeader.MasterAccountNum = null;
                dto.OrderShipmentHeader.ProfileNum = null;
                dto.OrderShipmentHeader.DatabaseNum = null;
                dto.OrderShipmentHeader.OrderShipmentUuid = null;
                // TODO 
                //dto.SalesOrderHeader.OrderNumber = null;
            }
            else
            {
                //TODO
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
            var isValid = true; 
            //TODO 
            //var pl = (OrderShipmentHeaderPayload)payload;
            //if (pl is null || !pl.Has OrderShipmentHeader)
            //{
            //    isValid = false;
            //    AddError($"No data found");
            //}
            //else
            //{
            //    var dto = pl.SalesOrder;
            //    //No matter what processingMode is,copy MasterAccountNum, ProfileNum and DatabaseNum from payload to dto
            //    dto.OrderShipmentHeader.MasterAccountNum = pl.MasterAccountNum;
            //    dto.OrderShipmentHeader.ProfileNum = pl.ProfileNum;
            //    dto.OrderShipmentHeader.DatabaseNum = pl.DatabaseNum;
            //    isValid =await ValidateAsync(dto, dbFactory, processingMode);
            //}
            return isValid;
        }
        /// <summary>
        /// Validate dto.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="dbFactory"></param>
        /// <param name="processingMode"></param>
        /// <returns></returns>
        public virtual async Task<bool> ValidateAsync(OrderShipmentDataDto dto, IDataBaseFactory dbFactory, ProcessingMode processingMode = ProcessingMode.Edit)
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
                  dto.OrderShipmentHeader.OrderShipmentUuid = new Guid().ToString(); 
                
                if (dto.OrderShipmentPackage != null && dto.OrderShipmentPackage.Count > 0)
                {
                    foreach (var detailItem in dto.OrderShipmentPackage)
                    { 
                        detailItem.OrderShipmentPackageUuid = new Guid().ToString();
                    }
                }
                  
 
                
            }
            if (processingMode == ProcessingMode.Edit)
            {
                if (!dto.OrderShipmentHeader.RowNum.HasValue)
                {
                    isValid = false;
                    AddError("OrderShipmentHeader.RowNum is required.");
                }
                if (dto.OrderShipmentHeader.RowNum.ToLong() <= 0)
                {
                    isValid = false;
                    AddError("OrderShipmentHeader.RowNum is invalid."); 
                }
                // This property should not be changed.
                dto.OrderShipmentHeader.MasterAccountNum = null;
                dto.OrderShipmentHeader.ProfileNum = null;
                dto.OrderShipmentHeader.DatabaseNum = null;
                dto.OrderShipmentHeader.OrderShipmentUuid = null;
                //TODO set uuid to null 
                //dto.OrderShipmentHeader.OrderNumber = null;
            }
            else
            {
                //TODO
            }
            IsValid=isValid;
            return isValid;
        }
        #endregion
    }
}



