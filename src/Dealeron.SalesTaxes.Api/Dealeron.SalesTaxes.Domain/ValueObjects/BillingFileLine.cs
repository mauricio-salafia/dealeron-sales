using Dealeron.SalesTaxes.Domain.Models;

namespace Dealeron.SalesTaxes.Domain.ValueObjects
{
    public class BillingFileLine
    {
        public int Quantity { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<Category> Categories { get; set; }
        public decimal TaxPrice { get; set; }
    }
}
