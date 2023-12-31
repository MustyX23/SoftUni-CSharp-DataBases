SELECT TOP 5
	c.CountryName,
	r.RiverName
FROM Countries AS c
LEFT JOIN CountriesRivers as cr 
	ON c.CountryCode = cr.CountryCode
LEFT JOIN Rivers as r 
	ON r.Id = cr.RiverId
WHERE c.ContinentCode = 'AF'
ORDER BY c.CountryName