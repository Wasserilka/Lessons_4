using Microsoft.AspNetCore.Mvc;
using Timesheets.Repositories;
using Timesheets.Requests;
using Timesheets.Responses;
using Timesheets.Models;
using System.Collections.Generic;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Timesheets.Validation;
using Timesheets.Validation.Requests;

namespace Timesheets.Controllers
{
    [Route("api/customer")]
    [Authorize]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerRepository _customerRepository;
        private IContractRepository _contractRepository;
        private IMapper _mapper;
        private IGetContractByIdValidator _getContractByIdValidator;
        private IGetAllContractsValidator _getAllContractsValidator;
        private ICreateContractValidator _createContractValidator;
        private IDeleteContractValidator _deleteContractValidator;
        private IGetCustomerByIdValidator _getCustomerByIdValidator;
        private ICreateCustomerValidator _createCustomerValidator;
        private IDeleteCustomerValidator _deleteCustomerValidator;

        public CustomerController(
            ICustomerRepository customerRepository, 
            IContractRepository contractRepository, 
            IMapper mapper,
            IGetContractByIdValidator getContractByIdValidator,
            IGetAllContractsValidator getAllContractsValidator,
            ICreateContractValidator createContractValidator,
            IDeleteContractValidator deleteContractValidator,
            IGetCustomerByIdValidator getCustomerByIdValidator,
            ICreateCustomerValidator createCustomerValidator,
            IDeleteCustomerValidator deleteCustomerValidator)
        {
            _customerRepository = customerRepository;
            _contractRepository = contractRepository;
            _mapper = mapper;
            _getContractByIdValidator = getContractByIdValidator;
            _getAllContractsValidator = getAllContractsValidator;
            _createContractValidator = createContractValidator;
            _deleteContractValidator = deleteContractValidator;
            _getCustomerByIdValidator = getCustomerByIdValidator;
            _createCustomerValidator = createCustomerValidator;
            _deleteCustomerValidator = deleteCustomerValidator;
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
            var validation = new OperationResult<GetCustomerByIdRequest>(_getCustomerByIdValidator.ValidateEntity(request));

            if (!validation.Succeed)
            {
                return BadRequest(validation);
            }

            var customer = await _customerRepository.GetById(request);
            var response = new GetCustomerByIdResponse();

            response.Customer = _mapper.Map<CustomerDto>(customer);

            return Ok(response);
        }

        [HttpPost("create/{name}")]
        public async Task<IActionResult> Create([FromRoute] string name)
        {
            var request = new CreateCustomerRequest { Name = name };
            var validation = new OperationResult<CreateCustomerRequest>(_createCustomerValidator.ValidateEntity(request));

            if (!validation.Succeed)
            {
                return BadRequest(validation);
            }

            await _customerRepository.Create(request);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var request = new DeleteCustomerRequest { Id = id };
            var validation = new OperationResult<DeleteCustomerRequest>(_deleteCustomerValidator.ValidateEntity(request));

            if (!validation.Succeed)
            {
                return BadRequest(validation);
            }

            await _customerRepository.Delete(request);
            return Ok();
        }

        [HttpGet("get/{customerId}/contracts/all")]
        public async Task<IActionResult> GetAllContracts([FromRoute] long customerId)
        {
            var request = new GetAllContractsRequest { CustomerId = customerId };
            var validation = new OperationResult<GetContractByIdRequest>(_getAllContractsValidator.ValidateEntity(request));

            if (!validation.Succeed)
            {
                return BadRequest(validation);
            }

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
            var validation = new OperationResult<GetContractByIdRequest>(_getContractByIdValidator.ValidateEntity(request));

            if (!validation.Succeed)
            {
                return BadRequest(validation);
            }

            var contract = await _contractRepository.GetById(request);
            var response = new GetContractByIdResponse();

            response.Contract = _mapper.Map<ContractDto>(contract);

            return Ok(response);
        }

        [HttpPost("create/{customerId}/contract/{name}")]
        public async Task<IActionResult> CreateContract([FromRoute] long customerId, [FromRoute] string name)
        {
            var request = new CreateContractRequest { CustomerId = customerId, Name = name };
            var validation = new OperationResult<CreateContractValidator>(_createContractValidator.ValidateEntity(request));

            if (!validation.Succeed)
            {
                return BadRequest(validation);
            }

            await _contractRepository.Create(request);
            return Ok();
        }

        [HttpDelete("delete/{customerId}/contract/{id}")]
        public async Task<IActionResult> DeleteContract([FromRoute] long customerId, [FromRoute] long id)
        {
            var request = new DeleteContractRequest { CustomerId = customerId, Id = id };
            var validation = new OperationResult<GetContractByIdRequest>(_deleteContractValidator.ValidateEntity(request));

            if (!validation.Succeed)
            {
                return BadRequest(validation);
            }

            await _contractRepository.Delete(request);
            return Ok();
        }
    }
}
