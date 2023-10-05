--Select the first 5 boardgames that have rating, bigger than 7.00 
--and containing the letter 'a' in the boardgame name or the rating of a boardgame 
--is bigger than 7.50 and the range of the min and max count of players is [2;5]. 
--Order the result by boardgames name (ascending), then by rating (descending).
--Required columns:
--•	Name
--•	Rating
--•	CategoryName


SELECT TOP 5 b.Name, b.Rating, c.Name as [CategoryName]
FROM Boardgames as b
LEFT JOIN Categories as c on c.Id = b.CategoryId
LEFT JOIN PlayersRanges as pr on b.PlayersRangeId = pr.Id
WHERE (b.Rating > 7.00 AND CHARINDEX('a', b.Name) > 0)
    OR
    (b.Rating > 7.50 AND pr.PlayersMin >= 2 AND pr.PlayersMax <= 5)
ORDER BY
    b.Name ASC,
    b.Rating DESC