
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Xunit;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.XUnit.Common;
using DigitBridge.CommerceCentral.ERPDb;
using Bogus;
using DigitBridge.CommerceCentral.ERPDb.Tests.Integration;
using DigitBridge.CommerceCentral.ERPMdl;

namespace DigitBridge.QuickBooks.Integration.Tests
{
    public partial class QboRefundServiceTests
    {

        #region Handle by number. This for api
        private async Task<bool> ExportInvoiceByNumberAsync(string invoiceNumber)
        {
            var payload = new QboInvoicePayload() { MasterAccountNum = MasterAccountNum, ProfileNum = ProfileNum };
            var srv = new QboInvoiceService(payload, DataBaseFactory);
            return await srv.ExportByNumberAsync(invoiceNumber);
        }


        [Fact()]
        public async Task GetQboRefundByNumberAsync_Test()
        {
            var payload = new QboRefundPayload() { MasterAccountNum = MasterAccountNum, ProfileNum = ProfileNum };
            var srv = new QboRefundService(payload, DataBaseFactory);
            (var invoiceNumber, var tranNum) = await GetErpInvoiceNumberAndTranNumAsync();

            var success = await ExportInvoiceByNumberAsync(invoiceNumber);

            success = success && await srv.ExportByNumberAsync(invoiceNumber, tranNum);

            success = success && await srv.GetQboRefundByNumberAsync(invoiceNumber, tranNum);

            Assert.True(success, srv.Messages.ObjectToString());
        }

        [Fact()]
        public async Task ExportByNumberAsync_Test()
        {
            var payload = new QboRefundPayload() { MasterAccountNum = MasterAccountNum, ProfileNum = ProfileNum };
            var srv = new QboRefundService(payload, DataBaseFactory);
            (var invoiceNumber, var tranNum) = await GetErpInvoiceNumberAndTranNumAsync();

            var success = await ExportInvoiceByNumberAsync(invoiceNumber);

            success = success && await srv.ExportByNumberAsync(invoiceNumber, tranNum);

            Assert.True(success, srv.Messages.ObjectToString());
        }

        [Fact()]
        public async Task DeleteQboRefundByNumberAsync_Test()
        {
            var payload = new QboRefundPayload() { MasterAccountNum = MasterAccountNum, ProfileNum = ProfileNum };
            var srv = new QboRefundService(payload, DataBaseFactory);
            (var invoiceNumber, var tranNum) = await GetErpInvoiceNumberAndTranNumAsync();

            var success = await ExportInvoiceByNumberAsync(invoiceNumber);

            success = success && await srv.ExportByNumberAsync(invoiceNumber, tranNum);

            success = success && await srv.DeleteQboRefundByNumberAsync(invoiceNumber, tranNum);

            Assert.True(success, srv.Messages.ObjectToString());
        }



        #endregion

        #region Handle by uuid. This for internal
        private async Task<bool> ExportInvoiceByUuidAsync(string invoiceUuid)
        {
            var payload = new QboInvoicePayload() { MasterAccountNum = MasterAccountNum, ProfileNum = ProfileNum };
            var srv = new QboInvoiceService(payload, DataBaseFactory);

            return await srv.ExportByUuidAsync(invoiceUuid);
        }

        [Fact()]
        public async Task ExportByUuidAsync_Test()
        {
            var payload = new QboRefundPayload() { MasterAccountNum = MasterAccountNum, ProfileNum = ProfileNum };
            var srv = new QboRefundService(payload, DataBaseFactory);

            (var invoiceUuid, var transUuid) =await GetErpInvoiceUuidAndTransUuidAsync();
            var success = await ExportInvoiceByUuidAsync(invoiceUuid);
            success = success && await srv.ExportByUuidAsync(transUuid);

            Assert.True(success, srv.Messages.ObjectToString());
        }

        [Fact()]
        public async Task DeleteQboRefundByUuidAsync_Test()
        {
            var payload = new QboRefundPayload() { MasterAccountNum = MasterAccountNum, ProfileNum = ProfileNum };
            var srv = new QboRefundService(payload, DataBaseFactory);

            (var invoiceUuid, var transUuid) = await GetErpInvoiceUuidAndTransUuidAsync();
            var success = await ExportInvoiceByUuidAsync(invoiceUuid);
            success = success && await srv.ExportByUuidAsync(transUuid);

            success = success && await srv.DeleteQboRefundByUuidAsync(invoiceUuid);

            Assert.True(success, srv.Messages.ObjectToString());
        }

        #endregion
    }
}


