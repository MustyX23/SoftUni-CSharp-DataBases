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

            string result = GetEmployeesFullInformation(context);

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
    }
}

