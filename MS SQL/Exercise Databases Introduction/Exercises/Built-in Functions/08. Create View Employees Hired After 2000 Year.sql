CREATE VIEW V_EmployeesHiredAfter2000  AS 
SELECT FirstName, LastName
FROM Employees
WHERE Year(HireDate) > 2000