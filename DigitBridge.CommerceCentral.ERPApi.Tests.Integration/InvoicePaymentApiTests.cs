using DigitBridge.CommerceCentral.ApiCommon;
using DigitBridge.CommerceCentral.ERPDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using Xunit;
namespace DigitBridge.CommerceCentral.ERPApi.Tests.Integration
{

    public class InvoicePaymentApiTests
    {
        [Fact]
        public async void Http_trigger_should_return_known_string()
        {
            var reqestInfo = new RequestInfo<InvoiceTransactionDataDto>()
            {
                RequestHeader = new RequestHeader()
                {
                    ProfileNum = 1,
                    MasterAccountNum = 1
                },
                RequestBody = new InvoiceTransactionDataDto()
                {
                    InvoiceTransaction = new InvoiceTransactionDto()
                    {
                        InvoiceNumber = Guid.NewGuid().ToString(),
                        InvoiceUuid = Guid.NewGuid().ToString(),
                        TransUuid = Guid.NewGuid().ToString(),
                    }
                }
            };
            //var req = HttpRequestFactory.GetRequest("/api/salesorder","POST", reqestInfo);
            var req = HttpRequestFactory.GetRequest(reqestInfo);
            var response = await InvoicePaymentApi.AddInvoicePayments(req);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }

        //[Theory]
        //[MemberData(nameof(TestFactory.Data), MemberType = typeof(TestFactory))]
        //public async void Http_trigger_should_return_known_string_from_member_data(string queryStringKey, string queryStringValue)
        //{
        //    var request = TestFactory.CreateHttpRequest(queryStringKey, queryStringValue);
        //    var response = (OkObjectResult)await MyHttpTrigger.Run(request, logger);
        //    Assert.Equal($"Hello, {queryStringValue}. This HTTP triggered function executed successfully.", response.Value);
        //}

        //[Fact]
        //public void Timer_should_log_message()
        //{
        //    var logger = (ListLogger)TestFactory.CreateLogger(LoggerTypes.List);
        //    MyTimerTrigger.Run(null, logger);
        //    var msg = logger.Logs[0];
        //    Assert.Contains("C# Timer trigger function executed at", msg);
        //}

    }
}
