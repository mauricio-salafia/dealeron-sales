using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dealeron.SalesTaxes.Application.Common.Interfaces;

namespace Dealeron.SalesTaxes.Infrastructure.Persistence
{
    public class SaleTaxesRepository<T> : ISalesTaxesRepository<T> where T : class
    {
        private readonly ISaleTaxesContext _context;

        public SaleTaxesRepository(ISaleTaxesContext context) 
        { 
            _context = context;
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await _context.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync();
            return result;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<T> FindByIdAsync(int id, string navigation = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _context.FindAsync<T>(new object[] {id}, cancellationToken: cancellationToken);
            return response;
        }

        public async Task<IEnumerable<T>> GetAsync(int pageIndex = 0, int pageSize = 1, Expression<Func<T, bool>> filter = null)
        {
            var response = await _context.GetAsync(pageIndex, pageSize, filter);
            return response;
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _context.UpdateAsync(entity, cancellationToken);
            await _context.SaveChangesAsync();
        }
    }
}
