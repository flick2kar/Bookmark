ALTER FUNCTION dbo.GetBooksByMember
( 
    @memberid VARCHAR(32) 
) 
RETURNS VARCHAR(8000) 
AS 
BEGIN 
    DECLARE @bookid VARCHAR(8000) 
SELECT @bookid = COALESCE(substring(@bookid,0,3) + ', ', '') + substring(bookid,0,3) FROM booktrans bt
where memberid=@memberid
    RETURN @bookid 
END 
GO 
