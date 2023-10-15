SELECT 
    h.Id,
    h.Name
FROM Hotels h
JOIN HotelsRooms as hr on h.Id = hr.HotelId
JOIN Rooms r ON hr.RoomId = r.Id
JOIN Bookings b ON h.Id = b.HotelId
WHERE r.Type = 'VIP Apartment'
GROUP BY h.Id, h.Name
ORDER BY COUNT(b.Id) DESC;


