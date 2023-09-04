SELECT c.CountryCode, COUNT(m.MountainRange) AS [MountainRanges]
FROM Countries AS c
JOIN MountainsCountries as mc ON mc.CountryCode = c.CountryCode
JOIN Mountains as m ON m.Id = mc.MountainId
WHERE c.CountryName in ('United States', 'Russia', 'Bulgaria')
GROUP BY c.CountryCode