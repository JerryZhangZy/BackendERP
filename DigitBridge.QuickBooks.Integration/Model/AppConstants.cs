﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.QuickBooks.Integration
{
    public class QboMappingConsts
    {
        public static readonly string DiscountRefValue = "86";
        public static readonly string DiscountRefName = "Discounts given";
        public static readonly string SummaryDiscountLineDescription = "Discount Line for Sales Order DigitBridge Order Id: ";
        public static readonly string SippingCostRefValue = "SHIPPING_ITEM_ID";
        public static readonly string SummaryShippingLineDescription = "Shipping Cost Line for Sales Order DigitBridge Order Id: ";
        public static readonly string SalesTaxItemRefValue = "55";
        public static readonly string SalesTaxItemDescription = "Calculated Sales Tax From DigitBridge Central for Sales Order DigitBridge Order Id: ";
        public static readonly string EndCustomerPoNumCustomFieldId = "1";
        public static readonly string ChnlOrderIdCustomFieldId = "2";
        public static readonly string SecChnlOrderIdCustomFieldId = "3";
        public static readonly string NoMatchingItemReturnString = "Skip";
        public static readonly string QboItemNullId = "0";
    }
}
