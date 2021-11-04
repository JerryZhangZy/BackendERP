﻿


//-------------------------------------------------------------------------
// This document is generated by T4
// It will overwrite your changes, please keep it as it is
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
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
using Bogus;
using DigitBridge.CommerceCentral.ERPMdl;
using DigitBridge.CommerceCentral.ERPDb;
using DigitBridge.CommerceCentral.ERPDb.Tests.Integration;

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
 
    public partial class ApInvoiceListDataTests
    {

        public const int MasterAccountNum = 10001;
        public const int ProfileNum = 10001;
        public static async Task<ApInvoiceData> GetFakerPoReceiveDataAsync(IDataBaseFactory dbFactory)
        {
 
          return await ApInvoiceDataTests.GetFakerApInvoiceDataAsync();
        }

        public static async Task<ApInvoiceData> SaveFakerPoReceive(IDataBaseFactory dbFactory, ApInvoiceData data = null)
        {
            if (data == null)
                data = await GetFakerPoReceiveDataAsync(dbFactory);

            //data = await PoTransactionDataTests.SaveFakerPoTransaction(dbFactory, data);

            var srv = new ApInvoiceService(dbFactory);
            srv.Add();
            var mapper = srv.DtoMapper;
            var dto = mapper.WriteDto(data, null);


            srv.Add();

            //var mapper = srv.DtoMapper;

            //var dto = mapper.WriteDto(data, null);
            //var success = srv.Add(dto);

            var poTransactionPayload_Add = new ApInvoicePayload()
            {
                MasterAccountNum = MasterAccountNum,
                ProfileNum = ProfileNum,
                 ApInvoice = dto,
            };

            var success = await srv.AddAsync(poTransactionPayload_Add);

            Assert.True(success, srv.Messages.ObjectToString());

            return srv.Data;
        }
    }
}