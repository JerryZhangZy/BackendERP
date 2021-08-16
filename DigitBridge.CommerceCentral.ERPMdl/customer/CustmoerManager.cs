

              
    

//-------------------------------------------------------------------------
// This document is generated by T4
// It will overwrite your changes, please keep it as it is
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    /// <summary>
    /// Represents a SalesOrderService.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class CustmoerManager : IMessage
    {
        protected CustomerService customerService;
        protected CustomerDataDtoCsv customerDataDtoCsv;

        public CustmoerManager() : base() {}
        public CustmoerManager(IDataBaseFactory dbFactory)
        {
            SetDataBaseFactory(dbFactory);
            customerService = new CustomerService(dbFactory);
            customerDataDtoCsv = new CustomerDataDtoCsv();
        }

        public async Task<byte[]> ExportAsync(CustomerPayload payload)
        {
            var rowNumList = new List<long>();
            using (var tx = new ScopedTransaction(dbFactory))
            {
                rowNumList = await CustomerServiceHelper.GetRowNumsAsync(payload.MasterAccountNum, payload.ProfileNum);
            }
            var dtoList = new List<CustomerDataDto>();
            rowNumList.ForEach(x =>
            {
                if (customerService.GetData(x))
                    dtoList.Add(customerService.ToDto());
            });
            if (dtoList.Count == 0)
                dtoList.Add(new CustomerDataDto());
            return customerDataDtoCsv.Export(dtoList);
        }

        public byte[] Export(CustomerPayload payload)
        {
            var rowNumList = new List<long>();
            using (var tx = new ScopedTransaction(dbFactory))
            {
                rowNumList = CustomerServiceHelper.GetRowNums(payload.MasterAccountNum, payload.ProfileNum);
            }
            var dtoList = new List<CustomerDataDto>();
            rowNumList.ForEach(x =>
            {
                if (customerService.GetData(x))
                    dtoList.Add(customerService.ToDto());
            });
            if (dtoList.Count == 0)
                dtoList.Add(new CustomerDataDto());
            return customerDataDtoCsv.Export(dtoList);
        }

        #region DataBase
        [XmlIgnore, JsonIgnore]
        protected IDataBaseFactory _dbFactory;

        [XmlIgnore, JsonIgnore]
        public IDataBaseFactory dbFactory
        {
            get
            {
                if (_dbFactory is null)
                    _dbFactory = DataBaseFactory.CreateDefault();
                return _dbFactory;
            }
        }

        public void SetDataBaseFactory(IDataBaseFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        #endregion DataBase

        #region Messages
        protected IList<MessageClass> _messages;
        [XmlIgnore, JsonIgnore]
        public virtual IList<MessageClass> Messages
        {
            get
            {
                if (_messages is null)
                    _messages = new List<MessageClass>();
                return _messages;
            }
            set { _messages = value; }
        }
        public IList<MessageClass> AddInfo(string message, string code = null) =>
             Messages.Add(message, MessageLevel.Info, code);
        public IList<MessageClass> AddWarning(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Warning, code);
        public IList<MessageClass> AddError(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Error, code);
        public IList<MessageClass> AddFatal(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Fatal, code);
        public IList<MessageClass> AddDebug(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Debug, code);

        #endregion Messages


    }
}



