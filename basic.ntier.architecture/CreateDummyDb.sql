﻿USE [master]
GO

/****** Object:  Database [HelloWorld]    Script Date: 17/06/2015 20:33:03 ******/
CREATE DATABASE [HelloWorld]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HelloWorld', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\HelloWorld.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'HelloWorld_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\HelloWorld_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [HelloWorld] SET COMPATIBILITY_LEVEL = 120
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HelloWorld].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [HelloWorld] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [HelloWorld] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [HelloWorld] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [HelloWorld] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [HelloWorld] SET ARITHABORT OFF 
GO

ALTER DATABASE [HelloWorld] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [HelloWorld] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [HelloWorld] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [HelloWorld] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [HelloWorld] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [HelloWorld] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [HelloWorld] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [HelloWorld] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [HelloWorld] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [HelloWorld] SET  DISABLE_BROKER 
GO

ALTER DATABASE [HelloWorld] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [HelloWorld] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [HelloWorld] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [HelloWorld] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [HelloWorld] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [HelloWorld] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [HelloWorld] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [HelloWorld] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [HelloWorld] SET  MULTI_USER 
GO

ALTER DATABASE [HelloWorld] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [HelloWorld] SET DB_CHAINING OFF 
GO

ALTER DATABASE [HelloWorld] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [HelloWorld] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [HelloWorld] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [HelloWorld] SET  READ_WRITE 
GO

/****** Object:  Table [dbo].[Customer]    Script Date: 17/06/2015 20:31:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nchar](50) NULL,
	[LastName] [nchar](50) NULL,	
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

INSERT INTO [dbo].[Customer] ([FirstName], [LastName]) VALUES ('Hello', 'World')
GO