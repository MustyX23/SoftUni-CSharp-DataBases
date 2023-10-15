-- Create Owners table
CREATE TABLE Owners (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    PhoneNumber VARCHAR(15) NOT NULL,
    Address VARCHAR(50)
);

-- Create AnimalTypes table
CREATE TABLE AnimalTypes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AnimalType VARCHAR(30) NOT NULL
);

-- Create Cages table
CREATE TABLE Cages (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AnimalTypeId INT NOT NULL,
    CONSTRAINT FK_Cages_AnimalTypes FOREIGN KEY (AnimalTypeId) REFERENCES AnimalTypes(Id)
);

-- Create Animals table
CREATE TABLE Animals (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(30) NOT NULL,
    BirthDate DATE NOT NULL,
    OwnerId INT,
    AnimalTypeId INT NOT NULL,
    CONSTRAINT FK_Animals_Owners FOREIGN KEY (OwnerId) REFERENCES Owners(Id),
    CONSTRAINT FK_Animals_AnimalTypes FOREIGN KEY (AnimalTypeId) REFERENCES AnimalTypes(Id)
);

-- Create AnimalsCages table
CREATE TABLE AnimalsCages (
    CageId INT NOT NULL,
    AnimalId INT NOT NULL,
    PRIMARY KEY (CageId, AnimalId),
    CONSTRAINT FK_AnimalsCages_Cages FOREIGN KEY (CageId) REFERENCES Cages(Id),
    CONSTRAINT FK_AnimalsCages_Animals FOREIGN KEY (AnimalId) REFERENCES Animals(Id)
);

-- Create VolunteersDepartments table
CREATE TABLE VolunteersDepartments (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DepartmentName VARCHAR(30) NOT NULL
);

-- Create Volunteers table
CREATE TABLE Volunteers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    PhoneNumber VARCHAR(15) NOT NULL,
    Address VARCHAR(50),
    AnimalId INT,
    DepartmentId INT NOT NULL,
    CONSTRAINT FK_Volunteers_Animals FOREIGN KEY (AnimalId) REFERENCES Animals(Id),
    CONSTRAINT FK_Volunteers_VolunteersDepartments FOREIGN KEY (DepartmentId) REFERENCES VolunteersDepartments(Id)
);
