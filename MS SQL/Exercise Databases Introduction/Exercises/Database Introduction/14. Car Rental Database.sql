CREATE TABLE Categories
(
	Id int PRIMARY KEY IDENTITY(1,1),
	CategoryName NVARCHAR(50) NOT NULL,
	DailyRate DECIMAL(10, 2) NOT NULL,
    WeeklyRate DECIMAL(10, 2) NOT NULL,
    MonthlyRate DECIMAL(10, 2) NOT NULL,
    WeekendRate DECIMAL(10, 2) NOT NULL
);

INSERT INTO Categories(CategoryName, DailyRate, WeeklyRate, MonthlyRate, WeekendRate)
VALUES
	('Economy', 30.00, 180.00, 600.00, 50.00),
    ('Compact', 40.00, 240.00, 800.00, 60.00),
    ('SUV', 60.00, 360.00, 1200.00, 90.00);


CREATE TABLE Cars
(
	Id int PRIMARY KEY IDENTITY(1,1),
	PlateNumber NVARCHAR(15) NOT NULL,
	Manufacturer NVARCHAR(50) NOT NULL,
	Model NVARCHAR(50) NOT NULL,
	CarYear INT NOT NULL,
	CategoryId INT NOT NULL,
	Doors INT NOT NULL,
	Picture VARBINARY(MAX),
	Condition NVARCHAR(50),
	Available BIT NOT NULL,
	FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);

INSERT INTO Cars(PlateNumber, Manufacturer, Model, CarYear, CategoryId, Doors, Picture, Condition, Available)
VALUES
	('ABC123', 'Toyota', 'Corolla', 2022, 2, 4, NULL, 'Excellent', 1),
    ('XYZ456', 'Honda', 'Civic', 2021, 2, 4, NULL, 'Very good', 1),
    ('DEF789', 'Ford', 'Escape', 2020, 3, 4, NULL, 'Good', 1);

CREATE TABLE Employees 
(
	Id INT PRIMARY KEY IDENTITY (1,1),
	FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Title NVARCHAR(50),
    Notes NVARCHAR(MAX)
);

INSERT INTO Employees(FirstName, LastName, Title, Notes)
VALUES
	('John', 'Doe', 'Manager', NULL),
    ('Jane', 'Smith', 'Sales Associate', NULL),
    ('Michael', 'Johnson', 'Rental Agent', NULL);

CREATE TABLE Customers
(
	Id INT PRIMARY KEY IDENTITY (1,1),
	DriverLicenceNumber NVARCHAR(20) NOT NULL,
	FullName NVARCHAR(50) NOT NULL,
	Address NVARCHAR(100) NOT NULL,
    City NVARCHAR(50) NOT NULL,
    ZIPCode NVARCHAR(10) NOT NULL,
    Notes NVARCHAR(MAX)
);

INSERT INTO Customers(DriverLicenceNumber, FullName, Address, City, ZIPCode, Notes)
VALUES
	('DL12345', 'Alice Johnson', '123 Main St', 'Cityville', '12345', NULL),
    ('DL67890', 'Bob Smith', '456 Elm St', 'Towntown', '67890', NULL),
    ('DL54321', 'Eve Williams', '789 Oak St', 'Villageville', '54321', NULL);


CREATE TABLE RentalOrders
(
	Id INT PRIMARY KEY IDENTITY (1,1),
	EmployeeId INT NOT NULL,
	CustomerId INT NOT NULL,
	CarId INT NOT NULL,
	TankLevel INT,
	KilometrageStart INT,
	KilometrageEnd INT,
	TotalKilometrage INT,
	StartDate DATETIME,
	EndDate DATETIME,
	TotalDays INT NOT NULL,
	RateApplied DECIMAL (10,2) NOT NULL,
	TaxRate DECIMAL(5, 2) NOT NULL,
	OrderStatus NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX),
	FOREIGN KEY (EmployeeId) REFERENCES Employees(Id),
	FOREIGN KEY (CustomerId) REFERENCES Customers(Id),
	FOREIGN KEY (CarId) REFERENCES Cars(Id)
);

INSERT INTO RentalOrders(EmployeeId, CustomerId, CarId, TankLevel, KilometrageStart, KilometrageEnd, TotalKilometrage, StartDate, EndDate, TotalDays, RateApplied, TaxRate, OrderStatus, Notes)
VALUES
	(3, 1, 2, 75, 5000, 5200, 200, '2023-08-01', '2023-08-05', 5, 240.00, 0.1, 'Completed', NULL),
    (2, 2, 1, 85, 7000, 7200, 200, '2023-08-02', '2023-08-06', 4, 220.00, 0.1, 'Completed', NULL),
    (1, 3, 3, 90, 6000, 6200, 200, '2023-08-03', '2023-08-07', 4, 320.00, 0.1, 'Completed', NULL);