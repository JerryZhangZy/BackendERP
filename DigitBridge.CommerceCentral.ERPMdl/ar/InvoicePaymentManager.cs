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
    public class InvoicePaymentManager : IMessage, IInvoicePaymentManager
    {
        public InvoicePaymentManager() : base() { }
        public InvoicePaymentManager(IDataBaseFactory dbFactory)
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
        public async Task<byte[]> ExportAsync(InvoicePaymentPayload payload)
        {
            var listService = new InvoicePaymentList(dbFactory);
            var service = new InvoicePaymentService(dbFactory);
            var invoicePaymentDataDtoCsv = new InvoicePaymentDataDtoCsv();
            var rowNumList = await listService.GetRowNumListAsync(payload);
            var dtoList = new List<InvoiceTransactionDataDto>();
            foreach (var x in rowNumList)
            {
                if (await service.GetDataAsync(x))
                    dtoList.Add(service.ToDto());
            };
            if (dtoList.Count == 0)
                dtoList.Add(new InvoiceTransactionDataDto());
            return invoicePaymentDataDtoCsv.Export(dtoList);
        }

        public byte[] Export(InvoicePaymentPayload payload)
        {
            var listService = new InvoicePaymentList(dbFactory);
            var service = new InvoicePaymentService(dbFactory);
            var invoicePaymentDataDtoCsv = new InvoicePaymentDataDtoCsv();
            var rowNumList = listService.GetRowNumList(payload);
            var dtoList = new List<InvoiceTransactionDataDto>();
            foreach (var x in rowNumList)
            {
                if (service.GetData(x))
                    dtoList.Add(service.ToDto());
            };
            if (dtoList.Count == 0)
                dtoList.Add(new InvoiceTransactionDataDto());
            return invoicePaymentDataDtoCsv.Export(dtoList);
        }
        #endregion

        #region Import   
        public void Import(InvoicePaymentPayload payload, IFormFileCollection files)
        {
            var iTranService = new InvoicePaymentService(dbFactory);
            var invoicePaymentDataDtoCsv = new InvoicePaymentDataDtoCsv();

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
                var list = invoicePaymentDataDtoCsv.Import(file.OpenReadStream());
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

        public async Task ImportAsync(InvoicePaymentPayload payload, IFormFileCollection files)
        {
            if (files == null || files.Count == 0)
            {
                AddError("no files upload");
                return;
            }

            var iTranService = new InvoicePaymentService(dbFactory);
            var invoicePaymentDataDtoCsv = new InvoicePaymentDataDtoCsv();

            foreach (var file in files)
            {
                if (!file.FileName.ToLower().EndsWith("csv"))
                {
                    AddError($"invalid file type:{file.FileName}");
                    continue;
                }
                var list = invoicePaymentDataDtoCsv.Import(file.OpenReadStream());
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
        public async Task<bool> SaveImportDataAsync(IList<InvoiceTransactionDataDto> dtos)
        {
            if (dtos == null || dtos.Count == 0)
            {
                AddError("no files upload");
                return false;
            }
            var success = true;
            var invoicePaymentService = new InvoicePaymentService(dbFactory);
            foreach (var dto in dtos)
            {
                invoicePaymentService.DetachData(null);
                invoicePaymentService.NewData();
                var prepare = new InvoiceTransactionDtoPrepareDefault(invoicePaymentService);
                if (!(await prepare.PrepareDtoAsync(dto))) continue;

                if (!await invoicePaymentService.AddAsync(dto))
                {
                    success = false;
                    AddError($"Add invoicepayment failed, uniqueid {dto.InvoiceTransaction.UniqueId}");
                    this.Messages.Add(invoicePaymentService.Messages);
                }
            }
            return success;
        }
        #endregion

        //#region Add payment from prepayment.

        //public async Task<bool> AddPaymentFromPrepayment(string miscInvoiceUuid, string invoiceUuid, decimal amount)
        //{
        //    if (miscInvoiceUuid.IsZero())
        //        return true;

        //    if (invoiceUuid.IsZero())
        //    {
        //        AddError("invoiceUuid is required.");
        //        return false;
        //    }
        //    if (amount.IsZero())
        //    {
        //        AddError("amount is error.");
        //        return false;
        //    }

        //    var srv_MisPayment = new MiscInvoicePaymentService(dbFactory);

        //    var actualApplyAmount = await srv_MisPayment.GetCanApplyAmount(miscInvoiceUuid, amount);

        //    //Add payment to invoice trans and pay invoice.
        //    var srv_payment = new InvoicePaymentService(dbFactory);
        //    var success = await srv_payment.AddAsync(invoiceUuid, actualApplyAmount, miscInvoiceUuid);
        //    if (!success)
        //    {
        //        this.Messages = this.Messages.Concat(srv_payment.Messages).ToList();
        //        return false;
        //    }
        //    var paymentData = srv_payment.Data.InvoiceTransaction;

        //    //Add mis payment
        //    success = await srv_MisPayment.AddMiscPayment(miscInvoiceUuid, paymentData.TransUuid, paymentData.InvoiceNumber, actualApplyAmount);
        //    if (!success)
        //    {
        //        this.Messages = this.Messages.Concat(srv_MisPayment.Messages).ToList();
        //        return false;
        //    }

        //    return true;
        //}

        //#endregion
    }
}
