    

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
    public partial class InvoiceServiceValidatorDefault : IValidator<InvoiceData>
    {
        public virtual bool IsValid { get; set; }
        public virtual IList<string> Messages { get; set; }

        public virtual void Clear()
        {
            IsValid = true;
            Messages = new List<string>();
        }

        public virtual bool ValidatePayload(InvoiceData data, IPayload payload, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            Clear();
            var pl = payload as InvoicePayload;
            if (processingMode == ProcessingMode.Add)
            {
                //TODO set MasterAccountNum, ProfileNum and DatabaseNum from payload
                data.InvoiceHeader.MasterAccountNum = pl.MasterAccountNum;
                data.InvoiceHeader.ProfileNum = pl.ProfileNum;
                data.InvoiceHeader.DatabaseNum = pl.DatabaseNum;
            }
            else 
            {
                //TODO check MasterAccountNum, ProfileNum and DatabaseNum between data and payload
                if (
                    data.InvoiceHeader.MasterAccountNum != pl.MasterAccountNum ||
                    data.InvoiceHeader.ProfileNum != pl.ProfileNum
                )
                    IsValid = false;
                this.Messages.Add($"Invoice not found.");
                return IsValid;
            }
            return true;
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
                this.Messages.Add($"Unique Id cannot be empty.");
                return IsValid;
            }
            //if (string.IsNullOrEmpty(data.InvoiceHeader.CustomerUuid))
            //{
            //    IsValid = false;
            //    this.Messages.Add($"Customer cannot be empty.");
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
                this.Messages.Add($"RowNum: {data.InvoiceHeader.RowNum} is duplicate.");
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
                this.Messages.Add($"RowNum: {data.InvoiceHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.InvoiceHeader.RowNum != 0 && !dbFactory.Exists<InvoiceHeader>(data.InvoiceHeader.RowNum))
            {
                IsValid = false;
                this.Messages.Add($"RowNum: {data.InvoiceHeader.RowNum} not found.");
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
                this.Messages.Add($"RowNum: {data.InvoiceHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.InvoiceHeader.RowNum != 0 && !dbFactory.Exists<InvoiceHeader>(data.InvoiceHeader.RowNum))
            {
                IsValid = false;
                this.Messages.Add($"RowNum: {data.InvoiceHeader.RowNum} not found.");
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
                this.Messages.Add($"Unique Id cannot be empty.");
                return IsValid;
            }
            //if (string.IsNullOrEmpty(data.InvoiceHeader.CustomerUuid))
            //{
            //    IsValid = false;
            //    this.Messages.Add($"Customer cannot be empty.");
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
                this.Messages.Add($"RowNum: {data.InvoiceHeader.RowNum} is duplicate.");
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
                this.Messages.Add($"RowNum: {data.InvoiceHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.InvoiceHeader.RowNum != 0 && !(await dbFactory.ExistsAsync<InvoiceHeader>(data.InvoiceHeader.RowNum)))
            {
                IsValid = false;
                this.Messages.Add($"RowNum: {data.InvoiceHeader.RowNum} not found.");
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
                this.Messages.Add($"RowNum: {data.InvoiceHeader.RowNum} not found.");
                return IsValid;
            }

            if (data.InvoiceHeader.RowNum != 0 && !(await dbFactory.ExistsAsync<InvoiceHeader>(data.InvoiceHeader.RowNum)))
            {
                IsValid = false;
                this.Messages.Add($"RowNum: {data.InvoiceHeader.RowNum} not found.");
                return IsValid;
            }
            return true;
        }

        #endregion Async Methods
    }
}



