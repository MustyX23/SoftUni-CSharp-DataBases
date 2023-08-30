-- Create Users Table
CREATE TABLE Users
(
	Id BIGINT  PRIMARY KEY IDENTITY (1,1),
	Username VARCHAR(30) NOT NULL,
	Password VARCHAR(26) NOT NULL,
	ProfilePicture VARBINARY(MAX), --900 BYTE
	LastLoginTime DATETIME,
	IsDeleted BIT NOT NULL DEFAULT 0 
);

-- Insert 5 records
INSERT INTO Users (Username, Password, ProfilePicture, LastLoginTime, IsDeleted)
VALUES
('firstUser', 'password1', NULL, '2023-08-10 08:30:00', 0),
('secondUser', 'password2', NULL, '2023-08-11 14:45:00', 1),
('thirdUser', 'password3', NULL, '2023-08-09 18:15:00', 0),
('fourthUser', 'password4', NULL, '2023-08-12 10:00:00', 0),
('fifthUser', 'password5', NULL, '2023-08-08 12:20:00', 1);