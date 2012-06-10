USE [CashCow]
GO

/****** Object:  StoredProcedure [dbo].[WatchListItem_Delete]    Script Date: 06/10/2012 10:46:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WatchListItem_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WatchListItem_Delete]
GO

USE [CashCow]
GO

/****** Object:  StoredProcedure [dbo].[WatchListItem_Delete]    Script Date: 06/10/2012 10:46:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[WatchListItem_Delete]
@WatchListID INT    
          
AS          
    
DELETE FROM WatchList WHERE WatchListID = @WatchListID
GO

