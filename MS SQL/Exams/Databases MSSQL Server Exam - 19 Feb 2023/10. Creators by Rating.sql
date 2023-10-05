SELECT
    c.LastName,
    CEILING(AVG(bg.Rating)) AS AverageRating,
    p.Name AS PublisherName
FROM
    Creators c
LEFT JOIN
    CreatorsBoardgames cb ON c.Id = cb.CreatorId
LEFT JOIN
    Boardgames bg ON cb.BoardgameId = bg.Id
LEFT JOIN
    Publishers p ON bg.PublisherId = p.Id
WHERE
    p.Name = 'Stonemaier Games'
GROUP BY
    c.LastName, p.Name
ORDER BY
    (AVG(bg.Rating)) DESC; 