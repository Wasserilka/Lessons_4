using System.Collections.Generic;
using Timesheets.Requests;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Model = Timesheets.Models;
using System;

namespace Timesheets.Repositories
{
    public interface IInvoiceRepository
    {
        Task Create(CreateInvoiceRequest request);
        Task Delete(DeleteInvoiceRequest request);
        Task<List<Model.Invoice>> GetAll();
        Task<Model.Invoice> GetById(GetInvoiceByIdRequest request);
    }
    public class InvoiceRepository : IInvoiceRepository
    {
        TimesheetsDbContext _context;

        public InvoiceRepository(TimesheetsDbContext context)
        {
            _context = context;
        }

        public async Task Create(CreateInvoiceRequest request)
        {
            try
            {
                var lastItem = await _context
                    .Invoices
                    .OrderBy(x => x.Id)
                    .LastOrDefaultAsync();
                var id = lastItem != null ? lastItem.Id + 1 : 1;

                var contract = await _context
                    .Contracts
                    .Where(x => x.Id == request.ContractId)
                    .SingleOrDefaultAsync();
                var task = await _context
                    .Tasks
                    .Where(x => x.Id == request.TaskId)
                    .SingleOrDefaultAsync();

                var cost = task.GetCost();
                var factory = new Model.InvoiceFactory();
                var item = factory.Create(id, contract, cost);

                await _context.Invoices.AddAsync(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
        }

        public async Task Delete(DeleteInvoiceRequest request)
        {
            try
            {
                var item = await _context
                    .Invoices
                    .Where(x => x.Id == request.Id)
                    .SingleOrDefaultAsync();

                _context.Invoices.Remove(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
        }

        public async Task<List<Model.Invoice>> GetAll()
        {
            try
            {
                return await _context
                    .Invoices
                    .Include(x => x.Contract)
                    .ToListAsync();
            }
            catch (Exception)
            {

            }

            return null;
        }

        public async Task<Model.Invoice> GetById(GetInvoiceByIdRequest request)
        {
            try
            {
                return await _context
                    .Invoices
                    .Where(x => x.Id == request.Id)
                    .Include(x => x.Contract)
                    .SingleOrDefaultAsync();
            }
            catch (Exception)
            {

            }

            return null;
        }
    }
}
