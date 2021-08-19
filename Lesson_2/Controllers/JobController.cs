using Microsoft.AspNetCore.Mvc;
using Lesson_2.Repositories;
using Lesson_2.Requests;
using Lesson_2.Responses;
using Lesson_2.Models;
using System.Collections.Generic;
using System;
using AutoMapper;

namespace Lesson_2.Controllers
{
    [Route("api/job")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private IJobRepository _repository;
        private IMapper _mapper;
        public JobController(IJobRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("get/all")]
        public IActionResult GetAllJobs()
        {
            var jobs = _repository.GetAllJobs();
            var response = new GetAllJobsResponse()
            {
                Jobs = new List<JobDto>()
            };

            foreach (Job job in jobs)
            {
                response.Jobs.Add(_mapper.Map<JobDto>(job));
            }

            return Ok(response);
        }

        [HttpGet("get/{id}")]
        public IActionResult GetJobById([FromRoute] long id)
        {
            var request = new GetJobByIdRequest { Id = id };
            var job = _repository.GetJobById(request);
            var response = new GetJobByIdResponse();
            
            response.Job = _mapper.Map<JobDto>(job);

            return Ok(response);
        }

        [HttpPost("create")]
        public IActionResult CreateJob([FromQuery] DateTimeOffset start, [FromQuery] DateTimeOffset end, [FromQuery] double price)
        {
            var request = new CreateJobRequest { Start = start, End = end, PricePerHour = price };
            _repository.CreateJob(request);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteJob([FromRoute] long id)
        {
            var request = new DeleteJobRequest { Id = id };
            _repository.DeleteJob(request);
            return Ok();
        }

        [HttpPut("add/{jobId}/employee/{employeeId}")]
        public IActionResult AddEmployeeToJob([FromRoute] long jobId, [FromRoute] long employeeId)
        {
            var request = new AddEmployeeToJobRequest { JobId = jobId, EmployeeId = employeeId };
            _repository.AddEmployeeToJob(request);
            return Ok();
        }

        [HttpPut("remove/{jobId}/employee/{employeeId}")]
        public IActionResult RemoveEmployeeFromJob([FromRoute] long jobId, [FromRoute] long employeeId)
        {
            var request = new RemoveEmployeeFromJobRequest { JobId = jobId, EmployeeId = employeeId };
            _repository.RemoveEmployeeFromJob(request);
            return Ok();
        }
    }
}
