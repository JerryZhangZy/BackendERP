using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPMdl;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using DigitBridge.Base.Utility;

namespace DigitBridge.CommerceCentral.WebApi
{
    /// <summary>
    /// Process sale order
    /// </summary>
    [ApiExplorerSettings(GroupName = "SalesOrders")]
    public class SalesOrderApi : BaseApi
    {
        /// <summary>
        /// Delete sales order 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(SalesOrderDataDto), (int)HttpStatusCode.OK)]
        [FunctionName("Delete")]
        public async Task<IActionResult> Delete(
           [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "salesorder")]
             SalesOrderDataDto dto)
        {
            var dataBaseFactory = new DataBaseFactory(ConfigFile.Dsn);
            var srv = new SalesOrderService(dataBaseFactory);
            var result = srv.Add(dto);
            return new OkObjectResult(result);
        }
        /// <summary>
        /// Update sales order 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(SalesOrderDataDto), (int)HttpStatusCode.OK)]
        [FunctionName("Update")]
        public async Task<IActionResult> Update(
           [HttpTrigger(AuthorizationLevel.Function, "patch", Route = "salesorder")]
             SalesOrderDataDto dto)
        {
            var dataBaseFactory = new DataBaseFactory(ConfigFile.Dsn);
            var srv = new SalesOrderService(dataBaseFactory);
            var result = srv.Add(dto);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Add sales order 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns> 
        [FunctionName("Add")]
        public async Task<IActionResult> Add(
           [HttpTrigger(AuthorizationLevel.Function, "post", Route = "salesorder")]
           [RequestBodyType(typeof(SalesOrderDataDto), "dto")]
           HttpRequest httpRequest)
        {
            if (httpRequest.Method.Equals("post", StringComparison.OrdinalIgnoreCase))
            {
                using (var reader = new StreamReader(httpRequest.Body))
                {
                    var json = await reader.ReadToEndAsync();
                    var dto = json.StringToObject<SalesOrderDataDto>(); //JsonConvert.DeserializeObject<SalesOrderDataDto>(json); 
                    var dataBaseFactory = new DataBaseFactory(ConfigFile.Dsn);
                    var srv = new SalesOrderService(dataBaseFactory);
                    var result = srv.Add(dto);
                    return new OkObjectResult(result);
                }
            }
           
            return new OkObjectResult(false);
        }

        /// <summary>
        /// Get sales order
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(SalesOrderDataDto), (int)HttpStatusCode.OK)]
        [QueryStringParameter("orderNumber", "it is orderNumber parameter", DataType = typeof(string), Required = true)]
        [FunctionName("Get")]
        public async Task<IActionResult> Get(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "salesorder")]
            HttpRequest req
            )
        {
            //todo get orderNumber from querystring
            var orderNumber = "a8hungmbtd19t8qo3pgipvcc8uaxdsjnngi7l4tcpeknvsqhza";
            var dataBaseFactory = new DataBaseFactory(ConfigFile.Dsn);
            var srv = new SalesOrderService(dataBaseFactory);
            var success = await srv.GetByOrderNumberAsync(orderNumber);
            if (success)
            {
                var dto = srv.ToDto(srv.Data);
                return new OkObjectResult(dto);
            }
            else
                return new NoContentResult();
        }
        [ProducesResponseType(typeof(SalesOrderDataDto), (int)HttpStatusCode.OK)]
        [FunctionName("GetByOrderNumber")]
        public async Task<IActionResult> GetByOrderNumber(
            string orderNumber,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "salesorder/{orderNumber}")]
            HttpRequest req
            )
        {
            var dataBaseFactory = new DataBaseFactory(ConfigFile.Dsn);
            var srv = new SalesOrderService(dataBaseFactory);
            var success = await srv.GetByOrderNumberAsync(orderNumber);
            if (success)
            {
                var dto = srv.ToDto(srv.Data);
                return new OkObjectResult(dto);
            }
            else
                return new NoContentResult();
        }
    }
}
