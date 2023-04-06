Create database P229ConstrainJoinUnion

Use P229ConstrainJoinUnion

Create Table Students
(
	Id int identity Primary Key,
	Name nvarchar(100) Not Null Check(Len(Name) >= 3),
	SurName nvarchar(100) Not Null Check(Len(SurName) >= 5),
	Age TinyInt Check(Age Between 15 And 50),
	Score decimal(5,2) Check(Score Between 0 And 100),
	Email nvarchar(100) Not NUll Unique
)

Create Table Students
(
	Id int identity Constraint PK_Students_Id Primary Key,
	Name nvarchar(100) Not NUll,
	SurName nvarchar(100),
	Constraint CK_Students_Name Check(Len(Name) >= 3)
)

Alter Table Students
Add Constraint CK_Students_SurName Check(len(SurName) >= 5)

Alter Table Students
Add Age TinyInt Check(Age Between 15 And 50)

Alter Table Students
Drop Constraint CK__Students__Age__52593CB8

Alter table Students
Drop Column Age

Alter Table Students
Add Age TinyInt Constraint CK_Students_Age Check(Age Between 15 And 50)

Alter Table Students
Add Email nvarchar(100) 

Alter Table Students
Add Constraint UQ_Students_Email Unique(Email)

Alter table Students
Add CreatedBy nvarchar(100) Constraint DF_Students_CB Default('system')

Alter table Students
Add CreatedBy nvarchar(100)

Alter Table Students
Add Constraint DF_Students_CB Default('system') For CreatedBy

Create Table Groups
(
	Id int identity primary key,
	Name nvarchar(10) Check(Len(Name) >= 4),
	StudentCount int Check(StudentCount Between 12 And 18),
	Room nvarchar(15)
)

Alter Table Students
Add GroupId int Foreign Key References Groups(Id)

Insert Into Groups
Values
('P229',18,'Jupiter'),
('P133',15,'Jupiter'),
('P225',15,'Titan')

Insert Into Students
Values
('Hamid','Mammadov',19,40,'hamid@code.edu.az',NULL),
('Kamil','Abdullayev',19,100,'kamil@code.edu.az',1),
('Rashad','Ismayilov',22,70,'rashad@code.edu.az',1),
('Rovshen','Bayramov',19,50,'rovshan@code.edu.az',1)

Select 
s.Name,
s.SurName,
s.Age,
s.Email,
s.Score,
g.Name 
From Groups g
Inner Join Students s
On s.GroupId = g.Id

Select 
Students.Name,
Students.SurName,
Students.Age,
Students.Email,
Students.Score,
Groups.Name 
From Students 
Join Groups
On Students.GroupId = Groups.Id

Select * From Students 
Right Join Groups 
On Students.GroupId = Groups.Id

Select * From Students 
Right Outer Join Groups 
On Students.GroupId = Groups.Id

Select * From Groups 
Left Join Students 
On Students.GroupId = Groups.Id

Select * From Groups 
Left Outer Join Students 
On Students.GroupId = Groups.Id

Select * From Groups 
Full Join Students 
On Students.GroupId = Groups.Id

Select * From Groups 
Full Outer Join Students 
On Students.GroupId = Groups.Id

Create Table Certificates
(
	Id int identity Primary Key,
	Name nvarchar(100),
	Min int,
	Max int
)

Insert Into Certificates
Values
('Standart',65,84),
('Honour',85,94),
('High Honour',95,100)

--Non Equal Join
Select * From Students
Join Certificates
on Students.Score Between Certificates.Min And Certificates.Max
Join Groups
On Students.GroupId = Groups.Id

Create table Positions
(
	Id int Identity Primary Key,
	Name nvarchar(100),
	ParentId int Foreign Key References Positions(Id)
)

--Self Join
Select P.Name UstVezife,C.Name AltVezife From Positions p
Join Positions c
On p.Id = c.ParentId

Create Table Products
(
	Id int Identity Primary Key,
	Name nvarchar(100)
)

Create Table Sizes
(
	Id int Identity Primary Key,
	Name nvarchar(100)
)

Create Table Colors
(
	Id int Identity Primary Key,
	Name nvarchar(100)
)

Insert Into Colors
Values
('Red'),
('Green'),
('Yellow')

Select * From Sizes
cross join Products

Create Table ProductColorAndSizes
(
	Id Int identity Primary Key,
	ColorId int Foreign Key references Colors(Id),
	SizeId int Foreign Key references Sizes(Id),
	ProductId int Foreign Key references Products(Id),
	Count int,
	Price money
)

Select 
p.Name as [Mehsulun Adi],
c.Name as [reng],
s.Name as [Olcusu],
pcs.Count as [Say],
pcs.Price as [Qiymet]
From Products p
Join ProductColorAndSizes pcs
On pcs.ProductId = p.Id
Join Colors c
On c.Id = pcs.ColorId
Join Sizes s
On s.Id = pcs.SizeId