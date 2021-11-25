using System;
using System.Collections.Generic;
using System.Text;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    public static class SqlStringHelper
    {
        /// <summary>
        /// Get Setting_Channel table join statement
        /// </summary>
        /// <param name="channelNum">channelNum field name</param>
        /// <param name="allies">Optional Joined table allies</param>
        /// <returns></returns>
        public static string Join_Setting_Channel(string masterAccountNum, string profileNum, string channelNum, string allies = null) 
        {
            if (string.IsNullOrEmpty(allies))
                allies = "chanel";
            return @$"
LEFT JOIN Setting_Channel {allies} ON(
{masterAccountNum} = chanel.MasterAccountNum AND 
{profileNum} = chanel.ProfileNum AND 
{channelNum} = chanel.ChannelNum)
";
        }

        /// <summary>
        /// Get Setting_Channel table join statement
        /// </summary>
        /// <param name="channelNum">channelNum field name</param>
        /// <param name="allies">Optional Joined table allies</param>
        /// <returns></returns>
        public static string Join_Setting_ChannelAccount(string masterAccountNum, string profileNum, string channelNum, string channelAccountNum, string allies = null)
        {
            if (string.IsNullOrEmpty(allies))
                allies = "channelAccount";
            return @$"
LEFT JOIN Setting_ChannelAccount channelAccount ON(
{masterAccountNum} = channelAccount.MasterAccountNum AND 
{profileNum} = channelAccount.ProfileNum AND 
{channelNum} = channelAccount.ChannelNum AND 
{channelAccountNum} = channelAccount.ChannelAccountNum)
";
        }
    }
}
