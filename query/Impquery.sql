--Risk Books ignore this
select MAX(memberid),sum(lendrate),MAX(bt.duedate) from booktrans bt
where bt.returndate is null and MemberID like 'B%' 
group by memberid having sum(lendrate)>40 
UNION
select MAX(memberid),sum(lendrate),MAX(bt.duedate) from booktrans bt
where bt.returndate is null and MemberID like 'C%'
group by memberid having sum(lendrate)>50 
UNION
select MAX(memberid),sum(lendrate),MAX(bt.duedate) from booktrans bt
where bt.returndate is null and MemberID like 'I%' 
group by memberid having sum(lendrate)>60 
UNION
select MAX(memberid),sum(lendrate),MAX(bt.duedate) from booktrans bt
where bt.returndate is null and MemberID like 'F%' --and 
group by memberid having sum(lendrate)>70 
order by sum(lendrate) desc

--Sheet1:Risk Books - Change date
SELECT max(bt.memberid) memberid,max(membername) membername,MAX(m.Mobile) Mobile,bt.BookID,MAX(b.BookName) BookName,MAX(a.AuthorName) AuthorName,max(DueDate) Duedate,max(bt.lendrate) lendrate,max(m.Status ) status
--into #memdet
from booktrans bt
inner join books b on b.BookID=bt.BookID
inner join members m on m.memberid=bt.memberid
left join authors a on a.AuthorID=b.AuthorID
where bt.ReturnDate is null and bt.DueDate<'12/15/2017'
group by bt.bookid order by max(bt.lendrate) desc

select m.memberid,MAX(bt.lenddate) from #memdet m
inner join booktrans bt on bt.memberid=m.memberid
group by m.memberid

--Max_Lend_Authors
select max(bookname),max(authorname),bt.bookid,count(bt.bookid),SUM(bt.lendrate),MAX(bt.lenddate) from booktrans bt
inner join books b on b.bookid=bt.bookid
left join authors a on a.authorid=b.authorid
where bt.BookID not like 'tm%'
--where AuthorName like '%samit%' 
group by bt.bookid having count(bt.bookid)>10 
union
select max(bookname),max(authorname),bt.bookid,count(bt.bookid),SUM(bt.lendrate),MAX(bt.lenddate) from booktrans bt
inner join books b on b.bookid=bt.bookid
left join authors a on a.authorid=b.authorid
where bt.BookID like 'tm%'
--where AuthorName like '%samit%' 
group by bt.bookid having count(bt.bookid)>30 
order by count(bt.bookid) desc

--New Book Rotation
select max(bookname),max(authorname),bt.bookid,count(bt.bookid),max(oprice),sum(bt.lendrate),MAX(b.CreatedDate),MAX(LENDDATE) from booktrans bt
inner join books b on b.bookid=bt.bookid
--inner join BookPurchase p on b.BookID=p.BookID
left join authors a on a.authorid=b.authorid
WHERE b.CreatedDate between '01/01/2017' and '12/31/2017'
AND p.OPrice<>0 and status=1
group by bt.bookid order by count(bt.bookid) desc

select max(bookname),max(authorname),b.bookid,0,max(b.OrgPrice),0,MAX(b.CreatedDate) from books b
--inner join BookPurchase p on b.BookID=p.BookID
left join authors a on a.authorid=b.authorid
WHERE b.OrgPrice<>0 and b.CreatedDate between '01/01/2017' and '12/31/2017' and status=1
AND B.BookID NOT IN (SELECT BookID FROM booktrans)
group by b.bookid 

select max(bookname) bookname,max(authorname) authorname,bt.bookid,count(bt.bookid) 'COUNT',MAX(b.CreatedDate) CreatedDate,MAX(bt.lenddate) lenddate,MAX(DATEDIFF(DAY,B.CreatedDate,GETDATE())) DateDiff from booktrans bt
inner join books b on b.bookid=bt.bookid
inner join BookPurchase p on b.BookID=p.BookID
left join authors a on a.authorid=b.authorid
WHERE p.OPrice<>0
group by bt.bookid order by count(bt.bookid) desc

--Max_Lend_Members
SELECT bt.memberid,max(membername),count(bookid) maxbooks,max(lenddate) maxdate,sum(lendrate) totalamt,max(status )
from booktrans bt
inner join members m on m.memberid=bt.memberid
group by bt.memberid order by sum(lendrate) desc

--LOST BOOKS
select distinct b.bookid,bookname,authorname,bookprice,createddate
into #lstbooks
from books b 
left join authors a on a.authorid=b.authorid
--left join BookPurchase bp on bp.BookID=SUBSTRING(b.BookID,3,8)
where b.BookID like 'LS%' --and b.BookID not like '%tm%' and b.BookID not like '%th%'
order by CreatedDate desc

