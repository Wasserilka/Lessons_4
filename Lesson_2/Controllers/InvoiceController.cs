using Microsoft.AspNetCore.Mvc;
using Lesson_2.Repositories;

namespace Lesson_2.Controllers
{
    [Route("api/invoice")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private IInvoiceRepository _repository;
        public InvoiceController(IInvoiceRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("get")]
        public IActionResult Get()
        {
            var invoices = _repository.GetAllInvoices();
            return Ok(invoices);
        }

        [HttpPost("create/{invoiceId}")]
        public IActionResult Post([FromRoute] long invoiceId)
        {
            _repository.CreateInvoice(invoiceId);
            return Ok();
        }

        [HttpDelete("delete/{invoiceId}")]
        public IActionResult Delete([FromRoute] long invoiceId)
        {
            _repository.DeleteInvoice(invoiceId);
            return Ok();
        }

        [HttpPut("put/{invoiceId}/contract/{contractId}")]
        public IActionResult PutContract([FromRoute] long invoiceId, [FromRoute] long contractId)
        {
            _repository.PutContractToInvoice(invoiceId, contractId);
            return Ok();
        }
    }
}
