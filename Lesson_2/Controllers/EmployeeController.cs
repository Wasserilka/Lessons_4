using Microsoft.AspNetCore.Mvc;
using Lesson_2.Repositories;
using Lesson_2.Requests;
using Lesson_2.Responses;
using Lesson_2.Models;
using System.Collections.Generic;
using AutoMapper;

namespace Lesson_2.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeRepository _repository;
        private IMapper _mapper;
        public EmployeeController(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("get/all")]
        public IActionResult GetAllEmployees()
        {
            var employees = _repository.GetAllEmployees();
            var response = new GetAllEmployeesResponse()
            {
                Employees = new List<EmployeeDto>()
            };

            foreach (Employee employee in employees)
            {
                response.Employees.Add(_mapper.Map<EmployeeDto>(employee));
            }

            return Ok(response);
        }

        [HttpGet("get/{id}")]
        public IActionResult GetEmployeeById([FromRoute] long id)
        {
            var request = new GetEmployeeByIdRequest { Id = id };
            var employee = _repository.GetEmployeeById(request);
            var response = new GetEmployeeByIdResponse();
            
            response.Employee = _mapper.Map<EmployeeDto>(employee);

            return Ok(response);
        }

        [HttpPost("create/{name}")]
        public IActionResult CreateEmployee([FromRoute] string name)
        {
            var request = new CreateEmployeeRequest { Name = name };
            _repository.CreateEmployee(request);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteEmployee([FromRoute] long id)
        {
            var request = new DeleteEmployeeRequest { Id = id };
            _repository.DeleteEmployee(request);
            return Ok();
        }
    }
}
