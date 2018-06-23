select distinct memberid,dbo.GetBooksByMember(memberid) bookpref into #prefbooks from members --order by substring(memberid,3,6)

select m.memberid,max(m.doj) doj,MAX(lenddate) Lenddate,MAX(status) Status into #memdetails from members m 
left outer join booktrans bt on bt.MemberID=m.MemberID
group by m.MemberID --order by substring(m.memberid,3,6)

insert into MemPrefBooks
select p.MemberID,Doj,Lenddate,Status,bookpref from #prefbooks p
inner join #memdetails m on m.MemberID=p.MemberID order by substring(p.memberid,3,6)
drop table #prefbooks
drop table #memdetails

select * from MemPrefBooks where PrefBooks like '%IN%'

select * from MemPrefBooks mp where status=1 and PrefBooks like '%ch%'and PrefBooks like '%th%'