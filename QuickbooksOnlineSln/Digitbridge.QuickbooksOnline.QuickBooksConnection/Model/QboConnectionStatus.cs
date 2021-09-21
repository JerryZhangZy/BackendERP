using System;
using System.Collections.Generic;
using System.Text;

namespace Digitbridge.QuickbooksOnline.QuickBooksConnection.Model
{
    public class QboConnectionTokenStatus
    {
        public ConnectionTokenStatus AccessTokenStatus { get; set; }
        public ConnectionTokenStatus RefreshTokenStatus { get; set; }

        public QboConnectionTokenStatus()
        {
            this.AccessTokenStatus = ConnectionTokenStatus.Initalized;
            this.RefreshTokenStatus = ConnectionTokenStatus.Initalized;
        }
    }
    public enum ConnectionTokenStatus
    {
        Initalized = 0,
        Unchanged = 1,
        Updated = 2,
        UpdatedWithError = 3
    }
}
