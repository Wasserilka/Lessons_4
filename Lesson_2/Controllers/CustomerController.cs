using Microsoft.AspNetCore.Mvc;
using Timesheets.Repositories;
using Timesheets.Requests;
using Timesheets.Responses;
using Timesheets.Models;
using System.Collections.Generic;
using AutoMapper;
using System.Threading.Tasks;

namespace Timesheets.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerRepository _customerRepository;
        private IContractRepository _contractRepository;
        private IMapper _mapper;
        public CustomerController(ICustomerRepository customerRepository, IContractRepository contractRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _contractRepository = contractRepository;
            _mapper = mapper;
        }

        [HttpGet("get/all")]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerRepository.GetAll();
            var response = new GetAllCustomersResponse()
            {
                Customers = new List<CustomerDto>()
            };

            foreach (Customer customer in customers)
            {
                response.Customers.Add(_mapper.Map<CustomerDto>(customer));
            }

            return Ok(response);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            var request = new GetCustomerByIdRequest { Id = id };
            var customer = await _customerRepository.GetById(request);
            var response = new GetCustomerByIdResponse();

            response.Customer = _mapper.Map<CustomerDto>(customer);

            return Ok(response);
        }

        [HttpPost("create/{name}")]
        public async Task<IActionResult> Create([FromRoute] string name)
        {
            var request = new CreateCustomerRequest { Name = name };
            await _customerRepository.Create(request);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var request = new DeleteCustomerRequest { Id = id };
            await _customerRepository.Delete(request);
            return Ok();
        }

        [HttpGet("get/{customerId}/contracts/all")]
        public async Task<IActionResult> GetAllContracts([FromRoute] long customerId)
        {
            var request = new GetAllContractsRequest { CustomerId = customerId };
            var contracts = await _contractRepository.GetAll(request);
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

        [HttpGet("get/{customerId}/contracts/{id}")]
        public async Task<IActionResult> GetContractById([FromRoute] long customerId, [FromRoute] long id)
        {
            var request = new GetContractByIdRequest { CustomerId = customerId, Id = id };
            var contract = await _contractRepository.GetById(request);
            var response = new GetContractByIdResponse();

            response.Contract = _mapper.Map<ContractDto>(contract);

            return Ok(response);
        }

        [HttpPost("create/{customerId}/contract/{name}")]
        public async Task<IActionResult> CreateContract([FromRoute] long customerId, [FromRoute] string name)
        {
            var request = new CreateContractRequest { CustomerId = customerId, Name = name };
            await _contractRepository.Create(request);
            return Ok();
        }

        [HttpDelete("delete/{customerId}/contract/{id}")]
        public async Task<IActionResult> DeleteContract([FromRoute] long customerId, [FromRoute] long id)
        {
            var request = new DeleteContractRequest { CustomerId = customerId, Id = id };
            await _contractRepository.Delete(request);
            return Ok();
        }
    }
}
