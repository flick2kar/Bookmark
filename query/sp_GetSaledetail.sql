-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
alter PROCEDURE GetSalesPrice 
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
