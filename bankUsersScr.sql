use master 
go

if exists(select * from sys.databases where name = 'bankUsers')
drop database bankUsers

create database bankUsers
go

use bankUsers
go

create table tbUsers
(
Id int primary key identity (1,1),
name nvarchar(25),
mainSurname nvarchar(25),
secondarySurname nvarchar(25),
degreeBeforeName nvarchar(10),
degreeAfterName nvarchar(10),
homeAddress nvarchar(100),
organizationalUnit nvarchar(50),
telNumberWork nvarchar(24),
telNumberPrivate nvarchar(24),
emailWork nvarchar(50),
emailPrivate nvarchar(50),
employmentFrom datetime,
employmentTo datetime,
maternityOrParentalLeave bit
)