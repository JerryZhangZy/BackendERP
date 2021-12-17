CREATE TABLE [dbo].[Setting_ShippingCarrier](
	[CarrierId] [varchar](30) NOT NULL,
	[CarrierCode] [nvarchar](80) NOT NULL,
	[CarrierName] [nvarchar](80) NOT NULL,
	[TrackingUrl] [nvarchar](500) NULL,
	[Note] [nvarchar](500) NULL,
 CONSTRAINT [PK_Setting_ShippingCarrier] PRIMARY KEY CLUSTERED 
(
	[CarrierId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [UK_Setting_ShippingCarrier_CarrierCode]
    ON [dbo].[Setting_ShippingCarrier]([CarrierCode] ASC);
GO

