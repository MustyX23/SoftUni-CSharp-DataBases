SELECT TOP 3 e.EmployeeID, e.FirstName 
FROM Employees AS e
FULL OUTER JOIN EmployeesProjects as ep
	ON e.EmployeeID = ep.EmployeeID
WHERE ep.EmployeeID IS NULL
ORDER BY e.EmployeeID