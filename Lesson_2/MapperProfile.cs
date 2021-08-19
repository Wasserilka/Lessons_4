using AutoMapper;
using Lesson_2.Responses;
using Lesson_2.Models;

namespace Lesson_2
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Job, JobDto>();
            CreateMap<Client, ClientDto>();
            CreateMap<Contract, ContractDto>();
            CreateMap<Invoice, InvoiceDto>();
        }
    }
}
