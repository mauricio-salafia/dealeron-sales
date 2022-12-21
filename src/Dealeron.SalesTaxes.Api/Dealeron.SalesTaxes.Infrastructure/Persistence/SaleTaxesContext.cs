using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dealeron.SalesTaxes.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Dealeron.SalesTaxes.Infrastructure.Persistence
{
    public class SaleTaxesContext : DbContext, ISaleTaxesContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public SaleTaxesContext(DbContextOptions<SaleTaxesContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public async Task<T> AddAsync<T>(T entity, CancellationToken cancellationToken = default(CancellationToken)) where T : class
        {
            var result = await base.AddAsync(entity, cancellationToken);
            return result?.Entity;
        }

        public async Task<T> UpdateAsync<T>(T entity, CancellationToken cancellationToken = default(CancellationToken)) where T: class
        {
            var result = await Task.FromResult(base.Update(entity));
            return result?.Entity;
        }

        public async Task<T> FindAsync<T>(object[] keys, 
            CancellationToken cancellationToken = default(CancellationToken)) where T : class
        {
            var result = await base.FindAsync<T>(keys, cancellationToken);
            return result;
        }

        public async Task<IEnumerable<T>> GetAsync<T>(int pageIndex, 
            int pageSize,
            Expression<Func<T, bool>> expression, 
            CancellationToken cancellationToken = default(CancellationToken)) where T : class
        {
            IQueryable<T> query = base.Set<T>();
            if (expression != null)
            {
                query = query.Where(expression);
            }

            var result = query.Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();
            return await Task.FromResult(result);
        }
    }
}
