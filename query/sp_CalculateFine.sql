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
Alter PROCEDURE [dbo].spCalculateFine
	-- Add the parameters for the stored procedure here
	@memberid as varchar(10),@bookid as varchar(10),@fineorrenewal as bit,@finalfine as int=0 OUTPUT
AS
BEGIN
	declare @dayslate as int
	declare @duedate as date
	declare @cntbookid as int
	declare @CreatedDate as datetime
	declare @lenddays as decimal(4,2)=15
	declare @lendrateperday as decimal(4,2)
	declare @bookprice as int
	

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
		

	return @finalfine
		
		
END
GO
