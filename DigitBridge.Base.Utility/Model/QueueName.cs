﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.Base.Utility
{
    public class QueueName
    {
        public const string Erp_Qbo_Invoice_Queue = "erp-quickbooks-invoice-queue";
        public const string Erp_Qbo_Invoice_Void_Queue = "erp-quickbooks-invoice-void-queue";


        public const string Erp_Qbo_Payment_Queue = "erp-quickbooks-payment-queue";
        public const string Erp_Qbo_Payment_Delete_Queue = "erp-quickbooks-payment-delete-queue";


        public const string Erp_Qbo_Return_Queue = "erp-quickbooks-return-queue";
        public const string Erp_Qbo_Return_Delete_Queue = "erp-quickbooks-return-delete-queue";

        /// <summary>
        /// TODO put Shipment queue name here
        /// </summary>
        public const string Erp_Shipment_Queue = "";
    }
}
