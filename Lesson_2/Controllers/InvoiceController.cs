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
    [Route("api/invoice")]
    [Authorize]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private IInvoiceRepository _repository;
        private IMapper _mapper;
        private IGetInvoiceByIdValidator _getInvoiceByIdValidator;
        private ICreateInvoiceValidator _createInvoiceValidator;
        private IDeleteInvoiceValidator _deleteInvoiceValidator;
        public InvoiceController(
            IInvoiceRepository repository, 
            IMapper mapper,
            IGetInvoiceByIdValidator getInvoiceByIdValidator,
            ICreateInvoiceValidator createInvoiceValidator,
            IDeleteInvoiceValidator deleteInvoiceValidator)
        {
            _repository = repository;
            _mapper = mapper;
            _getInvoiceByIdValidator = getInvoiceByIdValidator;
            _createInvoiceValidator = createInvoiceValidator;
            _deleteInvoiceValidator = deleteInvoiceValidator;
        }

        [HttpGet("get/all")]
        public async Task<IActionResult> GetAll()
        {
            var invoices = await _repository.GetAll();
            var response = new GetAllInvoicesResponse()
            {
                Invoices = new List<InvoiceDto>()
            };

            foreach (Invoice invoice in invoices)
            {
                response.Invoices.Add(_mapper.Map<InvoiceDto>(invoice));
            }

            return Ok(response);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetInvoiceById([FromRoute] long id)
        {
            var request = new GetInvoiceByIdRequest { Id = id };
            var validation = new OperationResult<GetInvoiceByIdRequest>(_getInvoiceByIdValidator.ValidateEntity(request));

            if (!validation.Succeed)
            {
                return BadRequest(validation);
            }

            var invoice = await _repository.GetById(request);
            var response = new GetInvoiceByIdResponse();

            response.Invoice = _mapper.Map<InvoiceDto>(invoice);

            return Ok(response);
        }

        [HttpPost("create/contract/{contractId}/task/{taskId}")]
        public async Task<IActionResult> CreateInvoice([FromRoute] long contractId, [FromRoute] long taskId)
        {
            var request = new CreateInvoiceRequest { ContractId = contractId, TaskId = taskId};
            var validation = new OperationResult<CreateInvoiceRequest>(_createInvoiceValidator.ValidateEntity(request));

            if (!validation.Succeed)
            {
                return BadRequest(validation);
            }

            await _repository.Create(request);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteInvoice([FromRoute] long id)
        {
            var request = new DeleteInvoiceRequest { Id = id };
            var validation = new OperationResult<DeleteInvoiceRequest>(_deleteInvoiceValidator.ValidateEntity(request));

            if (!validation.Succeed)
            {
                return BadRequest(validation);
            }

            await _repository.Delete(request);
            return Ok();
        }
    }
}
