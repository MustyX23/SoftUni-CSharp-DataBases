-- Delete related bookings first to avoid foreign key constraint conflicts
DELETE FROM Bookings
WHERE TouristId IN (SELECT Id FROM Tourists WHERE Name LIKE '%Smith%');

-- Delete tourists with the name containing "Smith"
DELETE FROM Tourists
WHERE Name LIKE '%Smith%';