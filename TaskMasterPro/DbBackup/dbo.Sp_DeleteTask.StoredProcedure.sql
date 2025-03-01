USE [TaskMasterPro]
GO
/****** Object:  StoredProcedure [dbo].[Sp_DeleteTask]    Script Date: 31-01-2025 12:07:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Sp_DeleteTask]
    @id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Check if task exists and is active
    IF EXISTS (SELECT 1 FROM Tasks WHERE Id = @id AND IsActive = 1)
    BEGIN
        -- Soft delete by updating IsActive
        UPDATE Tasks 
        SET IsActive = 0
        WHERE Id = @id;

        -- Return the updated task id
        SELECT @id AS Id;
        RETURN 0;
    END
    ELSE
    BEGIN
        -- Task not found or already deleted
        SELECT 0 AS Id;
        RETURN 1;
    END
END
GO
