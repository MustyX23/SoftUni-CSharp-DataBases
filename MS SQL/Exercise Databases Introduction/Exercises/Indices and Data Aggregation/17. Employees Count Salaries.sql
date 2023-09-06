--Count the salaries of all employees, who don’t have a manager
SELECT COUNT(Salary) AS [COUNT]
FROM Employees
WHERE ManagerID IS NULL