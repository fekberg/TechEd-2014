USE [master]
GO
CREATE DATABASE [TechEd2014]
GO
USE [TechEd2014]
GO

CREATE TABLE [dbo].[Customers](
	[Id] [uniqueidentifier] NOT NULL,
	[Firstname] [nvarchar](max) NULL,
	[Lastname] [nvarchar](max) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


CREATE TABLE [dbo].[Devices](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[CustomerId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Device] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

INSERT [dbo].[Customers] ([Id], [Firstname], [Lastname]) VALUES (N'feb1fc2a-d6e4-4abf-b813-2748ae6832cf', N'Filip', N'Ekberg')
INSERT [dbo].[Customers] ([Id], [Firstname], [Lastname]) VALUES (N'5ac52c61-4e4e-4369-af64-2f2ffdb1e683', N'Dr Who', N'')
INSERT [dbo].[Customers] ([Id], [Firstname], [Lastname]) VALUES (N'223d49a4-acf5-4ee0-b74f-6a916cb7b8f4', N'Mr. Bond', NULL)
INSERT [dbo].[Customers] ([Id], [Firstname], [Lastname]) VALUES (N'eff80c32-31fc-4fce-9bf7-f11cd8b6a763', N'Bill', N'G')
INSERT [dbo].[Devices] ([Id], [Name], [CustomerId]) VALUES (N'72d1b10e-a998-4b24-b3a0-1dd741e227d9', N'Surface Pro 3', N'feb1fc2a-d6e4-4abf-b813-2748ae6832cf')
INSERT [dbo].[Devices] ([Id], [Name], [CustomerId]) VALUES (N'3a5cd6bf-8cb2-4ebf-9eb7-50116f4c5e5e', N'Nokia Lumia 930', N'feb1fc2a-d6e4-4abf-b813-2748ae6832cf')
INSERT [dbo].[Devices] ([Id], [Name], [CustomerId]) VALUES (N'ad2b3cb5-ea62-45a4-86ce-8d2350acdd22', N'Tardis', N'5ac52c61-4e4e-4369-af64-2f2ffdb1e683')

ALTER TABLE [dbo].[Customers] ADD  CONSTRAINT [DF_Customer_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Devices] ADD  CONSTRAINT [DF_Device_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD  CONSTRAINT [FK_Device_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[Devices] CHECK CONSTRAINT [FK_Device_Customer]
GO
