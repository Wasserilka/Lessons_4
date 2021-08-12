using System.Collections.Generic;
using Lesson_2.Models;

namespace Lesson_2.Repositories
{
    public interface IDataRepository
    {
        public List<Employee> employees { get; set; }
        public long employeeCounter { get; set; }
        public List<Job> jobs { get; set; }
        public long jobCounter { get; set; }
        public List<Contract> contracts { get; set; }
        public long contractCounter { get; set; }
        public List<Client> clients { get; set; }
        public long clientCounter { get; set; }
        public List<Invoice> invoices { get; set; }
        public long invoiceCounter { get; set; }
    }
    public class DataRepository : IDataRepository
    {
        public List<Employee> employees { get; set; }
        public long employeeCounter { get; set; }
        public List<Job> jobs { get; set; }
        public long jobCounter { get; set; }
        public List<Contract> contracts { get; set; }
        public long contractCounter { get; set; }
        public List<Client> clients { get; set; }
        public long clientCounter { get; set; }
        public List<Invoice> invoices { get; set; }
        public long invoiceCounter { get; set; }

        public DataRepository()
        {
            employees = new List<Employee>();
            jobs = new List<Job>();
            contracts = new List<Contract>();
            clients = new List<Client>();
            invoices = new List<Invoice>();

            employeeCounter = 0;
            jobCounter = 0;
            contractCounter = 0;
            clientCounter = 0;
            invoiceCounter = 0;
        }
    }
}
