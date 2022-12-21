using System.Linq.Expressions;

namespace Dealeron.SalesTaxes.Application.Common.Interfaces
{
    /// <summary>
    /// Generic Interface
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    public interface ISalesTaxesRepository<T> where T : class
    {
        /// <summary>
        /// Add a new entity of T
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>A task with the ID of the added entity</returns>
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default (CancellationToken));

        /// <summary>
        /// Update the entity
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>A task</returns>
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete the entity with the given id
        /// </summary>
        /// <param name="id">The id to delete the entity</param>
        /// <returns>A task</returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// Look for the entity that matches with the given ID
        /// </summary>
        /// <param name="id">The ID to look for</param>
        /// <param name="navigation">Optional to include navigation properties</param>
        /// <returns>The entity that matches</returns>
        Task<T> FindByIdAsync(int id, string navigation = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get entities with pagination that matches with the given criteria
        /// </summary>
        /// <param name="pageIndex">Current Page Index</param>
        /// <param name="pageSize">Page Size</param>
        /// <param name="filter">Optional filter to apply</param>
        /// <param name="orderBy">Optional sorting</param>
        /// <param name="navigation">Optional includes navigation</param>
        /// <returns>Tasklt&l;IEnumerablelt&;Tgt&;gt&;</returns>
        Task<IEnumerable<T>> GetAsync(int pageIndex = 0, int pageSize = 1, Expression<Func<T, bool>> filter = null);
    }
}
