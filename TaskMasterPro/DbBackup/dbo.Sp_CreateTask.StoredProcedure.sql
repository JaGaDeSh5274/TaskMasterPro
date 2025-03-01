USE [TaskMasterPro]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CreateTask]    Script Date: 31-01-2025 12:07:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Sp_CreateTask]
    @Title NVARCHAR(255),
    @Description NVARCHAR(1000),
    @DueDate DATE,
    @Priority VARCHAR(50),
    @Status VARCHAR(50)
AS
BEGIN
 DECLARE @PriorityId INT;
 DECLARE @StatusId INT;
 DECLARE @NewTaskId INT;

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
        RETURN -1;
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
        RAISERROR('Invalid status value. Allowed values are "pending" or "inprogress" or "Completed".', 16, 1);
        RETURN -2;
    END

    -- Insert the new task into the Tasks table
    INSERT INTO Tasks (Title, Description, DueDate, PriorityId, StatusId)
    VALUES (@Title, @Description, @DueDate, @PriorityId, @StatusId);

   -- Get the newly created Task Id and set the OUTPUT parameter
   SET @NewTaskId = SCOPE_IDENTITY();
  -- Optional: Return 0 if successful
   SELECT @NewTaskId as Id;
   RETURN 0;
END;


GO
