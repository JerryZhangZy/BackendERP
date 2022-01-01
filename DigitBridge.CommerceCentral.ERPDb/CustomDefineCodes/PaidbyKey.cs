namespace DigitBridge.CommerceCentral.ERPDb
{
    public class PaidbyKey
    {
        public int MasterAccountNum { get; set; }
        public int ProfileNum { get; set; }
        public int ChannelNum { get; set; }
        public int ChannelAccountNum { get; set; }
        public string ChannelPaidby { get; set; }
    }
}
