using Microsoft.VisualBasic;
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

            string result = RemoveTown(context);

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
        public static string GetEmployee147(SoftUniContext context) 
        {
            StringBuilder sb = new StringBuilder();

            var employee147 = context.Employees
                .Select(e => new {
                    e.EmployeeId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    JobTitle = e.JobTitle,
                    EmployeesProjects = e.EmployeesProjects
                    .Select(ep => new {ProjectName = ep.Project.Name})
                    .OrderBy(ep => ep.ProjectName)
                    .ToArray()
                })
                .FirstOrDefault(e => e.EmployeeId == 147);

            //var projects = context.Projects
            //    .Where(p => p.EmployeesProjects.Any(p => p.EmployeeId == employee147.EmployeeId))
            //    .Select(p => new {ProjectName = p.Name })
            //    .OrderBy(ep => ep.ProjectName);

            sb.AppendLine($"{employee147.FirstName} {employee147.LastName} - {employee147.JobTitle}");

            foreach (var project in employee147.EmployeesProjects)
            {
                sb.AppendLine(project.ProjectName);
            }
            return sb.ToString().TrimEnd();
        }
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context) 
        {
            StringBuilder sb = new StringBuilder();

            var departments = context.Departments
                .Select(d => new
                {
                    DepartmentName = d.Name,
                    ManagerFirstName = d.Manager.FirstName,
                    ManagerLastName = d.Manager.LastName,
                    EmployeesCount = d.Employees.Count(),
                    Employees = d.Employees.Select(e => new
                    {
                        EmployeeFirstName = e.FirstName,
                        EmployeeLastName = e.LastName,
                        EmployeeJobTitle = e.JobTitle,
                    })
                    .OrderBy(e => e.EmployeeFirstName)
                    .ThenBy(e => e.EmployeeLastName)
                    .ToArray()
                })
                .Where(d => d.EmployeesCount > 5)
                .OrderBy(d => d.EmployeesCount)
                .ThenBy(d => d.DepartmentName)
                .ToArray();


            foreach (var department in departments)
            {
                sb.AppendLine($"{department.DepartmentName} - {department.ManagerFirstName} {department.ManagerLastName}");

                foreach (var employee in department.Employees)
                {
                    sb.AppendLine($"{employee.EmployeeFirstName} {employee.EmployeeLastName} - {employee.EmployeeJobTitle}");
                }
            }
            return sb.ToString().TrimEnd();
        }
        public static string GetLatestProjects(SoftUniContext context) 
        {
            StringBuilder sb = new StringBuilder();
            var projects = context.Projects                
                .Select(p => new
                {
                    ProjectName = p.Name,
                    ProjectDescription = p.Description,
                    ProjectStartDate = p.StartDate
                }
                )
                .OrderByDescending(p => p.ProjectStartDate)
                .Take(10)
                .OrderBy(e => e.ProjectName)
                .ToArray();

            foreach ( var project in projects)
            {
                sb.AppendLine(project.ProjectName);
                sb.AppendLine(project.ProjectDescription);
                sb.AppendLine(project.ProjectStartDate.ToString("M/d/yyyy h:mm:ss tt"));
            }

            return sb.ToString().TrimEnd();

        }
        public static string IncreaseSalaries(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            string[] departments = {"Engineering", "Tool Design", "Marketing", "Information Services" };

            var employees = context.Employees
                .Where(e => departments.Contains(e.Department.Name))
                .ToArray();

            foreach(var employee in employees)
            {
                employee.Salary *= 1.12m;
            }

            context.SaveChanges();

            foreach (var employee in employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName))
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:f2})");
            }
            return sb.ToString().TrimEnd();
        }
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context.Employees
                .Where(e => e.FirstName.StartsWith("Sa"))
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToArray();

            //Sairaj Uddin - Scheduling Assistant - ($16000.00)

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary:f2})");
            }

            return sb.ToString().TrimEnd();
        }
        public static string DeleteProjectById(SoftUniContext context) 
        {
            var sb = new StringBuilder();

            var projectToDelete = context.Projects.FirstOrDefault(p => p.ProjectId == 2);

            var employeeProjectToDelete = context.EmployeesProjects
                .Where(p => p.ProjectId == projectToDelete.ProjectId)
                .ToArray();

            context.EmployeesProjects.RemoveRange(employeeProjectToDelete);

            context.Projects.Remove(projectToDelete);

            context.SaveChanges();

            foreach (var project in context.Projects.Take(10))
            {
                sb.AppendLine(project.Name);
            }

            return sb.ToString().TrimEnd();
        }
        public static string RemoveTown(SoftUniContext context) 
        {
            var sb = new StringBuilder();

            var townToDelete = context.Towns.FirstOrDefault(t => t.Name == "Seattle");
            var addressesToDelete = context.Addresses.Where(a => a.Town.Name == "Seattle");

            int count = addressesToDelete.Count();

            var employees = context.Employees.Where(e => addressesToDelete.Any(a => a.AddressId == e.AddressId));

            foreach (var employee in employees)
            {
                employee.AddressId = null;
            }

            context.RemoveRange(addressesToDelete);

            context.Towns.Remove(townToDelete);

            context.SaveChanges();

            sb.AppendLine($"{count} addresses in {townToDelete.Name} were deleted");
           

            return sb.ToString().TrimEnd();
        }


    }
}

