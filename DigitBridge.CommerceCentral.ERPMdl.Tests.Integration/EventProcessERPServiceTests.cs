

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
using DigitBridge.CommerceCentral.ERPDb;
using Bogus;
using DigitBridge.Base.Common;
using Newtonsoft.Json.Linq;

namespace DigitBridge.CommerceCentral.ERPMdl.Tests.Integration
{
    public partial class EventProcessERPServiceTests
    {

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void AddDto_Test()
        {
            var srv = new EventProcessERPService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);


            srv.Add(dto);

            var id = dto.EventProcessERP.EventUuid;

            var srvGet = new EventProcessERPService(DataBaseFactory);
            //srvGet.Edit();
            srvGet.GetDataById(id);
            var result = srv.Data.Equals(srvGet.Data);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void UpdateDto_Test()
        {
            SaveData_Test();

            var id = DataBaseFactory.GetValue<EventProcessERP, string>(@"
SELECT TOP 1 ins.EventUuid 
FROM EventProcessERP ins 
");


            var srv = new EventProcessERPService(DataBaseFactory);
            srv.Edit(id);
            var rowNum = srv.Data.EventProcessERP.RowNum;

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            dto.EventProcessERP.RowNum = rowNum;
            dto.EventProcessERP.EventUuid = id;

            srv.Clear();
            srv.Update(dto);

            var srvGet = new EventProcessERPService(DataBaseFactory);
            srvGet.Edit();
            srvGet.GetDataById(id);
            var result = srv.Data.Equals(srvGet.Data);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task AddDtoAsync_Test()
        {
            var srv = new EventProcessERPService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);

            await srv.AddAsync(dto);

            var id = dto.EventProcessERP.EventUuid;

            var srvGet = new EventProcessERPService(DataBaseFactory);
            srvGet.Edit();
            await srvGet.GetDataByIdAsync(id);
            var result = srv.Data.Equals(srvGet.Data);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task UpdateDtoAsync_Test()
        {
            await SaveDataAsync_Test();

            var id = await DataBaseFactory.GetValueAsync<EventProcessERP, string>(@"
SELECT TOP 1 ins.EventUuid 
FROM EventProcessERP ins 
");


            var srv = new EventProcessERPService(DataBaseFactory);
            await srv.EditAsync(id);
            var rowNum = srv.Data.EventProcessERP.RowNum;

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            dto.EventProcessERP.RowNum = rowNum;
            dto.EventProcessERP.EventUuid = id;

            srv.Clear();
            await srv.UpdateAsync(dto);

            var srvGet = new EventProcessERPService(DataBaseFactory);
            //srvGet.Edit();
            await srvGet.GetDataByIdAsync(id);
            var result = srv.Data.Equals(srvGet.Data);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }


        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void AddPayload_Test()
        {
            var srv = new EventProcessERPService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);


            var payload = new EventProcessERPPayload();
            payload.EventProcessERP = dto;
            payload.MasterAccountNum = 1;
            payload.ProfileNum = 1;
            payload.DatabaseNum = 1;

            srv.Add(payload);
            var id = dto.EventProcessERP.EventUuid;
            var srvGet = new EventProcessERPService(DataBaseFactory);
            //srvGet.Edit();
            srvGet.GetDataById(id);
            var result = srv.Data.Equals(srvGet.Data);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public void UpdatePayload_Test()
        {
            SaveData_Test();

            var id = DataBaseFactory.GetValue<EventProcessERP, string>(@"
SELECT TOP 1 ins.EventUuid 
FROM EventProcessERP ins 
");


            var srv = new EventProcessERPService(DataBaseFactory);
            srv.Edit(id);
            var rowNum = srv.Data.EventProcessERP.RowNum;

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            dto.EventProcessERP.RowNum = rowNum;
            dto.EventProcessERP.EventUuid = id;

            var payload = new EventProcessERPPayload();
            payload.EventProcessERP = dto;
            payload.MasterAccountNum = srv.Data.EventProcessERP.MasterAccountNum;
            payload.ProfileNum = srv.Data.EventProcessERP.ProfileNum;
            payload.DatabaseNum = srv.Data.EventProcessERP.DatabaseNum;

            srv.Clear();
            srv.Update(payload);

            var srvGet = new EventProcessERPService(DataBaseFactory);
            srvGet.Edit();
            srvGet.GetDataById(id);
            var result = srv.Data.Equals(srvGet.Data);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task AddPayloadAsync_Test()
        {
            var srv = new EventProcessERPService(DataBaseFactory);
            srv.Add();

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);

            var payload = new EventProcessERPPayload();
            payload.EventProcessERP = dto;
            payload.MasterAccountNum = 1;
            payload.ProfileNum = 1;
            payload.DatabaseNum = 1;

            await srv.AddAsync(payload);

            var id = dto.EventProcessERP.EventUuid;

            var srvGet = new EventProcessERPService(DataBaseFactory);
            srvGet.Edit();
            await srvGet.GetDataByIdAsync(id);
            var result = srv.Data.Equals(srvGet.Data);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task UpdatePayloadAsync_Test()
        {
            await SaveDataAsync_Test();

            var id = await DataBaseFactory.GetValueAsync<EventProcessERP, string>(@"
SELECT TOP 1 ins.EventUuid 
FROM EventProcessERP ins 
");


            var srv = new EventProcessERPService(DataBaseFactory);
            await srv.EditAsync(id);
            var rowNum = srv.Data.EventProcessERP.RowNum;

            var mapper = srv.DtoMapper;
            var data = GetFakerData();
            var dto = mapper.WriteDto(data, null);
            dto.EventProcessERP.RowNum = rowNum;
            dto.EventProcessERP.EventUuid = id;

            var payload = new EventProcessERPPayload();
            payload.EventProcessERP = dto;
            payload.MasterAccountNum = srv.Data.EventProcessERP.MasterAccountNum;
            payload.ProfileNum = srv.Data.EventProcessERP.ProfileNum;
            payload.DatabaseNum = srv.Data.EventProcessERP.DatabaseNum;

            srv.Clear();
            await srv.UpdateAsync(payload);

            var srvGet = new EventProcessERPService(DataBaseFactory);
            //srvGet.Edit();
            await srvGet.GetDataByIdAsync(id);
            var result = srv.Data.Equals(srvGet.Data);

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }


        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task AddEventProcessERPAsync_Test()
        {

            var data = new EventProcessERP(DataBaseFactory)
            {
                ChannelNum = 10001,
                ChannelAccountNum = 101,
                ERPEventProcessType = (int)EventProcessTypeEnum.SalesOrderToWMS,
                ProcessSource = string.Empty,
                ProcessUuid = "602e17ff-c31a-0b94-ccc3-833dc0809943",
                ProcessData = string.Empty,
                ActionStatus = EventProcessActionStatusEnum.Pending.ToInt(),
                EventMessage = string.Empty
            };

            var srv = new EventProcessERPService(DataBaseFactory);
            var result = (await srv.AddEventProcessERPAsync(data));

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task AddEventAndQueueMessageAsync_Test()
        {
            // only test message to eventprocess table and to queue.
            var shipmentID = "602e17ff-c31a-0b94-ccc3-833dc0809943";
            var data = new EventProcessERP(DataBaseFactory)
            {
                ChannelNum = 10001,
                ChannelAccountNum = 101,
                ERPEventProcessType = (int)EventProcessTypeEnum.ShipmentFromWMS,
                ProcessSource = string.Empty,
                ProcessUuid = shipmentID,
                ProcessData = string.Empty,
                ActionStatus = EventProcessActionStatusEnum.Pending.ToInt(),
                EventMessage = string.Empty
            };

            var srv = new EventProcessERPService(DataBaseFactory);
            var result = (await srv.AddEventAndQueueMessageAsync(data));

            Assert.True(result, srv.Messages.ObjectToString());
        }
        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task ResendMessageToQueueAsync_Test()
        {
            // only test message to eventprocess table and to queue.
            var shipmentID = "113-10000000496"; 

            var srv = new EventProcessERPService(DataBaseFactory,MySingletonAppSetting.AzureWebJobsStorage);
            var result = (await srv.ResendMessageToQueueAsync(EventProcessTypeEnum.ShipmentFromWMS,shipmentID));

            Assert.True(result, srv.Messages.ObjectToString());
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task UpdateActionStatusAsync_Test()
        {

            var data = new AcknowledgePayload()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                DatabaseNum = 1,

                EventProcessType = EventProcessTypeEnum.SalesOrderToWMS,
                ProcessUuids = new List<string>()
                {
                    "602e17ff-c31a-0b94-ccc3-833dc0809943",
                    "cb23e397-6205-0629-3df3-19a253223309"
                },
            };

            var srv = new EventProcessERPService(DataBaseFactory);
            var result = (await srv.UpdateActionStatusAsync(data));

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task UpdateProcessStatusAsync_Test()
        {

            var data = new AcknowledgeProcessPayload()
            {
                MasterAccountNum = 10001,
                ProfileNum = 10001,
                DatabaseNum = 1,

                EventProcessType = EventProcessTypeEnum.SalesOrderToWMS,
                ProcessResults = new List<ProcessResult>()
                {
                    new ProcessResult()
                    {
                        ProcessUuid = "602e17ff-c31a-0b94-ccc3-833dc0809943",
                        ProcessStatus = 1,
                        ProcessData = new JObject()
                            {
                                { "ClassCode", "WMS class code" },
                                { "ProcessData", "WMS message" },
                                { "ProcessBy", "processor name" }
                            },
                        EventMessage =  new JObject()
                            {
                                { "ClassCode", "WMS class code" },
                                { "EventMessage", "WMS message" }
                            },
                    },
                    new ProcessResult()
                    {
                        ProcessUuid = "cb23e397-6205-0629-3df3-19a253223309",
                        ProcessStatus = 2,
                        ProcessData = null
                    }
                },
            };

            var srv = new EventProcessERPService(DataBaseFactory);
            var result = (await srv.UpdateProcessStatusAsync(data));

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }

        [Fact()]
        //[Fact(Skip = SkipReason)]
        public async Task UpdateCloseStatusAsync_Test()
        {
            var eventProcessType = (int)EventProcessTypeEnum.SalesOrderToWMS;
            var ProcessUuid = "602e17ff-c31a-0b94-ccc3-833dc0809943";
            var closeStatus = (int)EventCloseStatusEnum.Closed;

            var srv = new EventProcessERPService(DataBaseFactory);
            var result = (await srv.UpdateCloseStatusAsync(eventProcessType, ProcessUuid, closeStatus));

            Assert.True(result, "This is a generated tester, please report any tester bug to team leader.");
        }


        [Fact]
        public async Task GetUnprocessedEvent_Test()
        {
            var srv = new EventProcessERPService(DataBaseFactory);
            var result = await srv.GetUnprocessedEvent(10001, 10001, 1, 1, EventProcessTypeEnum.InvoiceToCommerceCentral);
            Assert.NotEmpty(result.ToString());
        }
    }
}



