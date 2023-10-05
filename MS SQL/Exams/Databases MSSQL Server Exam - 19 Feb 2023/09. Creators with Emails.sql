WITH RankedBoardGames AS (
    SELECT
        CONCAT(c.FirstName, ' ', c.LastName) AS [FullName],
        c.Email,
        b.Rating,
        ROW_NUMBER() OVER (PARTITION BY CONCAT(c.FirstName, ' ', c.LastName) ORDER BY b.Rating DESC) AS rn
    FROM
        Creators c
     JOIN
        CreatorsBoardgames cb ON c.Id = cb.CreatorId
     LEFT JOIN
        Boardgames b ON b.Id = cb.BoardgameId
    WHERE
        c.Email LIKE '%.com'
)
SELECT
    [FullName],
    Email,
    Rating
FROM
    RankedBoardGames
WHERE
    rn = 1
ORDER BY
    [FullName] ASC;