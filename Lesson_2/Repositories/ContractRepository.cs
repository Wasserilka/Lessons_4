using System.Collections.Generic;
using Lesson_2.Models;

namespace Lesson_2.Repositories
{
    public interface IContractRepository
    {
        void CreateContract(long _contractId);
        void DeleteContract(long _contractId);
        void PutEmployeeToContract(long _contractId, long _employeeId);
        List<Contract> GetAllContracts();
    }
    public class ContractRepository : IContractRepository
    {
        List<Contract> contracts;

        public ContractRepository()
        {
            contracts = new List<Contract>();
        }

        public void CreateContract(long _contractId)
        {
            foreach (Contract item in contracts)
            {
                if (item.contractId == _contractId)
                {
                    return;
                }
            }

            contracts.Add(new Contract { contractId = _contractId });
        }

        public void DeleteContract(long _contractId)
        {
            foreach (Contract item in contracts)
            {
                if (item.contractId == _contractId)
                {
                    contracts.Remove(item);
                    break;
                }
            }
        }

        public List<Contract> GetAllContracts()
        {
            return contracts;
        }

        public void PutEmployeeToContract(long _contractId, long _employeeId)
        {
            foreach (Contract item in contracts)
            {
                if (item.contractId == _contractId && !item.employees.Contains(_employeeId))
                {
                    item.employees.Add(_employeeId);
                    break;
                }
            }
        }
    }
}
