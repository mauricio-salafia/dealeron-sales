using System.Linq.Expressions;

namespace Dealeron.SalesTaxes.Infrastructure.Persistence
{
    public interface ISaleTaxesContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<T> AddAsync<T>(T entity, CancellationToken cancellationToken = default (CancellationToken)) where T : class;
        Task<T> UpdateAsync<T>(T entity, CancellationToken cancellationToken = default(CancellationToken)) where T : class;
        Task<T> FindAsync<T>(object[] keys, CancellationToken cancellationToken = default (CancellationToken)) where T: class;
        Task<IEnumerable<T>> GetAsync<T>(int pageIndex,
            int pageSize,
            Expression<Func<T, bool>> expression = null,
            CancellationToken cancellationToken = default(CancellationToken)) where T : class;

    }
}
