CREATE TABLE People
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Name NVARCHAR(200) NOT NULL,
	Picture VARBINARY(MAX),
	Height DECIMAL (5, 2),
	Weight DECIMAL (5,2),
	Gender CHAR(1) NOT NULL,
	Birthdate DATE NOT NULL,
	Biography NVARCHAR(MAX),
	CONSTRAINT CK_Gender CHECK (Gender IN('m' , 'f'))
);

INSERT INTO People(Name, Picture, Height, Weight, Gender, Birthdate, Biography)
VALUES
	('Naruto', NULL, 1.72, 60.2, 'm', '1990-05-15' , 'Naruto Uzumaki is an amazing ninja - RAAASEEENGAAAAN!'),
	('Sasuke', NULL, 1.65, 60.2, 'm', '1988-11-30', 'Uchiha Sasuke is an obtainer of a Mangekyo Sharingan - Suusaanoooo!'),
	('Sakura', NULL, 1.50, 59.29, 'f', '1991-11-30', 'Sakura loves Sasuke and hates Naruto (weakest ninja)'),
	('Kakashi Hatake', NULL, 1.90, 70, 'm', '1970-05-13', 'Hatake Kakashi loves reading Jiraya sensei''s books'),
	('Jiraya', NULL, 1.80, 90, 'm', '1960-03-23', 'Jiraya loves drinking sake and has feelings for Tsunade');


SELECT * FROM People