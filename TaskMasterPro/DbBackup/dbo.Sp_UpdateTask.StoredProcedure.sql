USE [TaskMasterPro]
GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdateTask]    Script Date: 31-01-2025 12:07:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Sp_UpdateTask]
    @Id INT,                -- Accept Task ID to identify which task to update
    @Title NVARCHAR(255) = null,       -- Accept Title for the task
    @Description NVARCHAR(1000) = null,-- Accept Description for the task
    @DueDate DATE = null,          -- Accept DueDate for the task
    @Priority NVARCHAR(50)= null,     -- Accept Priority as string (e.g., 'low', 'medium', 'high')
    @Status NVARCHAR(50) = null        -- Accept Status as string (e.g., 'pending', 'inprogress')
AS
BEGIN
    DECLARE @PriorityId INT;
    DECLARE @StatusId INT;
	DECLARE @IsActive BIT;

    -- Check if the given Task ID exists and is active (not deleted)
    SELECT @IsActive = IsActive FROM Tasks WHERE Id = @Id;
	  -- Check if the task is deleted
    IF @IsActive = 0
    BEGIN
        RAISERROR('Task is marked as deleted. Update not allowed.', 16, 1);
        RETURN;
    END
    ELSE
	BEGIN
    -- Map the @Priority string to corresponding PriorityId integer
    SET @PriorityId = CASE 
                        WHEN @Priority = 'Low' THEN 1
                        WHEN @Priority = 'Medium' THEN 2
                        WHEN @Priority = 'High' THEN 3
                        ELSE NULL -- Handle invalid priority values
                     END;

    -- Check if the priority is valid
    IF @PriorityId IS NULL
    BEGIN
        RAISERROR('Invalid priority value. Allowed values are "low", "medium", or "high".', 16, 1);
        RETURN;
    END

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
    SET Title = @Title,
        Description = @Description,
        DueDate = @DueDate,
        PriorityId = @PriorityId,
        StatusId = @StatusId
    WHERE id = @Id;

    -- Optionally, return the updated task ID or any other relevant information
    SELECT @Id AS UpdatedTaskId;
	END
END;
GO
