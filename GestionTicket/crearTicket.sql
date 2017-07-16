USE [BD_GestionTickets]
GO

/****** Object:  Table [dbo].[Ticket]    Script Date: 15/07/2017 04:12:15 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Ticket](
	[Codigo] [varchar](10) NULL,
	[RucCliente] [numeric](18, 0) NULL,
	[Descripcion] [varchar](100) NULL,
	[DniEmpleado] [int] NULL,
	[Estado] [varchar](50) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


