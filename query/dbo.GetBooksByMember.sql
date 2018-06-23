ALTER FUNCTION dbo.GetBooksByMember
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