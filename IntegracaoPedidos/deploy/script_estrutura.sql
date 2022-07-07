USE [IntegracaoVendas]
GO

/****** Object:  Table [dbo].[INFORMACOES_PEDIDOS]    Script Date: 20/06/2020 13:36:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[INFORMACOES_PEDIDOS](
	[PEDIDO] [nvarchar](450) NOT NULL,
	[VOLUME] [int] NOT NULL,
	[PESO] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_INFORMACOES_PEDIDOS] PRIMARY KEY CLUSTERED 
(
	[PEDIDO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

