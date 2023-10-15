SELECT 
    h.Name as HotelName,
    SUM(DATEDIFF(day, b.ArrivalDate, b.DepartureDate) * r.Price) as TotalRevenue
FROM Bookings b
JOIN Hotels h ON b.HotelId = h.Id
JOIN Rooms r ON b.RoomId = r.Id
GROUP BY h.Name
ORDER BY TotalRevenue DESC;
