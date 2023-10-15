SELECT Animals.Name, AnimalTypes.AnimalType, FORMAT(Animals.BirthDate, 'dd.MM.yyyy') AS FormattedBirthDate
FROM Animals
JOIN AnimalTypes ON Animals.AnimalTypeId = AnimalTypes.Id
ORDER BY Animals.Name ASC;