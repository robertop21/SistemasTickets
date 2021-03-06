USE [master]
GO
/****** Object:  Database [BD_GestionTickets]    Script Date: 21/07/2017 09:31:16 p.m. ******/
CREATE DATABASE [BD_GestionTickets]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BD_GestionTickets', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\BD_GestionTickets.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BD_GestionTickets_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\BD_GestionTickets_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [BD_GestionTickets] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BD_GestionTickets].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BD_GestionTickets] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BD_GestionTickets] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BD_GestionTickets] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BD_GestionTickets] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BD_GestionTickets] SET ARITHABORT OFF 
GO
ALTER DATABASE [BD_GestionTickets] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BD_GestionTickets] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [BD_GestionTickets] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BD_GestionTickets] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BD_GestionTickets] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BD_GestionTickets] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BD_GestionTickets] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BD_GestionTickets] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BD_GestionTickets] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BD_GestionTickets] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BD_GestionTickets] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BD_GestionTickets] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BD_GestionTickets] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BD_GestionTickets] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BD_GestionTickets] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BD_GestionTickets] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BD_GestionTickets] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BD_GestionTickets] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BD_GestionTickets] SET RECOVERY FULL 
GO
ALTER DATABASE [BD_GestionTickets] SET  MULTI_USER 
GO
ALTER DATABASE [BD_GestionTickets] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BD_GestionTickets] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BD_GestionTickets] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BD_GestionTickets] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'BD_GestionTickets', N'ON'
GO
USE [BD_GestionTickets]
GO
/****** Object:  FullTextCatalog [TestFTCat]    Script Date: 21/07/2017 09:31:17 p.m. ******/
CREATE FULLTEXT CATALOG [TestFTCat]WITH ACCENT_SENSITIVITY = ON

GO
/****** Object:  Table [dbo].[t_Cliente]    Script Date: 21/07/2017 09:31:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_Cliente](
	[Ruc] [numeric](11, 0) NULL,
	[RazonSocial] [varchar](50) NULL,
	[Estado] [varchar](20) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_Empleado]    Script Date: 21/07/2017 09:31:17 p.m. ******/
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
	[Estado] [varchar](50) NULL,
	[CodigoEspecialidad] [varchar](10) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_Especialidad]    Script Date: 21/07/2017 09:31:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_Especialidad](
	[Codigo] [varchar](10) NULL,
	[Descripcion] [varchar](100) NULL,
	[Estado] [varchar](20) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_ruc]    Script Date: 21/07/2017 09:31:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_ruc](
	[cod_cli] [varchar](50) NOT NULL,
	[ruc_cli] [varchar](11) NULL,
 CONSTRAINT [PK_ruc] PRIMARY KEY CLUSTERED 
(
	[cod_cli] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_Ticket]    Script Date: 21/07/2017 09:31:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_Ticket](
	[CodigoTicket] [int] IDENTITY(1,1) NOT NULL,
	[RucCliente] [numeric](11, 0) NULL,
	[Fecha] [datetime] NULL,
	[Descripcion] [varchar](200) NULL,
	[CodigoEspecialidad] [varchar](10) NULL,
	[Estado] [varchar](20) NULL,
	[DniEmpleado] [int] NULL,
	[Respuesta] [varchar](200) NULL,
 CONSTRAINT [PK_t_Ticket] PRIMARY KEY CLUSTERED 
(
	[CodigoTicket] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [master]
GO
ALTER DATABASE [BD_GestionTickets] SET  READ_WRITE 
GO
