using SoftUni.Data;
using SoftUni.Models;
using System.Linq;
using System.Text;

namespace SoftUni 
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();

<<<<<<< Updated upstream
            string result = GetAddressesByTown(context);
=======
            string result = GetEmployeesInPeriod(context);
>>>>>>> Stashed changes

            Console.WriteLine(result);
        }
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context.Employees
                .Select(e => new
                {
                    e.EmployeeId,
                    Name = e.FirstName + " " + e.LastName + " " + e.MiddleName,
                    JobInfo = e.JobTitle + " " + $"{e.Salary:f2}"
                })
                .OrderBy(e => e.EmployeeId)
                .ToList();

            foreach (var employee in employees)
            {
                sb.AppendLine(employee.Name +" " + employee.JobInfo);
            }
            
            return sb.ToString().TrimEnd();

        }
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context) 
        {
            StringBuilder sb = new StringBuilder();

            var employees = context.Employees
                .Where(e => e.Salary > 50000)
                .Select(e => new
                {
                    e.FirstName,
                    FirstNameAndSalary = $"{e.FirstName} - {e.Salary:f2}"
                })
                .OrderBy(e => e.FirstName)
                .ToList();

            foreach (var employee in employees)
            {
                sb.AppendLine(employee.FirstNameAndSalary);
            }

            return sb.ToString().TrimEnd();
        }
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context) 
        {
            StringBuilder sb = new StringBuilder();

            var employees = context.Employees
                .Where(e => e.Department.Name == "Research and Development")
                .Select(e => new
                {
                    e.Salary,
                    e.FirstName,
                    Name = $"{e.FirstName} {e.LastName}",
                    DepartmentName = e.Department.Name,
                    FormattedSalary = $"{e.Salary:f2}"
                })
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .ToList();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.Name} from {employee.DepartmentName} - ${employee.FormattedSalary}");
            }

            return sb.ToString().TrimEnd();
        }
        public static string AddNewAddressToEmployee(SoftUniContext context) 
        {
            StringBuilder sb = new StringBuilder();            

            var employee = context.Employees
                .FirstOrDefault(e => e.LastName == "Nakov");

            var address = new Address
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            context.Addresses.Add(address);
            employee.Address = address;
            context.SaveChanges();


            var employees = context.Employees
                .OrderByDescending(e => e.AddressId)
                .Select(e => new {AddressText = e.Address.AddressText })
                .Take(10)
                .ToList();

            foreach (var userEmployee in employees)
            {
                sb.AppendLine(userEmployee.AddressText);
            }

            return sb.ToString().TrimEnd();
                
        }

        public static string GetEmployeesInPeriod(SoftUniContext context) 
        {
            StringBuilder sb = new StringBuilder();
            var employees = context.Employees
                .Take(10)          
                .Select(e => new
                {
                    Name = e.FirstName + " " + e.LastName,                   
                    ManagerName= e.Manager.FirstName + " " + e.Manager.LastName,
                    Projects = e.EmployeesProjects
                    .Where(ep => ep.Project.StartDate.Year >= 2001 
                    && ep.Project.StartDate.Year <= 2003)
                    .Select(ep => new 
                    {
                        ProjectName = ep.Project.Name,
                        StartingDate = ep.Project.StartDate.ToString("M/d/yyyy h:mm:ss tt"),
                        EndingDate = ep.Project.EndDate != null 
                        ? ep.Project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt")
                        : "not finished"
                    })


                })
                .ToList();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.Name} - Manager: {employee.ManagerName}");   
                
                foreach (var project in employee.Projects) 
                {
                    sb.AppendLine($"--{project.ProjectName} - {project.StartingDate} - {project.EndingDate}");
                }
            }


            return sb.ToString().TrimEnd();
            
        }

        public static string GetAddressesByTown(SoftUniContext context) 
        {
            StringBuilder sb = new StringBuilder();

            var addresses = context.Addresses                
                .Select
                (a => new 
                {
                    Address = a.AddressText,
                    Employees = a.Employees.Count,
                    TownName = a.Town.Name

                })
                .OrderByDescending(a => a.Employees)
                .ThenBy(a => a.TownName)
                .ThenBy(a => a.Address)
                .Take(10)
                .ToList();

            foreach (var address in addresses)
            {
                sb.AppendLine($"{address.Address}, {address.TownName} - {address.Employees} employees");
            }

            return sb.ToString().TrimEnd();

        }


    }
}

