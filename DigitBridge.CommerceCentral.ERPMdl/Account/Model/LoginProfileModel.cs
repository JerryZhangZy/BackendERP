using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public class LoginProfileModel
    {
        /// <summary>
        /// MasterAccountNum
        /// </summary>
        public int MasterAccountNum { get; set; }

        /// <summary>
        /// ProfileNum
        /// </summary>
        public int ProfileNum { get; set; }

        /// <summary>
        /// company name
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// email
        /// </summary>
        public string Email { get; set; }
    }
}
