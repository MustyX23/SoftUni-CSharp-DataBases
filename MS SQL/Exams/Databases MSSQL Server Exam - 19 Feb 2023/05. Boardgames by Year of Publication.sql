--Select all boardgames, ordered by year of publication (ascending), then by name (descending). 
--Required columns:
--•	Name
--•	Rating

SELECT Name, Rating
FROM Boardgames
ORDER BY YearPublished, Name DESC