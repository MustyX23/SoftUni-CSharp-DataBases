-- Creating table Categories
CREATE TABLE Categories (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(50) NOT NULL
);

-- Creating table Locations
CREATE TABLE Locations (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    Municipality VARCHAR(50),
    Province VARCHAR(50)
);

-- Creating table Sites
CREATE TABLE Sites (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    LocationId INT NOT NULL,
    CategoryId INT NOT NULL,
    Establishment VARCHAR(15),
    FOREIGN KEY (LocationId) REFERENCES Locations(Id),
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);

-- Creating table Tourists
CREATE TABLE Tourists (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    Age INT NOT NULL CHECK (Age >= 0 AND Age <= 120),
    PhoneNumber VARCHAR(20) NOT NULL,
    Nationality VARCHAR(30) NOT NULL,
    Reward VARCHAR(20)
);

-- Creating table SitesTourists
CREATE TABLE SitesTourists (
    TouristId INT NOT NULL,
    SiteId INT NOT NULL,
    PRIMARY KEY (TouristId, SiteId),
    FOREIGN KEY (TouristId) REFERENCES Tourists(Id),
    FOREIGN KEY (SiteId) REFERENCES Sites(Id)
);
-- Creating table BonusPrizes
CREATE TABLE BonusPrizes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(50) NOT NULL
);

-- Creating table TouristsBonusPrizes
CREATE TABLE TouristsBonusPrizes (
    TouristId INT NOT NULL,
    BonusPrizeId INT NOT NULL,
    PRIMARY KEY (TouristId, BonusPrizeId),
    FOREIGN KEY (TouristId) REFERENCES Tourists(Id),
    FOREIGN KEY (BonusPrizeId) REFERENCES BonusPrizes(Id)
);