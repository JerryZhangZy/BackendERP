
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
    public partial class QboInvoiceServiceTests
    {


        #region Handle by number. This for api

        [Fact()]
        public async Task GetQboInvoiceByNumberAsync_Test()
        {
            var payload = new QboInvoicePayload() { MasterAccountNum = MasterAccountNum, ProfileNum = ProfileNum };
            var srv = new QboInvoiceService(payload, DataBaseFactory);
            var invoiceNumber = GetErpInvoiceNumber();
            var success = await srv.ExportByNumberAsync(invoiceNumber);

            success = success && await srv.GetQboInvoiceByNumberAsync(invoiceNumber);

            Assert.True(success, srv.Messages.ObjectToString());
        }

        [Fact()]
        public async Task ExportByNumberAsync_Test()
        {
            var payload = new QboInvoicePayload() { MasterAccountNum = MasterAccountNum, ProfileNum = ProfileNum };
            var srv = new QboInvoiceService(payload, DataBaseFactory);
            var invoiceNumber = GetErpInvoiceNumber();
            var result = await srv.ExportByNumberAsync(invoiceNumber);
            Assert.True(result, srv.Messages.ObjectToString());
        }

        //[Fact()]
        //public async Task DeleteQboInvoiceByNumberAsync_Test()
        //{
        //    var payload = new QboInvoicePayload() { MasterAccountNum = MasterAccountNum, ProfileNum = ProfileNum };
        //    var srv = new QboInvoiceService(payload, DataBaseFactory);
        //    var invoiceNumber = GetErpInvoiceNumber();
        //    var success = await srv.ExportByNumberAsync(invoiceNumber);

        //    success = success && await srv.DeleteQboInvoiceByNumberAsync(invoiceNumber);

        //    Assert.True(success, "This is a generated tester, please report any tester bug to team leader.");
        //}

        [Fact()]
        public async Task VoidQboInvoiceByNumberAsync_Test()
        {
            var payload = new QboInvoicePayload() { MasterAccountNum = MasterAccountNum, ProfileNum = ProfileNum };
            var srv = new QboInvoiceService(payload, DataBaseFactory);
            var invoiceNumber = GetErpInvoiceNumber();
            var success = await srv.ExportByNumberAsync(invoiceNumber);

            success = success && await srv.VoidQboInvoiceByNumberAsync(invoiceNumber);

            Assert.True(success, srv.Messages.ObjectToString());
        }

        #endregion

        #region Handle by uuid. This for internal


        [Fact()]
        public async Task ExportByUuidAsync_Test()
        {
            var payload = new QboInvoicePayload() { MasterAccountNum = MasterAccountNum, ProfileNum = ProfileNum };
            var srv = new QboInvoiceService(payload, DataBaseFactory);
            var invoiceUuid = GetErpInvoiceUuid();
            var result = await srv.ExportByUuidAsync(invoiceUuid);
            Assert.True(result, srv.Messages.ObjectToString());
        }

        //[Fact()]
        //public async Task DeleteQboInvoiceByUuidAsync_Test()
        //{
        //    var payload = new QboInvoicePayload() { MasterAccountNum = MasterAccountNum, ProfileNum = ProfileNum };
        //    var srv = new QboInvoiceService(payload, DataBaseFactory);
        //    var invoiceUuid = GetErpInvoiceUuid();
        //    var success = await srv.ExportByNumberAsync(invoiceUuid);

        //    success = success && await srv.DeleteQboInvoiceByUuidAsync(invoiceUuid);

        //    Assert.True(success, "This is a generated tester, please report any tester bug to team leader.");
        //}

        [Fact()]
        public async Task VoidQboInvoiceByUuidAsync_Test()
        {
            var payload = new QboInvoicePayload() { MasterAccountNum = MasterAccountNum, ProfileNum = ProfileNum };
            var srv = new QboInvoiceService(payload, DataBaseFactory);
            var invoiceUuid = GetErpInvoiceUuid();
            var success = await srv.ExportByUuidAsync(invoiceUuid);

            success = success && await srv.VoidQboInvoiceByUuidAsync(invoiceUuid);

            Assert.True(success, srv.Messages.ObjectToString());
        }
        #endregion
    }
}



