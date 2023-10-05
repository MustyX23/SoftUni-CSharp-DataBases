-- Create the function
CREATE FUNCTION udf_CreatorWithBoardgames (@firstName NVARCHAR(255))
RETURNS INT
AS
BEGIN
    DECLARE @TotalBoardgames INT

    SELECT @TotalBoardgames = COUNT(*)
    FROM Creators c
    INNER JOIN CreatorsBoardgames cb ON c.Id = cb.CreatorId
    INNER JOIN Boardgames bg ON cb.BoardgameId = bg.Id
    WHERE c.FirstName = @firstName

    RETURN @TotalBoardgames
END;
