using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.QuickBooks.Integration.Infrastructure
{
    public class QboConnectionConfig
    {
        public string RedirectUrl { get; set; }
        public string Environment { get; set; }
        public string BaseUrl { get; set; }
        public string MinorVersion { get; set; }
        public string AppClientId { get; set; }
        public string AppClientSecret { get; set; }



        public QboConnectionConfig(string redirectUrl, string environment,
            string baseURl, string minorVersion, string appClientId, string appClientKey)
        {
            RedirectUrl = redirectUrl;
            Environment = environment;
            BaseUrl = baseURl;
            MinorVersion = minorVersion;
            AppClientId = appClientId;
            AppClientSecret = appClientKey;
        }
    }
}
