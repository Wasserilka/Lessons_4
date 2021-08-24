using Microsoft.AspNetCore.Mvc;
using Timesheets.Repositories;
using Timesheets.Requests;
using Timesheets.Responses;
using Model = Timesheets.Models;
using System.Collections.Generic;
using AutoMapper;
using System.Threading.Tasks;

namespace Timesheets.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeRepository _employeeRepository;
        private ITaskRepository _taskRepository;
        private IMapper _mapper;
        public EmployeeController(IEmployeeRepository employeeRepository, ITaskRepository taskRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        [HttpGet("get/all")]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeRepository.GetAll();
            var response = new GetAllEmployeesResponse()
            {
                Employees = new List<EmployeeDto>()
            };

            foreach (Model.Employee employee in employees)
            {
                response.Employees.Add(_mapper.Map<EmployeeDto>(employee));
            }

            return Ok(response);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            var request = new GetEmployeeByIdRequest { Id = id };
            var employee = await _employeeRepository.GetById(request);
            var response = new GetEmployeeByIdResponse();
            
            response.Employee = _mapper.Map<EmployeeDto>(employee);

            return Ok(response);
        }

        [HttpPost("create/{name}")]
        public async Task<IActionResult> Create([FromRoute] string name)
        {
            var request = new CreateEmployeeRequest { Name = name };
            await _employeeRepository.Create(request);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var request = new DeleteEmployeeRequest { Id = id };
            await _employeeRepository.Delete(request);
            return Ok();
        }

        [HttpGet("get/task/all")]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _taskRepository.GetAll();
            var response = new GetAllTasksResponse()
            {
                Tasks = new List<TaskDto>()
            };

            foreach (Model.Task task in tasks)
            {
                response.Tasks.Add(_mapper.Map<TaskDto>(task));
            }

            return Ok(response);
        }

        [HttpGet("get/task/{id}")]
        public async Task<IActionResult> GetTaskById([FromRoute] long id)
        {
            var request = new GetTaskByIdRequest { Id = id };
            var task = await _taskRepository.GetById(request);
            var response = new GetTaskByIdResponse();

            response.Task = _mapper.Map<TaskDto>(task);

            return Ok(response);
        }

        [HttpPost("create/task/{price}")]
        public async Task<IActionResult> CreateTask([FromRoute] long price)
        {
            var request = new CreateTaskRequest { PricePerHour = price };
            await _taskRepository.Create(request);
            return Ok();
        }

        [HttpDelete("delete/task/{id}")]
        public async Task<IActionResult> DeleteTask([FromRoute] long id)
        {
            var request = new DeleteTaskRequest { Id = id };
            await _taskRepository.Delete(request);
            return Ok();
        }

        [HttpPut("update/task/{id}/employee/add/{employeeId}")]
        public async Task<IActionResult> AddEmployeeToTask([FromRoute] long employeeId, [FromRoute] long id)
        {
            var request = new AddEmployeeToTaskRequest { TaskId = id, EmployeeId = employeeId };
           await _taskRepository.AddEmployeeToTask(request);
            return Ok();
        }

        [HttpPut("update/task/{id}/employee/remove/{employeeId}")]
        public async Task<IActionResult> RemoveEmployeeFromTask([FromRoute] long employeeId, [FromRoute] long id)
        {
            var request = new RemoveEmployeeFromTaskRequest { TaskId = id, EmployeeId = employeeId };
            await _taskRepository.RemoveEmployeeFromTask(request);
            return Ok();
        }

        [HttpPut("update/task/{id}/close")]
        public async Task<IActionResult> CloseTask([FromRoute] long id)
        {
            var request = new CloseTaskRequest { Id = id };
            await _taskRepository.CloseTask(request);
            return Ok();
        }
    }
}
