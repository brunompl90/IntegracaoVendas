/****** Object:  Table [dbo].[Orders]    Script Date: 24/05/2020 09:52:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Orders](
	[OrderNumber] [nvarchar](450) NOT NULL,
	[CustomerCode] [nvarchar](max) NULL,
	[LineTotal] [nvarchar](max) NULL,
	[CustomerTotal] [nvarchar](max) NULL,
	[CustomerDiscountTotal] [nvarchar](max) NULL,
	[DiscountCode] [nvarchar](max) NULL,
	[CustomerTaxTotal] [nvarchar](max) NULL,
	[VatCode] [nvarchar](max) NULL,
	[Culture] [nvarchar](max) NULL,
	[Currency] [nvarchar](max) NULL,
	[Status] [nvarchar](max) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


/****** Object:  Table [dbo].[LineItens]    Script Date: 24/05/2020 09:53:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LineItens](
	[LineItemId] [int] IDENTITY(1,1) NOT NULL,
	[OrderNumber] [nvarchar](450) NULL,
	[LineNumber] [nvarchar](max) NULL,
	[VariantId] [nvarchar](max) NULL,
	[DisplayName] [nvarchar](max) NULL,
	[Quantity] [nvarchar](max) NULL,
	[UnitListPrice] [nvarchar](max) NULL,
	[UnitCustomerPrice] [nvarchar](max) NULL,
	[LineListPrice] [nvarchar](max) NULL,
	[LineCustomerPrice] [nvarchar](max) NULL,
	[LineTaxAmount] [nvarchar](max) NULL,
	[TaxPercentage] [nvarchar](max) NULL,
 CONSTRAINT [PK_LineItens] PRIMARY KEY CLUSTERED 
(
	[LineItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[LineItens]  WITH CHECK ADD  CONSTRAINT [FK_LineItens_Orders_OrderNumber] FOREIGN KEY([OrderNumber])
REFERENCES [dbo].[Orders] ([OrderNumber])
GO

ALTER TABLE [dbo].[LineItens] CHECK CONSTRAINT [FK_LineItens_Orders_OrderNumber]
GO

/****** Object:  Table [dbo].[BillingAddresses]    Script Date: 24/05/2020 15:03:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BillingAddresses](
	[BillingAddressId] [int] IDENTITY(1,1) NOT NULL,
	[OrderNumber] [nvarchar](450) NULL,
	[FirstName] [nvarchar](max) NULL,
	[AddressLine1] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
	[CountryCode] [nvarchar](max) NULL,
	[CountryName] [nvarchar](max) NULL,
	[PostalCode] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[AddressLine2] [nvarchar](max) NULL,
	[AddressLine3] [nvarchar](max) NULL,
	[AddressLine4] [nvarchar](max) NULL,
 CONSTRAINT [PK_BillingAddresses] PRIMARY KEY CLUSTERED 
(
	[BillingAddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[BillingAddresses]  WITH CHECK ADD  CONSTRAINT [FK_BillingAddresses_Orders_OrderNumber] FOREIGN KEY([OrderNumber])
REFERENCES [dbo].[Orders] ([OrderNumber])
GO

ALTER TABLE [dbo].[BillingAddresses] CHECK CONSTRAINT [FK_BillingAddresses_Orders_OrderNumber]
GO


/****** Object:  Table [dbo].[Payments]    Script Date: 24/05/2020 09:53:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Payments](
	[PaymentId] [nvarchar](450) NOT NULL,
	[OrderNumber] [nvarchar](450) NULL,
	[PaymentType] [nvarchar](max) NULL,
	[PaymentCurrency] [nvarchar](max) NULL,
	[PaymentValue] [nvarchar](max) NULL,
	[AdyenInstallmentsNo] [nvarchar](max) NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_Orders_OrderNumber] FOREIGN KEY([OrderNumber])
REFERENCES [dbo].[Orders] ([OrderNumber])
GO

ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_Orders_OrderNumber]
GO


/****** Object:  Table [dbo].[Shipments]    Script Date: 24/05/2020 15:43:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Shipments](
	[ShipmentID] [nvarchar](450) NOT NULL,
	[OrderNumber] [nvarchar](450) NULL,
	[ShippingMethodId] [nvarchar](max) NULL,
	[ShippingMethodName] [nvarchar](max) NULL,
	[ShipmentAmount] [nvarchar](max) NULL,
	[ShipmentTaxAmount] [nvarchar](max) NULL,
	[FirstName] [nvarchar](max) NULL,
	[AddressLine1] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
	[CountryCode] [nvarchar](max) NULL,
	[CountryName] [nvarchar](max) NULL,
	[PostalCode] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[AddressLine2] [nvarchar](max) NULL,
	[AddressLine3] [nvarchar](max) NULL,
	[AddressLine4] [nvarchar](max) NULL,
 CONSTRAINT [PK_Shipments] PRIMARY KEY CLUSTERED 
(
	[ShipmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Shipments]  WITH CHECK ADD  CONSTRAINT [FK_Shipments_Orders_OrderNumber] FOREIGN KEY([OrderNumber])
REFERENCES [dbo].[Orders] ([OrderNumber])
GO

ALTER TABLE [dbo].[Shipments] CHECK CONSTRAINT [FK_Shipments_Orders_OrderNumber]
GO
