--Create a SQL query that finds the first and last names of those employees who do not have a manager.
SELECT FirstName, LastName
FROM Employees
WHERE ManagerID IS NULL