select SUBSTRING(lb.BookID,3,8) bookid,max(b.BookName),max(a.AuthorName),count(lb.bookid) maxbooks,sum(bt.lendrate) totalamt
from #lstbooks lb
inner join books b on lb.BookID=b.bookid
left join authors a on a.authorid=b.authorid
inner join booktrans bt on SUBSTRING(lb.BookID,3,8)=bt.BookID
where b.BookID not like '%tm%'
 group by SUBSTRING(lb.BookID,3,8) order by totalamt desc

--MEMBERS FINE
SELECT bt.memberid,max(membername),count(bookid) maxbooks,max(lenddate) maxdate,sum(lendrate) totalamt,sum(fine) totalfine,max(status )
from booktrans bt
inner join members m on m.memberid=bt.memberid
group by bt.memberid order by sum(fine) desc

--CATEGORY WISE
select max(bookname),max(authorname),max(memberid),bt.bookid,SUM(BT.LENDRATE),count(bt.bookid) from booktrans bt
inner join books b on b.bookid=bt.bookid
left join authors a on a.authorid=b.authorid
where bt.bookid like 'TH%' and bt.lenddate between '09/01/2012' and '09/30/2012'
group by bt.bookid order by count(bt.bookid) desc

--ELITE AUTHORS
select max(bookname) bookname,authorname,max(bt.bookid) bookid,max(a.AuthorID) authorid,count(bt.bookid) cntbookid,SUM(bt.lendrate) sumlendrate --into #goodauth 
from booktrans bt 
inner join books b on b.bookid=bt.bookid
left join authors a on a.authorid=b.authorid
--where AuthorName like '%samit%' 
where bt.bookid like 'tm%'
group by authorname having count(authorname)>50 order by authorname--order by count(bt.bookid) desc

SELECT SUM(BT.LENDRATE) FROM booktrans BT
inner join books b on b.bookid=bt.bookid
inner join authors a on a.authorid=b.authorid
WHERE authorname
IN (
select authorname
from booktrans bt 
inner join books b on b.bookid=bt.bookid
left join authors a on a.authorid=b.authorid
--where AuthorName like '%samit%' 
where bt.bookid like 'TH%'
group by authorname having count(authorname)>50)
AND LENDDATE BETWEEN '09/01/2012' AND '09/30/2012'

--MONTH ON MONTH AUTHOR LEND COUNT
SELECT MAX(A.authorid) authorid INTO #GOODAUTH
from booktrans bt 
inner join books b on b.bookid=bt.bookid
left join authors a on a.authorid=b.authorid
group by authorname having count(authorname)>50
select max(authorname),MAX(bt.bookid),count(bt.bookid),SUM(BT.LendRate),MONTH(LendDate),YEAR(LendDate) from booktrans bt
inner join books b on b.bookid=bt.bookid
left join authors a on a.authorid=b.authorid
inner join #GOODAUTH g on a.authorid=g.authorid
WHERE YEAR(LendDate)='2018'
group by A.authorid,YEAR(LendDate),MONTH(LendDate) order by YEAR(LendDate),MONTH(LendDate) desc
--drop table #GOODAUTH
--Selected authors
SELECT MAX(A.authorid) authorid INTO #GOODAUTH
from booktrans bt 
inner join books b on b.bookid=bt.bookid
left join authors a on a.authorid=b.authorid
where a.AuthorID in (4369,5106,1791,1848,3474,5863,2504,3051)
group by authorname --having count(authorname)>50
select max(authorname),MAX(bt.bookid),count(bt.bookid),SUM(BT.LendRate),MONTH(LendDate),YEAR(LendDate) from booktrans bt
inner join books b on b.bookid=bt.bookid
left join authors a on a.authorid=b.authorid
inner join #GOODAUTH g on a.authorid=g.authorid
WHERE YEAR(LendDate)='2018'
group by A.authorid,YEAR(LendDate),MONTH(LendDate) order by YEAR(LendDate),MONTH(LendDate) desc

--Book purchase from June'13
  select substring(b.BookID,0,3),SUM(p.OPrice) from books b
  inner join [BookPurchase] p on b.BookID=p.BookID
  where CreatedDate between '01/1/2015' and GETDATE()
  group by substring(b.BookID,0,3)
  
  --Authors not lent
  SELECT distinct a.*,c.CategoryName FROM authors a
  inner join books b on b.AuthorID=a.AuthorID
  left join category c on c.CategoryID=b.CategoryID
  where CategoryName is not null and a.AuthorID in(
  select distinct AuthorID from authors
  except
  select distinct AuthorID from booktrans bt
  inner join books on books.BookID=bt.BookID
  where LendDate between '1/1/2013' and GETDATE())
 
  
   select substring(b.BookID,0,3),SUM(B.LendRate) from books b
  where CreatedDate between '06/1/2013' and '10/31/2013'
  AND B.BookID LIKE 'PCH%'
  group by substring(b.BookID,0,3)

