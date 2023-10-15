CREATE FUNCTION udf_RoomsWithTourists
(
    @name NVARCHAR(40)
)
RETURNS INT
AS
BEGIN
    DECLARE @TotalTourists INT;
    SELECT @TotalTourists = SUM(AdultsCount + ChildrenCount)
    FROM Bookings b
    JOIN Rooms r ON b.RoomId = r.Id
    WHERE r.Type = @name;
    RETURN @TotalTourists;
END;