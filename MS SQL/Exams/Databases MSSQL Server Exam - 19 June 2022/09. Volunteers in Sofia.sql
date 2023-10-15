SELECT Name, PhoneNumber, SUBSTRING(Address, CHARINDEX(',', Address) + 2, LEN(Address)) AS Address
FROM Volunteers
WHERE DepartmentId = 2
AND Address LIKE '%Sofia%'
ORDER BY Name ASC;