select max(authorname),SUBSTRING(S.Name,0,5),MAX(bt.bookid),count(bt.bookid),SUM(BT.LENDRATE) 
from booktrans bt
inner join books b on b.bookid=bt.bookid
--FROM books b
left join authors a on a.authorid=b.authorid
left join SubCategory S on S.Id=B.SubCategoryID --AND S.CategoryID=B.CategoryID
WHERE (S.Name LIKE '%MILLS%' OR S.Name LIKE '%HARL%' OR S.Name LIKE '%SIL%')
AND MONTH(LendDate)=9 AND YEAR(LendDate)='2013'
group by SUBSTRING(S.Name,0,5)
select distinct MemberID from booktrans WHERE lendDate between '07/01/2013' and '07/31/2013'

select distinct * from members where (Address like '%thiru%'
or Address like '%tvmr%' or Address like '%41%')


select distinct MemberID from booktrans where lenddate between '11/01/2013' and '11/30/2013' 

select bookname,authorname,bt.bookid,bt.memberid,membername from booktrans bt
inner join books b on b.bookid=bt.bookid
inner join authors a on a.authorid=b.authorid
inner join members m on m.memberid=bt.memberid
where bt.bookid like 'tm%' and lenddate between '07/01/2012' and '07/31/2012' order by bt.memberid

select bookname,authorname,bt.bookid,bt.memberid,membername from booktrans bt
inner join books b on b.bookid=bt.bookid
left join authors a on a.authorid=b.authorid
inner join members m on m.memberid=bt.memberid
where AuthorName like '%James Patterson%' order by BookID 

select distinct bookname from books where BookName like '%tinkle%'
where bookid like 'ch%' order by bookid desc authorname like '%bheem%'

select max(bookname),max(authorname),bt.bookid,max(memberid),count(bt.bookid) from booktrans bt
inner join books b on b.bookid=bt.bookid
left join authors a on a.authorid=b.authorid
where bt.bookid like 'tm%' and bt.lenddate between '06/01/2012' and '06/30/2012'
group by bt.bookid order by count(bt.bookid) desc


--drop table #goodauth
select sum(bt.lendrate) from booktrans bt
inner join books b on b.BookID=bt.BookID
inner join #goodauth ga on  b.AuthorID=ga.authorid
WHERE lendDate between '06/01/2012' and '06/30/2012' and bt.BookID not like 'RM%' 

select max(bookname),max(authorname),bt.bookid,count(bt.bookid),MAX(b.CreatedDate) from booktrans bt
inner join books b on b.bookid=bt.bookid
left join authors a on a.authorid=b.authorid
WHERE b.CreatedDate between '08/01/2011' and '09/30/2012'
group by bt.bookid order by count(bt.bookid) desc



select MAX(A.AUTHORID) AUTHORID,AuthorName into #goodauth 
from booktrans bt 
inner join books b on b.bookid=bt.bookid
left join authors a on a.authorid=b.authorid
--where AuthorName like '%samit%' 
--where bt.bookid like 'RM%'
--WHERE LendDate BETWEEN '09/01/2012' AND '09/30/2012'
group by authorname having count(authorname)>50 --order by count(bt.bookid) desc

select authorname,count(bt.bookid) cntbookid,SUM(bt.lendrate) sumlendrate 
from booktrans bt 
inner join books b on b.bookid=bt.bookid
INNER JOIN #goodauth A on A.AUTHORID=b.authorid
WHERE LendDate BETWEEN '11/01/2011' AND '11/30/2011'
AND bt.BookID NOT LIKE 'TM%'
GROUP BY authorname ORDER BY authorname--MAX(BT.BOOKID)
SELECT * FROM #goodauth

select MEMBERID,MAX(LENDDATE),count(bt.bookid)--,bt.bookid,count(bt.bookid) cntbookid,SUM(bt.lendrate) sumlendrate --into #goodauth 
from booktrans bt 
inner join books b on b.bookid=bt.bookid
left join authors a on a.authorid=b.authorid
WHERE MemberID IN 
(select distinct bt.MemberID
from booktrans bt
inner join books b on b.bookid=bt.bookid
left join authors a on a.authorid=b.authorid
left join members m on m.MemberID=bt.MemberID
WHERE authorname LIKE '%DEAVER%' and m.Status=1
group by bt.MemberID) AND authorname LIKE '%DEAVER%' 
GROUP BY bt.MEMBERID ORDER BY MAX(LENDDATE) DESC

