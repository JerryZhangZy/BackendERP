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
        public string Digit_seller_id { get; set; }
        /// <summary>
        /// Digit_supplier_id
        /// </summary>
        public string Digit_supplier_id { get; set; }

        /// <summary>
        /// Company Name
        /// </summary>
        public string CompanyName { get; set; }
        public string CompanyName2 { get; set; }
        public string Attention { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string StateFullName { get; set; }
        public string PostalCode { get; set; }
        public string PostalCodeExt { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string DaytimePhone { get; set; }
        public string NightPhone { get; set; }
        public string Fax { get; set; }


        public string DefaultWholeSalesBank { get; set; }
        public string DefaultEcommerceBank { get; set; }

        public string DefaultWholeSalesWarehouse { get; set; }
        public string DefaultEcommerceWarehouse { get; set; }

        public string DefaultWholeSalesTaxRate { get; set; }
        public string DefaultEcommerceTaxRate { get; set; }

        public string DefaultWholeSalesFob { get; set; }
        public string DefaultEcommerceFob { get; set; }

        /// <summary>
        /// Charge Tax For Shipping
        /// </summary>
        public bool ChargeTaxForShipping { get; set; }
        /// <summary>
        /// Charge Tax For Handling
        /// </summary>
        public bool ChargeTaxForHandling { get; set; }


    }
}