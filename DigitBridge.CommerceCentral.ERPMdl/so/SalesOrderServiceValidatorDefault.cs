    

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
    public partial class SalesOrderServiceValidatorDefault : IValidator<SalesOrderData>
    {
        public virtual bool IsValid { get; set; }
        public virtual IList<string> Messages { get; set; }

        public virtual void Clear()
        {
            IsValid = true;
            Messages = new List<string>();
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
                this.Messages.Add($"Sales Order not found.");
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
            var dbFactory = data.dbFactory;
            if (string.IsNullOrEmpty(data.SalesOrderHeader.SalesOrderUuid))
            {
                IsValid = false;
                this.Messages.Add($"Unique Id cannot be empty.");
                return IsValid;
            }
            //if (string.IsNullOrEmpty(data.SalesOrderHeader.CustomerUuid))
            //{
            //    IsValid = false;
            //    this.Messages.Add($"Customer cannot be empty.");
            //    return IsValid;
            //}
            return true;

        }

        protected virtual bool ValidateAdd(SalesOrderData data)
        {
            var dbFactory = data.dbFactory;
            if (data.SalesOrderHeader.RowNum != 0 && dbFactory.Exists<SalesOrderHeader>(data.SalesOrderHeader.RowNum))
            {
                IsValid = false;
                this.Messages.Add($"RowNum: {data.SalesOrderHeader.RowNum} is duplicate.");
                return IsValid;
            }
            return true;

        }

        protected virtual bool ValidateEdit(SalesOrderData data)
        {
            var dbFactory = data.dbFactory;
            if (data.SalesOrderHeader.RowNum == 0)
            {
                IsValid = false;
                this.Messages.Add($"RowNum: {data.SalesOrderHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.SalesOrderHeader.RowNum != 0 && !dbFactory.Exists<SalesOrderHeader>(data.SalesOrderHeader.RowNum))
            {
                IsValid = false;
                this.Messages.Add($"RowNum: {data.SalesOrderHeader.RowNum} not found.");
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
                this.Messages.Add($"RowNum: {data.SalesOrderHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.SalesOrderHeader.RowNum != 0 && !dbFactory.Exists<SalesOrderHeader>(data.SalesOrderHeader.RowNum))
            {
                IsValid = false;
                this.Messages.Add($"RowNum: {data.SalesOrderHeader.RowNum} not found.");
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
                this.Messages.Add($"Unique Id cannot be empty.");
                return IsValid;
            }
            //if (string.IsNullOrEmpty(data.SalesOrderHeader.CustomerUuid))
            //{
            //    IsValid = false;
            //    this.Messages.Add($"Customer cannot be empty.");
            //    return IsValid;
            //}
            return true;

        }

        protected virtual async Task<bool> ValidateAddAsync(SalesOrderData data)
        {
            var dbFactory = data.dbFactory;
            if (data.SalesOrderHeader.RowNum != 0 && (await dbFactory.ExistsAsync<SalesOrderHeader>(data.SalesOrderHeader.RowNum)))
            {
                IsValid = false;
                this.Messages.Add($"RowNum: {data.SalesOrderHeader.RowNum} is duplicate.");
                return IsValid;
            }
            return true;

        }

        protected virtual async Task<bool> ValidateEditAsync(SalesOrderData data)
        {
            var dbFactory = data.dbFactory;
            if (data.SalesOrderHeader.RowNum == 0)
            {
                IsValid = false;
                this.Messages.Add($"RowNum: {data.SalesOrderHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.SalesOrderHeader.RowNum != 0 && !(await dbFactory.ExistsAsync<SalesOrderHeader>(data.SalesOrderHeader.RowNum)))
            {
                IsValid = false;
                this.Messages.Add($"RowNum: {data.SalesOrderHeader.RowNum} not found.");
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
                this.Messages.Add($"RowNum: {data.SalesOrderHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.SalesOrderHeader.RowNum != 0 && !(await dbFactory.ExistsAsync<SalesOrderHeader>(data.SalesOrderHeader.RowNum)))
            {
                IsValid = false;
                this.Messages.Add($"RowNum: {data.SalesOrderHeader.RowNum} not found.");
                return IsValid;
            }
            return true;
        }

        #endregion Async Methods
    }
}



