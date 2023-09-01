CREATE TABLE Persons
(
	PersonID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	FirstName NVARCHAR(50),
	Salary DECIMAL(10,2),
	PassportID INT,
)

CREATE TABLE Passports
(
	PassportID INT NOT NULL PRIMARY KEY,
	PassportNumber CHAR(8),
);

INSERT INTO Persons(FirstName, Salary, PassportID)
	VALUES
	('Roberto', 43300.00, 102),
	('Tom', 56100.00, 103),
	('Yana', 60200.00, 101);

INSERT INTO Passports(PassportID, PassportNumber)
	VALUES
	(101, 'N34FG21B'),
	(102, 'K65LO4R7'),
	(103, 'ZE657QP2');

ALTER TABLE Persons
ADD FOREIGN KEY (PassportID) REFERENCES Passports(PassportID);