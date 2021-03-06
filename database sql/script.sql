USE [listeDeContact]
GO
/****** Object:  Table [dbo].[adress]    Script Date: 2018-02-05 08:15:15 ******/
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
/****** Object:  Table [dbo].[events]    Script Date: 2018-02-05 08:15:15 ******/
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
/****** Object:  Table [dbo].[pictures]    Script Date: 2018-02-05 08:15:15 ******/
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
/****** Object:  Table [dbo].[users]    Script Date: 2018-02-05 08:15:15 ******/
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
/****** Object:  Table [dbo].[usersContactList]    Script Date: 2018-02-05 08:15:15 ******/
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
