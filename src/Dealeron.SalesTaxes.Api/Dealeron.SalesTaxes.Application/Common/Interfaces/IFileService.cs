using Dealeron.SalesTaxes.Domain.ValueObjects;

namespace Dealeron.SalesTaxes.Application.Common.Interfaces
{
    public interface IFileService
    {
        Task<BillingFile> ProcessFileAsync(string dataFile, CancellationToken cancellationToken = default(CancellationToken));
    }
}
