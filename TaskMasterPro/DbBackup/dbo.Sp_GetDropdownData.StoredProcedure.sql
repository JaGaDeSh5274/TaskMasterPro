USE [TaskMasterPro]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetDropdownData]    Script Date: 31-01-2025 12:07:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[Sp_GetDropdownData]
AS
BEGIN
     -- Select data from the first table
    SELECT Id,
	Status FROM status;

    -- Select data from the second table
    SELECT Id,Priority FROM priority;
END;
GO
