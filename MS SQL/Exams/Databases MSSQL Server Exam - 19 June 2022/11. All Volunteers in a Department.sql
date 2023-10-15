CREATE FUNCTION udf_GetVolunteersCountFromADepartment (@VolunteersDepartment VARCHAR(100))
RETURNS INT
AS
BEGIN
    DECLARE @VolunteerCount INT;
    SELECT @VolunteerCount = COUNT(*)
    FROM Volunteers V
    JOIN VolunteersDepartments D ON V.DepartmentId = D.Id
    WHERE D.DepartmentName = @VolunteersDepartment;
    RETURN @VolunteerCount;
END;
