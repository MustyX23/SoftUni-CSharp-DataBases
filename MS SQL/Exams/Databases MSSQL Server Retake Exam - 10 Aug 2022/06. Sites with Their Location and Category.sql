SELECT s.Name as [Site], l.Name as [Location], s.Establishment, c.Name as [Category]
FROM Sites as s
JOIN Locations as l on s.LocationId = l.Id
JOIN Categories as c on s.CategoryId = c.Id
ORDER BY c.Name DESC, l.Name, s.Name
