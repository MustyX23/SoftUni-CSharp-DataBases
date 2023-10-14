SELECT 
	SUBSTRING(t.Name, CHARINDEX(' ', t.Name) + 1, LEN(t.Name)) AS LastName
	,t.Nationality
	,t.Age
	,t.PhoneNumber
FROM Tourists as t
JOIN SitesTourists as st on t.Id = st.TouristId
JOIN Sites as s on st.SiteId = s.Id
JOIN Categories as c on c.Id = s.CategoryId
WHERE c.Name = 'History and archaeology'
GROUP BY SUBSTRING(t.Name, CHARINDEX(' ', t.Name) + 1, LEN(t.Name))
	,t.Nationality
	,t.Age
	,t.PhoneNumber
ORDER BY LastName
