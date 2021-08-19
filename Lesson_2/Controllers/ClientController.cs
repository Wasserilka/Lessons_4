using Microsoft.AspNetCore.Mvc;
using Lesson_2.Repositories;
using Lesson_2.Requests;
using Lesson_2.Responses;
using Lesson_2.Models;
using System.Collections.Generic;
using AutoMapper;

namespace Lesson_2.Controllers
{
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private IClientRepository _repository;
        private IMapper _mapper;
        public ClientController(IClientRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("get/all")]
        public IActionResult GetAllClients()
        {
            var clients = _repository.GetAllClients();
            var response = new GetAllClientsResponse()
            {
                Clients = new List<ClientDto>()
            };

            foreach (Client client in clients)
            {
                response.Clients.Add(_mapper.Map<ClientDto>(client));
            }

            return Ok(response);
        }

        [HttpGet("get/{id}")]
        public IActionResult GetClientById([FromRoute] long id)
        {
            var request = new GetClientByIdRequest { Id = id };
            var client = _repository.GetClientById(request);
            var response = new GetClientByIdResponse();

            response.Client = _mapper.Map<ClientDto>(client);

            return Ok(response);
        }

        [HttpPost("create/{name}")]
        public IActionResult Post([FromRoute] string name)
        {
            var request = new CreateClientRequest { Name = name };
            _repository.CreateClient(request);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete([FromRoute] long id)
        {
            var request = new DeleteClientRequest { Id = id };
            _repository.DeleteClient(request);
            return Ok();
        }
    }
}
