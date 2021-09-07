using System.Collections.Generic;
using Timesheets.Requests;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Model = Timesheets.Models;
using System;

namespace Timesheets.Repositories
{
    public interface IContractRepository
    {
        Task Create(CreateContractRequest request);
        Task Delete(DeleteContractRequest request);
        Task<List<Model.Contract>> GetAll(GetAllContractsRequest request);
        Task<Model.Contract> GetById(GetContractByIdRequest request);
    }
    public class ContractRepository : IContractRepository
    {
        TimesheetsDbContext _context;

        public ContractRepository(TimesheetsDbContext context)
        {
            _context = context;
        }

        public async Task Create(CreateContractRequest request)
        {
            try
            {
                var lastItem = await _context
                    .Contracts
                    .OrderBy(x => x.Id)
                    .LastOrDefaultAsync();
                var id = lastItem != null ? lastItem.Id + 1 : 1;
                var factory = new Model.ContractFactory();
                var item = factory.Create(id, request.Name, request.CustomerId);

                await _context.Contracts.AddAsync(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
        }

        public async Task Delete(DeleteContractRequest request)
        {
            try
            {
                var item = await _context
                    .Contracts
                    .Where(x => x.CustomerId == request.CustomerId && x.Id == request.Id)
                    .SingleOrDefaultAsync();

                _context.Contracts.Remove(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
        }

        public async Task<List<Model.Contract>> GetAll(GetAllContractsRequest request)
        {
            try
            {
                return await _context
                    .Contracts
                    .Where(x => x.CustomerId == request.CustomerId)
                    .ToListAsync();
            }
            catch (Exception)
            {

            }

            return null;
        }

        public async Task<Model.Contract> GetById(GetContractByIdRequest request)
        {
            try
            {
                return await _context
                    .Contracts
                    .Where(x => x.CustomerId == request.CustomerId && x.Id == request.Id)
                    .SingleOrDefaultAsync();
            }
            catch (Exception)
            {

            }

            return null;
        }
    }
}
