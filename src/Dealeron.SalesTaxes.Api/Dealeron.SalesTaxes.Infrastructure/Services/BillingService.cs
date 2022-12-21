using System.Text;
using Dealeron.SalesTaxes.Application.Common.Interfaces;
using Dealeron.SalesTaxes.Domain.ValueObjects;

namespace Dealeron.SalesTaxes.Infrastructure.Services
{
    public class BillingService : IBillingService
    {
        public async Task<string> CreateBillAsync(BillingFile billingFile, CancellationToken cancellationToken = default)
        {
            var lines = billingFile.Lines
                .GroupBy(x => new
                {
                    x.Description,
                    x.TaxPrice,
                    x.Price
                })
                .Select(group => new
                {
                    Count = group.Count(),
                    Description = group.Key.Description,
                    TaxPrice = group.Key.TaxPrice,
                    Price = group.Key.Price
                });

            StringBuilder builder = new StringBuilder();
            decimal totalTaxes = 0;
            decimal total = 0;
            foreach (var line in lines)
            {
                totalTaxes += line.TaxPrice * line.Count;
                total += (line.Price + line.TaxPrice) * line.Count;
                builder.AppendLine($"{line.Description}: {(line.Price + line.TaxPrice) * line.Count} {(line.Count > 1 ? $"({line.Count} @ {line.Price + line.TaxPrice})" : "")}");
            }

            builder.AppendLine($"Sales Taxes: {totalTaxes}");
            builder.Append($"Total: {total}");
            return await Task.FromResult(builder.ToString());
        }
    }
}
