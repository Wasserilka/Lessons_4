using System.Collections.Generic;
using Lesson_2.Models;
using Lesson_2.Requests;

namespace Lesson_2.Repositories
{
    public interface IClientRepository
    {
        void CreateClient(CreateClientRequest request);
        void DeleteClient(DeleteClientRequest request);
        List<Client> GetAllClients();
        Client GetClientById(GetClientByIdRequest request);
    }
    public class ClientRepository : IClientRepository
    {
        IDataRepository _data;

        public ClientRepository(IDataRepository data)
        {
            _data = data;
        }

        public void CreateClient(CreateClientRequest request)
        {
            _data.clientCounter++;
            _data.clients.Add(new Client { Id = _data.clientCounter, Name = request.Name });
        }

        public void DeleteClient(DeleteClientRequest request)
        {
            foreach (Client item in _data.clients)
            {
                if (item.Id == request.Id)
                {
                    _data.clients.Remove(item);
                    break;
                }
            }
        }

        public List<Client> GetAllClients()
        {
            return _data.clients;
        }

        public Client GetClientById(GetClientByIdRequest request)
        {
            foreach (Client client in _data.clients)
            {
                if (client.Id == request.Id)
                {
                    return client;
                }
            }

            return null;
        }
    }
}
