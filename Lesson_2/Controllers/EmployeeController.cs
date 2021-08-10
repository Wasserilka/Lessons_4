using Microsoft.AspNetCore.Mvc;
using Lesson_2.Repositories;

namespace Lesson_2.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeRepository _repository;
        public EmployeeController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("get")]
        public IActionResult Get()
        {
            var employees = _repository.GetAllEmployees();
            return Ok(employees);
        }

        [HttpPost("create/{employeeId}")]
        public IActionResult Post([FromRoute] long employeeId)
        {
            _repository.CreateEmployee(employeeId);
            return Ok();
        }

        [HttpDelete("delete/{employeeId}")]
        public IActionResult Delete([FromRoute] long employeeId)
        {
            _repository.DeleteEmployee(employeeId);
            return Ok();
        }
    }
}
