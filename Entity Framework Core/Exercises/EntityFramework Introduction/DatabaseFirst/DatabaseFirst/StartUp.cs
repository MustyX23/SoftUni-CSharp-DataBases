using SoftUni.Data;
using System.Linq;
using System.Text;

namespace SoftUni 
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();

            string result = GetEmployeesFromResearchAndDevelopment(context);

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
                .OrderBy(e => e.EmployeeId);

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
                .OrderBy(e => e.FirstName);

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
                .ThenByDescending(e => e.FirstName);

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.Name} from {employee.DepartmentName} - ${employee.FormattedSalary}");
            }

            return sb.ToString().TrimEnd();
        }


    }
}

