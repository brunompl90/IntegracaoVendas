﻿
/****** Object:  Table [dbo].[INFORMACOES_CORREIOS]    Script Date: 22/06/2020 15:26:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[INFORMACOES_CORREIOS](
	[OBJETO] [nvarchar](450) NOT NULL,
	[NOTA] [nvarchar](max) NULL,
	[STATUS] [int] NOT NULL,
	[DATA] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_INFORMACOES_CORREIOS] PRIMARY KEY CLUSTERED 
(
	[OBJETO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


