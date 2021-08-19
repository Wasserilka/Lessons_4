using System.Collections.Generic;
using Timesheets.Requests;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Model = Timesheets.Models;
using System;

namespace Timesheets.Repositories
{
    public interface IEmployeeRepository
    {
        Task Create(CreateEmployeeRequest request);
        Task Delete(DeleteEmployeeRequest request);
        Task<List<Model.Employee>> GetAll();
        Task<Model.Employee> GetById(GetEmployeeByIdRequest request);
    }
    public class EmployeeRepository : IEmployeeRepository
    {
        TimesheetsDbContext _context;

        public EmployeeRepository(TimesheetsDbContext context)
        {
            _context = context;
        }

        public async Task Create(CreateEmployeeRequest request)
        {
            try
            {
                var lastItem = await _context
                    .Employees
                    .OrderBy(x => x.Id)
                    .LastOrDefaultAsync();
                var id = lastItem != null ? lastItem.Id + 1 : 1;
                var item = new Model.Employee { Id = id, Name = request.Name };

                await _context.Employees.AddAsync(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
        }

        public async Task Delete(DeleteEmployeeRequest request)
        {
            try
            {
                var item = await _context
                    .Employees
                    .Where(x => x.Id == request.Id)
                    .SingleOrDefaultAsync();

                _context.Employees.Remove(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
        }

        public async Task<List<Model.Employee>> GetAll()
        {
            try
            {
                return await _context.Employees.ToListAsync();
            }
            catch (Exception)
            {

            }

            return null;
        }

        public async Task<Model.Employee> GetById(GetEmployeeByIdRequest request)
        {
            try
            {
                return await _context
                    .Employees
                    .Where(x => x.Id == request.Id)
                    .SingleOrDefaultAsync();
            }
            catch (Exception)
            {

            }

            return null;
        }
    }
}
