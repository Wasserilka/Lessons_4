using Microsoft.AspNetCore.Mvc;
using Lesson_2.Repositories;
using Lesson_2.Requests;
using Lesson_2.Responses;
using Lesson_2.Models;
using System.Collections.Generic;
using AutoMapper;

namespace Lesson_2.Controllers
{
    [Route("api/invoice")]
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
        public IActionResult GetAllInvoices()
        {
            var invoices = _repository.GetAllInvoices();
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
        public IActionResult GetInvoiceById([FromRoute] long id)
        {
            var request = new GetInvoiceByIdRequest { Id = id };
            var invoice = _repository.GetInvoiceById(request);
            var response = new GetInvoiceByIdResponse();

            response.Invoice = _mapper.Map<InvoiceDto>(invoice);

            return Ok(response);
        }

        [HttpPost("create")]
        public IActionResult CreateInvoice([FromQuery] long contractId)
        {
            var request = new CreateInvoiceRequest { ContractId = contractId };
            _repository.CreateInvoice(request);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteInvoice([FromRoute] long id)
        {
            var request = new DeleteInvoiceRequest { Id = id };
            _repository.DeleteInvoice(request);
            return Ok();
        }
    }
}
