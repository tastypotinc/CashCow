USE [CashCow]
GO

/****** Object:  Table [dbo].[WatchList]    Script Date: 06/10/2012 10:45:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WatchList]') AND type in (N'U'))
DROP TABLE [dbo].[WatchList]
GO

USE [CashCow]
GO

/****** Object:  Table [dbo].[WatchList]    Script Date: 06/10/2012 10:45:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[WatchList](
	[WatchListID] [int] IDENTITY(1,1) NOT NULL,
	[BseSymbol] [nvarchar](50) NULL,
	[NseSymbol] [nvarchar](50) NULL,
	[Name] [nvarchar](200) NULL,
	[AltNameOne] [nvarchar](200) NULL,
	[AltNameTwo] [nvarchar](200) NULL,
	[AltNameThree] [nvarchar](200) NULL,
	[TempName] [nvarchar](200) NULL,
	[IsActive] [bit] NULL,
	[AlertRequired] [bit] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_WatchList] PRIMARY KEY CLUSTERED 
(
	[WatchListID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

