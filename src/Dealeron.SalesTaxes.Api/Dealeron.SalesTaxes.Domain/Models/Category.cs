using System.Diagnostics.CodeAnalysis;

namespace Dealeron.SalesTaxes.Domain.Models
{
    [ExcludeFromCodeCoverage]
    public sealed class Category
    {
        public int CategoryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal TaxToApply { get; set; }
        public List<Product> Products { get; set; }
    }
}
