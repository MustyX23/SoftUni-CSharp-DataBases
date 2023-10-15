SELECT 
    h.Name as HotelName,
    r.Price as RoomPrice
FROM Tourists t
JOIN Bookings b ON t.Id = b.TouristId
JOIN Hotels h ON b.HotelId = h.Id
JOIN Rooms r ON b.RoomId = r.Id
WHERE t.Name NOT LIKE '%EZ'
ORDER BY r.Price DESC;