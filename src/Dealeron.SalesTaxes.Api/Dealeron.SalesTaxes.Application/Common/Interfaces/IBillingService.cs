using Dealeron.SalesTaxes.Domain.ValueObjects;

namespace Dealeron.SalesTaxes.Application.Common.Interfaces
{
    public interface IBillingService
    {
        Task<string> CreateBillAsync(BillingFile billingFile, CancellationToken cancellationToken = default(CancellationToken));
    }
}
