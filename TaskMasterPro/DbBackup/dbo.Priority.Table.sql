USE [TaskMasterPro]
GO
/****** Object:  Table [dbo].[Priority]    Script Date: 31-01-2025 12:07:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Priority](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Priority] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Priority] ON 

INSERT [dbo].[Priority] ([id], [Priority]) VALUES (1, N'Low')
INSERT [dbo].[Priority] ([id], [Priority]) VALUES (2, N'Medium')
INSERT [dbo].[Priority] ([id], [Priority]) VALUES (3, N'High')
SET IDENTITY_INSERT [dbo].[Priority] OFF
GO
