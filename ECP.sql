use master;
go
drop database if exists ECP;
create database ECP;
go
use ECP;
go

set dateformat dmy;

create table Users(
	UserId nvarchar(20) primary key not null,
	Name nvarchar(256) not null,
	Description nvarchar(max) not null,
	Password nvarchar(20) not null,
	Permissions int not null default 1,
);

create table Events(
	EventId int primary key identity(1,1) not null,
	StartDate datetime not null,
	EndDate datetime not null,
	NeedsVolunteers bit not null default 0,
	Description nvarchar(max) not null,
	OrganizerId_FK nvarchar(20) foreign key references Users(UserId) not null,
);

create table Tasks(
	TaskId int primary key identity(1,1) not null,
	Description nvarchar(max) not null,
	EventId_FK int foreign key references Events(EventId) not null,
);

create table Candidates(
	primary key(VolunteerId_FK, TaskId_FK),
	VolunteerId_FK nvarchar(20) foreign key references Users(UserId) not null,
	TaskId_FK int foreign key references Tasks(TaskId) not null,
);

create table Assignments(
	primary key(VolunteerId_FK, TaskId_FK),
	VolunteerId_FK nvarchar(20) foreign key references Users(UserId) not null,
	TaskId_FK int foreign key references Tasks(TaskId) not null,
);

insert into Users values 
	('TestId', 'Test', 'This is a test', '1234', 2),
	('TestId2', 'Test', 'This is a test', '1234', 1);

insert into Events(StartDate, EndDate, NeedsVolunteers, Description, OrganizerId_FK) values 
	('31/05/2023 12:30:00.000', '31/05/2023 13:00:00.000', 1, 'Walk & Talk', 'TestId');

insert into Tasks(Description, EventId_FK) values
	('Gå', 1);

insert into Candidates(VolunteerId_FK, TaskId_FK) values
	('TestId2', 1);

insert into Assignments(VolunteerId_FK, TaskId_FK) values
	('TestId', 1);