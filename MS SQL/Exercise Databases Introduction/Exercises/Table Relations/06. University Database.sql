CREATE TABLE Students
(
	StudentID INT PRIMARY KEY,
	StudentNumber NVARCHAR(50),
	StudentName NVARCHAR(50),
	MajorID INT, --FOREIGN KEY
)

CREATE TABLE Majors
(
	MajorID INT PRIMARY KEY, --PRIMARY KEY
	Name NVARCHAR(50)
)

CREATE TABLE Agenda
(
	StudentID INT NOT NULL,
	SubjectID INT NOT NULL,
	PRIMARY KEY(StudentID, SubjectID)
)

CREATE TABLE Subjects
(
	SubjectID INT PRIMARY KEY,
	SubjectName NVARCHAR(50)
)

CREATE TABLE Payments
(
	PaymentID INT PRIMARY KEY,
	PaymentDate DATETIME,
	PaymentAmount DECIMAL (10,2),
	StudentID INT NOT NULL
)

ALTER TABLE Students
ADD FOREIGN KEY (MajorID) REFERENCES Majors(MajorID)

ALTER TABLE Agenda
ADD
FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
FOREIGN KEY(SubjectID) REFERENCES Subjects(SubjectID)

ALTER TABLE Payments
ADD
FOREIGN KEY (StudentID) REFERENCES Students(StudentID)