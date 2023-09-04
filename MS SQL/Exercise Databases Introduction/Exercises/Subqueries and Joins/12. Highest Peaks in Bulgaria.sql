SELECT c.CountryCode, m.MountainRange, p.PeakName, p.Elevation
FROM Countries AS c
	LEFT JOIN MountainsCountries as mc 
		ON mc.CountryCode = c.CountryCode
	LEFT JOIN Mountains as m 
		ON mc.MountainId = m.Id
	LEFT JOIN Peaks as p 
		ON p.MountainId = m.Id
WHERE p.Elevation > 2835 AND c.CountryCode = 'BG'
ORDER BY p.Elevation DESC