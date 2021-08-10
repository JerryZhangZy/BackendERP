
    

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
    public partial class InvoiceService
    {

        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override InvoiceService Init()
        {
            base.Init();
            SetDtoMapper(new InvoiceDataDtoMapperDefault());
            SetCalculator(new InvoiceServiceCalculatorDefault());
            AddValidator(new InvoiceServiceValidatorDefault(this, dbFactory));
            return this;
        }


        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(InvoiceDataDto dto)
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
        public virtual async Task<bool> AddAsync(InvoiceDataDto dto)
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
        public virtual async Task<bool> AddAsync(InvoicePayload payload)
        {
            if (payload is null || !payload.HasInvoice)
                return false;

            // set Add mode and clear data
            Add();
            if (!(await ValidateAsync(payload.Invoice).ConfigureAwait(false)))
                return false;
            // load data from dto
            FromDto(payload.Invoice);

            if (!(await ValidateAccountAsync(payload).ConfigureAwait(false)))
                return false;

            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync().ConfigureAwait(false);
        }
        /// <summary>
        /// Update data from Dto object.
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual bool Update(InvoiceDataDto dto)
        {
            if (dto is null || !dto.HasInvoiceHeader || dto.InvoiceHeader.RowNum.ToLong() <= 0)
                return false;
            // set Add mode and clear data
            Edit(dto.InvoiceHeader.RowNum.ToLong());
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
        public virtual async Task<bool> UpdateAsync(InvoiceDataDto dto)
        {
            if (dto is null || !dto.HasInvoiceHeader || dto.InvoiceHeader.RowNum.ToLong() <= 0)
                return false;
            // set Add mode and clear data
            await EditAsync(dto.InvoiceHeader.RowNum.ToLong()).ConfigureAwait(false);
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
        public virtual async Task<bool> UpdateAsync(InvoicePayload payload)
        {
            if (payload is null || !payload.HasInvoice || payload.Invoice.InvoiceHeader.RowNum.ToLong() <= 0)
                return false;
            // set Add mode and clear data
            await EditAsync(payload.Invoice.InvoiceHeader.RowNum.ToLong()).ConfigureAwait(false);

            if (!(await ValidateAsync(payload.Invoice).ConfigureAwait(false)))
                return false;

            // validate data for Add processing
            if (!(await ValidateAccountAsync(payload).ConfigureAwait(false)))
                return false;

            // load data from dto
            FromDto(payload.Invoice);
            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync();
        }
        /// <summary>
        /// Get invoice with detail by invoiceNumber
        /// </summary>
        /// <param name="invoiceNumber"></param>
        /// <returns></returns>
        public virtual async Task<bool> GetByInvoiceNumberAsync(string invoiceNumber, InvoicePayload payload)
        {
            if (string.IsNullOrEmpty(invoiceNumber))
                return false;
            List();
            var rowNum = await _data.GetRowNumAsync(invoiceNumber,payload.ProfileNum,payload.MasterAccountNum);
            if (!rowNum.HasValue)
                return false;
            var success = await GetDataAsync(rowNum.Value);
            //if (success) ToDto();
            return success;
        }
        /// <summary>
        /// Delete invoice by invoiceUuid
        /// </summary>
        /// <param name="invoiceUuid"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteByInvoiceUuidAsync(string invoiceUuid, InvoicePayload payload)
        {
            if (string.IsNullOrEmpty(invoiceUuid))
                return false;
            Delete();  
            var success = await GetDataByIdAsync(invoiceUuid);
            // validate before deleting
            if (!(await ValidateAccountAsync(payload).ConfigureAwait(false)))
                return false;
            success = success && await DeleteDataAsync();
            return success;
        }

    }
}



