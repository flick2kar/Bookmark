select distinct substring(memberid,3,6) 'memid',memberid,'Aug' as 'Month' into #temp from booktrans where lenddate between '08/01/2011' and '08/31/2011'
UNION
select distinct substring(memberid,3,6) 'memid',memberid,'Jul' as 'Month' from booktrans where lenddate between '07/01/2011' and '07/31/2011'
UNION
select distinct substring(memberid,3,6) 'memid',memberid,'Jun' as 'Month' from booktrans where lenddate between '06/01/2011' and '06/30/2011'
UNION
select distinct substring(memberid,3,6) 'memid',memberid,'May' as 'Month' from booktrans where lenddate between '05/01/2011' and '05/31/2011'
UNION
select distinct substring(memberid,3,6) 'memid',memberid,'Apr' as 'Month' from booktrans where lenddate between '04/01/2011' and '04/30/2011'
UNION
select distinct substring(memberid,3,6) 'memid',memberid,'Mar' as 'Month' from booktrans where lenddate between '03/01/2011' and '03/31/2011'
order by substring(memberid,3,6) 


select m.memberid,dbo.GetBooksByMember(m.memberid),membername from
(select distinct memberid from #temp group by memberid having count(memberid)=3) mem
inner join members m on mem.memberid=m.memberid order by substring(m.memberid,3,6)

select bt.memberid,sum(lendrate),max(lenddate),max(status) from booktrans bt
inner join (select distinct memberid from #temp group by memberid having count(memberid)=3) mem on mem.memberid=bt.memberid
inner join members m on mem.memberid=m.memberid group by bt.memberid order by substring(bt.memberid,3,6)




select * from #temp t where month='mar'
and memberid not in (select distinct memberid from #temp where month in ('aug','jul','jun','may','apr')) order by memid

select distinct memberid from booktrans where lenddate between '07/01/2010' and '07/31/2010'
select distinct memberid from booktrans where lenddate between '07/01/2011' and '07/31/2011'

select * from members order by doj desc where lenddate between '08/01/2010' and '08/31/2010'

select * from booktrans where memberid='B00904'
