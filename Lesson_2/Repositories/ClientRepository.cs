using System.Collections.Generic;
using Lesson_2.Models;

namespace Lesson_2.Repositories
{
    public interface IClientRepository
    {
        void CreateClient(long _clientId);
        void DeleteClient(long _clientId);
        void PutInvoiceToClient(long _clientId, long _incvoiceId);
        List<Client> GetAllClients();
    }
    public class ClientRepository : IClientRepository
    {
        List<Client> clients;

        public ClientRepository()
        {
            clients = new List<Client>();
        }

        public void CreateClient(long _clientId)
        {
            foreach (Client item in clients)
            {
                if (item.clientId == _clientId)
                {
                    return;
                }
            }

            clients.Add(new Client { clientId = _clientId });
        }

        public void DeleteClient(long _clientId)
        {
            foreach (Client item in clients)
            {
                if (item.clientId == _clientId)
                {
                    clients.Remove(item);
                    break;
                }
            }
        }

        public List<Client> GetAllClients()
        {
            return clients;
        }

        public void PutInvoiceToClient(long _clientId, long _incvoiceId)
        {
            foreach (Client item in clients)
            {
                if (item.clientId == _clientId)
                {
                    item.invoice = _incvoiceId;
                    break;
                }
            }
        }
    }
}
