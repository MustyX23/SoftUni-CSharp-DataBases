SELECT 
    FORMAT(ArrivalDate, 'yyyy-MM-dd') as [ArrivalDate],
    AdultsCount,
    ChildrenCount
FROM Bookings
ORDER BY 
    (SELECT Price FROM Rooms WHERE Rooms.Id = Bookings.RoomId) DESC,
    ArrivalDate ASC;