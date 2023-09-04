SELECT TOP 5
	c.CountryName, 
	MAX(p.Elevation) AS [HighestPeakElevation],
	MAX(r.Length) AS [LongestRiverLength]
FROM Countries AS c
	LEFT JOIN MountainsCountries as mc ON c.CountryCode = mc.CountryCode
	LEFT JOIN Mountains as m ON mc.MountainId = m.Id
	LEFT JOIN Peaks as p ON p.MountainId = m.Id
	LEFT JOIN CountriesRivers as cr ON c.CountryCode = cr.CountryCode
	LEFT JOIN Rivers as r ON r.Id = cr.RiverId
	GROUP BY c.CountryName
	ORDER BY
		HighestPeakElevation DESC,
		c.CountryName



