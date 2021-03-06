USE [Bookmark_New]
GO
/****** Object:  Table [dbo].[SubCategory]    Script Date: 10/14/2014 09:33:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubCategory](
	[Id] [int] NOT NULL,
	[CategoryID] [int] NULL,
	[Name] [nvarchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Series]    Script Date: 10/14/2014 09:33:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Series](
	[SeriesID] [int] IDENTITY(1,1) NOT NULL,
	[SeriesName] [varchar](max) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MemPrefBooks]    Script Date: 10/14/2014 09:33:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MemPrefBooks](
	[MemberID] [varchar](50) NULL,
	[Doj] [datetime] NULL,
	[Lenddate] [datetime] NULL,
	[Status] [bit] NULL,
	[PrefBooks] [varchar](max) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[members]    Script Date: 10/14/2014 09:33:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[members](
	[ID] [int] NOT NULL,
	[MemberID] [nvarchar](255) NULL,
	[MemberName] [nvarchar](255) NULL,
	[Address] [nvarchar](255) NULL,
	[Notes] [nvarchar](255) NULL,
	[Doj] [datetime] NULL,
	[Status] [int] NULL,
	[Mobile] [nvarchar](255) NULL,
	[Landline] [nvarchar](50) NULL,
	[Email] [nvarchar](255) NULL,
	[Amount] [int] NULL,
	[MemberType] [int] NULL,
	[SortID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LibAccount]    Script Date: 10/14/2014 09:33:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LibAccount](
	[AccountDate] [date] NULL,
	[Category] [varchar](max) NULL,
	[Expense] [int] NULL,
	[Income] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[category]    Script Date: 10/14/2014 09:33:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[CategoryID] [int] NOT NULL,
	[CategoryName] [nvarchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[booktrans]    Script Date: 10/14/2014 09:33:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[booktrans](
	[BookID] [nvarchar](255) NULL,
	[MemberID] [nvarchar](255) NULL,
	[LendDate] [datetime] NULL,
	[DueDate] [datetime] NULL,
	[ReturnDate] [datetime] NULL,
	[LendRate] [int] NULL,
	[Fine] [int] NULL,
	[Balance] [nvarchar](255) NULL,
	[RenewalDays] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[LibBal] [int] NULL,
	[MemBal] [int] NULL,
	[TRANSID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[books]    Script Date: 10/14/2014 09:33:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[books](
	[BookID] [nvarchar](255) NULL,
	[BookName] [nvarchar](255) NULL,
	[LendRate] [float] NULL,
	[BookPrice] [float] NULL,
	[AuthorID] [float] NULL,
	[CategoryID] [float] NULL,
	[SubCategoryID] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[SeriesID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookPurchase]    Script Date: 10/14/2014 09:33:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BookPurchase](
	[BookID] [varchar](50) NOT NULL,
	[OPrice] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[authors]    Script Date: 10/14/2014 09:33:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[authors](
	[AuthorID] [int] NOT NULL,
	[AuthorName] [nvarchar](255) NULL
) ON [PRIMARY]
GO
