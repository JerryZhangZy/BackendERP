using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.QuickBooks.Integration
{
    public class QboIntegrationSetting
    {
        /// <summary>
        /// summary discount
        /// </summary>
        public string QboDiscountName { get; set; }
        public int QboDiscountId { get; set; }
        /// <summary>
        /// summary shipping cost name
        /// </summary>
        public string QboShippingName { get; set; }
        /// <summary>
        /// summary shipping cost id
        /// </summary>
        public int QboShippingId { get; set; }

        /// <summary>
        /// summary misc cost name
        /// </summary>
        public string QboMiscName { get; set; }
        /// <summary>
        /// summary misc cost id
        /// </summary>
        public int QboMiscId { get; set; }

        /// <summary>
        /// summary ChargeAndAllowance cost name
        /// </summary>
        public string QboChargeAndAllowanceName { get; set; }
        /// <summary>
        /// summary ChargeAndAllowance cost id
        /// </summary>
        public int QboChargeAndAllowanceId { get; set; }

        /// <summary>
        /// summary Tax cost name
        /// </summary>
        public string QboTaxName { get; set; }
        /// <summary>
        /// summary Tax cost id
        /// </summary>
        public int QboTaxId { get; set; }
    }
}
