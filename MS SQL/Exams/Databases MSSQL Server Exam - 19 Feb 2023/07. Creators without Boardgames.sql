SELECT c.Id, CONCAT(c.FirstName, ' ', c.LastName) AS [CreatorName], c.Email
FROM Creators as c
LEFT JOIN CreatorsBoardgames as cb on c.Id = cb.CreatorId
WHERE cb.CreatorId IS NULL
