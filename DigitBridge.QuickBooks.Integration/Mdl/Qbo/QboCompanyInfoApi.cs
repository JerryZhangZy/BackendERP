using DigitBridge.CommerceCentral.YoPoco;
using Intuit.Ipp.Data;
using Intuit.Ipp.QueryFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.QuickBooks.Integration.Mdl.Qbo
{
    public class QboCompanyInfoApi : QboServiceBase
    {

        public QboCompanyInfoApi(IPayload payload, IDataBaseFactory databaseFactory) : base(payload, databaseFactory) { }

        private QueryService<CompanyInfo> _companyInfoQueryService;

        protected async Task<QueryService<CompanyInfo>> GetCompanyInfoQueryService()
        {
            if (_companyInfoQueryService == null)
                _companyInfoQueryService = await GetQueryServiceAsync<CompanyInfo>();
            return _companyInfoQueryService;
        }

        public async Task<List<CompanyInfo>> GetCompanyInfosAsync()
        {
            var companyInfoService = await GetCompanyInfoQueryService();
            return companyInfoService.ExecuteIdsQuery("SELECT * FROM CompanyInfo").ToList();
        }

        public async Task<CompanyInfo> CreateOrUpdateCompanyInfo(CompanyInfo companyInfo)
        {
            if (!await CompanyInfoExistAsync(companyInfo.Id))
            {
                //return await AddDataAsync(companyInfo);
                return await AddDataAsync(companyInfo);
            }
            else
            {
                return await UpdateDataAsync(companyInfo);
            }
        }

        public async Task<CompanyInfo> CreateCompanyInfoIfAbsent(CompanyInfo companyInfo)
        {
            if (!await CompanyInfoExistAsync(companyInfo.Id))
            {
                return await AddDataAsync(companyInfo);
            }
            return null;
        }

        public async Task<bool> CompanyInfoExistAsync(string id)
        {
            var queryService = await GetCompanyInfoQueryService();
            return queryService.ExecuteIdsQuery($"select * from CompanyInfo Where id = '{id}'").FirstOrDefault() != null;
        }

        public async Task<CompanyInfo> DeleteCompanyInfoAsync(CompanyInfo companyInfo)
        {
            if (companyInfo != null)
                return await DeleteDataAsync(companyInfo);
            return null;
        }

        public async Task<CompanyInfo> DeleteCompanyInfoAsync(string id)
        {
            var companyInfo = await GetCompanyInfoByIdAsync(id);
            if (companyInfo != null)
            {
                return await DeleteCompanyInfoAsync(companyInfo);
            }
            return null;
        }

        public async Task<CompanyInfo> UpdateCompanyInfoAsync(CompanyInfo companyInfo)
        {
            return await UpdateDataAsync(companyInfo);
        }

        /// <summary>
        /// Check if Channel CompanyInfo Id from Database can be found in Quickbooks Online
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CompanyInfo> GetCompanyInfoByIdAsync(string id)
        {
            var companyInfoService = await GetCompanyInfoQueryService();
            return companyInfoService.ExecuteIdsQuery("SELECT * FROM CompanyInfo where id = '" + id + "'").FirstOrDefault();
        }
    }
}
