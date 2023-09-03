SELECT e.EmployeeID, e.FirstName, e.ManagerID, m.FirstName
FROM Employees AS e
LEFT JOIN Employees AS m 
	ON e.ManagerID = m.EmployeeID
WHERE e.ManagerID IN (3, 7) 
ORDER BY EmployeeID