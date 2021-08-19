using System.Collections.Generic;
using Lesson_2.Models;

namespace Lesson_2.Repositories
{
    public interface IEmployeeRepository
    {
        void CreateEmployee(long _employeeId);
        void DeleteEmployee(long _employeeId);
        List<Employee> GetAllEmployees();
    }
    public class EmployeeRepository : IEmployeeRepository
    {
        List<Employee> employees;

        public EmployeeRepository()
        {
            employees = new List<Employee>();
        }

        public void CreateEmployee(long _employeeId)
        {
            foreach (Employee item in employees)
            {
                if (item.employeeId == _employeeId)
                {
                    return;
                }
            }

            employees.Add(new Employee { employeeId = _employeeId });
        }

        public void DeleteEmployee(long _employeeId)
        {
            foreach (Employee item in employees)
            {
                if (item.employeeId == _employeeId)
                {
                    employees.Remove(item);
                    break;
                }
            }
        }

        public List<Employee> GetAllEmployees()
        {
            return employees;
        }
    }
}
