    

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
    public partial class InventoryServiceValidatorDefault : IValidator<InventoryData>
    {
        public virtual bool IsValid { get; set; }
        public virtual IList<string> Messages { get; set; }

        public virtual void Clear()
        {
            IsValid = true;
            Messages = new List<string>();
        }

        public virtual bool ValidatePayload(InventoryData data, IPayload payload, ProcessingMode processingMode = ProcessingMode.Edit)
        {
            Clear();
            var pl = payload as ProductExPayload;
            if (processingMode == ProcessingMode.Add)
            {
                //TODO set MasterAccountNum, ProfileNum and DatabaseNum from payload
                data.ProductBasic.MasterAccountNum = pl.MasterAccountNum;
                data.ProductBasic.ProfileNum = pl.ProfileNum;
                data.ProductBasic.DatabaseNum = pl.DatabaseNum;
            }
            else
            {
                //TODO check MasterAccountNum, ProfileNum and DatabaseNum between data and payload
                if (
                    data.ProductBasic.MasterAccountNum != pl.MasterAccountNum ||
                    data.ProductBasic.ProfileNum != pl.ProfileNum
                )
                    IsValid = false;
                this.Messages.Add($"Sales Order not found.");
                return IsValid;
            }
            return true;
        }

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
                this.Messages.Add($"Unique Id cannot be empty.");
                return IsValid;
            }
            //if (string.IsNullOrEmpty(data.ProductBasic.CustomerUuid))
            //{
            //    IsValid = false;
            //    this.Messages.Add($"Customer cannot be empty.");
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
                this.Messages.Add($"RowNum: {data.ProductBasic.RowNum} is duplicate.");
                return IsValid;
            }
            return true;

        }

        protected virtual bool ValidateEdit(InventoryData data)
        {
            var dbFactory = data.dbFactory;
            if (data.ProductBasic.RowNum == 0)
            {
                IsValid = false;
                this.Messages.Add($"RowNum: {data.ProductBasic.RowNum} not found.");
                return IsValid;
            }

            if (data.ProductBasic.RowNum != 0 && !dbFactory.Exists<ProductBasic>(data.ProductBasic.RowNum))
            {
                IsValid = false;
                this.Messages.Add($"RowNum: {data.ProductBasic.RowNum} not found.");
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
                this.Messages.Add($"RowNum: {data.ProductBasic.RowNum} not found.");
                return IsValid;
            }

            if (data.ProductBasic.RowNum != 0 && !dbFactory.Exists<ProductBasic>(data.ProductBasic.RowNum))
            {
                IsValid = false;
                this.Messages.Add($"RowNum: {data.ProductBasic.RowNum} not found.");
                return IsValid;
            }
            return true;
        }


        #region Async Methods

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
                this.Messages.Add($"Unique Id cannot be empty.");
                return IsValid;
            }
            //if (string.IsNullOrEmpty(data.ProductBasic.CustomerUuid))
            //{
            //    IsValid = false;
            //    this.Messages.Add($"Customer cannot be empty.");
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
                this.Messages.Add($"RowNum: {data.ProductBasic.RowNum} is duplicate.");
                return IsValid;
            }
            return true;

        }

        protected virtual async Task<bool> ValidateEditAsync(InventoryData data)
        {
            var dbFactory = data.dbFactory;
            if (data.ProductBasic.RowNum == 0)
            {
                IsValid = false;
                this.Messages.Add($"RowNum: {data.ProductBasic.RowNum} not found.");
                return IsValid;
            }

            if (data.ProductBasic.RowNum != 0 && !(await dbFactory.ExistsAsync<ProductBasic>(data.ProductBasic.RowNum)))
            {
                IsValid = false;
                this.Messages.Add($"RowNum: {data.ProductBasic.RowNum} not found.");
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
                this.Messages.Add($"RowNum: {data.ProductBasic.RowNum} not found.");
                return IsValid;
            }

            if (data.ProductBasic.RowNum != 0 && !(await dbFactory.ExistsAsync<ProductBasic>(data.ProductBasic.RowNum)))
            {
                IsValid = false;
                this.Messages.Add($"RowNum: {data.ProductBasic.RowNum} not found.");
                return IsValid;
            }
            return true;
        }

        #endregion Async Methods
    }
}



