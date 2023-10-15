SELECT TOP 5 o.Name as [Owner], COUNT(*) as [CountOfAnimals]
FROM Owners as o
JOIN Animals as a on a.OwnerId = o.Id
GROUP BY o.Name
ORDER BY CountOfAnimals DESC, o.Name