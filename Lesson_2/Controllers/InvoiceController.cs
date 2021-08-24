using Microsoft.AspNetCore.Mvc;
using Timesheets.Repositories;
using Timesheets.Requests;
using Timesheets.Responses;
using Timesheets.Models;
using System.Collections.Generic;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Timesheets.Controllers
{
    [Route("api/invoice")]
    [Authorize]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private IInvoiceRepository _repository;
        private IMapper _mapper;
        public InvoiceController(IInvoiceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
            var invoice = await _repository.GetById(request);
            var response = new GetInvoiceByIdResponse();

            response.Invoice = _mapper.Map<InvoiceDto>(invoice);

            return Ok(response);
        }

        [HttpPost("create/contract/{contractId}/task/{taskId}")]
        public async Task<IActionResult> CreateInvoice([FromRoute] long contractId, [FromRoute] long taskId)
        {
            var request = new CreateInvoiceRequest { ContractId = contractId, TasktId = taskId};
            await _repository.Create(request);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteInvoice([FromRoute] long id)
        {
            var request = new DeleteInvoiceRequest { Id = id };
            await _repository.Delete(request);
            return Ok();
        }
    }
}
