

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
using Microsoft.Data.SqlClient;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    /// <summary>
    /// Represents a default SalesOrderService Validator class.
    /// </summary>
    public partial class SalesOrderServiceValidatorDefault : IValidator<SalesOrderData>, IMessage
    {
        public virtual bool IsValid { get; set; }

        public SalesOrderServiceValidatorDefault() { }
        public SalesOrderServiceValidatorDefault(IMessage serviceMessage) { ServiceMessage = serviceMessage; }

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

        public virtual bool ValidatePayload(SalesOrderData data, IPayload payload, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            Clear();
            var pl = payload as SalesOrderPayload;
            if (processingMode == ProcessingMode.Add)
            {
                //set MasterAccountNum, ProfileNum and DatabaseNum from payload
                data.SalesOrderHeader.MasterAccountNum = pl.MasterAccountNum;
                data.SalesOrderHeader.ProfileNum = pl.ProfileNum;
                data.SalesOrderHeader.DatabaseNum = pl.DatabaseNum;
            }
            else
            {
                //check MasterAccountNum, ProfileNum and DatabaseNum between data and payload
                if (
                    data.SalesOrderHeader.MasterAccountNum != pl.MasterAccountNum ||
                    data.SalesOrderHeader.ProfileNum != pl.ProfileNum
                )
                    IsValid = false;
                AddError($"Sales Order not found.");
                return IsValid;
            }
            return true;
        }

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
            //var dbFactory = data.dbFactory;
            var isValid = true;
            if (string.IsNullOrEmpty(data.SalesOrderHeader.SalesOrderUuid))
            {
                isValid = false;
                AddError($"SalesOrderHeader.SalesOrderUuid cannot be empty.");
            }
            if (string.IsNullOrEmpty(data.SalesOrderHeader.OrderNumber))
            {
                isValid = false;
                AddError($"SalesOrderHeader.OrderNumber cannot be empty.");
            }
            this.IsValid = isValid;
            return isValid;

        }

        protected virtual bool ValidateAdd(SalesOrderData data)
        {
            var isValid = true;
            var dbFactory = data.dbFactory;
            if (data.SalesOrderHeader.RowNum != 0 && dbFactory.Exists<SalesOrderHeader>(data.SalesOrderHeader.RowNum))
            {
                isValid = false;
                AddError($"RowNum: {data.SalesOrderHeader.RowNum} is duplicate.");
            }
            if (dbFactory.Exists<SalesOrderHeader>($" SalesOrderUuid='{data.SalesOrderHeader.SalesOrderUuid}'"))
            {
                isValid = false;
                AddError($"SalesOrderHeader.SalesOrderUuid: {data.SalesOrderHeader.SalesOrderUuid} is duplicate.");
            }
            if (dbFactory.Exists<SalesOrderHeader>($" ProfileNum={data.SalesOrderHeader.ProfileNum} and OrderNumber='{data.SalesOrderHeader.OrderNumber}'"))
            {
                isValid = false;
                AddError($"[SalesOrderHeader.ProfileNum: {data.SalesOrderHeader.ProfileNum},[SalesOrderHeader.SalesOrderUuid: {data.SalesOrderHeader.SalesOrderUuid} ]is duplicate.");
            }

            IsValid = isValid;
            return isValid;
        }

        //private async Task<bool> ExistsUKAsync(int profileNum, string OrderNumber, IDataBaseFactory dbFactory)
        //{
        //    //todo error parameters was replace by @0,@1
        //    var conditionSql = " ProfileNum=@ProfileNum and OrderNumber=@OrderNumber";
        //    var parameters = new SqlParameter[]
        //       {
        //            new SqlParameter("@ProfileNum",profileNum),
        //            new SqlParameter("@OrderNumber",OrderNumber)
        //       };
        //    // parameters was replace by @0,@1
        //    return await dbFactory.ExistsAsync<SalesOrderHeader>(conditionSql, parameters);
        //    // correct
        //    //return await dbFactory.ExistsAsync<SalesOrderHeader>($" ProfileNum={profileNum} and OrderNumber='{OrderNumber}'");
        //}


        protected virtual bool ValidateEdit(SalesOrderData data)
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

            if (dbFactory.Exists<SalesOrderHeader>($" SalesOrderUuid='{data.SalesOrderHeader.SalesOrderUuid}'"))
            {
                IsValid = false;
                AddError($"SalesOrderHeader.SalesOrderUuid: {data.SalesOrderHeader.SalesOrderUuid} is duplicate.");
                return IsValid;
            }
            if (dbFactory.Exists<SalesOrderHeader>($" ProfileNum={data.SalesOrderHeader.ProfileNum} and OrderNumber='{data.SalesOrderHeader.OrderNumber}'"))
            {
                IsValid = false;
                AddError($"[SalesOrderHeader.ProfileNum: {data.SalesOrderHeader.ProfileNum},[SalesOrderHeader.SalesOrderUuid: {data.SalesOrderHeader.SalesOrderUuid} ]is duplicate.");
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


        #region Async Methods

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
            //if (string.IsNullOrEmpty(data.SalesOrderHeader.CustomerUuid))
            //{
            //    IsValid = false;
            //    AddError($"Customer cannot be empty.");
            //    return IsValid;
            //}
            return true;

        }

        protected virtual async Task<bool> ValidateAddAsync(SalesOrderData data)
        {
            var isValid = true;
            var dbFactory = data.dbFactory;
            if (data.SalesOrderHeader.RowNum != 0 && await dbFactory.ExistsAsync<SalesOrderHeader>(data.SalesOrderHeader.RowNum))
            {
                isValid = false;
                AddError($"RowNum: {data.SalesOrderHeader.RowNum} is duplicate.");
            }
            if (await dbFactory.ExistsAsync<SalesOrderHeader>($" SalesOrderUuid='{data.SalesOrderHeader.SalesOrderUuid}'"))
            {
                isValid = false;
                AddError($"SalesOrderHeader.SalesOrderUuid: {data.SalesOrderHeader.SalesOrderUuid} is duplicate.");
            }
            if (await dbFactory.ExistsAsync<SalesOrderHeader>($" ProfileNum={data.SalesOrderHeader.ProfileNum} and OrderNumber='{data.SalesOrderHeader.OrderNumber}'"))
            {
                isValid = false;
                AddError($"[SalesOrderHeader.ProfileNum: {data.SalesOrderHeader.ProfileNum},[SalesOrderHeader.SalesOrderUuid: {data.SalesOrderHeader.SalesOrderUuid} ]is duplicate.");
            }

            IsValid = isValid;
            return isValid;
        }

        protected virtual async Task<bool> ValidateEditAsync(SalesOrderData data)
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

            if (await dbFactory.ExistsAsync<SalesOrderHeader>($" SalesOrderUuid='{data.SalesOrderHeader.SalesOrderUuid}'"))
            {
                IsValid = false;
                AddError($"SalesOrderHeader.SalesOrderUuid: {data.SalesOrderHeader.SalesOrderUuid} is duplicate.");
                return IsValid;
            }
            if (await dbFactory.ExistsAsync<SalesOrderHeader>($" ProfileNum={data.SalesOrderHeader.ProfileNum} and OrderNumber='{data.SalesOrderHeader.OrderNumber}'"))
            {
                IsValid = false;
                AddError($"[SalesOrderHeader.ProfileNum: {data.SalesOrderHeader.ProfileNum},[SalesOrderHeader.SalesOrderUuid: {data.SalesOrderHeader.SalesOrderUuid} ]is duplicate.");
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

        #endregion Async Methods
    }
}



