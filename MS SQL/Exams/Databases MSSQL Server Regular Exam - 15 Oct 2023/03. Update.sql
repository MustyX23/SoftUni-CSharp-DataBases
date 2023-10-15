UPDATE Bookings
SET DepartureDate = DATEADD(day, 1, DepartureDate)
WHERE YEAR(ArrivalDate) = 2023 AND MONTH(ArrivalDate) = 12;

UPDATE Tourists
SET Email = NULL
WHERE Name LIKE '%MA%';