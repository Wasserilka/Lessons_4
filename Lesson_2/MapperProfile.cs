using AutoMapper;
using Timesheets.Responses;
using Timesheets.Requests;
using Timesheets.Models;

namespace Timesheets
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Task, TaskDto>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<Contract, ContractDto>();
            CreateMap<Invoice, InvoiceDto>();
        }
    }
}
