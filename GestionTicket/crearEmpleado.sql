USE [BD_GestionTickets]
GO

/****** Object:  Table [dbo].[t_Empleado]    Script Date: 15/07/2017 04:11:34 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[t_Empleado](
	[Dni] [int] NULL,
	[Nombre] [varchar](50) NULL,
	[Certificado] [bit] NULL,
	[Edad] [int] NULL,
	[Estado] [varchar](50) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


