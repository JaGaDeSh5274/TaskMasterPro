USE [TaskMasterPro]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetAllTaskDetails]    Script Date: 31-01-2025 12:07:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Sp_GetAllTaskDetails]
AS
BEGIN
    SELECT 
	t.Id,
        t.Title, 
        t.Description, 
        t.DueDate, 
        p.Priority, 
        s.Status
    FROM 
        Tasks t
    LEFT JOIN 
        priority p ON t.PriorityId = p.id
    LEFT JOIN 
        status s ON t.StatusId = s.id
	where
		t.IsActive = 1
	order by 1 desc 
END;
GO
