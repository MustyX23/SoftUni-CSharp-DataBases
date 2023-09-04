SELECT COUNT(c.CountryCode) AS [Count]
FROM Countries as c
LEFT JOIN MountainsCountries as mc ON c.CountryCode = mc.CountryCode
LEFT JOIN Mountains as m ON mc.MountainId = M.Id
WHERE mc.CountryCode IS NULL