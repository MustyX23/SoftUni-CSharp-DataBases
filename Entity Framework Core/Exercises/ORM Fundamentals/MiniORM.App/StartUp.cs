using MiniORM.App.Data;
using MiniORM.App.Data.Entities;

string connectionString = "Server=.;Database=MiniORM;Trusted_Connection=true;TrustServerCertificate=true";

var context = new SoftUniDbContext(connectionString);

context.Employees.Add(new Employee
{
    FirstName = "Gosho",
    LastName = "Inserted",
    DepartmentId = context.Departments.First().Id,
    IsEmployed = true
});

Employee employee = context.Employees.Last();
employee.FirstName = "Modified";

context.SaveChanges();