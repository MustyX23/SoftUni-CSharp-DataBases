SELECT l.Province, l.Municipality, l.Name as [Location], COUNT(s.Id) as [CountOfSites]
FROM Locations as l
JOIN Sites as s on l.Id = s.LocationId
WHERE l.Province = 'Sofia'
GROUP BY 
    l.Province, l.Municipality, l.Name
ORDER BY 
	COUNT(s.Id) DESC, l.Name
