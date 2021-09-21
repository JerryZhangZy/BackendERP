using System;
using System.Collections.Generic;
using System.Text;

namespace Digitbridge.QuickbooksOnline.IntegrationApiFunc
{
    public class MyAppConst
    {
    }

    public class MyHttpHeaderName
    {
        public const string MasterAccountNum = "MasterAccountNum";
        public const string ProfileNum = "ProfileNum";
    }

    public class HttpResponseCodeID
    {
        public const string OK = "OK";
        public const string BAD_REQUEST = "BAD_REQUEST";
        public const string FOBIDDEN = "FOBIDDEN";
        public const string CONFLICT = "CONFLICT";
        public const string NOT_MODIFIED = "NOT_MODIFIED";
        public const string NOT_FOUND = "NOT_FOUND";
        public const string INTERNAL_SERVER_ERROR = "INTERNAL_SERVER_ERROR";
    }
}
