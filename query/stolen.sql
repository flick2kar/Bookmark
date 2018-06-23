select distinct bt.MemberID,max(m.MemberName),COUNT(bt.bookid),MAX(bt.lenddate) from booktrans bt
inner join books b on b.bookid=bt.bookid
left join authors a on a.authorid=b.authorid
inner join members m on bt.MemberID=m.MemberID
where BookID in 
('TM3338','TM3523','TM2987','TM3173','TM3695','TM3951,TM3088','TM2933','TM2858,TM2953','TM3100','TM3160','TM3116','TM3372','TM2963',' TM3800','TM2955','TM3639','TM3388','TM3413','TM3124','TM3318','TM3599','TM3090','TM3375','TM3414','TM3248','TM3648','TM3466','TM3783','TM3115','TM3229','TM3322','TM3781','TM3415. TM2991','TM3469','TM3462','TM 3671','TM3486','TM3649','TM3607','TM3376','TM2950 TM3543','TM3873')
group by bt.MemberID order by COUNT(bt.bookid) desc


select  bt.BookID,max(b.BookName),max(a.AuthorName),COUNT(bt.bookid),MAX(bt.lenddate) from booktrans bt
inner join books b on b.bookid=bt.bookid
left join authors a on a.authorid=b.authorid
inner join members m on bt.MemberID=m.MemberID
where bt.BookID in 
('TM3338','TM3523','TM2987','TM3173','TM3695','TM3951,TM3088','TM2933','TM2858,TM2953','TM3100','TM3160','TM3116','TM3372','TM2963',' TM3800','TM2955','TM3639','TM3388','TM3413','TM3124','TM3318','TM3599','TM3090','TM3375','TM3414','TM3248','TM3648','TM3466','TM3783','TM3115','TM3229','TM3322','TM3781','TM3415','TM2991','TM3469','TM3462','TM3671','TM3486','TM3649','TM3607','TM3376','TM2950 TM3543','TM3873')
group by bt.BookID
