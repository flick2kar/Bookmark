SELECT 1,count(distinct BookID)
FROM books
WHERE status=1 and bookid like 'TH%'
UNION
SELECT 2,count(distinct BookID)
FROM books
WHERE status=1 and 
 bookid like 'AD%'
UNION
SELECT 3,count(distinct BookID)
FROM books
WHERE status=1 and 
 bookid like 'AU%'
UNION
SELECT 4,count(distinct BookID)
FROM books
WHERE status=1 and 
 bookid like 'BB%'
UNION
SELECT 5,count(distinct BookID)
FROM books
WHERE status=1 and 
 bookid like 'BI%'
UNION
SELECT 6,count(distinct BookID)
FROM books
WHERE status=1 and 
 bookid like 'CH%'
UNION
SELECT 7,count(distinct BookID)
FROM books
WHERE status=1 and 
 bookid like 'CS%'
UNION
SELECT 9,count(distinct BookID)
FROM books
WHERE status=1 and 
 bookid like 'GF%'
UNION
SELECT 14,count(distinct BookID)
FROM books
WHERE status=1 and 
 bookid like 'HU%'
UNION
SELECT 15,count(distinct BookID)
FROM books
WHERE status=1 and 
 bookid like 'IN%'
UNION
SELECT 16,count(distinct BookID)
FROM books
WHERE status=1 and 
 bookid like 'MG%'
UNION
SELECT 19,count(distinct BookID)
FROM books
WHERE status=1 and 
 bookid like 'NF%'
UNION
SELECT 20,count(distinct BookID)
FROM books
WHERE status=1 and 
 bookid like 'RM%'
UNION
SELECT 22,count(distinct BookID)
FROM books
WHERE status=1 and 
 bookid like 'SH%'
UNION
SELECT 23,count(distinct BookID)
FROM books
WHERE status=1 and 
 bookid like 'SP%'
UNION
SELECT 24,count(distinct BookID)
FROM books
WHERE status=1 and 
 bookid like 'TM%'
UNION
SELECT 26,count(distinct BookID)
FROM books
WHERE status=1 and 
 bookid like 'WE%';
