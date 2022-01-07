CREATE TABLE [dbo].[Setting_ShippingCarrierService](
	[CarrierServiceId] [varchar](80) NOT NULL,
	[CarrierId] [varchar](30) NOT NULL,
	[ServiceCode] [nvarchar](100) NOT NULL,
	[ServiceName] [nvarchar](100) NOT NULL,
	[Note] [nvarchar](500) NULL,
 CONSTRAINT [PK_Setting_ShippingCarrierService] PRIMARY KEY CLUSTERED 
(
	[CarrierServiceId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_Setting_ShippingCarrierService_CarrierId_ServiceCode] UNIQUE NONCLUSTERED 
(
	[CarrierId] ASC,
	[ServiceCode] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Setting_ShippingCarrierService]  WITH CHECK ADD  CONSTRAINT [FK_Setting_ShippingCarrierService_Setting_ShippingCarrier] FOREIGN KEY([CarrierId])
REFERENCES [dbo].[Setting_ShippingCarrier] ([CarrierId])
GO

ALTER TABLE [dbo].[Setting_ShippingCarrierService] CHECK CONSTRAINT [FK_Setting_ShippingCarrierService_Setting_ShippingCarrier]