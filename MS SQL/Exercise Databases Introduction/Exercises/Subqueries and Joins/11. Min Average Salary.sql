SELECT MIN(AverageSalary) AS [MinAverageSalary]
FROM 
(	SELECT AVG(e.Salary) AS [AverageSalary]
	FROM Employees AS e
	GROUP BY DepartmentID
) AS Average
