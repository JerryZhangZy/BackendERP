using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace DigitBridge.Base.Utility
{
    [Serializable()]
    public class ErpSettingClass
    {
        /// <summary>
        /// Digit_seller_id
        /// </summary>
        public string Digit_seller_id { get; set; } = string.Empty;
        /// <summary>
        /// Digit_supplier_id
        /// </summary>
        public string Digit_supplier_id { get; set; } = string.Empty;

        /// <summary>
        /// Company Name
        /// </summary>
        public string CompanyName { get; set; } = string.Empty;
        public string CompanyName2 { get; set; } = string.Empty;
        public string Attention { get; set; } = string.Empty;
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string AddressLine3 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string StateFullName { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string PostalCodeExt { get; set; } = string.Empty;
        public string County { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string DaytimePhone { get; set; } = string.Empty;
        public string NightPhone { get; set; } = string.Empty;
        public string Fax { get; set; } = string.Empty;


        public string DefaultWholeSalesBank { get; set; } = string.Empty;
        public string DefaultEcommerceBank { get; set; } = string.Empty;

        public string DefaultWholeSalesWarehouse { get; set; } = string.Empty;
        public string DefaultEcommerceWarehouse { get; set; } = string.Empty;

        public string DefaultWholeSalesTaxRate { get; set; } = string.Empty;
        public string DefaultEcommerceTaxRate { get; set; } = string.Empty;

        public string DefaultWholeSalesFob { get; set; } = string.Empty;
        public string DefaultEcommerceFob { get; set; } = string.Empty;

        /// <summary>
        /// Charge Tax For Shipping
        /// </summary>
        public bool ChargeTaxForShipping { get; set; } = false;
        /// <summary>
        /// Charge Tax For Handling
        /// </summary>
        public bool ChargeTaxForHandling { get; set; } = false;


    }
}