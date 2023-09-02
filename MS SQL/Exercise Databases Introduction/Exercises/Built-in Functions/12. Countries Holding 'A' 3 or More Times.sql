SELECT CountryName as [Country Name], IsoCode as [ISO Code]
FROM Countries
WHERE LOWER(CountryName) LIKE '%a%a%a%'
ORDER BY [ISO Code]