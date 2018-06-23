--Sync with online website
SELECT [AuthorID]
      ,[AuthorName] into #authors
  FROM [dbo].[authors]
except
SELECT [AuthorID]
      ,[AuthorName]
  FROM [120.138.8.94, 14433].[ecrbookl_booksdb].[dbo].[authors]

  delete from [120.138.8.94, 14433].[ecrbookl_booksdb].[dbo].[authors] where authorid in
  (select authorid from #authors)

  insert into [120.138.8.94, 14433].[ecrbookl_booksdb].[dbo].[authors] select [AuthorID]
      ,[AuthorName] from #authors
GO



SELECT [BookID]
      ,[BookName]
      ,[LendRate]
      ,[BookPrice]
      ,[AuthorID]
      ,[CategoryID]
      ,[SubCategoryID]
      ,[CreatedDate]
      ,[SeriesID]
      ,[OrgPrice] into #local   
  FROM [dbo].[books] 
  except
SELECT [bookcode]
      ,[BookName]
      ,[LendRate]
      ,[BookPrice]
      ,[AuthorID]
      ,[CategoryID]
      ,[SubCategoryID]
      ,[CreatedDate]
      ,[SeriesID]
      ,[OrgPrice]
      
  FROM [120.138.8.94, 14433].[ecrbookl_booksdb].[dbo].[books]

  delete from [120.138.8.94, 14433].[ecrbookl_booksdb].[dbo].[books] where bookcode in
  (select bookid from #local)

  insert into [120.138.8.94, 14433].[ecrbookl_booksdb].[dbo].[books] ([bookcode]
      ,[BookName]
      ,[LendRate]
      ,[BookPrice]
      ,[AuthorID]
      ,[CategoryID]
      ,[SubCategoryID]
      ,[CreatedDate]
      ,[SeriesID]
      ,[OrgPrice]) select [BookID]
      ,[BookName]
      ,[LendRate]
      ,[BookPrice]
      ,[AuthorID]
      ,[CategoryID]
      ,[SubCategoryID]
      ,[CreatedDate]
      ,[SeriesID]
      ,[OrgPrice] from #local
  
GO


