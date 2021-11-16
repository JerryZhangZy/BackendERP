CREATE TABLE [dbo].[Setting_Channel]
(
	[MasterAccountNum] [int] NOT NULL,
	[ProfileNum] [int] NOT NULL,
	[ChannelNum] INT NOT NULL,
	[ChannelName] NVARCHAR(200) NOT NULL,
	[OrderSplitFlag] tinyint Null, -- 0 Not Split, 1 Split
	[OrderSplitPriorityLevel] tinyint Null, -- 0: Least DistributionCenters 1: DistributionCenters Priority. 0: No 1: Yes
	[Category] [varchar](50) NULL,
    [PlatformNum] [int] NULL, 
	[PlatformName] [nvarchar](50) NULL,
	[ChannelCurrency] [nvarchar](50) NULL,
	CONSTRAINT [PK_Setting_Channel] PRIMARY KEY ([MasterAccountNum], [ProfileNum], [ChannelNum])
)
