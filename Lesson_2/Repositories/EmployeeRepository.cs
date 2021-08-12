using System.Collections.Generic;
using Lesson_2.Models;
using Lesson_2.Requests;

namespace Lesson_2.Repositories
{
    public interface IEmployeeRepository
    {
        void CreateEmployee(CreateEmployeeRequest request);
        void DeleteEmployee(DeleteEmployeeRequest request);
        List<Employee> GetAllEmployees();
        Employee GetEmployeeById(GetEmployeeByIdRequest request);
    }
    public class EmployeeRepository : IEmployeeRepository
    {
        IDataRepository _data;

        public EmployeeRepository(IDataRepository data)
        {
            _data = data;
        }

        public void CreateEmployee(CreateEmployeeRequest request)
        {
            _data.employeeCounter++;
            _data.employees.Add(new Employee { Id = _data.employeeCounter, Name = request.Name });
        }

        public void DeleteEmployee(DeleteEmployeeRequest request)
        {
            foreach (Employee item in _data.employees)
            {
                if (item.Id == request.Id)
                {
                    _data.employees.Remove(item);
                    break;
                }
            }
        }

        public List<Employee> GetAllEmployees()
        {
            return _data.employees;
        }

        public Employee GetEmployeeById(GetEmployeeByIdRequest request)
        {
            foreach (Employee employee in _data.employees)
            {
                if(employee.Id == request.Id)
                {
                    return employee;
                }
            }

            return null;
        }
    }
}
