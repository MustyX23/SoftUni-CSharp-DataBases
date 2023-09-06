SELECT e.DepartmentID, MIN(e.Salary) AS [MinimumSalary]
FROM Employees AS e
WHERE e.HireDate > '01/01/2000'
GROUP BY e.DepartmentID
HAVING e.DepartmentID IN (2, 5, 7)