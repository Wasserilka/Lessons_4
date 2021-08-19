using Microsoft.AspNetCore.Mvc;
using Lesson_2.Repositories;


namespace Lesson_2.Controllers
{
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private IClientRepository _repository;
        public ClientController(IClientRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("get")]
        public IActionResult Get()
        {
            var clients = _repository.GetAllClients();
            return Ok(clients);
        }

        [HttpPost("create/{clientId}")]
        public IActionResult Post([FromRoute] long clientId)
        {
            _repository.CreateClient(clientId);
            return Ok();
        }

        [HttpDelete("delete/{clientId}")]
        public IActionResult Delete([FromRoute] long clientId)
        {
            _repository.DeleteClient(clientId);
            return Ok();
        }

        [HttpPut("put/{clientId}/invoice/{invoiceId}")]
        public IActionResult PutInvoice([FromRoute] long clientId, [FromRoute] long invoiceId)
        {
            _repository.PutInvoiceToClient(clientId, invoiceId);
            return Ok();
        }
    }
}
