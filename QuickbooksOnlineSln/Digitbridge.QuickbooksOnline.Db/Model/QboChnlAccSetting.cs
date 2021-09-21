using System;
using System.Collections.Generic;
using System.Text;

namespace Digitbridge.QuickbooksOnline.Db.Model
{
	public class QboChnlAccSetting
	{
		public long ChannelAccSettingNum { get; set; }
		public int MasterAccountNum { get; set; }
		public int ProfileNum { get; set; }
		/// <summary>
		/// Central Channel Account name, Max 10 chars ( because of qbo doc Number 21 chars restrictions )
		/// </summary>
		public string ChannelAccountName { get; set; }
		/// <summary>
		/// Central Channel Account Number
		/// </summary>
		public int ChannelAccountNum { get; set; }
		/// <summary>
		/// Central Channel name
		/// </summary>
		public string ChannelName { get; set; }
		/// <summary>
		/// Central Channel Number
		/// </summary>
		public int ChannelNum { get; set; }
		/// <summary>
		/// Use if select Create Customer Records per Marketplace
		/// </summary>
		public string ChannelQboCustomerName { get; set; }
		/// <summary>
		/// Use if select Create Customer Records per Marketplace
		/// </summary>
		public int ChannelQboCustomerId { get; set; }
		/// <summary>
		/// Account for fee in this Marketplace
		/// </summary>
		public string ChannelQboFeeAcountName { get; set; }
		/// <summary>
		/// Account for fee in this Marketplace
		/// </summary>
		public int ChannelQboFeeAcountId { get; set; }
		/// <summary>
		/// Account for collecting money in this Mrketplace
		/// </summary>
		public string ChannelQboBankAcountName { get; set; }
		/// <summary>
		/// Account for collecting money in this Mrketplace
		/// </summary>
		public int ChannelQboBankAcountId { get; set; }
		public DateTime EnterDate { get; set; }
		public DateTime LastUpdate { get; set; }
		/// <summary>
		/// Last DateTime that the system exported the orders in this ChannelAccount
		/// </summary>
		public DateTime DailySummaryLastExport { get; set; }

	}
}
