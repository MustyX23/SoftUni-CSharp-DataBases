CREATE PROCEDURE usp_EmployeesBySalaryLevel @LevelOfSalary NVARCHAR(MAX) 
AS
BEGIN
	SELECT FirstName as [First Name], LastName as [Last Name]
	FROM Employees
	WHERE dbo.ufn_GetSalaryLevel(Salary) = @LevelOfSalary
END


