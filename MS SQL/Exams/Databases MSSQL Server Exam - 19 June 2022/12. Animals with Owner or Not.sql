CREATE PROCEDURE usp_AnimalsWithOwnersOrNot
    @AnimalName VARCHAR(100)
AS
BEGIN
    DECLARE @OwnerName VARCHAR(50);

    SELECT @OwnerName = CASE
                            WHEN o.Name IS NOT NULL THEN o.Name
                            ELSE 'For adoption'
                        END
    FROM Animals a
    LEFT JOIN Owners o ON a.OwnerId = o.Id
    WHERE a.Name = @AnimalName;

    SELECT @AnimalName AS [Name], @OwnerName AS OwnerName;
END;
