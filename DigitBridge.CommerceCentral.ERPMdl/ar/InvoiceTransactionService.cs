


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
    public partial class InvoiceTransactionService
    {

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override InvoiceTransactionService Init()
        {
            base.Init();
            SetDtoMapper(new InvoiceTransactionDataDtoMapperDefault());
            SetCalculator(new InvoiceTransactionServiceCalculatorDefault());
            AddValidator(new InvoiceTransactionServiceValidatorDefault(this));
            return this;
        }


        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(InvoiceTransactionDataDto dto)
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
        public virtual async Task<bool> AddAsync(InvoiceTransactionDataDto dto)
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
        public virtual bool Update(InvoiceTransactionDataDto dto)
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
        public virtual async Task<bool> UpdateAsync(InvoiceTransactionDataDto dto)
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
        /// Delete invoice by transaction uuid
        /// </summary>
        /// <param name="invoiceUuid"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByTransUuidAsync(string transUuid, PayloadBase payloadBase)
        {
            if (string.IsNullOrEmpty(transUuid))
                return false;
            Delete();
            var success = await GetDataByIdAsync(transUuid);
            // validate before deleting
            if (!(await ValidatePayloadAsync(payloadBase).ConfigureAwait(false)))
                return false;
            success = success && await DeleteDataAsync();
            return success;
        }

    }
}



