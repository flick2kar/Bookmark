--select distinct AuthorName,max(a.AuthorID) AuthorID
--into #goodauth 
--from booktrans bt 
--inner join books b on b.bookid=bt.bookid
--left join authors a on a.authorid=b.authorid
--where authorname<>''
--group by authorname having count(authorname)>50 order by AuthorName

SELECT 'TH%',sum(bt.LendRate)
FROM booktrans bt
inner join books b on b.bookid=bt.bookid
inner join #goodauth g on g.authorid=b.authorid
WHERE bt.LendDate between '04/01/2013' and '04/30/2013'
AND b.bookid like 'TH%'
UNION
SELECT 'AD%',sum(bt.LendRate)
FROM booktrans bt
inner join books b on b.bookid=bt.bookid
inner join #goodauth g on g.authorid=b.authorid
WHERE bt.LendDate between '04/01/2013' and '04/30/2013'
AND b.bookid  like 'AD%'
UNION
SELECT 'CH%',sum(bt.LendRate)
FROM booktrans bt
inner join books b on b.bookid=bt.bookid
inner join #goodauth g on g.authorid=b.authorid
WHERE bt.LendDate between '04/01/2013' and '04/30/2013'
AND b.bookid  like 'CH%'
UNION
SELECT 'IN%',sum(bt.LendRate)
FROM booktrans bt
inner join books b on b.bookid=bt.bookid
inner join #goodauth g on g.authorid=b.authorid
WHERE bt.LendDate between '04/01/2013' and '04/30/2013'
AND b.bookid  like 'IN%'
UNION
SELECT 'MG%',sum(bt.LendRate)
FROM booktrans bt
inner join books b on b.bookid=bt.bookid
inner join #goodauth g on g.authorid=b.authorid
WHERE bt.LendDate between '04/01/2013' and '04/30/2013'
AND b.bookid  like 'MG%'
UNION
SELECT 'NF%',sum(bt.LendRate)
FROM booktrans bt
inner join books b on b.bookid=bt.bookid
inner join #goodauth g on g.authorid=b.authorid
WHERE bt.LendDate between '04/01/2013' and '04/30/2013'
AND b.bookid  like 'NF%'
UNION
SELECT 'RM%',sum(bt.LendRate)
FROM booktrans bt
inner join books b on b.bookid=bt.bookid
inner join #goodauth g on g.authorid=b.authorid
WHERE bt.LendDate between '04/01/2013' and '04/30/2013'
AND b.bookid  like 'RM%'
UNION
SELECT 'TM%',sum(bt.LendRate)
FROM booktrans bt
inner join books b on b.bookid=bt.bookid
inner join #goodauth g on g.authorid=b.authorid
WHERE bt.LendDate between '04/01/2013' and '04/30/2013'
AND b.bookid  like 'TM%'

