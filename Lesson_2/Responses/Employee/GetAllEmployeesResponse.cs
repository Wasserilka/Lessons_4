using System.Collections.Generic;

namespace Timesheets.Responses
{
    public class GetAllEmployeesResponse
    {
        public List<EmployeeDto> Employees { get; set; }
    }
}
