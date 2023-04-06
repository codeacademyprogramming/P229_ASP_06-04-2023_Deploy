--DDL Queries (Data Definition Language)
--Database Yaradilma Sorgusu
Create Database P229FirstSQLCommand

--Database Isdifade Etmek Sorgusu
Use P229FirstSQLCommand

--Table Yaratmaq Sorgusu
Create Table Groups
(
	Id int,
	Name nvarchar(100)
)

--Table-da Struktural Deyisiklik Etmek Sorgusu - Yeni Column Elave Olunmasi
Alter Table Groups 
Add StudentCount int

--Table-da Struktural Deyisiklik Etmek Sorgusu - Column Uzerinde Deyisiklik
Alter Table Groups 
Alter Column StudentCount nvarchar(50)

--Table-da Struktural Deyisiklik Etmek Sorgusu - Column-un Silinmesi
Alter Table Groups
Drop Column StudentCount

Drop Table Groups

--DML Queries Data Manipulation Language
--Datanin Tabela Elave Olunmasi
Insert Into Groups 
Values
(1,'P229',18)

Insert Into Groups(Name,StudentCount,Id) 
Values
('P133',15,2)

Insert Into Groups(Id,Name) 
Values
(3,'P217')

Insert Into Groups
Values
(4,'P225',6),
(5,'P129',10),
(6,'P509',13),
(7,'P409',9)

Create Table Students
(
	Id int,
	Name nvarchar(100),
	SurName nvarchar(100),
	Age TinyInt,
	Score TinyInt,
	GroupId int
)

Insert Into Students
Values
(1,N'Rəşad',N'Ismayılov',22,70,1),
(2,N'Mahmud',N'Əliyev',21,42,1),
(1,N'Rövşən','Bayramov',20,50,1),
(1,'Kamil',N'Abdullayev',19,100,1)

Update Students Set Id = 4 where Age = 19

Update Students Set Score = 90, GroupId = 2 where Age = 19 OR Age = 20

Update Students Set Score = 99 where Age >= 19 AND Age <= 21

Update Students Set Score = 50 where Age Between 19 And 21

Delete From Students where Id = 2

--DQL Queries Data Query Language

Select Id,Name,SurName,Age,Score,GroupId From Students

Select * From Students

Select Name,SurName From Students

Select Name+' '+SurName From Students

Select Name+' '+SurName FullName From Students

Select Name+' '+SurName 'Full Name' From Students

Select Name+' '+SurName 'FullName' From Students

Select Name+' '+SurName as 'Full Name' From Students

Select Name+' '+SurName [Full Name] From Students

Select Name+' '+SurName as [Full Name] From Students

Select Name Ad, SurName SoyAd From Students

Select * From Students Where Age = 19 Or Age = 21 Or Age = 22

Select * From Students Where Age In(19,22,21)

Select MIN(Age) From Students where age > 20

Select Max(Age) From Students

Select Avg(Age) From Students

Select SUm(Age) From Students

Select COUNT(*) From Students where Age > 20

Select UPPER(Name),SurName From Students

Select UPPER(Name+' '+SurName) From Students

Select LOwer(Name+' '+SurName) From Students

Select LEN(Name) From Students

Select LEN(Name+' '+SurName) From Students

Select * from Students Where LEN(Name) >= 6

Select CHARINDEX('a',Name) from Students

Select SUBSTRING(Name,2,LEN(Name)) From Students

Select REPLACE(SurName,'e',' CODE ') From Students

Alter Table Students 
Add Email nvarchar(100)

Select SUBSTRING(Email,CHARINDEX('@',Email)+1,Len(Email)) From Students

Select * From Students Where Age=(Select MIN(Age) From Students)