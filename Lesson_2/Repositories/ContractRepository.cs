using System.Collections.Generic;
using Lesson_2.Models;
using Lesson_2.Requests;

namespace Lesson_2.Repositories
{
    public interface IContractRepository
    {
        void CreateContract(CreateContractRequest request);
        void DeleteContract(DeleteContractRequest request);
        List<Contract> GetAllContracts();
        Contract GetContractById(GetContractByIdRequest request);
    }
    public class ContractRepository : IContractRepository
    {
        IDataRepository _data;

        public ContractRepository(IDataRepository data)
        {
            _data = data;
        }

        public void CreateContract(CreateContractRequest request)
        {
            _data.contractCounter++;

            Job _job = null;
            Client _client = null;

            foreach (Job item in _data.jobs)
            {
                if (item.Id == request.JobId)
                {
                    _job = item;
                    break;
                }
            }

            foreach (Client item in _data.clients)
            {
                if (item.Id == request.ClientId)
                {
                    _client = item;
                    break;
                }
            }

            if (_job != null && _client != null)
            {
                _data.contracts.Add(new Contract { Id = _data.contractCounter, Client = _client, Job = _job });
            }
        }

        public void DeleteContract(DeleteContractRequest request)
        {
            foreach (Contract item in _data.contracts)
            {
                if (item.Id == request.Id)
                {
                    _data.contracts.Remove(item);
                    break;
                }
            }
        }

        public List<Contract> GetAllContracts()
        {
            return _data.contracts;
        }

        public Contract GetContractById(GetContractByIdRequest request)
        {
            foreach (Contract contract in _data.contracts)
            {
                if (contract.Id == request.Id)
                {
                    return contract;
                }
            }

            return null;
        }
    }
}
