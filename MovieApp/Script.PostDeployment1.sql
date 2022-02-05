/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
CREATE TABLE [dbo].[Movie](
	[MovieID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ReleaseDate] [datetime] NOT NULL,
	[MpaaRating] [int] NOT NULL,
	[Genre] [varchar](50) NOT NULL,
	[Rating] [float] NOT NULL,
 CONSTRAINT [PK_Movie] PRIMARY KEY CLUSTERED 
(
	[MovieID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Movie] ON 

GO
INSERT [dbo].[Movie] ([MovieID], [Name], [ReleaseDate], [MpaaRating], [Genre], [Rating]) VALUES (1, N'The Amazing Spider-Man', CAST(N'2012-07-03 00:00:00.000' AS DateTime), 3, N'Adventure', 7)
GO
INSERT [dbo].[Movie] ([MovieID], [Name], [ReleaseDate], [MpaaRating], [Genre], [Rating]) VALUES (2, N'Beauty and the Beast', CAST(N'2017-03-17 00:00:00.000' AS DateTime), 3, N'Family', 7.8)
GO
INSERT [dbo].[Movie] ([MovieID], [Name], [ReleaseDate], [MpaaRating], [Genre], [Rating]) VALUES (3, N'The Secret Life of Pets', CAST(N'2016-07-08 00:00:00.000' AS DateTime), 1, N'Adventure', 6.6)
GO
INSERT [dbo].[Movie] ([MovieID], [Name], [ReleaseDate], [MpaaRating], [Genre], [Rating]) VALUES (4, N'The Jungle Book', CAST(N'2016-04-15 00:00:00.000' AS DateTime), 2, N'Fantasy', 7.5)
GO
INSERT [dbo].[Movie] ([MovieID], [Name], [ReleaseDate], [MpaaRating], [Genre], [Rating]) VALUES (5, N'Split', CAST(N'2017-01-20 00:00:00.000' AS DateTime), 3, N'Horror', 7.4)
GO
INSERT [dbo].[Movie] ([MovieID], [Name], [ReleaseDate], [MpaaRating], [Genre], [Rating]) VALUES (6, N'The Mummy', CAST(N'2017-06-09 00:00:00.000' AS DateTime), 4, N'Action', 6.7)
GO
SET IDENTITY_INSERT [dbo].[Movie] OFF
GO
