USE [master]
GO
/****** Object:  Database [listeDeContact]    Script Date: 2018-02-03 16:29:11 ******/
CREATE DATABASE [listeDeContact]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'listeDeContact', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\listeDeContact.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'listeDeContact_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\listeDeContact_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [listeDeContact] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [listeDeContact].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [listeDeContact] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [listeDeContact] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [listeDeContact] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [listeDeContact] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [listeDeContact] SET ARITHABORT OFF 
GO
ALTER DATABASE [listeDeContact] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [listeDeContact] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [listeDeContact] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [listeDeContact] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [listeDeContact] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [listeDeContact] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [listeDeContact] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [listeDeContact] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [listeDeContact] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [listeDeContact] SET  DISABLE_BROKER 
GO
ALTER DATABASE [listeDeContact] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [listeDeContact] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [listeDeContact] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [listeDeContact] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [listeDeContact] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [listeDeContact] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [listeDeContact] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [listeDeContact] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [listeDeContact] SET  MULTI_USER 
GO
ALTER DATABASE [listeDeContact] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [listeDeContact] SET DB_CHAINING OFF 
GO
ALTER DATABASE [listeDeContact] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [listeDeContact] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [listeDeContact] SET DELAYED_DURABILITY = DISABLED 
GO
USE [listeDeContact]
GO
/****** Object:  Table [dbo].[adress]    Script Date: 2018-02-03 16:29:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[adress](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[number] [int] NOT NULL,
	[street] [varchar](255) NOT NULL,
	[city] [varchar](255) NOT NULL,
	[province] [varchar](255) NOT NULL,
	[country] [nchar](10) NOT NULL,
 CONSTRAINT [PK_adress] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[events]    Script Date: 2018-02-03 16:29:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[events](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[description] [text] NOT NULL,
	[idRelation] [int] NOT NULL,
	[isConfirmed] [bit] NULL,
	[date] [datetime] NOT NULL,
 CONSTRAINT [PK_events] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[pictures]    Script Date: 2018-02-03 16:29:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pictures](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[src] [varchar](255) NOT NULL,
 CONSTRAINT [PK_pictures] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[users]    Script Date: 2018-02-03 16:29:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[lastName] [varchar](255) NOT NULL,
	[firstName] [varchar](255) NOT NULL,
	[personnage] [varchar](255) NOT NULL,
	[password] [varchar](255) NOT NULL,
	[idAddress] [int] NULL,
	[idPicture] [int] NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_users] UNIQUE NONCLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[usersContactList]    Script Date: 2018-02-03 16:29:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usersContactList](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idUser] [int] NOT NULL,
	[idFriend] [int] NOT NULL,
	[isFriend] [bit] NOT NULL,
	[addedDate] [datetime] NULL,
 CONSTRAINT [PK_usersContactList] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[events] ADD  CONSTRAINT [DF_events_isConfirmed]  DEFAULT ((0)) FOR [isConfirmed]
GO
ALTER TABLE [dbo].[events] ADD  CONSTRAINT [DF_events_date]  DEFAULT (getdate()) FOR [date]
GO
ALTER TABLE [dbo].[usersContactList] ADD  CONSTRAINT [DF_usersContactList_isFriend]  DEFAULT ((0)) FOR [isFriend]
GO
ALTER TABLE [dbo].[usersContactList] ADD  CONSTRAINT [DF_usersContactList_addedDate]  DEFAULT (getdate()) FOR [addedDate]
GO
ALTER TABLE [dbo].[events]  WITH CHECK ADD  CONSTRAINT [FK_events_usersContactList] FOREIGN KEY([idRelation])
REFERENCES [dbo].[usersContactList] ([id])
GO
ALTER TABLE [dbo].[events] CHECK CONSTRAINT [FK_events_usersContactList]
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_users_adress] FOREIGN KEY([idAddress])
REFERENCES [dbo].[adress] ([id])
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_users_adress]
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_users_pictures] FOREIGN KEY([idPicture])
REFERENCES [dbo].[pictures] ([id])
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_users_pictures]
GO
ALTER TABLE [dbo].[usersContactList]  WITH CHECK ADD  CONSTRAINT [FK_usersContactList_users] FOREIGN KEY([idUser])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[usersContactList] CHECK CONSTRAINT [FK_usersContactList_users]
GO
ALTER TABLE [dbo].[usersContactList]  WITH CHECK ADD  CONSTRAINT [FK_usersContactList_users1] FOREIGN KEY([idFriend])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[usersContactList] CHECK CONSTRAINT [FK_usersContactList_users1]
GO
USE [master]
GO
ALTER DATABASE [listeDeContact] SET  READ_WRITE 
GO
