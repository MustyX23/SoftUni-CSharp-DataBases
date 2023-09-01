--Create a SQL query that finds the first and last names of the 5 best-paid Employees, ordered descending by their salary.
SELECT TOP 5 FirstName, LastName
FROM Employees
ORDER BY Salary DESC