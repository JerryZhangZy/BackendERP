/****** Object:  Table [dbo].[Setting_Channel]    Script Date: 11/12/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Setting_Channel](
	[MasterAccountNum] [int] NOT NULL,
	[ProfileNum] [int] NOT NULL,
	[ChannelNum] [int] NOT NULL,
	[ChannelName] [nvarchar](200) NOT NULL,
	[OrderSplitFlag] [tinyint] NULL,
	[OrderSplitPriorityLevel] [tinyint] NULL,
	[Category] [varchar](50) NULL,
	[PlatformNum] [int] NULL,
	[PlatformName] [nvarchar](50) NULL,
	[ChannelCurrency] [nvarchar](50) NULL,
 CONSTRAINT [PK_Setting_Channel] PRIMARY KEY CLUSTERED 
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[ChannelNum] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Setting_ChannelAccount]    Script Date: 11/12/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Setting_ChannelAccount](
	[MasterAccountNum] [int] NOT NULL,
	[ProfileNum] [int] NOT NULL,
	[ChannelNum] [int] NOT NULL,
	[ChannelAccountNum] [int] NOT NULL,
	[ChannelAccountName] [nvarchar](200) NOT NULL,
	[MissingItemCreation] [tinyint] NULL,
	[CompanyName] [nvarchar](200) NULL,
 CONSTRAINT [PK_Setting_ChannelAccount] PRIMARY KEY CLUSTERED 
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[ChannelNum] ASC,
	[ChannelAccountNum] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10001, 10001, 10001, N'Amazon', 0, 0, N'Marketplace', 0, N' ', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10001, 10001, 10002, N'Walmart', 0, 0, N'Marketplace', 0, N'  ', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10001, 10001, 10004, N'eBay', 0, 0, N'Marketplace', 0, N' ', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10001, 10001, 20001, N'Shopify', 0, 0, N'Webstore', 0, N' ', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10001, 10001, 20003, N'Magento', 0, 0, N'Webstore', 0, N' ', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10001, 10001, 30001, N'Nordstrom by Dsco', 0, 0, N'Retailer', 910, N'DSCO', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10001, 10001, 30006, N'Bloomingdales', 0, 0, N'Retailer', 900, N'CommerceHub', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10001, 10001, 30007, N'JC Penney', 0, 0, N'Retailer', 900, N'CommerceHub', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10001, 10001, 30008, N'Belk', 0, 0, N'Retailer', 900, N'CommerceHub', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10001, 10001, 30009, N'LoardAndTaylor', 0, 0, N'Retailer', 910, N'DSCO', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10001, 10011, 30002, N'Nordstrom Rack by Dsco', 0, 0, N'Retailer', 910, N'DSCO', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10001, 10012, 10001, N'Amazon', 0, 0, N'Marketplace', 0, N' ', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10001, 10012, 10002, N'Walmart', 0, 0, N'Marketplace', 0, N'  ', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10001, 10012, 10004, N'eBay', 0, 0, N'Marketplace', 0, N' ', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10001, 10012, 20001, N'Shopify', 0, 0, N'Webstore', 0, N' ', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10001, 10012, 30001, N'Nordstrom by Dsco', 0, 0, N'Retailer', 910, N'DSCO', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10001, 10012, 30006, N'Bloomingdales', 0, 0, N'Retailer', 900, N'CommerceHub', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10001, 10012, 30007, N'JC Penney', 0, 0, N'Retailer', 900, N'CommerceHub', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10001, 10012, 30008, N'Belk', 0, 0, N'Retailer', 900, N'CommerceHub', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10001, 10012, 30009, N'LoardAndTaylor', 0, 0, N'Retailer', 910, N'DSCO', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10002, 10003, 10002, N'Walmart', 0, 0, N'Marketplace', 0, N' ', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10002, 10003, 10004, N'eBay', 0, 0, N'Marketplace', 0, N' ', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10002, 10003, 20001, N'Shopify', 0, 0, N'Webstore', 0, N'0', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10002, 10003, 20003, N'Magento', 0, 0, N'Webstore', 0, N' ', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10002, 10003, 30001, N'Nordstrome by DSCO', 0, 0, N'Retailer', 910, N'DSCO', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10002, 10003, 30002, N'Nordstrom Rack by DSCO', 0, 0, N'Retailer', 910, N'DSCO', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10002, 10003, 30006, N'Bloomingdales', 0, 0, N'Retailer', 900, N'CommerceHub', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10002, 10003, 30007, N'JC Penney', 0, 0, N'Retailer', 900, N'CommerceHub', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10002, 10003, 30008, N'Belk', 0, 0, N'Retailer', 900, N'CommerceHub', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10002, 10003, 30009, N'LoardAndTaylor by DSCO', 0, 0, N'Retailer', 910, N'DSCO', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10002, 10004, 10002, N'Walmart ', 0, 0, N'Marketplace', 0, N' ', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10002, 10004, 10004, N'ebay', 0, 0, N'Marketplace', 0, N' ', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10002, 10004, 20001, N'Shopify', 0, 0, N'Webstore', 0, N'0', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10002, 10004, 20003, N'Magento', 0, 0, N'Webstore', 0, N'0', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10002, 10004, 30001, N'Nordstrome by DSCO', 0, 0, N'Retailer', 910, N'DSCO', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10002, 10004, 30002, N'Nordstrome Rack by DSCO', 0, 0, N'Retailer', 910, N'DSCO', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10002, 10004, 30009, N'LordAndTaylor', 0, 0, N'Retailer', 910, N'DSCO', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10002, 10005, 10001, N'Amazon', 0, 0, N'Marketplace', 0, N' ', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10002, 10005, 10002, N'Walmart', 0, 0, N'Marketplace', 0, N'  ', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10002, 10005, 10004, N'eBay', 0, 0, N'Marketplace', 0, N' ', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10002, 10005, 20001, N'Shopify-Test', 0, 0, N'Webstore', 0, N'0', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10002, 10005, 20003, N'Magento', 0, 0, N'Webstore', 0, N' ', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10002, 10005, 30001, N'Test', 0, 0, N'Retailer', 910, N'DSCO', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10002, 10005, 30006, N'Bloomingdales', 0, 0, N'Retailer', 900, N'CommerceHub', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10002, 10005, 30007, N'JC Penney', 0, 0, N'Retailer', 900, N'CommerceHub', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10002, 10005, 30008, N'Belk', 0, 0, N'Retailer', 900, N'CommerceHub', N'USD')
GO
INSERT [dbo].[Setting_Channel] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelName], [OrderSplitFlag], [OrderSplitPriorityLevel], [Category], [PlatformNum], [PlatformName], [ChannelCurrency]) VALUES (10002, 10005, 30009, N'LoardAndTaylor', 0, 0, N'Retailer', 910, N'DSCO', N'USD')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 900, 10107, N'nick1231231', 0, N'nick1231231')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 900, 10108, N'nickname3030303', 0, N'nickname3030303')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 900, 10109, N'foobar2010isback', 0, N'foobar2010isback')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 900, 10111, N'foobar2005isback', 0, N'foobar2005isback')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 900, 10116, N'foobar2010ggeazy', 0, N'foobar2010ggeazy')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 900, 10117, N'foobar2010werwer', 0, N'foobar2010werwer')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 900, 10118, N'foobar2010useVerify', 0, N'foobar2010useVerify')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 900, 10119, N'foobar20101001', 0, N'foobar20101001')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 900, 10120, N'foobar2010110101', 0, N'foobar2010110101')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 900, 10121, N'foobar2010h3tyh3t', 0, N'foobar2010h3tyh3t')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 900, 10122, N'foobar201089osfgbhniyhuo', 0, N'foobar201089osfgbhniyhuo')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 900, 10123, N'foobar2010wertert3', 0, N'foobar2010wertert3')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 900, 10135, N'nickname', 0, N'nickname')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10001, 0, N'string', 0, N'string')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10001, 10001, N'Vibes-AM-PJ', NULL, NULL)
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10001, 10002, N'Vibes-AM-SP', NULL, NULL)
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10001, 10003, N'Vibes-AM-VB', NULL, NULL)
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10001, 10103, N'string-a', 0, N'string-a')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10001, 10127, N'nick name amazon', 0, N'nick name amazon')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10001, 10128, N'New Account', 0, N'New Account')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10001, 10132, N'string-aaa', 0, N'string-aaa')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10001, 10134, N'string-aaaa', 0, N'string-aaaa')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10001, 10191, N'Testpost5', 0, N'Testpost5')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10001, 10231, N'CarlosAmazonSep10', 0, N'CarlosAmazonSep10')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10001, 10232, N'CrlsAmznSep10-2', 0, N'CrlsAmznSep10-2')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10001, 10233, N'CarlosAmazonSep10-3', 0, N'CarlosAmazonSep10-3')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10001, 10234, N'CarlosAmazonSep10-4', 0, N'CarlosAmazonSep10-4')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10002, 10004, N'Vibes-WM', NULL, NULL)
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10002, 10005, N'VB-EBAY', 0, NULL)
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10002, 10187, N'string-aaaa', 0, N'string-aaaa')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10002, 10188, N'string-bbbbb', 0, N'string-bbbbb')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10002, 10193, N'WalmartTestAugust', 0, N'WalmartTestAugust')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10002, 10207, N'Testpost30', 0, N'Testpost30')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10002, 10208, N'Testpost31', 0, N'Testpost31')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10002, 10209, N'Testpost32', 0, N'Testpost32')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10002, 10210, N'Testpost33', 0, N'Testpost33')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10002, 10211, N'Testpost35', 0, N'Testpost35')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10002, 10212, N'Testpost36', 0, N'Testpost36')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10002, 10219, N'Testpost51', 0, N'Testpost51')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10004, 10008, N'Vibes-eBay', 0, NULL)
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10004, 10144, N'eBay Test!', 0, N'eBay Test!')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10004, 10145, N'eBayTestOAuth', 0, N'eBayTestOAuth')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10004, 10146, N'eBayTestOAuth2', 0, N'eBayTestOAuth2')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10004, 10147, N'oauth-test-2', 0, N'oauth-test-2')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10004, 10148, N'oauth-test-3', 0, N'oauth-test-3')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10004, 10149, N'eBayNator', 0, N'eBayNator')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10004, 10150, N'test', 0, N'test')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10004, 10154, N'aTestNickname2', 0, N'aTestNickname2')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10004, 10155, N'aTestNickname3', 0, N'aTestNickname3')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 10004, 10190, N'string-bbbbb', 0, N'string-bbbbb')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 20001, 10016, N'Vibes-Shopify', 0, N'Vibes')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 20001, 10030, N'Shopify', 0, NULL)
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 20001, 10240, N'', 0, N'Shopify')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 20001, 10256, N'crlsShopify', 0, N'crlsShopify')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 20003, 10015, N'Vibes-Magento-SP', 0, N'Vibes')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 20003, 10018, N'Vibes-Magento-PJ', 0, N'Vibes')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 20003, 10129, N'Kevins Magento Store', 0, N'Kevins Magento Store')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 20003, 10130, N'Another Magento Store', 0, N'Another Magento Store')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 20003, 10272, N'magentoNickNeim', 0, N'magentoNickNeim')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 20003, 10273, N'magentoTestCarlos', 0, N'magentoTestCarlos')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 20003, 10274, N'magentoCarlos2x', 0, N'magentoCarlos2x')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 20003, 10302, N'FE417RETESTdev', 0, N'FE417RETESTdev')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30001, 10009, N'Vibes-Nordstrom (By DSCO)', 0, N'Vibes')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30001, 10031, N'Nordstrom (By DSCO)', 0, NULL)
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30001, 10105, N'pok', 0, N'pok')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30001, 10110, N'foobar2010isbaaaaaaack', 0, N'foobar2010isbaaaaaaack')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30001, 10112, N'foobar2000asdasd', 0, N'foobar2000asdasd')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30001, 10113, N'foobar2004gg', 0, N'foobar2004gg')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30001, 10114, N'foobar2010gg', 0, N'foobar2010gg')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30001, 10115, N'foobar2010asdas', 0, N'foobar2010asdas')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30001, 10124, N'foobar2010sdfsdfsdf', 0, N'foobar2010sdfsdfsdf')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30001, 10125, N'foobar2010ghbfghb', 0, N'foobar2010ghbfghb')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30001, 10140, N'nicknames are overrated', 0, N'nicknames are overrated')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30001, 10142, N'Askldhjlaskdhaskha', 0, N'Askldhjlaskdhaskha')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30001, 10143, N'nickname!', 0, N'nickname!')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30001, 10152, N'yoshi88', 0, N'yoshi88')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30001, 10153, N'marioIsNotItalian', 0, N'marioIsNotItalian')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30001, 10158, N'Carlos Regular Test', 0, N'Carlos Regular Test')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30001, 10236, N'DSCO STAGING  14', 0, N'DSCO STAGING  14')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30001, 10318, N'Carlos Like Disco!', 0, N'Carlos Like Disco!')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30002, 10010, N'Vibes-Nordstrom Rack(by DSCO)', 0, N'Vibes')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30002, 10131, N'nicknametesting233', 0, N'nicknametesting233')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30002, 10136, N'Nickname', 0, N'Nickname')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30006, 10007, N'Vibes-Bloomingdale''s', 0, N'Vibes')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30006, 10106, N'testing1', 0, N'testing1')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30006, 10151, N'testing', 0, N'testing')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30006, 10267, N'CH-Bloomingdale', 0, N'CH-Bloomingdale')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30007, 10006, N'Vibes-JC Penney', 0, NULL)
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30007, 10126, N'Testing123444', 0, N'Testing123444')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30007, 10138, N'nickname', 0, N'nickname')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30007, 10156, N'Tes3945', 0, N'Tes3945')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30007, 10167, N'El Jey Zi', 0, N'El Jey Zi')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30007, 10170, N'Disco Vibes JC-Penney Integration', 0, N'Disco Vibes JC-Penney Integration')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30007, 10171, N'DSCO JC Penney Carlos', 0, N'DSCO JC Penney Carlos')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30007, 10264, N'CH-JC-Penney', 0, N'CH-JC-Penney')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30007, 10268, N'CH-JC2', 0, N'CH-JC2')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30007, 10269, N'CHJC3', 0, N'CHJC3')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30008, 10005, N'Vibes-Belk', 0, N'Vibes')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30008, 10137, N'newnickname123', 0, N'newnickname123')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30008, 10141, N'donke', 0, N'donke')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30008, 10157, N'nickname1233333', 0, N'nickname1233333')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30008, 10168, N'BELK Test Account', 0, N'BELK Test Account')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30008, 10265, N'CH-Belk', 0, N'CH-Belk')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30009, 10011, N'Vibes-LoardAndTaylor', 0, N'Vibes')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30009, 10038, N'LoardAndTaylor', 0, NULL)
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30009, 10133, N'nick1234444', 0, N'nick1234444')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30009, 10139, N'carlos integration', 0, N'carlos integration')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30009, 10169, N'L&T Test', 0, N'L&T Test')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10001, 30009, 10172, N'DSCO Loard And Taylor', 0, N'DSCO Loard And Taylor')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10011, 30002, 10303, N'WDF-Nordstrom Rack', 0, N'WDF-Nordstrom Rack')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10012, 10001, 10319, N'INT-Amazon', 0, N'INT-Amazon')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10012, 10002, 10320, N'INT-Walmart', 0, N'INT-Walmart')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10012, 10004, 10321, N'INT-eBay', 0, N'INT-eBay')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10012, 20001, 10322, N'INT-Shopify', 0, N'INT-Shopify')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10012, 20003, 10323, N'INT-Magento-SP', 0, N'INT-Magento')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10012, 20003, 10324, N'INT-Magento-PJ', 0, N'INT-Magento-PJ')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10012, 30001, 10326, N'INT-DSCO-N', 0, N'INT-DSCO-N')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10012, 30001, 10332, N'VAN-TEST-03', 0, N'VAN-TEST-03')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10012, 30002, 10325, N'INT-DSCO-NR', 0, N'INT-DSCO-NR')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10012, 30006, 10327, N'INT-CommerceHub-Bloomingdales', 0, N'INT-CommerceHub-Bloomingdales')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10012, 30007, 10328, N'INT-CommerceHub-JCPenny', 0, N'INT-CommerceHub-JCPenny')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10012, 30008, 10329, N'INT-CommerceHub-Belk', 0, N'INT-CommerceHub-Belk')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10001, 10012, 30009, 10330, N'INT-DSCO-LT', 0, N'INT-DSCO-LT')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 10001, 10159, N'Supplier Test ', 0, N'Supplier Test ')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 10001, 10162, N'Second Test for Amazon', 0, N'Second Test for Amazon')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 10001, 10163, N'TESTVAN', 0, N'TESTVAN')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 10001, 10164, N'TESTVAN007', 0, N'TESTVAN007')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 10001, 10165, N'Conan Test', 0, N'Conan Test')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 10001, 10175, N'AT-Amazon', 0, N'AT-Amazon')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 10001, 10176, N'AT-Amazon-1', 0, N'AT-Amazon-1')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 10001, 10177, N'Test-Aubree', 0, N'Test-Aubree')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 10001, 10178, N'TESTVAN008', 0, N'TESTVAN008')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 10001, 10179, N'TESTVAN009', 0, N'TESTVAN009')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 10002, 10160, N'Walmart Test Account', 0, N'Walmart Test Account')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 10002, 10174, N'Push Test Account', 0, N'Push Test Account')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 10002, 10192, N'Kevin Test', 0, N'Kevin Test')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 10002, 10194, N'Production VB Account Be Careful', 0, N'Production VB Account Be Careful')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 10002, 10239, N'Kevin Walmart Account', 0, N'Kevin Walmart Account')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 10002, 10244, N'Walmart-12', 0, N'Walmart-12')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 10002, 10299, N'Walmart Demo', 0, N'Walmart Demo')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 10002, 10300, N'Demo 2', 0, N'Demo 2')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 10002, 10317, N'Walmart Test 2', 0, N'Walmart Test 2')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 10004, 10166, N'eBay Test Account', 0, N'eBay Test Account')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 10004, 10173, N'AT-eBay', 0, N'AT-eBay')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 20001, 10241, N'', 0, N'Shopify')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 20001, 10243, N'', 0, N'Shopify-1')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 20001, 10251, N'ShopTest1', 0, N'ShopifyTest7')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 20001, 10252, N'ShopTest2', 0, N'ShopifyTest8')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 20001, 10253, N'ShopTest3', 0, N'ShopifyTest9')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 20001, 10254, N'', 0, N'ShopifyTest9')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 20001, 10255, N'', 0, N'ShopifyTest10')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 20001, 10257, N'Test-007', 0, N'Test-007')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 20001, 10261, N'Test_Shopify_Will', 0, N'Test_Shopify_Will')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 20001, 10262, N'Test_Shopify_Will2', 0, N'Test_Shopify_Will2')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 20001, 10279, N'Test-008', 0, N'Test-008')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 20001, 10280, N'Shopify Demo', 0, N'Shopify Demo')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 20003, 10180, N'AT-Magento', 0, N'AT-Magento')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 20003, 10197, N'Magento s&p', 0, N'Magento s&p')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 20003, 10278, N'AT-Magento-TestMapping', 0, N'AT-Magento-TestMapping')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 20003, 10281, N'Magento Demo', 0, N'Magento Demo')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 20003, 10307, N'Magento v1', 0, N'Magento v1')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 20003, 10308, N'Magento v2', 0, N'Magento v2')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 30001, 10183, N'AT-Nordstrom', 0, N'AT-Nordstrom')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 30001, 10282, N'DSCO Nord Demo', 0, N'DSCO Nord Demo')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 30001, 10331, N'VAN-TEST-02', 0, N'VAN-TEST-02')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 30002, 10182, N'AT-NordstromRack', 0, N'AT-NordstromRack')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 30006, 10186, N'AT-Bloomingdale', 0, N'AT-Bloomingdale')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 30007, 10185, N'AT-JCPenney', 0, N'AT-JCPenney')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 30008, 10184, N'AT-Belk', 0, N'AT-Belk')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 30008, 10283, N'CH Belk Demo', 0, N'CH Belk Demo')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 30009, 10161, N'carlos loard and taylor supply A', 0, N'carlos loard and taylor supply A')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 30009, 10181, N'AT-LoardAndTaylor', 0, N'AT-LoardAndTaylor')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10003, 30009, 10304, N'VAN-TEST-01', 0, N'VAN-TEST-01')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 10001, 10315, N'Amazon Test 1', 0, N'Amazon Test 1')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 10002, 10195, N'Roger Test Account', 0, N'Roger Test Account')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 10002, 10198, N'Roger Test Account (Sandbox API)', 0, N'Roger Test Account (Sandbox API)')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 10002, 10202, N'Walmart Sandbox API - 2', 0, N'Walmart Sandbox API - 2')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 10002, 10287, N'Demo', 0, N'Demo')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 10002, 10316, N'Walmart Test 1', 0, N'Walmart Test 1')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 10004, 10196, N'Test', 0, N'Test')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 10004, 10201, N'eBays_CETC', 0, N'eBays_CETC')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 10004, 10290, N'Demo', 0, N'Demo')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 10004, 10311, N'Test 2', 0, N'Test 2')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 10004, 10314, N'Ebay Test 3', 0, N'Ebay Test 3')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 20001, 10246, N'', 0, N'ShopifyTest1')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 20001, 10247, N'', 0, N'ShopifyTest2')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 20001, 10248, N'', 0, N'ShopifyTest4')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 20001, 10249, N'', 0, N'ShopifyTest5')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 20001, 10250, N'ShopTest1', 0, N'ShopifyTest7')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 20001, 10258, N'', 0, N'ShopifyTest10')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 20001, 10259, N'', 0, N'ShopifyTest11')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 20001, 10260, N' vibesshopifyapi', 0, N' vibesshopifyapi')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 20001, 10291, N'Demo', 0, N'Demo')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 20001, 10309, N'VibeShopify2', 0, N'VibeShopify2')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 20001, 10310, N' vibesshopifyapi2', 0, N' vibesshopifyapi2')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 20001, 10312, N'Shopify v1', 0, N'Shopify v1')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 20001, 10334, N'Shopify Test 1', 0, N'Shopify Test 1')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 20003, 10199, N'Magento s&p', 0, N'Magento s&p')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 20003, 10200, N'Magento s&p DEV', 0, N'Magento s&p DEV')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 20003, 10286, N'sp_api_get', 0, N'sp_api_get')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 20003, 10298, N'Demo Magento', 0, N'Demo Magento')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 20003, 10305, N'Magento v3', 0, N'Magento v3')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 20003, 10306, N'Magento v4', 0, N'Magento v4')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 20003, 10313, N'Magento v5', 0, N'Magento v5')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 30001, 10229, N'DSCO STAGING  12', 0, N'DSCO STAGING  12')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 30001, 10235, N'DSCO STAGING  13', 0, N'DSCO STAGING  13')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 30001, 10237, N'DSCO STAGING  14', 0, N'DSCO STAGING  14')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 30001, 10293, N'Demo Nordstrom', 0, N'Demo Nordstrom')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 30002, 10292, N'Demo - Nordstrom Rack', 0, N'Demo - Nordstrom Rack')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 30007, 10238, N'Test Account', 0, N'Test Account')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 30007, 10296, N'Demo - JCPenny', 0, N'Demo - JCPenny')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 30008, 10297, N'Demo Belk', 0, N'Demo Belk')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 30009, 10270, N'LT_TEST', 0, N'LT_TEST')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 30009, 10271, N'LT Testing', 0, N'LT Testing')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10004, 30009, 10288, N'Demo', 0, N'Demo')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10005, 20001, 10242, N'', 0, N'Shopify')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10005, 20001, 10335, N'Shopify Test 1', 0, N'Shopify Test 1')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10005, 20003, 10333, N'Magento Test 1', 0, N'Magento Test 1')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10005, 20003, 10337, N'Magento Test 2', 0, N'Magento Test 2')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10005, 20003, 10338, N'Magento Test 3', 0, N'Magento Test 3')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10005, 30001, 10301, N'BE-Test-Aubree', 0, N'BE-Test-Aubree')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10002, 10005, 30001, 10336, N'DSCO Staging Test 1', 0, N'DSCO Staging Test 1')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10003, 10007, 10002, 10275, N'Roger Test Account', 0, N'Roger Test Account')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10003, 10007, 10002, 10276, N'Walmart Test Account 2', 0, N'Walmart Test Account 2')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10003, 10007, 10002, 10277, N'Walmart Test Account 3', 0, N'Walmart Test Account 3')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10003, 10007, 20003, 10284, N'sp_api_get', 0, N'sp_api_get')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10003, 10007, 20003, 10285, N'API for DEV S&P', 0, N'API for DEV S&P')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10004, 10004, 20003, 10020, N'Test for DEV Magento1', 0, N'Test for DEV Magento1')
GO
INSERT [dbo].[Setting_ChannelAccount] ([MasterAccountNum], [ProfileNum], [ChannelNum], [ChannelAccountNum], [ChannelAccountName], [MissingItemCreation], [CompanyName]) VALUES (10004, 10004, 20003, 10021, N'Test for DEV Magento2', 0, N'Test for DEV Magento2')
GO
ALTER TABLE [dbo].[Setting_ChannelAccount] ADD  CONSTRAINT [DF_Setting_ChannelAccount_MissingItemCreation]  DEFAULT ((0)) FOR [MissingItemCreation]
GO
