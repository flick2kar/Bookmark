select * from books order by createddate desc
sp_rename 'books.bookid','bookcode','COLUMN'
ALTER TABLE books ADD  ID INT IDENTITY(1,1)
select * from books1011
select * from category

delete from category where categoryid in (4,8,14,17,21,22,29,30,27,28,33,34)

ALTER TABLE books ALTER COLUMN bookcode varchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS

ALTER TABLE books ALTER COLUMN BookName varchar(510) COLLATE SQL_Latin1_General_CP1_CI_AS

ALTER TABLE authors ALTER COLUMN authorname nvarchar(510) COLLATE SQL_Latin1_General_CP1_CI_AS

ALTER TABLE category ALTER COLUMN MetaKeywords nvarchar(800) COLLATE SQL_Latin1_General_CP1_CI_AS

ALTER TABLE [UrlRecord] ALTER COLUMN EntityName nvarchar(800) COLLATE SQL_Latin1_General_CP1_CI_AS

ALTER TABLE [UrlRecord] ALTER COLUMN slug nvarchar(800) COLLATE SQL_Latin1_General_CP1_CI_AS

INSERT INTO [ecrbookl_nop].[dbo].[UrlRecord]
           ([EntityId]
           ,[EntityName]
           ,[Slug]
           ,[IsActive]
           ,[LanguageId])
select [EntityId]
           ,[EntityName]
           ,[Slug]
           ,[IsActive]
           ,[LanguageId] from #url from [UrlRecord]
delete from [UrlRecord]

select * from #url