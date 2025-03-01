USE [TaskMasterPro]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 31-01-2025 12:07:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](255) NOT NULL,
	[Description] [text] NULL,
	[DueDate] [date] NULL,
	[PriorityId] [int] NULL,
	[StatusId] [int] NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Tasks] ON 

INSERT [dbo].[Tasks] ([Id], [Title], [Description], [DueDate], [PriorityId], [StatusId], [IsActive]) VALUES (1, N'Task1', N'EndPoint Testing', CAST(N'2025-01-29' AS Date), 1, 3, 1)
INSERT [dbo].[Tasks] ([Id], [Title], [Description], [DueDate], [PriorityId], [StatusId], [IsActive]) VALUES (2, N'Task2', N'EndPoint Testing', CAST(N'2025-01-29' AS Date), 2, 3, 1)
INSERT [dbo].[Tasks] ([Id], [Title], [Description], [DueDate], [PriorityId], [StatusId], [IsActive]) VALUES (3, N'Test', N'Task test', CAST(N'2025-01-14' AS Date), 2, 3, 1)
INSERT [dbo].[Tasks] ([Id], [Title], [Description], [DueDate], [PriorityId], [StatusId], [IsActive]) VALUES (4, N'add', N'Hello', CAST(N'2025-01-31' AS Date), 1, 2, 1)
INSERT [dbo].[Tasks] ([Id], [Title], [Description], [DueDate], [PriorityId], [StatusId], [IsActive]) VALUES (5, N'Hello', N'DEs c', CAST(N'2025-01-07' AS Date), 1, 3, 1)
INSERT [dbo].[Tasks] ([Id], [Title], [Description], [DueDate], [PriorityId], [StatusId], [IsActive]) VALUES (6, N'Hi task', N'Hello', CAST(N'2025-01-06' AS Date), 2, 2, 1)
INSERT [dbo].[Tasks] ([Id], [Title], [Description], [DueDate], [PriorityId], [StatusId], [IsActive]) VALUES (7, N'Hello task', N'Hi', CAST(N'2025-01-17' AS Date), 2, 3, 1)
INSERT [dbo].[Tasks] ([Id], [Title], [Description], [DueDate], [PriorityId], [StatusId], [IsActive]) VALUES (8, N'Hi ', N'Hello', CAST(N'2025-01-29' AS Date), 1, 3, 1)
INSERT [dbo].[Tasks] ([Id], [Title], [Description], [DueDate], [PriorityId], [StatusId], [IsActive]) VALUES (9, N'Just task 2', N'Gf 2', CAST(N'2025-01-07' AS Date), 3, 2, 1)
INSERT [dbo].[Tasks] ([Id], [Title], [Description], [DueDate], [PriorityId], [StatusId], [IsActive]) VALUES (10, N'Task2', N'Testing Task', CAST(N'2025-01-31' AS Date), 1, 3, 1)
SET IDENTITY_INSERT [dbo].[Tasks] OFF
GO
ALTER TABLE [dbo].[Tasks] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD FOREIGN KEY([PriorityId])
REFERENCES [dbo].[Priority] ([id])
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD FOREIGN KEY([StatusId])
REFERENCES [dbo].[status] ([id])
GO
