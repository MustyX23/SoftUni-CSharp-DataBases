SELECT CONCAT(FirstName,' ',MiddleName,' ',LastName) AS 'Full Name'
FROM Employees
WHERE Salary = 25000 OR Salary = 14000 OR Salary = 12500 OR Salary = 23600;
--salary is exactly 25000, 14000, 12500 or 23600