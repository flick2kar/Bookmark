Alter table books alter column BookID varchar(255) not null 
Alter table booktrans add primary key (transid)
Alter table Series add primary key (SeriesID)
Alter table members drop column ID 
Alter table members alter column memberid varchar(255) not null 
Alter table members add primary key (memberid)
Alter table books add primary key (BookID)
Alter table books alter column BookPrice int
Alter table books alter column AuthorID int
Alter table books alter column CategoryID int
