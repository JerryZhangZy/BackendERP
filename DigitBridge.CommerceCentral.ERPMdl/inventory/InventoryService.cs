


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
    public partial class InventoryService
    {
        /// <summary>
        /// Initiate service objcet, set instance of DtoMapper, Calculator and Validator 
        /// </summary>
        public override InventoryService Init()
        {
            base.Init();
            SetDtoMapper(new InventoryDataDtoMapperDefault());
            SetCalculator(new InventoryServiceCalculatorDefault());
            AddValidator(new InventoryServiceValidatorDefault(this));
            return this;
        }

        public string GetProductUuidBySku(int profileNum, string sku)
        {
            return dbFactory.Db.FirstOrDefault<string>($"select ProductUuid from ProductBasic where Sku='{sku}' and ProfileNum={profileNum}");
        }

        public List<string> GetProductUuidsBySkuArray(int profileNum, IList<string> skus)
        {
            var skusWhere = string.Join(",", skus.Select(x => $"'{x}'").ToArray());
            return dbFactory.Db.Query<string>($"select ProductUuid from ProductBasic where Sku in ({skusWhere}) and ProfileNum={profileNum}").ToList();
        }

        public ProductExPayload GetInventorysBySkuArray(ProductExPayload payload)
        {
            var uuids = GetProductUuidsBySkuArray(payload.ProfileNum, payload.Skus);
            var list = new List<InventoryDataDto>();
            uuids.ForEach(x =>
            {
                if (GetDataById(x))
                    list.Add(ToDto());
            });
            payload.InventoryDatas = list;
            return payload;
        }

        public InventoryDataDto GetInventoryBySku(int profileNum, string sku)
        {
            var uuid = GetProductUuidBySku(profileNum, sku);
            GetDataById(uuid);
            return ToDto();
        }

        public ProductExPayload Add(ProductExPayload payload)
        {
            if (payload is null || !payload.HasInventoryData)
                return payload;

            // set Add mode and clear data
            Add();
            // load data from dto
            FromDto(payload.InventoryData);

            if (!ValidatePayload(payload))
                return payload;

            // validate data for Add processing
            if (!Validate())
                return payload;

            if (SaveData())
                payload.InventoryData = ToDto();
            return payload;
        }

        public async Task<ProductExPayload> AddAsync(ProductExPayload payload)
        {
            if (payload is null || !payload.HasInventoryData)
                return payload;

            // set Add mode and clear data
            Add();
            // load data from dto
            FromDto(payload.InventoryData);

            if (!(await ValidatePayloadAsync(payload).ConfigureAwait(false)))
                return payload;

            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return payload;

            if (await SaveDataAsync().ConfigureAwait(false))
                payload.InventoryData = ToDto();
            return payload;
        }


        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual bool Add(InventoryDataDto dto)
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

        public bool DeleteBySku(int profileNum, string sku)
        {
            var uuid = GetProductUuidBySku(profileNum, sku);
            return Delete(uuid);
        }
        public async Task<ProductExPayload> DeleteBySkuAsync(ProductExPayload payload)
        {
            var uuid = GetProductUuidBySku(payload.ProfileNum, payload.Skus.First());
            if(await DeleteAsync(uuid)){
                payload.InventoryData = ToDto();
            }
            return payload;
        }

        /// <summary>
        /// Add new data from Dto object
        /// </summary>
        public virtual async Task<bool> AddAsync(InventoryDataDto dto)
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
        public virtual bool Update(InventoryDataDto dto)
        {
            if (dto is null || !dto.HasProductBasic || dto.ProductBasic.RowNum.ToLong() <= 0)
                return false;
            // set Add mode and clear data
            Edit(dto.ProductBasic.RowNum.ToLong());
            // load data from dto
            FromDto(dto);
            // validate data for Add processing
            if (!Validate())
                return false;

            return SaveData();
        }

        public ProductExPayload Update(ProductExPayload payload)
        {
            if (payload is null || !payload.HasInventoryData || payload.InventoryData.ProductBasic.RowNum.ToLong() <= 0)
                return payload;
            // set Add mode and clear data
            Edit(payload.InventoryData.ProductBasic.RowNum.ToLong());

            if (!ValidatePayload(payload))
                return payload;

            // load data from dto
            FromDto(payload.InventoryData);
            // validate data for Add processing
            if (!Validate())
                return payload;

            if (SaveData())
                payload.InventoryData = ToDto();
            return payload;
        }

        public async Task<ProductExPayload> UpdateAsync(ProductExPayload payload)
        {

            if (payload is null || !payload.HasInventoryData || payload.InventoryData.ProductBasic.RowNum.ToLong() <= 0)
                return payload;
            // set Add mode and clear data
            await EditAsync(payload.InventoryData.ProductBasic.RowNum.ToLong()).ConfigureAwait(false);

            // validate data for Add processing
            if (!(await ValidatePayloadAsync(payload).ConfigureAwait(false)))
                return payload;

            // load data from dto
            FromDto(payload.InventoryData);
            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return payload;

            if (await SaveDataAsync())
                payload.InventoryData = ToDto();
            return payload;
        }

        /// <summary>
        /// Update data from Dto object
        /// This processing will load data by RowNum of Dto, and then use change data by Dto.
        /// </summary>
        public virtual async Task<bool> UpdateAsync(InventoryDataDto dto)
        {
            if (dto is null || !dto.HasProductBasic || dto.ProductBasic.RowNum.ToLong() <= 0)
                return false;
            // set Add mode and clear data
            await EditAsync(dto.ProductBasic.RowNum.ToLong()).ConfigureAwait(false);
            // load data from dto
            FromDto(dto);
            // validate data for Add processing
            if (!(await ValidateAsync().ConfigureAwait(false)))
                return false;

            return await SaveDataAsync();
        }

    }
}



