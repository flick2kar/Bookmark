select count(distinct memberid),'1-100' from booktrans where LendDate between '05/01/2017' and '05/31/2017' and 
SUBSTRING(memberid,2,LEN(memberid)) <=100 
UNION
select count(distinct memberid),'101-200' from booktrans where LendDate between '05/01/2017' and '05/31/2017' and 
SUBSTRING(memberid,2,LEN(memberid)) between 101 and 200
UNION
select count(distinct memberid),'201-300' from booktrans where LendDate between '05/01/2017' and '05/31/2017' and 
SUBSTRING(memberid,2,LEN(memberid)) between 201 and 300
UNION
select count(distinct memberid),'301-400' from booktrans where LendDate between '05/01/2017' and '05/31/2017' and 
SUBSTRING(memberid,2,LEN(memberid)) between 301 and 400
UNION
select count(distinct memberid),'401-500' from booktrans where LendDate between '05/01/2017' and '05/31/2017' and 
SUBSTRING(memberid,2,LEN(memberid)) between 401 and 500
UNION
select count(distinct memberid),'501-600' from booktrans where LendDate between '05/01/2017' and '05/31/2017' and 
SUBSTRING(memberid,2,LEN(memberid)) between 501 and 600
UNION
select count(distinct memberid),'601-700' from booktrans where LendDate between '05/01/2017' and '05/31/2017' and 
SUBSTRING(memberid,2,LEN(memberid)) between 601 and 700
UNION
select count(distinct memberid),'701-800' from booktrans where LendDate between '05/01/2017' and '05/31/2017' and 
SUBSTRING(memberid,2,LEN(memberid)) between 701 and 800
UNION
select count(distinct memberid),'801-900' from booktrans where LendDate between '05/01/2017' and '05/31/2017' and 
SUBSTRING(memberid,2,LEN(memberid)) between 801 and 900
UNION
select count(distinct memberid),'901-1000' from booktrans where LendDate between '05/01/2017' and '05/31/2017' and 
SUBSTRING(memberid,2,LEN(memberid)) between 901 and 1000
UNION
select count(distinct memberid),'1001-1100' from booktrans where LendDate between '05/01/2017' and '05/31/2017' and 
SUBSTRING(memberid,2,LEN(memberid)) between 1001 and 1100
UNION
select count(distinct memberid),'1101-1200' from booktrans where LendDate between '05/01/2017' and '05/31/2017' and 
SUBSTRING(memberid,2,LEN(memberid)) between 1101 and 1200
UNION
select count(distinct memberid),'1201-1300' from booktrans where LendDate between '05/01/2017' and '05/31/2017' and 
SUBSTRING(memberid,2,LEN(memberid)) between 1201 and 1300
UNION
select count(distinct memberid),'1301-1400' from booktrans where LendDate between '05/01/2017' and '05/31/2017' and 
SUBSTRING(memberid,2,LEN(memberid)) between 1301 and 1400
UNION
select count(distinct memberid),'1401-1500' from booktrans where LendDate between '05/01/2017' and '05/31/2017' and 
SUBSTRING(memberid,2,LEN(memberid)) between 1401 and 1500
UNION
select count(distinct memberid),'1501-1600' from booktrans where LendDate between '05/01/2017' and '05/31/2017' and 
SUBSTRING(memberid,2,LEN(memberid)) between 1501 and 1600
UNION
select count(distinct memberid),'1601-1700' from booktrans where LendDate between '05/01/2017' and '05/31/2017' and 
SUBSTRING(memberid,2,LEN(memberid)) between 1601 and 1700
UNION
select count(distinct memberid),'1701-1800' from booktrans where LendDate between '05/01/2017' and '05/31/2017' and 
SUBSTRING(memberid,2,LEN(memberid)) between 1701 and 1800
UNION
select count(distinct memberid),'1801-1900' from booktrans where LendDate between '05/01/2017' and '05/31/2017' and 
SUBSTRING(memberid,2,LEN(memberid)) between 1801 and 1900
UNION
select count(distinct memberid),'1901-2000' from booktrans where LendDate between '05/01/2017' and '05/31/2017' and 
SUBSTRING(memberid,2,LEN(memberid)) between 1901 and 2000
UNION
select count(distinct memberid),'2001-2100' from booktrans where LendDate between '05/01/2017' and '05/31/2017' and 
SUBSTRING(memberid,2,LEN(memberid)) between 2001 and 2100
UNION
select count(distinct memberid),'2101-2200' from booktrans where LendDate between '05/01/2017' and '05/31/2017' and 
SUBSTRING(memberid,2,LEN(memberid)) between 2101 and 2200
UNION
select count(distinct memberid),'2201-2300' from booktrans where LendDate between '05/01/2017' and '05/31/2017' and 
SUBSTRING(memberid,2,LEN(memberid)) between 2201 and 2300