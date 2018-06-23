select max(bookname) bookname,max(authorname) authorname,bt.bookid,count(bt.bookid) CntBookid,sum(bt.lendrate)SumLendRate,max(b.OrgPrice) OPrice,MAX(b.CreatedDate) CreatedDate,MAX(LENDDATE) LENDDATE,CONVERT(decimal(16,0),100*(convert(float,(sum(bt.lendrate)-max(b.OrgPrice)))/convert(float,max(b.OrgPrice)))) AS ProfitPercent into #ProfitCalc from booktrans bt
inner join books b on b.bookid=bt.bookid
left join authors a on a.authorid=b.authorid
WHERE YEAR(b.CreatedDate)='2017'-- between '01/01/2014' and '03/31/2014'
AND b.OrgPrice<>0 --and bt.BookID  like 'pth%'
group by bt.bookid order by count(bt.bookid) desc

--Total Books Bought
Select 1 SNO,'Total Books Bought',count(b.bookid) CntBooks,SUM(b.OrgPrice) PurchaseAmt,0 AmtInreturn from books b
WHERE YEAR(b.CreatedDate)='2017'-- between '01/01/2014' and '03/31/2014'
AND b.OrgPrice<>0
UNION
Select 2 SNO,'Total Books in Transaction',count(bookid),SUM(OPrice),SUM(SumLendRate) from #ProfitCalc
UNION
Select 3 SNO,'Total Books in Thriller',count(bookid),SUM(OPrice),SUM(SumLendRate) from #ProfitCalc where BookID like '%Th%'
UNION
Select 4 SNO,'Total Books in Children',count(bookid),SUM(OPrice),SUM(SumLendRate) from #ProfitCalc where BookID like 'Ch%'
UNION
Select 5 SNO,'Total Books in Romance',count(bookid),SUM(OPrice),SUM(SumLendRate) from #ProfitCalc where BookID like 'RM%'
UNION
Select 6 SNO,'Total Books in Indian',count(bookid),SUM(OPrice),SUM(SumLendRate) from #ProfitCalc where BookID like 'IN%'
UNION
Select 7 SNO,'Total Books in Tamil',count(bookid),SUM(OPrice),SUM(SumLendRate) from #ProfitCalc where BookID like 'TM%'
UNION
Select 8 SNO,'Total Books in Transaction with profit',count(bookid),SUM(OPrice),SUM(SumLendRate) from #ProfitCalc where ProfitPercent>0
UNION
Select 9 SNO,'Total Books in Thriller with profit',count(bookid),SUM(OPrice),SUM(SumLendRate) from #ProfitCalc where BookID like 'Th%' and ProfitPercent>0
UNION
Select 10 SNO,'Total Books in Children with profit',count(bookid),SUM(OPrice),SUM(SumLendRate) from #ProfitCalc where BookID like 'Ch%' and ProfitPercent>0
UNION
Select 11 SNO,'Total Books in Romance with profit',count(bookid),SUM(OPrice),SUM(SumLendRate) from #ProfitCalc where BookID like 'RM%' and ProfitPercent>0
UNION
Select 12 SNO,'Total Books in Indian with profit',count(bookid),SUM(OPrice),SUM(SumLendRate) from #ProfitCalc where BookID like 'IN%' and ProfitPercent>0
UNION
Select 13 SNO,'Total Books in Tamil with profit',count(bookid),SUM(OPrice),SUM(SumLendRate) from #ProfitCalc where BookID like 'TM%'and ProfitPercent>0

--drop table #ProfitCalc

--Select * from #ProfitCalc where BookID like 'rm%'
--select * from booktrans where BookID='TM2662'
