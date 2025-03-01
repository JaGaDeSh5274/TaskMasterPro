USE [TaskMasterPro]
GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdateTaskStatus]    Script Date: 31-01-2025 12:07:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Sp_UpdateTaskStatus]
    @Id INT,                
    @Status NVARCHAR(50) = null   
AS
BEGIN

    DECLARE @StatusId INT;
	DECLARE @IsActive BIT;
	DECLARE @PendingStatus NVARCHAR(50);

    -- Check if the given Task ID exists and is active (not deleted)
    SELECT @IsActive = IsActive , @PendingStatus = statusId FROM Tasks WHERE Id = @Id

	  -- Check if the task is deleted
    IF @IsActive = 0
    BEGIN
        RAISERROR('Task is marked as deleted. Update not allowed.', 16, 1);
        RETURN;
    END
    ELSE IF  @PendingStatus =1
	BEGIN
    -- Map the @Status string to corresponding StatusId integer
    SET @StatusId = CASE
                        WHEN @Status = 'Pending' THEN 1
                        WHEN @Status = 'In Progress' THEN 2
						WHEN @Status = 'Completed' THEN 3
                        ELSE NULL -- Handle invalid status values
                    END;

    -- Check if the status is valid
    IF @StatusId IS NULL
    BEGIN
        RAISERROR('Invalid status value. Allowed values are "pending" or "inprogress"or "Completed".', 16, 1);
        RETURN;
    END

    -- Update the task in the Tasks table
    UPDATE Tasks
    SET 
        StatusId = 3
    WHERE id = @Id;

    -- Optionally, return the updated task ID or any other relevant information
    SELECT @Id AS UpdatedTaskId;
	END
	ELSE
	BEGIN
        RAISERROR('Invalid status. Only Pending status Tasks are allowed to update".', 16, 1);
        RETURN;
    END
END;


BACKUP DATABASE TaskMasterPro
TO DISK = 'D:\Project\TaskMasterPro.bak'
WITH FORMAT, INIT,
NAME = 'Full Backup of TaskMasterPro',
STATS = 10;
GO
