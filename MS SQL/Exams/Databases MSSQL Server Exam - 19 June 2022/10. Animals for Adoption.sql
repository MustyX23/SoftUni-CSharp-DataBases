SELECT A.Name, YEAR(A.BirthDate) as [BirthYear], AT.AnimalType
FROM Animals A
JOIN AnimalTypes AT ON A.AnimalTypeId = AT.Id
WHERE A.OwnerId IS NULL 
AND AT.AnimalType != 'Birds' 
AND A.BirthDate > DATEADD(year, -4, '2022-01-01')
ORDER BY A.Name;
