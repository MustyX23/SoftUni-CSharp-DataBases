SELECT 
    s.Name AS SiteName,
    l.Name AS LocationName,
    l.Municipality,
    l.Province,
    s.Establishment
FROM 
    Sites s
JOIN 
    Locations l ON s.LocationId = l.Id
WHERE 
    (l.Name NOT LIKE 'B%' AND l.Name NOT LIKE 'M%' AND l.Name NOT LIKE 'D%')
    AND s.Establishment LIKE '% BC'
ORDER BY 
    SiteName ASC;