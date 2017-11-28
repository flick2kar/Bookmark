USE [Bookmark_New]
GO
/****** Object:  Table [dbo].[authors]    Script Date: 28-11-2017 11:03:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[authors](
	[AuthorID] [int] NOT NULL,
	[AuthorName] [nvarchar](255) NULL,
 CONSTRAINT [PK_authors] PRIMARY KEY CLUSTERED 
(
	[AuthorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BookPurchase]    Script Date: 28-11-2017 11:03:28 ******/
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
/****** Object:  Table [dbo].[books]    Script Date: 28-11-2017 11:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[books](
	[BookID] [varchar](255) NOT NULL,
	[BookName] [nvarchar](255) NULL,
	[Soldprice] [int] NULL,
	[BookPrice] [int] NULL,
	[AuthorID] [int] NULL,
	[CategoryID] [int] NULL,
	[SubCategoryID] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[SeriesID] [int] NULL,
	[OrgPrice] [int] NULL,
	[Status] [int] NULL,
	[ISBN] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[BookID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[booktrans]    Script Date: 28-11-2017 11:03:28 ******/
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
	[TRANSID] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TRANSID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[category]    Script Date: 28-11-2017 11:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[CategoryID] [int] NOT NULL,
	[CategoryName] [nvarchar](255) NULL,
 CONSTRAINT [PK_category] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[members]    Script Date: 28-11-2017 11:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[members](
	[MemberID] [varchar](255) NOT NULL,
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
	[SortID] [int] NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MemberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Series]    Script Date: 28-11-2017 11:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Series](
	[SeriesID] [int] IDENTITY(1,1) NOT NULL,
	[SeriesName] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[SeriesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SubCategory]    Script Date: 28-11-2017 11:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubCategory](
	[Id] [int] NOT NULL,
	[CategoryID] [int] NULL,
	[Name] [nvarchar](255) NULL,
 CONSTRAINT [PK_SubCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WishList]    Script Date: 28-11-2017 11:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WishList](
	[WishID] [int] IDENTITY(1,1) NOT NULL,
	[MemberID] [varchar](255) NULL,
	[BookID] [varchar](255) NULL,
	[AuthorName] [varchar](255) NULL,
	[BookName] [varchar](255) NULL,
	[Status] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[GrantDate] [datetime] NULL,
 CONSTRAINT [PK_WishList] PRIMARY KEY CLUSTERED 
(
	[WishID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[GetSalesPrice]    Script Date: 28-11-2017 11:03:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetSalesPrice] 
	-- Add the parameters for the stored procedure here
	@bookid as varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @bookprice as int=0;
	declare @badbookrate as int=10;
	declare @goodbookrate as int=30;
	declare @cntbookid as int=0;
	declare @CreatedDate as datetime;
	declare @forsales as int=4;

	--List books active and for sales
	if(select status from books where bookid=@bookid) !=@forsales
	begin

	select @bookprice=bookprice,@CreatedDate=CreatedDate from books where bookid=@bookid
	SELECT @cntbookid=count(bookid) from booktrans where bookid=@bookid

    --  rented less than 3 times
	IF @cntbookid<3
	begin
	--not recently purchased
	if @CreatedDate<year(GETDATE())-1 or @CreatedDate is null
		select cast(@badbookrate as varchar(10))+','+cast(@goodbookrate  as varchar(10)) saleprice
	else
	--recently purchased
		select cast(@bookprice*20/100 as varchar(10))+','+cast(@bookprice*80/100 as varchar(10)) saleprice
	end;

	-- rented more than 3 times
	IF @cntbookid>=3
	begin
	--not recently purchased
	if @CreatedDate<year(GETDATE())-1 or @CreatedDate is null
		select cast(@badbookrate as varchar(10))+','+cast(@goodbookrate*1.5  as varchar(10)) saleprice
	else
	--recently purchased
		select cast(@bookprice*30/100 as varchar(10))+','+cast(@bookprice*80/100 as varchar(10)) saleprice
	end;
	end;
	ELSE
		select 'NOVAL' saleprice
END

GO
