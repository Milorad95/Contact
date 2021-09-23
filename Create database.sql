USE master
go

create database Contact
GO

USE Contact
GO

CREATE TABLE Contact
(
	ContactID int primary key identity(1,1),
	FirstName nvarchar(50),
	LastName nvarchar(50),
	Email nvarchar(50),
	MobilePhone nvarchar(50)
)

INSERT INTO Contact
VALUES ('Pera','Peric','pera@mail.com','011123456')

INSERT INTO Contact
VALUES ('Milorad','Komljenovic','miloradk@gmail.com','063567352')

INSERT INTO Contact
VALUES ('Vladan', 'Mirkovic','vladanm9@gmail.com','0655457199')

INSERT INTO Contact
VALUES ('Mileva','Markovic','markovic@gmail.com','0655522111')

INSERT INTO Contact
VALUES ('Boris','Katanic','katanicb@yahoo.com','0623355879')

select * from Contact