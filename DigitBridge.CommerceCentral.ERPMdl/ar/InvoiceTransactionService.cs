


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
            AddValidator(new InvoiceTransactionServiceValidatorDefault(this, dbFactory));
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
        public virtual async Task<bool> AddAsync(InvoiceTransactionPayload payload)
        {
            // set Add mode and clear data
            Add();
            // validate before data loaded.  //TODO Add interface to servicebase
            //new InvoiceTransactionServiceValidatorDefault().Validating(payload, ProcessMode, dbFactory);
            // load data from dto
            FromDto(payload.InvoiceTransaction);
            // validate after data loaded //TODO Add interface to servicebase
            //new InvoiceTransactionServiceValidatorDefault().Validated(this.Data, payload, ProcessMode);

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
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(InvoiceTransactionPayload payload)
        {
            // set edit mode
            Edit();

            // validate before data loaded.  //TODO Add interface to servicebase
            //new InvoiceTransactionServiceValidatorDefault().Validating(payload, ProcessMode, dbFactory);

            // load data from db
            await GetDataAsync(payload.InvoiceTransaction.InvoiceTransaction.RowNum.ToLong()).ConfigureAwait(false);

            // validate after data loaded //TODO Add interface to servicebase
            //new InvoiceTransactionServiceValidatorDefault().Validated(this.Data, payload, ProcessMode);

            // load data from dto
            FromDto(payload.InvoiceTransaction);

            return await SaveDataAsync();
        }
        /// <summary>
        /// Delete invoice by transaction uuid
        /// </summary>
        /// <param name="invoiceUuid"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByTransUuidAsync(string transUuid, PayloadBase payload)
        {
            if (string.IsNullOrEmpty(transUuid))
                return false;
            // set mode to delete
            Delete();
            var success = await GetDataByIdAsync(transUuid);
            /// validate after data loaded 
            if (!(await ValidateAccountAsync(payload).ConfigureAwait(false)))
                return false;

            success = success && await DeleteDataAsync();
            return success;
        }

    }
}



