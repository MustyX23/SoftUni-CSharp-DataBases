SELECT 
    CONCAT(o.Name, '-', a.Name) AS OwnerAnimal,
    o.PhoneNumber,
    ac.CageId
FROM Owners o
JOIN Animals a ON o.Id = a.OwnerId
JOIN AnimalTypes at ON a.AnimalTypeId = at.Id
JOIN AnimalsCages ac ON a.Id = ac.AnimalId
JOIN Cages c ON ac.CageId = c.Id
WHERE at.AnimalType = 'Mammals'
ORDER BY o.Name ASC, a.Name DESC;
