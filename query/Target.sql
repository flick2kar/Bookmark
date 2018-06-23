SELECT 1,sum(booktrans.LendRate)
FROM booktrans
WHERE booktrans.LendDate between '05/01/2018' and '05/31/2018'
AND bookid like '%TH%'
UNION
SELECT 2,sum(booktrans.LendRate)
FROM booktrans
WHERE booktrans.LendDate between '05/01/2018' and '05/31/2018'
AND bookid like 'AD%'
UNION
SELECT 3,sum(booktrans.LendRate)
FROM booktrans
WHERE booktrans.LendDate between '05/01/2018' and '05/31/2018'
AND bookid like 'AU%'
UNION
SELECT 4,sum(booktrans.LendRate)
FROM booktrans
WHERE booktrans.LendDate between '05/01/2018' and '05/31/2018'
AND bookid like 'PTH%'
UNION
SELECT 5,sum(booktrans.LendRate)
FROM booktrans
WHERE booktrans.LendDate between '05/01/2018' and '05/31/2018'
AND bookid like 'BI%'
UNION
SELECT 6,sum(booktrans.LendRate)
FROM booktrans
WHERE booktrans.LendDate between '05/01/2018' and '05/31/2018'
AND bookid like 'CH%'
UNION
SELECT 7,sum(booktrans.LendRate)
FROM booktrans
WHERE booktrans.LendDate between '05/01/2018' and '05/31/2018'
AND bookid like 'CS%'
UNION
SELECT 9,sum(booktrans.LendRate)
FROM booktrans
WHERE booktrans.LendDate between '05/01/2018' and '05/31/2018'
AND bookid like 'GF%'
UNION
SELECT 14,sum(booktrans.LendRate)
FROM booktrans
WHERE booktrans.LendDate between '05/01/2018' and '05/31/2018'
AND bookid like 'HU%'
UNION
SELECT 15,sum(booktrans.LendRate)
FROM booktrans
WHERE booktrans.LendDate between '05/01/2018' and '05/31/2018'
AND bookid like 'IN%'
UNION
SELECT 16,sum(booktrans.LendRate)
FROM booktrans
WHERE booktrans.LendDate between '05/01/2018' and '05/31/2018'
AND bookid like 'MG%'
UNION
SELECT 19,sum(booktrans.LendRate)
FROM booktrans
WHERE booktrans.LendDate between '05/01/2018' and '05/31/2018'
AND bookid like 'NF%'
UNION
SELECT 20,sum(booktrans.LendRate)
FROM booktrans
WHERE booktrans.LendDate between '05/01/2018' and '05/31/2018'
AND bookid like 'RM%'
UNION
SELECT 22,sum(booktrans.LendRate)
FROM booktrans
WHERE booktrans.LendDate between '05/01/2018' and '05/31/2018'
AND bookid like 'SH%'
UNION
SELECT 23,sum(booktrans.LendRate)
FROM booktrans
WHERE booktrans.LendDate between '05/01/2018' and '05/31/2018'
AND bookid like 'SP%'
UNION
SELECT 24,sum(booktrans.LendRate)
FROM booktrans
WHERE booktrans.LendDate between '05/01/2018' and '05/31/2018'
AND bookid like 'TM%'
UNION SELECT 26,sum(booktrans.LendRate)
FROM booktrans
WHERE booktrans.LendDate between '05/01/2018' and '05/31/2018'
AND bookid like 'WE%';
