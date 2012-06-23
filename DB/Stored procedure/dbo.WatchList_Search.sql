USE [CashCow]
GO

/****** Object:  StoredProcedure [dbo].[WatchList_Search]    Script Date: 06/23/2012 14:21:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WatchList_Search]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[WatchList_Search]
GO

USE [CashCow]
GO

/****** Object:  StoredProcedure [dbo].[WatchList_Search]    Script Date: 06/23/2012 14:21:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[WatchList_Search]              
@WatchListID INT,
@StartRowIndex INT,
@MaximumRows INT,              
@SortColumn VARCHAR(50),              
@SortAscending BIT,              
@SearchCriteria XML,
@TextSearchKey NVARCHAR(200),
@SearchAgainst BIT,              
@SearchWithOr BIT,              
@RecordCount INT OUTPUT              
              
AS                
BEGIN                
	SET NOCOUNT ON;                
	                
	-- Set default sort column is nothing is specified.              
	IF ISNULL(@SortColumn,'') NOT IN ('BseSymbol','NseSymbol','Name','AltNameOne',              
	'AltNameTwo','AltNameThree','TempName','IsActive','AlertRequired','ModifiedOn','CreatedOn')                
		SET @SortColumn = 'Name';              
	                   
	-- Create the result table              
	IF OBJECT_ID('tempdb..#TempResults','U') IS NOT NULL                
		DROP TABLE #TempResults;              
	                  
	CREATE TABLE #TempResults(              
	WatchListID INT,              
	BseSymbol NVARCHAR(50),              
	NseSymbol NVARCHAR(50),              
	Name NVARCHAR(200),              
	AltNameOne NVARCHAR(200),              
	AltNameTwo NVARCHAR(200),              
	AltNameThree NVARCHAR(200),              
	TempName NVARCHAR(200),              
	IsActive BIT,              
	AlertRequired BIT,              
	ModifiedOn DATETIME,              
	CreatedOn DATETIME              
	);              
	
	-- Create the final result table              
	IF OBJECT_ID('tempdb..#TempFinalResults','U') IS NOT NULL                
		DROP TABLE #TempFinalResults;              
	                  
	CREATE TABLE #TempFinalResults(              
	WatchListID INT,              
	BseSymbol NVARCHAR(50),              
	NseSymbol NVARCHAR(50),              
	Name NVARCHAR(200),              
	AltNameOne NVARCHAR(200),              
	AltNameTwo NVARCHAR(200),              
	AltNameThree NVARCHAR(200),              
	TempName NVARCHAR(200),              
	IsActive BIT,              
	AlertRequired BIT,              
	ModifiedOn DATETIME,              
	CreatedOn DATETIME              
	);
               
	-- Create the search criteria table.              
	IF OBJECT_ID('tempdb..#TempSearchCriteria','U') IS NOT NULL                
		DROP TABLE #TempSearchCriteria;
	                  
	CREATE TABLE #TempSearchCriteria(              
	SearchOn VARCHAR(50),              
	SearchValue NVARCHAR(200)              
	);              
              
	-- Insert data into #TempSearchCriteria table.              
	INSERT INTO #TempSearchCriteria
		(SearchOn, SearchValue)              
		SELECT              
			SearchCriteria.Criteria.value('@SearchOn','VARCHAR(50)'),              
			SearchCriteria.Criteria.value('@SearchValue','VARCHAR(200)')              
		FROM
			@SearchCriteria.nodes('/SearchCriteria/Criteria') as SearchCriteria(Criteria)              
              
	DECLARE @SearchCriteriaCount INT              
	SET @SearchCriteriaCount = (SELECT COUNT(*) FROM #TempSearchCriteria);              
                  
	-- Fill the #TempResults table with filtered data.              
	-- Fetch particular watchlist record if @WatchListID is specified i.e. > 0
	IF @WatchListID > 0
		BEGIN
			INSERT INTO #TempResults              
				(WatchListID,BseSymbol,NseSymbol,Name,AltNameOne,AltNameTwo,AltNameThree,              
				TempName,IsActive,AlertRequired,ModifiedOn,CreatedOn)               
				SELECT              
					WatchListID,BseSymbol,NseSymbol,Name,AltNameOne,AltNameTwo,AltNameThree,              
					TempName,IsActive,AlertRequired,ModifiedOn,CreatedOn              
				FROM
					WatchList
				WHERE
					WatchListID = @WatchListID
		END
	-- Consider all records if search criteria is not specified.              
	ELSE IF @SearchCriteriaCount = 0              
		BEGIN              
			INSERT INTO #TempResults              
				(WatchListID,BseSymbol,NseSymbol,Name,AltNameOne,AltNameTwo,AltNameThree,              
				TempName,IsActive,AlertRequired,ModifiedOn,CreatedOn)               
				SELECT              
					WatchListID,BseSymbol,NseSymbol,Name,AltNameOne,AltNameTwo,AltNameThree,              
					TempName,IsActive,AlertRequired,ModifiedOn,CreatedOn              
				FROM
					WatchList
		END              
	-- Since search criteria is specified, use it.              
	ELSE              
		BEGIN              
			INSERT INTO #TempResults              
			  (WatchListID,BseSymbol,NseSymbol,Name,AltNameOne,AltNameTwo,AltNameThree,              
			   TempName,IsActive,AlertRequired,ModifiedOn,CreatedOn)               
			SELECT              
				WL.WatchListID,WL.BseSymbol,WL.NseSymbol,WL.Name,WL.AltNameOne,WL.AltNameTwo,              
				WL.AltNameThree,WL.TempName,WL.IsActive,WL.AlertRequired,WL.ModifiedOn,WL.CreatedOn              
			FROM
				WatchList AS WL              
			WHERE
				1 = (
				CASE              
					-- Connect search criterias with OR and Search as per search criteria
					WHEN              
						@SearchWithOr = 1 AND              
						@SearchAgainst = 0 AND              
						EXISTS (
						SELECT
							*
						FROM
							#TempSearchCriteria TC
						WHERE
							1 =  
							CASE TC.SearchOn  
								WHEN 'BseSymbol' THEN CASE WHEN WL.BseSymbol LIKE '%' + TC.SearchValue + '%' THEN 1 END
								WHEN 'NseSymbol' THEN CASE WHEN WL.NseSymbol LIKE '%' + TC.SearchValue + '%' THEN 1 END
								WHEN 'Name' THEN CASE WHEN WL.Name LIKE '%' + TC.SearchValue + '%' THEN 1 END  
								WHEN 'AltNameOne' THEN CASE WHEN WL.AltNameOne LIKE '%' + TC.SearchValue + '%' THEN 1 END
								WHEN 'AltNameTwo' THEN CASE WHEN WL.AltNameTwo LIKE '%' + TC.SearchValue + '%' THEN 1 END
								WHEN 'AltNameThree' THEN CASE WHEN WL.AltNameThree LIKE '%' + TC.SearchValue + '%' THEN 1 END
								WHEN 'TempName' THEN CASE WHEN WL.TempName LIKE '%' + TC.SearchValue + '%' THEN 1 END
								WHEN 'IsActive' THEN CASE WHEN TC.SearchValue = CAST(WL.IsActive AS NVARCHAR(200)) THEN 1 END
								WHEN 'AlertRequired' THEN CASE WHEN TC.SearchValue = CAST(WL.AlertRequired AS NVARCHAR(200)) THEN 1 END
								WHEN 'ModifiedOn' THEN CASE WHEN TC.SearchValue = CAST(WL.ModifiedOn AS NVARCHAR(200)) THEN 1 END
								WHEN 'CreatedOn' THEN CASE WHEN TC.SearchValue = CAST(WL.CreatedOn AS NVARCHAR(200)) THEN 1 END
							END)
					THEN              
						1              
					-- Connect search criterias with OR and Search against search criteria              
					WHEN              
						@SearchWithOr = 1 AND              
						@SearchAgainst = 1 AND              
						NOT EXISTS (
						SELECT
							*
						FROM
							#TempSearchCriteria TC
						WHERE
							1 =  
							CASE TC.SearchOn  
								WHEN 'BseSymbol' THEN CASE WHEN WL.BseSymbol LIKE '%' + TC.SearchValue + '%' THEN 1 END
								WHEN 'NseSymbol' THEN CASE WHEN WL.NseSymbol LIKE '%' + TC.SearchValue + '%' THEN 1 END
								WHEN 'Name' THEN CASE WHEN WL.Name LIKE '%' + TC.SearchValue + '%' THEN 1 END  
								WHEN 'AltNameOne' THEN CASE WHEN WL.AltNameOne LIKE '%' + TC.SearchValue + '%' THEN 1 END
								WHEN 'AltNameTwo' THEN CASE WHEN WL.AltNameTwo LIKE '%' + TC.SearchValue + '%' THEN 1 END
								WHEN 'AltNameThree' THEN CASE WHEN WL.AltNameThree LIKE '%' + TC.SearchValue + '%' THEN 1 END
								WHEN 'TempName' THEN CASE WHEN WL.TempName LIKE '%' + TC.SearchValue + '%'THEN 1 END
								WHEN 'IsActive' THEN CASE WHEN TC.SearchValue = CAST(WL.IsActive AS NVARCHAR(200)) THEN 1 END
								WHEN 'AlertRequired' THEN CASE WHEN TC.SearchValue = CAST(WL.AlertRequired AS NVARCHAR(200)) THEN 1 END
								WHEN 'ModifiedOn' THEN CASE WHEN TC.SearchValue = CAST(WL.ModifiedOn AS NVARCHAR(200)) THEN 1 END
								WHEN 'CreatedOn' THEN CASE WHEN TC.SearchValue = CAST(WL.CreatedOn AS NVARCHAR(200)) THEN 1 END
							END)
					THEN              
						1              
					-- Connect search criterias with AND and Search as per search criteria              
					WHEN             
						@SearchWithOr = 0 AND              
						@SearchAgainst = 0 AND  
						@SearchCriteriaCount = (
						SELECT
							COUNT(*)
						FROM
							#TempSearchCriteria TC
						WHERE
							1 =  
							CASE TC.SearchOn  
								WHEN 'BseSymbol' THEN CASE WHEN WL.BseSymbol LIKE '%' + TC.SearchValue + '%' THEN 1 END
								WHEN 'NseSymbol' THEN CASE WHEN WL.NseSymbol LIKE '%' + TC.SearchValue + '%' THEN 1 END
								WHEN 'Name' THEN CASE WHEN WL.Name LIKE '%' + TC.SearchValue + '%' THEN 1 END  
								WHEN 'AltNameOne' THEN CASE WHEN WL.AltNameOne LIKE '%' + TC.SearchValue + '%' THEN 1 END
								WHEN 'AltNameTwo' THEN CASE WHEN WL.AltNameTwo LIKE '%' + TC.SearchValue + '%' THEN 1 END
								WHEN 'AltNameThree' THEN CASE WHEN WL.AltNameThree LIKE '%' + TC.SearchValue + '%' THEN 1 END
								WHEN 'TempName' THEN CASE WHEN WL.TempName LIKE '%' + TC.SearchValue + '%' THEN 1 END
								WHEN 'IsActive' THEN CASE WHEN TC.SearchValue = CAST(WL.IsActive AS NVARCHAR(200)) THEN 1 END
								WHEN 'AlertRequired' THEN CASE WHEN TC.SearchValue = CAST(WL.AlertRequired AS NVARCHAR(200)) THEN 1 END
								WHEN 'ModifiedOn' THEN CASE WHEN TC.SearchValue = CAST(WL.ModifiedOn AS NVARCHAR(200)) THEN 1 END
								WHEN 'CreatedOn' THEN CASE WHEN TC.SearchValue = CAST(WL.CreatedOn AS NVARCHAR(200)) THEN 1 END
							END)
					THEN      
						1      
					-- Connect search criterias with AND and Search against search criteria              
					WHEN					
						@SearchWithOr = 0 AND              
						@SearchAgainst = 1 AND              
						@SearchCriteriaCount <> (
						SELECT
							COUNT(*)
						FROM
							#TempSearchCriteria TC
						WHERE
							1 =  
							CASE TC.SearchOn  
								WHEN 'BseSymbol' THEN CASE WHEN WL.BseSymbol LIKE '%' + TC.SearchValue + '%' THEN 1 END
								WHEN 'NseSymbol' THEN CASE WHEN WL.NseSymbol LIKE '%' + TC.SearchValue + '%' THEN 1 END
								WHEN 'Name' THEN CASE WHEN WL.Name LIKE '%' + TC.SearchValue + '%' THEN 1 END  
								WHEN 'AltNameOne' THEN CASE WHEN WL.AltNameOne LIKE '%' + TC.SearchValue + '%' THEN 1 END
								WHEN 'AltNameTwo' THEN CASE WHEN WL.AltNameTwo LIKE '%' + TC.SearchValue + '%' THEN 1 END
								WHEN 'AltNameThree' THEN CASE WHEN WL.AltNameThree LIKE '%' + TC.SearchValue + '%' THEN 1 END
								WHEN 'TempName' THEN CASE WHEN WL.TempName LIKE '%' + TC.SearchValue + '%' THEN 1 END
								WHEN 'IsActive' THEN CASE WHEN TC.SearchValue = CAST(WL.IsActive AS NVARCHAR(200)) THEN 1 END
								WHEN 'AlertRequired' THEN CASE WHEN TC.SearchValue = CAST(WL.AlertRequired AS NVARCHAR(200)) THEN 1 END
								WHEN 'ModifiedOn' THEN CASE WHEN TC.SearchValue = CAST(WL.ModifiedOn AS NVARCHAR(200)) THEN 1 END
								WHEN 'CreatedOn' THEN CASE WHEN TC.SearchValue = CAST(WL.CreatedOn AS NVARCHAR(200)) THEN 1 END
							END)
					THEN              
						1              
					ELSE              
						0              
				END)               
		END              
              
	-- Now perform a text search on the result obtained and insert it in the final result table.
	IF @TextSearchKey IS NOT NULL AND @TextSearchKey != ''
		BEGIN
			INSERT INTO #TempFinalResults
				(WatchListID,BseSymbol,NseSymbol,Name,AltNameOne,AltNameTwo,AltNameThree,              
				TempName,IsActive,AlertRequired,ModifiedOn,CreatedOn)               
				SELECT              
					TR.WatchListID, TR.BseSymbol, TR.NseSymbol, TR.Name, TR.AltNameOne, TR.AltNameTwo,
					TR.AltNameThree, TR.TempName, TR.IsActive, TR.AlertRequired, TR.ModifiedOn, TR.CreatedOn              
				FROM
					#TempResults AS TR
				WHERE
					TR.BseSymbol LIKE '%' + @TextSearchKey + '%' OR
					TR.NseSymbol LIKE '%' + @TextSearchKey + '%' OR
					TR.Name LIKE '%' + @TextSearchKey + '%' OR
					TR.AltNameOne LIKE '%' + @TextSearchKey + '%' OR
					TR.AltNameTwo LIKE '%' + @TextSearchKey + '%' OR
					TR.AltNameThree LIKE '%' + @TextSearchKey + '%' OR
					TR.TempName LIKE '%' + @TextSearchKey + '%'
					
		END
	ELSE
		BEGIN
			INSERT INTO #TempFinalResults
				(WatchListID,BseSymbol,NseSymbol,Name,AltNameOne,AltNameTwo,AltNameThree,              
				TempName,IsActive,AlertRequired,ModifiedOn,CreatedOn)               
				SELECT              
					TR.WatchListID, TR.BseSymbol, TR.NseSymbol, TR.Name, TR.AltNameOne, TR.AltNameTwo,
					TR.AltNameThree, TR.TempName, TR.IsActive, TR.AlertRequired, TR.ModifiedOn, TR.CreatedOn              
				FROM
					#TempResults AS TR
		END
	
	-- Set the record counts.                  
	SELECT @RecordCount = COUNT(*) FROM #TempFinalResults;              
              
	-- If max row has been defined as 0 then all search record has to be returned.              
	-- Hence set @StartRowIndex to 0 and @MaximumRows to @RecordCount.              
	IF @MaximumRows = 0              
		BEGIN              
			SET @StartRowIndex = 0;              
			SET @MaximumRows = @RecordCount;              
		END              
                
	-- Construct query so as to select from #TempResults based on sorting and paging info.
	DECLARE @SQLCommand nvarchar(500);                 
	SET @SQLCommand = 'SELECT s.*' + CHAR(13) + CHAR(10) +                
		'FROM ( SELECT ROW_NUMBER() OVER (ORDER BY [' + @SortColumn + '] ' +                 
		CASE WHEN @SortAscending = 1 THEN 'ASC' ELSE 'DESC' END+ ') AS RowIndex, *' + CHAR(13) + CHAR(10) +                
		'   FROM #TempFinalResults) s' + CHAR(13) + CHAR(10) +                
		'WHERE s.RowIndex BETWEEN ' + CAST(@StartRowIndex + 1 AS varchar(20)) + ' AND ' + CAST(@StartRowIndex + @MaximumRows AS varchar(20)) + CHAR(13) + CHAR(10) +                
		'ORDER BY s.RowIndex;' + CHAR(13) + CHAR(10);                

	-- Execute the query constructed.
	EXEC (@SQLCommand);                
    
    -- Finally drop all temporary tables.
    IF OBJECT_ID('tempdb..#TempResults','U') IS NOT NULL                
		DROP TABLE #TempResults;
		
	IF OBJECT_ID('tempdb..#TempFinalResults','U') IS NOT NULL                
		DROP TABLE #TempFinalResults;
		
	IF OBJECT_ID('tempdb..#TempSearchCriteria','U') IS NOT NULL                
		DROP TABLE #TempSearchCriteria;             
END 
GO

