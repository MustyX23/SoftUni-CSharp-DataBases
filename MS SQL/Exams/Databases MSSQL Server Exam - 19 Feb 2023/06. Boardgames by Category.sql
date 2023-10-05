--Select all boardgames with "Strategy Games" or "Wargames" categories. Order results by YearPublished (descending).
--Required columns:
--•	Id
--•	Name
--•	YearPublished
--•	CategoryName

SELECT b.Id, b.Name, b.YearPublished, c.Name as [CategoryName]
FROM Boardgames AS b
JOIN Categories as c ON c.Id = b.CategoryId
WHERE c.Name IN ('Strategy Games', 'Wargames')
ORDER BY YearPublished DESC