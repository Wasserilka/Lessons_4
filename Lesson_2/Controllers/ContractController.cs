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
    [Route("api/contract")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private IContractRepository _repository;
        private IMapper _mapper;
        public ContractController(IContractRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("get/all")]
        public IActionResult GetAllContracts()
        {
            var contracts = _repository.GetAllContracts();
            var response = new GetAllContractsResponse()
            {
                Contracts = new List<ContractDto>()
            };

            foreach (Contract contract in contracts)
            {
                response.Contracts.Add(_mapper.Map<ContractDto>(contract));
            }

            return Ok(response);
        }

        [HttpGet("get/{id}")]
        public IActionResult GetContractById([FromRoute] long id)
        {
            var request = new GetContractByIdRequest { Id = id };
            var contract = _repository.GetContractById(request);
            var response = new GetContractByIdResponse();

            response.Contract = _mapper.Map<ContractDto>(contract);

            return Ok(response);
        }

        [HttpPost("create")]
        public IActionResult Post([FromQuery] long clientId, [FromQuery] long jobId)
        {
            var request = new CreateContractRequest { ClientId = clientId, JobId = jobId };
            _repository.CreateContract(request);
            return Ok();
        }

        [HttpDelete("delete/{Id}")]
        public IActionResult Delete([FromRoute] long id)
        {
            var request = new DeleteContractRequest { Id = id };
            _repository.DeleteContract(request);
            return Ok();
        }
    }
}
