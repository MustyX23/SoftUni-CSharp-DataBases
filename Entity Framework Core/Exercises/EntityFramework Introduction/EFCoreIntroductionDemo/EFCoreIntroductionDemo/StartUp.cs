using EFCoreIntroductionDemo.Data;
using EFCoreIntroductionDemo.Models;
using Microsoft.EntityFrameworkCore;

public class StartUp
{
    private static void Main(string[] args)
    {
        var db = new SoftUniContext();

        var employees = db.Employees
            .Where(e => e.Salary > 50000)
            .Include(e => e.Address)
            .Select(e => new
            {
                Name = e.FirstName + " " + e.LastName,
                AddressEmployee = e.Address
            });

        foreach (var employee in employees)
        {
            Console.WriteLine(employee.Name + " " + employee.AddressEmployee);
        }
        


    }
}