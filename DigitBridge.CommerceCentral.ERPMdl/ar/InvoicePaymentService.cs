
    

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public partial class InvoicePaymentService
    {

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override InvoicePaymentService Init()
        {
            base.Init();
            SetDtoMapper(new InvoicePaymentDataDtoMapperDefault());
            SetCalculator(new InvoicePaymentServiceCalculatorDefault());
            AddValidator(new InvoicePaymentServiceValidatorDefault());
            return this;
        }


        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(InvoicePaymentDataDto dto)
        {
            if (dto is null) 
                return false;
            // set Add mode and clear data
            Add();
            // load data from dto
            FromDto(dto);
            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual async Task<bool> AddAsync(InvoicePaymentDataDto dto)
        {
            if (dto is null)
                return false;
            // set Add mode and clear data
            Add();
            // load data from dto
            FromDto(dto);
            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Update data from Dto object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(InvoicePaymentDataDto dto)
        {
            if (dto is null || !dto.HasInvoiceTransaction || dto.InvoiceTransaction.RowNum.ToLong() <= 0)
                return false;
            // set Add mode and clear data
            Edit(dto.InvoiceTransaction.RowNum.ToLong());
            // load data from dto
            FromDto(dto);
            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(InvoicePaymentDataDto dto)
        {
            if (dto is null || !dto.HasInvoiceTransaction || dto.InvoiceTransaction.RowNum.ToLong() <= 0)
                return false;
            // set Add mode and clear data
            await EditAsync(dto.InvoiceTransaction.RowNum.ToLong()).ConfigureAwait(false);
            // load data from dto
            FromDto(dto);
            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync();
        }
        /// <summary>
        /// Delete invoice payments by invoice number
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByInvoiceNumberAsync(string invoiceNumber)
        {
            if (string.IsNullOrEmpty(invoiceNumber))
                return false;
            Delete();
            var rowNum = await _data.GetRowNumAsync(invoiceNumber);
            if (!rowNum.HasValue)
                return false;
            var success = await GetDataAsync(rowNum.Value);
            success = success && await DeleteDataAsync();
            return success;
        }
        /// <summary>
        /// Get invoice payment with detail by invoiceNumber
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> GetByInvoiceNumberAsync(string invoiceNumber)
        {
            if (string.IsNullOrEmpty(invoiceNumber))
                return false;
            List();
            var rowNum = await _data.GetRowNumAsync(invoiceNumber);
            if (!rowNum.HasValue)
                return false;
            var success = await GetDataAsync(rowNum.Value);
            //if (success) ToDto();
            return success;
        }
    }
}



