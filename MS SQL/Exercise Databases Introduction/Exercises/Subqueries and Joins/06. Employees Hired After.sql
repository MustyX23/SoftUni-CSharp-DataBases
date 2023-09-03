SELECT
    e.FirstName,
    e.LastName,
    e.HireDate,
    CASE
        WHEN d.Name = 'Sales' THEN 'Sales'
        WHEN d.Name = 'Finance' THEN 'Finance'
    END AS DeptName
FROM Employees AS e
JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
WHERE e.HireDate > '1999-01-01' AND (d.Name IN ('Sales', 'Finance'))
ORDER BY e.HireDate ASC;
