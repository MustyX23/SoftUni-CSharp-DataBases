SELECT 
    Id,
    Name,
    PhoneNumber
FROM Tourists
WHERE Id NOT IN (SELECT DISTINCT TouristId FROM Bookings)
ORDER BY Name ASC;
