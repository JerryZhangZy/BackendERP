using System;
using System.Collections.Generic;
using System.Text;

namespace Digitbridge.QuickbooksOnline.CentralToDbMdl
{
    public class OrderExtensionFlagsPostType
    {
        public List<int> ExtensionFlags { get; set; }
        public List<string> DigitBridgeOrderid { get; set; }
        public OrderExtensionFlagsPostType() { }
        public OrderExtensionFlagsPostType(int value)
        {
            ExtensionFlags = new List<int> { value };
            DigitBridgeOrderid = new List<string>();
        }
    }

    public class OrderExtensionFlagsResponseType
    {
        public bool IsSuccessful { get; set; }
        public string ResultMessage { get; set; }
    }

}
