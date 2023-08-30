CREATE TABLE Directors
(
	Id int PRIMARY KEY IDENTITY(1,1),
	DirectorName nvarchar(30) NOT NULL,
	Notes text
); 

INSERT INTO Directors(DirectorName, Notes)
Values
	('James Cameron', 'Famous for directing Titanic and Avatar.'),
	('Christopher Nolan', 'Known for movies like Inception and The Dark Knight trilogy.'),
    ('Quentin Tarantino', 'Director of Pulp Fiction and Kill Bill.'),
    ('Steven Spielberg', 'Director of classics like E.T. and Jurassic Park.'),
    ('Greta Gerwig', 'Director of Lady Bird and Little Women.');

CREATE TABLE Genres
(
	Id int PRIMARY KEY IDENTITY(1,1),
	GenreName nvarchar(30) NOT NULL,
	Notes text
);

INSERT INTO Genres (GenreName, Notes)
VALUES
    ('Action', 'Genre known for its thrilling and high-energy movies.'),
    ('Drama', 'Genre focusing on emotional and character-driven stories.'),
    ('Comedy', 'Genre intended to entertain and make the audience laugh.'),
    ('Science Fiction', 'Genre that explores futuristic concepts and technology.'),
    ('Adventure', 'Genre centered around exciting journeys and quests.');

CREATE TABLE Categories
(
	Id int PRIMARY KEY IDENTITY(1,1),
	CategorieName nvarchar(30) NOT NULL,
	Notes text
);

INSERT INTO Categories(CategorieName, Notes)
VALUES
	('Action-Adventure', 'Movies that combine action and adventure elements.'),
    ('Drama-Romance', 'Movies blending dramatic and romantic themes.'),
    ('Comedy-Drama', 'Movies that mix comedic and dramatic elements.'),
    ('Science Fiction-Fantasy', 'Movies that explore both science fiction and fantasy concepts.'),
    ('Thriller-Mystery', 'Movies with suspenseful and mystery-driven narratives.');

CREATE TABLE MOVIES
(
	Id int PRIMARY KEY IDENTITY(1,1),
	TITLE NVARCHAR(100) NOT NULL,
	DirectorId INT NOT NULL,
	CopyrightYear INT NOT NULL, --Might be Date
    Length INT NOT NULL,
    GenreId INT NOT NULL,
    CategoryId INT NOT NULL,
    Rating DECIMAL(3, 1),
    Notes NVARCHAR(MAX),
	FOREIGN KEY (DirectorId) REFERENCES Directors(Id),
	FOREIGN KEY (GenreId) REFERENCES Genres(Id),
	FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);

INSERT INTO Movies (Title, DirectorId, CopyrightYear, Length, GenreId, CategoryId, Rating, Notes)
VALUES
    ('Avatar', 1, 2009, 162, 1, 1, 7.8, 'Science fiction film directed by James Cameron.'),
    ('Inception', 2, 2010, 148, 4, 1, 8.8, 'Mind-bending thriller directed by Christopher Nolan.'),
    ('Pulp Fiction', 3, 1994, 154, 3, 5, 8.9, 'Cult classic directed by Quentin Tarantino.'),
    ('E.T. the Extra-Terrestrial', 4, 1982, 115, 2, 2, 7.8, 'Family-friendly movie directed by Steven Spielberg.'),
    ('Lady Bird', 5, 2017, 94, 2, 3, 7.4, 'Coming-of-age drama directed by Greta Gerwig.');


SELECT * FROM Directors
SELECT * FROM Genres
SELECT * FROM Categories
SELECT * FROM Movies