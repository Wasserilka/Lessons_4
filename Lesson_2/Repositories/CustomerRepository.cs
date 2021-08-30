using System.Collections.Generic;
using Timesheets.Requests;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Model = Timesheets.Models;
using System;

namespace Timesheets.Repositories
{
    public interface ICustomerRepository
    {
        Task Create(CreateCustomerRequest request);
        Task Delete(DeleteCustomerRequest request);
        Task<List<Model.Customer>> GetAll();
        Task<Model.Customer> GetById(GetCustomerByIdRequest request);
    }
    public class CustomerRepository : ICustomerRepository
    {
        TimesheetsDbContext _context;

        public CustomerRepository(TimesheetsDbContext context)
        {
            _context = context;
        }

        public async Task Create(CreateCustomerRequest request)
        {
            try
            {
                var lastItem = await _context
                    .Customers
                    .OrderBy(x => x.Id)
                    .LastOrDefaultAsync();
                var id = lastItem != null ? lastItem.Id + 1 : 1;
                var factory = new Model.CustomerFactory();
                var item = factory.Create(id, request.Name);

                await _context.Customers.AddAsync(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
        }

        public async Task Delete(DeleteCustomerRequest request)
        {
            try
            {
                var item = await _context
                    .Customers
                    .Where(x => x.Id == request.Id)
                    .SingleOrDefaultAsync();

                _context.Customers.Remove(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
        }

        public async Task<List<Model.Customer>> GetAll()
        {
            try
            {
                return await _context
                    .Customers
                    .Include(x => x.Contracts)
                    .ToListAsync();
            }
            catch (Exception)
            {

            }

            return null;
        }

        public async Task<Model.Customer> GetById(GetCustomerByIdRequest request)
        {
            try
            {
                return await _context
                    .Customers
                    .Where(x => x.Id == request.Id)
                    .Include(x => x.Contracts)
                    .SingleOrDefaultAsync();
            }
            catch (Exception)
            {

            }

            return null;
        }
    }
}
