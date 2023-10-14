SELECT 
    t.Name AS TouristName,
    t.Age,
    t.PhoneNumber,
    t.Nationality,
    ISNULL(bp.Name, '(no bonus prize)') AS BonusPrize
FROM 
    Tourists t
LEFT JOIN 
    TouristsBonusPrizes tbp ON t.Id = tbp.TouristId
LEFT JOIN 
    BonusPrizes bp ON tbp.BonusPrizeId = bp.Id
ORDER BY 
    TouristName ASC;
