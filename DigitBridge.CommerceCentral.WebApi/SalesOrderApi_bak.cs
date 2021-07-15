using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPMdl;
using DigitBridge.CommerceCentral.YoPoco;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.WebApi
{
    [ApiExplorerSettings(GroupName = "SalesOrder")]
    /// <summary>
    /// Processing sales order business
    /// </summary>
    [Obsolete]
    public class SalesOrderApi : BaseApi
    {
        /// <summary>
        /// get SalesOrder dto by orderNumber
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(SalesOrderDataDto), (int)HttpStatusCode.OK)]
        [FunctionName("SalesOrder")]
        public async Task<IActionResult> GetSalesOrder(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "salesorder/{orderNumber}")]
            HttpRequest req,
            ILogger log,
            string orderNumber)
        {
            var dataBaseFactory = new DataBaseFactory(ConfigFile.Dsn);
            var srv = new SalesOrderService(dataBaseFactory);
            srv.GetByOrderNumber(orderNumber);
            var dto = srv.ToDto(srv.Data);
            return new OkObjectResult(dto);
        }

        /// <summary>
        /// add one SalesOrderDto 
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.Created)]
        [FunctionName("AddSalesOrder")]

        public async Task<IActionResult> AddSalesOrder(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "AddSalesOrder")]
             object dto)
        {
            var dataBaseFactory = new DataBaseFactory(ConfigFile.Dsn);
            var srv = new SalesOrderService(dataBaseFactory);
            //srv.Add(dto);
            return new OkObjectResult(srv.Data.UniqueId);
        }
    }
}
