USE [CashCow]
GO

/****** Object:  StoredProcedure [dbo].[WatchListItem_Save]    Script Date: 06/10/2012 10:47:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WatchListItem_Save]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WatchListItem_Save]
GO

USE [CashCow]
GO

/****** Object:  StoredProcedure [dbo].[WatchListItem_Save]    Script Date: 06/10/2012 10:47:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[WatchListItem_Save]          
 @BseSymbol NVARCHAR(50),          
 @NseSymbol NVARCHAR(50),          
 @Name NVARCHAR(200),          
 @AltNameOne NVARCHAR(200),          
 @AltNameTwo NVARCHAR(200),          
 @AltNameThree NVARCHAR(200),          
 @TempName NVARCHAR(200),          
 @IsActive BIT,          
 @AlertRequired BIT,          
 @ModifiedOn DATETIME,          
 @CreatedOn DATETIME,          
 @WatchListID INT OUTPUT          
          
AS          
          
IF NOT EXISTS (SELECT 1 FROM WatchList w WHERE w.WatchListID = @WatchListID)        
 BEGIN        
  INSERT INTO WatchList          
  (BseSymbol, NseSymbol, Name, AltNameOne, AltNameTwo, AltNameThree, TempName, IsActive, AlertRequired, ModifiedOn, CreatedOn)          
  VALUES          
  (@BseSymbol, @NseSymbol, @Name, @AltNameOne, @AltNameTwo, @AltNameThree, @TempName, @IsActive, @AlertRequired, @ModifiedOn, @CreatedOn)          
            
  SELECT @WatchListID = SCOPE_IDENTITY();        
 END        
ELSE        
 BEGIN        
  UPDATE WatchList        
  SET        
   BseSymbol = @BseSymbol,        
   NseSymbol = @NseSymbol,        
   Name = @Name,        
   AltNameOne = @AltNameOne,        
   AltNameTwo = @AltNameTwo,        
   AltNameThree = @AltNameThree,        
   TempName = @TempName,        
   IsActive = @IsActive,        
   AlertRequired = @AlertRequired,        
   ModifiedOn = @ModifiedOn,        
   CreatedOn = @CreatedOn        
  WHERE        
   WatchListID = @WatchListID        
 END   
  
GO

