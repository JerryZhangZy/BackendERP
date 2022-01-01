﻿using DigitBridge.CommerceCentral.YoPoco;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class CompanySummaryInquiry
    {
        protected CustomerSummaryInquiry CustomerSummaryService;
        protected ProductSummaryInquiry ProductSummaryService;
        protected SalesOrderSummaryInquiry SalesOrderSummaryService;
        protected ShipmentSummaryInquiry ShipmentSummaryService;
        protected InvoiceSummaryInquiry InvoiceSummaryService;
        protected InvoicePaymentSummaryInquiry InvoicePaymentSummaryService;
        protected InvoiceReturnSummaryInquiry InvoiceReturnSummaryService;
        protected InventoryLogInquiry InventoryLogSummaryService;

        protected IDataBaseFactory dbFactory;

        public CompanySummaryInquiry(IDataBaseFactory dataBaseFactory)
        {
            dbFactory = dataBaseFactory;
            CustomerSummaryService = new CustomerSummaryInquiry(dataBaseFactory, new CustomerSummaryQuery());
            ProductSummaryService = new ProductSummaryInquiry(dataBaseFactory, new ProductSummaryQuery());
            SalesOrderSummaryService = new SalesOrderSummaryInquiry(dataBaseFactory, new SalesOrderSummaryQuery());
            ShipmentSummaryService = new ShipmentSummaryInquiry(dataBaseFactory, new ShipmentSummaryQuery());
            InvoiceSummaryService = new InvoiceSummaryInquiry(dataBaseFactory, new InvoiceSummaryQuery());
            InvoicePaymentSummaryService = new InvoicePaymentSummaryInquiry(dataBaseFactory, new InvoicePaymentSummaryQuery());
            InvoiceReturnSummaryService = new InvoiceReturnSummaryInquiry(dataBaseFactory, new InvoiceReturnSummaryQuery());
            InventoryLogSummaryService = new InventoryLogInquiry(dataBaseFactory, new InventoryLogSummaryQuery());
        }

        public async Task GetCompaySummaryAsync(CompanySummaryPayload payload)
        {
            await CustomerSummaryService.GetCompanySummaryAsync(payload);
            await ProductSummaryService.GetCompanySummaryAsync(payload);
            await InvoiceSummaryService.GetCompanySummaryAsync(payload);
            await InvoicePaymentSummaryService.GetCompanySummaryAsync(payload);
            await InvoiceReturnSummaryService.GetCompanySummaryAsync(payload);
            await SalesOrderSummaryService.GetCompanySummaryAsync(payload);
            await ShipmentSummaryService.GetCompanySummaryAsync(payload);
            await InventoryLogSummaryService.GetCompanySummaryAsync(payload);
        }
    }
}
