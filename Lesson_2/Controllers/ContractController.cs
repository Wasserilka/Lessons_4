using Microsoft.AspNetCore.Mvc;
using Lesson_2.Repositories;

namespace Lesson_2.Controllers
{
    [Route("api/contract")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private IContractRepository _repository;
        public ContractController(IContractRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("get")]
        public IActionResult Get()
        {
            var contracts = _repository.GetAllContracts();
            return Ok(contracts);
        }

        [HttpPost("create/{contractId}")]
        public IActionResult Post([FromRoute] long contractId)
        {
            _repository.CreateContract(contractId);
            return Ok();
        }

        [HttpDelete("delete/{contractId}")]
        public IActionResult Delete([FromRoute] long contractId)
        {
            _repository.DeleteContract(contractId);
            return Ok();
        }

        [HttpPut("put/{contractId}/employee/{employeeId}")]
        public IActionResult PutEmployee([FromRoute] long contractId, [FromRoute] long employeeId)
        {
            _repository.PutEmployeeToContract(contractId, employeeId);
            return Ok();
        }
    }
}
