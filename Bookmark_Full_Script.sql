/****** Object:  UserDefinedFunction [dbo].[GetBooksByMember]    Script Date: 26-01-2018 10:35:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetBooksByMember]
( 
    @memberid VARCHAR(32) 
) 
RETURNS VARCHAR(8000) 
AS 
BEGIN 
    DECLARE @bookid VARCHAR(8000); 
    with CTE AS
    (SELECT distinct substring(bookid,0,3) bookid from booktrans where MemberID=@memberid)
SELECT @bookid = COALESCE(@bookid+ ', ', '') + bookid FROM CTE;
RETURN @bookid; 
END 

GO
/****** Object:  Table [dbo].[authors]    Script Date: 26-01-2018 10:35:54 ******/
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
/****** Object:  Table [dbo].[balance]    Script Date: 26-01-2018 10:35:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[balance](
	[BalanceID] [int] IDENTITY(1,1) NOT NULL,
	[Balance] [int] NOT NULL CONSTRAINT [DF_balance1_Balance_1]  DEFAULT ((0)),
	[BalDate] [date] NULL,
	[MemberID] [varchar](10) NULL,
	[CreatedDate] [datetime] NOT NULL CONSTRAINT [DF_balance1_CreatedDate_1]  DEFAULT (getdate()),
 CONSTRAINT [PK_balance1] PRIMARY KEY CLUSTERED 
(
	[BalanceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BookPurchase]    Script Date: 26-01-2018 10:35:54 ******/
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
/****** Object:  Table [dbo].[books]    Script Date: 26-01-2018 10:35:54 ******/
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
/****** Object:  Table [dbo].[booktrans]    Script Date: 26-01-2018 10:35:54 ******/
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
/****** Object:  Table [dbo].[category]    Script Date: 26-01-2018 10:35:54 ******/
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
/****** Object:  Table [dbo].[Fine]    Script Date: 26-01-2018 10:35:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Fine](
	[FineID] [int] IDENTITY(1,1) NOT NULL,
	[FineAmt] [int] NOT NULL CONSTRAINT [DF_Table_1_Balance]  DEFAULT ((0)),
	[FineDate] [date] NULL,
	[MemberID] [varchar](10) NULL,
	[CreatedDate] [datetime] NOT NULL CONSTRAINT [DF_Fine_CreatedDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_Fine] PRIMARY KEY CLUSTERED 
(
	[FineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LibAccount]    Script Date: 26-01-2018 10:35:54 ******/
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
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[members]    Script Date: 26-01-2018 10:35:54 ******/
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
/****** Object:  Table [dbo].[MemPrefBooks]    Script Date: 26-01-2018 10:35:54 ******/
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
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Series]    Script Date: 26-01-2018 10:35:54 ******/
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
/****** Object:  Table [dbo].[SubCategory]    Script Date: 26-01-2018 10:35:54 ******/
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
/****** Object:  Table [dbo].[WishList]    Script Date: 26-01-2018 10:35:54 ******/
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
/****** Object:  StoredProcedure [dbo].[GetAccountDetails]    Script Date: 26-01-2018 10:35:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAccountDetails]
	-- Add the parameters for the stored procedure here
	 @startdate as varchar(max),
	 @enddate as varchar(max)

AS
BEGIN
	declare @sqlQuery as varchar(max)
	declare @lendrate as int
	declare @fine as int
	declare @balance as int
	--set @sqlQuery='SELECT sum(lendrate) lendrate FROM booktrans bt where lenddate between '+ cast(@startdate as varchar(max)) +' and '+ cast(@enddate as varchar(max))
	SELECT @lendrate=sum(lendrate) FROM booktrans bt where lenddate between CONVERT(date,@startdate,103) and CONVERT(date,@enddate,103)
	SELECT @fine=sum(f.FineAmt) FROM fine f where f.FineDate between CONVERT(date,@startdate,103) and CONVERT(date,@enddate,103)
	SELECT @balance=sum(b.Balance) FROM balance b where b.BalDate between CONVERT(date,@startdate,103) and CONVERT(date,@enddate,103)

	select @lendrate lendrate,@fine fine,@balance balance
	--exec @lendrate=@sqlQuery 
	--select @lendrate

END

GO
/****** Object:  StoredProcedure [dbo].[GetSalesPrice]    Script Date: 26-01-2018 10:35:54 ******/
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
/****** Object:  StoredProcedure [dbo].[spCalculateFine]    Script Date: 26-01-2018 10:35:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spCalculateFine]
	-- Add the parameters for the stored procedure here
	@memberid as varchar(10),@bookid as varchar(10),@fineorrenewal as bit
	--,@finalfine as int=0 OUTPUT
AS
BEGIN
	declare @dayslate as int
	declare @duedate as date
	declare @cntbookid as int
	declare @CreatedDate as datetime
	declare @lenddays as decimal(4,2)=15
	declare @lendrateperday as decimal(4,2)
	declare @bookprice as int
	declare @finalfine as int=0

	--Get number of days late
	select @duedate=duedate,@lendrateperday=cast(LendRate/@lenddays as decimal(2)) from booktrans where bookid=@bookid and memberid=@memberid
	select @dayslate=datediff(day,@duedate,cast(getdate() as date))

	--Renewl or fine
	--Old or new book?
	SELECT @cntbookid=count(bookid) from booktrans where bookid=@bookid
	select @CreatedDate=CreatedDate,@bookprice=BookPrice from books where bookid=@bookid

	--  rented less than 3 times
	IF @cntbookid<3
	begin
	--not recently purchased
	if year(@CreatedDate)<year(GETDATE())-1 or @CreatedDate is null
		select @dayslate=case when @dayslate>@lenddays then @lenddays else @lenddays end
		--set @dayslate=15
	else
	--recently purchased
		select @dayslate=@dayslate
	end;

	-- rented more than 3 times
	IF @cntbookid>=3
	begin
	--not recently purchased
	if year(@CreatedDate)<year(GETDATE())-1 or @CreatedDate is null
		select @dayslate=@dayslate/2
	else
	--recently purchased
		select @dayslate=@dayslate
	end;
	
	--How many transactions?
	--Renewal - 50%
	--Fine - 75%
	--1 for renewal
	if @fineorrenewal=1
		select @finalfine=@dayslate*@lendrateperday*50/100 
	else
		select @finalfine=@dayslate*@lendrateperday*75/100 

	if @finalfine>@bookprice
		select @finalfine=@bookprice*0.75

	--print cast(@duedate as varchar(10))  +' - @duedate'
	--print cast(@CreatedDate as varchar(15)) +' - @CreatedDate'
	--print cast(@cntbookid as varchar(5)) +' - @cntbookid'
	--print cast(@lendrateperday as varchar(5))  +' - @lendrateperday'
	--print cast(@dayslate as varchar(5)) +' - @dayslate'
		
		select @finalfine finalfine
	--return @finalfine
		
		
END

GO
