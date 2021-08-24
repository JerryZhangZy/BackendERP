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
using Microsoft.AspNetCore.Http;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class InvoiceReturnManager : IMessage, IServiceManager<InvoiceReturnPayload>
    {
        public InvoiceReturnManager() : base() { }
        public InvoiceReturnManager(IDataBaseFactory dbFactory)
        {
            SetDataBaseFactory(dbFactory);
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

        #region Export 
        public async Task<byte[]> ExportAsync(InvoiceReturnPayload payload)
        {
            var listService = new InvoiceReturnList(dbFactory, new InvoiceReturnQuery());
            var iTranService = new InvoiceReturnService(dbFactory);
            var invoiceTransactionDataDtoCsv = new InvoiceTransactionDataDtoCsv();

            var rowNumList = await listService.GetRowNumListAsync(payload);
            var dtoList = new List<InvoiceTransactionDataDto>();
            foreach (var x in rowNumList)
            {
                if (iTranService.GetData(x))
                    dtoList.Add(iTranService.ToDto());
            };
            if (dtoList.Count == 0)
                dtoList.Add(new InvoiceTransactionDataDto());
            return invoiceTransactionDataDtoCsv.Export(dtoList);
        }

        public byte[] Export(InvoiceReturnPayload payload)
        {
            var listService = new InvoiceReturnList(dbFactory, new InvoiceReturnQuery());
            var iTranService = new InvoiceReturnService(dbFactory);
            var invoiceTransactionDataDtoCsv = new InvoiceTransactionDataDtoCsv();

            var rowNumList = listService.GetRowNumList(payload);
            var dtoList = new List<InvoiceTransactionDataDto>();
            foreach (var x in rowNumList)
            {
                if (iTranService.GetData(x))
                    dtoList.Add(iTranService.ToDto());
            };
            if (dtoList.Count == 0)
                dtoList.Add(new InvoiceTransactionDataDto());
            return invoiceTransactionDataDtoCsv.Export(dtoList);
        }
        #endregion

        #region Import   
        public void Import(InvoiceReturnPayload payload, IFormFileCollection files)
        {
            var iTranService = new InvoiceReturnService(dbFactory);
            var invoiceTransactionDataDtoCsv = new InvoiceTransactionDataDtoCsv();

            if (files == null || files.Count == 0)
            {
                AddError("no files upload");
                return;
            }
            foreach (var file in files)
            {
                if (!file.FileName.ToLower().EndsWith("csv"))
                {
                    AddError($"invalid file type:{file.FileName}");
                    continue;
                }
                var list = invoiceTransactionDataDtoCsv.Import(file.OpenReadStream());
                var readcount = list.Count();
                var addsucccount = 0;
                var errorcount = 0;
                foreach (var item in list)
                {
                    payload.InvoiceTransaction = item;
                    if (iTranService.Add(payload))
                        addsucccount++;
                    else
                    {
                        errorcount++;
                        foreach (var msg in iTranService.Messages)
                            Messages.Add(msg);
                        iTranService.Messages.Clear();
                    }
                }
                if (payload.HasInvoiceTransaction)
                    payload.InvoiceTransaction = null;
                AddInfo($"FIle:{file.FileName},Read {readcount},Import Succ {addsucccount},Import Fail {errorcount}.");
            }
        }

        public async Task ImportAsync(InvoiceReturnPayload payload, IFormFileCollection files)
        {
            if (files == null || files.Count == 0)
            {
                AddError("no files upload");
                return;
            }

            var iTranService = new InvoiceReturnService(dbFactory);
            var invoiceTransactionDataDtoCsv = new InvoiceTransactionDataDtoCsv();

            foreach (var file in files)
            {
                if (!file.FileName.ToLower().EndsWith("csv"))
                {
                    AddError($"invalid file type:{file.FileName}");
                    continue;
                }
                var list = invoiceTransactionDataDtoCsv.Import(file.OpenReadStream());
                var readcount = list.Count();
                var addsucccount = 0;
                var errorcount = 0;
                foreach (var item in list)
                {
                    payload.InvoiceTransaction = item;
                    if (await iTranService.AddAsync(payload))
                        addsucccount++;
                    else
                    {
                        errorcount++;
                        foreach (var msg in iTranService.Messages)
                            Messages.Add(msg);
                        iTranService.Messages.Clear();
                    }
                }
                if (payload.HasInvoiceTransaction)
                    payload.InvoiceTransaction = null;
                AddInfo($"File:{file.FileName},Read {readcount},Import Succ {addsucccount},Import Fail {errorcount}.");
            }
        }
        #endregion
    }
}
