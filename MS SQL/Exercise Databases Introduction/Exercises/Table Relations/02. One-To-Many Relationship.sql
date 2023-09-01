CREATE TABLE Models
(
	ModelID INT NOT NULL PRIMARY KEY,
	Name NVARCHAR(50),
	ManufacturerID INT
)

CREATE TABLE Manufacturers
(
	ManufacturerID INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	Name NVARCHAR(50),
	EstablishedOn DATETIME
)

INSERT INTO Models(ModelID, Name, ManufacturerID)
	VALUES
	(101, 'X1', 1),
	(102, 'i6', 1),
	(103, 'Model S', 2),
	(104, 'Model X', 2),
	(105, 'Model 3', 2),
	(106, 'Nova', 3)

INSERT INTO Manufacturers(Name, EstablishedOn)
	VALUES
	('BMW', '07/03/1916'),
	('Tesla', '07/03/1916'),
	('Lada', '07/03/1916')

ALTER TABLE Models
ADD FOREIGN KEY (ManufacturerID) REFERENCES Manufacturers(ManufacturerID);
