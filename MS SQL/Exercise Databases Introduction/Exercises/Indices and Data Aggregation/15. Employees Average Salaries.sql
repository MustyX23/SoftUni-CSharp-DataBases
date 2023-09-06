SELECT *
INTO NewEmployees
FROM Employees
WHERE Salary > 30000;

DELETE FROM NewEmployees
WHERE ManagerID = 42;

UPDATE NewEmployees
SET Salary = Salary + 5000
WHERE DepartmentID = 1;

SELECT DepartmentID, AVG(Salary) AS AverageSalary
FROM NewEmployees
GROUP BY DepartmentID;