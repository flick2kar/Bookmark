select count(distinct memberid) from booktrans where createddate between '2013-01-01' and '2013-01-31' and BookID like 'ch%' 
UNION ALL
select count(distinct memberid) from booktrans where createddate between '2013-02-01' and '2013-02-28' and BookID like 'ch%'
UNION ALL
select count(distinct memberid) from booktrans where createddate between '2013-03-01' and '2013-03-31' and BookID like 'ch%'
UNION ALL
select count(distinct memberid) from booktrans where createddate between '2013-04-01' and '2013-04-30' and BookID like 'ch%'
UNION ALL
select count(distinct memberid) from booktrans where createddate between '2013-05-01' and '2013-05-31' and BookID like 'ch%'
UNION ALL
select count(distinct memberid) from booktrans where createddate between '2013-06-01' and '2013-06-30' and BookID like 'ch%'
UNION ALL
select count(distinct memberid) from booktrans where createddate between '2013-07-01' and '2013-07-31' and BookID like 'ch%'
UNION ALL
select count(distinct memberid) from booktrans where createddate between '2013-08-01' and '2013-08-31' and BookID like 'ch%'
UNION ALL
select count(distinct memberid) from booktrans where createddate between '2013-09-01' and '2013-09-30' and BookID like 'ch%'
UNION ALL
select count(distinct memberid) from booktrans where createddate between '2013-10-01' and '2013-10-31' and BookID like 'ch%'
UNION ALL
select count(distinct memberid) from booktrans where createddate between '2013-11-01' and '2013-11-30' and BookID like 'ch%'
UNION ALL
select count(distinct memberid) from booktrans where createddate between '2013-12-01' and '2013-12-31' and BookID like 'ch%'