UPDATE BT SET BT.LendRate=B.LendRate 
FROM booktrans BT
INNER JOIN (SELECT LENDRATE,BookID FROM books) B ON B.BookID=BT.BookID
WHERE BT.ReturnDate>'11/24/2013'

------------------Good Author Analysis-------------
select max(bookname) bookname,authorname,max(bt.bookid) bookid,max(a.AuthorID) authorid,count(bt.bookid) cntbookid,SUM(bt.lendrate) sumlendrate into #goodauth 
from booktrans bt 
inner join books b on b.bookid=bt.bookid
left join authors a on a.authorid=b.authorid
group by authorname having count(authorname)>50 order by authorname--order by count(bt.bookid) desc

select a.AuthorName,max(a.AuthorID) authorid,count(bt.bookid) cntbookid,SUM(bt.lendrate) sumlendrate,MONTH(bt.LendDate),year(bt.LendDate) --into #goodauth 
from booktrans bt 
inner join books b on b.bookid=bt.bookid
inner join #goodauth g on g.authorid=b.AuthorID
left join authors a on a.authorid=b.authorid
--where AuthorName like '%samit%' 
--where bt.bookid like 'in%'
group by a.AuthorName,MONTH(bt.LendDate),year(bt.LendDate) order by a.AuthorName,year(bt.LendDate),MONTH(bt.LendDate) asc--order by count(bt.bookid) desc
----------------------------------------------------

---Author of the month
select bt.BookID,bt.LendDate,a.AuthorName,bt.LendRate from booktrans bt
inner join books b on b.BookID=bt.BookID
left join authors a on a.authorid=b.authorid
where b.AuthorID in (2605,3235,1811)
and bt.LendDate between '7/01/2015'
and '7/31/2015' order by a.AuthorName

--Count of Books by Author
select a.authorname,count(b.bookid) CntBooks,SUBSTRING(max(b.BookID),0,3) Category,SUM(ISNULL(Oprice,BookPrice)) Price into #tempb1 from books b
left join authors a on a.authorid=b.authorid
left join BookPurchase bp on bp.BookID=b.BookID
where b.BookID not like 'LS%' and b.BookID not like 'SA%' and b.BookID not like 'PC%' and b.BookID not like 'SC%' and b.BookID not like 'PT%' and b.BookID not like 'TE%' and b.BookID not like 'FL%'
group by a.AuthorName  order by CntBooks
select authorname,count(bt.bookid) CntRentBooks into #tempb2 from booktrans bt
inner join books b on b.bookid=bt.bookid
left join authors a on a.authorid=b.authorid
where ReturnDate is null
group by AuthorName order by count(b.bookid) desc
select #tempb1.authorname,CntBooks,Category,CntRentBooks,Price from #tempb1
left join #tempb2 on #tempb1.AuthorName=#tempb2.AuthorName  order by CntBooks desc
drop table #tempb1,#tempb2

select a.authorname,count(b.bookid) CntBooks,SUBSTRING(max(b.BookID),0,3) Category,SUM(ISNULL(Oprice,BookPrice)) Price into #tempb1 from books b
left join authors a on a.authorid=b.authorid
left join BookPurchase bp on bp.BookID=b.BookID
where b.BookID not like 'LS%' and b.BookID not like 'SA%' and b.BookID not like 'PC%' and b.BookID not like 'SC%' and b.BookID not like 'PT%' and b.BookID not like 'TE%' and b.BookID not like 'FL%'
group by a.AuthorName  order by CntBooks
select authorname,count(bt.bookid) CntRentBooks,SUM(bt.LendRate) SumLendrate into #tempb2 from booktrans bt
inner join books b on b.bookid=bt.bookid
left join authors a on a.authorid=b.authorid
--where YEAR(lenddate)=2015
group by AuthorName order by count(b.bookid) desc
select #tempb1.authorname,CntBooks,Category,CntRentBooks,Price,SumLendrate from #tempb1
left join #tempb2 on #tempb1.AuthorName=#tempb2.AuthorName  order by CntRentBooks desc
drop table #tempb1,#tempb2


---Book not read by member
select max(bookname),max(authorname),b.bookid,MAX(b.CreatedDate) from books b
--inner join BookPurchase p on b.BookID=p.BookID
left join authors a on a.authorid=b.authorid
WHERE status=1 and AuthorName like 'jeffrey%' and b.BookID like 'th%'
and bookid not like '%-%'
AND B.BookID NOT IN (SELECT BookID FROM booktrans where MemberID='I00014')
AND B.BookID NOT IN (SELECT Substring(BookID,0,CHARINDEX('-',BookID)) FROM booktrans where MemberID='I00014' and bookid like '%-%')
group by b.bookid 