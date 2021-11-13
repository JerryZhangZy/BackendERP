CREATE TABLE [dbo].[Setting_ChannelAccount](
	[MasterAccountNum] [int] NOT NULL,
	[ProfileNum] [int] NOT NULL,
	[ChannelNum] [int] NOT NULL,
	[ChannelAccountNum] [int] NOT NULL,
	[ChannelAccountName] [nvarchar](200) NOT NULL,
	[MissingItemCreation] [tinyint] NULL,
	[CompanyName] NVARCHAR(200) NULL, 
CONSTRAINT [PK_Setting_ChannelAccount] PRIMARY KEY CLUSTERED 
(
	[MasterAccountNum] ASC,
	[ProfileNum] ASC,
	[ChannelNum] ASC,
	[ChannelAccountNum] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Setting_ChannelAccount] ADD  CONSTRAINT [DF_Setting_ChannelAccount_MissingItemCreation]  DEFAULT ((0)) FOR [MissingItemCreation]
GO


