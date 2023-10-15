CREATE PROCEDURE usp_SearchByCountry
    @country NVARCHAR(50)
AS
BEGIN
    SELECT 
        t.Name,
        t.PhoneNumber,
        t.Email,
        COUNT(b.Id) as CountOfBookings
    FROM Tourists t
    JOIN Bookings b ON t.Id = b.TouristId
    JOIN Countries c ON t.CountryId = c.Id
    WHERE c.Name = @country
    GROUP BY t.Name, t.PhoneNumber, t.Email
    ORDER BY t.Name ASC, CountOfBookings DESC;
END;

EXEC usp_SearchByCountry 'Greece'