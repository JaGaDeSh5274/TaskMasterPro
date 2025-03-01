USE [TaskMasterPro]
GO
/****** Object:  Table [dbo].[status]    Script Date: 31-01-2025 12:07:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[status](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[status] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[status] ON 

INSERT [dbo].[status] ([id], [status]) VALUES (1, N'Pending')
INSERT [dbo].[status] ([id], [status]) VALUES (2, N'In Progress')
INSERT [dbo].[status] ([id], [status]) VALUES (3, N'Completed')
SET IDENTITY_INSERT [dbo].[status] OFF
GO
