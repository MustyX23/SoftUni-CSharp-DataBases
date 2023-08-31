--Create a SQL query that finds the first name, last name and salary for employees with a salary higher than 50000.
--Order the result in decreasing order by salary.

SELECT FirstName, LastName, Salary
FROM Employees
WHERE Salary > 50000
ORDER BY Salary DESC