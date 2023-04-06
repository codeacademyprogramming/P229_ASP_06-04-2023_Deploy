--Kitabxana database-i qurursunuz
Create Database P229Library

Use P229Library
--Books (Id, Name, PageCount)
--Books-un Name columu minimum 2 simvol maksimum 100 simvol deyer ala bileceyi serti olsun.
--Books-un PageCount columu minimum 10 deyerini ala bileceyi serti olsun.

Create Table Books
(
	Id int identity primary key,
	Name nvarchar(100) Check(Len(Name) >= 2),
	PageCount int Check(PageCount >= 10)
)

Insert Into Books
Values
('In Search of Lost Time',550,1),
('Ulysses',700,2),
('Don Quixote',350,3),
('One Hundred Years of Solitude',850,4),
('The Great Gatsby',1000,5),
('Moby Dick',450,6),
('War and Peace',950,7),
('Hamlet',250,8)

--Authors (Id, Name, Surname)

Create Table Authors
(
	Id int identity primary key,
	Name nvarchar(100),
	SurName nvarchar(100)
)

Insert Into Authors
Values
('Marcel','Proust'),
('James','Joyce'),
('Miguel','de Cervantes'),
('Gabriel Garcia ','Marquez'),
('Scott','Fitzgerald'),
('Herman','Melville'),
('Leo','Tolstoy'),
(' William','Shakespeare')

--Books ve Authors table-larinizin mentiqi uygun elaqesi olsun.
Alter Table Books
Add AuthorId int Foreign Key References Authors(Id)


--Id, Name, PageCount ve AuthorFullName columnlarinin valuelarini qaytaran bir view yaradin
Create View usv_GetBooksWithAuthors
As
Select b.Id, b.Name,b.PageCount, CONCAT(a.Name,' ',a.SurName) 'AuthorFullName' From Books b
Join Authors a
On b.AuthorId = a.Id

Select * From usv_GetBooksWithAuthors

--Gonderilmis axtaris deyirene gore hemin axtaris deyeri name 
--ve ya authorFullNamelerinde olan Book-lari Id, Name, PageCount, AuthorFullName columnlari 
--seklinde gostern procedure yazin

Create Procedure usp_SearchBooks
@search nvarchar(100)
as
begin
	Select * From usv_GetBooksWithAuthors 
	Where 
	Name Like '%'+@search+'%' OR
	AuthorFullName Like '%'+@search+'%'
end

exec usp_SearchBooks 'arc'

--Authors tableinin insert, update ve deleti ucun (her biri ucun ayrica) procedure yaradin

Create Procedure usp_CreateAuthor
@name nvarchar(100), 
@surName nvarchar(100)
as
begin
	Insert Into Authors(Name,SurName)
	Values
	(@name,@surName)
end

exec usp_CreateAuthor 'Nizami','Gencevi'

Create Procedure usp_UpdateAuthor
@id int,
@name nvarchar(100), 
@surName nvarchar(100)
as
begin
	Update Authors set Name = @name , SurName = @surName where Id = @id
end

exec usp_UpdateAuthor 9,'Fuzuli','Mehemmed'


Create Procedure usp_DeleteAuthor
@id int
as
begin
	Delete Authors Where Id = @id 
end

exec usp_DeleteAuthor 9

--Authors-lari Id,FullName,BooksCount,MaxPageCount seklinde qaytaran view yaradirsiniz 
--Id-author id-si, FullName - Name ve Surname birlesmesi, BooksCount - Hemin authorun 
--elaqeli oldugu kitablarin sayi, MaxPageCount - hemin authorun elaqeli oldugu kitablarin 
--icerisindeki max pagecount deyeri

Create View usv_GetAuthors
As
Select a.Id, (a.Name+' '+a.SurName) 'Full Name', COUNT(*) 'BooksCount', MAX(b.PageCount) 'MaxPageCount' From Authors a
Join Books b
On b.AuthorId = a.Id
Group By a.Id, a.Name, a.SurName

select * from usv_GetAuthors