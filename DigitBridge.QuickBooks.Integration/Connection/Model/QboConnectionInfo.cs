using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.QuickBooks.Integration.Connection.Model
{
    public class QboConnectionInfo
    {
        public long ConnectionProfileNum { get; set; }
        public int MasterAccountNum { get; set; }
        public int ProfileNum { get; set; }
        /// <summary>
        /// Encrypted by TripleDES
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// Encrypted by TripleDES
        /// </summary>
        public string ClientSecret { get; set; }
        /// <summary>
        /// Encrypted by TripleDES
        /// </summary>
        public string RealmId { get; set; }
        /// <summary>
        /// Encrypted by TripleDES
        /// </summary>
        public string AuthCode { get; set; }
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public string RequestState { get; set; }
        /// <summary>
        /// UnInitiated = 0, Success = 1, Unlinked = 100, Error = 255
        /// </summary>
        public int QboOAuthTokenStatus { get; set; }
        public DateTime LastRefreshTokUpdate { get; set; }
        public DateTime LastAccessTokUpdate { get; set; }
        public DateTime EnterDate { get; set; }
        public DateTime LastUpdate { get; set; }

    }
}
