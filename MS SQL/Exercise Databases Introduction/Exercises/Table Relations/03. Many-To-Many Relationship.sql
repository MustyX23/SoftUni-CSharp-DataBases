CREATE TABLE Students
(
	StudentID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(50)
)

INSERT INTO Students(Name)
	VALUES
	('Mila'),
	('Toni'),
	('Ron')

CREATE TABLE Exams
(
	ExamID INT NOT NULL PRIMARY KEY,
	Name NVARCHAR(50)
)

INSERT INTO Exams(ExamID, Name)
	VALUES
	(101, 'SpringMVC'),
	(102, 'Neo4j'),
	(103, 'Oracle 11g')

CREATE TABLE StudentsExams
(
	StudentID INT,
	ExamID INT
	PRIMARY KEY(StudentID, ExamID),
	FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
	FOREIGN KEY(ExamID) REFERENCES Exams(ExamID)
)

INSERT INTO StudentsExams (StudentID, ExamID)
	VALUES
	(1, 101),
	(1, 102),
	(2, 101),
	(2, 102),
	(3, 103),
	(2, 103);