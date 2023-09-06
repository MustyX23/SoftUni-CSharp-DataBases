SELECT d.DepartmentID, SUM(e.Salary) AS TotalSalary
FROM Departments as d
JOIN Employees as e ON e.DepartmentID = d.DepartmentID
GROUP BY d.DepartmentID
ORDER BY d.DepartmentID