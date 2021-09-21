using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Digitbridge.QuickbooksOnline.IntegrationApiMdl.Model
{

    public class UserCredentilApiReqType
    {
        [JsonProperty("clientId")]
        [Required(ErrorMessage = "ClientId is required but not provided. "),
            StringLength(500, MinimumLength = 20, ErrorMessage = "Invalid length of ClientId. ")]
        public string ClientId { get; set; }

        [JsonProperty("clientSecret")]
        [Required(ErrorMessage = "ClientSecret is required but not provided. "),
            StringLength(500, MinimumLength = 10, ErrorMessage = "Invalid length of ClientSecret. ")]
        public string ClientSecret { get; set; }
    }

    public class UserCredentilApiResponseType
    {
        public string RedirectUrl { get; set; }
    }

    public class TokenReceiverApiResponseType
    {
        public string Result { get; set; }
    }

    public class TokenStatusApiResponseType
    {
        public int QboOAuthTokenStatus { get; set; }
    }

}
