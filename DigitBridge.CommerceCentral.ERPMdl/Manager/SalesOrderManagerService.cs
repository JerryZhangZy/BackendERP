﻿using DigitBridge.CommerceCentral.ERPMdl.Manager;
using DigitBridge.CommerceCentral.YoPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPMdl.Management
{
    public class SalesOrderManagerService : ServiceBase<SalesOrderManagerService, MessageData, CreateSalesOrderDto>
    {
        public SalesOrderManagerService() : base() { }

        private ChannelOrderService _channelOrderSrv;
        private SalesOrderService _salesOrderSrv;

        public SalesOrderManagerService(IDataBaseFactory dataBaseFactory)
        {
            _salesOrderSrv = new SalesOrderService(dataBaseFactory);

            _channelOrderSrv = new ChannelOrderService(dataBaseFactory);
        }

        public bool CreateSalesOrders()
        {
            MessageData msgData = Data;
            foreach(var sMsgData in msgData.Messages)
            {
                CreateSalesOrder(sMsgData);
            }

            return true;
        }

        private void CreateSalesOrder(SingleMessageData sMsgData)
        {
            GetChannelOrderDto(sMsgData.ProcessUuid);
        }

        public async string GetChannelOrderDtoAsync(string orderDCAssignmentUuid)
        {
            bool ret = await _channelOrderSrv.GetDataByIdAsync(orderDCAssignmentUuid);

            _channelOrderSrv.
            _salesOrderSrv.AddAsync()

        }

    }
}