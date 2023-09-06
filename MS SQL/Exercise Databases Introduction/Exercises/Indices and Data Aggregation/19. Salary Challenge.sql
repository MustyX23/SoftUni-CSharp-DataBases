WITH DepartmentAvgSalaries AS (
    SELECT
        DepartmentID,
        AVG(Salary) AS AvgSalary
    FROM Employees
    GROUP BY DepartmentID
)

SELECT TOP 10
    e.FirstName,
    e.LastName,
    e.DepartmentID
FROM Employees AS e
JOIN DepartmentAvgSalaries AS d ON e.DepartmentID = d.DepartmentID
WHERE e.Salary > d.AvgSalary
ORDER BY e.DepartmentID;